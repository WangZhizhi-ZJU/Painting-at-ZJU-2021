
namespace Painting_at_ZJU
{
    partial class ImageDocument
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PaintBoard = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PaintBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // PaintBoard
            // 
            this.PaintBoard.Location = new System.Drawing.Point(-1, -2);
            this.PaintBoard.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.PaintBoard.Name = "PaintBoard";
            this.PaintBoard.Size = new System.Drawing.Size(707, 346);
            this.PaintBoard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PaintBoard.TabIndex = 0;
            this.PaintBoard.TabStop = false;
            this.PaintBoard.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PaintBoard_MouseDown);
            // 
            // ImageDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(692, 335);
            this.Controls.Add(this.PaintBoard);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ImageDocument";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ImageDocument";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImageDocument_FormClosing);
            this.Load += new System.EventHandler(this.ImageDocument_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ImageDocument_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.PaintBoard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox PaintBoard;
    }
}