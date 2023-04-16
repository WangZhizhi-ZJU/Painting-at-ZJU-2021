using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Runtime.InteropServices;
using System.Diagnostics;


namespace Painting_at_ZJU
{
    public partial class StatisticsForm : Form
    {
        public StatisticsForm(int FatherWndX, int FatherWndY, Size mainWndSize, Image Picture)
        {
            InitializeComponent();
            this.Location = new Point(FatherWndX + mainWndSize.Width / 2 - this.Width / 2, FatherWndY + mainWndSize.Height / 2 - this.Height / 2);
            Thumb.Image = Picture;
            pictureBox1.Visible = false;
            Opacity = 0;
        }

        List<Statistics.MajorColor> MC;
        int PixelAmount = 0;

        private void CmdOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.FilterIndex = 4;
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Thumb.Image =  (Bitmap)Bitmap.FromFile(openFileDialog.FileName);
            }
        }
        public static Color IntToColor(int color)
        {
            int R = color & 255;
            int G = (color & 65280) / 256;
            int B = (color & 16711680) / 65536;
            return Color.FromArgb(255, R, G, B);
        }

        private void PicR_Paint(object sender, PaintEventArgs e)
        {
            if (MC != null)
            {
                e.Graphics.Clear(PicR.BackColor);
                Font font = new Font("微软雅黑", 9F);
                SolidBrush B = new SolidBrush(Color.Black);
                e.Graphics.DrawString("      颜色        ", font, B, new PointF(78, 0));
                e.Graphics.DrawString("     百分比      ", font, B, new PointF(230, 0));
                e.Graphics.DrawString("数量", font, B, new PointF(360, 0));
                B.Dispose();
                SolidBrush K = new SolidBrush(Color.Black);
                for (int i = 0; i < MC.Count; i++)
                {
                    B = new SolidBrush(IntToColor(MC[i].Color));
                    e.Graphics.FillRectangle(B, new Rectangle(50, (i + 1) * 20, 130, 15));
                    e.Graphics.DrawString(((double)MC[i].Amount / PixelAmount).ToString(), font, K, new PointF(200, (i + 1) * 20 + 3));
                    e.Graphics.DrawString(MC[i].Amount.ToString(), font, K, new PointF(350, (i + 1) * 20 + 3));
                    B.Dispose();
                }
                font.Dispose();
            }
        }

        private void CmdDeal_Click(object sender, EventArgs e)
        {
            if (Thumb.Image != null ) 
            {
                Stopwatch Sw = new Stopwatch();
                Sw.Start();
                MC = Statistics.PrincipalColorAnalysis((Bitmap)Thumb.Image, 20, 24);
                Sw.Stop();
                PixelAmount = Thumb.Image.Width * Thumb.Image.Height;
                PicR.Refresh();
            }
        }

        private void FrmTest_Load(object sender, EventArgs e)
        {
            CmdDeal_Click(sender,e);
            timer1.Start();
        }

        private void StatisticsForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            g.DrawRectangle(new Pen(Color.Black), Thumb.Location.X - 2, Thumb.Location.Y - 2, Thumb.Width + 2, Thumb.Height + 2);
            g.DrawRectangle(new Pen(Color.Black), PicR.Location.X - 2, PicR.Location.Y - 2, PicR.Width + 2, PicR.Height + 2);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1.BackgroundImage = Painting_at_ZJU.Properties.Resources.QuitButton02;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.BackgroundImage = Painting_at_ZJU.Properties.Resources.QuitButton01;
            timer2.Start();
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Opacity -= 0.1;
            if (Opacity <= 0)
            {
                timer2.Enabled = false;
                timer2 = null;
                this.DialogResult = DialogResult.OK;
            }
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

        private void StatisticsForm_MouseMove(object sender, MouseEventArgs e)
        {
            if(843 <= e.X && e.X <= 999 && 1 <= e.Y && e.Y <= 26)
            {
                pictureBox1.Visible = true;
            }
            else
            {
                pictureBox1.Visible = false;
            }
        }
    }
}
