using Microsoft.VisualBasic;
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
    public partial class ResizeForm : Form
    {
        private bool isText1Changing = false;
        private bool isText2Changing = false;

        private int OriginalX = 0;
        private int OriginalY = 0;

        public int ResolutionX = 0;
        public int ResolutionY = 0;

        public ResizeForm(int FatherWndX, int FatherWndY, Size mainWndSize, int DocResolutionX, int DocResolutionY)
        {
            InitializeComponent();
            this.Location = new Point(FatherWndX + mainWndSize.Width / 2 - this.Width / 2, FatherWndY + mainWndSize.Height / 2 - this.Height / 2);
            this.ResolutionX = this.OriginalX = DocResolutionX;
            this.ResolutionY = this.OriginalY = DocResolutionY;
            this.textBox1.Text = this.ResolutionX.ToString();
            this.textBox2.Text = this.ResolutionY.ToString();
            comboBox1.SelectedItem = "像素";
            comboBox2.SelectedItem = "像素";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (comboBox1.Text != "像素" && comboBox1.Text != "百分比")
            {
                comboBox1.Text = "像素";
            }
            if (comboBox2.Text != "像素" && comboBox2.Text != "百分比")
            {
                comboBox2.Text = "像素";
            }

            try
            {
                int trail = Convert.ToInt32(textBox1.Text);
            }
            catch (Exception)
            {
                Interaction.Beep();
                MessageBox.Show("输入的值中含有无效字符！", "不合法的值", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if ((string)comboBox1.SelectedItem == "像素")
                {
                    textBox1.Text = OriginalX.ToString();
                }
                else
                {
                    textBox1.Text = "100";
                }
            }

            try
            {
                int trail = Convert.ToInt32(textBox2.Text);
            }
            catch (Exception)
            {
                Interaction.Beep();
                MessageBox.Show("输入的值中含有无效字符！", "不合法的值", MessageBoxButtons.OK, MessageBoxIcon.Error);            
                if ((string)comboBox2.SelectedItem == "像素")
                {
                    textBox2.Text = OriginalY.ToString();
                }
                else
                {
                    textBox2.Text = "100";
                }
            }

            if (textBox1.Text == "0")
            {
                textBox1.Text = "1";
            }

            if (textBox2.Text == "0")
            {
                textBox2.Text = "1";
            }

            if (textBox1.Text != "")
            {
                if ((string)comboBox1.SelectedItem == "像素")
                {
                    this.ResolutionX = Convert.ToInt32(textBox1.Text);
                }
                else
                {
                    this.ResolutionX = Convert.ToInt32(textBox1.Text) * this.OriginalX / 100;
                }
            }

            if (textBox2.Text != "")
            {
                if ((string)comboBox2.SelectedItem == "像素")
                {
                    this.ResolutionY = Convert.ToInt32(textBox2.Text);
                }
                else
                {
                    this.ResolutionY = Convert.ToInt32(textBox2.Text) * this.OriginalY / 100;
                }
            }
        }

        private void ResizeForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
            comboBox1.SelectedItem = "像素";
            comboBox2.SelectedItem = "像素";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || isText2Changing == true)
            {
                return;
            }
            isText1Changing = true;

            int Value = 0;
            try
            {
                Value = Convert.ToInt32(textBox1.Text);
            }
            catch (Exception)
            {
                isText1Changing = false;
                return;
            }

            if ((string)comboBox1.SelectedItem == "像素")
            {
                if (Value <= 0 || Value > 10 * OriginalX)
                {
                    Interaction.Beep();
                    if (MessageBox.Show("像素值必须介于1至" + (10 * OriginalX).ToString() + "之间！", "不合法的值", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                    {
                        textBox1.Text = OriginalX.ToString();
                    }
                }
                else
                {
                    if (checkBox1.Checked == true)
                    {
                        ResolutionY = Math.Max(OriginalY * Convert.ToInt32(textBox1.Text) / OriginalX, 1);
                        if ((string)comboBox2.SelectedItem == "像素")
                        {
                            textBox2.Text = ResolutionY.ToString();
                        }
                        else
                        {
                            textBox2.Text = (ResolutionY * 100 / OriginalY).ToString();
                        }
                    }
                }
            }
            else
            {
                if (Value <= 0 || Value > 1000)
                {
                    Interaction.Beep();
                    if (MessageBox.Show("百分比值必须介于1至1000之间！", "不合法的值", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                    {
                        textBox1.Text = "100";
                    }
                }
                else
                {
                    if (checkBox1.Checked == true)
                    {
                        ResolutionY = Math.Max(OriginalY * Convert.ToInt32(textBox1.Text) / 100, 1);
                        if ((string)comboBox2.SelectedItem == "百分比")
                        {
                            textBox2.Text = textBox1.Text;
                        }
                        else
                        {
                            textBox2.Text = ResolutionY.ToString();
                        }
                    }
                }
            }
            isText1Changing = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || isText1Changing == true)
            {
                return;
            }
            isText2Changing = true;

            int Value = 0;
            try
            {
                Value = Convert.ToInt32(textBox2.Text);
            }
            catch (Exception)
            {
                isText2Changing = false;
                return;
            }

            if ((string)comboBox2.SelectedItem == "像素")
            {
                if (Value <= 0 || Value > 10 * OriginalY)
                {
                    Interaction.Beep();
                    if (MessageBox.Show("像素值必须介于1至" + (10 * OriginalY).ToString() + "之间！", "不合法的值", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                    {
                        textBox2.Text = OriginalY.ToString();
                    }
                }
                else
                {
                    if (checkBox1.Checked == true)
                    {
                        ResolutionX = Math.Max(OriginalX * Convert.ToInt32(textBox2.Text) / OriginalY, 1);
                        if ((string)comboBox1.SelectedItem == "像素")
                        {
                            textBox1.Text = ResolutionX.ToString();
                        }
                        else
                        {
                            textBox1.Text = (ResolutionX * 100 / OriginalX).ToString();
                        }
                    }
                }
            }
            else
            {
                if (Value <= 0 || Value > 1000)
                {
                    Interaction.Beep();
                    if (MessageBox.Show("百分比值必须介于1至1000之间！", "不合法的值", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                    {
                        textBox2.Text = "100";
                    }
                }
                else
                {
                    if (checkBox1.Checked == true)
                    {
                        ResolutionX = Math.Max(OriginalX * Convert.ToInt32(textBox2.Text) / 100, 1);
                        if ((string)comboBox1.SelectedItem == "百分比")
                        {
                            textBox1.Text = textBox2.Text;
                        }
                        else
                        {
                            textBox1.Text = ResolutionX.ToString();
                        }
                    }
                }
            }
            isText2Changing = false;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            try
            {
                int trail = Convert.ToInt32(textBox1.Text);
            }
            catch (Exception)
            {
                Interaction.Beep();
                MessageBox.Show("输入的值中含有无效字符！", "不合法的值", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if ((string)comboBox1.SelectedItem == "像素")
                {
                    textBox1.Text = OriginalX.ToString();
                }
                else
                {
                    textBox1.Text = "100";
                }
            }

            if (textBox1.Text == "")
            {
                if ((string)comboBox1.SelectedItem == "像素")
                {
                    textBox1.Text = OriginalX.ToString();
                }
                else
                {
                    textBox1.Text = "100";
                }
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            try
            {
                int trail = Convert.ToInt32(textBox2.Text);
            }
            catch (Exception)
            {
                Interaction.Beep();
                MessageBox.Show("输入的值中含有无效字符！", "不合法的值", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if ((string)comboBox2.SelectedItem == "像素")
                {
                    textBox2.Text = OriginalY.ToString();
                }
                else
                {
                    textBox2.Text = "100";
                }
            }

            if (textBox2.Text == "")
            {
                if ((string)comboBox2.SelectedItem == "像素")
                {
                    textBox2.Text = OriginalY.ToString();
                }
                else
                {
                    textBox2.Text = "100";
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }  
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8)
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }
    }
}
