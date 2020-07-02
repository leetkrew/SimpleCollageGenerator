using SimpleCollageGenerator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace SimpleCollageGenerator
{
    public partial class frmMain : Form
    {
        BackgroundWorker _bw = new BackgroundWorker();
        double _qualitySettings = 1;
        string _destinationFolder = string.Empty;

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            
            if ((btnStart.Text == "Start")|| (btnStart.Text == "&Start"))
            {
                _qualitySettings = Double.Parse(trkQuality.Value.ToString()) / 100;
                lstLogs.Items.Clear();
                triggerButtons(false);
                _bw.RunWorkerAsync();
            } else if ((btnStart.Text == "&Cancel") || (btnStart.Text == "Cancel"))
            {
                if (_bw.IsBusy)
                {
                    _bw.CancelAsync();
                }
            } else
            {
                MessageBox.Show(btnStart.Text);
            }
        }

        private void triggerButtons(bool enable = false)
        {
            if (enable)
            {
                txtDestination.Enabled = true;
                txtSource.Enabled = true;
                btnBrowseDest.Enabled = true;
                btnBrowseSrc.Enabled = true;
                trkQuality.Enabled = true;
                btnStart.Text = "&Start";
            } else
            {
                txtDestination.Enabled = false;
                txtSource.Enabled = false;
                btnBrowseDest.Enabled = false;
                btnBrowseSrc.Enabled = false;
                trkQuality.Enabled = false;
                btnStart.Text = "&Cancel";
            }
        }

        private void _bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _bw.Dispose();
            lstLogs.Items.Add("Done");
            lstLogs.SelectedIndex = lstLogs.Items.Count - 1;
            triggerButtons(true);
            Process.Start("explorer.exe", _destinationFolder);
        }

        private void _bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            if (e.UserState.GetType() == typeof(UserStateModel))
            {
                var tmp = new UserStateModel();
                tmp = (UserStateModel)e.UserState;

                lstLogs.Items.Add(tmp.Message);
                if (!string.IsNullOrEmpty(tmp.PreviewFile))
                {
                    pctPreview.SizeMode = PictureBoxSizeMode.StretchImage;
                    pctPreview.ImageLocation = tmp.PreviewFile;
                }
            }
            else
            {
                lstLogs.Items.Add(e.UserState);
            }

            lstLogs.SelectedIndex = lstLogs.Items.Count - 1;
        }

        private void _bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                var fileList = new List<string>();
                var files = Directory.GetFiles(txtSource.Text, "*.*", SearchOption.TopDirectoryOnly).ToList();
                files = files.CustomSort().ToList();


                foreach (string filename in files)
                {
                    if (_bw.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    if (Regex.IsMatch(filename, @".jpg|.png|.gif|.tiff$", RegexOptions.IgnoreCase))
                        fileList.Add(filename);
                }

                fileList = fileList.CustomSort().ToList();

                if (fileList.Count() < 1)
                {
                    throw new Exception("No Items Found");
                }

                var cellSpacing = 3;

                Generator.Generate2x2(ref fileList, ref e, ref _bw, ref pctPreview, ref _destinationFolder, ref txtDestination, ref _qualitySettings, ref cellSpacing);

            } catch (Exception ex)
            {
                _bw.ReportProgress (0, ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblQuality.Text = string.Format("Quality ({0}%)", trkQuality.Value);
            _bw.DoWork += _bw_DoWork;
            _bw.ProgressChanged += _bw_ProgressChanged;
            _bw.RunWorkerCompleted += _bw_RunWorkerCompleted;
            _bw.WorkerReportsProgress = true;
            _bw.WorkerSupportsCancellation = true;
        }

        private void btnBrowseSrc_Click(object sender, EventArgs e)
        {
            var folder = SelectFolder();
            if (!string.IsNullOrEmpty(folder))
            {
                txtSource.Text = folder;
            }
        }

        private string SelectFolder()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    return fbd.SelectedPath;
                }
            }
            return "";
        }

        private void btnBrowseDest_Click(object sender, EventArgs e)
        {
            var folder = SelectFolder();
            if (!string.IsNullOrEmpty(folder))
            {
                txtDestination.Text = folder;
            }
        }

        private void btnExploreSrc_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(txtSource.Text))
            {
                Process.Start("explorer.exe", txtSource.Text);
            } else
            {
                MessageBox.Show("Source Folder Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExploreDest_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(txtDestination.Text))
            {
                Process.Start("explorer.exe", txtDestination.Text);
            }
            else
            {
                MessageBox.Show("Source Folder Not Found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void trkQuality_Scroll(object sender, EventArgs e)
        {
            lblQuality.Text = string.Format("Quality ({0}%)", trkQuality.Value);
        }
    }
}
