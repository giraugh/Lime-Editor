using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using YamlDotNet.Serialization;
using System.Reflection;

namespace Lime_Editor
{
    public partial class Main : Form
    {
        string Project = "D:/REPOS/LimeEditor/default";
        Loading.ProjectOptions ProjOps;
        Editor.TileGrid grid;

        public Main() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Load_Project(Project);

            //Enable Double Buffering on the canvas panel
            typeof(Panel).InvokeMember("DoubleBuffered",
            BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
            null, Canvas, new object[] { true });
        }

        public void Load_Project(string proj)
        {
            ProjOps = Loading.Load_Project(proj, Icons);
            if (ProjOps != null)
            {
                grid = new Editor.TileGrid(ProjOps.gridSize, proj, ProjOps);
            }
            else
            { MessageBox.Show("Project failed to load, no ops returned."); }
        }

        private void loadProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (!string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                Project = fbd.SelectedPath;
                Load_Project(Project);
            }
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.WhiteSmoke);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
            if (ProjOps != null && grid != null)
            {
                foreach (Editor.Tile tile in grid.tiles)
                {
                    float ts = ProjOps.tileSize;
                    int x = (tile.position.x*(int)ts)*(int)grid.zoomFactor;
                    int y = (tile.position.y*(int)ts)*(int)grid.zoomFactor;
                    bool Hover = (tile.position.x == getUnscaledMousePos().X && tile.position.y == getUnscaledMousePos().Y);
                    if (tile.image != null && !Hover)
                    {
                        g.DrawImage(tile.image, x, y, (float)Math.Round(ts*grid.zoomFactor), (float)Math.Round(ts*grid.zoomFactor));
                    }
                    else
                    {
                        g.DrawRectangle(new Pen(Color.DarkGray), new Rectangle(x, y, (int)(ts*grid.zoomFactor), (int)(ts * grid.zoomFactor)));
                    }
                }
            }

            //Draw tile at mouse pos, rounded to grid coord
            if (Icons.SelectedIndices.Count > 0)
            {
                var Selected = Icons.SelectedIndices[0];
                float sc = (float)Math.Round(ProjOps.tileSize * grid.zoomFactor);
                g.DrawImage(ProjOps.images.Images[Selected], getMousePos().X, getMousePos().Y, sc, sc);
            }
        }

        private void UpdateGridForMouse(object sender, MouseEventArgs e)
        {
            if (Icons.SelectedIndices.Count > 0) {
                Point m = getUnscaledMousePos();
                foreach (Editor.Tile tile in grid.tiles)
                {
                    if (tile.position.x == m.X && tile.position.y == m.Y)
                    {
                        tile.tileId = Icons.SelectedIndices[0];
                    }
                }
            }
        }


        private Point getUnscaledMousePos()
        {
            Point mp = Canvas.PointToClient(Cursor.Position);
            float mpx = mp.X;
            float mpy = mp.Y;
            float sc = (grid.zoomFactor * ProjOps.tileSize);
            mpx = (float)Math.Round(mpx / sc);
            mpy = (float)Math.Round(mpy / sc);
            return new Point((int)mpx, (int)mpy);
        }

        private Point getMousePos()
        {
            Point mp = Canvas.PointToClient(Cursor.Position);
            float mpx = mp.X;
            float mpy = mp.Y;
            float sc = (grid.zoomFactor * ProjOps.tileSize);
            mpx = (float)Math.Round(mpx / sc) * sc;
            mpy = (float)Math.Round(mpy / sc) * sc;
            return new Point((int)mpx, (int)mpy);
        }

        private void UpdateOnMouseMove(object sender, MouseEventArgs e)
        {
            Canvas.Invalidate();
        }
    }
}
