namespace Painting_at_ZJU
{
    partial class StatisticsForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatisticsForm));
            this.PicR = new System.Windows.Forms.PictureBox();
            this.Thumb = new System.Windows.Forms.PictureBox();
            this.LblStatus = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PicR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Thumb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // PicR
            // 
            this.PicR.Location = new System.Drawing.Point(440, 43);
            this.PicR.Name = "PicR";
            this.PicR.Size = new System.Drawing.Size(438, 425);
            this.PicR.TabIndex = 2;
            this.PicR.TabStop = false;
            this.PicR.Paint += new System.Windows.Forms.PaintEventHandler(this.PicR_Paint);
            // 
            // Thumb
            // 
            this.Thumb.Location = new System.Drawing.Point(14, 43);
            this.Thumb.Name = "Thumb";
            this.Thumb.Size = new System.Drawing.Size(411, 425);
            this.Thumb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Thumb.TabIndex = 3;
            this.Thumb.TabStop = false;
            // 
            // LblStatus
            // 
            this.LblStatus.AutoSize = true;
            this.LblStatus.Location = new System.Drawing.Point(200, 17);
            this.LblStatus.Name = "LblStatus";
            this.LblStatus.Size = new System.Drawing.Size(0, 12);
            this.LblStatus.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::Painting_at_ZJU.Properties.Resources.QuitButton012;
            this.pictureBox1.Location = new System.Drawing.Point(843, 1);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(45, 25);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Interval = 3;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 3;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9.75F);
            this.label1.Location = new System.Drawing.Point(37, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 19);
            this.label1.TabIndex = 6;
            this.label1.Text = "颜色统计分析";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::Painting_at_ZJU.Properties.Resources.SmallIcon;
            this.pictureBox2.Location = new System.Drawing.Point(14, 7);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(22, 22);
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // StatisticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::Painting_at_ZJU.Properties.Resources.StatisticsBkg;
            this.ClientSize = new System.Drawing.Size(890, 480);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.LblStatus);
            this.Controls.Add(this.Thumb);
            this.Controls.Add(this.PicR);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StatisticsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "图像分析";
            this.Load += new System.EventHandler(this.FrmTest_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.StatisticsForm_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.StatisticsForm_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.PicR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Thumb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox PicR;
        private System.Windows.Forms.PictureBox Thumb;
        private System.Windows.Forms.Label LblStatus;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}

