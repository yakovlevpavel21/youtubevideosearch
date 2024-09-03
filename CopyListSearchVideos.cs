using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace YouTubeVideoSearch
{
    public partial class CopyListSearchVideos : Form
    {
        public bool copyed = false;
        private List<string> _links = new List<string>();
        public CopyListSearchVideos(List<string> links)
        {
            InitializeComponent();
            _links = links;
            ReloadList();
        }

        private void ReloadList()
        {
            richTextBoxLinks.Clear();
            for (int i = 0; i < _links.Count; i++) 
            {
                string line = "";
                if (checkBoxNumLinks.Checked)
                    line += $"{i + 1}. ";
                if (!checkBoxOnlyId.Checked)
                    line += "https://youtube.com/watch/";
                line += _links[i];
                if (i != _links.Count - 1)
                    line += "\n";
                richTextBoxLinks.AppendText(line);
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            copyed = true;
            Clipboard.SetText(richTextBoxLinks.Text);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            copyed = false;
            this.Close();
        }

        private void checkBoxNumLinks_CheckedChanged(object sender, EventArgs e)
        {
            ReloadList();
        }

        private void checkBoxOnlyId_CheckedChanged(object sender, EventArgs e)
        {
            ReloadList();
        }
    }
}
