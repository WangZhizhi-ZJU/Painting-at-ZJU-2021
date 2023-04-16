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
    public partial class ContrastForm : Form
    {
        public int Value = 0;

        public ContrastForm(int FatherWndX, int FatherWndY, Size mainWndSize, int CurrentContrast)
        {
            InitializeComponent();
            pictureBox1.Visible = false;
            this.Location = new Point(FatherWndX + mainWndSize.Width / 2 - this.Size.Width / 2, FatherWndY + mainWndSize.Height / 2 - this.Size.Height / 2);
            trackBar1.Value = CurrentContrast;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            label3.Text = "当前对比度：" + (trackBar1.Value / 2).ToString();
            this.Value = trackBar1.Value;
        }
        private void ContrastForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (364 <= e.X && 409 >= e.X && 1 <= e.Y && e.Y <= 26)
            {
                pictureBox1.Visible = true;
                pictureBox1.BackgroundImage = Painting_at_ZJU.Properties.Resources.QuitButton01;
            }
            else
            {
                pictureBox1.Visible = false;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1.BackgroundImage = Painting_at_ZJU.Properties.Resources.QuitButton02;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.BackgroundImage = Painting_at_ZJU.Properties.Resources.QuitButton01;
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
