using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Painting_at_ZJU 
{
    public class GaussianBlur 
    {
        /// <summary>
        /// 模糊半径
        /// </summary>
        public int BlurRadius { get; private set; }
        private Bitmap SourceImage { get; set; }
        private List<double> BlurArray { get; set; }
        private int MaxWidth { get; set; }
        private int MaxHeight { get; set; }

        public GaussianBlur(int blurRadius) 
        {
            BlurArray = new List<double>();
            this.BlurRadius = blurRadius;
            this.SetBlurArray();
        }

        private void SetBlurArray() 
        {
            int i = 0;
            double sum = 0;
            for (i = this.BlurRadius * -1; i <= this.BlurRadius; i++) 
            {
                double d = GetWeighing(i);
                sum += d;
                BlurArray.Add(d);
            }
            if (sum >= 1)
            {
                throw new Exception("error");
            } 
            for (i = 0; i < BlurArray.Count; i++)
            {
                BlurArray[i] = BlurArray[i] / sum;
            }

        }

        /// <summary>
        /// 设置需要模糊的图片
        /// </summary>
        /// <param name="img"></param>
        public void SetSourceImage(Image img) 
        {
            this.SourceImage = (Bitmap)img;
            this.MaxWidth = this.SourceImage.Width - 1;
            this.MaxHeight = this.SourceImage.Height - 1;
        }

        /// <summary>
        /// 获取模糊之后的图片
        /// </summary>
        /// <returns></returns>
        public Bitmap GetBlurImage() 
        {
            if (this.SourceImage == null)
                return null;
            Bitmap newImage = new Bitmap(SourceImage.Width, SourceImage.Height);
            for (int y = 0; y < this.SourceImage.Height; y++) 
            {
                for (int x = 0; x < this.SourceImage.Width; x++) 
                {
                    var nC = GetBlurColor(x, y, true);
                    newImage.SetPixel(x, y, nC);
                }
            }
            //return newImage;
            this.SourceImage = newImage;
            Bitmap newImageX = new Bitmap(SourceImage.Width, SourceImage.Height);
            for (int x = 0; x < this.SourceImage.Width; x++) 
            {
                for (int y = 0; y < this.SourceImage.Height; y++) 
                {
                    var nC = GetBlurColor(x, y, false);
                    newImageX.SetPixel(x, y, nC);
                }
            }
            return newImageX;
        }

        /// <summary>
        /// 获取高斯模糊的颜色值
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private Color GetBlurColor(int x, int y, bool h) 
        {
            int index = 0;
            double r = 0, g = 0, b = 0, a = 0;
            var orgColor = this.SourceImage.GetPixel(x, y);
            bool countalpha = orgColor.A != 255;
            for (int i = this.BlurRadius * -1; i <= this.BlurRadius; i++) 
            {
                Color color;
                if (h)
                    color = this.GetDefautColor(x, y + i);
                else
                    color = this.GetDefautColor(x + i, y);
                if (color.A == 0) 
                {
                    color = Color.FromArgb(0, 255, 255, 255);
                    countalpha = true;
                }
                var weighValue = BlurArray[index];
                //if (countalpha)
                a += color.A * weighValue;
                //Console.Write(string.Format("({0},{1})", l, t));
                r += color.R * weighValue;
                g += color.G * weighValue;
                b += color.B * weighValue;
                index++;
            }
            a *= 1.3;
            if (!countalpha)
                a = 255;
            else
                a = Math.Min(255, a);

            return Color.FromArgb((byte)a, (byte)r, (byte)g, (byte)b);
        }

        private Color GetDefautColor(int x, int y) 
        {
            if (x < 0 && y < 0)
                return this.SourceImage.GetPixel(0, 0);
            else if (x < 0)
                return this.SourceImage.GetPixel(0, Math.Min(MaxHeight, y));
            else if (y < 0)
                return this.SourceImage.GetPixel(Math.Min(MaxWidth, x), 0);
            else
                return this.SourceImage.GetPixel(Math.Min(MaxWidth, x), Math.Min(MaxHeight, y));
        }


        private int Edge(int i, int x, int w) 
        {
            int i_k = x + i;
            if (i_k < 0)
                i_k = -x;
            else if (i_k >= w)
                i_k = w - 1 - x;
            else
                i_k = i;
            return i_k;
        }

        private double GetWeighing(int x) 
        {
            double q = (this.BlurRadius * 2 + 1) / 2;
            return 1 / (q * Math.Sqrt(2.0 * Math.PI)) * Math.Exp(-Math.Pow(x, 2) / (2 * Math.Pow(q, 2)));
        }
    }
}
