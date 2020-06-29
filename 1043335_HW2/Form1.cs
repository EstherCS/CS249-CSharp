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
