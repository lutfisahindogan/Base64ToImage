using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Base64ToImage
{
    public partial class Form1 : Form
    {
        public int CropPixel { get; set; }
        public Form1()
        {
            InitializeComponent();
            CropPixel = 0;
            label1.Text = "B\nA\nS\nE\n\n6\n4\n\nC\no\nd\ne\n";
            button1.Text = "C\nO\nN\nV\nE\nR\nT\n";
            button2.Text = "C\nL\nE\nA\nR\n";
            button3.Text = "C\nR\nO\nP\n";
            button1.Click += Button1_Click;
            button2.Click += Button2_Click;
            button3.Click += Button3_Click;
            textBox1.MaxLength = Int32.MaxValue;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            CropPixel += 5;
            string @base = null;
            @base = textBox1.Text.Replace("\r", "");
            Bitmap bmp = newBitmapFromBase64(@base);


            Rectangle cropRect = new Rectangle(0, 0, bmp.Width, bmp.Height - CropPixel);
            Bitmap src = bmp;
            Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(src, new Rectangle(0, 0, target.Width, target.Height),
                                 cropRect,
                                 GraphicsUnit.Pixel);
            }
            bmp = target;
            pictureBox1.Image = bmp;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            CropPixel = 0;
            string @base = null;
            @base = textBox1.Text.Replace("\r", "");
            Bitmap bmp = newBitmapFromBase64(@base);
            pictureBox1.Image = bmp;
        }
        public Bitmap newBitmapFromBase64(string base64)
        {
            byte[] array = Convert.FromBase64String(base64);
            Bitmap result;
            using (MemoryStream memoryStream = new MemoryStream(array, 0, array.Length))
            {
                Bitmap bitmap = new Bitmap(memoryStream, false);
                result = bitmap;
            }
            return result;
        }
    }
}
