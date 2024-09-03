namespace YouTubeVideoSearch
{
    partial class OtherMethods
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OtherMethods));
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelChannelName = new System.Windows.Forms.Label();
            this.progressBarLoading = new System.Windows.Forms.ProgressBar();
            this.btnCancelLoading = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelStatus.Location = new System.Drawing.Point(12, 80);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(90, 23);
            this.labelStatus.TabIndex = 31;
            this.labelStatus.Text = "Загрузка...";
            // 
            // labelChannelName
            // 
            this.labelChannelName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelChannelName.Location = new System.Drawing.Point(9, 10);
            this.labelChannelName.MaximumSize = new System.Drawing.Size(400, 0);
            this.labelChannelName.Name = "labelChannelName";
            this.labelChannelName.Size = new System.Drawing.Size(382, 70);
            this.labelChannelName.TabIndex = 30;
            this.labelChannelName.Text = "Ссылка:";
            // 
            // progressBarLoading
            // 
            this.progressBarLoading.Location = new System.Drawing.Point(12, 109);
            this.progressBarLoading.MarqueeAnimationSpeed = 50;
            this.progressBarLoading.Maximum = 1000;
            this.progressBarLoading.Name = "progressBarLoading";
            this.progressBarLoading.Size = new System.Drawing.Size(379, 31);
            this.progressBarLoading.Step = 1;
            this.progressBarLoading.TabIndex = 29;
            // 
            // btnCancelLoading
            // 
            this.btnCancelLoading.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancelLoading.Location = new System.Drawing.Point(260, 150);
            this.btnCancelLoading.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancelLoading.Name = "btnCancelLoading";
            this.btnCancelLoading.Size = new System.Drawing.Size(131, 42);
            this.btnCancelLoading.TabIndex = 28;
            this.btnCancelLoading.Text = "Отменить";
            this.btnCancelLoading.UseVisualStyleBackColor = true;
            // 
            // OtherMethods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(403, 203);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.labelChannelName);
            this.Controls.Add(this.progressBarLoading);
            this.Controls.Add(this.btnCancelLoading);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OtherMethods";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Загрузка";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelChannelName;
        private System.Windows.Forms.ProgressBar progressBarLoading;
        private System.Windows.Forms.Button btnCancelLoading;
    }
}