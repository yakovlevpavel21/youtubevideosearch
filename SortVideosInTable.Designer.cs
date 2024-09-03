namespace YouTubeVideoSearch
{
    partial class SortVideosInTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SortVideosInTable));
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewChannelsSort = new System.Windows.Forms.DataGridView();
            this.ColumnNumChoiseChannels = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnNameChannels = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnChoiseChannel = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTitleVideoSort = new System.Windows.Forms.TextBox();
            this.textBoxIdVideoSort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridViewThemesSort = new System.Windows.Forms.DataGridView();
            this.ColumnNumThemeSort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTitleThemeSort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnChoiseThemeSort = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dateTimePickerEndSort = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerStartSort = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxMaxDurVideoSort = new System.Windows.Forms.TextBox();
            this.comboBoxMaxDurVideoSort = new System.Windows.Forms.ComboBox();
            this.textBoxMinDurVideoSort = new System.Windows.Forms.TextBox();
            this.comboBoxMinDurVideoSort = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnDateToday = new System.Windows.Forms.Button();
            this.btnDateCreateChannel = new System.Windows.Forms.Button();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.comboBoxUsed = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.dataGridViewHistorySort = new System.Windows.Forms.DataGridView();
            this.label11 = new System.Windows.Forms.Label();
            this.ColumnNumHistory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDateHistory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnChoiseHistorySort = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewChannelsSort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewThemesSort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistorySort)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSave.Location = new System.Drawing.Point(549, 570);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(126, 42);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Подтвердить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.Location = new System.Drawing.Point(431, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 23);
            this.label2.TabIndex = 21;
            this.label2.Text = "Канал";
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
            this.dataGridViewChannelsSort.Location = new System.Drawing.Point(435, 53);
            this.dataGridViewChannelsSort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewChannelsSort.Name = "dataGridViewChannelsSort";
            this.dataGridViewChannelsSort.RowHeadersWidth = 51;
            this.dataGridViewChannelsSort.RowTemplate.Height = 24;
            this.dataGridViewChannelsSort.Size = new System.Drawing.Size(371, 209);
            this.dataGridViewChannelsSort.TabIndex = 22;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.Location = new System.Drawing.Point(18, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 23);
            this.label1.TabIndex = 23;
            this.label1.Text = "Название видео";
            // 
            // textBoxTitleVideoSort
            // 
            this.textBoxTitleVideoSort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxTitleVideoSort.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textBoxTitleVideoSort.Location = new System.Drawing.Point(162, 23);
            this.textBoxTitleVideoSort.Name = "textBoxTitleVideoSort";
            this.textBoxTitleVideoSort.Size = new System.Drawing.Size(231, 30);
            this.textBoxTitleVideoSort.TabIndex = 24;
            // 
            // textBoxIdVideoSort
            // 
            this.textBoxIdVideoSort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxIdVideoSort.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.textBoxIdVideoSort.Location = new System.Drawing.Point(162, 64);
            this.textBoxIdVideoSort.Name = "textBoxIdVideoSort";
            this.textBoxIdVideoSort.Size = new System.Drawing.Size(231, 30);
            this.textBoxIdVideoSort.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label3.Location = new System.Drawing.Point(18, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 23);
            this.label3.TabIndex = 25;
            this.label3.Text = "Идентификатор";
            // 
            // dataGridViewThemesSort
            // 
            this.dataGridViewThemesSort.AllowUserToAddRows = false;
            this.dataGridViewThemesSort.AllowUserToDeleteRows = false;
            this.dataGridViewThemesSort.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewThemesSort.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnNumThemeSort,
            this.ColumnTitleThemeSort,
            this.ColumnChoiseThemeSort});
            this.dataGridViewThemesSort.Location = new System.Drawing.Point(22, 133);
            this.dataGridViewThemesSort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewThemesSort.Name = "dataGridViewThemesSort";
            this.dataGridViewThemesSort.RowHeadersWidth = 51;
            this.dataGridViewThemesSort.RowTemplate.Height = 24;
            this.dataGridViewThemesSort.Size = new System.Drawing.Size(371, 129);
            this.dataGridViewThemesSort.TabIndex = 32;
            // 
            // ColumnNumThemeSort
            // 
            this.ColumnNumThemeSort.HeaderText = "№";
            this.ColumnNumThemeSort.MinimumWidth = 6;
            this.ColumnNumThemeSort.Name = "ColumnNumThemeSort";
            this.ColumnNumThemeSort.ReadOnly = true;
            this.ColumnNumThemeSort.Width = 60;
            // 
            // ColumnTitleThemeSort
            // 
            this.ColumnTitleThemeSort.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnTitleThemeSort.HeaderText = "Название";
            this.ColumnTitleThemeSort.MinimumWidth = 6;
            this.ColumnTitleThemeSort.Name = "ColumnTitleThemeSort";
            this.ColumnTitleThemeSort.ReadOnly = true;
            // 
            // ColumnChoiseThemeSort
            // 
            this.ColumnChoiseThemeSort.HeaderText = "Выбор";
            this.ColumnChoiseThemeSort.MinimumWidth = 6;
            this.ColumnChoiseThemeSort.Name = "ColumnChoiseThemeSort";
            this.ColumnChoiseThemeSort.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnChoiseThemeSort.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColumnChoiseThemeSort.Width = 60;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label4.Location = new System.Drawing.Point(18, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 23);
            this.label4.TabIndex = 31;
            this.label4.Text = "Тема";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label6.Location = new System.Drawing.Point(19, 500);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 20);
            this.label6.TabIndex = 37;
            this.label6.Text = "До";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label7.Location = new System.Drawing.Point(19, 456);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 20);
            this.label7.TabIndex = 36;
            this.label7.Text = "После";
            // 
            // dateTimePickerEndSort
            // 
            this.dateTimePickerEndSort.CalendarFont = new System.Drawing.Font("Segoe UI", 9F);
            this.dateTimePickerEndSort.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dateTimePickerEndSort.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEndSort.Location = new System.Drawing.Point(82, 499);
            this.dateTimePickerEndSort.MinDate = new System.DateTime(2005, 2, 14, 0, 0, 0, 0);
            this.dateTimePickerEndSort.Name = "dateTimePickerEndSort";
            this.dateTimePickerEndSort.Size = new System.Drawing.Size(266, 27);
            this.dateTimePickerEndSort.TabIndex = 33;
            this.toolTipInfo.SetToolTip(this.dateTimePickerEndSort, "Максимальная дата");
            this.dateTimePickerEndSort.Value = new System.DateTime(2024, 8, 3, 0, 0, 0, 0);
            // 
            // dateTimePickerStartSort
            // 
            this.dateTimePickerStartSort.CalendarFont = new System.Drawing.Font("Segoe UI", 9F);
            this.dateTimePickerStartSort.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dateTimePickerStartSort.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStartSort.Location = new System.Drawing.Point(82, 454);
            this.dateTimePickerStartSort.MinDate = new System.DateTime(2005, 2, 14, 0, 0, 0, 0);
            this.dateTimePickerStartSort.Name = "dateTimePickerStartSort";
            this.dateTimePickerStartSort.Size = new System.Drawing.Size(266, 27);
            this.dateTimePickerStartSort.TabIndex = 34;
            this.toolTipInfo.SetToolTip(this.dateTimePickerStartSort, "Минимальная дата");
            this.dateTimePickerStartSort.Value = new System.DateTime(2005, 2, 14, 0, 0, 0, 0);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label8.Location = new System.Drawing.Point(19, 419);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(146, 23);
            this.label8.TabIndex = 38;
            this.label8.Text = "Дата публикации";
            // 
            // textBoxMaxDurVideoSort
            // 
            this.textBoxMaxDurVideoSort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxMaxDurVideoSort.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxMaxDurVideoSort.Location = new System.Drawing.Point(23, 377);
            this.textBoxMaxDurVideoSort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxMaxDurVideoSort.Name = "textBoxMaxDurVideoSort";
            this.textBoxMaxDurVideoSort.Size = new System.Drawing.Size(92, 27);
            this.textBoxMaxDurVideoSort.TabIndex = 44;
            // 
            // comboBoxMaxDurVideoSort
            // 
            this.comboBoxMaxDurVideoSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMaxDurVideoSort.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.comboBoxMaxDurVideoSort.FormattingEnabled = true;
            this.comboBoxMaxDurVideoSort.Items.AddRange(new object[] {
            "Секунд",
            "Минут",
            "Часов"});
            this.comboBoxMaxDurVideoSort.Location = new System.Drawing.Point(128, 377);
            this.comboBoxMaxDurVideoSort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxMaxDurVideoSort.Name = "comboBoxMaxDurVideoSort";
            this.comboBoxMaxDurVideoSort.Size = new System.Drawing.Size(148, 28);
            this.comboBoxMaxDurVideoSort.TabIndex = 43;
            // 
            // textBoxMinDurVideoSort
            // 
            this.textBoxMinDurVideoSort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxMinDurVideoSort.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxMinDurVideoSort.Location = new System.Drawing.Point(22, 309);
            this.textBoxMinDurVideoSort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBoxMinDurVideoSort.Name = "textBoxMinDurVideoSort";
            this.textBoxMinDurVideoSort.Size = new System.Drawing.Size(92, 27);
            this.textBoxMinDurVideoSort.TabIndex = 42;
            // 
            // comboBoxMinDurVideoSort
            // 
            this.comboBoxMinDurVideoSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMinDurVideoSort.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.comboBoxMinDurVideoSort.FormattingEnabled = true;
            this.comboBoxMinDurVideoSort.Items.AddRange(new object[] {
            "Секунд",
            "Минут",
            "Часов"});
            this.comboBoxMinDurVideoSort.Location = new System.Drawing.Point(127, 309);
            this.comboBoxMinDurVideoSort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxMinDurVideoSort.Name = "comboBoxMinDurVideoSort";
            this.comboBoxMinDurVideoSort.Size = new System.Drawing.Size(148, 28);
            this.comboBoxMinDurVideoSort.TabIndex = 41;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label5.Location = new System.Drawing.Point(18, 346);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(238, 23);
            this.label5.TabIndex = 40;
            this.label5.Text = "Максимальная длительность";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label9.Location = new System.Drawing.Point(18, 278);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(233, 23);
            this.label9.TabIndex = 39;
            this.label9.Text = "Минимальная длительность";
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnReset.Location = new System.Drawing.Point(681, 570);
            this.btnReset.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(125, 42);
            this.btnReset.TabIndex = 45;
            this.btnReset.Text = "Сбросить";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnDateToday
            // 
            this.btnDateToday.BackColor = System.Drawing.Color.White;
            this.btnDateToday.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDateToday.BackgroundImage")));
            this.btnDateToday.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDateToday.Location = new System.Drawing.Point(354, 494);
            this.btnDateToday.Name = "btnDateToday";
            this.btnDateToday.Size = new System.Drawing.Size(40, 40);
            this.btnDateToday.TabIndex = 48;
            this.toolTipInfo.SetToolTip(this.btnDateToday, "Сегодня");
            this.btnDateToday.UseVisualStyleBackColor = false;
            this.btnDateToday.Click += new System.EventHandler(this.btnDateToday_Click);
            // 
            // btnDateCreateChannel
            // 
            this.btnDateCreateChannel.BackColor = System.Drawing.Color.White;
            this.btnDateCreateChannel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDateCreateChannel.BackgroundImage")));
            this.btnDateCreateChannel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDateCreateChannel.Location = new System.Drawing.Point(354, 448);
            this.btnDateCreateChannel.Name = "btnDateCreateChannel";
            this.btnDateCreateChannel.Size = new System.Drawing.Size(40, 40);
            this.btnDateCreateChannel.TabIndex = 47;
            this.toolTipInfo.SetToolTip(this.btnDateCreateChannel, "Дата создания");
            this.btnDateCreateChannel.UseVisualStyleBackColor = false;
            this.btnDateCreateChannel.Click += new System.EventHandler(this.btnDateCreateChannel_Click);
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
            this.comboBoxUsed.Location = new System.Drawing.Point(110, 577);
            this.comboBoxUsed.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBoxUsed.Name = "comboBoxUsed";
            this.comboBoxUsed.Size = new System.Drawing.Size(418, 28);
            this.comboBoxUsed.TabIndex = 49;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label10.Location = new System.Drawing.Point(19, 579);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 23);
            this.label10.TabIndex = 50;
            this.label10.Text = "Получить";
            // 
            // dataGridViewHistorySort
            // 
            this.dataGridViewHistorySort.AllowUserToAddRows = false;
            this.dataGridViewHistorySort.AllowUserToDeleteRows = false;
            this.dataGridViewHistorySort.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHistorySort.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnNumHistory,
            this.ColumnDateHistory,
            this.ColumnChoiseHistorySort});
            this.dataGridViewHistorySort.Location = new System.Drawing.Point(435, 308);
            this.dataGridViewHistorySort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewHistorySort.Name = "dataGridViewHistorySort";
            this.dataGridViewHistorySort.RowHeadersWidth = 51;
            this.dataGridViewHistorySort.RowTemplate.Height = 24;
            this.dataGridViewHistorySort.Size = new System.Drawing.Size(371, 226);
            this.dataGridViewHistorySort.TabIndex = 52;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label11.Location = new System.Drawing.Point(431, 278);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(156, 23);
            this.label11.TabIndex = 51;
            this.label11.Text = "История запросов";
            // 
            // ColumnNumHistory
            // 
            this.ColumnNumHistory.HeaderText = "№";
            this.ColumnNumHistory.MinimumWidth = 6;
            this.ColumnNumHistory.Name = "ColumnNumHistory";
            this.ColumnNumHistory.ReadOnly = true;
            this.ColumnNumHistory.Width = 60;
            // 
            // ColumnDateHistory
            // 
            this.ColumnDateHistory.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnDateHistory.HeaderText = "Дата";
            this.ColumnDateHistory.MinimumWidth = 6;
            this.ColumnDateHistory.Name = "ColumnDateHistory";
            this.ColumnDateHistory.ReadOnly = true;
            // 
            // ColumnChoiseHistorySort
            // 
            this.ColumnChoiseHistorySort.HeaderText = "Выбор";
            this.ColumnChoiseHistorySort.MinimumWidth = 6;
            this.ColumnChoiseHistorySort.Name = "ColumnChoiseHistorySort";
            this.ColumnChoiseHistorySort.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnChoiseHistorySort.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColumnChoiseHistorySort.Width = 60;
            // 
            // SortVideosInTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(823, 626);
            this.Controls.Add(this.dataGridViewHistorySort);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.comboBoxUsed);
            this.Controls.Add(this.btnDateToday);
            this.Controls.Add(this.btnDateCreateChannel);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.textBoxMaxDurVideoSort);
            this.Controls.Add(this.comboBoxMaxDurVideoSort);
            this.Controls.Add(this.textBoxMinDurVideoSort);
            this.Controls.Add(this.comboBoxMinDurVideoSort);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dateTimePickerEndSort);
            this.Controls.Add(this.dateTimePickerStartSort);
            this.Controls.Add(this.dataGridViewThemesSort);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxIdVideoSort);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxTitleVideoSort);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridViewChannelsSort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSave);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SortVideosInTable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Фильтр видео";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewChannelsSort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewThemesSort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHistorySort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridViewChannelsSort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxTitleVideoSort;
        private System.Windows.Forms.TextBox textBoxIdVideoSort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridViewThemesSort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dateTimePickerEndSort;
        private System.Windows.Forms.DateTimePicker dateTimePickerStartSort;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxMaxDurVideoSort;
        private System.Windows.Forms.ComboBox comboBoxMaxDurVideoSort;
        private System.Windows.Forms.TextBox textBoxMinDurVideoSort;
        private System.Windows.Forms.ComboBox comboBoxMinDurVideoSort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNumChoiseChannels;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNameChannels;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnChoiseChannel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNumThemeSort;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTitleThemeSort;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnChoiseThemeSort;
        private System.Windows.Forms.Button btnDateToday;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.Button btnDateCreateChannel;
        private System.Windows.Forms.ComboBox comboBoxUsed;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView dataGridViewHistorySort;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnNumHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDateHistory;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColumnChoiseHistorySort;
        private System.Windows.Forms.Label label11;
    }
}