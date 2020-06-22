namespace SimpleCollageGenerator
{
    partial class frmMain
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
            this.txtSource = new System.Windows.Forms.TextBox();
            this.txtDestination = new System.Windows.Forms.MaskedTextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.lstLogs = new System.Windows.Forms.ListBox();
            this.trkQuality = new System.Windows.Forms.TrackBar();
            this.pctPreview = new System.Windows.Forms.PictureBox();
            this.btnBrowseSrc = new System.Windows.Forms.Button();
            this.btnBrowseDest = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trkQuality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(12, 15);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(561, 20);
            this.txtSource.TabIndex = 1;
            this.txtSource.Text = "D:\\Users\\rj\\Desktop\\TASKS\\Nationwide\\B41\\start";
            // 
            // txtDestination
            // 
            this.txtDestination.Location = new System.Drawing.Point(12, 41);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(561, 20);
            this.txtDestination.TabIndex = 3;
            this.txtDestination.Text = "E:\\TEST";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(579, 67);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 26);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lstLogs
            // 
            this.lstLogs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstLogs.FormattingEnabled = true;
            this.lstLogs.Location = new System.Drawing.Point(12, 125);
            this.lstLogs.Name = "lstLogs";
            this.lstLogs.Size = new System.Drawing.Size(810, 420);
            this.lstLogs.TabIndex = 7;
            // 
            // trkQuality
            // 
            this.trkQuality.Location = new System.Drawing.Point(12, 67);
            this.trkQuality.Minimum = 1;
            this.trkQuality.Name = "trkQuality";
            this.trkQuality.Size = new System.Drawing.Size(561, 45);
            this.trkQuality.TabIndex = 5;
            this.trkQuality.Value = 4;
            // 
            // pctPreview
            // 
            this.pctPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pctPreview.Location = new System.Drawing.Point(674, 15);
            this.pctPreview.Name = "pctPreview";
            this.pctPreview.Size = new System.Drawing.Size(148, 97);
            this.pctPreview.TabIndex = 5;
            this.pctPreview.TabStop = false;
            // 
            // btnBrowseSrc
            // 
            this.btnBrowseSrc.Location = new System.Drawing.Point(579, 9);
            this.btnBrowseSrc.Name = "btnBrowseSrc";
            this.btnBrowseSrc.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseSrc.TabIndex = 2;
            this.btnBrowseSrc.Text = "&Browse";
            this.btnBrowseSrc.UseVisualStyleBackColor = true;
            this.btnBrowseSrc.Click += new System.EventHandler(this.btnBrowseSrc_Click);
            // 
            // btnBrowseDest
            // 
            this.btnBrowseDest.Location = new System.Drawing.Point(579, 38);
            this.btnBrowseDest.Name = "btnBrowseDest";
            this.btnBrowseDest.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseDest.TabIndex = 4;
            this.btnBrowseDest.Text = "&Browse";
            this.btnBrowseDest.UseVisualStyleBackColor = true;
            this.btnBrowseDest.Click += new System.EventHandler(this.btnBrowseDest_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 561);
            this.Controls.Add(this.btnBrowseDest);
            this.Controls.Add(this.btnBrowseSrc);
            this.Controls.Add(this.pctPreview);
            this.Controls.Add(this.trkQuality);
            this.Controls.Add(this.lstLogs);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtDestination);
            this.Controls.Add(this.txtSource);
            this.MinimumSize = new System.Drawing.Size(850, 600);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simple Collage Maker";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trkQuality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.MaskedTextBox txtDestination;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ListBox lstLogs;
        private System.Windows.Forms.TrackBar trkQuality;
        private System.Windows.Forms.PictureBox pctPreview;
        private System.Windows.Forms.Button btnBrowseSrc;
        private System.Windows.Forms.Button btnBrowseDest;
    }
}

