using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GF_10
{
    public partial class Form1 : Form
    {
        PointF mid;
        PointF[] mainP = new PointF[2] { new PointF(2.0f, 1.0f), new PointF(8.0f, 6.0f) };
        Graphics g; Pen p; Pen line; PointF[] points;
        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            p = new Pen(Color.Black, 1);
            line = new Pen(Color.Red, 1);
            points = new PointF[3] { new PointF(1.0f, 2.0f), new PointF(9.0f, 4.0f), new PointF(5.0f, 3.0f) };
        }
        /* Метод преобразования вещественных координат X и Y в целые (10 на 7) */
        private int IX(double x)
        {
            double xx = x * (pictureBox1.Size.Width / 10.0) + 0.5;
            return (int)xx;
        }
        private int IY(double y)
        {
            double yy = pictureBox1.Size.Height - y * (pictureBox1.Size.Height / 7.0) + 0.5;
            return (int)yy;
        }
        //
        /*Рисование в координатной плоскости 10 на 7*/
        private void Draw(PointF f, PointF s)
        {
            Point point1 = new Point(IX(f.X), IY(f.Y));
            Point point2 = new Point(IX(s.X), IY(s.Y));
            g.DrawLine(p, point1, point2);
        }
        //
        private uint code(double x, double y)
        {
            return (uint)((Convert.ToUInt16(x < mainP[0].X) << 3) |
            (Convert.ToUInt16(x > mainP[1].X) << 2) |
            (Convert.ToUInt16(y < mainP[0].Y) << 1) |
             Convert.ToUInt16(y > mainP[1].Y));
        }
        private void button2_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            p = new Pen(Color.Black, 1);
            /* Вычерчивание границ окна */
            Draw(mainP[0], new PointF(mainP[1].X, mainP[0].Y)); Draw(new PointF(mainP[1].X, mainP[0].Y), mainP[1]);
            Draw(mainP[1], new PointF(mainP[0].X, mainP[1].Y)); Draw(new PointF(mainP[0].X, mainP[1].Y), mainP[0]);
            //
            p = line;
            Draw(points[0], points[1]);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            p = new Pen(Color.Black, 1);
            Draw(mainP[0], new PointF(mainP[1].X, mainP[0].Y)); Draw(new PointF(mainP[1].X, mainP[0].Y), mainP[1]);
            Draw(mainP[1], new PointF(mainP[0].X, mainP[1].Y)); Draw(new PointF(mainP[0].X, mainP[1].Y), mainP[0]);
            p = line;
            mid = points[2];
            points = CutOff(points[0], points[1], mid, mid);
            Draw(points[0], points[1]);
        }
        private PointF[] CutOff(PointF f, PointF s,PointF mid1,PointF mid2)
        {
            float x1 = f.X, y1 = f.Y, x2 = s.X, y2 = s.Y;
            PointF[] tmp;

            if (mid1.X > mainP[0].X && mid2.X < mainP[1].X)
            {
                if (mid1.Y > mainP[0].Y && mid2.Y < mainP[1].Y)
                {
                    mid1 = new PointF((mid1.X + x1) / 2, (mid1.Y + y1) / 2);
                    mid2 = new PointF((mid2.X + x2) / 2, (mid2.Y + y2) / 2);
                    return CutOff(f, s, mid1, mid2);
                }
            }
            else if (mid1.X <= mainP[0].X && mid2.X >= mainP[1].X)
            {
                if (mid1.Y <= mainP[0].Y && mid2.Y >= mainP[1].Y)
                {
                    x1 = mid1.X;
                    y1 = mid1.Y;
                    x2 = mid2.X;
                    y2 = mid2.Y;
                    tmp = new PointF[3] { new PointF(x1, y1), new PointF(x2, y2), new PointF((x1 + x2) / 2, (y1 + y2) / 2) };
                    return tmp;
                }
            }
            tmp = new PointF[3] { new PointF(x1, y1), new PointF(x2, y2), new PointF((x1 + x2) / 2, (y1 + y2) / 2) };
            return tmp;
        }
    }
}
