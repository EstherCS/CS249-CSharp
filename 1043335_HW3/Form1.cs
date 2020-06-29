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
