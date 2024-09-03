using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace YouTubeVideoSearch
{
    public partial class SaveListSearchVideos : Form
    {
        public string pathSave = "";
        public bool saved=false;
        private List<string> _links;
        public SaveListSearchVideos(List<string> links)
        {
            InitializeComponent();
            _links = links;
            ReloadList();
            textBoxPathFile.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\list.txt";
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

        private void btnChoisePathFile_Click(object sender, EventArgs e)
        {
            saveFileDialogLinks.Title = "Выберите путь сохранения";
            saveFileDialogLinks.FileName = "list.txt";
            saveFileDialogLinks.InitialDirectory = textBoxPathFile.Text;

            if (saveFileDialogLinks.ShowDialog() == DialogResult.OK)
            {
                textBoxPathFile.Text = saveFileDialogLinks.FileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(textBoxPathFile.Text, richTextBoxLinks.Text);
                saved = true;
                pathSave = textBoxPathFile.Text;
                this.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show("Ошибка при сохранении файла: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            saved = false;
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
