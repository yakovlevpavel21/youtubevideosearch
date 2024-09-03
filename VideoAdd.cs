using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTubeVideoSearch
{ 
    public partial class VideoAdd : Form
    {
        public int numChannelsAdd = 0;
        public int numVideoAdd = 0;
        public int numVideoSearch = 0;
        public bool stateCanceledLoad = false;
        public VideoAdd()
        {
            InitializeComponent();

            toolTipInfo.SetToolTip(pictureBoxInfoAddVideo, "В случае отсутствия каналов в базе,\n" +
                "они будут добавлены автоматически!\nС таких каналов новые видео\nзагружаться не будут!");
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (richTextBoxData.Lines.Length == 0)
            {
                MessageBox.Show("Введите ссылки!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                richTextBoxData.Focus();
                return;
            }
            panelAdd.Visible = false;
            panelLoading.Visible = true;
            Height = 250;

            LoadingVideos();
        }

        private async void LoadingVideos()
        {
            progressBarLoading.Maximum = richTextBoxData.Lines.Length*4;
            labelStatus.Text = "Загрузка...";


            for (int i = 0; i < richTextBoxData.Lines.Length; i++)
            {
                string line = richTextBoxData.Lines[i].Trim();

                if (line.Length != 0)
                {
                    numVideoSearch++;

                    labelChannel.Text = $"Ссылка на видео: {line}";

                    labelStatus.Text = "Получение ID видео...";
                    string videoId = YouTubeApi.ExtractVideoId(line);
                    progressBarLoading.Increment(1);
                    object countVideoInBlackList = await SQLiteManager.QueryScalar($@"
                        SELECT COUNT(*) FROM black_list WHERE id_content = '{videoId}'
                    ");
                    if (Convert.ToInt32(countVideoInBlackList) != 0)
                    {
                        progressBarLoading.Increment(3);
                        continue;
                    }
                    if (videoId is null)
                    {
                        progressBarLoading.Increment(3);
                        continue;
                    }

                    labelStatus.Text = "Получение данных...";
                    VideoData videoData = await YouTubeApi.GetVideoInfoByIdAsync(videoId);
                    progressBarLoading.Increment(1);
                    if (videoData is null)
                    {
                        Close();
                        return;
                    }
                    if (!videoData.Success)
                    {
                        progressBarLoading.Increment(2);
                        continue;
                    }

                    object count = await SQLiteManager.QueryScalar($@"
                        SELECT COUNT(*) FROM channels WHERE id_channel = '{videoData.ChannelId}'
                    ");
                    if (Convert.ToInt32(count) == 0)
                    {
                        object countInBlackList = await SQLiteManager.QueryScalar($@"
                            SELECT COUNT(*) FROM black_list WHERE id_content = '{videoData.ChannelId}'
                        ");
                        if (Convert.ToInt32(countInBlackList) != 0)
                        {
                            progressBarLoading.Increment(2);
                            continue;
                        }
                        labelStatus.Text = "Добавление канала...";
                        ChannelData channelData = await YouTubeApi.GetChannelInfoById(videoData.ChannelId);
                        if (channelData is null)
                        {
                            Close();
                            return;
                        }
                        if (!channelData.Success)
                        {
                            progressBarLoading.Increment(2);
                            continue;
                        }

                        DateTime createDate = DateTime.Parse(channelData.PublishedRow);

                        if (stateCanceledLoad)
                            return;

                        numChannelsAdd++;
                        await SQLiteManager.QueryExecuteNonQuery($@"
                            INSERT INTO channels (title, nickname, id_channel, create_date, date_limit, num_videos, load_new_videos)
                            VALUES ('{channelData.Title}', 
                                '{channelData.CustomUrl}', 
                                '{channelData.Id}', 
                                '{createDate.ToString("yyyy-MM-ddTHH:mm:ssZ")}', 
                                '{createDate.ToString("yyyy-MM-ddTHH:mm:ssZ")}', 
                                '{channelData.VideoCount}',
                                'False')
                        ");
                    }
                    progressBarLoading.Increment(1);

                    labelStatus.Text = "Сохранение данных...";
                    object count_video = await SQLiteManager.QueryScalar($@"
                        SELECT COUNT(*) FROM videos WHERE id_video = '{videoData.Id}'
                    ");
                    if (Convert.ToInt32(count_video) != 0)
                    {
                        progressBarLoading.Increment(1);
                        continue;
                    }

                    if (stateCanceledLoad)
                        return;

                    numVideoAdd++;
                    await SQLiteManager.QueryExecuteNonQuery($@"
                        INSERT INTO videos (title, id_video, published_at, duration, num_views, channel_id)
                        VALUES ('{videoData.Title}', 
                            '{videoData.Id}', 
                            '{videoData.PublishedRow}', 
                            '{videoData.Duration}', 
                            '{videoData.ViewCount}', 
                            (SELECT id FROM channels WHERE id_channel = '{videoData.ChannelId}'))
                    ");
                    progressBarLoading.Increment(1);
                }
                else
                {
                    progressBarLoading.Increment(4);
                }
            }
            await Task.Delay(1000);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            stateCanceledLoad = true;
            this.Close();
        }
    }
}
