using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTubeVideoSearch
{ 
    public partial class СontentAddInBlackList : Form
    {
        public int numContentAdd = 0;
        public int numContentSearch = 0;
        public bool stateCanceledLoad = false;
        public СontentAddInBlackList()
        {
            InitializeComponent();

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (comboBoxTypeContent.Text == "")
            {
                MessageBox.Show("Выберите тип контента!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxTypeContent.Focus();
                return;
            }
            if (comboBoxSearchMethod.Text == "")
            {
                MessageBox.Show("Выберите метод поиска контента!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxSearchMethod.Focus();
                return;
            }
            if (richTextBoxData.Lines.Length == 0)
            {
                MessageBox.Show("Введите данные!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                richTextBoxData.Focus();
                return;
            }
            panelAdd.Visible = false;
            panelLoading.Visible = true;
            Height = 250;

            LoadingContent();
        }

        private async void LoadingContent()
        {
            progressBarLoading.Maximum = richTextBoxData.Lines.Length*3;
            labelStatus.Text = "Загрузка...";


            for (int i = 0; i < richTextBoxData.Lines.Length; i++)
            {
                string line = richTextBoxData.Lines[i].Trim();

                if (line.Length != 0)
                {
                    string title = "";
                    string id_content = "";
                    numContentSearch++;

                    if (comboBoxTypeContent.Text == comboBoxTypeContent.Items[0].ToString())
                    {
                        ChannelData channelData = new ChannelData();
                        if (comboBoxSearchMethod.Text == comboBoxSearchMethod.Items[0].ToString())
                        {
                            labelChannel.Text = $"ID канала: {line}";

                            labelStatus.Text = "Получение данных...";
                            channelData = await YouTubeApi.GetChannelInfoById(line);
                            progressBarLoading.Increment(1);
                            if (channelData is null)
                            {
                                this.Close();
                                return;
                            }
                            if (!channelData.Success)
                            {
                                progressBarLoading.Increment(1);
                                continue;
                            }
                            title = channelData.Title;
                            id_content = channelData.Id;
                        }
                        else if (comboBoxSearchMethod.Text == comboBoxSearchMethod.Items[1].ToString())
                        {
                            labelChannel.Text = $"Название канала: {line}";

                            labelStatus.Text = "Поиск канала...";
                            ChannelData channelId = await YouTubeApi.GetChannelIdByName(line);
                            progressBarLoading.Increment(1);
                            if (channelId is null)
                            {
                                this.Close();
                                return;
                            }
                            if (!channelId.Success)
                            {
                                progressBarLoading.Increment(2);
                                continue;
                            }

                            labelStatus.Text = "Получение данных...";
                            channelData = await YouTubeApi.GetChannelInfoById(channelId.Id);
                            progressBarLoading.Increment(1);
                            if (channelData is null)
                            {
                                this.Close();
                                return;
                            }
                            if (!channelData.Success)
                            {
                                progressBarLoading.Increment(1);
                                continue;
                            }
                            title = channelData.Title;
                            id_content = channelData.Id;
                        }
                        else if (comboBoxSearchMethod.Text == comboBoxSearchMethod.Items[2].ToString())
                        {
                            labelChannel.Text = $"Ссылка на видео: {line}";

                            labelStatus.Text = "Поиск канала...";
                            ChannelData channelId = await YouTubeApi.GetChannelIdByVideoId(line);
                            progressBarLoading.Increment(1);
                            if (channelId is null)
                            {
                                this.Close();
                                return;
                            }
                            if (!channelId.Success)
                            {
                                progressBarLoading.Increment(2);
                                continue;
                            }

                            labelStatus.Text = "Получение данных...";
                            channelData = await YouTubeApi.GetChannelInfoById(channelId.Id);
                            progressBarLoading.Increment(1);
                            if (channelData is null)
                            {
                                this.Close();
                                return;
                            }
                            if (!channelData.Success)
                            {
                                progressBarLoading.Increment(1);
                                continue;
                            }
                            title = channelData.Title;
                            id_content = channelData.Id;
                        }
                    }
                    else if (comboBoxTypeContent.Text == comboBoxTypeContent.Items[1].ToString())
                    {
                        labelChannel.Text = $"Ссылка на видео: {line}";

                        labelStatus.Text = "Получение данных...";
                        VideoData videoData = await YouTubeApi.GetVideoInfoByIdAsync(line);
                        progressBarLoading.Increment(2);
                        if (videoData is null)
                        {
                            this.Close();
                            return;
                        }
                        if (!videoData.Success)
                        {
                            progressBarLoading.Increment(1);
                            continue;
                        }
                        title = videoData.Title;
                        id_content = videoData.Id;
                    }

                    labelStatus.Text = "Сохранение данных...";

                    object count = await SQLiteManager.QueryScalar($@"
                        SELECT COUNT(*) FROM black_list WHERE id_content = '{id_content}'
                    ");
                    if (Convert.ToInt32(count) != 0)
                    {
                        progressBarLoading.Increment(1);
                        continue;
                    }

                    if (stateCanceledLoad)
                        return;

                    numContentAdd++;
                    await SQLiteManager.QueryExecuteNonQuery($@"
                        INSERT INTO black_list (type, title, id_content)
                        VALUES ('{comboBoxTypeContent.Text}', 
                            '{title}', 
                            '{id_content}');

                        DELETE FROM videos WHERE id_video = '{id_content}';

                        DELETE FROM channels WHERE id_channel = '{id_content}';

                    ");
                    if(comboBoxTypeContent.Text == comboBoxTypeContent.Items[0].ToString())
                    {
                        await SQLiteManager.QueryExecuteNonQuery($@"
                            DELETE FROM themes_videos WHERE video_id = (SELECT id FROM videos WHERE channel_id = (SELECT id FROM channels WHERE id_channel = '{id_content}'));

                            DELETE FROM history_videos  WHERE video_id = (SELECT id FROM videos WHERE channel_id = (SELECT id FROM channels WHERE id_channel = '{id_content}'));

                            DELETE FROM videos WHERE channel_id = (SELECT id FROM channels WHERE id_channel = '{id_content}');

                            DELETE FROM channels WHERE id_channel = '{id_content}';
                        ");
                    }
                    else if (comboBoxTypeContent.Text == comboBoxTypeContent.Items[1].ToString())
                    {
                        await SQLiteManager.QueryExecuteNonQuery($@"
                            DELETE FROM themes_videos WHERE video_id = (SELECT id FROM videos WHERE id_video = '{id_content}');

                            DELETE FROM history_videos  WHERE video_id = (SELECT id FROM videos WHERE id_video = '{id_content}');

                            DELETE FROM videos WHERE id_video = '{id_content}';
                        ");
                    }
                    progressBarLoading.Increment(1);
                }
                else
                {
                    progressBarLoading.Increment(3);
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


        private void comboBoxTypeContent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTypeContent.Text == comboBoxTypeContent.Items[0].ToString())
            {
                comboBoxSearchMethod.Items.Clear();
                comboBoxSearchMethod.Items.Add("идентификатору");
                comboBoxSearchMethod.Items.Add("названию или псевдониму");
                comboBoxSearchMethod.Items.Add("ссылке на видео");
            }
            else if (comboBoxTypeContent.Text == comboBoxTypeContent.Items[1].ToString())
            {
                comboBoxSearchMethod.Items.Clear();
                comboBoxSearchMethod.Items.Add("ссылке");
            }
        }
    }
}
