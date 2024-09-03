namespace YouTubeVideoSearch
{
    partial class SaveListSearchVideos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveListSearchVideos));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.checkBoxNumLinks = new System.Windows.Forms.CheckBox();
            this.checkBoxOnlyId = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPathFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnChoisePathFile = new System.Windows.Forms.Button();
            this.saveFileDialogLinks = new System.Windows.Forms.SaveFileDialog();
            this.richTextBoxLinks = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.Location = new System.Drawing.Point(214, 417);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(179, 42);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnSave.Location = new System.Drawing.Point(22, 417);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(180, 42);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // checkBoxNumLinks
            // 
            this.checkBoxNumLinks.AutoSize = true;
            this.checkBoxNumLinks.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.checkBoxNumLinks.Location = new System.Drawing.Point(22, 326);
            this.checkBoxNumLinks.Name = "checkBoxNumLinks";
            this.checkBoxNumLinks.Size = new System.Drawing.Size(121, 27);
            this.checkBoxNumLinks.TabIndex = 16;
            this.checkBoxNumLinks.Text = "Нумерация";
            this.checkBoxNumLinks.UseVisualStyleBackColor = true;
            this.checkBoxNumLinks.CheckedChanged += new System.EventHandler(this.checkBoxNumLinks_CheckedChanged);
            // 
            // checkBoxOnlyId
            // 
            this.checkBoxOnlyId.AutoSize = true;
            this.checkBoxOnlyId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.checkBoxOnlyId.Location = new System.Drawing.Point(22, 359);
            this.checkBoxOnlyId.Name = "checkBoxOnlyId";
            this.checkBoxOnlyId.Size = new System.Drawing.Size(109, 27);
            this.checkBoxOnlyId.TabIndex = 17;
            this.checkBoxOnlyId.Text = "Только ID";
            this.checkBoxOnlyId.UseVisualStyleBackColor = true;
            this.checkBoxOnlyId.CheckedChanged += new System.EventHandler(this.checkBoxOnlyId_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.Location = new System.Drawing.Point(18, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 23);
            this.label1.TabIndex = 18;
            this.label1.Text = "Путь к файлу";
            // 
            // textBoxPathFile
            // 
            this.textBoxPathFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxPathFile.Location = new System.Drawing.Point(22, 48);
            this.textBoxPathFile.Name = "textBoxPathFile";
            this.textBoxPathFile.Size = new System.Drawing.Size(338, 25);
            this.textBoxPathFile.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.Location = new System.Drawing.Point(18, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 23);
            this.label2.TabIndex = 20;
            this.label2.Text = "Список ссылок";
            // 
            // btnChoisePathFile
            // 
            this.btnChoisePathFile.Location = new System.Drawing.Point(366, 48);
            this.btnChoisePathFile.Name = "btnChoisePathFile";
            this.btnChoisePathFile.Size = new System.Drawing.Size(27, 25);
            this.btnChoisePathFile.TabIndex = 21;
            this.btnChoisePathFile.Text = "...";
            this.btnChoisePathFile.UseVisualStyleBackColor = true;
            this.btnChoisePathFile.Click += new System.EventHandler(this.btnChoisePathFile_Click);
            // 
            // richTextBoxLinks
            // 
            this.richTextBoxLinks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxLinks.Location = new System.Drawing.Point(22, 120);
            this.richTextBoxLinks.Name = "richTextBoxLinks";
            this.richTextBoxLinks.Size = new System.Drawing.Size(371, 190);
            this.richTextBoxLinks.TabIndex = 23;
            this.richTextBoxLinks.Text = "";
            // 
            // SaveListSearchVideos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(416, 479);
            this.Controls.Add(this.richTextBoxLinks);
            this.Controls.Add(this.btnChoisePathFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxPathFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxOnlyId);
            this.Controls.Add(this.checkBoxNumLinks);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SaveListSearchVideos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Сохранение ссылок";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox checkBoxNumLinks;
        private System.Windows.Forms.CheckBox checkBoxOnlyId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPathFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnChoisePathFile;
        private System.Windows.Forms.SaveFileDialog saveFileDialogLinks;
        private System.Windows.Forms.RichTextBox richTextBoxLinks;
    }
}