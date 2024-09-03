namespace YouTubeVideoSearch
{
    partial class ReloadChannelsData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReloadChannelsData));
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.progressBarLoading = new System.Windows.Forms.ProgressBar();
            this.panelLoading = new System.Windows.Forms.Panel();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelChannelName = new System.Windows.Forms.Label();
            this.btnCancelLoading = new System.Windows.Forms.Button();
            this.backgroundWorkerLoading = new System.ComponentModel.BackgroundWorker();
            this.panelLoading.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBarLoading
            // 
            this.progressBarLoading.Location = new System.Drawing.Point(6, 92);
            this.progressBarLoading.MarqueeAnimationSpeed = 50;
            this.progressBarLoading.Maximum = 1000;
            this.progressBarLoading.Name = "progressBarLoading";
            this.progressBarLoading.Size = new System.Drawing.Size(379, 31);
            this.progressBarLoading.Step = 1;
            this.progressBarLoading.TabIndex = 25;
            // 
            // panelLoading
            // 
            this.panelLoading.Controls.Add(this.labelStatus);
            this.panelLoading.Controls.Add(this.labelChannelName);
            this.panelLoading.Controls.Add(this.progressBarLoading);
            this.panelLoading.Controls.Add(this.btnCancelLoading);
            this.panelLoading.Location = new System.Drawing.Point(12, 12);
            this.panelLoading.Name = "panelLoading";
            this.panelLoading.Size = new System.Drawing.Size(392, 185);
            this.panelLoading.TabIndex = 26;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelStatus.Location = new System.Drawing.Point(3, 62);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(90, 23);
            this.labelStatus.TabIndex = 27;
            this.labelStatus.Text = "Загрузка...";
            // 
            // labelChannelName
            // 
            this.labelChannelName.AutoSize = true;
            this.labelChannelName.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelChannelName.Location = new System.Drawing.Point(3, 12);
            this.labelChannelName.MaximumSize = new System.Drawing.Size(400, 0);
            this.labelChannelName.Name = "labelChannelName";
            this.labelChannelName.Size = new System.Drawing.Size(61, 23);
            this.labelChannelName.TabIndex = 26;
            this.labelChannelName.Text = "Канал:";
            // 
            // btnCancelLoading
            // 
            this.btnCancelLoading.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancelLoading.Location = new System.Drawing.Point(254, 133);
            this.btnCancelLoading.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancelLoading.Name = "btnCancelLoading";
            this.btnCancelLoading.Size = new System.Drawing.Size(131, 42);
            this.btnCancelLoading.TabIndex = 8;
            this.btnCancelLoading.Text = "Отменить";
            this.btnCancelLoading.UseVisualStyleBackColor = true;
            this.btnCancelLoading.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ReloadChannelsData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(416, 206);
            this.Controls.Add(this.panelLoading);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReloadChannelsData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Обновление каналов";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReloadChannelsData_FormClosing);
            this.panelLoading.ResumeLayout(false);
            this.panelLoading.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.ProgressBar progressBarLoading;
        private System.Windows.Forms.Panel panelLoading;
        private System.Windows.Forms.Label labelChannelName;
        private System.Windows.Forms.Button btnCancelLoading;
        private System.Windows.Forms.Label labelStatus;
        private System.ComponentModel.BackgroundWorker backgroundWorkerLoading;
    }
}