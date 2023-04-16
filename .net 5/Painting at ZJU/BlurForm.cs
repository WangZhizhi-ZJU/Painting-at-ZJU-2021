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
    public partial class BlurForm : Form
    {
        public int Radius = 0;
        public int Value = 0;

        public BlurForm(int FatherWndX, int FatherWndY, Size mainWndSize, bool Method)
        {
            InitializeComponent();
            pictureBox1.Visible = false;
            this.Location = new Point(FatherWndX + mainWndSize.Width / 2 - this.Size.Width / 2, FatherWndY + mainWndSize.Height / 2 - this.Size.Height / 2);
            if(Method == false)
            {
                trackBar2.Maximum = 20;
                label5.Text = "20";
                label1.Visible = label2.Visible = label3.Visible = trackBar1.Visible = false;
                label4.Location = new Point(31, 95);
                trackBar2.Location = new Point(25, 50);
                trackBar2.Size = new Size(360, 43);
                label5.Location = new Point(353, 75);
                label6.Location = new Point(31, 75);
                button1.Location = new Point(181, 94);
            }
            else
            {
                label7.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            label4.Text = "当前模糊半径：" + trackBar2.Value.ToString();
            this.Radius = trackBar2.Value;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            label3.Text = "当前模糊程度：" + trackBar1.Value.ToString();
            this.Value = trackBar1.Value;
        }

        private void BlurForm_MouseMove(object sender, MouseEventArgs e)
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
