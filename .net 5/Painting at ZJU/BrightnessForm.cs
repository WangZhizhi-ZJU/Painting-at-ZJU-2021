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
    public partial class BrightnessForm : Form
    {
        public int Value = 0;

        public BrightnessForm(int FatherWndX, int FatherWndY, Size mainWndSize, int CurrentBrightness)
        {
            InitializeComponent();
            pictureBox1.Visible = false;
            this.Location = new Point(FatherWndX + mainWndSize.Width / 2 - this.Size.Width / 2, FatherWndY + mainWndSize.Height / 2 - this.Size.Height / 2);
            trackBar1.Value = CurrentBrightness;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            label3.Text = "当前亮度：" + (trackBar1.Value / 2).ToString();
            this.Value = trackBar1.Value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1.BackgroundImage = Painting_at_ZJU.Properties.Resources.QuitButton02;
        }

        private void BrightnessForm_MouseMove(object sender, MouseEventArgs e)
        {
            if(364 <= e.X && 409 >= e.X && 1 <= e.Y && e.Y <= 26)
            {
                pictureBox1.Visible = true;
                pictureBox1.BackgroundImage = Painting_at_ZJU.Properties.Resources.QuitButton01;
            }
            else
            {
                pictureBox1.Visible = false;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.BackgroundImage = Painting_at_ZJU.Properties.Resources.QuitButton01;
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
