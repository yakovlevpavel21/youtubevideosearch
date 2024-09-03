using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace YouTubeVideoSearch
{ 
    public partial class APIKeyAdd : Form
    {
        public bool click_add = false;
        public bool stateCanceledLoad = false;
        public APIKeyAdd()
        {
            InitializeComponent();

            toolTipInfo.SetToolTip(pictureBoxInfoGoogleConsole, "Нажмите чтобы узнать подробнее про API ключи.");
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            if (await YouTubeApi.CheckYouTubeApiKey(textBoxAPIKey.Text))
            {
                click_add = true;
                await SQLiteManager.QueryExecuteNonQuery($@"
                    INSERT INTO api_keys (value)
                    VALUES ('{textBoxAPIKey.Text}')
                ");
                this.Close();
            }
            else
            {
                MessageBox.Show("API ключ недействителен! Проверьте, что ваш API ключ включен!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxAPIKey.Focus();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            stateCanceledLoad = true;
            this.Close();
        }

        private void pictureBoxInfoGoogleConsole_Click(object sender, EventArgs e)
        {
            try
            {
                // Используем Process.Start для открытия ссылки в браузере
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://console.developers.google.com/",
                    UseShellExecute = true
                });
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Не удалось открыть ссылку: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
