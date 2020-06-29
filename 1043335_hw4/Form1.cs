using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1043335_hw4
{
    public partial class Form1 : Form
    {
        Rectangle rect;    // 矩形區域
        int Vx = 5;        // 球的初始速度
        int Vy = 5;
        int panelX;        // 板子的 X 起點
        PictureBox ball;
        int counter = 0;   // 計時器
        public Form1()
        {
            InitializeComponent();
            rect = new Rectangle(10,50, 250, 250); // 寬高100的矩形區域
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.Black, rect); // 繪出矩形
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
            timer3.Start();
            ball = new PictureBox();
            ball.Location = new Point(20, 150);
            ball.Size = new Size(10, 10);
            ball.BackColor = Color.Red;
            System.Drawing.Drawing2D.GraphicsPath g = new System.Drawing.Drawing2D.GraphicsPath();
            g.AddEllipse(new Rectangle(0, 0, 10, 10));
            ball.Region = new Region(g);
            g.Dispose();
            this.Controls.Add(ball);
            toolStripStatusLabel1.Text = "  ";
            toolStripStatusLabel2.Text = "Playing";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ball.Left += Vx;
            ball.Top += Vy;
            if (ball.Left < 10)       // 超過左邊界，反彈
            {
                Vx = Math.Abs(Vx);
            }
            if (ball.Top < 50)        // 超過上面界，反彈
            {
                Vy = Math.Abs(Vy);
            }
            if (ball.Right > 260)     // 超過右邊界，反彈
            {
                Vx = -Math.Abs(Vx);
            }
            if(ball.Bottom >= panel.Top)
            {
                int center = (ball.Left + ball.Right) / 2;          // 球的中心點
                if(center >= panel.Left && center <= panel.Right)   // 球在板子的範圍
                {
                    double F = ((double)center - (double)panel.Left) / (double)panel.Width;
                    if (Vx < 0)
                        F = 1.0 - F;
                    F += 0.5;
                    Vx = (int)Math.Round(Vx * F);
                    Vy = -Math.Abs(Vy);
                }
                else
                {
                    if (ball.Top > 300)
                    {
                        timer1.Stop();
                        timer2.Stop();
                        timer3.Stop();
                        toolStripStatusLabel2.Text = "Game Over!";
                    }
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (timer1.Interval > 1)    // 每過五秒，球加速
                timer1.Interval -= 9;
        }

        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            panelX = e.X;
        }

        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)  // 左鍵按下
            {
                int x = panel.Left + (e.X - panelX);
                if (x < 10)                    // 不能超出左邊界
                    x = 10;
                if (x > 260 - panel.Width)     // 不能超出右邊界
                    x = 260 - panel.Width;
                panel.Left = x;
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ball.Left = 130;
            ball.Top = 150;
            panel.Left = 106;
            ball.BackColor = Color.Red;
            counter = 0;
            toolStripStatusLabel2.Text = "Playing";
            toolStripStatusLabel1.Text = "  ";
            timer1.Start();
            timer2.Start();
            timer3.Start();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ball.BackColor = Color.Red;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ball.BackColor = Color.Green;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ball.BackColor = Color.Blue;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            counter++;
            toolStripStatusLabel1.Text = counter.ToString();
        }
    }
}
