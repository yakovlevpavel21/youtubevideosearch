using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTubeVideoSearch
{
    public partial class Search : Form
    {
        public VideoSortDataSQLite videoSortParams = new VideoSortDataSQLite()
        {
            Title = "",
            Id = "",
            ThemesId = null,
            StartDate = "",
            EndDate = "",
            MinDuration = -1,
            MaxDuration = -1,
            ChannelsId = null,
            Method = -1
        };
        public HistoryDataSqlite _history = null;
        public bool getInDuration = false;
        public int numInputVideos = 0;
        public int totalGetDuration = 0;
        public int totalGetVideos = 0;
        public bool reload_table = false;
        private bool stopLoading = true;
        private bool checkErrorYTAPI = false;

        public Search()
        {
            InitializeComponent();
            ReloadData();
        }

        private async void ReloadData()
        {
            comboBoxMinDurVideo.SelectedIndex = 0;
            comboBoxMaxDurVideo.SelectedIndex = 0;
            comboBoxNumVideo.SelectedIndex = 1;
            comboBoxUsed.SelectedIndex = 5;

            List<HistoryDataSqlite> history = await SQLiteManager.GetHistory(last_history: true);
            List<string> themesInHistory = new List<string>();
            if (history is null) return;
            if (history.Count > 0)
            {
                textBoxMinDurVideo.Text = history[0].MinDuration.ToString();
                textBoxMaxDurVideo.Text = history[0].MaxDuration.ToString();

                themesInHistory = await SQLiteManager.GetThemesInHistory(history: history[0]);
            }

            dataGridViewChoiseThemes.Rows.Clear();
            List<ThemeDataSQLite> themes = await SQLiteManager.GetThemes();
            if (themes is null) return;
            foreach (ThemeDataSQLite theme in themes)
            {
                bool isCheckedTheme = false;
                if (themesInHistory.Count > 0)
                    foreach (string theme_title in themesInHistory)
                    {
                        if (theme.Title == theme_title)
                        {
                            isCheckedTheme = true;
                            break;
                        }
                    }
                dataGridViewChoiseThemes.Rows.Add(new object[]
                {
                    theme.Id,
                    theme.Title,
                    isCheckedTheme
                });
            }
            
            dataGridViewChannelsSort.Rows.Clear();
            List<ChannelDataSQLite> channels = await SQLiteManager.GetChannels();
            if (channels is null) return;
            foreach (ChannelDataSQLite channel in channels)
            {
                dataGridViewChannelsSort.Rows.Add(new object[]
                {
                channel.Id,
                channel.Title,
                true
                });
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            reload_table = false;
            this.Close();
        }

        private async void btnStartSearch_Click(object sender, EventArgs e)
        {
            reload_table = true;
            stopLoading = false;
            if (videoSortParams.ThemesId != null) videoSortParams.ThemesId.Clear();
            else videoSortParams.ThemesId = new List<int>();

            for (int i = 0; i < dataGridViewChoiseThemes.RowCount; i++)
                if (Convert.ToBoolean(dataGridViewChoiseThemes.Rows[i].Cells["ColumnChoise"].Value))
                    videoSortParams.ThemesId.Add(Convert.ToInt32(dataGridViewChoiseThemes.Rows[i].Cells["ColumnNumThemeSearch"].Value));

            videoSortParams.StartDate = dateTimePickerStartSort.Value.ToString("yyyy-MM-ddTHH:mm:ssZ");
            videoSortParams.EndDate = dateTimePickerEndSort.Value.ToString("yyyy-MM-ddTHH:mm:ssZ");

            if (textBoxMinDurVideo.Text != "" && int.TryParse(textBoxMinDurVideo.Text, out int minDurInt))
                videoSortParams.MinDuration = minDurInt * (int)Math.Pow(60, comboBoxMinDurVideo.SelectedIndex);
            else
            {
                videoSortParams.MinDuration = -1;
                MessageBox.Show("Минимальная длительность видео задана неверно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBoxMaxDurVideo.Text != "" && int.TryParse(textBoxMaxDurVideo.Text, out int maxDurInt))
                videoSortParams.MaxDuration = maxDurInt * (int)Math.Pow(60, comboBoxMaxDurVideo.SelectedIndex);
            else
            {
                videoSortParams.MaxDuration = -1;
                MessageBox.Show("Максимальная длительность видео задана неверно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (videoSortParams.MaxDuration < videoSortParams.MinDuration)
            {
                MessageBox.Show("Максимальная длительность видео меньше минимальной!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (videoSortParams.ChannelsId != null) videoSortParams.ChannelsId.Clear();
            else videoSortParams.ChannelsId = new List<int>();

            for (int i = 0; i < dataGridViewChannelsSort.RowCount; i++)
                if (Convert.ToBoolean(dataGridViewChannelsSort.Rows[i].Cells["ColumnChoiseChannel"].Value))
                    videoSortParams.ChannelsId.Add(Convert.ToInt32(dataGridViewChannelsSort.Rows[i].Cells["ColumnNumChoiseChannels"].Value));

            if (comboBoxUsed.SelectedIndex != -1)
                videoSortParams.Method = comboBoxUsed.SelectedIndex;

            if (textBoxNumVideo.Text != "" && int.TryParse(textBoxNumVideo.Text, out int numVideos))
            {
                List<VideoDataSQLite> videoChecked = await SQLiteManager.GetVideos(videoSortParams: videoSortParams, limit: 1);
                if (videoChecked is null)
                    return;
                if (videoChecked.Count == 0)
                {
                    MessageBox.Show("Видео с данными параметрами не найдены!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                panelLoading.Visible = true;
                await Task.Delay(1);
                int? result = await SQLiteManager.InsertHistory(new HistoryDataSqlite()
                {
                    Date = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                    MinDuration = videoSortParams.MinDuration,
                    MaxDuration = videoSortParams.MaxDuration
                });
                if (result is null) return;

                List<HistoryDataSqlite> history = await SQLiteManager.GetHistory(last_history: true);
                if (history is null || history.Count == 0) return;
                _history = history[0];

                foreach (int theme_id in videoSortParams.ThemesId)
                {
                    int? result_theme = await SQLiteManager.InsertHistoryThemes(history[0], new ThemeDataSQLite() { Id = theme_id });
                    if (result_theme is null) return;
                }

                totalGetVideos = 0;
                totalGetDuration = 0;

                if (comboBoxNumVideo.SelectedIndex == 3)
                {
                    getInDuration = false;
                    numInputVideos = numVideos;
                    List<VideoDataSQLite>  videos = await SQLiteManager.GetVideos(videoSortParams: videoSortParams, limit: numVideos, history: history[0]);
                    await Task.Delay(1);
                    if (videos is null)
                        return;

                    foreach (VideoDataSQLite video in videos)
                    {
                        if (stopLoading)
                        {
                            panelLoading.Visible = false;
                            await SQLiteManager.QueryExecuteNonQuery($@"
                                DELETE FROM history_videos WHERE history_id = {_history.Id};

                                DELETE FROM history_themes WHERE history_id = {_history.Id};
                            
                                DELETE FROM history WHERE id = {_history.Id};
                            ");
                            reload_table = false;
                            return;
                        }
                        totalGetVideos++;
                        int? result_video = await SQLiteManager.InsertHistoryVideos(history[0], video);
                        await Task.Delay(1);
                        if (result_video is null) return;
                        if (!checkErrorYTAPI)
                        {
                            VideoData videoData = await YouTubeApi.GetVideoInfoByIdAsync(video.IdVideo);
                            await Task.Delay(1);
                            if (videoData != null)
                            {
                                await SQLiteManager.QueryExecuteNonQuery($@"
                                    UPDATE videos 
                                    SET num_views = {videoData.ViewCount}
                                    WHERE id = {video.Id}
                                ");
                            }
                            else
                            {
                                checkErrorYTAPI = true;
                            }
                        }
                        totalGetDuration += (int)video.Duration;
                        await Task.Delay(1);
                    }
                }
                else
                {
                    getInDuration = true;
                    int totalDuration = numVideos * (int)Math.Pow(60, comboBoxNumVideo.SelectedIndex);
                    numInputVideos = totalDuration;
                    while (totalDuration > 0)
                    {
                        if (stopLoading)
                        {
                            panelLoading.Visible = false;
                            await SQLiteManager.QueryExecuteNonQuery($@"
                                DELETE FROM history_videos WHERE history_id = {_history.Id};

                                DELETE FROM history_themes WHERE history_id = {_history.Id};
                            
                                DELETE FROM history WHERE id = {_history.Id};
                            ");
                            reload_table = false;
                            return;
                        }
                        List<VideoDataSQLite> video = await SQLiteManager.GetVideos(videoSortParams: videoSortParams, limit: 1, history: history[0]);
                        await Task.Delay(1);
                        if (video is null)
                            return;
                        if (video.Count == 1)
                        {
                            totalGetVideos++;
                            totalDuration -= (int)video[0].Duration;
                            int? result_video = await SQLiteManager.InsertHistoryVideos(history[0], video[0]);
                            await Task.Delay(1);
                            if (!checkErrorYTAPI)
                            {
                                VideoData videoData = await YouTubeApi.GetVideoInfoByIdAsync(video[0].IdVideo);
                                await Task.Delay(1);
                                if (videoData != null)
                                {
                                    await SQLiteManager.QueryExecuteNonQuery($@"
                                        UPDATE videos 
                                        SET num_views = {videoData.ViewCount}
                                        WHERE id = {video[0].Id}
                                    ");
                                }
                            else
                            {
                                checkErrorYTAPI = true;
                            }
                            }
                            if (result_video is null) return;
                        }
                        else
                        {
                            break;
                        }
                    }
                    totalGetDuration = numInputVideos - totalDuration;
                }
            }
            else
            {
                MessageBox.Show("Количество видео задано неверно!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxNumVideo.Focus();
                return;
            }
            stopLoading = true;
            Close();
        }

        private void btnDateCreateChannel_Click(object sender, EventArgs e)
        {
            dateTimePickerStartSort.Value = dateTimePickerStartSort.MinDate;
        }

        private void btnDateToday_Click(object sender, EventArgs e)
        {
            dateTimePickerEndSort.Value = DateTime.Now;
        }

        private void btnCancelLoading_Click(object sender, EventArgs e)
        {
            stopLoading = true;
        }

        private void Search_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!stopLoading)
            {
                stopLoading = true;
                e.Cancel = true;
            }
        }
    }
}
