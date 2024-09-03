namespace YouTubeVideoSearch
{
    partial class APIKeyAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(APIKeyAdd));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.backgroundWorkerLoading = new System.ComponentModel.BackgroundWorker();
            this.panelAdd = new System.Windows.Forms.Panel();
            this.pictureBoxInfoGoogleConsole = new System.Windows.Forms.PictureBox();
            this.textBoxAPIKey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panelAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfoGoogleConsole)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.Location = new System.Drawing.Point(197, 86);
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
            this.btnCreate.Location = new System.Drawing.Point(7, 86);
            this.btnCreate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(179, 42);
            this.btnCreate.TabIndex = 9;
            this.btnCreate.Text = "Добавить";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // panelAdd
            // 
            this.panelAdd.Controls.Add(this.pictureBoxInfoGoogleConsole);
            this.panelAdd.Controls.Add(this.textBoxAPIKey);
            this.panelAdd.Controls.Add(this.label2);
            this.panelAdd.Controls.Add(this.btnCreate);
            this.panelAdd.Controls.Add(this.btnCancel);
            this.panelAdd.Location = new System.Drawing.Point(12, 12);
            this.panelAdd.Name = "panelAdd";
            this.panelAdd.Size = new System.Drawing.Size(392, 137);
            this.panelAdd.TabIndex = 27;
            // 
            // pictureBoxInfoGoogleConsole
            // 
            this.pictureBoxInfoGoogleConsole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxInfoGoogleConsole.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxInfoGoogleConsole.Image")));
            this.pictureBoxInfoGoogleConsole.Location = new System.Drawing.Point(358, 9);
            this.pictureBoxInfoGoogleConsole.Name = "pictureBoxInfoGoogleConsole";
            this.pictureBoxInfoGoogleConsole.Size = new System.Drawing.Size(25, 25);
            this.pictureBoxInfoGoogleConsole.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxInfoGoogleConsole.TabIndex = 49;
            this.pictureBoxInfoGoogleConsole.TabStop = false;
            this.pictureBoxInfoGoogleConsole.Click += new System.EventHandler(this.pictureBoxInfoGoogleConsole_Click);
            // 
            // textBoxAPIKey
            // 
            this.textBoxAPIKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxAPIKey.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.textBoxAPIKey.Location = new System.Drawing.Point(7, 42);
            this.textBoxAPIKey.Name = "textBoxAPIKey";
            this.textBoxAPIKey.Size = new System.Drawing.Size(379, 27);
            this.textBoxAPIKey.TabIndex = 46;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.Location = new System.Drawing.Point(2, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 23);
            this.label2.TabIndex = 45;
            this.label2.Text = "Введите API ключ";
            // 
            // APIKeyAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(416, 159);
            this.Controls.Add(this.panelAdd);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "APIKeyAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление темы";
            this.panelAdd.ResumeLayout(false);
            this.panelAdd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfoGoogleConsole)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.ComponentModel.BackgroundWorker backgroundWorkerLoading;
        private System.Windows.Forms.Panel panelAdd;
        private System.Windows.Forms.TextBox textBoxAPIKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBoxInfoGoogleConsole;
    }
}