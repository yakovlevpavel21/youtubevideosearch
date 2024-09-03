using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTubeVideoSearch
{ 
    public partial class ThemeEdit : Form
    {
        public int numVideoSearch = 0;
        public bool click_save = false;
        public bool editOnlyTitle = false;
        public bool stateCanceledLoad = false;
        private object theme_id;
        private string richText;
        public ThemeEdit(object theme_id)
        {
            InitializeComponent();

            toolTipInfo.SetToolTip(pictureBoxInfoAddTheme, "Все ключевые слова должны\n" +
                "быть в нижнем регистре, кроме\n" +
                "слов из кириллицы.");
            this.theme_id = theme_id;
            Init();
        }

        private async void Init()
        {
            object theme_title = await SQLiteManager.QueryScalar($@"
                SELECT title FROM themes WHERE id = '{theme_id}'
            ");
            textBoxThemeName.Text = theme_title.ToString();
            object[][] key_words = await SQLiteManager.QueryReader($@"
                SELECT word FROM key_words WHERE theme_id = '{theme_id}'
            ");
            for (int i = 0; i < key_words.Length; i++)
            {
                richTextBoxData.Text += key_words[i][0];
                if (i != key_words.Length - 1)
                    richTextBoxData.Text += "\n";
            }
            richText = richTextBoxData.Text;
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
            foreach(string line in richTextBoxData.Lines)
            {
                if(line.Trim().Count() != 0)
                {
                    check_error = false;
                    object countWord = await SQLiteManager.QueryScalar($@"
                        SELECT COUNT(*) FROM key_words WHERE word = '{line}' AND theme_id != '{theme_id}'
                    ");
                    if (Convert.ToInt32(countWord) != 0)
                    {
                        MessageBox.Show($"Ключевое слово '{line}' уже используется другой темой!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        richTextBoxData.Focus();
                        return;
                    }
                }
            }
            if (check_error)
            {
                MessageBox.Show("Введите ключевые слова!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                richTextBoxData.Focus();
                return;
            }
            object count = await SQLiteManager.QueryScalar($@"
                SELECT COUNT(*) FROM themes WHERE title = '{textBoxThemeName.Text}' AND id != '{theme_id}'
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
            labelStatus.Text = "Обновление темы...";
            await Task.Delay(1);
            if (stateCanceledLoad)
                return;
            click_save = true;
            await SQLiteManager.QueryExecuteNonQuery($@"
                UPDATE themes SET title = '{textBoxThemeName.Text}' WHERE id = '{theme_id}'
            ");
            await Task.Delay(1);

            if (richText == richTextBoxData.Text) 
            {
                editOnlyTitle = true;
                await Task.Delay(1000);
                this.Close();
                return;
            }

            await SQLiteManager.QueryExecuteNonQuery($@"
                DELETE FROM key_words WHERE theme_id = '{theme_id}'
            ");
            await Task.Delay(1);
            string themes_for_query = "";
            for (int i = 0; i < richTextBoxData.Lines.Length; i++)
            {
                string word = richTextBoxData.Lines[i].Trim();

                if (word.Length == 0)
                    continue;

                object countWord = await SQLiteManager.QueryScalar($@"
                    SELECT COUNT(*) FROM key_words WHERE word = '{word}' AND theme_id = '{theme_id}'
                ");
                await Task.Delay(1);
                if (Convert.ToInt32(countWord) != 0)
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
            labelStatus.Text = "Поиск видео с измененной темой...";
            await Task.Delay(1);

            object[][] videos = await SQLiteManager.QueryReader($@"
                SELECT id FROM videos
                WHERE {themes_for_query};
            ");
            await Task.Delay(1);

            if (stateCanceledLoad)
                return;
            await SQLiteManager.QueryExecuteNonQuery($@"
                DELETE FROM themes_videos WHERE theme_id = '{theme_id}'
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
