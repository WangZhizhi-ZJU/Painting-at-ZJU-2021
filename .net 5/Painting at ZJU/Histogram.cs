using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Painting_at_ZJU
{
    public partial class Histogram : Form
    {
        private System.Drawing.Bitmap bmpHist;
        private int[] countPixel;
        private int maxPixel;

        public Histogram(int FatherWndX, int FatherWndY, Size mainWndSize, Image Picture)
        {
            InitializeComponent();
            Opacity = 0;
            this.Location = new Point(FatherWndX + mainWndSize.Width / 2 - this.Width / 2, FatherWndY + mainWndSize.Height / 2 - this.Height / 2);
            bmpHist = (Bitmap)Picture;
            countPixel = new int[256];
            pictureBox2.Visible = false;
        }

        private void HistorgramForm_Paint(object sender, PaintEventArgs e)
        {
            int heightdelta = 280;

            Graphics g = e.Graphics;
            Pen curPen = new Pen(Brushes.Black, 1);
            Font DefaultFont = this.Font;

            pictureBox1.Image = (Image)bmpHist;

            g.DrawRectangle(new Pen(Color.Black, 1), 25, heightdelta - 220, 305, 180);
            g.DrawRectangle(new Pen(Color.Black, 1), 25, heightdelta - 10, 305, 290);

            g.DrawLine(curPen, 100, 240 + heightdelta, 100, 242 + heightdelta);
            g.DrawLine(curPen, 150, 240 + heightdelta, 150, 242 + heightdelta);
            g.DrawLine(curPen, 200, 240 + heightdelta, 200, 242 + heightdelta);
            g.DrawLine(curPen, 250, 240 + heightdelta, 250, 242 + heightdelta);
            g.DrawLine(curPen, 300, 240 + heightdelta, 300, 242 + heightdelta);
            g.DrawString("0", DefaultFont, Brushes.Black, new PointF(46, 242 + heightdelta));
            g.DrawString("50", DefaultFont, Brushes.Black, new PointF(92, 242 + heightdelta));
            g.DrawString("100", DefaultFont, Brushes.Black, new PointF(139, 242 + heightdelta));
            g.DrawString("150", DefaultFont, Brushes.Black, new PointF(189, 242 + heightdelta));
            g.DrawString("200", DefaultFont, Brushes.Black, new PointF(239, 242 + heightdelta));
            g.DrawString("250", DefaultFont, Brushes.Black, new PointF(289, 242 + heightdelta));
            g.DrawLine(curPen, 48, 40 + heightdelta, 50, 40 + heightdelta);
            g.DrawString(maxPixel.ToString(), DefaultFont, Brushes.Black, new PointF(50, 34 + heightdelta));

            double temp = 0;
            for (int i = 0; i < 256; i++)
            {
                temp = 200.0 * countPixel[i] / maxPixel;
                g.DrawLine(new Pen(Color.FromArgb(i, 0, 0)), 50 + i, 240 + heightdelta, 50 + i, 240 + heightdelta - (int)temp);
            }

            g.DrawLine(curPen, 50, 240 + heightdelta, 320, 240 + heightdelta);
            g.DrawLine(curPen, 50, 240 + heightdelta, 50, 30 + heightdelta);
            curPen.Dispose();
        }

        private void HistorgramForm_Load(object sender, EventArgs e)
        {
            //将图像数据复制到byte中
            Rectangle rect = new Rectangle(0, 0, bmpHist.Width, bmpHist.Height);
            System.Drawing.Imaging.BitmapData bmpdata = bmpHist.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmpHist.PixelFormat);
            IntPtr ptr = bmpdata.Scan0;

            int bytes = bmpHist.Width * bmpHist.Height * 3;
            byte[] grayValues = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

            //统计直方图信息
            byte temp = 0;
            maxPixel = 0;
            Array.Clear(countPixel, 0, 256);
            for (int i = 0; i < bytes; i++)
            {
                temp = grayValues[i];
                countPixel[temp]++;
                if (countPixel[temp] > maxPixel)
                {
                    maxPixel = countPixel[temp];
                }
            }
            System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
            bmpHist.UnlockBits(bmpdata);
            timer1.Start();
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox2.BackgroundImage = Painting_at_ZJU.Properties.Resources.QuitButton02;
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox2.BackgroundImage = Painting_at_ZJU.Properties.Resources.QuitButton01;
            timer2.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Opacity += 0.08;
            if(Opacity >= 1)
            {
                timer1.Enabled = false;
                timer1 = null;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Opacity -= 0.2;
            if (Opacity <= 0)
            {
                timer2.Enabled = false;
                timer2 = null;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void Histogram_MouseMove(object sender, MouseEventArgs e)
        {
            if(308 <= e.X && e.X <= 353 && 2 <= e.Y && e.Y <= 27)
            {
                pictureBox2.Visible = true;
            }
            else
            {
                pictureBox2.Visible = false;
            }
        }
    }
}
