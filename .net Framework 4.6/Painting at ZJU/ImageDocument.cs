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
    public partial class ImageDocument : Form
    {
        #region 属性TabPageManager：记录当前文档附着的TabControl
        private TabControl tabPageManager;
        public TabControl TabPageManager
        {
            get { return tabPageManager; }
            set { tabPageManager = value; }
        }
        #endregion

        #region 属性TabPageChild：记录当前文档归属的TabPage
        private TabPage tabPageChild;
        public TabPage TabPageChild
        {
            get { return tabPageChild; }
            set { tabPageChild = value; }
        }
        #endregion

        #region 属性ImgPath：记录当前文档的图像源路径
        private string imgPath;
        public string ImgPath
        {
            get { return imgPath; }
            set { imgPath = value; }
        }
        #endregion

        #region 字段Ratio、属性ScaleRatio：分别记录缩放比和缩放百分比
        public float Ratio;
        public int ScaleRatio
        {
            get { return (int)(100F * Ratio); }
        }
        #endregion

        #region 字段OriginalImage：图像的原图像
        public Image OriginalImage = null;
        #endregion

        #region 字段CurrentImage：图像文档当前图像
        public Image CurrentImage = null;
        #endregion

        #region PreviewImage：预览图像，由PaintBoard.Image直接实现
        #endregion

        #region 字段Brightness：图像亮度
        public int Brightness;
        #endregion

        #region 字段Contrast：图像对比度
        public int Contrast;
        #endregion

        // 撤回栈与恢复栈
        public Stack<Image> CtrlZ = new Stack<Image>();
        public Stack<Image> CtrlY = new Stack<Image>();

        public ImageDocument()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(245, 245, 245);
        }

        public ImageDocument(string Title, Size AreaSize, Image Img)
        {
            this.Text = Title;
            InitializeComponent();
            this.Size = AreaSize;
            this.BackColor = Color.FromArgb(245, 245, 245);
            Console.WriteLine(this.Size.Width);
            PaintBoard.Image = Img;
            PaintBoard.Size = new Size(Img.Width, Img.Height);
            if (PaintBoard.Size.Width < Size.Width && PaintBoard.Size.Height < Size.Height)
            {
                PaintBoard.Location = new Point(Size.Width / 2 - PaintBoard.Size.Width / 2, Size.Height / 2 - PaintBoard.Size.Height / 2);
                HorizontalScroll.Enabled = true;
                VerticalScroll.Enabled = true;
            }
            else if (PaintBoard.Size.Width < Size.Width)
            {
                PaintBoard.Location = new Point(Size.Width / 2 - PaintBoard.Size.Width / 2, PaintBoard.Location.Y);
                HorizontalScroll.Enabled = false;
                VerticalScroll.Enabled = true;
            }
            else if (PaintBoard.Size.Height < Size.Height)
            {
                PaintBoard.Location = new Point(PaintBoard.Location.X, Size.Height / 2 - PaintBoard.Size.Height / 2);
                HorizontalScroll.Enabled = true;
                VerticalScroll.Enabled = false;
            }
            else
            {
                PaintBoard.Location = new Point(0, 0);
                HorizontalScroll.Enabled = false;
                VerticalScroll.Enabled = false;
            }
            Text = Title;
            Name = Title;
            OriginalImage = CurrentImage = Img;
            Ratio = 1;
            Brightness = 100;
            Contrast = 100;

            CtrlZ.Push(CurrentImage);

            this.MouseWheel += new MouseEventHandler(ImageDocument_MouseWheel);
            PaintBoard.MouseWheel += new MouseEventHandler(PaintBoard_MouseWheel);
        }

        private void ImageDocument_Load(object sender, EventArgs e)
        {

        }

        #region 如果窗口被关闭且已经没有多余的窗体，则隐藏选项卡容器
        private void ImageDocument_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.tabPageChild.Dispose();
            if (!tabPageManager.HasChildren)
            {
                tabPageManager.Visible = false;
            }
        }
        #endregion

        #region Ctrl+鼠标滚轮缩放方法
        private void ImageDocZoomIn()
        {
            double ZoomRatio = Math.Sqrt((double)PaintBoard.Width * PaintBoard.Height / (CurrentImage.Width * CurrentImage.Height));
            if (ZoomRatio < 10)
            {
                PaintBoard.Size = new Size(PaintBoard.Size.Width + (int)(0.05 * CurrentImage.Width), PaintBoard.Size.Height + (int)(0.05 * CurrentImage.Height));
                if ((double)PaintBoard.Size.Width / (double)CurrentImage.Width >= 10.0)
                {
                    PaintBoard.Size = new Size(10 * CurrentImage.Width, 10 * CurrentImage.Height);
                }
                Ratio = (float)PaintBoard.Size.Width / (float)CurrentImage.Width;
            }
            else
            {

            }
            ResizeImageDoc();
        }

        private void ImageDocZoomout()
        {
            double ZoomRatio = Math.Sqrt((double)PaintBoard.Width * PaintBoard.Height / (CurrentImage.Width * CurrentImage.Height));
            if (0.1 < ZoomRatio)
            {
                PaintBoard.Size = new Size(PaintBoard.Size.Width - (int)(0.05 * CurrentImage.Width), PaintBoard.Size.Height - (int)(0.05 * CurrentImage.Height));
                if ((double)PaintBoard.Size.Width / (double)CurrentImage.Width <= 0.1)
                {
                    PaintBoard.Size = new Size((int)(0.1 * CurrentImage.Width), (int)(0.1 * CurrentImage.Height));
                }
                Ratio = (float)PaintBoard.Size.Width / (float)CurrentImage.Width;
            }
            else
            {

            }
            ResizeImageDoc();
        }

        private void ResizeImageDoc()
        {
            if (PaintBoard.Size.Width < Size.Width && PaintBoard.Size.Height < Size.Height)
            {
                PaintBoard.Location = new Point(Size.Width / 2 - PaintBoard.Size.Width / 2, Size.Height / 2 - PaintBoard.Size.Height / 2);
                HorizontalScroll.Enabled = true;
                VerticalScroll.Enabled = true;
            }
            else if (PaintBoard.Size.Width < Size.Width)
            {
                PaintBoard.Location = new Point(Size.Width / 2 - PaintBoard.Size.Width / 2, PaintBoard.Location.Y);
                HorizontalScroll.Enabled = false;
                VerticalScroll.Enabled = true;
            }
            else if (PaintBoard.Size.Height < Size.Height)
            {
                PaintBoard.Location = new Point(PaintBoard.Location.X, Size.Height / 2 - PaintBoard.Size.Height / 2);
                HorizontalScroll.Enabled = true;
                VerticalScroll.Enabled = false;
            }
            else
            {
                PaintBoard.Location = new Point(0, 0);
                HorizontalScroll.Enabled = false;
                VerticalScroll.Enabled = false;
            }
        }

        private void ImageDocument_MouseWheel(object sender, MouseEventArgs e)
        {
            if((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                this.VerticalScroll.Enabled = false;
                if(e.Delta > 0)
                {
                    ImageDocZoomIn();
                }
                else if(e.Delta < 0)
                {
                    ImageDocZoomout();
                }
                this.VerticalScroll.Enabled = true;
            }
        }

        private void PaintBoard_MouseWheel(object sender, MouseEventArgs e)
        {
            ImageDocument_MouseWheel(sender, e);
        }
        #endregion

        #region 构建显示与隐藏右键菜单栏的委托ShowContextMenuStrip与HideContextMenuStrip
        public delegate void ShowContextMenuStripHandle(object sender, MouseEventArgs e);
        public event ShowContextMenuStripHandle ShowContextMenuStrip;

        public delegate void HideContextMenuStripHandle(object sender, MouseEventArgs e);
        public event HideContextMenuStripHandle HideContextMenuStrip;
        #endregion

        // 如果在ImageDoc内发生右键按下，则触发委托事件显示右键菜单栏，左键则可以取消右键菜单栏
        private void PaintBoard_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ShowContextMenuStrip(sender, e);
            }
            else
            {
                HideContextMenuStrip(sender, e);
            }
        }

        private void ImageDocument_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ShowContextMenuStrip(sender, e);
            }
            else
            {
                HideContextMenuStrip(sender, e);
            }
        }
    }
}
