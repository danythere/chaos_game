using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        Bitmap image;
        public Form1()
        {
            InitializeComponent();
        }
        float[] resX;
        float[] resY;
        double zoom = 20;
        int n;
        public void Draw(float w, float h, Pen pen, float[] A1, float[] A2, float[] A3, float[] A4, float[] b1, float[] b2, float[] b3, float[] b4, int n)
        {
            image=new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = image;
            Graphics g = Graphics.FromImage(image);
            g.Clear(Color.FromArgb(255, 255, 255, 255));
            double moveX = 0.1*(hScrollBar1.Value - 50), moveY = 0.1* (vScrollBar1.Value-50);

            resX = new float[n];
            resY = new float[n];
            resX[0] = 1;
            resX[1] = 1;
            Random rnd = new Random();

            for (int i = 1; i < n; i++)
            {
                int p = rnd.Next(0, 100);
                if (p == 1)
                {
                    resX[i] = resX[i - 1] * A1[0] + resY[i - 1] * A1[1] + b1[0];
                    resY[i] = resX[i - 1] * A1[2] + resY[i - 1] * A1[3] + b1[1];
                }
                else if (p > 1 && p <= 86)
                {
                    resX[i] = resX[i - 1] * A2[0] + resY[i - 1] * A2[1] + b2[0];
                    resY[i] = resX[i - 1] * A2[2] + resY[i - 1] * A2[3] + b2[1];
                }
                else if (p > 86 && p <= 93)
                {
                    resX[i] = resX[i - 1] * A3[0] + resY[i - 1] * A3[1] + b3[0];
                    resY[i] = resX[i - 1] * A3[2] + resY[i - 1] * A3[3] + b3[1];
                }
                else
                {
                    resX[i] = resX[i - 1] * A4[0] + resY[i - 1] * A4[1] + b1[0];
                    resY[i] = resX[i - 1] * A4[2] + resY[i - 1] * A4[3] + b1[1];
                }

            }
            
            for (int i = 1; i < n; i++)
            {
                pen.Color = Color.FromArgb(255, 100, (i * 100) % 255, (i * 100) % 255);
                g.DrawRectangle(pen, ((float)resX[i] + (float)moveX) *(float)zoom+w/2, ((float)resY[i] +(float)moveY)*(float)zoom+h/2, 1, 1);

            }


        }
        private void button1_Click(object sender, EventArgs e)
        {

            Pen myPen = new Pen(Color.Black, 1);
           
            n = int.Parse(textBox27.Text);
            float[] A1 = new float[4] { float.Parse(textBox1.Text), float.Parse(textBox2.Text), float.Parse(textBox4.Text), float.Parse(textBox3.Text) };
            float[] A2 = new float[4] { float.Parse(textBox8.Text), float.Parse(textBox7.Text), float.Parse(textBox6.Text), float.Parse(textBox5.Text) };
            float[] A3 = new float[4] { float.Parse(textBox12.Text), float.Parse(textBox11.Text), float.Parse(textBox10.Text), float.Parse(textBox9.Text) };
            float[] A4 = new float[4] { float.Parse(textBox16.Text), float.Parse(textBox15.Text), float.Parse(textBox14.Text), float.Parse(textBox13.Text) };
            float[] b1 = new float[2] { float.Parse(textBox18.Text), float.Parse(textBox17.Text) };
            float[] b2 = new float[2] { float.Parse(textBox20.Text), float.Parse(textBox19.Text) };
            float[] b3 = new float[2] { float.Parse(textBox22.Text), float.Parse(textBox21.Text) };
            float[] b4 = new float[2] { float.Parse(textBox24.Text), float.Parse(textBox23.Text) };

            Draw(pictureBox1.Width, pictureBox1.Height, myPen, A1, A2, A3, A4, b1, b2, b3, b4, n);
        }

        private void button2_Click(object sender, EventArgs e)
        {



            if (pictureBox1.Image != null) //если в pictureBox есть изображение
            {
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.OverwritePrompt = true;
                savedialog.CheckPathExists = true;
                savedialog.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|Image Files(*.GIF)|*.GIF|Image Files(*.PNG)|*.PNG|All files (*.*)|*.*";
                savedialog.ShowHelp = true;
                if (savedialog.ShowDialog() == DialogResult.OK) //если в диалоговом окне нажата кнопка "ОК"
                {
                    try
                    {
                        image.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch
                    {
                        MessageBox.Show("Невозможно сохранить изображение", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


            }

            }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {

            zoom *= 1.5;
            Bitmap image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Pen pen = new Pen(Color.Black, 1);
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            g.Clear(Color.FromArgb(255, 255, 255, 255));
            double moveX = 0.1 * (hScrollBar1.Value - 50), moveY = 0.1 * (vScrollBar1.Value - 50);
            for (int i = 1; i < n; i++)
            {
                pen.Color = Color.FromArgb(255, 100, (i * 100) % 255, (i * 100) % 255);
                g.DrawRectangle(pen, ((float)resX[i] + (float)moveX) * (float)zoom + pictureBox1.Width / 2, ((float)resY[i] + (float)moveY) * (float)zoom + pictureBox1.Height / 2, 1, 1);

            }
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            Bitmap image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Pen pen = new Pen(Color.Black, 1);
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            g.Clear(Color.FromArgb(255, 255, 255, 255));
            double moveX = 0.1 * (hScrollBar1.Value - 50), moveY = 0.1 * (vScrollBar1.Value - 50);
            for (int i = 1; i < n; i++)
            {
                pen.Color = Color.FromArgb(255, 100, (i * 100) % 255, (i * 100) % 255);
                g.DrawRectangle(pen, ((float)resX[i] + (float)moveX) * (float)zoom + pictureBox1.Width / 2, ((float)resY[i] + (float)moveY) * (float)zoom + pictureBox1.Height / 2, 1, 1);

            }
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            Bitmap image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Pen pen = new Pen(Color.Black, 1);
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            g.Clear(Color.FromArgb(255, 255, 255, 255));
            double moveX = 0.1 * (hScrollBar1.Value - 50), moveY = 0.1 * (vScrollBar1.Value - 50);
            for (int i = 1; i < n; i++)
            {
                pen.Color = Color.FromArgb(255, 100, (i * 100) % 255, (i * 100) % 255);
                g.DrawRectangle(pen, ((float)resX[i] + (float)moveX) * (float)zoom + pictureBox1.Width / 2, ((float)resY[i] + (float)moveY) * (float)zoom + pictureBox1.Height / 2, 1, 1);

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            zoom /= 1.5;
            Bitmap image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Pen pen = new Pen(Color.Black, 1);
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            g.Clear(Color.FromArgb(255, 255, 255, 255));
            double moveX = 0.1 * (hScrollBar1.Value - 50), moveY = 0.1 * (vScrollBar1.Value - 50);
            for (int i = 1; i < n; i++)
            {
                pen.Color = Color.FromArgb(255, 100, (i * 100) % 255, (i * 100) % 255);
                g.DrawRectangle(pen, ((float)resX[i] + (float)moveX) * (float)zoom + pictureBox1.Width / 2, ((float)resY[i] + (float)moveY) * (float)zoom + pictureBox1.Height / 2, 1, 1);

            }
        }
    }
}
