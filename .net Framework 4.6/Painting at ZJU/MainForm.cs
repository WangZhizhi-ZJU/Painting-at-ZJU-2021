// For .net Framework 4.6

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using AForge.Imaging;

using Microsoft.VisualBasic;
using System.Drawing.Imaging;
using Image = System.Drawing.Image;
using System.Runtime.InteropServices;
using AForge.Imaging.Filters;
using AForge;
using System.Collections;
using System.Drawing.Drawing2D;
using static Painting_at_ZJU.ImageDocument;

namespace Painting_at_ZJU
{

    public partial class MainForm : Form
    {
        #region 获取系统的控制台接口
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        static extern bool AllocConsole();
        #endregion

        #region 赋予无边框窗体可拖动改变大小的功能
        /*
        const int HTLEFT = 10;
        const int HTRIGHT = 11;
        const int HTTOP = 12;
        const int HTTOPLEFT = 13;
        const int HTTOPRIGHT = 14;
        const int HTBOTTOM = 15;
        const int HTBOTTOMLEFT = 0x10;
        const int HTBOTTOMRIGHT = 17;
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0084:
                    base.WndProc(ref m);
                    Point vPoint = new Point((int)m.LParam & 0xFFFF,
                        (int)m.LParam >> 16 & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (vPoint.X <= 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPLEFT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOMLEFT;
                        else m.Result = (IntPtr)HTLEFT;
                    else if (vPoint.X >= ClientSize.Width - 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)HTTOPRIGHT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)HTBOTTOMRIGHT;
                        else m.Result = (IntPtr)HTRIGHT;
                    else if (vPoint.Y <= 5)
                        m.Result = (IntPtr)HTTOP;
                    else if (vPoint.Y >= ClientSize.Height - 5)
                        m.Result = (IntPtr)HTBOTTOM;
                    break;
                case 0x0201://鼠标左键按下的消息 
                    m.Msg = 0x00A1;//更改消息为非客户区按下鼠标 
                    m.LParam = IntPtr.Zero;//默认值 
                    m.WParam = new IntPtr(2);//鼠标放在标题栏内 
                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        */
        #endregion

        #region 赋予无边框窗体可以被拖拽移动的功能
        //窗体是否移动
        bool formMove = false;

        //记录窗体的位置 
        Point formPoint;
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            formPoint = new Point();
            int xOffset;
            int yOffset;
            if (e.Button == MouseButtons.Left)
            {
                xOffset = -e.X;
                yOffset = -e.Y;
                formPoint = new Point(xOffset, yOffset);
                formMove = true;
                // this.WindowState = FormWindowState.Normal;
                this.Location = new Point(this.Location.X, this.Location.Y);
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (formMove == true)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(formPoint.X, formPoint.Y);
                Location = mousePos;
                if (this.WindowState == FormWindowState.Maximized)
                {
                    this.WindowState = FormWindowState.Normal;
                }
            }
            else
            {
                if (this.Width - 143 <= e.X && e.X <= this.Width - 98 && 0 <= e.Y && e.Y <= 25)
                {
                    pictureBox2.Visible = true;
                }
                else
                {
                    pictureBox2.Visible = false;
                }

                if (this.Width - 94 <= e.X && e.X <= this.Width - 49 && 0 <= e.Y && e.Y <= 25)
                {
                    pictureBox3.Visible = true;
                }
                else
                {
                    pictureBox3.Visible = false;
                }

                if (this.Width - 45 <= e.X && e.X <= this.Width && 0 <= e.Y && e.Y <= 25)
                {
                    pictureBox1.Visible = true;
                }
                else
                {
                    pictureBox1.Visible = false;
                }
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                formMove = false;
                if (this.Location.Y <= 0)
                {
                    this.WindowState = FormWindowState.Maximized;
                }
                this.Refresh();
            }
        }

