using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleCollageGenerator.Models
{
    static class Generator
    {
        public static void Generate2x2(ref List<string> fileList, ref DoWorkEventArgs e, ref BackgroundWorker _bw, ref PictureBox pctPreview, ref string _destinationFolder, ref MaskedTextBox txtDestination, ref double _qualitySettings, ref int cellSpacing)
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
                    _bw.ReportProgress(0, string.Format("Queueing page #{0}", pages.Count() + 1));
                    page = new PageInfo();
                    page.Pic0 = fileList[i];
                    _bw.ReportProgress(0, string.Format("Adding: {0}", fileList[i]));
                    pages.Add(page);
                }
            }

            _destinationFolder = string.Format("{0}\\output_{1}{2}{3}{4}"
                        , txtDestination.Text
                        , DateTime.Now.ToString("yyyyMMdd")
                        , DateTime.Now.Hour
                        , DateTime.Now.Minute
                        , DateTime.Now.Second
                        );

            if (!Directory.Exists(_destinationFolder))
            {
                Directory.CreateDirectory(_destinationFolder);
                _bw.ReportProgress(0, string.Format("{0} has been created", _destinationFolder));
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


                var imgHeight0 = (int)Math.Ceiling(img0.Height * _qualitySettings);
                var imgHeight1 = (int)Math.Ceiling(img1.Height * _qualitySettings);
                var imgHeight2 = (int)Math.Ceiling(img2.Height * _qualitySettings);
                var imgHeight3 = (int)Math.Ceiling(img3.Height * _qualitySettings);


                var imgWidth0 = (int)Math.Ceiling(img0.Width * _qualitySettings);
                var imgWidth1 = (int)Math.Ceiling(img1.Width * _qualitySettings);
                var imgWidth2 = (int)Math.Ceiling(img2.Width * _qualitySettings);
                var imgWidth3 = (int)Math.Ceiling(img3.Width * _qualitySettings);

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
                            ); ;

                        g.DrawImage(
                            img3,
                            imgWidth2 + cellSpacing,
                            ((imgHeight0 > imgHeight1 ? imgHeight0 : imgHeight1)) + cellSpacing,
                            imgWidth3,
                            imgHeight3
                            );
                    }

                    var fileName = string.Format("{0}\\page_{1}.png", _destinationFolder, i + 1);
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
}
