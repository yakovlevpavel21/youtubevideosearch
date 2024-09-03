using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTubeVideoSearch
{ 
    public partial class ChannelAdd : Form
    {
        public int numChannelsAdd = 0;
        public int numChannelsSearch = 0;
        public int numVideoAdd = 0;
        public int numVideoSearch = 0;
        public bool stateCanceledLoad = false;
        public ChannelAdd()
        {
            InitializeComponent();

            toolTipInfo.SetToolTip(btnDateCreateChannel, "Дата создания канала");

            dateTimePickerStart.Value = dateTimePickerStart.MinDate;
            dateTimePickerEnd.Value = DateTime.Now;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (comboBoxSearchChannel.Text == "")
            {
                MessageBox.Show("Выберите метод поиска канала!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxSearchChannel.Focus();
                return;
            }
            if (richTextBoxData.Lines.Length == 0)
            {
                MessageBox.Show("Введите данные!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                richTextBoxData.Focus();
                return;
            }
            if (dateTimePickerStart.Value > dateTimePickerEnd.Value)
            {
                MessageBox.Show("Дата <После> не может быть больше даты <До>!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateTimePickerStart.Focus();
                return;
            }
            panelAdd.Visible = false;
            panelLoading.Visible = true;
            Height = 250;

            LoadingChannels();
        }

        private async void LoadingChannels()
        {
            progressBarLoading.Maximum = richTextBoxData.Lines.Length*4;
            labelStatus.Text = "Загрузка...";


            for (int i = 0; i < richTextBoxData.Lines.Length; i++)
            {
                string line = richTextBoxData.Lines[i].Trim();

                if (line.Length == 0)
                {
                    progressBarLoading.Increment(4);
                    continue;
                }
                ChannelData channelData = new ChannelData();
                numChannelsSearch++;
                if (comboBoxSearchChannel.Text == comboBoxSearchChannel.Items[0].ToString())
                {
                    labelChannel.Text = $"ID канала: {line}";

                    labelStatus.Text = "Получение данных...";
                    channelData = await YouTubeApi.GetChannelInfoById(line);
                    progressBarLoading.Increment(2);
                }
                else if (comboBoxSearchChannel.Text == comboBoxSearchChannel.Items[1].ToString())
                {
                    labelChannel.Text = $"Название канала: {line}";

                    labelStatus.Text = "Поиск канала...";
                    ChannelData channelId = await YouTubeApi.GetChannelIdByName(line);
                    if (channelId is null)
                    {
                        this.Close();
                        return;
                    }
                    if (!channelId.Success)
                    {
                        progressBarLoading.Increment(4);
                        continue;
                    }
                    progressBarLoading.Increment(1);

                    labelStatus.Text = "Получение данных...";
                    channelData = await YouTubeApi.GetChannelInfoById(channelId.Id);
                    progressBarLoading.Increment(1);
                }
                else if (comboBoxSearchChannel.Text == comboBoxSearchChannel.Items[2].ToString())
                {
                    labelChannel.Text = $"Ссылка на видео: {line}";

                    labelStatus.Text = "Поиск канала...";
                    ChannelData channelId = await YouTubeApi.GetChannelIdByVideoId(line);
                    if (channelId is null)
                    {
                        this.Close();
                        return;
                    }
                    if (!channelId.Success)
                    {
                        progressBarLoading.Increment(4);
                        continue;
                    }
                    progressBarLoading.Increment(1);

                    labelStatus.Text = "Получение данных...";
                    channelData = await YouTubeApi.GetChannelInfoById(channelId.Id);
                    progressBarLoading.Increment(1);
                }

                labelStatus.Text = "Сохранение данных...";

                if (channelData is null)
                    return;
                if (!channelData.Success)
                {
                    progressBarLoading.Increment(2);
                    continue;
                }

                object count = await SQLiteManager.QueryScalar($@"
                    SELECT COUNT(*) FROM channels WHERE id_channel = '{channelData.Id}'
                ");
                if (Convert.ToInt32(count) != 0)
                {
                    progressBarLoading.Increment(2);
                    continue;
                }

                object countInBlackList = await SQLiteManager.QueryScalar($@"
                    SELECT COUNT(*) FROM black_list WHERE id_content = '{channelData.Id}'
                ");
                if (Convert.ToInt32(countInBlackList) != 0)
                {
                    progressBarLoading.Increment(2);
                    continue;
                }

                DateTime createDate = DateTime.Parse(channelData.PublishedRow);

                if (stateCanceledLoad)
                    return;
                string loadNewVideos = "False";
                if (checkBoxLoadNew.Checked)
                    loadNewVideos = "True";

                numChannelsAdd++;
                await SQLiteManager.QueryExecuteNonQuery($@"
                    INSERT INTO channels (title, nickname, id_channel, create_date, date_limit, num_videos, load_new_videos)
                    VALUES ('{channelData.Title}', 
                        '{channelData.CustomUrl}', 
                        '{channelData.Id}', 
                        '{createDate.ToString("yyyy-MM-ddTHH:mm:ssZ")}', 
                        '{dateTimePickerEnd.Value.ToString("yyyy-MM-ddTHH:mm:ssZ")}', 
                        '{channelData.VideoCount}',
                        '{loadNewVideos}')
                ");
                labelTotalVideos.Visible = true;
                labelTotalVideos.Text = "Всего видео: " + channelData.VideoCount.ToString();
                progressBarLoading.Increment(1);

                labelStatus.Text = "Загрузка видео с канала...";

                List<VideoData> videosData = await YouTubeApi.LoadingVideosFromChannel(channelData.Id, new DateTimeOffset(dateTimePickerStart.Value), new DateTimeOffset(dateTimePickerEnd.Value));
                numVideoSearch += (int)channelData.VideoCount;
                if (videosData == null)
                {
                    progressBarLoading.Increment(1);
                    continue;
                }
                foreach (VideoData video in videosData)
                {
                    if (stateCanceledLoad)
                        return;
                    object count_videos = await SQLiteManager.QueryScalar($@"
                        SELECT COUNT(*) FROM videos WHERE id_video = '{video.Id}'
                    ");
                    if (Convert.ToInt32(count_videos) != 0)
                        continue;

                    object countVIdeoInBlackList = await SQLiteManager.QueryScalar($@"
                        SELECT COUNT(*) FROM black_list WHERE id_content = '{video.Id}'
                    ");
                    if (Convert.ToInt32(countVIdeoInBlackList) != 0)
                        continue;

                    numVideoAdd++;
                    await SQLiteManager.QueryExecuteNonQuery($@"
                        INSERT INTO videos (title, id_video, published_at, duration, num_views, channel_id)
                        VALUES ('{video.Title}', 
                            '{video.Id}', 
                            '{video.PublishedRow}', 
                            '{video.Duration}', 
                            '{video.ViewCount}', 
                            (SELECT id FROM channels WHERE id_channel = '{channelData.Id}'))
                    ");
                }
                progressBarLoading.Increment(1);
                labelTotalVideos.Visible = false;
            }
            await Task.Delay(1000);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            stateCanceledLoad = true;
            this.Close();
        }

        private void btnDateCreateChannel_Click(object sender, EventArgs e)
        {
            dateTimePickerStart.Value = dateTimePickerStart.MinDate;
        }

        private void btnDateToday_Click(object sender, EventArgs e)
        {
            dateTimePickerEnd.Value = DateTime.Now;
        }
    }
}
