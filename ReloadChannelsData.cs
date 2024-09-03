using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTubeVideoSearch
{ 
    public partial class ReloadChannelsData : Form
    {
        public int numChannelsReloaded = 0;
        public int numChannelsDeleted = 0;
        public int numAddedVideos = 0;
        public bool stateCanceledLoad = false;
        public bool formClosing = false;
        public int numTotalChannels = 0;

        public ReloadChannelsData(List<ChannelDataSQLite> channelsData)
        {
            InitializeComponent();
            if (channelsData.Count == 0)
                StartReload();
            else
                StartReload(channelsData);
        }

        private async void StartReload(List<ChannelDataSQLite> channelsData = null)
        {
            await Reload(channelsData);
            formClosing = true;
            Close();
        }

        private async Task Reload(List<ChannelDataSQLite> channelsData = null)
        {
            List<ChannelDataSQLite> channels = channelsData;
            if (channels is null)
                channels = await SQLiteManager.GetChannels();

            if (channels is null)
                return;
            numTotalChannels = channels.Count;

            int? FrequencyReloadedHours = await SQLiteManager.GetValueIntParameters("FREQUENCY_RELOADED_HOURS");
            if (FrequencyReloadedHours == null) return;

            DateTime nowDate = DateTime.Now;

            progressBarLoading.Maximum = numTotalChannels;

            foreach (ChannelDataSQLite channel in channels)
            {
                if (stateCanceledLoad) return;
                progressBarLoading.Increment(1);
                labelChannelName.Text = "Канал: " + channel.Title;
                DateTime dateLimit = DateTime.Parse(channel.DateLimit);

                if (channel.LoadNewVideos == "False")
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
                    numAddedVideos++;

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
            await SQLiteManager.UpdateValueParameter("LAST_RELOAD_DATE", nowDate.ToString("yyyy-MM-ddTHH:mm:ssZ"));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            stateCanceledLoad = true;
            labelStatus.Text = "Отменяем загрузку...";
        }

        private void ReloadChannelsData_FormClosing(object sender, FormClosingEventArgs e)
        {
            stateCanceledLoad = true;

            if (!formClosing)
            {
                e.Cancel = true;
            }
        }
    }
}
