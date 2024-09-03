namespace YouTubeVideoSearch
{
    partial class CopyListSearchVideos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CopyListSearchVideos));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.checkBoxNumLinks = new System.Windows.Forms.CheckBox();
            this.checkBoxOnlyId = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBoxLinks = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.Location = new System.Drawing.Point(214, 416);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(179, 42);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCopy.Location = new System.Drawing.Point(22, 416);
            this.btnCopy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(180, 42);
            this.btnCopy.TabIndex = 9;
            this.btnCopy.Text = "Скопировать";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // checkBoxNumLinks
            // 
            this.checkBoxNumLinks.AutoSize = true;
            this.checkBoxNumLinks.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.checkBoxNumLinks.Location = new System.Drawing.Point(22, 330);
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
            this.checkBoxOnlyId.Location = new System.Drawing.Point(22, 363);
            this.checkBoxOnlyId.Name = "checkBoxOnlyId";
            this.checkBoxOnlyId.Size = new System.Drawing.Size(109, 27);
            this.checkBoxOnlyId.TabIndex = 17;
            this.checkBoxOnlyId.Text = "Только ID";
            this.checkBoxOnlyId.UseVisualStyleBackColor = true;
            this.checkBoxOnlyId.CheckedChanged += new System.EventHandler(this.checkBoxOnlyId_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.Location = new System.Drawing.Point(18, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 23);
            this.label2.TabIndex = 21;
            this.label2.Text = "Список ссылок";
            // 
            // richTextBoxLinks
            // 
            this.richTextBoxLinks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBoxLinks.Location = new System.Drawing.Point(22, 48);
            this.richTextBoxLinks.Name = "richTextBoxLinks";
            this.richTextBoxLinks.Size = new System.Drawing.Size(371, 270);
            this.richTextBoxLinks.TabIndex = 22;
            this.richTextBoxLinks.Text = "";
            // 
            // CopyListSearchVideos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(416, 478);
            this.Controls.Add(this.richTextBoxLinks);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkBoxOnlyId);
            this.Controls.Add(this.checkBoxNumLinks);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCopy);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CopyListSearchVideos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Копирование ссылок";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.CheckBox checkBoxNumLinks;
        private System.Windows.Forms.CheckBox checkBoxOnlyId;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox richTextBoxLinks;
    }
}