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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblQuality = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnExploreSrc = new System.Windows.Forms.Button();
            this.btnExploreDest = new System.Windows.Forms.Button();
            this.cboLayout = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trkQuality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(12, 25);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(477, 20);
            this.txtSource.TabIndex = 1;
            this.txtSource.Text = "D:\\Users\\rj\\Desktop\\tmp";
            // 
            // txtDestination
            // 
            this.txtDestination.Location = new System.Drawing.Point(12, 74);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(477, 20);
            this.txtDestination.TabIndex = 4;
            this.txtDestination.Text = "E:\\TEST";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(579, 126);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 26);
            this.btnStart.TabIndex = 8;
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
            this.lstLogs.Location = new System.Drawing.Point(12, 190);
            this.lstLogs.Name = "lstLogs";
            this.lstLogs.Size = new System.Drawing.Size(810, 407);
            this.lstLogs.TabIndex = 9;
            // 
            // trkQuality
            // 
            this.trkQuality.Location = new System.Drawing.Point(12, 126);
            this.trkQuality.Maximum = 100;
            this.trkQuality.Minimum = 1;
            this.trkQuality.Name = "trkQuality";
            this.trkQuality.Size = new System.Drawing.Size(477, 45);
            this.trkQuality.TabIndex = 7;
            this.trkQuality.Value = 30;
            this.trkQuality.Scroll += new System.EventHandler(this.trkQuality_Scroll);
            // 
            // pctPreview
            // 
            this.pctPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pctPreview.Location = new System.Drawing.Point(660, 22);
            this.pctPreview.Name = "pctPreview";
            this.pctPreview.Size = new System.Drawing.Size(162, 130);
            this.pctPreview.TabIndex = 5;
            this.pctPreview.TabStop = false;
            // 
            // btnBrowseSrc
            // 
            this.btnBrowseSrc.Location = new System.Drawing.Point(579, 22);
            this.btnBrowseSrc.Name = "btnBrowseSrc";
            this.btnBrowseSrc.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseSrc.TabIndex = 3;
            this.btnBrowseSrc.Text = "Browse";
            this.btnBrowseSrc.UseVisualStyleBackColor = true;
            this.btnBrowseSrc.Click += new System.EventHandler(this.btnBrowseSrc_Click);
            // 
            // btnBrowseDest
            // 
            this.btnBrowseDest.Location = new System.Drawing.Point(579, 71);
            this.btnBrowseDest.Name = "btnBrowseDest";
            this.btnBrowseDest.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseDest.TabIndex = 6;
            this.btnBrowseDest.Text = "Browse";
            this.btnBrowseDest.UseVisualStyleBackColor = true;
            this.btnBrowseDest.Click += new System.EventHandler(this.btnBrowseDest_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Source Folder";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Destination Folder";
            // 
            // lblQuality
            // 
            this.lblQuality.AutoSize = true;
            this.lblQuality.Location = new System.Drawing.Point(12, 110);
            this.lblQuality.Name = "lblQuality";
            this.lblQuality.Size = new System.Drawing.Size(39, 13);
            this.lblQuality.TabIndex = 10;
            this.lblQuality.Text = "Quality";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Logs";
            // 
            // btnExploreSrc
            // 
            this.btnExploreSrc.Location = new System.Drawing.Point(498, 22);
            this.btnExploreSrc.Name = "btnExploreSrc";
            this.btnExploreSrc.Size = new System.Drawing.Size(75, 23);
            this.btnExploreSrc.TabIndex = 2;
            this.btnExploreSrc.Text = "Explore";
            this.btnExploreSrc.UseVisualStyleBackColor = true;
            this.btnExploreSrc.Click += new System.EventHandler(this.btnExploreSrc_Click);
            // 
            // btnExploreDest
            // 
            this.btnExploreDest.Location = new System.Drawing.Point(498, 71);
            this.btnExploreDest.Name = "btnExploreDest";
            this.btnExploreDest.Size = new System.Drawing.Size(75, 23);
            this.btnExploreDest.TabIndex = 5;
            this.btnExploreDest.Text = "Explore";
            this.btnExploreDest.UseVisualStyleBackColor = true;
            this.btnExploreDest.Click += new System.EventHandler(this.btnExploreDest_Click);
            // 
            // cboLayout
            // 
            this.cboLayout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLayout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboLayout.FormattingEnabled = true;
            this.cboLayout.Location = new System.Drawing.Point(498, 126);
            this.cboLayout.Name = "cboLayout";
            this.cboLayout.Size = new System.Drawing.Size(75, 21);
            this.cboLayout.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(498, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Layout";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 622);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cboLayout);
            this.Controls.Add(this.btnExploreDest);
            this.Controls.Add(this.btnExploreSrc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblQuality);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblQuality;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnExploreSrc;
        private System.Windows.Forms.Button btnExploreDest;
        private System.Windows.Forms.ComboBox cboLayout;
        private System.Windows.Forms.Label label3;
    }
}

