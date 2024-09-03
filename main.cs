using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Diagnostics;

namespace YouTubeVideoSearch
{
    public partial class main : Form
    {
        private int maxNumberOnPage = 200;
        private int numPageSearchVideos = 0;
        private int numPageHistory = 0;
        private int numPageVideos = 0;
        private int numPageChannels = 0;
        private int numPageThemes = 0;
        private int numPageBlackList = 0;

        private int historyId = -1;
        private bool skipLoading = false;

        private bool stateReloadChannels = false;
        private int numChannelsReloaded = 0;
        private int numChannelsDeleted = 0;
        private int numVideoAdded = 0;

        public main()
        {
            SQLiteManager.Initialize("data.db");
            
            InitializeComponent();
        }

        private async void main_Shown(object sender, EventArgs e)
        {
            panelLoading.BringToFront();
            progressBarLoading.Maximum = 5;
            await Task.Delay(500);

            ReloadAPIKeysInTable();

            string LastReloadDate = await SQLiteManager.GetValueStringParameters("LAST_RELOAD_DATE");
            int? FrequencyReloadedHours = await SQLiteManager.GetValueIntParameters("FREQUENCY_RELOADED_HOURS");

            int? countChannels = await SQLiteManager.GetCountChannels();
            if (countChannels == null) return;

            if (LastReloadDate != null && FrequencyReloadedHours.HasValue)
            {
                DateTime LastReloadDateTime = DateTime.Parse(LastReloadDate);
                TimeSpan timeDiff = DateTime.Now - LastReloadDateTime;

                labelLastReloadedDate.Text = "Дата последнего обновления: " + LastReloadDateTime.ToString("HH:mm dd.MM.yyyy");
                if (timeDiff.TotalHours >= FrequencyReloadedHours)
                {
                    btnSkip.Visible = true;
                    progressBarLoading.Maximum += (int)countChannels;
                    labelStatusLoading.Text = "Обновление каналов...";
                    await ReloadChannels();
                    stateReloadChannels = true;
                    progressBarLoading.Value = (int)countChannels;
                }
                if (FrequencyReloadedHours == 168)
                    comboBoxFrequencyReloaded.Text = comboBoxFrequencyReloaded.Items[3].ToString();
                else
                    comboBoxFrequencyReloaded.Text = comboBoxFrequencyReloaded.Items[(int)FrequencyReloadedHours / 24 - 1].ToString();

            }

            labelStatusLoading.Text = "Загрузка таблиц...";
            ReloadHistoryInTable();
            progressBarLoading.Increment(1);
            ReloadChannelsInTable();
            progressBarLoading.Increment(1);
            ReloadVideosInTable();
            progressBarLoading.Increment(1);
            ReloadThemesInTable();
            progressBarLoading.Increment(1);
            ReloadBlackList();
            progressBarLoading.Increment(1);

            await Task.Delay(1000);
            panelLoading.Dispose();
            this.FormBorderStyle = FormBorderStyle.Sizable;

            if (stateReloadChannels)
                MessageBox.Show($"Обнавлено каналов: {numChannelsReloaded} из {countChannels}\n" +
                        $"Удалено каналов: {numChannelsDeleted}\n" +
                        $"Добавлено видео: {numVideoAdded}\n",
                        "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        /// <summary>
        /// забыл постфикс Async 
        /// </summary>
        /// <returns></returns>
        private async Task ReloadChannels()
        {
            List<ChannelDataSQLite> channels = await SQLiteManager.GetChannels();
            if (channels is null)
                return;

            int? FrequencyReloadedHours = await SQLiteManager.GetValueIntParameters("FREQUENCY_RELOADED_HOURS");
            if (FrequencyReloadedHours == null) return;
            DateTime nowDate = DateTime.Now;

            foreach (ChannelDataSQLite channel in channels)
            {
                if (skipLoading) return;
                progressBarLoading.Increment(1);
                DateTime dateLimit = DateTime.Parse(channel.DateLimit);
                TimeSpan timeDiff = nowDate - dateLimit;
                if (channel.LoadNewVideos == "False" || timeDiff.TotalHours < FrequencyReloadedHours)
                    continue;

                ChannelData channelData = await YouTubeApi.GetChannelInfoById(channel.IdChannel);
                if (channelData is null)
                    return;
                if (!channelData.Success)
                {
                    int? resultDeleteChannel = await SQLiteManager.DeleteChannel(channel);
                    if (resultDeleteChannel == null) return;
                    numChannelsDeleted++;
                    continue;
                }

                DateTime createDate = DateTime.Parse(channelData.PublishedRow);

                List<VideoData> videosData = await YouTubeApi.LoadingVideosFromChannel(channelData.Id, new DateTimeOffset(dateLimit), new DateTimeOffset(nowDate));
                if (videosData is null)
                    return;

                foreach (VideoData video in videosData)
                {
                    await SQLiteManager.InsertVideo(video: new VideoDataSQLite
                    {
                        Title = video.Title,
                        IdVideo = video.Id,
                        PublishedAt = video.PublishedRow,
                        Duration = video.Duration,
                        NumViews = video.ViewCount,
                        ChannelId = channel.Id
                    });
                    numVideoAdded++;

                    List<ThemeDataSQLite> themes = await SQLiteManager.GetThemes();
                    if (themes is null) return;
                    foreach (ThemeDataSQLite theme in themes)
                    {
                        List<string> words = await SQLiteManager.GetKeyWordsInTheme(theme);
                        if (words is null) return;
                        foreach (string word in words)
                        {
                            string title_lower = video.Title.ToLower();
                            if (title_lower.Contains(word))
                            {
                                await SQLiteManager.QueryExecuteNonQuery($@"
                                    INSERT INTO themes_videos (theme_id, video_id)
                                    VALUES ('{theme.Id}', (SELECT id FROM videos WHERE id_video = '{video.Id}'))
                                ");
                                break;
                            }
                        }
                    }
                }
                await SQLiteManager.UpdateChannel(channel: new ChannelDataSQLite
                {
                    Id = channel.Id,
                    Title = channelData.Title,
                    Nickname = channelData.CustomUrl,
                    IdChannel = channelData.Id,
                    CreateDate = createDate.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                    DateLimit = nowDate.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                    NumVideos = channelData.VideoCount,
                    LoadNewVideos = channel.LoadNewVideos
                });
                numChannelsReloaded++;
            }
            labelLastReloadedDate.Text = "Дата последнего обновления: " + nowDate.ToString("HH:mm dd.MM.yyyy");
            await SQLiteManager.UpdateValueParameter("LAST_RELOAD_DATE", nowDate.ToString("yyyy-MM-ddTHH:mm:ssZ"));
        }

        private async void ReloadSearchVideosInTable(int? history_id=null)
        {
            if (history_id is null)
            {
                btnPreviousPageSearchVideos.Enabled = false;
                btnNextPageSearchVideos.Enabled = false;
                labelTotalDuration.Text = "Общая длительность: 00:00:00" ;
                labelTotalSearchVideos.Text = $"Всего: 0";
                labelStartEndSearchVideos.Text = "0";
                dataGridViewSearchVideos.Rows.Clear();
                return;
            }

            int? countSearchVideos = await SQLiteManager.GetCountSearchVideos(history: new HistoryDataSqlite
            {
                Id = Convert.ToInt32(history_id),
            });
            if (!countSearchVideos.HasValue)
                return;

            int maxPage = (int)Math.Floor((double)countSearchVideos / maxNumberOnPage);

            if (numPageSearchVideos == 0)
                btnPreviousPageSearchVideos.Enabled = false;
            else if (numPageSearchVideos > 0)
                btnPreviousPageSearchVideos.Enabled = true;

            if (numPageSearchVideos == maxPage)
                btnNextPageSearchVideos.Enabled = false;
            else if (numPageSearchVideos < maxPage)
                btnNextPageSearchVideos.Enabled = true;

            dataGridViewSearchVideos.Rows.Clear();
            List<VideoDataSQLite> videos = await SQLiteManager.GetSearchVideos(history: new HistoryDataSqlite
            {
                Id = Convert.ToInt32(history_id)
            }, maxNumberOnPage, numPageSearchVideos * maxNumberOnPage);
            if (videos is null)
                return;
            int duration = 0;
            foreach (VideoDataSQLite video in videos)
            {
                List<ChannelDataSQLite> channelData = await SQLiteManager.GetChannels(id: (int)video.ChannelId);
                if (channelData is null) return;
                if (channelData.Count > 0)
                {
                    dataGridViewSearchVideos.Rows.Add(new object[]{
                        video.Id,
                        video.Title,
                        video.IdVideo,
                        YouTubeApi.DurationToString((int)video.Duration),
                        video.NumViews,
                        channelData[0].Title
                    });
                    duration += (int)video.Duration;
                }
            }

            labelTotalDuration.Text = "Общая длительность: " + YouTubeApi.DurationToString(duration);
            labelTotalSearchVideos.Text = $"Всего: {countSearchVideos}";
            if (countSearchVideos == 0)
                labelStartEndSearchVideos.Text = "0";
            else
                labelStartEndSearchVideos.Text = $"{numPageSearchVideos * maxNumberOnPage + 1} - {numPageSearchVideos * maxNumberOnPage + dataGridViewSearchVideos.RowCount}";
        }
        private async void ReloadHistoryInTable()
        {
            int? countHistory = await SQLiteManager.GetCountHistory();
            if (countHistory is null) return;
            int maxPage = (int)Math.Floor((double)countHistory / maxNumberOnPage);

            if (numPageHistory == 0)
                btnPreviousPageHistory.Enabled = false;
            else if (numPageHistory > 0)
                btnPreviousPageHistory.Enabled = true;

            if (numPageHistory == maxPage)
                btnNextPageHistory.Enabled = false;
            else if (numPageHistory < maxPage)
                btnNextPageHistory.Enabled = true;

            dataGridViewHistory.Rows.Clear();
            List<HistoryDataSqlite> history = await SQLiteManager.GetHistory(limit: maxNumberOnPage, offset: numPageHistory * maxNumberOnPage);
            if (history is null) return;

            foreach (HistoryDataSqlite hist in history)
            {
                string date_history = "";
                DateTime datetime_history;
                if (DateTime.TryParseExact(hist.Date, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out datetime_history))
                {
                    date_history = datetime_history.ToString("dd.MM.yyyy");
                }

                List<string> themes = await SQLiteManager.GetThemesInHistory(history: hist);
                if (themes is null) return;
                string themes_str = string.Join(", ", themes);

                List<VideoDataSQLite> videos = await SQLiteManager.GetSearchVideos(history: hist);
                if (videos is null) return;
                int duration = 0;
                foreach (VideoDataSQLite video in videos)
                    duration += (int)video.Duration;

                dataGridViewHistory.Rows.Add(new object[]{
                    hist.Id,
                    date_history,
                    themes_str,
                    videos.Count,
                    YouTubeApi.DurationToString(duration),
                    YouTubeApi.DurationToString((int)hist.MinDuration),
                    YouTubeApi.DurationToString((int)hist.MaxDuration),
                });
            }

            labelTotalHistory.Text = $"Всего: {countHistory}";
            if (countHistory == 0)
                labelStartEndHistory.Text = "0";
            else
                labelStartEndHistory.Text = $"{numPageHistory * maxNumberOnPage + 1} - {numPageHistory * maxNumberOnPage + dataGridViewHistory.RowCount}";
        }
        private async void ReloadChannelsInTable()
        {
            int? countChannels = await SQLiteManager.GetCountChannels();
            if (countChannels == null) return;
            int maxPage = (int)Math.Floor((double)countChannels / maxNumberOnPage);

            if (numPageChannels == 0)
                btnPreviousPageChannels.Enabled = false;
            else if (numPageChannels > 0)
                btnPreviousPageChannels.Enabled = true;

            if (numPageChannels == maxPage)
                btnNextPageChannels.Enabled = false;
            else if (numPageChannels < maxPage)
                btnNextPageChannels.Enabled = true;

            dataGridViewChannels.Rows.Clear();
            List<ChannelDataSQLite> channels = await SQLiteManager.GetChannels(limit: maxNumberOnPage, offset: numPageChannels * maxNumberOnPage);
            if (channels is null) return;
            foreach (ChannelDataSQLite channel in channels)
            {
                string create_date_str = "";
                DateTime create_date;
                if (DateTime.TryParseExact(channel.CreateDate, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out create_date))
                {
                    create_date_str = create_date.ToString("dd.MM.yyyy");
                }

                int? numVideos = await SQLiteManager.GetCountVideos(videoSortParams: new VideoSortDataSQLite() { ChannelsId = new List<int>() { (int)channel.Id} });
                if (numVideos is null) return;
                dataGridViewChannels.Rows.Add(new object[] { 
                    channel.Id,
                    channel.Title,
                    channel.Nickname,
                    channel.IdChannel,
                    create_date_str, 
                    numVideos,
                    channel.NumVideos,
                    channel.LoadNewVideos,
                });
            }

            labelTotalChannels.Text = $"Всего: {countChannels}";
            if (countChannels == 0)
                labelStartEndChannels.Text = "0";
            else
                labelStartEndChannels.Text = $"{numPageChannels * maxNumberOnPage + 1} - {numPageChannels * maxNumberOnPage + dataGridViewChannels.RowCount}";
        }
        private async void ReloadVideosInTable(VideoSortDataSQLite videoSortParams = null)
        {
            int? countVideos = await SQLiteManager.GetCountVideos(videoSortParams: videoSortParams);
            if (countVideos is null) return;
            int maxPage = (int)Math.Floor((double)countVideos / maxNumberOnPage);

            if (numPageVideos == 0)
                btnPreviousPageVideos.Enabled = false;
            else if (numPageVideos > 0)
                btnPreviousPageVideos.Enabled = true;

            if (numPageVideos == maxPage)
                btnNextPageVideos.Enabled = false;
            else if (numPageVideos < maxPage)
                btnNextPageVideos.Enabled = true;

            dataGridViewVideos.Rows.Clear();

            List<VideoDataSQLite> videos = await SQLiteManager.GetVideos(videoSortParams: videoSortParams, limit: maxNumberOnPage, offset: numPageVideos * maxNumberOnPage);
            if (videos is null) return;

            foreach (VideoDataSQLite video in videos)
            {
                List<string> themes = await SQLiteManager.GetThemesInVideo(video: video);
                if (themes is null) return;

                string themes_str = string.Join(", ", themes);

                string published_at_str = "";
                DateTime published_at;
                if (DateTime.TryParseExact(video.PublishedAt, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out published_at))
                {
                    published_at_str = published_at.ToString("dd.MM.yyyy");
                }

                List<ChannelDataSQLite> channelData = await SQLiteManager.GetChannels(id: (int)video.ChannelId);
                if (channelData is null) return;
                if (channelData.Count > 0)
                {
                    dataGridViewVideos.Rows.Add(new object[]{
                        video.Id,
                        video.Title,
                        video.IdVideo,
                        themes_str,
                        published_at_str,
                        YouTubeApi.DurationToString((int)video.Duration),
                        channelData[0].Title
                    });
                }
            }

            labelTotalVideos.Text = $"Всего: {countVideos}";
            if (countVideos == 0)
                labelStartEndVideos.Text = "0";
            else
                labelStartEndVideos.Text = $"{numPageVideos * maxNumberOnPage + 1} - {numPageVideos * maxNumberOnPage + dataGridViewVideos.RowCount}";
        }
        private async void ReloadBlackList()
        {
            int? countBlackList = await SQLiteManager.GetCountContentsInBlackList();
            if (countBlackList is null) return;
            int maxPage = (int)Math.Floor((double)countBlackList / maxNumberOnPage);

            if (numPageBlackList == 0)
                btnPreviousPageBlackList.Enabled = false;
            else if (numPageBlackList > 0)
                btnPreviousPageBlackList.Enabled = true;

            if (numPageBlackList == maxPage)
                btnNextPageBlackList.Enabled = false;
            else if (numPageBlackList < maxPage)
                btnNextPageBlackList.Enabled = true;

            dataGridViewBlackList.Rows.Clear();
            List<ContentBlockDataSQLite> contents = await SQLiteManager.GetContentsInBlackList(limit: maxNumberOnPage, offset: numPageBlackList * maxNumberOnPage);
            if (contents is null) return;

            foreach (ContentBlockDataSQLite content in contents)
            {
                dataGridViewBlackList.Rows.Add(new object[]
                {
                    content.Id,
                    content.Type,
                    content.Title,
                    content.IdContent
                });
            }

            labelTotalBlackList.Text = $"Всего: {countBlackList}";
            if (countBlackList == 0)
                labelStartEndBlackList.Text = "0";
            else
                labelStartEndBlackList.Text = $"{numPageBlackList * maxNumberOnPage + 1} - {numPageBlackList * maxNumberOnPage + dataGridViewBlackList.RowCount}";
        }
        private async void ReloadThemesInTable()
        {
            int? countThemes = await SQLiteManager.GetCountThemes();
            if (countThemes is null) return;
            int maxPage = (int)Math.Floor((double)countThemes / maxNumberOnPage);

            if (numPageThemes == 0)
                btnPreviousPageThemes.Enabled = false;
            else if (numPageThemes > 0)
                btnPreviousPageThemes.Enabled = true;

            if (numPageThemes == maxPage)
                btnNextPageThemes.Enabled = false;
            else if (numPageThemes < maxPage)
                btnNextPageThemes.Enabled = true;

            dataGridViewThemes.Rows.Clear();
            List<ThemeDataSQLite> themes = await SQLiteManager.GetThemes(limit: maxNumberOnPage, offset: numPageThemes * maxNumberOnPage);

            foreach (ThemeDataSQLite theme in themes)
            {
                List<string> words = await SQLiteManager.GetKeyWordsInTheme(theme);
                if (words is null) return;
                string words_str = "";
                for (int j = 0; j < words.Count; j++)
                {
                    words_str += words[j];
                    if (j != words.Count - 1)
                        words_str += ", ";
                }

                int? numVideos = await SQLiteManager.GetCountVideosInTheme(theme);
                if (numVideos is null) return;

                dataGridViewThemes.Rows.Add(new object[] {
                    theme.Id,
                    theme.Title,
                    words_str,
                    numVideos
                });
            }

            labelTotalThemes.Text = $"Всего: {countThemes}";
            if (countThemes == 0)
                labelStartEndThemes.Text = "0";
            else
                labelStartEndThemes.Text = $"{numPageThemes * maxNumberOnPage + 1} - {numPageThemes * maxNumberOnPage + dataGridViewThemes.RowCount}";
        }
        private async void ReloadAPIKeysInTable()
        {
            dataGridViewAPIKeys.Rows.Clear();
            List<APIKeyDataSQLite> apiKeys = await SQLiteManager.GetAPIKeys();
            if (apiKeys is null) return;
            YouTubeApi.APIKeysClear();

            foreach (APIKeyDataSQLite apiKey in apiKeys)
            {
                dataGridViewAPIKeys.Rows.Add(new object[]
                {
                    apiKey.Id,
                    apiKey.Value
                });

                YouTubeApi.APIKeyAdd(apiKey.Value);
            }
        }

        // SEARCH
        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (var search = new Search())
            {
                search.FormBorderStyle = FormBorderStyle.FixedDialog;
                search.TopMost = true;
                search.ShowDialog();
                this.Activate();
                if (search.reload_table && search.totalGetVideos > 0)
                {
                    historyId = (int)search._history.Id;
                    ReloadSearchVideosInTable(historyId);
                    ReloadHistoryInTable();
                    if (search.getInDuration)
                    {
                        MessageBox.Show($"Получено видео: {search.totalGetVideos}\n" +
                            $"Общая длительность: {YouTubeApi.DurationToString(search.totalGetDuration)} из {YouTubeApi.DurationToString(search.numInputVideos)}", 
                            "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Получено видео: {search.totalGetVideos} из {search.numInputVideos}\n" +
                            $"Общая длительность: {YouTubeApi.DurationToString(search.totalGetDuration)}",
                            "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        private void btnCopySearchVideos_Click(object sender, EventArgs e)
        {
            List<string> links = new List<string>();
            foreach (DataGridViewRow row in dataGridViewSearchVideos.Rows)
                links.Add(row.Cells["ColumnGetIdVideo"].Value.ToString());

            using (var copyListWin = new CopyListSearchVideos(links))
            {
                copyListWin.FormBorderStyle = FormBorderStyle.FixedDialog;
                copyListWin.TopMost = true;
                copyListWin.ShowDialog();
                this.Activate();
                if (copyListWin.copyed)
                {
                    MessageBox.Show("Ссылки скопированы в буфер обмена!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void btnSaveSearchVideos_Click(object sender, EventArgs e)
        {
            List<string> links = new List<string>();
            foreach (DataGridViewRow row in dataGridViewSearchVideos.Rows)
                links.Add(row.Cells["ColumnGetIdVideo"].Value.ToString());

            using (var saveListWin = new SaveListSearchVideos(links))
            {
                saveListWin.FormBorderStyle = FormBorderStyle.FixedDialog;
                saveListWin.TopMost = true;
                saveListWin.ShowDialog();
                this.Activate();
                if (saveListWin.saved)
                {
                    MessageBox.Show($"Ссылки сохранены в файл: {saveListWin.pathSave}", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void btnDeleteSearchVideos_Click(object sender, EventArgs e)
        {
            dataGridViewSearchVideos.Rows.Clear();
            MessageBox.Show("Список очищен!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void btnPreviousPageSearchVideos_Click(object sender, EventArgs e)
        {
            numPageSearchVideos--;
            ReloadSearchVideosInTable(historyId);
        }
        private void btnNextPageSearchVideos_Click(object sender, EventArgs e)
        {
            numPageSearchVideos++;
            ReloadSearchVideosInTable(historyId);
        }

        // HISTORY
        private async void btnCopyListVideosHistory_Click(object sender, EventArgs e)
        {
            List<string> links = new List<string>();
            DataGridViewSelectedRowCollection rows = dataGridViewHistory.SelectedRows;
            if (rows.Count == 0)
            {
                MessageBox.Show("Выберите нужные истории запросов из таблицы!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            foreach (DataGridViewRow row in rows)
            {
                List<VideoDataSQLite> videos = await SQLiteManager.GetSearchVideos(new HistoryDataSqlite() { Id = Convert.ToInt32(row.Cells["ColumnNumHistory"].Value) });
                if (videos is null) return;
                foreach (VideoDataSQLite video in videos)
                    links.Add(video.IdVideo);
            }

            using (var copyListWin = new CopyListSearchVideos(links))
            {
                copyListWin.FormBorderStyle = FormBorderStyle.FixedDialog;
                copyListWin.TopMost = true;
                copyListWin.ShowDialog();
                this.Activate();
                if (copyListWin.copyed)
                {
                    MessageBox.Show("Ссылки скопированы в буфер обмена!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private async void btnSaveListVideosHistory_Click(object sender, EventArgs e)
        {
            List<string> links = new List<string>();
            DataGridViewSelectedRowCollection rows = dataGridViewHistory.SelectedRows;
            if (rows.Count == 0)
            {
                MessageBox.Show("Выберите нужные истории запросов из таблицы!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            foreach (DataGridViewRow row in rows)
            {
                List<VideoDataSQLite> videos = await SQLiteManager.GetSearchVideos(new HistoryDataSqlite() { Id = Convert.ToInt32(row.Cells["ColumnNumHistory"].Value) });
                if (videos is null) return;
                foreach (VideoDataSQLite video in videos)
                    links.Add(video.IdVideo);
            }

            using (var saveListWin = new SaveListSearchVideos(links))
            {
                saveListWin.FormBorderStyle = FormBorderStyle.FixedDialog;
                saveListWin.TopMost = true;
                saveListWin.ShowDialog();
                this.Activate();
                if (saveListWin.saved)
                {
                    MessageBox.Show($"Ссылки сохранены в файл: {saveListWin.pathSave}", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private async void btnDeleteHistory_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = dataGridViewHistory.SelectedRows;
            if (rows.Count > 0)
            {
                DialogResult result;
                if (rows.Count == 1)
                    result = MessageBox.Show("Вы уверены, что хотите удалить выбранную историю?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                else
                    result = MessageBox.Show("Вы уверены, что хотите удалить выбранные истории?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in rows)
                    {
                        int history_id_selected = Convert.ToInt32(row.Cells["ColumnNumHistory"].Value);
                        await SQLiteManager.QueryExecuteNonQuery($@"
                            DELETE FROM history_videos WHERE history_id = {history_id_selected};

                            DELETE FROM history_themes WHERE history_id = {history_id_selected};
                            
                            DELETE FROM history WHERE id = {history_id_selected};
                        ");
                        if (history_id_selected == historyId)
                            ReloadSearchVideosInTable();
                    }
                    
                    ReloadHistoryInTable();
                    MessageBox.Show("Выбранные истории запросов удалены из базы!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Выберите нужные истории запросов из таблицы!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnPreviousPageHistory_Click(object sender, EventArgs e)
        {
            numPageHistory--;
            ReloadHistoryInTable();
        }
        private void btnNextPageHistory_Click(object sender, EventArgs e)
        {
            numPageHistory++;
            ReloadHistoryInTable();
        }

        // CHANNELS
        private void btnAddChannel_Click(object sender, EventArgs e)
        {
            using (var channelAdd = new ChannelAdd())
            {
                channelAdd.FormBorderStyle = FormBorderStyle.FixedDialog;
                channelAdd.TopMost = true;
                channelAdd.ShowDialog();
                this.Activate();
                if (channelAdd.numChannelsAdd > 0)
                    ReloadChannelsInTable();
                if (channelAdd.numVideoAdd > 0)
                    ReloadVideosInTable(SortVideosInTable.videoSortParams);
                if (channelAdd.numChannelsSearch > 0)
                    MessageBox.Show($"Добавлено каналов: {channelAdd.numChannelsAdd} из {channelAdd.numChannelsSearch}\n" +
                        $"Добавлено видео: {channelAdd.numVideoAdd} из {channelAdd.numVideoSearch}", 
                        "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private async void btnDeleteChannel_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = dataGridViewChannels.SelectedRows;
            if (rows.Count > 0)
            {
                DialogResult result;
                if (rows.Count == 1)
                    result = MessageBox.Show("Вы уверены, что хотите удалить выбранный канал?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                else
                    result = MessageBox.Show("Вы уверены, что хотите удалить выбранные каналы?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in rows)
                    {
                        int? resultDeleteChannel = await SQLiteManager.DeleteChannel(new ChannelDataSQLite()
                        {
                            Id = Convert.ToInt32(row.Cells["ColumnId"].Value),
                            Title = row.Cells["ColumnTitleChannel"].Value.ToString()
                        });
                        if (resultDeleteChannel == null) return;
                    }
                    ReloadChannelsInTable();
                    ReloadThemesInTable();
                    ReloadSearchVideosInTable(historyId);
                    ReloadHistoryInTable();
                    ReloadVideosInTable(SortVideosInTable.videoSortParams);
                    MessageBox.Show("Выбранные каналы удалены из базы!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Выберите нужные каналы из таблицы!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private async void btnBlockChannel_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = dataGridViewChannels.SelectedRows;
            if (rows.Count > 0)
            {
                DialogResult result;
                if (rows.Count == 1)
                    result = MessageBox.Show("Вы уверены, что хотите добавить выбранный канал в черный список?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                else
                    result = MessageBox.Show("Вы уверены, что хотите добавить выбранные каналы в черный список?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in rows)
                    {
                        int? resultBlockChannel = await SQLiteManager.InsertContentInBlackList(new ContentBlockDataSQLite()
                        {
                            Type = "канал",
                            Title = row.Cells["ColumnTitleChannel"].Value.ToString(),
                            IdContent = row.Cells["ColumnIdChannel"].Value.ToString()
                        });
                        if (resultBlockChannel == null) return;


                        int? resultDeleteChannel = await SQLiteManager.DeleteChannel(new ChannelDataSQLite()
                        {
                            Id = Convert.ToInt32(row.Cells["ColumnId"].Value),
                            Title = row.Cells["ColumnTitleChannel"].Value.ToString()
                        });
                        if (resultDeleteChannel == null) return;
                    }
                    ReloadVideosInTable(SortVideosInTable.videoSortParams);
                    ReloadChannelsInTable();
                    ReloadThemesInTable();
                    ReloadBlackList();
                    ReloadSearchVideosInTable(historyId);
                    ReloadHistoryInTable();
                    MessageBox.Show("Выбранные каналы добавлены в черный список!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Выберите нужные каналы из таблицы!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private async void btnReloadChannels_Click(object sender, EventArgs e)
        {
            List<ChannelDataSQLite> channels = new List<ChannelDataSQLite>();
            DataGridViewSelectedRowCollection rows = dataGridViewChannels.SelectedRows;
            if (rows.Count != 0)
            {
                foreach (DataGridViewRow row in rows)
                {
                    List<ChannelDataSQLite> channelData = await SQLiteManager.GetChannels(id: Convert.ToInt32(row.Cells["ColumnId"].Value));
                    if (channelData is null) return;
                    if (channelData.Count != 0)
                        channels.Add(channelData[0]);
                }
            }
            using (var reloadChannels = new ReloadChannelsData(channels))
            {
                reloadChannels.FormBorderStyle = FormBorderStyle.FixedDialog;
                reloadChannels.TopMost = true;
                reloadChannels.ShowDialog();
                this.Activate();

                ReloadChannelsInTable();
                ReloadVideosInTable(SortVideosInTable.videoSortParams);
                ReloadThemesInTable();
                ReloadHistoryInTable();
                ReloadSearchVideosInTable(historyId);

                string LastReloadDate = await SQLiteManager.GetValueStringParameters("LAST_RELOAD_DATE");
                DateTime LastReloadDateTime = DateTime.Parse(LastReloadDate);
                labelLastReloadedDate.Text = "Дата последнего обновления: " + LastReloadDateTime.ToString("HH:mm dd.MM.yyyy");

                MessageBox.Show($"Обнавлено каналов: {reloadChannels.numChannelsReloaded} из {reloadChannels.numTotalChannels}\n" +
                        $"Удалено каналов: {reloadChannels.numChannelsDeleted}\n" +
                        $"Добавлено видео: { reloadChannels.numAddedVideos}\n",
                        "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnPreviousPageChannels_Click(object sender, EventArgs e)
        {
            numPageChannels--;
            ReloadChannelsInTable();
        }
        private void btnNextPageChannels_Click(object sender, EventArgs e)
        {
            numPageChannels++;
            ReloadChannelsInTable();
        }

        // VIDEOS
        private void btnAddVideo_Click(object sender, EventArgs e)
        {
            using (var videoAdd = new VideoAdd())
            {
                videoAdd.FormBorderStyle = FormBorderStyle.FixedDialog;
                videoAdd.TopMost = true;
                videoAdd.ShowDialog();
                this.Activate();
                if (videoAdd.numChannelsAdd > 0)
                    ReloadChannelsInTable();
                if (videoAdd.numVideoAdd > 0)
                {
                    ReloadVideosInTable(SortVideosInTable.videoSortParams);
                    ReloadThemesInTable();
                }
                if (videoAdd.numVideoSearch > 0)
                    MessageBox.Show($"Добавлено видео: {videoAdd.numVideoAdd} из {videoAdd.numVideoSearch}\n" + 
                            $"Добавлено каналов: {videoAdd.numChannelsAdd}\n",
                            "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private async void btnDeleteVideo_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = dataGridViewVideos.SelectedRows;
            if (rows.Count > 0)
            {
                DialogResult result;
                if (rows.Count == 1)
                    result = MessageBox.Show("Вы уверены, что хотите удалить выбранное видео?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                else
                    result = MessageBox.Show("Вы уверены, что хотите удалить выбранные видео?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in rows)
                    {
                        int? resultDeleteVideo = await SQLiteManager.DeleteVideo(new VideoDataSQLite()
                        {
                            Id = Convert.ToInt32(row.Cells["ColumnNumVideoInTable"].Value),
                            Title = row.Cells["ColumnTitleVideo"].Value.ToString()
                        });
                        if (resultDeleteVideo == null) return;
                    }
                    ReloadChannelsInTable();
                    ReloadThemesInTable();
                    ReloadSearchVideosInTable(historyId);
                    ReloadHistoryInTable();
                    ReloadVideosInTable(SortVideosInTable.videoSortParams);
                    MessageBox.Show("Выбранные видео удалены из базы!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Выберите нужные видео из таблицы!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private async void btnBlockVideo_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = dataGridViewVideos.SelectedRows;
            if (rows.Count > 0)
            {
                DialogResult result;
                if (rows.Count == 1)
                    result = MessageBox.Show("Вы уверены, что хотите добавить выбранное видео в черный список?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                else
                    result = MessageBox.Show("Вы уверены, что хотите добавить выбранные видео в черный список?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in rows)
                    {
                        int? resultBlockVideo = await SQLiteManager.InsertContentInBlackList(new ContentBlockDataSQLite()
                        {
                            Type = "видео",
                            Title = row.Cells["ColumnTitleVideo"].Value.ToString(),
                            IdContent = row.Cells["ColumnIdVideo"].Value.ToString()
                        });
                        if (resultBlockVideo == null) return;

                        int? resultDeleteVideo = await SQLiteManager.DeleteVideo(new VideoDataSQLite()
                        {
                            Id = Convert.ToInt32(row.Cells["ColumnNumVideoInTable"].Value),
                            Title = row.Cells["ColumnTitleVideo"].Value.ToString()
                        });
                        if (resultDeleteVideo == null) return;
                    }
                    ReloadChannelsInTable();
                    ReloadThemesInTable();
                    ReloadSearchVideosInTable(historyId);
                    ReloadHistoryInTable();
                    ReloadVideosInTable(SortVideosInTable.videoSortParams);
                    ReloadBlackList();
                    MessageBox.Show("Выбранные видео добавлены в черный список!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Выберите нужные видео из таблицы!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnPreviousPageVideos_Click(object sender, EventArgs e)
        {
            numPageVideos--;
            ReloadVideosInTable(SortVideosInTable.videoSortParams);
        }
        private void btnNextPageVideos_Click(object sender, EventArgs e)
        {
            numPageVideos++;
            ReloadVideosInTable(SortVideosInTable.videoSortParams);
        }
        private void btnSort_Click(object sender, EventArgs e)
        {
            SortVideosInTable.Instance.ReloadChannels();
            SortVideosInTable.Instance.ReloadStories();
            SortVideosInTable.Instance.ReloadThemes();

            SortVideosInTable.Instance.FormBorderStyle = FormBorderStyle.FixedDialog;
            SortVideosInTable.Instance.TopMost = true;
            SortVideosInTable.Instance.ShowDialog();
            this.Activate();

            numPageVideos = 0;
            ReloadVideosInTable(SortVideosInTable.videoSortParams);
        }

        // THEMES
        private void btnAddTheme_Click(object sender, EventArgs e)
        {
            using (var themeAdd = new ThemeAdd())
            {
                themeAdd.FormBorderStyle = FormBorderStyle.FixedDialog;
                themeAdd.TopMost = true;
                themeAdd.ShowDialog();
                this.Activate();
                if (themeAdd.click_add)
                {
                    ReloadThemesInTable();
                    ReloadVideosInTable(SortVideosInTable.videoSortParams);
                    MessageBox.Show($"Найдено видео с данной темой: {themeAdd.numVideoSearch}",
                                "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void btnEditTheme_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = dataGridViewThemes.SelectedRows;
            if (rows.Count != 1)
            {
                MessageBox.Show("Выберите одну тему из таблицы!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var themeEdit = new ThemeEdit(rows[0].Cells["ColumnNumTheme"].Value))
            {
                themeEdit.FormBorderStyle = FormBorderStyle.FixedDialog;
                themeEdit.TopMost = true;
                themeEdit.ShowDialog();
                this.Activate();
                if (themeEdit.click_save)
                {
                    ReloadThemesInTable();
                    ReloadVideosInTable(SortVideosInTable.videoSortParams);
                    if (themeEdit.editOnlyTitle)
                    {
                        MessageBox.Show("Изменения сохранены!",
                                    "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Найдено видео с данной темой: {themeEdit.numVideoSearch}",
                                    "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        private async void btnDeleteTheme_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = dataGridViewThemes.SelectedRows;
            if (rows.Count > 0)
            {
                DialogResult result;
                if (rows.Count == 1)
                    result = MessageBox.Show("Вы уверены, что хотите удалить выбранную тему?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                else
                    result = MessageBox.Show("Вы уверены, что хотите удалить выбранные темы?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in rows)
                    {
                        await SQLiteManager.QueryExecuteNonQuery($@"
                        DELETE FROM history_themes WHERE theme_id = '{row.Cells["ColumnNumTheme"].Value}';

                        DELETE FROM themes_videos WHERE theme_id = '{row.Cells["ColumnNumTheme"].Value}';

                        DELETE FROM key_words WHERE theme_id = '{row.Cells["ColumnNumTheme"].Value}';

                        DELETE FROM themes WHERE id = '{row.Cells["ColumnNumTheme"].Value}';
                    ");
                    }
                    ReloadThemesInTable();
                    ReloadVideosInTable(SortVideosInTable.videoSortParams);
                    MessageBox.Show("Выбранные темы удалены из базы!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Выберите нужные темы из таблицы!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnPreviousPageThemes_Click(object sender, EventArgs e)
        {
            numPageThemes--;
            ReloadThemesInTable();
        }
        private void btnNextPageThemes_Click(object sender, EventArgs e)
        {
            numPageThemes++;
            ReloadThemesInTable();
        }

        // BLACK LIST
        private void btnAddBlock_Click(object sender, EventArgs e)
        {
            using (var contentAddInBlackList = new СontentAddInBlackList())
            {
                contentAddInBlackList.FormBorderStyle = FormBorderStyle.FixedDialog;
                contentAddInBlackList.TopMost = true;
                contentAddInBlackList.ShowDialog();
                this.Activate();
                if (contentAddInBlackList.numContentAdd > 0)
                {
                    ReloadBlackList();
                    ReloadVideosInTable(SortVideosInTable.videoSortParams);
                    ReloadChannelsInTable();
                    ReloadSearchVideosInTable(historyId);
                }
                if (contentAddInBlackList.numContentSearch > 0)
                    MessageBox.Show($"Добавлено контента: {contentAddInBlackList.numContentAdd} из {contentAddInBlackList.numContentSearch}", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private async void btnDeleteBlock_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = dataGridViewBlackList.SelectedRows;
            if (rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранный контент из черного списка?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in rows)
                    {
                        await SQLiteManager.QueryExecuteNonQuery($@"
                            DELETE FROM black_list WHERE id = '{row.Cells["ColumnIdContentBlackList"].Value}';
                        ");
                    }
                    ReloadBlackList();
                    MessageBox.Show("Выбранные контент удален из черного списка!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Выберите нужный контент из таблицы!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnPreviousPageBlackList_Click(object sender, EventArgs e)
        {
            numPageBlackList--;
            ReloadBlackList();
        }
        private void btnNextPageBlackList_Click(object sender, EventArgs e)
        {
            numPageBlackList++;
            ReloadBlackList();
        }

        // API KEYS
        private async void comboBoxFrequencyReloaded_SelectedIndexChanged(object sender, EventArgs e)
        {
            int value;
            if (comboBoxFrequencyReloaded.SelectedIndex == 3)
                value = 168;
            else
                value = (comboBoxFrequencyReloaded.SelectedIndex + 1) * 24;

            await SQLiteManager.UpdateValueParameter("FREQUENCY_RELOADED_HOURS", value.ToString());
        }
        private void btnAddAPIKey_Click(object sender, EventArgs e)
        {
            using (var apiKeyAdd = new APIKeyAdd())
            {
                apiKeyAdd.FormBorderStyle = FormBorderStyle.FixedDialog;
                apiKeyAdd.TopMost = true;
                apiKeyAdd.ShowDialog();
                this.Activate();
                if (apiKeyAdd.click_add)
                {
                    ReloadAPIKeysInTable();
                    MessageBox.Show($"Ключ добавлен!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private async void btnDeleteAPIKey_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = dataGridViewAPIKeys.SelectedRows;
            if (rows.Count > 0)
            {
                DialogResult result;
                if (rows.Count == 1)
                    result = MessageBox.Show("Вы уверены, что хотите удалить выбранный ключ?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                else
                    result = MessageBox.Show("Вы уверены, что хотите удалить выбранные ключи?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in rows)
                    {
                        await SQLiteManager.QueryExecuteNonQuery($@"
                            DELETE FROM api_keys WHERE value = '{row.Cells["ColumnValueAPIKey"].Value}';
                        ");
                    }
                    ReloadAPIKeysInTable();
                    MessageBox.Show("Выбранные ключи удалены из базы!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Выберите нужные ключи из таблицы!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridViewChannels_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = $"https://www.youtube.com/channel/{dataGridViewChannels.Rows[e.RowIndex].Cells["ColumnIdChannel"].Value}",
                    UseShellExecute = true
                });
            }
        }

        private void dataGridViewVideos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = $"https://youtube.com/watch/{dataGridViewVideos.Rows[e.RowIndex].Cells["ColumnIdVideo"].Value}",
                    UseShellExecute = true
                });
            }
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            skipLoading = true;
            labelStatusLoading.Text = "Отмена обновления каналов...";
        }

        private void dataGridViewSearchVideos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = $"https://youtube.com/watch/{dataGridViewSearchVideos.Rows[e.RowIndex].Cells["ColumnGetIdVideo"].Value}",
                    UseShellExecute = true
                });
            }
        }

        private void dataGridViewBlackList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string url = "";
                if (dataGridViewBlackList.Rows[e.RowIndex].Cells["ColumnType"].Value.ToString() == "канал")
                    url += "https://www.youtube.com/channel/";
                else if (dataGridViewBlackList.Rows[e.RowIndex].Cells["ColumnType"].Value.ToString() == "видео")
                    url += "https://youtube.com/watch/";
                Process.Start(new ProcessStartInfo
                {
                    FileName = url + dataGridViewBlackList.Rows[e.RowIndex].Cells["ColumnContentId"].Value,
                    UseShellExecute = true
                });
            }
        }

        private async void btnStartOther_Click(object sender, EventArgs e)
        {

            if (comboBoxOther.SelectedIndex == 0)
            {

            }
            else if (comboBoxOther.SelectedIndex == 1)
            {
                for (int i = 0; i < richTextBoxLinksOther.Lines.Length; i++)
                {
                    string line = richTextBoxLinksOther.Lines[i].Trim();

                    if (line.Length == 0) continue;

                    
                }
            }
        }
    }
}
