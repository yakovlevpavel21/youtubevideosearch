namespace YouTubeVideoSearch
{
    partial class ChannelAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChannelAdd));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxSearchChannel = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePickerStart = new System.Windows.Forms.DateTimePicker();
            this.btnDateCreateChannel = new System.Windows.Forms.Button();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.dateTimePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.btnDateToday = new System.Windows.Forms.Button();
            this.richTextBoxData = new System.Windows.Forms.RichTextBox();
            this.progressBarLoading = new System.Windows.Forms.ProgressBar();
            this.panelLoading = new System.Windows.Forms.Panel();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelChannel = new System.Windows.Forms.Label();
            this.btnCancelLoading = new System.Windows.Forms.Button();
            this.backgroundWorkerLoading = new System.ComponentModel.BackgroundWorker();
            this.panelAdd = new System.Windows.Forms.Panel();
            this.checkBoxLoadNew = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelTotalVideos = new System.Windows.Forms.Label();
            this.panelLoading.SuspendLayout();
            this.panelAdd.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.Location = new System.Drawing.Point(197, 460);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(188, 42);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCreate.Location = new System.Drawing.Point(7, 460);
            this.btnCreate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(179, 42);
            this.btnCreate.TabIndex = 9;
            this.btnCreate.Text = "Добавить";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.Location = new System.Drawing.Point(3, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(269, 23);
            this.label1.TabIndex = 18;
            this.label1.Text = "Введите данные (одну на строку)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label5.Location = new System.Drawing.Point(3, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 23);
            this.label5.TabIndex = 15;
            this.label5.Text = "Найти канал по";
            // 
            // comboBoxSearchChannel
            // 
            this.comboBoxSearchChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSearchChannel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboBoxSearchChannel.FormattingEnabled = true;
            this.comboBoxSearchChannel.Items.AddRange(new object[] {
            "идентификатору",
            "названию или псевдониму",
            "ссылке на видео"});
            this.comboBoxSearchChannel.Location = new System.Drawing.Point(134, 7);
            this.comboBoxSearchChannel.Name = "comboBoxSearchChannel";
            this.comboBoxSearchChannel.Size = new System.Drawing.Size(252, 31);
            this.comboBoxSearchChannel.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.Location = new System.Drawing.Point(3, 249);
            this.label2.MaximumSize = new System.Drawing.Size(400, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(386, 46);
            this.label2.TabIndex = 21;
            this.label2.Text = "Выберите период за который нужно загрузить видео с канала";
            // 
            // dateTimePickerStart
            // 
            this.dateTimePickerStart.CalendarFont = new System.Drawing.Font("Segoe UI", 9F);
            this.dateTimePickerStart.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dateTimePickerStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerStart.Location = new System.Drawing.Point(61, 311);
            this.dateTimePickerStart.MinDate = new System.DateTime(2005, 2, 14, 0, 0, 0, 0);
            this.dateTimePickerStart.Name = "dateTimePickerStart";
            this.dateTimePickerStart.Size = new System.Drawing.Size(278, 27);
            this.dateTimePickerStart.TabIndex = 22;
            this.toolTipInfo.SetToolTip(this.dateTimePickerStart, "Минимальная дата");
            this.dateTimePickerStart.Value = new System.DateTime(2005, 2, 14, 0, 0, 0, 0);
            // 
            // btnDateCreateChannel
            // 
            this.btnDateCreateChannel.BackColor = System.Drawing.Color.White;
            this.btnDateCreateChannel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDateCreateChannel.BackgroundImage")));
            this.btnDateCreateChannel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDateCreateChannel.Location = new System.Drawing.Point(345, 304);
            this.btnDateCreateChannel.Name = "btnDateCreateChannel";
            this.btnDateCreateChannel.Size = new System.Drawing.Size(40, 40);
            this.btnDateCreateChannel.TabIndex = 23;
            this.toolTipInfo.SetToolTip(this.btnDateCreateChannel, "Дата создания канала");
            this.btnDateCreateChannel.UseVisualStyleBackColor = false;
            this.btnDateCreateChannel.Click += new System.EventHandler(this.btnDateCreateChannel_Click);
            // 
            // dateTimePickerEnd
            // 
            this.dateTimePickerEnd.CalendarFont = new System.Drawing.Font("Segoe UI", 9F);
            this.dateTimePickerEnd.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dateTimePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerEnd.Location = new System.Drawing.Point(61, 356);
            this.dateTimePickerEnd.MinDate = new System.DateTime(2005, 2, 14, 0, 0, 0, 0);
            this.dateTimePickerEnd.Name = "dateTimePickerEnd";
            this.dateTimePickerEnd.Size = new System.Drawing.Size(278, 27);
            this.dateTimePickerEnd.TabIndex = 22;
            this.toolTipInfo.SetToolTip(this.dateTimePickerEnd, "Максимальная дата");
            this.dateTimePickerEnd.Value = new System.DateTime(2024, 8, 3, 0, 0, 0, 0);
            // 
            // btnDateToday
            // 
            this.btnDateToday.BackColor = System.Drawing.Color.White;
            this.btnDateToday.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDateToday.BackgroundImage")));
            this.btnDateToday.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDateToday.Location = new System.Drawing.Point(345, 350);
            this.btnDateToday.Name = "btnDateToday";
            this.btnDateToday.Size = new System.Drawing.Size(40, 40);
            this.btnDateToday.TabIndex = 27;
            this.toolTipInfo.SetToolTip(this.btnDateToday, "Сегодня");
            this.btnDateToday.UseVisualStyleBackColor = false;
            this.btnDateToday.Click += new System.EventHandler(this.btnDateToday_Click);
            // 
            // richTextBoxData
            // 
            this.richTextBoxData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxData.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.richTextBoxData.Location = new System.Drawing.Point(6, 92);
            this.richTextBoxData.Name = "richTextBoxData";
            this.richTextBoxData.Size = new System.Drawing.Size(379, 143);
            this.richTextBoxData.TabIndex = 24;
            this.richTextBoxData.Text = "";
            // 
            // progressBarLoading
            // 
            this.progressBarLoading.Location = new System.Drawing.Point(7, 115);
            this.progressBarLoading.Maximum = 1000;
            this.progressBarLoading.Name = "progressBarLoading";
            this.progressBarLoading.Size = new System.Drawing.Size(376, 19);
            this.progressBarLoading.Step = 1;
            this.progressBarLoading.TabIndex = 25;
            // 
            // panelLoading
            // 
            this.panelLoading.Controls.Add(this.labelTotalVideos);
            this.panelLoading.Controls.Add(this.labelStatus);
            this.panelLoading.Controls.Add(this.labelChannel);
            this.panelLoading.Controls.Add(this.progressBarLoading);
            this.panelLoading.Controls.Add(this.btnCancelLoading);
            this.panelLoading.Location = new System.Drawing.Point(12, 12);
            this.panelLoading.Name = "panelLoading";
            this.panelLoading.Size = new System.Drawing.Size(392, 185);
            this.panelLoading.TabIndex = 26;
            this.panelLoading.Visible = false;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelStatus.Location = new System.Drawing.Point(4, 84);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(90, 23);
            this.labelStatus.TabIndex = 27;
            this.labelStatus.Text = "Загрузка...";
            // 
            // labelChannel
            // 
            this.labelChannel.AutoSize = true;
            this.labelChannel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelChannel.Location = new System.Drawing.Point(3, 9);
            this.labelChannel.MaximumSize = new System.Drawing.Size(400, 0);
            this.labelChannel.Name = "labelChannel";
            this.labelChannel.Size = new System.Drawing.Size(154, 23);
            this.labelChannel.TabIndex = 26;
            this.labelChannel.Text = "Название канала: ";
            // 
            // btnCancelLoading
            // 
            this.btnCancelLoading.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancelLoading.Location = new System.Drawing.Point(288, 146);
            this.btnCancelLoading.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancelLoading.Name = "btnCancelLoading";
            this.btnCancelLoading.Size = new System.Drawing.Size(95, 30);
            this.btnCancelLoading.TabIndex = 8;
            this.btnCancelLoading.Text = "Отменить";
            this.btnCancelLoading.UseVisualStyleBackColor = true;
            this.btnCancelLoading.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelAdd
            // 
            this.panelAdd.Controls.Add(this.checkBoxLoadNew);
            this.panelAdd.Controls.Add(this.btnDateToday);
            this.panelAdd.Controls.Add(this.label4);
            this.panelAdd.Controls.Add(this.label3);
            this.panelAdd.Controls.Add(this.btnCreate);
            this.panelAdd.Controls.Add(this.richTextBoxData);
            this.panelAdd.Controls.Add(this.btnCancel);
            this.panelAdd.Controls.Add(this.btnDateCreateChannel);
            this.panelAdd.Controls.Add(this.label1);
            this.panelAdd.Controls.Add(this.dateTimePickerEnd);
            this.panelAdd.Controls.Add(this.dateTimePickerStart);
            this.panelAdd.Controls.Add(this.comboBoxSearchChannel);
            this.panelAdd.Controls.Add(this.label2);
            this.panelAdd.Controls.Add(this.label5);
            this.panelAdd.Location = new System.Drawing.Point(12, 12);
            this.panelAdd.Name = "panelAdd";
            this.panelAdd.Size = new System.Drawing.Size(392, 502);
            this.panelAdd.TabIndex = 27;
            // 
            // checkBoxLoadNew
            // 
            this.checkBoxLoadNew.AutoSize = true;
            this.checkBoxLoadNew.Checked = true;
            this.checkBoxLoadNew.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxLoadNew.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.checkBoxLoadNew.Location = new System.Drawing.Point(7, 403);
            this.checkBoxLoadNew.Name = "checkBoxLoadNew";
            this.checkBoxLoadNew.Size = new System.Drawing.Size(260, 24);
            this.checkBoxLoadNew.TabIndex = 28;
            this.checkBoxLoadNew.Text = "Загружать новые видео с канала";
            this.checkBoxLoadNew.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label4.Location = new System.Drawing.Point(3, 359);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 20);
            this.label4.TabIndex = 26;
            this.label4.Text = "До";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label3.Location = new System.Drawing.Point(3, 313);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 20);
            this.label3.TabIndex = 25;
            this.label3.Text = "После";
            // 
            // labelTotalVideos
            // 
            this.labelTotalVideos.AutoSize = true;
            this.labelTotalVideos.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelTotalVideos.Location = new System.Drawing.Point(4, 36);
            this.labelTotalVideos.MaximumSize = new System.Drawing.Size(400, 0);
            this.labelTotalVideos.Name = "labelTotalVideos";
            this.labelTotalVideos.Size = new System.Drawing.Size(110, 23);
            this.labelTotalVideos.TabIndex = 28;
            this.labelTotalVideos.Text = "Всего видео:";
            this.labelTotalVideos.Visible = false;
            // 
            // ChannelAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(416, 526);
            this.Controls.Add(this.panelLoading);
            this.Controls.Add(this.panelAdd);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChannelAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление канала";
            this.panelLoading.ResumeLayout(false);
            this.panelLoading.PerformLayout();
            this.panelAdd.ResumeLayout(false);
            this.panelAdd.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxSearchChannel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePickerStart;
        private System.Windows.Forms.Button btnDateCreateChannel;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.RichTextBox richTextBoxData;
        private System.Windows.Forms.ProgressBar progressBarLoading;
        private System.Windows.Forms.Panel panelLoading;
        private System.Windows.Forms.Label labelChannel;
        private System.Windows.Forms.Button btnCancelLoading;
        private System.Windows.Forms.Label labelStatus;
        private System.ComponentModel.BackgroundWorker backgroundWorkerLoading;
        private System.Windows.Forms.Panel panelAdd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePickerEnd;
        private System.Windows.Forms.Button btnDateToday;
        private System.Windows.Forms.CheckBox checkBoxLoadNew;
        private System.Windows.Forms.Label labelTotalVideos;
    }
}