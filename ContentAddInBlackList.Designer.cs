namespace YouTubeVideoSearch
{
    partial class СontentAddInBlackList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(СontentAddInBlackList));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxTypeContent = new System.Windows.Forms.ComboBox();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.richTextBoxData = new System.Windows.Forms.RichTextBox();
            this.progressBarLoading = new System.Windows.Forms.ProgressBar();
            this.panelLoading = new System.Windows.Forms.Panel();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelChannel = new System.Windows.Forms.Label();
            this.btnCancelLoading = new System.Windows.Forms.Button();
            this.backgroundWorkerLoading = new System.ComponentModel.BackgroundWorker();
            this.panelAdd = new System.Windows.Forms.Panel();
            this.comboBoxSearchMethod = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panelLoading.SuspendLayout();
            this.panelAdd.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.Location = new System.Drawing.Point(197, 311);
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
            this.btnCreate.Location = new System.Drawing.Point(7, 311);
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
            this.label1.Location = new System.Drawing.Point(3, 109);
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
            this.label5.Size = new System.Drawing.Size(114, 23);
            this.label5.TabIndex = 15;
            this.label5.Text = "Тип контента";
            // 
            // comboBoxTypeContent
            // 
            this.comboBoxTypeContent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTypeContent.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboBoxTypeContent.FormattingEnabled = true;
            this.comboBoxTypeContent.Items.AddRange(new object[] {
            "канал",
            "видео"});
            this.comboBoxTypeContent.Location = new System.Drawing.Point(123, 7);
            this.comboBoxTypeContent.Name = "comboBoxTypeContent";
            this.comboBoxTypeContent.Size = new System.Drawing.Size(263, 31);
            this.comboBoxTypeContent.TabIndex = 19;
            this.comboBoxTypeContent.SelectedIndexChanged += new System.EventHandler(this.comboBoxTypeContent_SelectedIndexChanged);
            // 
            // richTextBoxData
            // 
            this.richTextBoxData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxData.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.richTextBoxData.Location = new System.Drawing.Point(6, 142);
            this.richTextBoxData.Name = "richTextBoxData";
            this.richTextBoxData.Size = new System.Drawing.Size(379, 143);
            this.richTextBoxData.TabIndex = 24;
            this.richTextBoxData.Text = "";
            // 
            // progressBarLoading
            // 
            this.progressBarLoading.Location = new System.Drawing.Point(6, 92);
            this.progressBarLoading.Maximum = 1000;
            this.progressBarLoading.Name = "progressBarLoading";
            this.progressBarLoading.Size = new System.Drawing.Size(379, 31);
            this.progressBarLoading.Step = 1;
            this.progressBarLoading.TabIndex = 25;
            // 
            // panelLoading
            // 
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
            this.labelStatus.Location = new System.Drawing.Point(3, 59);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(90, 23);
            this.labelStatus.TabIndex = 27;
            this.labelStatus.Text = "Загрузка...";
            // 
            // labelChannel
            // 
            this.labelChannel.AutoSize = true;
            this.labelChannel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelChannel.Location = new System.Drawing.Point(3, 15);
            this.labelChannel.Name = "labelChannel";
            this.labelChannel.Size = new System.Drawing.Size(154, 23);
            this.labelChannel.TabIndex = 26;
            this.labelChannel.Text = "Название канала: ";
            // 
            // btnCancelLoading
            // 
            this.btnCancelLoading.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancelLoading.Location = new System.Drawing.Point(254, 132);
            this.btnCancelLoading.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancelLoading.Name = "btnCancelLoading";
            this.btnCancelLoading.Size = new System.Drawing.Size(131, 42);
            this.btnCancelLoading.TabIndex = 8;
            this.btnCancelLoading.Text = "Отменить";
            this.btnCancelLoading.UseVisualStyleBackColor = true;
            this.btnCancelLoading.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelAdd
            // 
            this.panelAdd.Controls.Add(this.btnCreate);
            this.panelAdd.Controls.Add(this.richTextBoxData);
            this.panelAdd.Controls.Add(this.btnCancel);
            this.panelAdd.Controls.Add(this.label1);
            this.panelAdd.Controls.Add(this.comboBoxSearchMethod);
            this.panelAdd.Controls.Add(this.label2);
            this.panelAdd.Controls.Add(this.comboBoxTypeContent);
            this.panelAdd.Controls.Add(this.label5);
            this.panelAdd.Location = new System.Drawing.Point(12, 12);
            this.panelAdd.Name = "panelAdd";
            this.panelAdd.Size = new System.Drawing.Size(392, 361);
            this.panelAdd.TabIndex = 27;
            // 
            // comboBoxSearchMethod
            // 
            this.comboBoxSearchMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSearchMethod.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.comboBoxSearchMethod.FormattingEnabled = true;
            this.comboBoxSearchMethod.Items.AddRange(new object[] {
            "идентификатору",
            "названию или псевдониму",
            "ссылке на видео"});
            this.comboBoxSearchMethod.Location = new System.Drawing.Point(92, 55);
            this.comboBoxSearchMethod.Name = "comboBoxSearchMethod";
            this.comboBoxSearchMethod.Size = new System.Drawing.Size(294, 31);
            this.comboBoxSearchMethod.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.Location = new System.Drawing.Point(3, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 23);
            this.label2.TabIndex = 15;
            this.label2.Text = "Найти по";
            // 
            // СontentAddInBlackList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(416, 377);
            this.Controls.Add(this.panelAdd);
            this.Controls.Add(this.panelLoading);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "СontentAddInBlackList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление контента в ЧС";
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
        private System.Windows.Forms.ComboBox comboBoxTypeContent;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.RichTextBox richTextBoxData;
        private System.Windows.Forms.ProgressBar progressBarLoading;
        private System.Windows.Forms.Panel panelLoading;
        private System.Windows.Forms.Label labelChannel;
        private System.Windows.Forms.Button btnCancelLoading;
        private System.Windows.Forms.Label labelStatus;
        private System.ComponentModel.BackgroundWorker backgroundWorkerLoading;
        private System.Windows.Forms.Panel panelAdd;
        private System.Windows.Forms.ComboBox comboBoxSearchMethod;
        private System.Windows.Forms.Label label2;
    }
}