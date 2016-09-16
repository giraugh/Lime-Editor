﻿using System;
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
using System.Diagnostics;

namespace Lime_Editor
{
    public partial class Main : Form
    {
        string Project = "D:/REPOS/LimeEditor/default";
        Loading.ProjectOptions ProjOps;
        Editor.Layers layers;
        int selectedLayer = 0;

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
                layers = new Editor.Layers((uint)ProjOps.layerCount, ProjOps.gridSize, ProjOps);
            }
            else
            { MessageBox.Show("Project failed to load, no ops returned."); }

            //set layer selection combo box
            for (int i = 0; i < ProjOps.layerCount; i++) {
                if (i < ProjOps.layerNames.Count<string>())
                    comLayerSelect.Items.Add(ProjOps.layerNames[i]);
                else
                    comLayerSelect.Items.Add("Layer "+(i+1).ToString());
            }

            comLayerSelect.SelectedIndex = 0;
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
            if (ProjOps != null && layers != null)
            {
                foreach (Editor.TileGrid grid in layers.layers)
                {
                    foreach (Editor.Tile tile in grid.tiles)
                    {
                        float ts = ProjOps.tileSize;
                        int sc = (int)((ts-1) * ProjOps.zoomFactor);
                        int x = (int)Math.Floor((double)(tile.position.x * sc));
                        int y = (int)Math.Floor((double)(tile.position.y * sc));
                        bool Hover = (tile.position.x == getUnscaledMousePos().X && tile.position.y == getUnscaledMousePos().Y);
                        if (tile.image != null)
                        {
                            g.DrawImage(tile.image, x, y, (float)Math.Round(ts * ProjOps.zoomFactor), (float)Math.Round(ts * ProjOps.zoomFactor));
                        }
                    }
                }
            }

            //Draw tile at mouse pos, rounded to grid coord
            if (Icons.SelectedIndices.Count > 0)
            {
                var Selected = Icons.SelectedIndices[0];
                float sc = (float)Math.Round(ProjOps.tileSize * ProjOps.zoomFactor);
                g.DrawImage(ProjOps.images.Images[Selected], getMousePos().X, getMousePos().Y, sc, sc);
            }
        }

        private void UpdateGridForMouse(MouseEventArgs e)
        {
            if (Icons.SelectedIndices.Count > 0) {
                Point m = getUnscaledMousePos();
                    foreach (Editor.Tile tile in layers.layers[selectedLayer].tiles)
                    {
                        if (tile.position.x == m.X && tile.position.y == m.Y)
                        {
                            if (e.Button == MouseButtons.Left)
                                tile.tileId = Icons.SelectedIndices[0];
                            if (e.Button == MouseButtons.Right)
                                tile.tileId = -1;
                        }
                    }
            }
        }


        private Point getUnscaledMousePos()
        {
            Point mp = Canvas.PointToClient(Cursor.Position);
            float mpx = mp.X;
            float mpy = mp.Y;
            float sc = (ProjOps.zoomFactor * (ProjOps.tileSize-1));
            mpx = (float)mpx / sc;
            mpy = (float)mpy / sc;
            return new Point((int)mpx, (int)mpy);
        }

        private Point getMousePos()
        {
            Point m = getUnscaledMousePos();
            float sc = (ProjOps.zoomFactor * (ProjOps.tileSize-1));
            return new Point((int)(m.X * sc), (int)(m.Y * sc));
        }

        private void UpdateOnMouseMove(object sender, MouseEventArgs e)
        {
            Canvas.Invalidate();
            UpdateGridForMouse(e);
        }

        private void comLayerSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedLayer = comLayerSelect.SelectedIndex;
        }
    }
}