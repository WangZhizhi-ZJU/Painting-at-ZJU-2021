using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Painting_at_ZJU
{
    // 求是画板欢迎界面
    class WelcomeForm: Form
    {
        private System.ComponentModel.IContainer components;
        private Timer FadeIn;       // 渐现计时器
        private Timer Wait;         // 滞留计时器
        private Timer FadeOut;      // 渐隐计时器

        public WelcomeForm()
        {
            // 先将欢迎界面的透明度设置为0
            this.Opacity = 0;
            InitializeComponent();
            // 开始渐现
            FadeIn.Start();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WelcomeForm));
            this.FadeIn = new System.Windows.Forms.Timer(this.components);
            this.Wait = new System.Windows.Forms.Timer(this.components);
            this.FadeOut = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // FadeIn
            // 
            this.FadeIn.Interval = 3;
            this.FadeIn.Tick += new System.EventHandler(this.FadeIn_Tick);
            // 
            // Wait
            // 
            this.Wait.Interval = 3000;
            this.Wait.Tick += new System.EventHandler(this.Wait_Tick);
            // 
            // FadeOut
            // 
            this.FadeOut.Interval = 3;
            this.FadeOut.Tick += new System.EventHandler(this.FadeOut_Tick);
            // 
            // WelcomeForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::Painting_at_ZJU.Properties.Resources.Welcome;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WelcomeForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }

        private void FadeIn_Tick(object sender, EventArgs e)
        {
            this.Opacity += 0.05;
            // 渐现结束
            if(this.Opacity >= 1)
            {
                FadeIn.Enabled = false;
                // 开始滞留
                Wait.Start();
                FadeIn.Dispose();
            }
        }

        private void Wait_Tick(object sender, EventArgs e)
        {
            Wait.Enabled = false;
            // 开始渐隐
            FadeOut.Start();
            Wait.Dispose();
        }

        private void FadeOut_Tick(object sender, EventArgs e)
        {
            this.Opacity -= 0.05;
            // 渐隐结束
            if (this.Opacity <= 0)
            {
                FadeOut.Enabled = false;
                FadeOut.Dispose();
                // 退出还原界面并联系主窗体
                this.DialogResult = DialogResult.OK;
                this.Dispose();
            }
        }
    }
}
