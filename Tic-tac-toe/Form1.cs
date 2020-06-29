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

namespace Tic_tac_toe
{
    public partial class Form1 : Form
    {
        Rectangle[] block = new Rectangle[9];

        int[] drawed = new int[9];

        public Form1()
        {
            InitializeComponent();

            for (int i = 0; i < 9; i++)

                drawed[i] = -10;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int blocknum = 0;

            Pen reccol = new Pen(Color.Black, 4);

            for (int i = 30; i <= 150; i += 60)
            {
                for (int j = 2; j <= 122; j += 60)
                {
                    block[blocknum] = new Rectangle(j, i, 60, 60);

                    e.Graphics.DrawRectangle(reccol, block[blocknum]);

                    blocknum++;
                }
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            int blockidx = -1;

            for (int i = 0; i < 9; i++)
            {
                if (block[i].Contains(e.Location))
                {
                    blockidx = i;

                    break;
                }
            }

            if (blockidx != -1 && drawed[blockidx] == -10)
            {
                Pen me = new Pen(Color.Blue, 4), skline = new Pen(Color.Red, 4);

                Graphics cle = this.CreateGraphics();

                bool draw = true;

                cle.DrawEllipse(me, new Rectangle(block[blockidx].X + 10, block[blockidx].Y + 10, block[blockidx].Width - 20, block[blockidx].Height - 20));

                drawed[blockidx] = 0;

                for (int i = 0; i < 7; i += 3)
                {
                    if (drawed[i] + drawed[i + 1] + drawed[i + 2] == 0)
                    {
                        cle.DrawLine(skline, block[i].X + block[i].Width / 4, block[i].Y + block[i].Height / 2, block[i + 2].X + block[i + 2].Width * 3 / 4, block[i + 2].Y + block[i + 2].Height / 2);

                        label1.Text = "You win!";

                        for (int j = 0; j < 9; j++)

                            drawed[j] = 10;

                        return;
                    }
                }

                for (int i = 0; i < 3; i++)
                {
                    if (drawed[i] + drawed[i + 3] + drawed[i + 6] == 0)
                    {
                        cle.DrawLine(skline, block[i].X + block[i].Width / 2, block[i].Y + block[i].Height / 4, block[i + 6].X + block[i + 6].Width / 2, block[i + 6].Y + block[i + 6].Height * 3 / 4);

                        label1.Text = "You win!";

                        for (int j = 0; j < 9; j++)

                            drawed[j] = 10;

                        return;
                    }
                }

                if (drawed[0] + drawed[4] + drawed[8] == 0)
                {
                    cle.DrawLine(skline, block[0].X + block[0].Width / 4, block[0].Y + block[0].Height / 4, block[8].X + block[8].Width * 3 / 4, block[8].Y + block[8].Height * 3 / 4);

                    label1.Text = "You win!";

                    for (int j = 0; j < 9; j++)

                        drawed[j] = 10;

                    return;
                }

                if (drawed[2] + drawed[4] + drawed[6] == 0)
                {
                    cle.DrawLine(skline, block[2].X + block[2].Width * 3 / 4, block[2].Y + block[2].Height / 4, block[6].X + block[6].Width / 4, block[6].Y + block[6].Height * 3 / 4);

                    label1.Text = "You win!";

                    for (int j = 0; j < 9; j++)

                        drawed[j] = 10;

                    return;
                }

                for (int i = 0; i < 9; i++)
                {
                    if (drawed[i] == -10)
                    {
                        draw = false;

                        break;
                    }
                }

                if (draw)
                {
                    label1.Text = "Draw!";

                    return;
                }

                for (int i = 0; i < 7; i += 3)
                {
                    if (drawed[i] + drawed[i + 1] + drawed[i + 2] == -8)
                    {
                        if (drawed[i] == -10)
                        {
                            cle.DrawLine(me, block[i].X + block[i].Width / 4, block[i].Y + block[i].Height / 4, block[i].X + block[i].Width * 3 / 4, block[i].Y + block[i].Height * 3 / 4);

                            cle.DrawLine(me, block[i].X + block[i].Width * 3 / 4, block[i].Y + block[i].Height / 4, block[i].X + block[i].Width / 4, block[i].Y + block[i].Height * 3 / 4);

                            drawed[i] = 1;

                            goto checking;
                        }

                        else if (drawed[i + 1] == -10)
                        {
                            cle.DrawLine(me, block[i + 1].X + block[i + 1].Width / 4, block[i + 1].Y + block[i + 1].Height / 4, block[i + 1].X + block[i + 1].Width * 3 / 4, block[i + 1].Y + block[i + 1].Height * 3 / 4);

                            cle.DrawLine(me, block[i + 1].X + block[i + 1].Width * 3 / 4, block[i + 1].Y + block[i + 1].Height / 4, block[i + 1].X + block[i + 1].Width / 4, block[i + 1].Y + block[i + 1].Height * 3 / 4);

                            drawed[i + 1] = 1;

                            goto checking;
                        }

                        else if (drawed[i + 2] == -10)
                        {
                            cle.DrawLine(me, block[i + 2].X + block[i + 2].Width / 4, block[i + 2].Y + block[i + 2].Height / 4, block[i + 2].X + block[i + 2].Width * 3 / 4, block[i + 2].Y + block[i + 2].Height * 3 / 4);

                            cle.DrawLine(me, block[i + 2].X + block[i + 2].Width * 3 / 4, block[i + 2].Y + block[i + 2].Height / 4, block[i + 2].X + block[i + 2].Width / 4, block[i + 2].Y + block[i + 2].Height * 3 / 4);

                            drawed[i + 2] = 1;

                            goto checking;
                        }
                    }
                }

                for (int i = 0; i < 3; i++)
                {
                    if (drawed[i] + drawed[i + 3] + drawed[i + 6] == -8)
                    {
                        if (drawed[i] == -10)
                        {
                            cle.DrawLine(me, block[i].X + block[i].Width / 4, block[i].Y + block[i].Height / 4, block[i].X + block[i].Width * 3 / 4, block[i].Y + block[i].Height * 3 / 4);

                            cle.DrawLine(me, block[i].X + block[i].Width * 3 / 4, block[i].Y + block[i].Height / 4, block[i].X + block[i].Width / 4, block[i].Y + block[i].Height * 3 / 4);

                            drawed[i] = 1;

                            goto checking;
                        }

                        else if (drawed[i + 3] == -10)
                        {
                            cle.DrawLine(me, block[i + 3].X + block[i + 3].Width / 4, block[i + 3].Y + block[i + 3].Height / 4, block[i + 3].X + block[i + 3].Width * 3 / 4, block[i + 3].Y + block[i + 3].Height * 3 / 4);

                            cle.DrawLine(me, block[i + 3].X + block[i + 3].Width * 3 / 4, block[i + 3].Y + block[i + 3].Height / 4, block[i + 3].X + block[i + 3].Width / 4, block[i + 3].Y + block[i + 3].Height * 3 / 4);

                            drawed[i + 3] = 1;

                            goto checking;
                        }

                        else if (drawed[i + 6] == -10)
                        {
                            cle.DrawLine(me, block[i + 6].X + block[i + 6].Width / 4, block[i + 6].Y + block[i + 6].Height / 4, block[i + 6].X + block[i + 6].Width * 3 / 4, block[i + 6].Y + block[i + 6].Height * 3 / 4);

                            cle.DrawLine(me, block[i + 6].X + block[i + 6].Width * 3 / 4, block[i + 6].Y + block[i + 6].Height / 4, block[i + 6].X + block[i + 6].Width / 4, block[i + 6].Y + block[i + 6].Height * 3 / 4);

                            drawed[i + 6] = 1;

                            goto checking;
                        }
                    }
                }

                if (drawed[0] + drawed[4] + drawed[8] == -8)
                {
                    if (drawed[0] == -10)
                    {
                        cle.DrawLine(me, block[0].X + block[0].Width / 4, block[0].Y + block[0].Height / 4, block[0].X + block[0].Width * 3 / 4, block[0].Y + block[0].Height * 3 / 4);

                        cle.DrawLine(me, block[0].X + block[0].Width * 3 / 4, block[0].Y + block[0].Height / 4, block[0].X + block[0].Width / 4, block[0].Y + block[0].Height * 3 / 4);

                        drawed[0] = 1;

                        goto checking;
                    }

                    else if (drawed[4] == -10)
                    {
                        cle.DrawLine(me, block[4].X + block[4].Width / 4, block[4].Y + block[4].Height / 4, block[4].X + block[4].Width * 3 / 4, block[4].Y + block[4].Height * 3 / 4);

                        cle.DrawLine(me, block[4].X + block[4].Width * 3 / 4, block[4].Y + block[4].Height / 4, block[4].X + block[4].Width / 4, block[4].Y + block[4].Height * 3 / 4);

                        drawed[4] = 1;

                        goto checking;
                    }

                    else if (drawed[8] == -10)
                    {
                        cle.DrawLine(me, block[8].X + block[8].Width / 4, block[8].Y + block[8].Height / 4, block[8].X + block[8].Width * 3 / 4, block[8].Y + block[8].Height * 3 / 4);

                        cle.DrawLine(me, block[8].X + block[8].Width * 3 / 4, block[8].Y + block[8].Height / 4, block[8].X + block[8].Width / 4, block[8].Y + block[8].Height * 3 / 4);

                        drawed[8] = 1;

                        goto checking;
                    }
                }

                if (drawed[2] + drawed[4] + drawed[6] == -8)
                {
                    if (drawed[2] == -10)
                    {
                        cle.DrawLine(me, block[2].X + block[2].Width / 4, block[2].Y + block[2].Height / 4, block[2].X + block[2].Width * 3 / 4, block[2].Y + block[2].Height * 3 / 4);

                        cle.DrawLine(me, block[2].X + block[2].Width * 3 / 4, block[2].Y + block[2].Height / 4, block[2].X + block[2].Width / 4, block[2].Y + block[2].Height * 3 / 4);

                        drawed[2] = 1;

                        goto checking;
                    }

                    else if (drawed[4] == -10)
                    {
                        cle.DrawLine(me, block[4].X + block[4].Width / 4, block[4].Y + block[4].Height / 4, block[4].X + block[4].Width * 3 / 4, block[4].Y + block[4].Height * 3 / 4);

                        cle.DrawLine(me, block[4].X + block[4].Width * 3 / 4, block[4].Y + block[4].Height / 4, block[4].X + block[4].Width / 4, block[4].Y + block[4].Height * 3 / 4);

                        drawed[4] = 1;

                        goto checking;
                    }

                    else if (drawed[6] == -10)
                    {
                        cle.DrawLine(me, block[6].X + block[6].Width / 4, block[6].Y + block[6].Height / 4, block[6].X + block[6].Width * 3 / 4, block[6].Y + block[6].Height * 3 / 4);

                        cle.DrawLine(me, block[6].X + block[6].Width * 3 / 4, block[6].Y + block[6].Height / 4, block[6].X + block[6].Width / 4, block[6].Y + block[6].Height * 3 / 4);

                        drawed[6] = 1;

                        goto checking;
                    }
                }

                for (int i = 0; i < 7; i += 3)
                {
                    if (drawed[i] + drawed[i + 1] + drawed[i + 2] == -10)
                    {
                        if (drawed[i] == -10)
                        {
                            cle.DrawLine(me, block[i].X + block[i].Width / 4, block[i].Y + block[i].Height / 4, block[i].X + block[i].Width * 3 / 4, block[i].Y + block[i].Height * 3 / 4);

                            cle.DrawLine(me, block[i].X + block[i].Width * 3 / 4, block[i].Y + block[i].Height / 4, block[i].X + block[i].Width / 4, block[i].Y + block[i].Height * 3 / 4);

                            drawed[i] = 1;

                            goto checking;
                        }

                        else if (drawed[i + 1] == -10)
                        {
                            cle.DrawLine(me, block[i + 1].X + block[i + 1].Width / 4, block[i + 1].Y + block[i + 1].Height / 4, block[i + 1].X + block[i + 1].Width * 3 / 4, block[i + 1].Y + block[i + 1].Height * 3 / 4);

                            cle.DrawLine(me, block[i + 1].X + block[i + 1].Width * 3 / 4, block[i + 1].Y + block[i + 1].Height / 4, block[i + 1].X + block[i + 1].Width / 4, block[i + 1].Y + block[i + 1].Height * 3 / 4);

                            drawed[i + 1] = 1;

                            goto checking;
                        }

                        else if (drawed[i + 2] == -10)
                        {
                            cle.DrawLine(me, block[i + 2].X + block[i + 2].Width / 4, block[i + 2].Y + block[i + 2].Height / 4, block[i + 2].X + block[i + 2].Width * 3 / 4, block[i + 2].Y + block[i + 2].Height * 3 / 4);

                            cle.DrawLine(me, block[i + 2].X + block[i + 2].Width * 3 / 4, block[i + 2].Y + block[i + 2].Height / 4, block[i + 2].X + block[i + 2].Width / 4, block[i + 2].Y + block[i + 2].Height * 3 / 4);

                            drawed[i + 2] = 1;

                            goto checking;
                        }
                    }
                }

                for (int i = 0; i < 3; i++)
                {
                    if (drawed[i] + drawed[i + 3] + drawed[i + 6] == -10)
                    {
                        if (drawed[i] == -10)
                        {
                            cle.DrawLine(me, block[i].X + block[i].Width / 4, block[i].Y + block[i].Height / 4, block[i].X + block[i].Width * 3 / 4, block[i].Y + block[i].Height * 3 / 4);

                            cle.DrawLine(me, block[i].X + block[i].Width * 3 / 4, block[i].Y + block[i].Height / 4, block[i].X + block[i].Width / 4, block[i].Y + block[i].Height * 3 / 4);

                            drawed[i] = 1;

                            goto checking;
                        }

                        else if (drawed[i + 3] == -10)
                        {
                            cle.DrawLine(me, block[i + 3].X + block[i + 3].Width / 4, block[i + 3].Y + block[i + 3].Height / 4, block[i + 3].X + block[i + 3].Width * 3 / 4, block[i + 3].Y + block[i + 3].Height * 3 / 4);

                            cle.DrawLine(me, block[i + 3].X + block[i + 3].Width * 3 / 4, block[i + 3].Y + block[i + 3].Height / 4, block[i + 3].X + block[i + 3].Width / 4, block[i + 3].Y + block[i + 3].Height * 3 / 4);

                            drawed[i + 3] = 1;

                            goto checking;
                        }

                        else if (drawed[i + 6] == -10)
                        {
                            cle.DrawLine(me, block[i + 6].X + block[i + 6].Width / 4, block[i + 6].Y + block[i + 6].Height / 4, block[i + 6].X + block[i + 6].Width * 3 / 4, block[i + 6].Y + block[i + 6].Height * 3 / 4);

                            cle.DrawLine(me, block[i + 6].X + block[i + 6].Width * 3 / 4, block[i + 6].Y + block[i + 6].Height / 4, block[i + 6].X + block[i + 6].Width / 4, block[i + 6].Y + block[i + 6].Height * 3 / 4);

                            drawed[i + 6] = 1;

                            goto checking;
                        }
                    }
                }

                if (drawed[0] + drawed[4] + drawed[8] == -10)
                {
                    if (drawed[0] == -10)
                    {
                        cle.DrawLine(me, block[0].X + block[0].Width / 4, block[0].Y + block[0].Height / 4, block[0].X + block[0].Width * 3 / 4, block[0].Y + block[0].Height * 3 / 4);

                        cle.DrawLine(me, block[0].X + block[0].Width * 3 / 4, block[0].Y + block[0].Height / 4, block[0].X + block[0].Width / 4, block[0].Y + block[0].Height * 3 / 4);

                        drawed[0] = 1;

                        goto checking;
                    }

                    else if (drawed[4] == -10)
                    {
                        cle.DrawLine(me, block[4].X + block[4].Width / 4, block[4].Y + block[4].Height / 4, block[4].X + block[4].Width * 3 / 4, block[4].Y + block[4].Height * 3 / 4);

                        cle.DrawLine(me, block[4].X + block[4].Width * 3 / 4, block[4].Y + block[4].Height / 4, block[4].X + block[4].Width / 4, block[4].Y + block[4].Height * 3 / 4);

                        drawed[4] = 1;

                        goto checking;
                    }

                    else if (drawed[8] == -10)
                    {
                        cle.DrawLine(me, block[8].X + block[8].Width / 4, block[8].Y + block[8].Height / 4, block[8].X + block[8].Width * 3 / 4, block[8].Y + block[8].Height * 3 / 4);

                        cle.DrawLine(me, block[8].X + block[8].Width * 3 / 4, block[8].Y + block[8].Height / 4, block[8].X + block[8].Width / 4, block[8].Y + block[8].Height * 3 / 4);

                        drawed[8] = 1;

                        goto checking;
                    }
                }

                if (drawed[2] + drawed[4] + drawed[6] == -10)
                {
                    if (drawed[2] == -10)
                    {
                        cle.DrawLine(me, block[2].X + block[2].Width / 4, block[2].Y + block[2].Height / 4, block[2].X + block[2].Width * 3 / 4, block[2].Y + block[2].Height * 3 / 4);

                        cle.DrawLine(me, block[2].X + block[2].Width * 3 / 4, block[2].Y + block[2].Height / 4, block[2].X + block[2].Width / 4, block[2].Y + block[2].Height * 3 / 4);

                        drawed[2] = 1;

                        goto checking;
                    }

                    else if (drawed[4] == -10)
                    {
                        cle.DrawLine(me, block[4].X + block[4].Width / 4, block[4].Y + block[4].Height / 4, block[4].X + block[4].Width * 3 / 4, block[4].Y + block[4].Height * 3 / 4);

                        cle.DrawLine(me, block[4].X + block[4].Width * 3 / 4, block[4].Y + block[4].Height / 4, block[4].X + block[4].Width / 4, block[4].Y + block[4].Height * 3 / 4);

                        drawed[4] = 1;

                        goto checking;
                    }

                    else if (drawed[6] == -10)
                    {
                        cle.DrawLine(me, block[6].X + block[6].Width / 4, block[6].Y + block[6].Height / 4, block[6].X + block[6].Width * 3 / 4, block[6].Y + block[6].Height * 3 / 4);

                        cle.DrawLine(me, block[6].X + block[6].Width * 3 / 4, block[6].Y + block[6].Height / 4, block[6].X + block[6].Width / 4, block[6].Y + block[6].Height * 3 / 4);

                        drawed[6] = 1;

                        goto checking;
                    }
                }

                if (drawed[4] == -10)
                {
                    cle.DrawLine(me, block[4].X + block[4].Width / 4, block[4].Y + block[4].Height / 4, block[4].X + block[4].Width * 3 / 4, block[4].Y + block[4].Height * 3 / 4);

                    cle.DrawLine(me, block[4].X + block[4].Width * 3 / 4, block[4].Y + block[4].Height / 4, block[4].X + block[4].Width / 4, block[4].Y + block[4].Height * 3 / 4);

                    drawed[4] = 1;

                    goto checking;
                }

                for (int i = 0; i < 3; i += 2)
                {
                    if (drawed[i] == -10)
                    {
                        cle.DrawLine(me, block[i].X + block[i].Width / 4, block[i].Y + block[i].Height / 4, block[i].X + block[i].Width * 3 / 4, block[i].Y + block[i].Height * 3 / 4);

                        cle.DrawLine(me, block[i].X + block[i].Width * 3 / 4, block[i].Y + block[i].Height / 4, block[i].X + block[i].Width / 4, block[i].Y + block[i].Height * 3 / 4);

                        drawed[i] = 1;

                        goto checking;
                    }

                    else if (drawed[i + 6] == -10)
                    {
                        cle.DrawLine(me, block[i + 6].X + block[i + 6].Width / 4, block[i + 6].Y + block[i + 6].Height / 4, block[i + 6].X + block[i + 6].Width * 3 / 4, block[i + 6].Y + block[i + 6].Height * 3 / 4);

                        cle.DrawLine(me, block[i + 6].X + block[i + 6].Width * 3 / 4, block[i + 6].Y + block[i + 6].Height / 4, block[i + 6].X + block[i + 6].Width / 4, block[i + 6].Y + block[i + 6].Height * 3 / 4);

                        drawed[i + 6] = 1;

                        goto checking;
                    }
                }

                for (int i = 0; i < 9; i++)
                {
                    if (drawed[i] == -10)
                    {
                        cle.DrawLine(me, block[i].X + block[i].Width / 4, block[i].Y + block[i].Height / 4, block[i].X + block[i].Width * 3 / 4, block[i].Y + block[i].Height * 3 / 4);

                        cle.DrawLine(me, block[i].X + block[i].Width * 3 / 4, block[i].Y + block[i].Height / 4, block[i].X + block[i].Width / 4, block[i].Y + block[i].Height * 3 / 4);

                        drawed[i] = 1;

                        break;
                    }
                }

                checking:

                for (int i = 0; i < 7; i += 3)
                {
                    if (drawed[i] + drawed[i + 1] + drawed[i + 2] == 3)
                    {
                        cle.DrawLine(skline, block[i].X + block[i].Width / 4, block[i].Y + block[i].Height / 2, block[i + 2].X + block[i + 2].Width * 3 / 4, block[i + 2].Y + block[i + 2].Height / 2);

                        label1.Text = "You lose!";

                        for (int j = 0; j < 9; j++)

                            drawed[j] = 10;

                        return;
                    }
                }

                for (int i = 0; i < 3; i++)
                {
                    if (drawed[i] + drawed[i + 3] + drawed[i + 6] == 3)
                    {
                        cle.DrawLine(skline, block[i].X + block[i].Width / 2, block[i].Y + block[i].Height / 4, block[i + 6].X + block[i + 6].Width / 2, block[i + 6].Y + block[i + 6].Height * 3 / 4);

                        label1.Text = "You lose!";

                        for (int j = 0; j < 9; j++)

                            drawed[j] = 10;

                        return;
                    }
                }

                if (drawed[0] + drawed[4] + drawed[8] == 3)
                {
                    cle.DrawLine(skline, block[0].X + block[0].Width / 4, block[0].Y + block[0].Height / 4, block[8].X + block[8].Width * 3 / 4, block[8].Y + block[8].Height * 3 / 4);

                    label1.Text = "You lose!";

                    for (int j = 0; j < 9; j++)

                        drawed[j] = 10;

                    return;
                }

                if (drawed[2] + drawed[4] + drawed[6] == 3)
                {
                    cle.DrawLine(skline, block[2].X + block[2].Width * 3 / 4, block[2].Y + block[2].Height / 4, block[6].X + block[6].Width / 4, block[6].Y + block[6].Height * 3 / 4);

                    label1.Text = "You lose!";

                    for (int j = 0; j < 9; j++)

                        drawed[j] = 10;

                    return;
                }
            }
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Invalidate();

            for (int i = 0; i < 9; i++)

                drawed[i] = -10;

            label1.Text = " ";
        }
    }
}