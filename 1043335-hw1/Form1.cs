// 視窗作業一 me
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1043335_hw1
{
    public partial class Form1 : Form
    {
        int r, g, b, x, y;
        Random rd = new Random(Guid.NewGuid().GetHashCode());
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics G = this.CreateGraphics();
            for (int i = 0, y = 0; i < 3; i++, y += 50)
            {
                for(x = 0; x < 150; x += 50)
                {
                    r = rd.Next(256);
                    g = rd.Next(256);
                    b = rd.Next(256);
                    Brush b1 = new SolidBrush(Color.FromArgb(r, g, b));
                    G.DrawRectangle(new Pen(Color.Black, 2), x, y, 50, 50);
                    e.Graphics.FillRectangle(b1, x, y, 50, 50);
                }
            }
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0, y = 0; i < 3; i++, y += 50)
            {
                for(x = 0; x < 150; x += 50)
                {
                    Rectangle rec = new Rectangle(x, y, 50, 50);
                    if(rec.Contains(e.Location))
                    {
                        r = rd.Next(256);
                        g = rd.Next(256);
                        b = rd.Next(256);
                        Brush b1 = new SolidBrush(Color.FromArgb(r, g, b));
                        Graphics g1 = this.CreateGraphics();
                        g1.DrawRectangle(new Pen(Color.Black, 2), x, y, 49, 49);
                        g1.FillRectangle(b1, x, y, 49, 49);
                    }
                }
            }
        }
    }
}

// 視窗作業二 me
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
        int x, y;
        Rectangle[,] rec = new Rectangle[3,3];
        int[,] check = new int[3,3];
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Graphics g1 = this.CreateGraphics();
            g1.Clear(Color.White);
            for (int i = 0, y = 60; i < 3 && y < 240; i++, y += 60)
            {
                for (int j = 0, x = 10; j < 3 && x < 190; x += 60)
                {
                    rec[i, j] = new Rectangle(x, y, 60, 60);
                    g1.DrawRectangle(Pens.Black, rec[i, j]);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                    check[i, j] = 0;
            }
            label1.Text = "";
            player = true;
        }

        Boolean player = true;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            for (int checknum1 = 0, i = 0, y = 60 ; i < 3 && checknum1 < 3; i++, y += 60, checknum1++)
            {
                for (int checknum2 = 0, x = 10; x < 190 && checknum2 < 3; x += 60, checknum2++)
                {
                    Rectangle rec = new Rectangle(x, y, 60, 60);
                    if (rec.Contains(e.Location) && player == true)
                    {
                        if (check[checknum2,checknum1] == 0)
                        {
                            check[checknum2, checknum1] = 1;
                            Pen pen1 = new Pen(Color.Black, 3);  // 黑色畫筆 粗細為 3
                            pen1.DashStyle = DashStyle.Solid; //實線
                            Graphics g1 = this.CreateGraphics();
                            g1.DrawLine(pen1, x + 10, y + 15, x + 50, y + 50);
                            g1.DrawLine(pen1, x + 10, y + 50, x + 50, y + 10);
                            player = !player;
                        }
                    }
                    while (player == false)
                    {
                        bool end = true;
                        for (int a = 0; a < 3; a++)
                        {
                            for (int b = 0; b < 3; b++)
                            {
                                if (check[a, b] == 0)
                                    end = false;
                            }
                        }
                        if (end)
                            break;
                        Random rd = new Random(Guid.NewGuid().GetHashCode());
                        int xx = rd.Next(10, 190);
                        int yy = rd.Next(60, 240);
                        int xLocation;
                        int yLocation;
                        if (xx < 70)
                        {
                            xx = 10;
                            xLocation = 0;
                        }
                        else if (xx >= 70 && xx < 130)
                        {
                            xx = 70;
                            xLocation = 1;
                        }
                        else
                        {
                            xx = 130;
                            xLocation = 2;
                        }
                        if (yy < 120)
                        {
                            yy = 60;
                            yLocation = 0;
                        }
                        else if (yy >= 120 && yy < 180)
                        {
                            yy = 120;
                            yLocation = 1;
                        }
                        else
                        {
                            yy = 180;
                            yLocation = 2;
                        }
                        rec = new Rectangle(xx, yy, 60, 60);
                        if (check[xLocation, yLocation] == 0)
                        {
                            check[xLocation, yLocation] = 2;
                            Pen p1 = new Pen(Color.Blue, 5);
                            Graphics g1 = this.CreateGraphics();
                            g1.DrawEllipse(p1, xx + 10, yy + 10, 40, 40);
                            player = !player;
                        }
                    } 
                }
            }
            if (checkWinner() != "")
            {
                if (checkWinner() == "2")
                    label1.Text = "You lose !!";
                else if (checkWinner() == "1")
                    label1.Text = "You Win !!";
                else
                    label1.Text = "Draw !!";
            }
        }

        string checkWinner()
        {
            Pen pen1 = new Pen(Color.Red, 8);  // 黑色畫筆 粗細為 3
            pen1.DashStyle = DashStyle.Solid; //實線
            Graphics g1 = this.CreateGraphics();
            Boolean num = true;
            for(int i = 0; i < 3; i++)
            {
                int j = 0;
                if (check[i, j] == check[i, j + 1] && check[i, j + 1] == check[i, j + 2] && check[i, j] != 0) // 平行
                    return check[i, j].ToString();
            }
            for(int j = 0; j < 3; j++)
            {
                int i= 0;
                if (check[i, j] == check[i + 1, j] && check[i + 1, j] == check[i + 2, j] && check[i, j] != 0) // 垂直
                    return check[i, j].ToString();
            }
            if (check[0, 0] == check[1, 1] && check[1, 1] == check[2, 2] && check[0, 0] != 0)
                return check[0, 0].ToString();
            else if (check[0, 2] == check[1, 1 ] && check[1, 1] == check[1, 2] && check[0, 2] != 0)
                return check[0, 2].ToString();
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    if (check[i, j] == 0)
                    num = false;
                }
            }
            if (num)
                return "3";
            else
                return "";
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0, y = 60; i < 3 && y < 240; i++, y += 60)
            {
                for (int j = 0, x = 10; j < 3 && x < 190; x += 60)
                {
                    rec[i, j] = new Rectangle(x, y, 60, 60);
                    e.Graphics.DrawRectangle(Pens.Black, rec[i, j]);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    check[i, j] = 0;
                }
            }
        }
    }
}

