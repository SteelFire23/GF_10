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
        float xmin = 2.0f, xmax = 8.0f, ymin = 2.0f, ymax = 5.0f;
        Graphics g; Pen p; Pen line;
        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
            p = new Pen(Color.Black, 1);
            line = new Pen(Color.Red, 1);
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
        private void Draw(double x1, double y1, double x2, double y2)
        {
            Point point1 = new Point(IX(x1), IY(y1));
            Point point2 = new Point(IX(x2), IY(y2));
            g.DrawLine(p, point1, point2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            p = new Pen(Color.Black, 1);
            /* Вычерчивание границ окна */
            Draw(xmin, ymin, xmax, ymin); Draw(xmax, ymin, xmax, ymax);
            Draw(xmax, ymax, xmin, ymax); Draw(xmin, ymax, xmin, ymin);
            p = line;
            Draw(1.0f, 3.0f, 9.0f, 3.0f);
        }

        //
        /*Алгоритм Коэна-Сазерленда*/
        private uint Code(double x, double y)
        {
            return (uint)((Convert.ToUInt16(x < xmin) << 3) |
            (Convert.ToUInt16(x > xmax) << 2) |
            (Convert.ToUInt16(y < ymin) << 1) |
            Convert.ToUInt16(y > ymax));
        }
        //
        private void button1_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            p = new Pen(Color.Black, 1);
            Draw(xmin, ymin, xmax, ymin); Draw(xmax, ymin, xmax, ymax);
            Draw(xmax, ymax, xmin, ymax); Draw(xmin, ymax, xmin, ymin);
            PointF[] np = CutOff(1.0f, 3.0f, 9.0f, 3.0f, 0,1.0f, 1.0f, 9.0f);
            p = line;
            Draw(np[0].X, np[0].Y, np[1].X, np[1].Y);
        }
        private PointF[] CutOff(float x1, float y1, float x2, float y2, int count,float tmp, float tmp1, float tmp2)
        {
            PointF[] points = new PointF[2];
            points[0] = new PointF(x1, y1);
            points[1] = new PointF(x2, y2);

            double len = (x2 + x1) / 2;
            if (count<2)
            {
                count++;
                tmp = (float)len;
                if(count==2) return CutOff(tmp1, y1, tmp2, y2, count, tmp, tmp1, tmp2);
                return CutOff(x1, y1, (float)len, y2, count,tmp,tmp1,tmp2);
            }
            if (count < 4 && count >= 1)
            {
                count++;
                if (count == 4) return CutOff(tmp, y1, (float)len, y2, count, tmp, tmp1, tmp2);
                return CutOff((float)len, y1, x2, y2, count, tmp, tmp1, tmp2);
            } 
            return points;
        }
    }
}
