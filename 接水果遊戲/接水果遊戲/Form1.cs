using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace 接水果遊戲
{
    public partial class Form1 : Form
    {
        Image[] background = { Properties.Resources._0, Properties.Resources._1, Properties.Resources._2, Properties.Resources._3,
                               Properties.Resources._4, Properties.Resources._5, Properties.Resources._6, Properties.Resources._7 },
                fruit = { Properties.Resources.Banana, Properties.Resources.StawBerry, Properties.Resources.Tomato },
                nowfruit = new Image[3];

        Image nowbackground, pedal = Properties.Resources.Bowl;

        float[][] colormatrixarray = { new float[] {1, 0, 0, 0, 0 },
                                       new float[] {0, 1, 0, 0, 0 },
                                       new float[] {0, 0, 1, 0, 0 },
                                       new float[] {0, 0, 0, 0.5f, 0 },
                                       new float[] {0, 0, 0, 0, 1 } };

        ImageAttributes imgattri = new ImageAttributes();

        Random rand = new Random();

        int fruitnum, second = 120, eat = 0;

        Rectangle[] destrec = new Rectangle[3];

        Rectangle pedalblock;

        bool[] whetherdraw = { true, true, true };

        public Form1()
        {
            InitializeComponent();

            DoubleBuffered = true;

            nowbackground = background[rand.Next() % 8];

            if(nowbackground.Width > this.ClientSize.Width || nowbackground.Height > this.ClientSize.Height - 168)

                this.ClientSize = new Size(500, 300);

            ColorMatrix colormatrix = new ColorMatrix(colormatrixarray);

            imgattri.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            pedalblock = new Rectangle(nowbackground.Width / 2 - pedal.Width / 2, nowbackground.Height + 31, pedal.Width, pedal.Height);

            label1.Location = new Point(12, nowbackground.Height + pedal.Height + 34);

            label2.Location = new Point(12, nowbackground.Height + pedal.Height + 59);

            NewFruit();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            int posx = e.Location.X;

            if (posx > nowbackground.Width - pedal.Width / 2)

                posx = nowbackground.Width - pedal.Width / 2;

            else if (posx < pedal.Width / 2)

                posx = pedal.Width / 2;

            pedalblock = new Rectangle(posx - pedal.Width / 2, nowbackground.Height + 31, pedal.Width, pedal.Height);
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Text = "Remaining: 120 Seconds";

            label2.Text = "Received: 0";

            second = 120;

            eat = 0;

            timer2.Enabled = true;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(nowbackground, new Rectangle(0, 28, nowbackground.Width, nowbackground.Height), 0, 0, nowbackground.Width, nowbackground.Height, GraphicsUnit.Pixel, imgattri);

            e.Graphics.DrawImage(pedal, pedalblock, new Rectangle(0, 0, pedal.Width, pedal.Height), GraphicsUnit.Pixel);

            for (int i = 0; i < fruitnum; i++)
            {
                if (pedalblock.Y < destrec[i].Bottom && pedalblock.Y > destrec[i].Bottom - destrec[i].Height / 2 && second > 0)
                    if (destrec[i].X + destrec[i].Width / 2 > pedalblock.X && destrec[i].X + destrec[i].Width / 2 < pedalblock.Right)
                    {
                        if(whetherdraw[i])

                            label2.Text = "Received: " + ++eat;

                        whetherdraw[i] = false;
                    }

                if (whetherdraw[i])
                {
                    e.Graphics.DrawImage(nowfruit[i], destrec[i], new Rectangle(0, 0, nowfruit[i].Width, nowfruit[i].Height), GraphicsUnit.Pixel);

                    destrec[i].Y += 3;

                    if (destrec[i].Y > this.ClientSize.Height)

                        whetherdraw[i] = false;
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            int backgroundindex;

            second--;

            if(second <= 1)

                label1.Text = "Remaining: " + second + " Second";

            else

                label1.Text = "Remaining: " + second + " Seconds";

            if (second == 0)
            {
                timer2.Enabled = false;

                return;
            }

            if (second % 10 == 0)
            {
                backgroundindex = rand.Next() % 8;

                while(background[backgroundindex] == nowbackground)

                    backgroundindex = rand.Next() % 8;

                nowbackground = background[backgroundindex];

                if (nowbackground.Width > this.ClientSize.Width || nowbackground.Height > this.ClientSize.Height - 168)

                    this.ClientSize = new Size(nowbackground.Width, nowbackground.Height + 168);
            }
        }

        private void NewFruit()
        {
            bool[] selected = { false, false, false };

            int fruitindex;

            fruitnum = 1 + rand.Next() % 3;

            for (int i = 0, posx, gap = 0; i < fruitnum; i++)
            {
                fruitindex = rand.Next() % 3;

                while (selected[fruitindex])

                    fruitindex = rand.Next() % 3;

                selected[fruitindex] = true;

                nowfruit[i] = fruit[fruitindex];

                posx = rand.Next() % (nowbackground.Width - nowfruit[i].Width);

                if (i > 0)

                    gap = 80 + rand.Next() % 141;

                destrec[i] = new Rectangle(posx, 28 - nowfruit[i].Height - gap, nowfruit[i].Width, nowfruit[i].Height);
            }
        }

        private bool WhetherReset()
        {
            for (int i = 0; i < fruitnum; i++)
                if (whetherdraw[i])
                    return false;

            return true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (WhetherReset() && second > 0)
            {
                for (int i = 0; i < fruitnum; i++)

                    whetherdraw[i] = true;

                NewFruit();
            }

            this.Invalidate();
        }
    }
}
