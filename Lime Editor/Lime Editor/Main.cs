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
using System.Diagnostics;

namespace Lime_Editor
{
    public partial class Main : Form
    {
        string Project = "default";
        Loading.ProjectOptions ProjOps;
        Editor.Layers layers;
        int selectedLayer = 0;
        string[] args;
        ListView InnerIcons = new ListView();

        public Main(string[] args) {
            this.args = args;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Load_Project(Project, true);

            //Enable Double Buffering on the canvas panel
            typeof(Panel).InvokeMember("DoubleBuffered",
            BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
            null, Canvas, new object[] { true });

            //Prevent Icons from losing selection when focus is lost
            Icons.HideSelection = false;

            //Load File if it is passed as argument
            if (args.Length > 0)
            {
                layers = Levels.DeserialiseLevel(layers, File.ReadAllText(args[0]), ProjOps);
            }
            
        }

        public void Load_Project(string proj, bool muteDirectoryError)
        {
            ProjOps = Loading.Load_Project(proj, InnerIcons, muteDirectoryError);
            if (ProjOps != null)
            {
                layers = new Editor.Layers((uint)ProjOps.layerCount, ProjOps.gridSize, ProjOps);
            }
            else
            {
                if (!muteDirectoryError)
                {
                    MessageBox.Show("Project failed to load, no ops returned.");
                }
                return;
            }

            //set layer selection combo box
            for (int i = 0; i < ProjOps.layerCount; i++) {
                if (i < ProjOps.layerNames.Count<string>())
                    comLayerSelect.Items.Add(ProjOps.layerNames[i]);
                else
                    comLayerSelect.Items.Add("Layer "+(i+1).ToString());
            }

            //set selected layer to first
            comLayerSelect.SelectedIndex = 0;

            //associate (items tag is whether to hide)
            foreach (ListViewItem item in InnerIcons.Items)
            {
                if (!(bool)item.Tag)
                {
                    ListViewItem newitem = (ListViewItem)item.Clone();
                    newitem.Tag = item.Index;
                    newitem.BackColor = Color.Empty;
                    //newitem.Text += ":"+item.Index;
                    Icons.Items.Add(newitem);
                }
            }
            //associate images
            Icons.LargeImageList = InnerIcons.LargeImageList;
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
                        int sc = (int)((ts - 1) * ProjOps.zoomFactor);
                        int x = (int)Math.Floor((double)(tile.position.x * sc));
                        int y = (int)Math.Floor((double)(tile.position.y * sc));
                        bool Hover = (tile.position.x == getUnscaledMousePos().X && tile.position.y == getUnscaledMousePos().Y);
                        if (tile.image != null)
                        {
                            g.DrawImage(tile.image, x, y, (float)Math.Round(ts * ProjOps.zoomFactor), (float)Math.Round(ts * ProjOps.zoomFactor));
                        }
                    }
                }

                //Draw Grid
                if (chkGrid.Checked)
                {
                    for (int i = 0; i < ProjOps.gridSize.width; i++)
                    {
                        for (int j = 0; j < ProjOps.gridSize.height; j++)
                        {
                            float ts = ProjOps.tileSize;
                            int sc = (int)((ts - 1) * ProjOps.zoomFactor);
                            int x = (int)Math.Floor((double)(i * sc));
                            int y = (int)Math.Floor((double)(j * sc));
                            int s = (int)Math.Round(ts * ProjOps.zoomFactor);
                            g.DrawRectangle(new Pen(Color.LightGray), new Rectangle(x, y, s - 1, s - 1));
                        }
                    }
                }

                //Draw tile at mouse pos, rounded to grid coord
                if (Icons.SelectedIndices.Count > 0)
                {
                    var Selected = (int)Icons.Items[Icons.SelectedIndices[0]].Tag; ;
                    float sc = (float)Math.Round(ProjOps.tileSize * ProjOps.zoomFactor);
                    g.DrawImage(ProjOps.images.Images[Selected], getMousePos().X, getMousePos().Y, sc, sc);
                }
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
                            tile.tileId = (int)Icons.Items[Icons.SelectedIndices[0]].Tag;
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

        private void UpdateNonMouse(object sender, EventArgs e)
        {
            Canvas.Invalidate();
        }

        private void comLayerSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedLayer = comLayerSelect.SelectedIndex;
        }

        private void saveLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Levels.SaveLevel(layers, ProjOps);
        }

        private void loadProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (!string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                Project = fbd.SelectedPath;
                Load_Project(Project, false);
            }
        }

        private void loadLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            layers = Levels.LoadLevel(layers, ProjOps, Project);
        }

        private void clearGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Editor.TileGrid grid in layers.layers)
            {
                foreach (Editor.Tile tile in grid.tiles)
                {
                    tile.tileId = -1;
                }
            }
        }

        private void clearLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Editor.Tile tile in layers.layers[selectedLayer].tiles)
            {
                tile.tileId = -1;
            }
        }

        private void fillLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Editor.Tile tile in layers.layers[selectedLayer].tiles)
            {
                if (Icons.SelectedIndices.Count > 0)
                    tile.tileId = (int)Icons.Items[Icons.SelectedIndices[0]].Tag;
            }
        }

        private void exportTilesheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int amount = ProjOps.images.Images.Count;
            int tileSize = ProjOps.tileSize;
            int columns = (int)Math.Sqrt(amount);
            int lines = (int)Math.Ceiling(amount / (float)columns);
            int width = columns * tileSize;
            int height = lines * tileSize;

            Bitmap bmp = new Bitmap(width, height);
            Graphics canvas = Graphics.FromImage(bmp);
            canvas.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            //Image Iterator
            int imageIt = 0;

            //Foreach Tile in the tilesheet, draw the correct tile
            for (int y = 0;y < lines;y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    if (imageIt < Icons.LargeImageList.Images.Count)
                        canvas.DrawImage(ProjOps.images.Images[imageIt], new Point(x * tileSize, y * tileSize));
                    imageIt++;
                }
            }

            //Save the canvas back into the image
            canvas.Save();

            //Create a save file dialog to get a path
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = "Portable Network Graphic (*.png)|*.png|All files (*.*)|*.*";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                //Save the bitmap as a png
                bmp.Save(fd.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        private void levelAsTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = "MoonShine (*.shine)|*.shine|All files (*.*)|*.*";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                //Save the level as a moonshine table
                var fileName = fd.FileName;
                var contents = Levels.SerializeLevelLua(layers, ProjOps);
                File.WriteAllText(fileName, contents);
            }
        }

        private void levelCoffeescriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = "Coffeescript (*.coffee)|*.coffee|All files (*.*)|*.*";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                //Save the level as a moonshine table
                var fileName = fd.FileName;
                var contents = Levels.SerializeLevelCoffee(layers, ProjOps);
                File.WriteAllText(fileName, contents);
            }
        }
    }
}
