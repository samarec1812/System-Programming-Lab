using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        bool firstPl = true,  fl3 = true;
        int score1 = 0, score2 = 0;
        int[,] mas = new int[5,5];
        public Form1()
        {
            InitializeComponent();
            RefreshMas(ref mas);
        }

        private void button1_Paint(object sender, PaintEventArgs e)
        {
            Draw(e);
        }

        private void button2_Paint(object sender, PaintEventArgs e)
        {
            Draw(e);
        }

        private void button3_Paint(object sender, PaintEventArgs e)
        {
            Draw(e);
        }

        private void button4_Paint(object sender, PaintEventArgs e)
        {
            Draw(e);
        }

        
        private void Form1_Paint(object sender, PaintEventArgs e)
        { 
             e.Graphics.DrawEllipse(new Pen(Color.Blue, 3), new Rectangle(320, 20, 50, 50));
             e.Graphics.DrawEllipse(new Pen(Color.Yellow, 3), new Rectangle(350, 40, 50, 50));
             e.Graphics.DrawEllipse(new Pen(Color.Black, 3), new Rectangle(380, 20, 50, 50));
             e.Graphics.DrawEllipse(new Pen(Color.Green, 3), new Rectangle(410, 40, 50, 50));
             e.Graphics.DrawEllipse(new Pen(Color.Red, 3), new Rectangle(440, 20, 50, 50));

             e.Graphics.DrawLine(new Pen(Color.Blue, 3), 320 + 50, 20 + 19, 320 + 50, 20 + 25);
             e.Graphics.DrawLine(new Pen(Color.Black, 3), 340 + 90, 20 + 19, 340 + 90, 20 + 25);
             e.Graphics.DrawLine(new Pen(Color.Green, 3), 360 + 79, 20 + 20, 360 + 83, 20 + 20);
             e.Graphics.DrawLine(new Pen(Color.Yellow, 3), 340 + 79 - 41, 20 + 20, 340 + 83 - 40, 20 + 20);  
        }
        
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

                Pen p = new Pen(Color.Black, 1);
                e.Graphics.FillRectangle(Brushes.AntiqueWhite, new Rectangle(1, 1, 255, 255));
                for (int i = 0; i < 6; ++i)
                {
                    e.Graphics.DrawLine(p, 50 * i, 0, 50 * i, 254);
                    e.Graphics.DrawLine(p, 0, 50 * i, 254, 50 * i);
                }
                fl3 = false;          
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {

            int x = e.X / 51;
            int y = e.Y / 51;
            Graphics dc = pictureBox1.CreateGraphics();
            Rectangle rec = new Rectangle(x * 50 , y * 50 , 50, 50);
            if (firstPl)
            {
                var br = new LinearGradientBrush(rec, Color.FromArgb(128,200,50,100), Color.Violet, LinearGradientMode.Horizontal);
                Image img = Image.FromFile("C:\\Users\\ПК\\Downloads\\ico.bmp");
                var PicBr = new TextureBrush(img);//C:\Users\ПК\Downloads
                dc.FillRectangle(br, rec);
                dc.FillRectangle(PicBr, rec);
               
                mas[x, y] = 1;
            }
            else
            {
                var br = new LinearGradientBrush(rec, Color.DeepPink, Color.Orange, LinearGradientMode.Vertical);
                Image img = Image.FromFile("C:\\Users\\ПК\\Downloads\\ico.bmp");
                var PicBr = new TextureBrush(img);
                dc.FillRectangle(br, rec);
                dc.FillRectangle(PicBr, rec);
                
                mas[x, y] = 2;
            }

            var winner = Winner();
            if (winner != 0)
            {
                if (winner == 1)
                    ++score1;
                else
                    ++score2;
                label1.Text = "Счет " + Convert.ToString(score1) + " : " + Convert.ToString(score2);
              
                label2.Text = "Победил " + (winner == 1 ? "первый " : "второй ") + "игрок.";
                Refresh();
                RefreshMas(ref mas);
                firstPl = true;
            }
            else
                if (Full(mas))
            {
                label2.Text = "Ничья.";
                Refresh();
                RefreshMas(ref mas);
                firstPl = true;
            }
            else
                firstPl = !firstPl;
        }
        
        private int Winner()
        {
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 2; j++)
                    if (mas[i, j] > 0)
                    {
                        for (int k = j + 1; k < 5; k++)
                        {
                            if (mas[i, k] != mas[i, j])
                                break;
                            else
                            if (k - j == 3)
                                if (mas[i, j] == 1)
                                    return 1;
                                else
                                    return 2;
                        }
                    }
            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 5; j++)
                    if (mas[i, j] > 0)
                    {
                        for (int k = i + 1; k < 5; k++)
                        {
                            if (mas[k, j] != mas[i, j])
                                break;
                            else
                            if (k - i == 3)
                                if (mas[i, j] == 1)
                                    return 1;
                                else
                                    return 2;
                        }
                    }


            return 0;  
        }   

        static void Draw(PaintEventArgs e)
        {
           
            e.Graphics.FillRectangle(new SolidBrush(Color.LightCoral), 1, 1, 100, 50);
            e.Graphics.FillRectangle(new SolidBrush(Color.Gold), 3, 3, 50, 50);
            e.Graphics.FillEllipse(new SolidBrush(Color.Aquamarine), 44, 0, 90, 45);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //button1_Paint(sender, e); 
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        public static void RefreshMas(ref int[,] mas)
        {
            for (int i = 0; i <= mas.GetUpperBound(0); i++)
                for (int j = 0; j <= mas.GetUpperBound(1); j++)
                    mas[i, j] = 0;
        }
        public static bool Full( int[,] mas)
        {
            for (int i = 0; i <= mas.GetUpperBound(0); i++)
                for (int j = 0; j <= mas.GetUpperBound(1); j++)
                    if (mas[i, j] == 0)
                        return false;
            return true;
        }
    }
}