        private void panel1_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
            this.Refresh();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            formPoint = new Point();
            int xOffset;
            int yOffset;
            if (e.Button == MouseButtons.Left)
            {
                xOffset = -e.X;
                yOffset = -e.Y;
                formPoint = new Point(xOffset, yOffset - panel1.Height);
                formMove = true;
                // this.WindowState = FormWindowState.Normal;
                this.Location = new Point(this.Location.X, this.Location.Y);
            }
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (formMove == true)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(formPoint.X, formPoint.Y);
                Location = mousePos;
                if (this.WindowState == FormWindowState.Maximized)
                {
                    this.WindowState = FormWindowState.Normal;
                }
            }
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            panel1_MouseUp(sender, e);
        }

        private void panel2_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
            this.Refresh();
        }
        #endregion

        public MainForm()
        {
            // AllocConsole();
            // 将窗体透明度调为0，用于渐现
            this.Opacity = 0;
            this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
            InitializeComponent();

            button1.BackColor = Color.FromArgb(224, 227, 230);
            button1.ForeColor = Color.FromArgb(45, 45, 45);

            setStatusLabel(false);

            TabPageManager.Visible = false;
            toolStripProgressBar1.Enabled = false;
            panel3.BackColor = Color.FromArgb(238, 238, 242);
            label1.Location = new Point(panel3.Width / 2 - label1.Width / 2, panel3.Height / 2 - label1.Height / 2);

            #region 初始状态下隐藏窗体右上角的三个按钮动效
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            #endregion

            #region 将所有控件的颜色初始化为统一色彩
            panel1.BackColor = Color.FromArgb(238, 238, 242);
            panel2.BackColor = Color.FromArgb(238, 238, 242);
            toolStripStatusLabel1.BackColor = Color.FromArgb(238, 238, 242);
            toolStripStatusLabel2.BackColor = Color.FromArgb(238, 238, 242);
            toolStripStatusLabel3.BackColor = Color.FromArgb(238, 238, 242);
            toolStripStatusLabel4.BackColor = Color.FromArgb(238, 238, 242);
            toolStripStatusLabel5.BackColor = Color.FromArgb(238, 238, 242);
            toolStripStatusLabel6.BackColor = Color.FromArgb(238, 238, 242);
            toolStripStatusLabel7.BackColor = Color.FromArgb(238, 238, 242);
            toolStripStatusLabel8.BackColor = Color.FromArgb(238, 238, 242);
            toolStripStatusLabel9.BackColor = Color.FromArgb(238, 238, 242);
            toolStripStatusLabel10.BackColor = Color.FromArgb(238, 238, 242);
            statusStrip1.BackColor = Color.FromArgb(238, 238, 242);
            toolStrip1.BackColor = Color.FromArgb(238, 238, 242);
            #endregion

            TabPageManager.DrawMode = TabDrawMode.OwnerDrawFixed;
            TabPageManager.Padding = new System.Drawing.Point(15, 5);

            工具栏ToolStripMenuItem.Checked = true;
            控件栏ToolStripMenuItem.Checked = true;

            MouseWheel += new MouseEventHandler(MainForm_MouseWheel);
        }

        public void MainForm_MouseWheel(object sender, MouseEventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // 开始渐现
            FadeIn.Start();
            StatusStripRunning.Start();
        }

        #region 渐现计时器
        private void FadeIn_Tick(object sender, EventArgs e)
        {
            this.Opacity += 0.05;
            if (this.Opacity >= 1)
            {
                FadeIn.Stop();
                FadeIn.Dispose();
            }
        }
        #endregion

        #region 渐隐计时器
        private void FadeOut_Tick(object sender, EventArgs e)
        {
            this.Opacity -= 0.05;
            if (this.Opacity <= 0)
            {
                FadeOut.Enabled = false;
                FadeOut = null;
                foreach (ImageDocument childForm in this.MdiChildren)
                {
                    if (childForm.TabPageChild.Equals(TabPageManager.SelectedTab))
                    {
                        childForm.Select();
                    }
                }
                this.Dispose();
                System.Environment.Exit(0);
            }
        }
        #endregion

        #region 建立下拉菜单项与选项卡中子窗体的映射哈希表：ItemtoWnd(ToolStripMenuItem, TabPage)
        private Hashtable ItemtoWnd = new Hashtable();
        private Hashtable WndtoItem = new Hashtable();

        private Hashtable ItemtoWnd_Button = new Hashtable();
        private Hashtable WndtoItem_Button = new Hashtable();
        #endregion

        #region 将下拉菜单项与选项卡中子窗体联系起来
        /*  操作范式：
            // 将新的选项卡和新的标签建立映射
            hashtable.Add(newWnd, tp);     
            // 为标签赋予点击事件，点击后切换到相应选项卡
            newWnd.Click += new System.EventHandler(newWndClick);   
            // 将标签添加到窗体
            窗体ToolStripMenuItem.DropDownItems.Add(newWnd);
        */

        private void newWndClick(object sender, EventArgs e)
        {
            TabPage tp = (TabPage)ItemtoWnd[(ToolStripMenuItem)sender];
            TabPageManager.SelectedTab = tp;
        }

        private void newWndClick_Button(object sender, EventArgs e)
        {
            TabPage tp = (TabPage)ItemtoWnd_Button[(ToolStripMenuItem)sender];
            TabPageManager.SelectedTab = tp;
        }
        #endregion

        #region 打开图像并新建文档
        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabPageManager.Visible == false)
            {
                TabPageManager.Visible = true;
                panel3.Visible = false;
                label1.Visible = false;
            }
            OpenFile();
        }

        private void OpenFile()
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.Filter = "图像文件|*.bmp;*.jpg;*.png";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string Path = openFileDialog.FileName.ToString();
                string[] PathSplit = Path.Split('\\');
                string ImageDocName = PathSplit[PathSplit.Length - 1];
                TabPage tp = new TabPage();
                tp.Parent = TabPageManager;
                tp.Text = ImageDocName;
                tp.Show();
                TabPageManager.SelectedTab = tp;
                TabPageManager.Visible = true;
                System.Drawing.Image Img = System.Drawing.Image.FromFile(Path);
                ImageDocument ImageDoc = new ImageDocument(ImageDocName, tp.Size, Img);
                ImageDoc.TopLevel = false;
                ImageDoc.Dock = DockStyle.Fill;
                ImageDoc.Parent = tp;
                ImageDoc.TabPageChild = tp;
                ImageDoc.Show();
                ImageDoc.TabPageManager = this.TabPageManager;
                ImageDoc.ImgPath = Path;

                ToolStripMenuItem newWnd = new ToolStripMenuItem();
                newWnd.Text = ImageDoc.Text;
                ItemtoWnd.Add(newWnd, tp);
                WndtoItem.Add(tp, newWnd);
                newWnd.Click += new System.EventHandler(newWndClick);
                窗体ToolStripMenuItem.DropDownItems.Add(newWnd);

                ToolStripMenuItem newWnd_Button = new ToolStripMenuItem();
                newWnd_Button.Text = ImageDoc.Text;
                ItemtoWnd_Button.Add(newWnd_Button, tp);
                WndtoItem_Button.Add(tp, newWnd_Button);
                newWnd_Button.Click += new System.EventHandler(newWndClick_Button);
                toolStripSplitButton1.DropDownItems.Add(newWnd_Button);

                ImageDoc.ShowContextMenuStrip += new ShowContextMenuStripHandle(ShowContextDialog);
                ImageDoc.HideContextMenuStrip += new HideContextMenuStripHandle(HideContextDialog);
            }
        }
        #endregion

        #region 重新加载图像并更新文档
        private void 重新加载ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocument doc = GetCurrentImageDoc();
            Interaction.Beep();
            if (MessageBox.Show("您确定要重新加载文档" + "\"" + TabPageManager.SelectedTab.Name + "\"吗？", "重新加载", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
            {
                return;
            }
            try
            {
                doc.CurrentImage = doc.OriginalImage = Bitmap.FromFile(doc.ImgPath);
                doc.PaintBoard.Size = new Size(doc.CurrentImage.Width, doc.CurrentImage.Height);
                if (doc.PaintBoard.Size.Width < doc.Size.Width && doc.PaintBoard.Size.Height < doc.Size.Height)
                {
                    doc.PaintBoard.Location = new Point(doc.Size.Width / 2 - doc.PaintBoard.Size.Width / 2, doc.Size.Height / 2 - doc.PaintBoard.Size.Height / 2);
                }
                else if (doc.PaintBoard.Size.Width < doc.Size.Width)
                {
                    doc.PaintBoard.Location = new Point(Size.Width / 2 - doc.PaintBoard.Size.Width / 2, doc.PaintBoard.Location.Y);
                }
                else if (doc.PaintBoard.Size.Height < doc.Size.Height)
                {
                    doc.PaintBoard.Location = new Point(doc.PaintBoard.Location.X, doc.Size.Height / 2 - doc.PaintBoard.Size.Height / 2);
                }
                else
                {
                    doc.PaintBoard.Location = new Point(0, 0);
                }
                doc.PaintBoard.Image = doc.CurrentImage;
                doc.Ratio = 1;
                doc.Brightness = 100;
                doc.Contrast = 100;
                doc.CtrlZ.Clear();
                doc.CtrlY.Clear();
                doc.CtrlZ.Push(doc.CurrentImage);
            }
            catch (Exception)
            {
                MessageBox.Show("加载失败，图像可能已经丢失或者损坏！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 保存当前文档所编辑的图像
        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabPageManager.TabCount <= 0)
            {
                return;
            }
            SaveFile();
        }

        private void SaveFile()
        {
            saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog.Filter = "位图图像(.bmp)|*.bmp|联合图像专家组(.jpg)|*.jpg|便携式网络图形(.png)|*.png";
            saveFileDialog.RestoreDirectory = true;
            if (MessageBox.Show("您确定要保存对当前图像的编辑吗？", "保存", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Cancel)
            {
                return;
            }
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 获取当前的
                string Path = saveFileDialog.FileName.ToString();
                string[] PathSplit = Path.Split('\\');
                int selection = saveFileDialog.FilterIndex;
                ImageFormat Format = null;
                switch (selection)
                {
                    case 1: Format = ImageFormat.Bmp; break;
                    case 2: Format = ImageFormat.Jpeg; break;
                    case 3: Format = ImageFormat.Png; break;
                }
                ImageDocument doc = null;
                foreach (Control ctrl in TabPageManager.SelectedTab.Controls)
                {
                    if (ctrl.GetType() == typeof(ImageDocument))
                    {
                        doc = (ImageDocument)ctrl;
                        break;
                    }
                }
                doc.CurrentImage.Save(Path, Format);
            }
        }
        #endregion

        #region 关闭文档和关闭全部文档
        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabPageManager.TabCount <= 0)
            {
                return;
            }
            if (MessageBox.Show("您确定要关闭当前页面吗？请确保您的修改已经保存", "关闭文档", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                foreach (Control ctrl in TabPageManager.SelectedTab.Controls)
                {
                    if (ctrl.GetType() == typeof(ImageDocument))
                    {
                        ctrl.Dispose();
                        break;
                    }
                }
                TabPageManager.SelectedTab.Visible = false;
                RemoveEmptyWndLabel(TabPageManager.SelectedTab);
                TabPageManager.TabPages.Remove(TabPageManager.SelectedTab);
            }
        }

        private void 关闭所有ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabPageManager.TabCount <= 0)
            {
                return;
            }
            if (MessageBox.Show("您确定要关闭所有页面吗？请确保您的全部修改已经保存", "关闭全部文档", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                foreach (TabPage tabPage in TabPageManager.TabPages)
                {
                    foreach (Control ctrl in tabPage.Controls)
                    {
                        if (ctrl.GetType() == typeof(ImageDocument))
                        {
                            ctrl.Dispose();
                            break;
                        }
                    }
                    tabPage.Visible = false;
                    RemoveEmptyWndLabel(tabPage);
                    TabPageManager.TabPages.Remove(tabPage);
                }
            }
        }
        #endregion

        #region 选项卡管理器的关闭图标绘制、选项卡切换效果
        private void TabPageManager_MouseDown(object sender, MouseEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.TabPageManager.SelectedTab.Text))
            {
                if (e.Button == MouseButtons.Left)
                {
                    int x = e.X, y = e.Y;

                    //计算关闭区域     
                    Rectangle myTabRect = this.TabPageManager.GetTabRect(this.TabPageManager.SelectedIndex); ;

                    //myTabRect.Offset(myTabRect.Width - (CLOSE_SIZE + 3), 2);
                    myTabRect.Offset(myTabRect.Width - 0x12, 2);
                    myTabRect.Width = 15;
                    myTabRect.Height = 15;

                    //如果鼠标在区域内就关闭选项卡     
                    bool isClose = x > myTabRect.X && x < myTabRect.Right && y > myTabRect.Y && y < myTabRect.Bottom;
                    if (isClose == true)
                    {
                        RemoveEmptyWndLabel(this.TabPageManager.SelectedTab);
                        this.TabPageManager.TabPages.Remove(this.TabPageManager.SelectedTab);
                    }
                }
            }
        }

        private void TabPageManager_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                //获取当前Tab选项卡的绘图区域
                Rectangle myTabRect = this.TabPageManager.GetTabRect(e.Index);
                Color BkgColor;
                Color ClsBtColor;
                if (e.Index == this.TabPageManager.SelectedIndex)
                {
                    BkgColor = Color.White;
                    ClsBtColor = Color.Black;
                    this.TabPageManager.SelectedTab.ForeColor = Color.White;
                }
                else
                {
                    BkgColor = Color.FromArgb(238, 238, 240);
                    ClsBtColor = Color.DarkGray;
                }

                e.Graphics.FillRectangle(new SolidBrush(BkgColor), myTabRect); //设置当前选中的tabgage的背景色


                //先添加TabPage属性
                e.Graphics.DrawString(this.TabPageManager.TabPages[e.Index].Text,
                    this.Font, SystemBrushes.ControlText, myTabRect.X + 2, myTabRect.Y + 2);


                //再画一个矩形框
                using (Pen p = new Pen(Color.Transparent))
                {
                    myTabRect.Offset(myTabRect.Width - (15 + 3), 2);
                    myTabRect.Width = 15;
                    myTabRect.Height = 15;
                    e.Graphics.DrawRectangle(p, myTabRect);
                }
                //填充矩形框
                Color recColor = (e.State == DrawItemState.Selected) ? Color.Transparent : Color.Transparent;
                using (Brush b = new SolidBrush(recColor))
                {
                    e.Graphics.FillRectangle(b, myTabRect);
                }

                //画Tab选项卡右上方关闭按钮  
                using (Pen objpen = new Pen(ClsBtColor, 1.8f))
                {
                    //自己画X
                    //"\"线
                    Point p1 = new Point(myTabRect.X + 2, myTabRect.Y + 2);
                    Point p2 = new Point(myTabRect.X + myTabRect.Width - 2, myTabRect.Y + myTabRect.Height - 2);
                    e.Graphics.DrawLine(objpen, p1, p2);
                    //"/"线
                    Point p3 = new Point(myTabRect.X + 2, myTabRect.Y + myTabRect.Height - 2);
                    Point p4 = new Point(myTabRect.X + myTabRect.Width - 2, myTabRect.Y + 2);
                    e.Graphics.DrawLine(objpen, p3, p4);
                }
                e.Graphics.Dispose();
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region 获取当前选项卡内的ImageDocument窗体：GetCurrentImageDoc()
        private ImageDocument GetCurrentImageDoc()
        {
            ImageDocument doc = null;
            if (TabPageManager.Visible == false)
            {
                return null;
            }
            foreach (Control ctrl in TabPageManager.SelectedTab.Controls)
            {
                if (ctrl.GetType() == typeof(ImageDocument))
                {
                    doc = (ImageDocument)ctrl;
                    break;
                }
            }
            return doc;
        }
        #endregion

        #region 退出程序菜单栏按钮以及窗体关闭时间
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("您确定要退出求是画板吗？请确保您的全部修改已经保存", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                FadeOut.Start();
            }
            else
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region 调整位图尺寸：ResizeBitmap(Image Map, double Ratio)
        private Bitmap ResizeBitmap(Image bit, double Ratio)
        {
            Bitmap destBitmap = new Bitmap(Convert.ToInt32(bit.Width * Ratio), Convert.ToInt32(bit.Height * Ratio));
            Graphics g = Graphics.FromImage(destBitmap);
            g.Clear(Color.Transparent);
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(bit, new Rectangle(0, 0, destBitmap.Width, destBitmap.Height), 0, 0, bit.Width, bit.Height, GraphicsUnit.Pixel);
            g.Dispose();
            return destBitmap;
        }
        #endregion

        #region 更新ImageDocument的状态以及重新设置图像的位置：UpdateImageDoc(ImageDocument o), ResizeImageDoc(ImageDocument o)
        private void UpdateImageDoc(ImageDocument o)
        {
            //o.PaintBoard.Image = null;
            Bitmap Map = new Bitmap(o.CurrentImage.Width, o.CurrentImage.Height);
            Map = (Bitmap)o.CurrentImage.Clone();
            Map = (Bitmap)BrightnessP(Map, o.Brightness).Clone();
            Map = (Bitmap)KiContrast(Map, o.Contrast - 100).Clone();
            ResizeImageDoc(o);
            Console.WriteLine(o.PaintBoard.Size.Width);
            Console.WriteLine(o.PaintBoard.Size.Height);
            o.PaintBoard.Image = Map;
            o.Refresh();
            TabPageManager.Refresh();
        }

        private void ResizeImageDoc(ImageDocument o)
        {
            if (o.PaintBoard.Size.Width < o.Size.Width && o.PaintBoard.Size.Height < o.Size.Height)
            {
                o.PaintBoard.Location = new Point(o.Size.Width / 2 - o.PaintBoard.Size.Width / 2, o.Size.Height / 2 - o.PaintBoard.Size.Height / 2);
                o.HorizontalScroll.Enabled = true;
                o.VerticalScroll.Enabled = true;
            }
            else if (o.PaintBoard.Size.Width < o.Size.Width)
            {
                o.PaintBoard.Location = new Point(o.Size.Width / 2 - o.PaintBoard.Size.Width / 2, o.PaintBoard.Location.Y);
                o.HorizontalScroll.Enabled = false;
                o.VerticalScroll.Enabled = true;
            }
            else if (o.PaintBoard.Size.Height < o.Size.Height)
            {
                o.PaintBoard.Location = new Point(o.PaintBoard.Location.X, o.Size.Height / 2 - o.PaintBoard.Size.Height / 2);
                o.HorizontalScroll.Enabled = true;
                o.VerticalScroll.Enabled = false;
            }
            else
            {
                o.PaintBoard.Location = new Point(0, 0);
                o.HorizontalScroll.Enabled = false;
                o.VerticalScroll.Enabled = false;
            }
        }
        #endregion

        #region 标准图像旋转范式：ImageDocRotation(RotateFlipType 旋转类型, bool 是否转过90°的奇数倍)
        private void ImageDocRotation(RotateFlipType type, bool isRectangle)
        {
            ImageDocument doc = GetCurrentImageDoc();
            Graphics g = Graphics.FromImage((Image)doc.CurrentImage.Clone());
            Rectangle rect;
            if (isRectangle == false)
            {
                rect = new Rectangle(0, 0, doc.CurrentImage.Width, doc.CurrentImage.Height);
            }
            else
            {
                rect = new Rectangle(0, 0, doc.CurrentImage.Height, doc.CurrentImage.Width);
            }
            Image newG = (Image)doc.CurrentImage.Clone();
            newG.RotateFlip(type);
            g.DrawImage(newG, rect);
            doc.CurrentImage = (Image)newG.Clone();
            newG.Dispose();
            g.Dispose();
            UpdateImageDoc(doc);
            PushChanges(doc);
        }
        #endregion

        #region 垂直翻转、水平翻转、旋转90°、旋转180°、旋转270°
        private void 垂直翻转ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocRotation(RotateFlipType.RotateNoneFlipY, false);
        }

        private void 水平翻转ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocRotation(RotateFlipType.RotateNoneFlipX, false);
        }

        private void 顺时针旋转90ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocRotation(RotateFlipType.Rotate90FlipNone, true);
        }

        private void 逆时针旋转90ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocRotation(RotateFlipType.Rotate270FlipNone, true);
        }

        private void 旋转180ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocRotation(RotateFlipType.Rotate180FlipNone, false);
        }
        #endregion

        #region 将图像缩放至一定比例：ZoomTo(float Ratio)
        private void ZoomTo(float Ratio)
        {
            ImageDocument o = GetCurrentImageDoc();
            o.PaintBoard.Size = new Size((int)(o.CurrentImage.Size.Width * Ratio), (int)(o.CurrentImage.Height * Ratio));
            o.Ratio = Ratio;
            ResizeImageDoc(o);
        }
        #endregion

        #region ImageDocument图像预览缩放功能
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ZoomTo(0.1F);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ZoomTo(0.2F);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            ZoomTo(0.3F);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            ZoomTo(0.4F);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            ZoomTo(0.5F);
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            ZoomTo(0.6F);
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            ZoomTo(0.7F);
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            ZoomTo(0.8F);
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            ZoomTo(0.9F);
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            ZoomTo(1.0F);
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            ZoomTo(2.0F);
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            ZoomTo(5.0F);
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            ZoomTo(10.0F);
        }
        #endregion

        #region 当有文档、无文档状态切换时设置全部相关控件可见状态：setStatusLabel(bool status)
        private void setStatusLabel(bool status)
        {
            重新加载ToolStripMenuItem.Enabled = status;
            保存ToolStripMenuItem.Enabled = status;
            复制ToolStripMenuItem.Enabled = status;
            粘贴ToolStripMenuItem.Enabled = status;
            关闭ToolStripMenuItem.Enabled = status;
            关闭所有ToolStripMenuItem.Enabled = status;
            页面大小ToolStripMenuItem.Enabled = status;
            打印ToolStripMenuItem.Enabled = status;
            打印预览ToolStripMenuItem.Enabled = status;
            将当前更改保存到新图像文档ToolStripMenuItem.Enabled = status;
            保存图像文档ToolStripMenuItem.Enabled = status;
            快速导出为PNGToolStripMenuItem.Enabled = status;

            foreach (var toolStripMenuItem in 图像ToolStripMenuItem.DropDownItems)
            {
                try
                {
                    ((ToolStripMenuItem)toolStripMenuItem).Enabled = status;
                }
                catch (Exception)
                {

                }
            }

            foreach (var toolStripMenuItem in 滤镜ToolStripMenuItem.DropDownItems)
            {
                try
                {
                    ((ToolStripMenuItem)toolStripMenuItem).Enabled = status;
                }
                catch (Exception)
                {

                }
            }

            foreach (var toolStripMenuItem in 窗体ToolStripMenuItem.DropDownItems)
            {
                try
                {
                    ((ToolStripMenuItem)toolStripMenuItem).Enabled = status;
                }
                catch (Exception)
                {
                    break;
                }
            }
            foreach (var toolStripMenuItem in 选项ToolStripMenuItem.DropDownItems)
            {
                try
                {
                    ((ToolStripMenuItem)toolStripMenuItem).Enabled = status;
                }
                catch (Exception)
                {

                }
            }
            foreach (var toolStripMenuItem in 视图ToolStripMenuItem.DropDownItems)
            {
                try
                {
                    if(toolStripMenuItem == 工具栏ToolStripMenuItem || toolStripMenuItem == 控件栏ToolStripMenuItem)
                    {
                        continue;
                    }
                    ((ToolStripMenuItem)toolStripMenuItem).Enabled = status;
                }
                catch (Exception)
                {

                }
            }

            toolStripStatusLabel1.Visible = status;
            toolStripStatusLabel2.Visible = status;
            toolStripStatusLabel3.Visible = status;
            toolStripStatusLabel4.Visible = status;
            toolStripStatusLabel5.Visible = status;
            toolStripStatusLabel6.Visible = status;
            toolStripStatusLabel7.Visible = status;
            toolStripStatusLabel8.Visible = status;
            toolStripStatusLabel9.Visible = status;

            toolStripButton2.Enabled = status;
            toolStripButton3.Enabled = status;
            toolStripButton4.Enabled = status;
            toolStripButton5.Enabled = status;
            toolStripButton6.Enabled = status;
            toolStripButton7.Enabled = status;
            toolStripButton8.Enabled = status;
            toolStripButton9.Enabled = status;
            toolStripButton10.Enabled = status;
            toolStripButton11.Enabled = status;
            toolStripButton12.Enabled = status;
            toolStripButton13.Enabled = status;
            toolStripButton14.Enabled = status;
            toolStripButton15.Enabled = status;
            toolStripButton16.Enabled = status;
            toolStripButton17.Enabled = status;
            toolStripButton18.Enabled = status;
            toolStripButton19.Enabled = status;
            toolStripButton20.Enabled = status;
            toolStripButton22.Enabled = status;
            toolStripButton23.Enabled = status;
            toolStripButton25.Enabled = status;

            button1.Enabled = status;
            if (button1.Enabled == false)
            {
                button1.ForeColor = Color.Gainsboro;
            }
            else
            {
                button1.ForeColor = Color.FromArgb(45, 45, 45);
            }
        }
        #endregion

        #region 亮度与亮度算法
        private void 亮度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocument doc = GetCurrentImageDoc();
            BrightnessForm brightnessForm = new BrightnessForm(this.Location.X, this.Location.Y, this.Size, (int)doc.Brightness);
            brightnessForm.ShowDialog();
            if (brightnessForm.DialogResult == DialogResult.OK)
            {
                doc.Brightness = brightnessForm.Value;
                brightnessForm.Dispose();
            }
            UpdateImageDoc(doc);
        }

        #region 亮度算法
        private static Bitmap BrightnessP(Bitmap a, int v)
        {
            System.Drawing.Imaging.BitmapData bmpData = a.LockBits(new Rectangle(0, 0, a.Width, a.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            int bytes = a.Width * a.Height * 3;
            IntPtr ptr = bmpData.Scan0;
            int stride = bmpData.Stride;
            unsafe
            {
                byte* p = (byte*)ptr;
                int temp;
                for (int j = 0; j < a.Height; j++)
                {
                    for (int i = 0; i < a.Width * 3; i++, p++)
                    {
                        temp = (int)(p[0] * v / 100);
                        temp = (temp > 255) ? 255 : temp < 0 ? 0 : temp;
                        p[0] = (byte)temp;
                    }
                    p += stride - a.Width * 3;
                }
            }
            a.UnlockBits(bmpData);
            return a;
        }
        #endregion

        private void 改变亮度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocument doc = GetCurrentImageDoc();
            BrightnessForm brightnessForm = new BrightnessForm(this.Location.X, this.Location.Y, this.Size, (int)doc.Brightness);
            brightnessForm.ShowDialog();
            if (brightnessForm.DialogResult == DialogResult.OK)
            {
                doc.Brightness = 100;
                doc.CurrentImage = (Image)BrightnessP((Bitmap)doc.CurrentImage.Clone(), brightnessForm.Value).Clone();
                brightnessForm.Dispose();
            }
            else
            {
                return;
            }
            UpdateImageDoc(doc);
            PushChanges(doc);
        }
        #endregion

        #region 对比度与对比度算法
        private void 调整对比度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocument doc = GetCurrentImageDoc();
            ContrastForm contrastForm = new ContrastForm(this.Location.X, this.Location.Y, this.Size, (int)doc.Contrast);
            contrastForm.ShowDialog();
            if (contrastForm.DialogResult == DialogResult.OK)
            {
                doc.Contrast = contrastForm.Value;
                contrastForm.Dispose();
            }
            UpdateImageDoc(doc);
        }

        #region 对比度算法
        private static Bitmap KiContrast(Bitmap b, int degree)
        {
            if (b == null)
            {
                return null;
            }

            if (degree < -100) degree = -100;
            if (degree > 100) degree = 100;

            try
            {

                double pixel = 0;
                double contrast = (100.0 + degree) / 100.0;
                contrast *= contrast;
                int width = b.Width;
                int height = b.Height;
                BitmapData data = b.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                unsafe
                {
                    byte* p = (byte*)data.Scan0;
                    int offset = data.Stride - width * 3;
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            // 处理指定位置像素的对比度
                            for (int i = 0; i < 3; i++)
                            {
                                pixel = ((p[i] / 255.0 - 0.5) * contrast + 0.5) * 255;
                                if (pixel < 0) pixel = 0;
                                if (pixel > 255) pixel = 255;
                                p[i] = (byte)pixel;
                            } // i
                            p += 3;
                        } // x
                        p += offset;
                    } // y
                }
                b.UnlockBits(data);
                return b;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        private void 改变对比度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocument doc = GetCurrentImageDoc();
            ContrastForm contrastForm = new ContrastForm(this.Location.X, this.Location.Y, this.Size, (int)doc.Contrast);
            contrastForm.ShowDialog();
            if (contrastForm.DialogResult == DialogResult.OK)
            {
                doc.Contrast = 100;
                doc.CurrentImage = (Image)KiContrast((Bitmap)doc.CurrentImage.Clone(), contrastForm.Value - 100).Clone();
                contrastForm.Dispose();
            }
            else
            {
                return;
            }
            UpdateImageDoc(doc);
            PushChanges(doc);
        }
        #endregion

        #region 简单模糊、高斯模糊、平均模糊方法
        private void 简单模糊ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocument doc = GetCurrentImageDoc();
            BlurForm blurForm = new BlurForm(this.Location.X, this.Location.Y, this.Size, true);
            blurForm.ShowDialog();
            if (blurForm.DialogResult == DialogResult.OK)
            {
                Console.WriteLine(blurForm.Radius);
                Console.WriteLine(blurForm.Value);
                doc.CurrentImage = (Image)SimpleBlur((Bitmap)doc.CurrentImage, blurForm.Radius, (float)blurForm.Value / 100F).Clone();
            }
            else
            {
                return;
            }
            UpdateImageDoc(doc);
            PushChanges(doc);
        }

        #region 简单模糊算法
        private static Bitmap SimpleBlur(Bitmap bitmap, int radius, float sigma)
        {

            if (bitmap == null)
            {
                return null;
            }

            int width = bitmap.Width;
            int height = bitmap.Height;

            float pa = (float)(1 / (Math.Sqrt(2 * Math.PI) * sigma));
            float pb = -1.0f / (2 * sigma * sigma);

            // generate the Gauss Matrix
            float[] gaussMatrix = new float[radius * 2 + 1];
            float gaussSum = 0f;
            for (int i = 0, x = -radius; x <= radius; ++x, ++i)
            {
                float g = (float)(pa * Math.Exp(pb * x * x));
                gaussMatrix[i] = g;
                gaussSum += g;
            }

            for (int i = 0, length = gaussMatrix.Length; i < length; ++i)
            {
                gaussMatrix[i] /= gaussSum;
            }

            try
            {
                Bitmap bmpReturn = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                BitmapData srcBits = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                BitmapData targetBits = bmpReturn.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
                unsafe
                {
                    byte* pSrcBits = (byte*)srcBits.Scan0.ToPointer();
                    byte* pTargetBits = (byte*)targetBits.Scan0.ToPointer();
                    int stride = srcBits.Stride;
                    byte* pTemp;

                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                            {
                                //最边上的像素不处理
                                pTargetBits[0] = pSrcBits[0];
                                pTargetBits[1] = pSrcBits[1];
                                pTargetBits[2] = pSrcBits[2];
                            }
                            else
                            {
                                //取周围9点的值
                                int r1, r2, r3, r4, r5, r6, r7, r8, r9;
                                int g1, g2, g3, g4, g5, g6, g7, g8, g9;
                                int b1, b2, b3, b4, b5, b6, b7, b8, b9;

                                float fR, fG, fB;

                                //左上
                                pTemp = pSrcBits - stride - 3;
                                r1 = pTemp[2];
                                g1 = pTemp[1];
                                b1 = pTemp[0];

                                //正上
                                pTemp = pSrcBits - stride;
                                r2 = pTemp[2];
                                g2 = pTemp[1];
                                b2 = pTemp[0];

                                //右上
                                pTemp = pSrcBits - stride + 3;
                                r3 = pTemp[2];
                                g3 = pTemp[1];
                                b3 = pTemp[0];

                                //左侧
                                pTemp = pSrcBits - 3;
                                r4 = pTemp[2];
                                g4 = pTemp[1];
                                b4 = pTemp[0];

                                //右侧
                                pTemp = pSrcBits + 3;
                                r5 = pTemp[2];
                                g5 = pTemp[1];
                                b5 = pTemp[0];

                                //右下
                                pTemp = pSrcBits + stride - 3;
                                r6 = pTemp[2];
                                g6 = pTemp[1];
                                b6 = pTemp[0];

                                //正下
                                pTemp = pSrcBits + stride;
                                r7 = pTemp[2];
                                g7 = pTemp[1];
                                b7 = pTemp[0];

                                //右下
                                pTemp = pSrcBits + stride + 3;
                                r8 = pTemp[2];
                                g8 = pTemp[1];
                                b8 = pTemp[0];

                                //自己
                                pTemp = pSrcBits;
                                r9 = pTemp[2];
                                g9 = pTemp[1];
                                b9 = pTemp[0];

                                fR = (float)(r1 + r2 + r3 + r4 + r5 + r6 + r7 + r8 + r9);
                                fG = (float)(g1 + g2 + g3 + g4 + g5 + g6 + g7 + g8 + g9);
                                fB = (float)(b1 + b2 + b3 + b4 + b5 + b6 + b7 + b8 + b9);

                                fR /= 9;
                                fG /= 9;
                                fB /= 9;

                                pTargetBits[0] = (byte)fB;
                                pTargetBits[1] = (byte)fG;
                                pTargetBits[2] = (byte)fR;

                            }

                            pSrcBits += 3;
                            pTargetBits += 3;
                        }

                        pSrcBits += srcBits.Stride - width * 3;
                        pTargetBits += srcBits.Stride - width * 3;
                    }
                }

                bitmap.UnlockBits(srcBits);
                bmpReturn.UnlockBits(targetBits);

                return bmpReturn;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        private void 高斯模糊ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocument doc = GetCurrentImageDoc();
            BlurForm blurForm = new BlurForm(this.Location.X, this.Location.Y, this.Size, false);
            blurForm.ShowDialog();
            if (blurForm.DialogResult == DialogResult.OK)
            {
                GaussianBlur gaussianBlur = new GaussianBlur(blurForm.Radius);
                gaussianBlur.SetSourceImage((Bitmap)doc.CurrentImage);
                this.Cursor = Cursors.WaitCursor;
                doc.CurrentImage = (Image)gaussianBlur.GetBlurImage().Clone();
            }
            else
            {
                return;
            }
            UpdateImageDoc(doc);
            PushChanges(doc);
            this.Cursor = Cursors.Default;
        }
        #endregion

        #region 获取指针所指像素颜色
        /// <summary>
        /// 获取指定窗口的设备场景
        /// </summary>
        /// <param name="hwnd">将获取其设备场景的窗口的句柄。若为0，则要获取整个屏幕的DC</param>
        /// <returns>指定窗口的设备场景句柄，出错则为0</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        /// <summary>
        /// 释放由调用GetDC函数获取的指定设备场景
        /// </summary>
        /// <param name="hwnd">要释放的设备场景相关的窗口句柄</param>
        /// <param name="hdc">要释放的设备场景句柄</param>
        /// <returns>执行成功为1，否则为0</returns>
        [DllImport("user32.dll")]
        public static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

        /// <summary>
        /// 在指定的设备场景中取得一个像素的RGB值
        /// </summary>
        /// <param name="hdc">一个设备场景的句柄</param>
        /// <param name="nXPos">逻辑坐标中要检查的横坐标</param>
        /// <param name="nYPos">逻辑坐标中要检查的纵坐标</param>
        /// <returns>指定点的颜色</returns>
        [DllImport("gdi32.dll")]
        public static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);
        public Color GetColor(int x, int y)
        {
            IntPtr hdc = GetDC(IntPtr.Zero); uint pixel = GetPixel(hdc, x, y);
            ReleaseDC(IntPtr.Zero, hdc);
            Color color = Color.FromArgb((int)(pixel & 0x000000FF), (int)(pixel & 0x0000FF00) >> 8, (int)(pixel & 0x00FF0000) >> 16);
            return color;
        }
        #endregion

        #region 屏幕底端文档动态信息栏、环境清理回收、全局控件控制计时器
        private bool isExistDocument = false;
        private void StatusStripRunning_Tick(object sender, EventArgs e)
        {
            if (isControlsbarActivated == true)
            {
                控件栏ToolStripMenuItem.Checked = true;
            }
            else
            {
                控件栏ToolStripMenuItem.Checked = false;
            }

            if (isToolsbarActivated == true)
            {
                工具栏ToolStripMenuItem.Checked = true;
            }
            else
            {
                工具栏ToolStripMenuItem.Checked = false;
            }

            if (toolStripProgressBar1.Enabled == true)
            {
                while (toolStripProgressBar1.Value != toolStripProgressBar1.Maximum)
                {
                    toolStripProgressBar1.Value += 1;
                    toolStripProgressBar1.Invalidate();
                }
            }
            else
            {
                toolStripProgressBar1.Visible = false;
            }

            if (TabPageManager.TabPages.Count != 0)
            {
                if (isExistDocument == false)
                {
                    isExistDocument = true;
                    setStatusLabel(true);
                }

                ImageDocument doc = ((ImageDocument)TabPageManager.SelectedTab.Controls[0]);
                toolStripStatusLabel1.Text = "图像大小：" + doc.CurrentImage.Width.ToString() + " × " + doc.CurrentImage.Height.ToString();
                toolStripStatusLabel2.Text = "预览缩放比率：" + doc.ScaleRatio.ToString() + "%";

                Point nowPos = PointToScreen(new Point(doc.Location.X, doc.Location.Y));
                int LocationX = MousePosition.X - nowPos.X - 4;
                int LocationY = MousePosition.Y - nowPos.Y - 91;
                if (0 <= LocationX && LocationX <= doc.Size.Width && 0 <= LocationY && LocationY <= doc.Size.Height)
                {
                    toolStripStatusLabel3.Text = "指针位置：" + "(" + LocationX.ToString() + ", " + LocationY.ToString() + ")";
                    Color CursorColor = GetColor(MousePosition.X, MousePosition.Y);
                    toolStripStatusLabel4.Text = "指针颜色RGB：" + "(" + CursorColor.R.ToString() + ", " + CursorColor.G.ToString() + ", " + CursorColor.B.ToString() + ")";
                    float Y = 0.299F * CursorColor.R + 0.587F * CursorColor.G + 0.114F * CursorColor.B;
                    float U = -0.1687F * CursorColor.R - 0.3313F * CursorColor.G + 0.5F * CursorColor.B + 128F;
                    float V = 0.5F * CursorColor.R - 0.4187F * CursorColor.G - 0.0813F * CursorColor.B + 128F;
                    toolStripStatusLabel9.Text = "指针位置YCbCr：" + "(" + string.Format("{0:f3}", Y) + ", " + string.Format("{0:f3}", U) + ", " + string.Format("{0:f3}", V) + ")";
                }
                else
                {
                    toolStripStatusLabel3.Text = "指针位置：超出画板区域";
                    toolStripStatusLabel4.Text = "指针位置RGB：超出画板区域";
                    toolStripStatusLabel9.Text = "指针位置YCbCr：超出画板区域";
                }

                if (TabPageManager.TabPages.Count == 1)
                {
                    关闭除当前选项卡外的所有选项卡ToolStripMenuItem.Enabled = false;
                }
                else
                {
                    关闭除当前选项卡外的所有选项卡ToolStripMenuItem.Enabled = true;
                }
                if (TabPageManager.SelectedTab == TabPageManager.TabPages[0])
                {
                    关闭右侧选项卡ToolStripMenuItem.Enabled = false;
                }
                else
                {
                    关闭右侧选项卡ToolStripMenuItem.Enabled = true;
                }
                if (TabPageManager.SelectedTab == TabPageManager.TabPages[TabPageManager.TabPages.Count - 1])
                {
                    关闭右侧选项卡ToolStripMenuItem1.Enabled = false;
                }
                else
                {
                    关闭右侧选项卡ToolStripMenuItem1.Enabled = true;
                }

                toolStripSeparator14.Visible = true;
                MarkNowImageDocatMenu();

                if(doc.CtrlZ.Count <= 1)
                {
                    撤销上一步操作ToolStripMenuItem.Enabled = false;
                    toolStripButton7.Enabled = false;
                }
                else
                {
                    撤销上一步操作ToolStripMenuItem.Enabled = true;
                    toolStripButton7.Enabled = true;
                }

                if(doc.CtrlY.Count <= 0)
                {
                    重做ToolStripMenuItem.Enabled = false;
                    toolStripButton8.Enabled = false;
                }
                else
                {
                    重做ToolStripMenuItem.Enabled = true;
                    toolStripButton8.Enabled = true;
                }

                string Format = doc.Name.Substring(doc.Name.Length - 4, 4);
                if(Format == ".bmp" || Format == ".BMP")
                {
                    toolStripLabel1.Image = toolStripButton20.Image;
                }
                else if(Format == ".jpg" || Format == ".JPG")
                {
                    toolStripLabel1.Image = Painting_at_ZJU.Properties.Resources._0xFFC;
                }
                else if(Format == ".png" || Format == ".PNG")
                {
                    toolStripLabel1.Image = Painting_at_ZJU.Properties.Resources._0xFFE;
                }
                else
                {
                    toolStripLabel1.Image = Painting_at_ZJU.Properties.Resources._0xFFD;
                }
            }

            else
            {
                if (isExistDocument == true)
                {
                    isExistDocument = false;
                    setStatusLabel(false);
                    TabPageManager.Visible = false;
                    label1.Visible = true;
                    panel3.Visible = true;
                }
                toolStripSeparator14.Visible = false;
                toolStripSplitButton1.Text = "当前无打开窗口";
                toolStripLabel1.Image = Painting_at_ZJU.Properties.Resources._0xFFF;
            }
            GC.Collect();
        }
        #endregion

        #region 自动标记在窗口下拉菜单中的当前打开文档
        private void MarkNowImageDocatMenu()
        {
            foreach(ToolStripItem toolStripItem in 窗体ToolStripMenuItem.DropDownItems)
            {
                try
                {
                    if(ItemtoWnd.ContainsKey(toolStripItem) == false)
                    {
                        continue;
                    }
                    else
                    {
                        if(((TabPage)ItemtoWnd[toolStripItem]) == TabPageManager.SelectedTab)
                        {
                            ((ToolStripMenuItem)toolStripItem).Checked = true;
                        }
                        else
                        {
                            ((ToolStripMenuItem)toolStripItem).Checked = false;
                        }
                    }
                }
                catch(Exception)
                {

                }
            }

            foreach (ToolStripItem toolStripItem in toolStripSplitButton1.DropDownItems)
            {
                try
                {
                    if (ItemtoWnd_Button.ContainsKey(toolStripItem) == false)
                    {
                        continue;
                    }
                    else
                    {
                        if (((TabPage)ItemtoWnd_Button[toolStripItem]) == TabPageManager.SelectedTab)
                        {
                            ((ToolStripMenuItem)toolStripItem).Checked = true;
                            toolStripSplitButton1.Text = toolStripItem.Text;
                        }
                        else
                        {
                            ((ToolStripMenuItem)toolStripItem).Checked = false;
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
        }
        #endregion

        #region 调用AForge图像处理算法API：UseAForgeAlogorithmAPI(IFilter filter)
        private void UseAForgeAlogorithmAPI(IFilter filter)
        {
            ImageDocument doc = GetCurrentImageDoc();
            Image TempImg = (Image)doc.CurrentImage.Clone();
            Bitmap newImage = filter.Apply((Bitmap)TempImg);
            doc.CurrentImage = (Image)newImage.Clone();
            newImage.Dispose();
            TempImg.Dispose();
            UpdateImageDoc(doc);
            PushChanges(doc);
        }
        #endregion

        #region 图像二值化以及图像抖动方法

        #region 自适应、定阈值、Otsu's二值化及其算法
        private void 自适应二值化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocument doc = GetCurrentImageDoc();
            Threshold(doc, 1);
            UpdateImageDoc(doc);
            PushChanges(doc);
        }

        private void 定阈值二值化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocument doc = GetCurrentImageDoc();
            int threshold = 0;
            ThresholdForm thresholdForm = new ThresholdForm(this.Location.X, this.Location.Y, this.Size);
            thresholdForm.ShowDialog();
            if (thresholdForm.DialogResult == DialogResult.OK)
            {
                threshold = thresholdForm.Value;
                thresholdForm.Dispose();
            }
            else
            {
                return;
            }
            Threshold(doc, 2, threshold);
            UpdateImageDoc(doc);
            PushChanges(doc);
        }

        private void otsus二值化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocument doc = GetCurrentImageDoc();
            Threshold(doc, 3);
            UpdateImageDoc(doc);
            PushChanges(doc);
        }

        private void Threshold(ImageDocument o, uint Method, int threshold = 0)
        {
            if (Method == 1)
            {
                o.CurrentImage = (Image)AdaptiveThreshold((Bitmap)o.CurrentImage.Clone()).Clone();
            }
            else if (Method == 2)
            {
                o.CurrentImage = (Image)FixedThreshold((Bitmap)o.CurrentImage.Clone(), (byte)threshold).Clone();
            }
            else if (Method == 3)
            {
                o.CurrentImage = (Image)OtsuThreshold((Bitmap)o.CurrentImage.Clone()).Clone();
            }
        }

        private Bitmap AdaptiveThreshold(Bitmap bmp)
        {
            int average = 0;
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color color = bmp.GetPixel(i, j);
                    average += (int)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11);
                }
            }
            average = (int)average / (bmp.Width * bmp.Height);

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color color = bmp.GetPixel(i, j);
                    int value = 255 - (int)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11);
                    Color newColor = value > average ? Color.FromArgb(0, 0, 0) : Color.FromArgb(255, 255, 255);
                    bmp.SetPixel(i, j, newColor);
                }
            }
            return bmp;
        }

        private Bitmap FixedThreshold(Bitmap b, byte threshold)
        {
            int width = b.Width;
            int height = b.Height;
            BitmapData data = b.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe

            {
                byte* p = (byte*)data.Scan0;
                int offset = data.Stride - width * 4;
                byte R, G, B, gray;
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        R = p[2];
                        G = p[1];
                        B = p[0];
                        gray = (byte)((R * 19595 + G * 38469 + B * 7472) >> 16);
                        if (gray >= threshold)
                        {
                            p[0] = p[1] = p[2] = 255;
                        }
                        else
                        {
                            p[0] = p[1] = p[2] = 0;
                        }
                        p += 4;
                    }
                    p += offset;
                }
                b.UnlockBits(data);
                return b;
            }
        }

        private Bitmap OtsuThreshold(Bitmap b)
        {
            int width = b.Width;
            int height = b.Height;
            byte threshold = 0;
            int[] hist = new int[256];
            int AllPixelNumber = 0, PixelNumberSmall = 0, PixelNumberBig = 0;
            double MaxValue, AllSum = 0, SumSmall = 0, SumBig, ProbabilitySmall, ProbabilityBig, Probability;
            BitmapData data = b.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* p = (byte*)data.Scan0;
                int offset = data.Stride - width * 4;
                for (int j = 0; j < height; j++)
                {
                    for (int i = 0; i < width; i++)
                    {
                        hist[p[0]]++;
                        p += 4;
                    }
                    p += offset;
                }
                b.UnlockBits(data);
            }
            for (int i = 0; i < 256; i++)
            {
                AllSum += i * hist[i];     //   质量矩   
                AllPixelNumber += hist[i];  //  质量       
            }
            MaxValue = -1.0;
            for (int i = 0; i < 256; i++)
            {
                PixelNumberSmall += hist[i];
                PixelNumberBig = AllPixelNumber - PixelNumberSmall;
                if (PixelNumberBig == 0)
                {
                    break;
                }
                SumSmall += i * hist[i];
                SumBig = AllSum - SumSmall;
                ProbabilitySmall = SumSmall / PixelNumberSmall;
                ProbabilityBig = SumBig / PixelNumberBig;
                Probability = PixelNumberSmall * ProbabilitySmall * ProbabilitySmall + PixelNumberBig * ProbabilityBig * ProbabilityBig;
                if (Probability > MaxValue)
                {
                    MaxValue = Probability;
                    threshold = (byte)i;
                }
            }
            return this.FixedThreshold(b, threshold);
        }
        #endregion

        private void floydSteinberg抖动算法ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UseAForgeAlogorithmAPI(new FloydSteinbergDithering());
        }

        private void 平均ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UseAForgeAlogorithmAPI(new Mean());
        }

        private void burkes抖动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UseAForgeAlogorithmAPI(new BurkesDithering());
        }

        private void stucki抖动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UseAForgeAlogorithmAPI(new StuckiDithering());
        }

        private void jarvisJudiceNinke抖动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UseAForgeAlogorithmAPI(new JarvisJudiceNinkeDithering());
        }

        private void sierra抖动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UseAForgeAlogorithmAPI(new SierraDithering());
        }

        private void 有序抖动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UseAForgeAlogorithmAPI(new OrderedDithering());
        }

        private void bayer有序抖动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UseAForgeAlogorithmAPI(new BayerDithering());
        }

        private void 简单图像统计二值化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UseAForgeAlogorithmAPI(new SISThreshold());
        }

        private void 误差进位阈值二值化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UseAForgeAlogorithmAPI(new ThresholdWithCarry());
        }
        #endregion

        #region 怀旧、通道旋转、保留和去除色彩通道色彩方法
        private void 怀旧ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UseAForgeAlogorithmAPI(new Sepia());
        }

        private void 三原色旋转ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UseAForgeAlogorithmAPI(new RotateChannels());
        }

        private void 青绿ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UseAForgeAlogorithmAPI(new ChannelFiltering(new IntRange(0, 0), new IntRange(0, 255), new IntRange(0, 255)));
        }

        private void 洋红ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UseAForgeAlogorithmAPI(new ChannelFiltering(new IntRange(0, 255), new IntRange(0, 0), new IntRange(0, 255)));
        }

        private void 黄色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UseAForgeAlogorithmAPI(new ChannelFiltering(new IntRange(0, 255), new IntRange(0, 255), new IntRange(0, 0)));
        }

        private void 红色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UseAForgeAlogorithmAPI(new ChannelFiltering(new IntRange(0, 255), new IntRange(0, 0), new IntRange(0, 0)));
        }

        private void 绿色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UseAForgeAlogorithmAPI(new ChannelFiltering(new IntRange(0, 0), new IntRange(0, 255), new IntRange(0, 0)));
        }

        private void 蓝色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UseAForgeAlogorithmAPI(new ChannelFiltering(new IntRange(0, 0), new IntRange(0, 0), new IntRange(0, 255)));
        }
        #endregion

        #region 腐蚀、膨胀、开运算、闭运算风格化方法
        private void 腐蚀ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UseAForgeAlogorithmAPI(new Erosion());
        }

        private void 膨胀ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UseAForgeAlogorithmAPI(new Dilatation());
        }

        private void 开运算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UseAForgeAlogorithmAPI(new Opening());
        }

        private void 闭运算ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UseAForgeAlogorithmAPI(new Closing());
        }
        #endregion

        #region 锐化、扩展锐化锐化方法
        private void 锐化ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ImageDocument doc = GetCurrentImageDoc();
            SharpenForm sharpenForm = new SharpenForm(this.Location.X, this.Location.Y, this.Size);
            sharpenForm.ShowDialog();
            if (sharpenForm.DialogResult == DialogResult.OK)
            {
                doc.CurrentImage = (Image)KiSharpen((Bitmap)doc.CurrentImage, (float)sharpenForm.Value / 100F).Clone();
            }
            PushChanges(doc);
            UpdateImageDoc(doc);
        }

        #region 锐化算法
        private static Bitmap KiSharpen(Bitmap b, float val)
        {
            if (b == null)
            {
                return null;
            }

            int w = b.Width;
            int h = b.Height;

            try
            {
                Bitmap bmpRtn = new Bitmap(w, h, PixelFormat.Format24bppRgb);

                BitmapData srcData = b.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                BitmapData dstData = bmpRtn.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

                unsafe
                {
                    byte* pIn = (byte*)srcData.Scan0.ToPointer();
                    byte* pOut = (byte*)dstData.Scan0.ToPointer();
                    int stride = srcData.Stride;
                    byte* p;

                    for (int y = 0; y < h; y++)
                    {
                        for (int x = 0; x < w; x++)
                        {
                            //取周围9点的值。位于边缘上的点不做改变。
                            if (x == 0 || x == w - 1 || y == 0 || y == h - 1)
                            {
                                //不做
                                pOut[0] = pIn[0];
                                pOut[1] = pIn[1];
                                pOut[2] = pIn[2];
                            }
                            else
                            {
                                int r1, r2, r3, r4, r5, r6, r7, r8, r0;
                                int g1, g2, g3, g4, g5, g6, g7, g8, g0;
                                int b1, b2, b3, b4, b5, b6, b7, b8, b0;

                                float vR, vG, vB;

                                //左上
                                p = pIn - stride - 3;
                                r1 = p[2];
                                g1 = p[1];
                                b1 = p[0];

                                //正上
                                p = pIn - stride;
                                r2 = p[2];
                                g2 = p[1];
                                b2 = p[0];

                                //右上
                                p = pIn - stride + 3;
                                r3 = p[2];
                                g3 = p[1];
                                b3 = p[0];

                                //左侧
                                p = pIn - 3;
                                r4 = p[2];
                                g4 = p[1];
                                b4 = p[0];

                                //右侧
                                p = pIn + 3;
                                r5 = p[2];
                                g5 = p[1];
                                b5 = p[0];

                                //右下
                                p = pIn + stride - 3;
                                r6 = p[2];
                                g6 = p[1];
                                b6 = p[0];

                                //正下
                                p = pIn + stride;
                                r7 = p[2];
                                g7 = p[1];
                                b7 = p[0];

                                //右下
                                p = pIn + stride + 3;
                                r8 = p[2];
                                g8 = p[1];
                                b8 = p[0];

                                //自己
                                p = pIn;
                                r0 = p[2];
                                g0 = p[1];
                                b0 = p[0];

                                vR = (float)r0 - (float)(r1 + r2 + r3 + r4 + r5 + r6 + r7 + r8) / 8;
                                vG = (float)g0 - (float)(g1 + g2 + g3 + g4 + g5 + g6 + g7 + g8) / 8;
                                vB = (float)b0 - (float)(b1 + b2 + b3 + b4 + b5 + b6 + b7 + b8) / 8;

                                vR = r0 + vR * val;
                                vG = g0 + vG * val;
                                vB = b0 + vB * val;

                                if (vR > 0)
                                {
                                    vR = Math.Min(255, vR);
                                }
                                else
                                {
                                    vR = Math.Max(0, vR);
                                }

                                if (vG > 0)
                                {
                                    vG = Math.Min(255, vG);
                                }
                                else
                                {
                                    vG = Math.Max(0, vG);
                                }

                                if (vB > 0)
                                {
                                    vB = Math.Min(255, vB);
                                }
                                else
                                {
                                    vB = Math.Max(0, vB);
                                }

                                pOut[0] = (byte)vB;
                                pOut[1] = (byte)vG;
                                pOut[2] = (byte)vR;

                            }

                            pIn += 3;
                            pOut += 3;
                        }// end of x

                        pIn += srcData.Stride - w * 3;
                        pOut += srcData.Stride - w * 3;
                    } // end of y
                }

                b.UnlockBits(srcData);
                bmpRtn.UnlockBits(dstData);

                return bmpRtn;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        private void 扩展锐化ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UseAForgeAlogorithmAPI(new SharpenEx());
        }
        #endregion

        #region 灰度化与灰度化算法
        private void 灰度化ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ImageDocument doc = GetCurrentImageDoc();
            GrayScale(doc);
            UpdateImageDoc(doc);
        }

        private void GrayScale(ImageDocument o)
        {
            Bitmap Map = new Bitmap(o.CurrentImage.Width, o.CurrentImage.Height);
            for (int i = 0; i < o.CurrentImage.Width; i++)
            {
                for (int j = 0; j < o.CurrentImage.Height; j++)
                {
                    //获取该点的像素的RGB的颜色
                    Color color = ((Bitmap)o.CurrentImage).GetPixel(i, j);
                    //利用公式计算灰度值
                    int gray = (int)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11);
                    Color newColor = Color.FromArgb(gray, gray, gray);
                    Map.SetPixel(i, j, newColor);
                }
            }
            o.CurrentImage = (Image)Map.Clone();
            Map.Dispose();
            PushChanges(o);
        }
        #endregion

        #region 反色与反射算法算法
        private void 反色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocument doc = GetCurrentImageDoc();
            GrayReverse(doc);
            UpdateImageDoc(doc);
        }

        private void GrayReverse(ImageDocument o)
        {
            Bitmap Map = new Bitmap(o.CurrentImage.Width, o.CurrentImage.Height);
            for (int i = 0; i < o.CurrentImage.Width; i++)
            {
                for (int j = 0; j < o.CurrentImage.Height; j++)
                {
                    Color color = ((Bitmap)o.CurrentImage).GetPixel(i, j);
                    Color newColor = Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B);
                    Map.SetPixel(i, j, newColor);
                }
            }
            o.CurrentImage = (Image)Map.Clone();
            Map.Dispose();
            PushChanges(o);
        }
        #endregion

        #region 边缘检测、同度、差值、Sobel、Canny边缘检测方法
        private void 边缘检测ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            UseAForgeAlogorithmAPI(new Edges());
        }

        private void 同度ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UseAForgeAlogorithmAPI(new HomogenityEdgeDetector());
        }

        private void 差值ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UseAForgeAlogorithmAPI(new DifferenceEdgeDetector());
        }

        private void sobelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UseAForgeAlogorithmAPI(new SobelEdgeDetector());
        }

        private void cannyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UseAForgeAlogorithmAPI(new CannyEdgeDetector());
        }
        #endregion

        #region 应用程序右上角自定义关闭、最大化、最小化按钮动效和事件
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox2.BackgroundImage = Painting_at_ZJU.Properties.Resources.Minimize02;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1.BackgroundImage = Painting_at_ZJU.Properties.Resources.QuitButton02;
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox2.BackgroundImage = Painting_at_ZJU.Properties.Resources.Minimize01;
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.BackgroundImage = Painting_at_ZJU.Properties.Resources.QuitButton01;
            this.Close();
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox3.BackgroundImage = Painting_at_ZJU.Properties.Resources.Maximize02;
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox3.BackgroundImage = Painting_at_ZJU.Properties.Resources.Maximize01;
            if (this.WindowState != FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
            pictureBox3.Visible = false;
            this.Refresh();
        }
        #endregion

        #region 快速导出为PNG功能
        private void button1_Click(object sender, EventArgs e)
        {
            ImageDocument doc = GetCurrentImageDoc();
            saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog.Filter = "便携式网络图形(.png)|*.png";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = doc.Name.Substring(0, doc.Name.Length - 4);
            if (MessageBox.Show("您确定要保存对当前图像的编辑吗？", "保存", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Cancel)
            {
                return;
            }
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string Path = saveFileDialog.FileName.ToString();
                doc.CurrentImage.Save(Path, ImageFormat.Png);
            }
        }
        #endregion

        #region 开发者信息窗体调用
        DeveloperForm developerForm = null;
        private void 开发者信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (developerForm != null)
            {
                developerForm.Dispose();
            }
            developerForm = new DeveloperForm(this.Location.X, this.Location.Y, this.Size);
            developerForm.Show();
        }
        #endregion

        #region 当主窗体尺寸改变时，重设可能相对位置不会自动改变的所有控件的位置
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            label1.Location = new Point(panel3.Width / 2 - label1.Width / 2, panel3.Height / 2 - label1.Height / 2);
            ImageDocument doc = GetCurrentImageDoc();
            if (doc != null)
            {
                ResizeImageDoc(doc);
            }
        }
        #endregion

        #region 快速转移、复制图像文档
        private void 将当前更改保存到新图像文档ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocument nowDoc = GetCurrentImageDoc();
            string Path = nowDoc.ImgPath.Substring(0, nowDoc.ImgPath.Length - 4) + "[经过修改后的]" + ".bmp";
            Bitmap Clone = new Bitmap(nowDoc.CurrentImage);
            Clone.Save(Path, ImageFormat.Bmp);
            string[] PathSplit = Path.Split('\\');
            string ImageDocName = PathSplit[PathSplit.Length - 1];
            TabPage tp = new TabPage();
            tp.Parent = TabPageManager;
            tp.Text = ImageDocName;
            tp.Show();
            TabPageManager.SelectedTab = tp;
            TabPageManager.Visible = true;
            System.Drawing.Image Img = System.Drawing.Image.FromFile(Path);
            ImageDocument ImageDoc = new ImageDocument(ImageDocName, tp.Size, Img);
            ImageDoc.TopLevel = false;
            ImageDoc.Dock = DockStyle.Fill;
            ImageDoc.Parent = tp;
            ImageDoc.TabPageChild = tp;
            ImageDoc.Show();
            ImageDoc.TabPageManager = this.TabPageManager;
            ImageDoc.ImgPath = Path;

            ToolStripMenuItem newWnd = new ToolStripMenuItem();
            newWnd.Text = ImageDoc.Text;
            ItemtoWnd.Add(newWnd, tp);
            WndtoItem.Add(tp, newWnd);
            newWnd.Click += new System.EventHandler(newWndClick);
            窗体ToolStripMenuItem.DropDownItems.Add(newWnd);

            ToolStripMenuItem newWnd_Button = new ToolStripMenuItem();
            newWnd_Button.Text = ImageDoc.Text;
            ItemtoWnd_Button.Add(newWnd_Button, tp);
            WndtoItem_Button.Add(tp, newWnd_Button);
            newWnd_Button.Click += new System.EventHandler(newWndClick_Button);
            toolStripSplitButton1.DropDownItems.Add(newWnd_Button);

            ImageDoc.ShowContextMenuStrip += new ShowContextMenuStripHandle(ShowContextDialog);
            ImageDoc.HideContextMenuStrip += new HideContextMenuStripHandle(HideContextDialog);
        }

        private void 将当前文档复制到新的图像文档ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocument nowDoc = GetCurrentImageDoc();
            string Path = nowDoc.ImgPath.Substring(0, nowDoc.ImgPath.Length - 4) + "[新建的]" + ".bmp";
            Bitmap Clone = new Bitmap(nowDoc.CurrentImage);
            Clone.Save(Path, ImageFormat.Bmp);
            string[] PathSplit = Path.Split('\\');
            string ImageDocName = PathSplit[PathSplit.Length - 1];
            TabPage tp = new TabPage();
            tp.Parent = TabPageManager;
            tp.Text = ImageDocName;
            tp.Show();
            TabPageManager.SelectedTab = tp;
            TabPageManager.Visible = true;
            System.Drawing.Image Img = System.Drawing.Image.FromFile(Path);
            ImageDocument ImageDoc = new ImageDocument(ImageDocName, tp.Size, Img);
            ImageDoc.TopLevel = false;
            ImageDoc.Dock = DockStyle.Fill;
            ImageDoc.Parent = tp;
            ImageDoc.TabPageChild = tp;
            ImageDoc.Show();
            ImageDoc.TabPageManager = this.TabPageManager;
            ImageDoc.ImgPath = Path;

            ToolStripMenuItem newWnd = new ToolStripMenuItem();
            newWnd.Text = ImageDoc.Text;
            ItemtoWnd.Add(newWnd, tp);
            WndtoItem.Add(tp, newWnd);
            newWnd.Click += new System.EventHandler(newWndClick);
            窗体ToolStripMenuItem.DropDownItems.Add(newWnd);

            ToolStripMenuItem newWnd_Button = new ToolStripMenuItem();
            newWnd_Button.Text = ImageDoc.Text;
            ItemtoWnd_Button.Add(newWnd_Button, tp);
            WndtoItem_Button.Add(tp, newWnd_Button);
            newWnd_Button.Click += new System.EventHandler(newWndClick_Button);
            toolStripSplitButton1.DropDownItems.Add(newWnd_Button);

            ImageDoc.ShowContextMenuStrip += new ShowContextMenuStripHandle(ShowContextDialog);
            ImageDoc.HideContextMenuStrip += new HideContextMenuStripHandle(HideContextDialog);
        }
        #endregion

        #region 删除已经不存在的窗体对应的下拉菜单标签
        private void RemoveEmptyWndLabel(TabPage tabPage)
        {
            ((ToolStripMenuItem)WndtoItem[tabPage]).Visible = false;
            ItemtoWnd.Remove((ToolStripMenuItem)WndtoItem[tabPage]);
            窗体ToolStripMenuItem.DropDownItems.Remove((ToolStripMenuItem)WndtoItem[tabPage]);
            toolStripSplitButton1.DropDownItems.Remove((ToolStripMenuItem)WndtoItem_Button[tabPage]);
            WndtoItem.Remove(tabPage);
        }
        #endregion

        #region 窗口菜单下的五种选项卡关闭方式
        private void 关闭所有选项卡ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            关闭所有ToolStripMenuItem_Click(sender, e);
        }

        private void 关闭除当前选项卡外的所有选项卡ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TabPageManager.TabCount <= 0)
            {
                return;
            }
            if (MessageBox.Show("您确定要关闭其他所有页面吗？请确保您的全部修改已经保存", "关闭其他文档", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                foreach (TabPage tabPage in TabPageManager.TabPages)
                {
                    if (tabPage == TabPageManager.SelectedTab)
                    {
                        continue;
                    }
                    foreach (Control ctrl in tabPage.Controls)
                    {
                        if (ctrl.GetType() == typeof(ImageDocument))
                        {
                            ctrl.Dispose();
                            break;
                        }
                    }
                    tabPage.Visible = false;
                    RemoveEmptyWndLabel(tabPage);
                    TabPageManager.TabPages.Remove(tabPage);
                }
            }
        }

        private void 关闭当前选项卡ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            关闭ToolStripMenuItem_Click(sender, e);
        }

        private void 关闭右侧选项卡ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 实际上是关闭左侧选项卡
            if (TabPageManager.TabCount <= 0)
            {
                return;
            }
            if (MessageBox.Show("您确定要关闭左侧的所有页面吗？请确保您的全部修改已经保存", "关闭其他文档", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                foreach (TabPage tabPage in TabPageManager.TabPages)
                {
                    if (tabPage == TabPageManager.SelectedTab)
                    {
                        break;
                    }
                    foreach (Control ctrl in tabPage.Controls)
                    {
                        if (ctrl.GetType() == typeof(ImageDocument))
                        {
                            ctrl.Dispose();
                            break;
                        }
                    }
                    tabPage.Visible = false;
                    RemoveEmptyWndLabel(tabPage);
                    TabPageManager.TabPages.Remove(tabPage);
                }
            }
        }

        private void 关闭右侧选项卡ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (TabPageManager.TabCount <= 0)
            {
                return;
            }

            bool flag = false;

            if (MessageBox.Show("您确定要关闭右侧的所有页面吗？请确保您的全部修改已经保存", "关闭其他文档", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.OK)
            {
                foreach (TabPage tabPage in TabPageManager.TabPages)
                {
                    if (tabPage == TabPageManager.SelectedTab)
                    {
                        flag = true;
                        continue;
                    }
                    if (flag == true)
                    {
                        foreach (Control ctrl in tabPage.Controls)
                        {
                            if (ctrl.GetType() == typeof(ImageDocument))
                            {
                                ctrl.Dispose();
                                break;
                            }
                        }
                        tabPage.Visible = false;
                        RemoveEmptyWndLabel(tabPage);
                        TabPageManager.TabPages.Remove(tabPage);
                    }
                }
            }
        }
        #endregion

        #region 窗口菜单下方的选项卡显示方式
        private void 将选项卡显示在多行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (将选项卡显示在多行ToolStripMenuItem.Checked == true)
            {
                return;
            }
            else
            {
                将选项卡显示在多行ToolStripMenuItem.Checked = true;
                将选项卡显示在一行ToolStripMenuItem.Checked = false;
                TabPageManager.Multiline = true;
            }
        }

        private void 将选项卡显示在一行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (将选项卡显示在一行ToolStripMenuItem.Checked == true)
            {
                return;
            }
            else
            {
                将选项卡显示在多行ToolStripMenuItem.Checked = false;
                将选项卡显示在一行ToolStripMenuItem.Checked = true;
                TabPageManager.Multiline = false;
            }
        }
        #endregion

        #region 视图居中功能
        private void 视图居中ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocument doc = GetCurrentImageDoc();
            if(doc == null)
            {
                return;
            }
            doc.HorizontalScroll.Value = doc.HorizontalScroll.Maximum / 2 - doc.HorizontalScroll.LargeChange / 2;
            if(doc.HorizontalScroll.Value != doc.HorizontalScroll.Maximum / 2 - doc.HorizontalScroll.LargeChange / 2)
            {
                doc.HorizontalScroll.Value = doc.HorizontalScroll.Maximum / 2 - doc.HorizontalScroll.LargeChange / 2;
            }
            doc.VerticalScroll.Value = doc.VerticalScroll.Maximum / 2 - doc.VerticalScroll.LargeChange / 2;
            if(doc.VerticalScroll.Value != doc.VerticalScroll.Maximum / 2 - doc.VerticalScroll.LargeChange / 2)
            {
                doc.VerticalScroll.Value = doc.VerticalScroll.Maximum / 2 - doc.VerticalScroll.LargeChange / 2;
            }
        }
        #endregion

        #region 放大与缩小按钮的图像缩放方法，Ctrl+鼠标滚轮缩放由ImageDocument类实现
        private void 放大ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocZoomIn();
        }

        private void ImageDocZoomIn()
        {
            ImageDocument doc = GetCurrentImageDoc();
            double ZoomRatio = Math.Sqrt((double)doc.PaintBoard.Width * doc.PaintBoard.Height / (doc.CurrentImage.Width * doc.CurrentImage.Height));
            if (ZoomRatio < 10)
            {
                doc.PaintBoard.Size = new Size(doc.PaintBoard.Size.Width + (int)(0.05 * doc.CurrentImage.Width), doc.PaintBoard.Size.Height + (int)(0.05 * doc.CurrentImage.Height));
                if ((double)doc.PaintBoard.Size.Width / (double)doc.CurrentImage.Width >= 10.0)
                {
                    doc.PaintBoard.Size = new Size(10 * doc.CurrentImage.Width, 10 * doc.CurrentImage.Height);
                }
                doc.Ratio = (float)doc.PaintBoard.Size.Width / (float)doc.CurrentImage.Width;
            }
            else
            {

            }
            ResizeImageDoc(doc);
        }

        private void ImageDocZoomout()
        {
            ImageDocument doc = GetCurrentImageDoc();
            double ZoomRatio = Math.Sqrt((double)doc.PaintBoard.Width * doc.PaintBoard.Height / (doc.CurrentImage.Width * doc.CurrentImage.Height));
            if (0.1 < ZoomRatio)
            {
                doc.PaintBoard.Size = new Size(doc.PaintBoard.Size.Width - (int)(0.05 * doc.CurrentImage.Width), doc.PaintBoard.Size.Height - (int)(0.05 * doc.CurrentImage.Height));
                if ((double)doc.PaintBoard.Size.Width / (double)doc.CurrentImage.Width <= 0.1)
                {
                    doc.PaintBoard.Size = new Size((int)(0.1 * doc.CurrentImage.Width), (int)(0.1 * doc.CurrentImage.Height));
                }
                doc.Ratio = (float)doc.PaintBoard.Size.Width / (float)doc.CurrentImage.Width;
            }
            else
            {

            }
            ResizeImageDoc(doc);
        }

        private void 缩小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocZoomout();
        }
        #endregion

        #region 将当前修改压入撤回栈中：PushChanges(ImageDocument o)
        private void PushChanges(ImageDocument o)
        {
            Image newG = (Image)o.CurrentImage.Clone();
            o.CtrlZ.Push(o.CurrentImage);
            newG.Dispose();
            foreach(Image img in o.CtrlY)
            {
                img.Dispose();
            }
            o.CtrlY.Clear();
        }
        #endregion

        #region 撤回和重做功能
        private void 撤销上一步操作ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 撤销，即从撤回栈中弹出一个元素，恢复给当前的CurrentImage，并且压入恢复栈
            ImageDocument doc = GetCurrentImageDoc();
            if (doc.CtrlZ.Count <= 1)
            {
                return;
            }
            Console.WriteLine("Ctrl + Z");
            Image LastImg = doc.CtrlZ.Pop();
            doc.CurrentImage = doc.CtrlZ.Peek();
            doc.CtrlY.Push(LastImg);
            doc.PaintBoard.Size = new Size((int)(doc.CurrentImage.Width * doc.Ratio), (int)(doc.CurrentImage.Height * doc.Ratio));
            UpdateImageDoc(doc);
        }

        private void 重做ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocument doc = GetCurrentImageDoc();
            if (doc.CtrlY.Count <= 0)
            {
                return;
            }
            Console.WriteLine("Ctrl + Y");
            Image LastImg = doc.CtrlY.Pop();
            doc.CurrentImage = (Image)LastImg.Clone();
            doc.CtrlZ.Push((Image)LastImg.Clone());
            LastImg.Dispose();
            doc.PaintBoard.Size = new Size((int)(doc.CurrentImage.Width * doc.Ratio), (int)(doc.CurrentImage.Height * doc.Ratio));

            UpdateImageDoc(doc);
        }
        #endregion

        #region 开启进度条等待功能
        private void StartProcessingBar()
        {
            toolStripProgressBar1.Enabled = true;
            toolStripProgressBar1.Visible = true;
        }
        #endregion

        #region 结束进度条等待功能
        private void EndProcessingBar()
        {
            toolStripProgressBar1.Value = toolStripProgressBar1.Maximum;
            toolStripProgressBar1.Enabled = false;
        }

        #endregion

        #region 控件栏与工具栏的按钮点击事件
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            打开ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            保存ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            关闭ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            撤销上一步操作ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            重做ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            将当前更改保存到新图像文档ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            将当前文档复制到新的图像文档ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            放大ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            缩小ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            亮度ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            调整对比度ToolStripMenuItem_Click(sender, e);
        }

        private void 视图铺满ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocument doc = GetCurrentImageDoc();
            double WidthRatio = (double)doc.Width / doc.CurrentImage.Width;
            double HeightRatio = (double)doc.Height / doc.CurrentImage.Height;
            double ActuallRatio = Math.Max(HeightRatio, WidthRatio);
            doc.PaintBoard.Location = new Point(0, 0);
            doc.PaintBoard.Size = new Size((int)((double)doc.CurrentImage.Width * ActuallRatio), (int)((double)doc.CurrentImage.Height * ActuallRatio));
            doc.Ratio = (float)ActuallRatio;
            doc.Refresh();
        }

        private void toolStripButton16_Click(object sender, EventArgs e)
        {
            视图铺满ToolStripMenuItem_Click(sender, e);
        }

        private void 色阶ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Histogram historgramForm = new Histogram(this.Location.X, this.Location.Y, this.Size, GetCurrentImageDoc().CurrentImage);
            historgramForm.ShowDialog();
            if(historgramForm.DialogResult == DialogResult.OK)
            {
                historgramForm.Dispose();
                return;
            }
        }

        private void 图像分析ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StatisticsForm statisticsForm = new StatisticsForm(this.Location.X, this.Location.Y, this.Size, GetCurrentImageDoc().CurrentImage);
            statisticsForm.Show();
        }

        private void toolStripButton17_Click(object sender, EventArgs e)
        {
            色阶ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton18_Click(object sender, EventArgs e)
        {
            图像分析ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton15_Click(object sender, EventArgs e)
        {
            视图居中ToolStripMenuItem_Click(sender, e);
        }

        private void 快速导出为PNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        private void toolStripButton20_Click(object sender, EventArgs e)
        {
            快速导出为PNGToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton21_Click(object sender, EventArgs e)
        {
            开发者信息ToolStripMenuItem_Click(sender, e);
        }

        private void 图像原大小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocument doc = GetCurrentImageDoc();
            doc.PaintBoard.Size = new Size(doc.CurrentImage.Width, doc.CurrentImage.Height);
            doc.Ratio = 1F;
            UpdateImageDoc(doc);
        }

        private void toolStripButton22_Click(object sender, EventArgs e)
        {
            图像原大小ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton23_Click(object sender, EventArgs e)
        {
            适应窗口ToolStripMenuItem_Click(sender, e);
        }

        private void 适应窗口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocument doc = GetCurrentImageDoc();
            double WidthRatio = (double)doc.Width / doc.CurrentImage.Width;
            double HeightRatio = (double)doc.Height / doc.CurrentImage.Height;
            double ActuallRatio = Math.Min(HeightRatio, WidthRatio);
            doc.PaintBoard.Size = new Size((int)((double)doc.CurrentImage.Width * ActuallRatio), (int)((double)doc.CurrentImage.Height * ActuallRatio));
            doc.Ratio = (float)ActuallRatio;
            ResizeImageDoc(doc);
            doc.Refresh();
        }
        #endregion

        #region 通过控件栏与工具栏按钮隐藏、显示相关栏目方法
        private bool isControlsbarActivated = true;
        private bool isToolsbarActivated = true;
        private void 控件栏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(isControlsbarActivated == true)
            {
                isControlsbarActivated = false;
                setControlsbar(false);
            }
            else
            {
                isControlsbarActivated = true;
                setControlsbar(true);
            }
        }

        private void 工具栏ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isToolsbarActivated == true)
            {
                isToolsbarActivated = false;
                setToolsbar(false);
            }
            else
            {
                isToolsbarActivated = true;
                setToolsbar(true);
            }
        }
        #endregion

        #region 改变控件栏所有控件状态：setControlsbar(bool status)
        private void setControlsbar(bool status)
        {
            toolStripButton1.Visible = status;
            toolStripButton2.Visible = status;
            toolStripButton3.Visible = status;
            toolStripButton4.Visible = status;
            toolStripButton5.Visible = status;
            toolStripButton6.Visible = status;
            toolStripButton7.Visible = status;
            toolStripButton8.Visible = status;
            toolStripButton9.Visible = status;
            toolStripButton10.Visible = status;
            toolStripButton24.Visible = status;
            toolStripButton25.Visible = status;
            toolStripLabel1.Visible = status;
            toolStripSplitButton1.Visible = status;
            toolStripSeparator23.Visible = status;
            toolStripSeparator24.Visible = status;
            toolStripSeparator15.Visible = status;
            toolStripSeparator16.Visible = status;
            toolStripSeparator18.Visible = status;
        }
        #endregion

        #region 改变工具栏所有控件状态setToolsbar(bool status)
        private void setToolsbar(bool status)
        {
            toolStripButton11.Visible = status;
            toolStripButton12.Visible = status;
            toolStripButton13.Visible = status;
            toolStripButton14.Visible = status;
            toolStripButton15.Visible = status;
            toolStripButton16.Visible = status;
            toolStripButton17.Visible = status;
            toolStripButton18.Visible = status;
            toolStripButton19.Visible = status;
            toolStripButton20.Visible = status;
            toolStripButton21.Visible = status;
            toolStripButton22.Visible = status;
            toolStripButton23.Visible = status;
            toolStripSeparator19.Visible = status;
            toolStripSeparator20.Visible = status;
            toolStripSeparator22.Visible = status;
        }
        #endregion

        #region 二进制读取、写入ImageDoc图像文档的实现
        private void 保存图像文档ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveasImageDocFile(GetCurrentImageDoc());
        }

        private void 打开图像文档ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.Filter = "图像文档文件(*.imgdoc)|*.imgdoc";
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string ImdPath = openFileDialog.FileName.ToString();
                    FileStream fileStream = new FileStream(ImdPath, FileMode.Open);
                    BinaryReader binaryReader = new BinaryReader(fileStream);
                    string ImageDocName = binaryReader.ReadString();

                    TabPage tp = new TabPage();
                    tp.Parent = TabPageManager;
                    tp.Text = ImageDocName;
                    tp.Show();
                    TabPageManager.SelectedTab = tp;
                    TabPageManager.Visible = true;

                    string Path = binaryReader.ReadString();
                    System.Drawing.Image Img = System.Drawing.Image.FromFile(Path);
                    ImageDocument ImageDoc = new ImageDocument(ImageDocName, tp.Size, Img);
                    ImageDoc.Ratio = (float)binaryReader.ReadDouble();
                    ImageDoc.Brightness = binaryReader.ReadInt32();
                    ImageDoc.Contrast = binaryReader.ReadInt32();

                    Console.WriteLine(ImageDoc.Brightness);
                    Console.WriteLine(ImageDoc.Contrast);

                    int ResolutionX = binaryReader.ReadInt32();
                    int ResolutionY = binaryReader.ReadInt32();

                    // 按像素点读取图像素材
                    Bitmap Map = new Bitmap(ResolutionX, ResolutionY);
                    for(int i = 0; i < ResolutionX; i ++)
                    {
                        for(int j = 0; j < ResolutionY; j ++)
                        {
                            int R = binaryReader.ReadInt32();
                            int G = binaryReader.ReadInt32(); 
                            int B = binaryReader.ReadInt32();
                            Map.SetPixel(i, j, Color.FromArgb(R, G, B));
                        }
                    }
                    ImageDoc.CurrentImage = Map;

                    ImageDoc.PaintBoard.Size = new Size((int)(Map.Width * ImageDoc.Ratio), (int)(Map.Height * ImageDoc.Ratio));

                    ImageDoc.TopLevel = false;
                    ImageDoc.Dock = DockStyle.Fill;
                    ImageDoc.Parent = tp;
                    ImageDoc.TabPageChild = tp;
                    ImageDoc.TabPageManager = this.TabPageManager;
                    ImageDoc.ImgPath = Path;

                    // 初始化撤回栈
                    ImageDoc.CtrlZ.Clear();
                    ImageDoc.CtrlZ.Push(ImageDoc.CurrentImage);

                    UpdateImageDoc(ImageDoc);

                    // 建立文档与下拉菜单元素的映射
                    ToolStripMenuItem newWnd = new ToolStripMenuItem();
                    ImageDoc.Show();
                    newWnd.Text = ImageDoc.Text;
                    ItemtoWnd.Add(newWnd, tp);
                    WndtoItem.Add(tp, newWnd);
                    newWnd.Click += new System.EventHandler(newWndClick);
                    窗体ToolStripMenuItem.DropDownItems.Add(newWnd);

                    ToolStripMenuItem newWnd_Button = new ToolStripMenuItem();
                    newWnd_Button.Text = ImageDoc.Text;
                    ItemtoWnd_Button.Add(newWnd_Button, tp);
                    WndtoItem_Button.Add(tp, newWnd_Button);
                    newWnd_Button.Click += new System.EventHandler(newWndClick_Button);
                    toolStripSplitButton1.DropDownItems.Add(newWnd_Button);

                    ImageDoc.ShowContextMenuStrip += new ShowContextMenuStripHandle(ShowContextDialog);
                    ImageDoc.HideContextMenuStrip += new HideContextMenuStripHandle(HideContextDialog);
                }
                catch(Exception)
                {
                    Interaction.Beep();
                    if (MessageBox.Show("无法打开图像文档，它可能在上一次保存时未被正常保存或已经损坏！", "无法打开图像文档", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                    {
                        return;
                    }
                }
            }
        }

        private void SaveasImageDocFile(ImageDocument o)
        {
            Interaction.Beep();
            if (MessageBox.Show("确定要保存为图像文档文件吗？", "保存", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
            {
                return;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            saveFileDialog.Filter = "图像文档文件(*.imgdoc)|*.imgdoc";
            saveFileDialog.FileName = o.Name.Substring(0, o.Name.Length - 4) + ".imgdoc";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string ImdPath = saveFileDialog.FileName.ToString();
                FileStream fileStream = new FileStream(ImdPath, FileMode.Create);
                BinaryWriter binaryWriter = new BinaryWriter(fileStream);
                binaryWriter.Write((string)o.Name);
                binaryWriter.Write((string)o.ImgPath);
                binaryWriter.Write((double)o.Ratio);
                binaryWriter.Write((int)o.Brightness);
                binaryWriter.Write((int)o.Contrast);
                binaryWriter.Write((int)o.CurrentImage.Width);
                binaryWriter.Write((int)o.CurrentImage.Height);
                for(int i = 0; i < o.CurrentImage.Width; i ++)
                {
                    for(int j = 0; j < o.CurrentImage.Height; j ++)
                    {
                        Color pixelColor = ((Bitmap)o.CurrentImage).GetPixel(i, j);
                        binaryWriter.Write((int)(pixelColor.R));
                        binaryWriter.Write((int)(pixelColor.G));
                        binaryWriter.Write((int)(pixelColor.B));
                    }
                }
                binaryWriter.Flush();
                binaryWriter.Close();
            }
        }

        private void toolStripButton24_Click(object sender, EventArgs e)
        {
            打开图像文档ToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton25_Click(object sender, EventArgs e)
        {
            保存图像文档ToolStripMenuItem_Click(sender, e);
        }
        #endregion

        #region 复制、粘贴图像的实现
        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetImage(GetCurrentImageDoc().CurrentImage);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            复制ToolStripMenuItem_Click(sender, e);
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(Clipboard.GetImage() == null)
            {
                return;
            }
            ImageDocument doc = GetCurrentImageDoc();
            Image newG = (Image)doc.CurrentImage.Clone();
            Graphics g = Graphics.FromImage(newG);
            g.DrawImage(Clipboard.GetImage(), 0, 0);
            doc.CurrentImage = (Image)newG.Clone();
            PushChanges(doc);
            UpdateImageDoc(doc);
            g.Dispose();
            newG.Dispose();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            粘贴ToolStripMenuItem_Click(sender, e);
        }
        #endregion

        #region 打印和打印预览功能实现
        private void 打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDialog = new PrintDialog();
            printDialog.Document = printDocument;
            if(printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }

        }

        private void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                ImageDocument doc = GetCurrentImageDoc();
                if (doc.CurrentImage != null)
                {
                    e.Graphics.DrawImage(doc.CurrentImage, e.Graphics.VisibleClipBounds);
                    e.HasMorePages = false;
                }
            }
            catch (Exception)
            {

            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            打印ToolStripMenuItem_Click(sender, e);
        }

        private void 打印预览ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = printDocument;
            if(printPreviewDialog.ShowDialog() == DialogResult.OK)
            {
                return;
            }
        }
        #endregion

        #region 图像大小调整功能与算法：ResizeImage(Image, Size)是任意比例缩放, ResizeBitmap(Image, Ratio)是等比例缩放
        private void 页面大小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocument doc = GetCurrentImageDoc();
            ResizeForm resizeForm = new ResizeForm(this.Location.X, this.Location.Y, this.Size, doc.CurrentImage.Width, doc.CurrentImage.Height);
            resizeForm.ShowDialog();
            if(resizeForm.DialogResult == DialogResult.OK)
            {
                int ResolutionX = resizeForm.ResolutionX;
                int ResolutionY = resizeForm.ResolutionY;
                doc.CurrentImage = (Image)ResizeImage((Image)doc.CurrentImage.Clone(), new Size(ResolutionX, ResolutionY)).Clone();
                doc.PaintBoard.Size = new Size(ResolutionX, ResolutionY);
                PushChanges(doc);
                UpdateImageDoc(doc);
            }
            else
            {
                return;
            }
        }

        private static Image ResizeImage(Image imgToResize, Size size)
        {
            //获取图片宽度
            int sourceWidth = imgToResize.Width;
            //获取图片高度
            int sourceHeight = imgToResize.Height;

            float nPercentW = 0;
            float nPercentH = 0;
            //计算宽度的缩放比例
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //计算高度的缩放比例
            nPercentH = ((float)size.Height / (float)sourceHeight);

            //期望的宽度
            int destWidth = (int)(sourceWidth * nPercentW);
            //期望的高度
            int destHeight = (int)(sourceHeight * nPercentH);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //绘制图像
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (Image)b;
        }
        #endregion

        #region 鼠标右键菜单栏所有功能实现，唤起需要依托ImageDoc的委托
        public void ShowContextDialog(object sender, MouseEventArgs e)
        {
            contextMenuStrip.Show(Control.MousePosition);
        }

        public void HideContextDialog(object sender, MouseEventArgs e)
        {
            contextMenuStrip.Hide();
        }

        private void 保存ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            保存ToolStripMenuItem_Click(sender, e);
        }

        private void 保存图像文档ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            保存图像文档ToolStripMenuItem_Click(sender, e);
        }

        private void 垂直翻转ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            垂直翻转ToolStripMenuItem_Click(sender, e);
        }

        private void 水平翻转ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            水平翻转ToolStripMenuItem_Click(sender, e);
        }

        private void 顺时针旋转90ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            顺时针旋转90ToolStripMenuItem_Click(sender, e);
        }

        private void 逆时针旋转90ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            逆时针旋转90ToolStripMenuItem_Click(sender, e);
        }

        private void 旋转180ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            旋转180ToolStripMenuItem_Click(sender, e);
        }

        private void 放大ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            放大ToolStripMenuItem_Click(sender, e);
        }

        private void 缩小ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            缩小ToolStripMenuItem_Click(sender, e);
        }

        private void 视图居中ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            视图居中ToolStripMenuItem_Click(sender, e);
        }

        private void 视图铺满ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            视图铺满ToolStripMenuItem_Click(sender, e);
        }

        private void 原大小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            图像原大小ToolStripMenuItem_Click(sender, e);
        }

        private void 适应窗口ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            适应窗口ToolStripMenuItem_Click(sender, e);
        }
        #endregion

        #region 图像裁剪
        private void 裁剪ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageDocument doc = GetCurrentImageDoc();
            CropForm cropForm = new CropForm(this.Location.X, this.Location.Y, this.Size, doc.CurrentImage);
            cropForm.ShowDialog();
            if (cropForm.DialogResult == DialogResult.OK)
            {
                Rectangle Rect = cropForm.Rect;
                doc.CurrentImage = CropImageDoc((Bitmap)doc.CurrentImage.Clone(), Rect);
                doc.PaintBoard.Size = new Size(doc.CurrentImage.Width, doc.CurrentImage.Height);
                UpdateImageDoc(doc);
                PushChanges(doc);
            }
            else
            {
                return;
            }
        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            裁剪ToolStripMenuItem_Click(sender, e);
        }

        public static Bitmap CropImageDoc(Bitmap src, Rectangle range)
        {
            return (Bitmap)src.Clone(range, System.Drawing.Imaging.PixelFormat.DontCare);
        }
        #endregion
    }
}