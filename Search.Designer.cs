namespace YouTubeVideoSearch
{
    partial class Search
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Search));
            this.textBoxMaxDurVideo = new System.Windows.Forms.TextBox();
            this.comboBoxMaxDurVideo = new System.Windows.Forms.ComboBox();
            this.btnStartSearch = new System.Windows.Forms.Button();
            this.textBoxMinDurVideo = new System.Windows.Forms.TextBox();
            this.comboBoxMinDurVideo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxNumVideo = new System.Windows.Forms.TextBox();
            this.comboBoxNumVideo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewChoiseThemes = new System.Windows.Forms.DataGridView();
            this.ColumnNumThemeSearch = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNameTheme = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnChoise = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDateToday = new System.Windows.Forms.Button();
            this.btnDateCreateChannel = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dateTimePickerEndSort = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerStartSort = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxUsed = new System.Windows.Forms.ComboBox();
            this.dataGridViewChannelsSort = new System.Windows.Forms.DataGridView();
            this.ColumnNumChoiseChannels = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNameChannels = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnChoiseChannel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.panelLoading = new System.Windows.Forms.Panel();
            this.labelStatus = new System.Windows.Forms.Label();
            this.progressBarLoading = new System.Windows.Forms.ProgressBar();
            this.btnCancelLoading = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewChoiseThemes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewChannelsSort)).BeginInit();
            this.panelLoading.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxMaxDurVideo
            // 
            this.textBoxMaxDurVideo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxMaxDurVideo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxMaxDurVideo.Location = new System.Drawing.Point(24, 339);
            this.textBoxMaxDurVideo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxMaxDurVideo.Name = "textBoxMaxDurVideo";
            this.textBoxMaxDurVideo.Size = new System.Drawing.Size(92, 27);
            this.textBoxMaxDurVideo.TabIndex = 13;
            // 
            // comboBoxMaxDurVideo
            // 
            this.comboBoxMaxDurVideo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMaxDurVideo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.comboBoxMaxDurVideo.FormattingEnabled = true;
            this.comboBoxMaxDurVideo.Items.AddRange(new object[] {
            "Секунд",
            "Минут",
            "Часов"});
            this.comboBoxMaxDurVideo.Location = new System.Drawing.Point(129, 339);
            this.comboBoxMaxDurVideo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxMaxDurVideo.Name = "comboBoxMaxDurVideo";
            this.comboBoxMaxDurVideo.Size = new System.Drawing.Size(148, 28);
            this.comboBoxMaxDurVideo.TabIndex = 12;
            // 
            // btnStartSearch
            // 
            this.btnStartSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnStartSearch.Location = new System.Drawing.Point(544, 396);
            this.btnStartSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStartSearch.Name = "btnStartSearch";
            this.btnStartSearch.Size = new System.Drawing.Size(133, 40);
            this.btnStartSearch.TabIndex = 7;
            this.btnStartSearch.Text = "Найти";
            this.btnStartSearch.UseVisualStyleBackColor = true;
            this.btnStartSearch.Click += new System.EventHandler(this.btnStartSearch_Click);
            // 
            // textBoxMinDurVideo
            // 
            this.textBoxMinDurVideo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxMinDurVideo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxMinDurVideo.Location = new System.Drawing.Point(23, 271);
            this.textBoxMinDurVideo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxMinDurVideo.Name = "textBoxMinDurVideo";
            this.textBoxMinDurVideo.Size = new System.Drawing.Size(92, 27);
            this.textBoxMinDurVideo.TabIndex = 11;
            // 
            // comboBoxMinDurVideo
            // 
            this.comboBoxMinDurVideo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMinDurVideo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.comboBoxMinDurVideo.FormattingEnabled = true;
            this.comboBoxMinDurVideo.Items.AddRange(new object[] {
            "Секунд",
            "Минут",
            "Часов"});
            this.comboBoxMinDurVideo.Location = new System.Drawing.Point(128, 271);
            this.comboBoxMinDurVideo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxMinDurVideo.Name = "comboBoxMinDurVideo";
            this.comboBoxMinDurVideo.Size = new System.Drawing.Size(148, 28);
            this.comboBoxMinDurVideo.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label4.Location = new System.Drawing.Point(19, 309);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(359, 23);
            this.label4.TabIndex = 9;
            this.label4.Text = "Введите максимальную длительность видео";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label3.Location = new System.Drawing.Point(19, 240);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(354, 23);
            this.label3.TabIndex = 8;
            this.label3.Text = "Введите минимальную длительность видео";
            // 
            // textBoxNumVideo
            // 
            this.textBoxNumVideo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxNumVideo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxNumVideo.Location = new System.Drawing.Point(23, 200);
            this.textBoxNumVideo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxNumVideo.Name = "textBoxNumVideo";
            this.textBoxNumVideo.Size = new System.Drawing.Size(92, 27);
            this.textBoxNumVideo.TabIndex = 6;
            // 
            // comboBoxNumVideo
            // 
            this.comboBoxNumVideo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNumVideo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.comboBoxNumVideo.FormattingEnabled = true;
            this.comboBoxNumVideo.Items.AddRange(new object[] {
            "Секунд",
            "Минут",
            "Часов",
            "Штук"});
            this.comboBoxNumVideo.Location = new System.Drawing.Point(128, 200);
            this.comboBoxNumVideo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxNumVideo.Name = "comboBoxNumVideo";
            this.comboBoxNumVideo.Size = new System.Drawing.Size(148, 28);
            this.comboBoxNumVideo.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.Location = new System.Drawing.Point(19, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(220, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Введите количество видео";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.Location = new System.Drawing.Point(19, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Выберите темы";
            // 
            // dataGridViewChoiseThemes
            // 
            this.dataGridViewChoiseThemes.AllowUserToAddRows = false;
            this.dataGridViewChoiseThemes.AllowUserToDeleteRows = false;
            this.dataGridViewChoiseThemes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewChoiseThemes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnNumThemeSearch,
            this.ColumnNameTheme,
            this.ColumnChoise});
            this.dataGridViewChoiseThemes.Location = new System.Drawing.Point(23, 50);
            this.dataGridViewChoiseThemes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewChoiseThemes.Name = "dataGridViewChoiseThemes";
            this.dataGridViewChoiseThemes.RowHeadersWidth = 51;
            this.dataGridViewChoiseThemes.RowTemplate.Height = 24;
            this.dataGridViewChoiseThemes.Size = new System.Drawing.Size(403, 104);
            this.dataGridViewChoiseThemes.TabIndex = 0;
            // 
            // ColumnNumThemeSearch
            // 
            this.ColumnNumThemeSearch.HeaderText = "№";
            this.ColumnNumThemeSearch.MinimumWidth = 6;
            this.ColumnNumThemeSearch.Name = "ColumnNumThemeSearch";
            this.ColumnNumThemeSearch.Width = 50;
            // 
            // ColumnNameTheme
            // 
            this.ColumnNameTheme.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnNameTheme.HeaderText = "Название";
            this.ColumnNameTheme.MinimumWidth = 6;
            this.ColumnNameTheme.Name = "ColumnNameTheme";
            // 
            // ColumnChoise
            // 
            this.ColumnChoise.HeaderText = "Выбор";
            this.ColumnChoise.MinimumWidth = 6;
            this.ColumnChoise.Name = "ColumnChoise";
            this.ColumnChoise.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnChoise.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColumnChoise.Width = 60;
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.Location = new System.Drawing.Point(690, 396);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(134, 40);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDateToday
            // 
            this.btnDateToday.BackColor = System.Drawing.Color.White;
            this.btnDateToday.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDateToday.BackgroundImage")));
            this.btnDateToday.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDateToday.Location = new System.Drawing.Point(783, 92);
            this.btnDateToday.Name = "btnDateToday";
            this.btnDateToday.Size = new System.Drawing.Size(40, 40);
            this.btnDateToday.TabIndex = 55;
            this.toolTipInfo.SetToolTip(this.btnDateToday, "Сегодня");
            this.btnDateToday.UseVisualStyleBackColor = false;
            this.btnDateToday.Click += new System.EventHandler(this.btnDateToday_Click);
            // 
            // btnDateCreateChannel
            // 
            this.btnDateCreateChannel.BackColor = System.Drawing.Color.White;
            this.btnDateCreateChannel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDateCreateChannel.BackgroundImage")));
            this.btnDateCreateChannel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDateCreateChannel.Location = new System.Drawing.Point(783, 46);
            this.btnDateCreateChannel.Name = "btnDateCreateChannel";
            this.btnDateCreateChannel.Size = new System.Drawing.Size(40, 40);
            this.btnDateCreateChannel.TabIndex = 54;
            this.toolTipInfo.SetToolTip(this.btnDateCreateChannel, "Дата создания");
            this.btnDateCreateChannel.UseVisualStyleBackColor = false;
            this.btnDateCreateChannel.Click += new System.EventHandler(this.btnDateCreateChannel_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label8.Location = new System.Drawing.Point(448, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(301, 23);
            this.label8.TabIndex = 53;
            this.label8.Text = "Выберите период публикации видео";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label6.Location = new System.Drawing.Point(448, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 20);
            this.label6.TabIndex = 52;
            this.label6.Text = "До";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label7.Location = new System.Drawing.Point(448, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 20);
            this.label7.TabIndex = 51;
            this.label7.Text = "После";
            // 
            // dateTimePickerEndSort
            // 
            this.dateTimePickerEndSort.CalendarFont = new System.Drawing.Font("Segoe UI", 9F);
            this.dateTimePickerEndSort.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dateTimePickerEndSort.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEndSort.Location = new System.Drawing.Point(511, 98);
            this.dateTimePickerEndSort.MinDate = new System.DateTime(2005, 2, 14, 0, 0, 0, 0);
            this.dateTimePickerEndSort.Name = "dateTimePickerEndSort";
            this.dateTimePickerEndSort.Size = new System.Drawing.Size(266, 27);
            this.dateTimePickerEndSort.TabIndex = 49;
            this.toolTipInfo.SetToolTip(this.dateTimePickerEndSort, "Максимальная дата");
            this.dateTimePickerEndSort.Value = new System.DateTime(2024, 8, 3, 0, 0, 0, 0);
            // 
            // dateTimePickerStartSort
            // 
            this.dateTimePickerStartSort.CalendarFont = new System.Drawing.Font("Segoe UI", 9F);
            this.dateTimePickerStartSort.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dateTimePickerStartSort.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStartSort.Location = new System.Drawing.Point(511, 53);
            this.dateTimePickerStartSort.MinDate = new System.DateTime(2005, 2, 14, 0, 0, 0, 0);
            this.dateTimePickerStartSort.Name = "dateTimePickerStartSort";
            this.dateTimePickerStartSort.Size = new System.Drawing.Size(266, 27);
            this.dateTimePickerStartSort.TabIndex = 50;
            this.toolTipInfo.SetToolTip(this.dateTimePickerStartSort, "Минимальная дата");
            this.dateTimePickerStartSort.Value = new System.DateTime(2005, 2, 14, 0, 0, 0, 0);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label10.Location = new System.Drawing.Point(20, 404);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 23);
            this.label10.TabIndex = 59;
            this.label10.Text = "Получить";
            // 
            // comboBoxUsed
            // 
            this.comboBoxUsed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxUsed.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.comboBoxUsed.FormattingEnabled = true;
            this.comboBoxUsed.Items.AddRange(new object[] {
            "только использованные новые",
            "только использованные старые",
            "только использованные случайные",
            "ни разу не использованные новые",
            "ни разу не использованные старые",
            "ни разу не использованные случайные",
            "все новые",
            "все старые",
            "все случайные",
            "по умолчанию"});
            this.comboBoxUsed.Location = new System.Drawing.Point(111, 402);
            this.comboBoxUsed.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxUsed.Name = "comboBoxUsed";
            this.comboBoxUsed.Size = new System.Drawing.Size(414, 28);
            this.comboBoxUsed.TabIndex = 58;
            // 
            // dataGridViewChannelsSort
            // 
            this.dataGridViewChannelsSort.AllowUserToAddRows = false;
            this.dataGridViewChannelsSort.AllowUserToDeleteRows = false;
            this.dataGridViewChannelsSort.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewChannelsSort.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnNumChoiseChannels,
            this.ColumnNameChannels,
            this.ColumnChoiseChannel});
            this.dataGridViewChannelsSort.Location = new System.Drawing.Point(453, 180);
            this.dataGridViewChannelsSort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewChannelsSort.Name = "dataGridViewChannelsSort";
            this.dataGridViewChannelsSort.RowHeadersWidth = 51;
            this.dataGridViewChannelsSort.RowTemplate.Height = 24;
            this.dataGridViewChannelsSort.Size = new System.Drawing.Size(371, 188);
            this.dataGridViewChannelsSort.TabIndex = 57;
            // 
            // ColumnNumChoiseChannels
            // 
            this.ColumnNumChoiseChannels.HeaderText = "№";
            this.ColumnNumChoiseChannels.MinimumWidth = 6;
            this.ColumnNumChoiseChannels.Name = "ColumnNumChoiseChannels";
            this.ColumnNumChoiseChannels.ReadOnly = true;
            this.ColumnNumChoiseChannels.Width = 60;
            // 
            // ColumnNameChannels
            // 
            this.ColumnNameChannels.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnNameChannels.HeaderText = "Название";
            this.ColumnNameChannels.MinimumWidth = 6;
            this.ColumnNameChannels.Name = "ColumnNameChannels";
            this.ColumnNameChannels.ReadOnly = true;
            // 
            // ColumnChoiseChannel
            // 
            this.ColumnChoiseChannel.HeaderText = "Выбор";
            this.ColumnChoiseChannel.MinimumWidth = 6;
            this.ColumnChoiseChannel.Name = "ColumnChoiseChannel";
            this.ColumnChoiseChannel.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnChoiseChannel.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColumnChoiseChannel.Width = 60;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label5.Location = new System.Drawing.Point(449, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(149, 23);
            this.label5.TabIndex = 56;
            this.label5.Text = "Выберите каналы";
            // 
            // panelLoading
            // 
            this.panelLoading.Controls.Add(this.labelStatus);
            this.panelLoading.Controls.Add(this.progressBarLoading);
            this.panelLoading.Controls.Add(this.btnCancelLoading);
            this.panelLoading.Location = new System.Drawing.Point(12, 8);
            this.panelLoading.Name = "panelLoading";
            this.panelLoading.Size = new System.Drawing.Size(820, 428);
            this.panelLoading.TabIndex = 60;
            this.panelLoading.Visible = false;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelStatus.Location = new System.Drawing.Point(8, 294);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(90, 23);
            this.labelStatus.TabIndex = 27;
            this.labelStatus.Text = "Загрузка...";
            // 
            // progressBarLoading
            // 
            this.progressBarLoading.Location = new System.Drawing.Point(12, 328);
            this.progressBarLoading.MarqueeAnimationSpeed = 50;
            this.progressBarLoading.Maximum = 1000;
            this.progressBarLoading.Name = "progressBarLoading";
            this.progressBarLoading.Size = new System.Drawing.Size(797, 31);
            this.progressBarLoading.Step = 1;
            this.progressBarLoading.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBarLoading.TabIndex = 25;
            // 
            // btnCancelLoading
            // 
            this.btnCancelLoading.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancelLoading.Location = new System.Drawing.Point(678, 377);
            this.btnCancelLoading.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancelLoading.Name = "btnCancelLoading";
            this.btnCancelLoading.Size = new System.Drawing.Size(131, 42);
            this.btnCancelLoading.TabIndex = 8;
            this.btnCancelLoading.Text = "Отменить";
            this.btnCancelLoading.UseVisualStyleBackColor = true;
            this.btnCancelLoading.Click += new System.EventHandler(this.btnCancelLoading_Click);
            // 
            // Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(844, 447);
            this.Controls.Add(this.panelLoading);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.comboBoxUsed);
            this.Controls.Add(this.dataGridViewChannelsSort);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnDateToday);
            this.Controls.Add(this.btnDateCreateChannel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dateTimePickerEndSort);
            this.Controls.Add(this.dateTimePickerStartSort);
            this.Controls.Add(this.textBoxMaxDurVideo);
            this.Controls.Add(this.comboBoxMaxDurVideo);
            this.Controls.Add(this.dataGridViewChoiseThemes);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnStartSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxMinDurVideo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxMinDurVideo);
            this.Controls.Add(this.comboBoxNumVideo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxNumVideo);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Search";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Поиск";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Search_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewChoiseThemes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewChannelsSort)).EndInit();
            this.panelLoading.ResumeLayout(false);
            this.panelLoading.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxMaxDurVideo;
        private System.Windows.Forms.ComboBox comboBoxMaxDurVideo;
        private System.Windows.Forms.Button btnStartSearch;
        private System.Windows.Forms.TextBox textBoxMinDurVideo;
        private System.Windows.Forms.ComboBox comboBoxMinDurVideo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxNumVideo;
        private System.Windows.Forms.ComboBox comboBoxNumVideo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewChoiseThemes;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNumThemeSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNameTheme;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnChoise;
        private System.Windows.Forms.Button btnDateToday;
        private System.Windows.Forms.Button btnDateCreateChannel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndSort;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartSort;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBoxUsed;
        private System.Windows.Forms.DataGridView dataGridViewChannelsSort;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNumChoiseChannels;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNameChannels;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnChoiseChannel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.Panel panelLoading;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.ProgressBar progressBarLoading;
        private System.Windows.Forms.Button btnCancelLoading;
    }
}