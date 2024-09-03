using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTubeVideoSearch
{
    public partial class OtherMethods : Form
    {
        public int numChannelsReloaded = 0;
        public int numChannelsDeleted = 0;
        public int numAddedVideos = 0;
        public bool stateCanceledLoad = false;
        public bool formClosing = false;
        public int numTotalChannels = 0;

        public OtherMethods(List<string> links)
        {
            InitializeComponent();
            if (links.Count == 0)
                StartReload();
            else
                StartReload(links);
        }

        private async void StartReload(List<string> links = null)
        {
            await Reload(links);
            formClosing = true;
            Close();
        }

        private async Task Reload(List<string> links = null)
        {
            if (links == null)
                return;
            foreach (string link in links)
            {
                string video_id = YouTubeApi.ExtractVideoId(link);
                if (video_id is null) continue;

                VideoData videoData = await YouTubeApi.GetVideoInfoByIdAsync(video_id);
                if (videoData is null) continue;
            }
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
