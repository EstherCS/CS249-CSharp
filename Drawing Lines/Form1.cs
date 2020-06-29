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
using System.IO;

namespace Drawing_Lines
{
    public partial class Form1 : Form
    {
        List< Point > startpt = new List< Point >(), endpt = new List< Point >();

        List< Color > thecolor = new List< Color >();

        List< int > thewidth = new List< int >();

        List< DashStyle > thestyle = new List< DashStyle >();

        Color color = Color.Red;

        int width = 1;

        DashStyle style = DashStyle.Solid;

        Pen pen;

        public Form1()
        {
            InitializeComponent();

            pen = new Pen(color, width);

            pen.DashStyle = style;

            saveFileDialog1.Filter = "線段檔(*.pnt)|*.pnt";

            openFileDialog1.Filter = "線段檔(*.pnt)|*.pnt";
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            color = Color.Red;

            redToolStripMenuItem.Checked = true;

            greenToolStripMenuItem.Checked = false;

            blueToolStripMenuItem.Checked = false;
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            color = Color.Green;

            redToolStripMenuItem.Checked = false;

            greenToolStripMenuItem.Checked = true;

            blueToolStripMenuItem.Checked = false;
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            color = Color.Blue;

            redToolStripMenuItem.Checked = false;

            greenToolStripMenuItem.Checked = false;

            blueToolStripMenuItem.Checked = true;
        }

        private void solidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            style = DashStyle.Solid;

            solidToolStripMenuItem.Checked = true;

            dashToolStripMenuItem.Checked = false;
        }

        private void dashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            style = DashStyle.Dash;

            solidToolStripMenuItem.Checked = false;

            dashToolStripMenuItem.Checked = true;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            width = 1;

            toolStripMenuItem2.Checked = true;

            toolStripMenuItem3.Checked = false;

            toolStripMenuItem4.Checked = false;

            toolStripMenuItem5.Checked = false;

            toolStripMenuItem6.Checked = false;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            width = 2;

            toolStripMenuItem2.Checked = false;

            toolStripMenuItem3.Checked = true;

            toolStripMenuItem4.Checked = false;

            toolStripMenuItem5.Checked = false;

            toolStripMenuItem6.Checked = false;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            width = 3;

            toolStripMenuItem2.Checked = false;

            toolStripMenuItem3.Checked = false;

            toolStripMenuItem4.Checked = true;

            toolStripMenuItem5.Checked = false;

            toolStripMenuItem6.Checked = false;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            width = 4;

            toolStripMenuItem2.Checked = false;

            toolStripMenuItem3.Checked = false;

            toolStripMenuItem4.Checked = false;

            toolStripMenuItem5.Checked = true;

            toolStripMenuItem6.Checked = false;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            width = 5;

            toolStripMenuItem2.Checked = false;

            toolStripMenuItem3.Checked = false;

            toolStripMenuItem4.Checked = false;

            toolStripMenuItem5.Checked = false;

            toolStripMenuItem6.Checked = true;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            endpt.Add(e.Location);

            thecolor.Add(color);

            thewidth.Add(width);

            thestyle.Add(style);

            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < endpt.Count(); i++)
            {
                pen.Color = thecolor[i]; pen.Width = thewidth[i]; pen.DashStyle = thestyle[i];

                e.Graphics.DrawLine(pen, startpt[i], endpt[i]);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string f = saveFileDialog1.FileName;

                BinaryWriter output = new BinaryWriter(File.Open(f, FileMode.Create));

                output.Write(endpt.Count());

                for (int i = 0; i < endpt.Count(); i++)
                {
                    output.Write(startpt[i].X); output.Write(startpt[i].Y);

                    output.Write(endpt[i].X); output.Write(endpt[i].Y);

                    if (thecolor[i] == Color.Red) output.Write(1);

                    else if (thecolor[i] == Color.Green) output.Write(2);

                    else output.Write(3);

                    output.Write(thewidth[i]);

                    if (thestyle[i] == DashStyle.Solid) output.Write(1);

                    else output.Write(2);
                }

                output.Close();
            }
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string f = openFileDialog1.FileName;

                if (!File.Exists(f)) return;

                BinaryReader input = new BinaryReader(File.Open(f, FileMode.Open));

                startpt.Clear(); endpt.Clear(); thecolor.Clear(); thewidth.Clear(); thestyle.Clear();

                int count = input.ReadInt32(), cmp;

                for (int i = 0; i < count; i++)
                {
                    startpt.Add(new Point(input.ReadInt32(), input.ReadInt32()));

                    endpt.Add(new Point(input.ReadInt32(), input.ReadInt32()));

                    cmp = input.ReadInt32();

                    if (cmp == 1) thecolor.Add(Color.Red);

                    else if (cmp == 2) thecolor.Add(Color.Green);

                    else thecolor.Add(Color.Blue);

                    thewidth.Add(input.ReadInt32());

                    cmp = input.ReadInt32();

                    if (cmp == 1) thestyle.Add(DashStyle.Solid);

                    else thestyle.Add(DashStyle.Dash);
                }

                input.Close();

                this.Invalidate();
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            startpt.Clear(); endpt.Clear(); thecolor.Clear(); thewidth.Clear(); thestyle.Clear();

            this.Invalidate();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            startpt.Add(e.Location);
        }
    }
}
