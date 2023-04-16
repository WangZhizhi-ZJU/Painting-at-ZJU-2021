using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Painting_at_ZJU
{
    public partial class CropForm : Form
    {
        private bool isCropping = false;
        private bool inArea = false;

        private int CroppingX = 0;
        private int CroppingY = 0;

        private int CropRectX = 0;
        private int CropRectY = 0;

        private int StartX = -1;
        private int StartY = -1;
        private int EndX = -1;
        private int EndY = -1;

        private float ActuallRatio = 0F;

        public Rectangle Rect;

        private Graphics g;

        public CropForm(int FatherWndX, int FatherWndY, Size mainWndSize, Image CurrentImage)
        {
            InitializeComponent();

            #region 主窗体本身开启双缓冲
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
            DoubleBuffer(this);
            #endregion

            this.Location = new Point(FatherWndX + mainWndSize.Width / 2 - this.Width / 2, FatherWndY + mainWndSize.Height / 2 - this.Height / 2);
            int ResolutionX = CurrentImage.Width;
            int ResolutionY = CurrentImage.Height;
            this.panel1.BackgroundImage = CurrentImage;
            float RatioX = (float)panel1.Width / ResolutionX;
            float RatioY = (float)panel1.Height / ResolutionY;
            ActuallRatio = Math.Min(RatioX, RatioY);
            CroppingX = (int)(ActuallRatio * ResolutionX);
            CroppingY = (int)(ActuallRatio * ResolutionY);
            Console.WriteLine(CroppingX);
            Console.WriteLine(CroppingY);
            CropRectX = panel1.Width / 2 - CroppingX / 2;
            CropRectY = panel1.Height / 2 - CroppingY / 2;
            Console.WriteLine(CropRectX);
            Console.WriteLine(CropRectY);
        }

        #region 为主窗体以及内部所有控件开启双缓冲
        private void DoubleBuffer(Control control)
        {
            Control.ControlCollection sonControls = control.Controls;
            foreach (Control con in sonControls)
            {
                Type dgvType = con.GetType();
                if (dgvType.Name.Equals("Button") || dgvType.Name.Equals("TextBox") || dgvType.Name.Equals("Label") || dgvType.Name.Equals("DateTimePicker") || dgvType.Name.Equals("Panel") || dgvType.Name.Equals("DataGridView") || dgvType.Name.Equals("SplitterPanel") || dgvType.Name.Equals("TabControl") || dgvType.Name.Equals("Label") || dgvType.Name.Equals("SplitContainer"))
                {
                    PropertyInfo pi = dgvType.GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                    pi.SetValue(con, true, null);
                    if (dgvType.Name.Equals("Panel") || dgvType.Name.Equals("SplitterPanel") || dgvType.Name.Equals("TabControl") || dgvType.Name.Equals("SplitContainer"))
                    {
                        DoubleBuffer(con);
                    }
                }
                else if (dgvType.Name.Equals("TabPage"))
                {
                    DoubleBuffer(con);
                }
            }
        }
        #endregion

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if(inArea == false)
            {
                return;
            }
            else
            {
                if(isCropping == false)
                {
                    isCropping = true;
                    label1.Text = "请在图像区域内按下鼠标左键选取裁剪区域的右下角。";
                    StartX = e.X;
                    StartY = e.Y;
                    EndX = -1;
                    EndY = -1;
                    Console.WriteLine(StartX);
                    Console.WriteLine(StartY);
                }
                else
                {
                    isCropping = false;
                    label1.Text = "请在图像区域内按下鼠标左键选取裁剪区域的左上角。";
                    EndX = e.X;
                    EndY = e.Y;
                    Console.WriteLine(EndX);
                    Console.WriteLine(EndY);
                }
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(CropRectX <= e.X && e.X < CropRectX + CroppingX && CropRectY <= e.Y && e.Y < CropRectY + CroppingY)
            {
                this.Cursor = Cursors.Cross;
                inArea = true;
                if(isCropping == true)
                {
                    Bitmap MapG = new Bitmap(panel1.Width, panel1.Height);
                    g = Graphics.FromImage(MapG);
                    g.DrawImage(panel1.BackgroundImage, CropRectX, CropRectY, CroppingX, CroppingY);
                    g.DrawRectangle(new Pen(Color.Black, 2), new Rectangle(new Point(StartX, StartY), new Size(e.X - StartX, e.Y - StartY)));
                    Graphics p = panel1.CreateGraphics();
                    p.DrawImage(MapG, new Point(0, 0));
                    MapG.Dispose();
                    g.Dispose();
                }
            }
            else
            {
                this.Cursor = Cursors.Default;
                inArea = false;
            }
        }

        private void CropForm_MouseMove(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Default;
            inArea = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Rect = new Rectangle(new Point((int)((StartX - CropRectX) / ActuallRatio), (int)((StartY - CropRectY) / ActuallRatio)), new Size((int)((EndX - StartX) / ActuallRatio), (int)((EndY - StartY) / ActuallRatio)));
            
            this.DialogResult = DialogResult.OK;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(StartX == -1 || EndX == -1 || StartX >= EndX || StartY >= EndY)
            {
                button1.Enabled = false;
            }           
            else        
            {           
                button1.Enabled = true;
            }
        }

        private void CropForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
