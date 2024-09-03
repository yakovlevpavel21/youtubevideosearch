using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace YouTubeVideoSearch
{
    public partial class SortVideosInTable : Form
    {
        public static VideoSortDataSQLite videoSortParams = new VideoSortDataSQLite()
        {
            Title = "",
            Id = "",
            ThemesId = null,
            StoriesId = null,
            StartDate = "",
            EndDate = "",
            MinDuration = -1,
            MaxDuration = -1,
            ChannelsId = null,
            Method = -1
        };

        private static SortVideosInTable instance;

        private SortVideosInTable()
        {
            InitializeComponent();
            comboBoxMinDurVideoSort.SelectedIndex = 0;
            comboBoxMaxDurVideoSort.SelectedIndex = 0;
            comboBoxUsed.SelectedIndex = 9;
            dateTimePickerEndSort.Value = DateTime.Now;
            ReloadChannels();
            ReloadThemes();
            ReloadStories();
        }

        public static SortVideosInTable Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SortVideosInTable();
                }
                return instance;
            }
        }
        public async void ReloadChannels()
        {
            dataGridViewChannelsSort.Rows.Clear();
            List<ChannelDataSQLite> channels = await SQLiteManager.GetChannels();
            if (channels is null) return;
            foreach (ChannelDataSQLite channel in channels)
            {
                bool isCheckedChannel = false;
                if (videoSortParams.ChannelsId != null)
                    foreach (int channel_id in videoSortParams.ChannelsId)
                    {
                        if (channel.Id == channel_id)
                        {
                            isCheckedChannel = true;
                            break;
                        }
                    }
                dataGridViewChannelsSort.Rows.Add(new object[]
                {
                    channel.Id,
                    channel.Title,
                    isCheckedChannel
                });
            }
        }
        public async void ReloadStories()
        {
            dataGridViewHistorySort.Rows.Clear();
            List<HistoryDataSqlite> stories = await SQLiteManager.GetHistory();
            if (stories is null) return;
            foreach (HistoryDataSqlite history in stories)
            {
                bool isCheckedHistory = false;
                if (videoSortParams.StoriesId != null)
                    foreach (int history_id in videoSortParams.StoriesId)
                    {
                        if (history.Id == history_id)
                        {
                            isCheckedHistory = true;
                            break;
                        }
                    }
                string date = "";
                DateTime datetime;
                if (DateTime.TryParseExact(history.Date, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.None, out datetime))
                {
                    date = datetime.ToString("dd.MM.yyyy");
                }
                dataGridViewHistorySort.Rows.Add(new object[]
                {
                    history.Id,
                    date,
                    isCheckedHistory
                });
            }
        }
        public async void ReloadThemes()
        {
            dataGridViewThemesSort.Rows.Clear();
            List<ThemeDataSQLite> themes = await SQLiteManager.GetThemes();
            if (themes is null) return;
            foreach (ThemeDataSQLite theme in themes)
            {
                bool isCheckedTheme = false;
                if (videoSortParams.ThemesId != null)
                    foreach (int theme_id in videoSortParams.ThemesId)
                    {
                        if (theme.Id == theme_id)
                        {
                            isCheckedTheme = true;
                            break;
                        }
                    }
                dataGridViewThemesSort.Rows.Add(new object[]
                {
                    theme.Id,
                    theme.Title,
                    isCheckedTheme
                });
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            textBoxTitleVideoSort.Text = "";
            textBoxIdVideoSort.Text = "";

            if (videoSortParams.ThemesId != null) videoSortParams.ThemesId.Clear();
            ReloadThemes();

            if (videoSortParams.StoriesId != null) videoSortParams.StoriesId.Clear();
            ReloadStories();

            dateTimePickerStartSort.Value = dateTimePickerStartSort.MinDate;
            dateTimePickerEndSort.Value = DateTime.Now;

            textBoxMinDurVideoSort.Text = "";
            comboBoxMinDurVideoSort.SelectedIndex = 0;
            textBoxMaxDurVideoSort.Text = "";
            comboBoxMaxDurVideoSort.SelectedIndex = 0;

            if (videoSortParams.ChannelsId != null) videoSortParams.ChannelsId.Clear();
            ReloadChannels();

            comboBoxUsed.SelectedIndex = 9;
        }

        private void Save()
        {
            //MessageBox.Show("fdv");
            videoSortParams.Title = textBoxTitleVideoSort.Text;
            videoSortParams.Id = textBoxIdVideoSort.Text;

            if (videoSortParams.ThemesId != null) videoSortParams.ThemesId.Clear();
            else videoSortParams.ThemesId = new List<int>();

            for (int i = 0; i < dataGridViewThemesSort.RowCount; i++)
                if (Convert.ToBoolean(dataGridViewThemesSort.Rows[i].Cells["ColumnChoiseThemeSort"].Value))
                    videoSortParams.ThemesId.Add(Convert.ToInt32(dataGridViewThemesSort.Rows[i].Cells["ColumnNumThemeSort"].Value));

            if (videoSortParams.StoriesId != null) videoSortParams.StoriesId.Clear();
            else videoSortParams.StoriesId = new List<int>();

            for (int i = 0; i < dataGridViewHistorySort.RowCount; i++)
                if (Convert.ToBoolean(dataGridViewHistorySort.Rows[i].Cells["ColumnChoiseHistorySort"].Value))
                    videoSortParams.StoriesId.Add(Convert.ToInt32(dataGridViewHistorySort.Rows[i].Cells["ColumnNumHistory"].Value));

            videoSortParams.StartDate = dateTimePickerStartSort.Value.ToString("yyyy-MM-ddTHH:mm:ssZ");
            videoSortParams.EndDate = dateTimePickerEndSort.Value.ToString("yyyy-MM-ddTHH:mm:ssZ");

            if (textBoxMinDurVideoSort.Text != "" && int.TryParse(textBoxMinDurVideoSort.Text, out int minDurInt))
                videoSortParams.MinDuration = minDurInt * (int)Math.Pow(60, comboBoxMinDurVideoSort.SelectedIndex);
            else
                videoSortParams.MinDuration = -1;

            if (textBoxMaxDurVideoSort.Text != "" && int.TryParse(textBoxMaxDurVideoSort.Text, out int maxDurInt))
                videoSortParams.MaxDuration = maxDurInt * (int)Math.Pow(60, comboBoxMaxDurVideoSort.SelectedIndex);
            else
                videoSortParams.MaxDuration = -1;

            if (videoSortParams.ChannelsId != null) videoSortParams.ChannelsId.Clear();
            else videoSortParams.ChannelsId = new List<int>();

            for (int i = 0; i < dataGridViewChannelsSort.RowCount; i++)
                if (Convert.ToBoolean(dataGridViewChannelsSort.Rows[i].Cells["ColumnChoiseChannel"].Value))
                    videoSortParams.ChannelsId.Add(Convert.ToInt32(dataGridViewChannelsSort.Rows[i].Cells["ColumnNumChoiseChannels"].Value));

            if (comboBoxUsed.SelectedIndex != -1)
                videoSortParams.Method = comboBoxUsed.SelectedIndex;
        }

        private void btnDateCreateChannel_Click(object sender, EventArgs e)
        {
            dateTimePickerStartSort.Value = dateTimePickerStartSort.MinDate;
        }

        private void btnDateToday_Click(object sender, EventArgs e)
        {
            dateTimePickerEndSort.Value = DateTime.Now;
        }
    }
}
