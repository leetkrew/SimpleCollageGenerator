using SimpleCollageGenerator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        int _qualityDivisor;

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            
            if ((btnStart.Text == "Start")|| (btnStart.Text == "&Start"))
            {
                _qualityDivisor = 10 - trkQuality.Value + 1;
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

                var pages = new List<PageInfo>();
                var page = new PageInfo();

                for (int i = 0; i <= fileList.Count - 1; i++)
                {

                    if (_bw.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    try
                    {
                        //_bw.ReportProgress(0, string.Format("Validating File: {0}", fileList[i]));
                        Image testImage;
                        testImage = Image.FromFile(fileList[i]);
                        try
                        {
                            pctPreview.SizeMode = PictureBoxSizeMode.StretchImage;
                            pctPreview.ImageLocation = fileList[i];
                        }
                        catch
                        {
                            _bw.ReportProgress(0, "Failed to load preview");
                        }

                        testImage.Dispose();
                    } catch
                    {
                        _bw.ReportProgress(0, string.Format("Skipping File: {0}", fileList[i]));
                        continue;
                    }

                    var isLast = i == fileList.Count - 1 ? true : false;

                    if (string.IsNullOrEmpty(page.Pic0))
                    {
                        page.Pic0 = fileList[i];
                        _bw.ReportProgress(0, string.Format("Adding {0}, page {1}, upper left", 
                            fileList[i], 
                            pages.Count() + 1
                            ));
                        if (!isLast) continue;
                    }

                    if (string.IsNullOrEmpty(page.Pic1))
                    {
                        page.Pic1 = fileList[i];
                        _bw.ReportProgress(0, string.Format("Adding {0}, page {1}, upper right",
                            fileList[i],
                            pages.Count() + 1
                            ));
                        if (!isLast) continue;
                    }

                    if (string.IsNullOrEmpty(page.Pic2))
                    {
                        page.Pic2 = fileList[i];
                        _bw.ReportProgress(0, string.Format("Adding {0}, page {1}, lower left",
                            fileList[i],
                            pages.Count() + 1
                            ));
                        if (!isLast) continue;
                    }

                    if (string.IsNullOrEmpty(page.Pic3))
                    {
                        page.Pic3 = fileList[i];
                        _bw.ReportProgress(0, string.Format("Adding {0}, page {1}, upper right",
                            fileList[i],
                            pages.Count() + 1
                            ));
                        pages.Add(page);
                        page = new PageInfo();
                        if (!isLast) continue;
                    }

                    if (isLast)
                    {
                        if (
                            (string.IsNullOrEmpty(page.Pic0)) &&
                            (string.IsNullOrEmpty(page.Pic1)) &&
                            (string.IsNullOrEmpty(page.Pic2)) &&
                            (string.IsNullOrEmpty(page.Pic3))
                            )
                        {
                            break;
                        }

                        pages.Add(page);
                        page = new PageInfo();
                    }
                    else
                    {
                        _bw.ReportProgress(0, string.Format("Queueing page #{0}", pages.Count() + 1 ));
                        page = new PageInfo();
                        page.Pic0 = fileList[i];
                        _bw.ReportProgress(0, string.Format("Adding: {0}", fileList[i]));
                        pages.Add(page);
                    }
                }

                var destinationFolder = string.Format("{0}\\output_{1}{2}{3}{4}"
                            , txtDestination.Text
                            , DateTime.Now.ToString("yyyyMMdd")
                            , DateTime.Now.Hour
                            , DateTime.Now.Minute
                            , DateTime.Now.Second
                            );

                if (!Directory.Exists(destinationFolder))
                {
                    Directory.CreateDirectory(destinationFolder);
                    _bw.ReportProgress(0, string.Format("{0} has been created", destinationFolder));
                }

                for (int i = 0; i <= pages.Count() - 1; i++)
                {
                    if (_bw.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    _bw.ReportProgress(0, string.Format("Processing page: {0}", i + 1));

                    var img0 = Image.FromFile(pages[i].Pic0);
                    var img1 = Image.FromFile(pages[i].Pic1);
                    var img2 = Image.FromFile(pages[i].Pic2);
                    var img3 = Image.FromFile(pages[i].Pic3);


                    //int qualityDivisor = 7; // the lower, the better
                    var canvasWidth = (img0.Width + img1.Width + img2.Width + img3.Width) / _qualityDivisor;
                    var canvasHeight = (img0.Height + img1.Height + img2.Height + img3.Height) / _qualityDivisor;

                    using (Bitmap bmp = new Bitmap(canvasWidth, canvasHeight))
                    {
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            canvasWidth = canvasWidth / 2;
                            canvasHeight = canvasHeight / 2;
                            
                            g.DrawImage(
                                img0, 
                                0, 
                                0, 
                                canvasWidth, 
                                canvasHeight);
                            
                            g.DrawImage(
                                img1, 
                                canvasWidth, 
                                0, 
                                canvasWidth, 
                                canvasHeight);
                            
                            g.DrawImage(
                                img2, 
                                0, 
                                canvasHeight, 
                                canvasWidth, 
                                canvasHeight);


                            g.DrawImage(
                                img3, 
                                canvasWidth, 
                                canvasHeight, 
                                canvasWidth, 
                                canvasHeight);
                        }

                        var fileName = string.Format("{0}\\page_{1}.png", destinationFolder, i + 1);
                        bmp.Save(fileName, ImageFormat.Png);

                        _bw.ReportProgress(0, new UserStateModel()
                        {
                            Message = string.Format("Saving: {0}", fileName),
                            PreviewFile = fileName
                        });

                    }

                    img0.Dispose();
                    img1.Dispose();
                    img2.Dispose();
                    img3.Dispose();
                }

            } catch (Exception ex)
            {
                _bw.ReportProgress (0, ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
    }
}
