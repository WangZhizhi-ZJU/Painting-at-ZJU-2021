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
    public partial class ThresholdForm : Form
    {
        public int Value = 0;
        public ThresholdForm(int FatherWndX, int FatherWndY, Size mainWndSize)
        {
            InitializeComponent();
            pictureBox1.Visible = false;
            this.Location = new Point(FatherWndX + mainWndSize.Width / 2 - this.Size.Width / 2, FatherWndY + mainWndSize.Height / 2 - this.Size.Height / 2);
        }

        private void trackBar1_DragDrop(object sender, DragEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            label3.Text = "当前阈值：" + trackBar1.Value.ToString();
            this.Value = trackBar1.Value;
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

        private void ThresholdForm_MouseMove(object sender, MouseEventArgs e)
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

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }
    }
}
