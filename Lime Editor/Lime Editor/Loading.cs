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

namespace Lime_Editor
{
    public class Loading
    {
        public static ProjectOptions Load_Project(string proj, ListView Icons)
        {
            //Check Is Project
            if (!File.Exists(proj + "/project.yml"))
            {
                MessageBox.Show(
                    "Could not find \"project.yml\" in the project.",
                    "Folder is not a project",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            //Load Project Elements
            Init_Icons(Icons);
            ProjectOptions po = Load_ProjectOptions(proj + "/project.yml");

            //Check Files Exist
            if (!File.Exists(proj + "/" + po.tiles))
            {
                MessageBox.Show(
                    "Could not find \""+po.tiles+"\" in the project.",
                    "Tiles not found",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }
            if (!File.Exists(proj + "/" + po.spritesheet))
            {
                MessageBox.Show(
                    "Could not find \"" + po.spritesheet + "\" in the project.",
                    "Sprites not found",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            po.images = Load_Tiles(proj + "/"+po.tiles, proj + "/"+po.spritesheet, Icons, po.tileSize);
            return po;
        }

        public static void Init_Icons(ListView Icons)
        {
            //Icons.Columns.Add("Icons", 100);
            Icons.Items.Clear();
        }

        public static ProjectOptions Load_ProjectOptions(string file)
        {
            var input = File.ReadAllText(file);
            var r = new StringReader(input);
            var deserializer = new Deserializer();
            ProjectOptions opts = deserializer.Deserialize<ProjectOptions>(r);
            return opts;
        }

        public static ImageList Load_Images(string tilesheet, ListView Icons, List<Tile> tiles, int tileSize)
        {
            ImageList icons = new ImageList();
            ImageList imgs = new ImageList();
            imgs.ColorDepth = ColorDepth.Depth32Bit;
            imgs.ImageSize = new Size(tileSize, tileSize);

            //Get Icons
            int iterator = 0;
            foreach (Tile tile in tiles)
            {
                Icon ic = Loading.getIcon(tile.pos, tilesheet, tileSize);
                icons.Images.Add(ic);
                Icons.Items[iterator].ImageIndex = iterator;
                iterator++;
            }

            //Get Images
            foreach (Tile tile in tiles)
            {
                Image im = Loading.getImage(tile.pos, tilesheet, tileSize);
                imgs.Images.Add(im);
            }

            Icons.LargeImageList = icons;
            return imgs;
        }

        public static Icon getIcon(Vector2 pos, string tilesheet, int tileSize)
        {
            Bitmap bmp = new Bitmap(Image.FromFile(tilesheet, false));
            bmp = CropImage(bmp, new Rectangle(pos.x * tileSize, pos.y * tileSize, tileSize, tileSize));
            IntPtr hbmp = bmp.GetHicon();
            Icon ic = Icon.FromHandle(hbmp);
            return ic;
        }

        public static Bitmap getImage(Vector2 pos, string tilesheet, int tileSize)
        {
            Bitmap bmp = (Bitmap)Image.FromFile(tilesheet, false);
            bmp = CropImage(bmp, new Rectangle(pos.x * tileSize, pos.y * tileSize, tileSize, tileSize));
            return bmp;
        }

        private static ImageList Load_Tiles(string TilesDocument, string tileSheet, ListView Icons, int tileSize)
        {
            var input = File.ReadAllText(TilesDocument);
            var r = new StringReader(input);
            var deserializer = new Deserializer();
            List<Tile> tiles = new List<Tile>();
            Tiles tiletypes = deserializer.Deserialize<Tiles>(r);

            //Create Tiles from Tiletype definitions
            foreach (TileType tile in tiletypes.tiles)
            {

                string type = tile.name;
                uint iterator = 1;

                foreach (Position pos in tile.positions)
                {
                    iPosition ipos = pos.GetProper();
                    Vector2 start = ipos.start;
                    Vector2 end = ipos.end;
                    int ix = start.x;
                    int iy = start.y;
                    while (true)
                    {
                        Tile lt = new Tile();
                        lt.type = type;
                        lt.name = type + "." + iterator.ToString();
                        lt.pos = new Vector2(ix, iy);
                        tiles.Add(lt);

                        iterator++;

                        if (!(ix != end.x || iy != end.y))
                        { break; }

                        if (ix < end.x) { ix++; }
                        if (ix > end.x) { ix--; }
                        if (iy < end.y) { iy++; }
                        if (iy > end.y) { iy--; }
                    }
                }
            }

            //Add Tiles to list
            foreach (Tile tile in tiles)
            {
                Icons.Items.Add(new ListViewItem(tile.name));
            }

            //Load Images, and associate with ListView
            return Load_Images(tileSheet, Icons, tiles, tileSize);

        }

        public class ProjectOptions
        {
            public string name { get; set; }
            public gridSize gridSize { get; set; }
            public int tileSize { get; set; }
            public string tiles { get; set; }
            public string spritesheet { get; set; }
            public float zoomFactor { get; set; }
            public ImageList images;
        }

        public class Tiles
        {
            public TileType[] tiles { get; set; }
        }

        public class Tile
        {
            public string type;
            public string name;
            public Vector2 pos;
        }

        public class TileType
        {
            public string name { get; set; }
            public Position[] positions { get; set; }
        }

        public class Position
        {
            public string start { get; set; }
            public string end { get; set; }
            public iPosition GetProper()
            {
                Vector2 istart = new Vector2(0, 0);
                Vector2 iend = new Vector2(0, 0);
                Match m;
                Regex vectorReg = new Regex(@"([0-9]+),\s*([0-9]+)");

                m = vectorReg.Match(this.start);
                istart.x = Int32.Parse(m.Groups[1].ToString());
                istart.y = Int32.Parse(m.Groups[2].ToString());

                m = vectorReg.Match(this.end);
                iend.x = Int32.Parse(m.Groups[1].ToString());
                iend.y = Int32.Parse(m.Groups[2].ToString());

                var ret = new iPosition();
                ret.start = istart;
                ret.end = iend;
                return ret;
            }
        }

        public class iPosition
        {
            public Vector2 start { get; set; }
            public Vector2 end { get; set; }
        }

        public class Vector2
        {
            public Vector2(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
            public Vector2(gridSize o)
            {
                this.x = o.width;
                this.y = o.height;
            }
            public int x { get; set; }
            public int y { get; set; }
            public override string ToString()
            {
                return this.x.ToString() + ", " + this.y.ToString();
            }
        }

        public class gridSize
        {
            public int width { get; set; }
            public int height { get; set; }
        }

        public static Bitmap CropImage(Bitmap source, Rectangle section)
        {
            // An empty bitmap which will hold the cropped image
            Bitmap bmp = new Bitmap(section.Width, section.Height);

            Graphics g = Graphics.FromImage(bmp);

            // Draw the given area (section) of the source image
            // at location 0,0 on the empty bitmap (bmp)
            g.DrawImage(source, 0, 0, section, GraphicsUnit.Pixel);

            return bmp;
        }
    }
}