// 視窗作業三 me
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;    //DashStyle

namespace _1043335_HW3
{
    public partial class Form1 : Form
    {
        Color c = Color.Red;
        bool lineSolid = true;
        int lineWidth = 1;
        List<Pen> pen1 = new List<Pen>();
        List<Point> startPt = new List<Point>();
        List<Point> endPt = new List<Point>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            startPt.Add(e.Location);
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            endPt.Add(e.Location);
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < endPt.Count(); i++)
            {
                if (pen1.Count < endPt.Count)
                {
                    pen1.Add(new Pen(c, lineWidth));
                    if (lineSolid)
                        pen1[pen1.Count - 1].DashStyle = DashStyle.Solid;
                    else
                        pen1[pen1.Count - 1].DashStyle = DashStyle.Dash;
                }
                e.Graphics.DrawLine(pen1[i], startPt[i].X, startPt[i].Y, endPt[i].X, endPt[i].Y);
            } 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "線段檔(*.pnt)|*.pnt";
            saveFileDialog1.Filter = "線段檔(*.pnt)|*.pnt";
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String s = saveFileDialog1.FileName;
                BinaryWriter outFile = new BinaryWriter(File.Open(s, FileMode.Create));
                outFile.Write(endPt.Count());
                for (int i = 0; i < endPt.Count(); i++)
                {
                    outFile.Write(pen1[i].Color.Name);
                    outFile.Write(pen1[i].Width.ToString());
                    if (pen1[i].DashStyle == DashStyle.Solid)
                        outFile.Write(1);
                    else if (pen1[i].DashStyle == DashStyle.Dash)
                        outFile.Write(0);
                    outFile.Write(startPt[i].X);
                    outFile.Write(startPt[i].Y);
                    outFile.Write(endPt[i].X);
                    outFile.Write(endPt[i].Y);
                }
                outFile.Close();
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String s = openFileDialog1.FileName;
                if (!File.Exists(s)) return;
                BinaryReader inFile = new BinaryReader(File.Open(s, FileMode.Open));
                startPt.Clear();
                endPt.Clear();
                pen1.Clear();
                int n = inFile.ReadInt32();
                for (int i = 0; i < n; i++)
                {
                    Color cr = new Color();
                    string color = inFile.ReadString();
                    if (color == "Red")
                        cr = Color.Red;
                    else if (color == "Green")
                        cr = Color.Green;
                    else if (color == "Blue")
                        cr = Color.Blue;
                    float W = float.Parse(inFile.ReadString());
                    pen1.Add(new Pen(cr, W));
                    int dash = inFile.ReadInt32();
                    if (dash == 1)
                        pen1[i].DashStyle = DashStyle.Solid;
                    else
                        pen1[i].DashStyle = DashStyle.Dash;
                    startPt.Add(new Point(inFile.ReadInt32(), inFile.ReadInt32()));
                    endPt.Add(new Point(inFile.ReadInt32(), inFile.ReadInt32()));
                }
                this.Invalidate();
                inFile.Close();
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            startPt.Clear();
            endPt.Clear();
            pen1.Clear();
            this.Invalidate();
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            c = Color.Red;
            redToolStripMenuItem.Checked = true;
            greenToolStripMenuItem.Checked = false;
            blueToolStripMenuItem.Checked = false;
            this.Invalidate();
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            c = Color.Green;
            redToolStripMenuItem.Checked = false;
            greenToolStripMenuItem.Checked = true;
            blueToolStripMenuItem.Checked = false;
            this.Invalidate();
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            c = Color.Blue;
            redToolStripMenuItem.Checked = false;
            greenToolStripMenuItem.Checked = false;
            blueToolStripMenuItem.Checked = true;
            this.Invalidate();
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            lineWidth = 1;
            this.Invalidate();
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            lineWidth = 2;
            this.Invalidate();
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            lineWidth = 3;
            this.Invalidate();
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            lineWidth = 4;
            this.Invalidate();
        }

        private void toolStripMenuItem17_Click(object sender, EventArgs e)
        {
            lineWidth = 5;
            this.Invalidate();
        }

        private void solidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lineSolid = true;
            this.Invalidate();
        }

        private void dashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lineSolid = false;
            this.Invalidate();
        }
    }
}

// 視窗作業四 me
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

// 作業二參考
