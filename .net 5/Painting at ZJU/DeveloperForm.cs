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
    public partial class DeveloperForm: Form
    {
        public DeveloperForm(int FatherWndX, int FatherWndY, Size mainWndSize)
        {
            Opacity = 0;
            InitializeComponent();
            this.Location = new Point(FatherWndX + mainWndSize.Width / 2 - this.Size.Width / 2, FatherWndY + mainWndSize.Height / 2 - this.Size.Height / 2);
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

        private void DeveloperForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Opacity -= 0.1;
            if(Opacity <= 0)
            {
                timer2.Enabled = false;
                timer2 = null;
                this.Dispose();
            }
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = Painting_at_ZJU.Properties.Resources.InfoPageButton02;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1.BackgroundImage = Painting_at_ZJU.Properties.Resources.InfoPageButton03;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.BackgroundImage = Painting_at_ZJU.Properties.Resources.InfoPageButton02;
            timer2.Start();
        }

        private void DeveloperForm_MouseMove(object sender, MouseEventArgs e)
        {
            if(pictureBox1.BackgroundImage != Painting_at_ZJU.Properties.Resources.InfoPageButton01)
            {
                pictureBox1.BackgroundImage = Painting_at_ZJU.Properties.Resources.InfoPageButton01;
            }
        }
    }
}
