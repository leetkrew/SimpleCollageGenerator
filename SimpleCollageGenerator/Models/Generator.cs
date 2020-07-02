using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SimpleCollageGenerator.Models
{
    static class Generator
    {
        public static void Generate(ref List<string> fileList, ref DoWorkEventArgs e, ref BackgroundWorker _bw, ref PictureBox pctPreview, ref string destinationFolder, ref MaskedTextBox txtDestination, double qualitySettings, int cellSpacing, string layout)
        {
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
                }
                catch
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

                    if (layout == "1x1")
                    {
                        pages.Add(page);
                        page = new PageInfo();
                        continue;
                    } else
                    {
                        if (!isLast) continue;
                    }
                }

                if (
                    (layout == "1x2") ||
                    (layout == "2x2") 
                    )
                {
                    if (string.IsNullOrEmpty(page.Pic1))
                    {
                        page.Pic1 = fileList[i];
                        _bw.ReportProgress(0, string.Format("Adding {0}, page {1}, upper right",
                            fileList[i],
                            pages.Count() + 1
                            ));

                        if (layout == "1x2")
                        {
                            pages.Add(page);
                            page = new PageInfo();
                        }

                        if (!isLast) continue;
                    }
                }

                
                if (layout == "2x2")
                {
                    if (string.IsNullOrEmpty(page.Pic2))
                    {
                        page.Pic2 = fileList[i];
                        _bw.ReportProgress(0, string.Format("Adding {0}, page {1}, lower left",
                            fileList[i],
                            pages.Count() + 1
                            ));
                        if (!isLast) continue;
                    }
                }

                if (layout == "2x2")
                {
                    if (string.IsNullOrEmpty(page.Pic3))
                    {
                        page.Pic3 = fileList[i];
                        _bw.ReportProgress(0, string.Format("Adding {0}, page {1}, lower right",
                            fileList[i],
                            pages.Count() + 1
                            ));
                        pages.Add(page);
                        page = new PageInfo();
                        if (!isLast) continue;
                    }
                }

                if (isLast)
                {

                    if (layout == "1x1")
                    {
                        pages.Add(page);
                        page = new PageInfo();
                    }

                    if (layout == "1x2")
                    {
                        if (
                            (string.IsNullOrEmpty(page.Pic0)) &&
                            (string.IsNullOrEmpty(page.Pic1)) 
                            )
                        {
                            break;
                        }

                        pages.Add(page);
                        page = new PageInfo();
                    }

                    if (layout == "2x2")
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
                }
                else
                {
                    _bw.ReportProgress(0, string.Format("Queueing page #{0}", pages.Count() + 1));
                    page = new PageInfo();
                    page.Pic0 = fileList[i];
                    _bw.ReportProgress(0, string.Format("Adding: {0}", fileList[i]));
                    pages.Add(page);
                }
            }

            destinationFolder = string.Format("{0}\\output_{1}{2}{3}{4}"
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

                if (layout == "1x1")
                {
                    Save1x1(pages, i, qualitySettings, destinationFolder, ref _bw);
                } else if (layout == "1x2")
                {
                    Save1x2(pages, i, qualitySettings, cellSpacing, destinationFolder, ref _bw);
                } else if (layout == "2x2")
                {
                    Save2x2(pages, i, qualitySettings, cellSpacing, destinationFolder, ref _bw);
                }
            }
        }

        private static void Save1x1(List<PageInfo> pages, int i, double qualitySettings, string destinationFolder, ref BackgroundWorker _bw)
        {
            var img0 = Image.FromFile(pages[i].Pic0);
            var imgHeight0 = (int)Math.Ceiling(img0.Height * qualitySettings);
            var imgWidth0 = (int)Math.Ceiling(img0.Width * qualitySettings);

            using (Bitmap bmp = new Bitmap(imgWidth0, imgHeight0))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.White);

                    g.DrawImage(
                        img0,
                        0,
                        0,
                        imgWidth0,
                        imgHeight0
                        );
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
        }

        private static void Save1x2(List<PageInfo> pages, int i, double qualitySettings, int cellSpacing, string destinationFolder, ref BackgroundWorker _bw)
        {
            var img0 = Image.FromFile(pages[i].Pic0);
            var img1 = Image.FromFile(pages[i].Pic1);
            


            var imgHeight0 = (int)Math.Ceiling(img0.Height * qualitySettings);
            var imgHeight1 = (int)Math.Ceiling(img1.Height * qualitySettings);


            var imgWidth0 = (int)Math.Ceiling(img0.Width * qualitySettings);
            var imgWidth1 = (int)Math.Ceiling(img1.Width * qualitySettings);
            

            var canvasWidth = imgWidth0 + imgWidth1 + cellSpacing;
            var canvasHeight = (imgHeight0 > imgHeight1) ? imgHeight0 : imgHeight1;

            using (Bitmap bmp = new Bitmap(canvasWidth, canvasHeight))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.White);

                    g.DrawImage(
                        img0,
                        0,
                        0,
                        imgWidth0,
                        imgHeight0
                        );

                    g.DrawImage(
                        img1,
                        imgWidth0 + cellSpacing,
                        0,
                        imgWidth1,
                        imgHeight1
                        );
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
        }
        
        private static void Save2x2(List<PageInfo> pages, int i,  double qualitySettings, int cellSpacing, string destinationFolder, ref BackgroundWorker _bw)
        {
            var img0 = Image.FromFile(pages[i].Pic0);
            var img1 = Image.FromFile(pages[i].Pic1);
            var img2 = Image.FromFile(pages[i].Pic2);
            var img3 = Image.FromFile(pages[i].Pic3);


            var imgHeight0 = (int)Math.Ceiling(img0.Height * qualitySettings);
            var imgHeight1 = (int)Math.Ceiling(img1.Height * qualitySettings);
            var imgHeight2 = (int)Math.Ceiling(img2.Height * qualitySettings);
            var imgHeight3 = (int)Math.Ceiling(img3.Height * qualitySettings);


            var imgWidth0 = (int)Math.Ceiling(img0.Width * qualitySettings);
            var imgWidth1 = (int)Math.Ceiling(img1.Width * qualitySettings);
            var imgWidth2 = (int)Math.Ceiling(img2.Width * qualitySettings);
            var imgWidth3 = (int)Math.Ceiling(img3.Width * qualitySettings);

            var rowWidth01 = imgWidth0 + imgWidth1;
            var rowWidth23 = imgWidth2 + imgWidth3;
            var colHeight02 = imgHeight0 + imgHeight2;
            var colHeight13 = imgHeight1 + imgHeight3;

            var canvasWidth = (rowWidth01 > rowWidth23 ? rowWidth01 : rowWidth23) + cellSpacing;
            var canvasHeight = (colHeight02 > colHeight13 ? colHeight02 : colHeight13) + cellSpacing;

            using (Bitmap bmp = new Bitmap(canvasWidth, canvasHeight))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.White);

                    g.DrawImage(
                        img0,
                        0,
                        0,
                        imgWidth0,
                        imgHeight0
                        );

                    g.DrawImage(
                        img1,
                        imgWidth0 + cellSpacing,
                        0,
                        imgWidth1,
                        imgHeight1
                        );

                    g.DrawImage(
                        img2,
                        0,
                        (imgHeight0 > imgHeight1 ? imgHeight0 : imgHeight1) + cellSpacing,
                        imgWidth2,
                        imgHeight2
                        );

                    g.DrawImage(
                        img3,
                        imgWidth2 + cellSpacing,
                        ((imgHeight0 > imgHeight1 ? imgHeight0 : imgHeight1)) + cellSpacing,
                        imgWidth3,
                        imgHeight3
                        );
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

    }
}
