using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTubeVideoSearch
{ 
    public partial class ThemeAdd : Form
    {
        public int numVideoSearch = 0;
        public bool click_add = false;
        public bool stateCanceledLoad = false;
        public ThemeAdd()
        {
            InitializeComponent();

            toolTipInfo.SetToolTip(pictureBoxInfoAddTheme, "Все ключевые слова должны\n" +
                "быть в нижнем регистре, кроме\n" +
                "слов из кириллицы.");
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            if (textBoxThemeName.Text.Trim() == "")
            {
                MessageBox.Show("Введите название темы!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxThemeName.Focus();
                return;
            }
            bool check_error = true;
            foreach (string line in richTextBoxData.Lines)
            {
                if (line.Trim().Count() != 0)
                {
                    object countWord = await SQLiteManager.QueryScalar($@"
                        SELECT COUNT(*) FROM key_words WHERE word = '{line}'
                    ");
                    if(Convert.ToInt32(countWord) != 0)
                    {
                        MessageBox.Show($"Ключевое слово '{line}' уже используется другой темой!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        richTextBoxData.Focus();
                        return;
                    }
                    check_error = false;
                }
            }
            if (check_error)
            {
                MessageBox.Show("Введите ключевые слова!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                richTextBoxData.Focus();
                return;
            }
            object count = await SQLiteManager.QueryScalar($@"
                SELECT COUNT(*) FROM themes WHERE title = '{textBoxThemeName.Text}'
            ");
            if (Convert.ToInt32(count) != 0)
            {
                MessageBox.Show("Тема с таким названием уже существует!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxThemeName.Focus();
                return;
            }
            panelAdd.Visible = false;
            panelLoading.Visible = true;
            Height = 250;

            LoadingTheme();
        }

        private async void LoadingTheme()
        {
            labelThemeName.Text += $" {textBoxThemeName.Text}";
            labelStatus.Text = "Добавление темы...";
            await Task.Delay(1);

            if (stateCanceledLoad)
                return;
            click_add = true;
            await SQLiteManager.QueryExecuteNonQuery($@"
                INSERT INTO themes (title)
                VALUES ('{textBoxThemeName.Text}')
            ");
            await Task.Delay(1);
            object theme_id = await SQLiteManager.QueryScalar($@"
                SELECT id FROM themes WHERE title = '{textBoxThemeName.Text}'
            ");
            await Task.Delay(1);

            string themes_for_query = "";
            for (int i = 0; i < richTextBoxData.Lines.Length; i++)
            {
                string word = richTextBoxData.Lines[i].Trim();
                if (word.Length == 0)
                    continue;

                if (stateCanceledLoad)
                    return;
                await SQLiteManager.QueryExecuteNonQuery($@"
                    INSERT INTO key_words (word, theme_id)
                    VALUES ('{word}', '{theme_id}')
                ");
                await Task.Delay(1);

                if (themes_for_query != "")
                    themes_for_query += "OR";
                themes_for_query += $" lower(title) LIKE '%{word}%' ";
            }
            labelStatus.Text = "Поиск видео с данной темой...";

            object[][] videos = await SQLiteManager.QueryReader($@"
                SELECT id FROM videos
                WHERE {themes_for_query};
            ");
            await Task.Delay(1);

            for (int v = 0; v < videos.Length; v++)
            {
                if (stateCanceledLoad)
                    return;
                numVideoSearch++;
                await SQLiteManager.QueryExecuteNonQuery($@"
                    INSERT INTO themes_videos (theme_id, video_id)
                    VALUES ('{theme_id}', '{videos[v][0]}')
                ");
                await Task.Delay(1);
            }
            await Task.Delay(1000);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            stateCanceledLoad = true;
            this.Close();
        }
    }
}
