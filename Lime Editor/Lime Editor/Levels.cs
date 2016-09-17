using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lime_Editor.Editor;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace Lime_Editor
{
    class Levels
    {

        public static void SaveLevel(Layers layers, Loading.ProjectOptions projOptions)
        {
            Stream lvlStr;
            var lvl = SerializeLevel(layers, projOptions);
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = "levels (*.llvl)|*.llvl|All files (*.*)|*.*";

            if (fd.ShowDialog() == DialogResult.OK)
            {
                if ((lvlStr = fd.OpenFile()) != null)
                {
                    var utencoding = new UTF8Encoding();
                    lvlStr.Write(utencoding.GetBytes(lvl), 0, lvl.Length);
                    lvlStr.Close();
                }
            }
        }

        public static Layers LoadLevel(Layers layers, Loading.ProjectOptions projOps, string Project)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "levels (*.llvl)|*.llvl|All files (*.*)|*.*";
            od.InitialDirectory = Project;
            if (od.ShowDialog() == DialogResult.OK)
            {
                layers = DeserialiseLevel(layers, File.ReadAllText(od.FileName), projOps);
            }
            return layers;
        }

        public static Layers DeserialiseLevel(Layers layers, string lvl, Loading.ProjectOptions projOps)
        {

            //RESET LAYERS obj
            layers = new Layers((uint)projOps.layerCount, projOps.gridSize, projOps);


            //REGEXI
            Regex PROJECTNAME = new Regex(@"\[PROJECTNAME:(.+)\]");
            Regex TILESIZE = new Regex(@"\[TILESIZE:(.+)\]");
            Regex LEVELWIDTH = new Regex(@"\[LEVELWIDTH:(.+)\]");
            Regex LEVELHEIGHT = new Regex(@"\[LEVELHEIGHT:(.+)\]");
            Regex LAYER = new Regex(@"\[LAYER:(.+)\]");
            Regex NUMBER = new Regex(@"(.+),");

            //DETAILS
            var projName = "";
            var tileSize = 0;
            var levelHeight = 0;
            var levelWidth = 0;

            //ITERATORS
            var buffer = "";
            var curLayer = 0;
            var xIt = 0;
            var yIt = 0;
            foreach (char c in lvl)
            {
                //INCREMENT BUFFER
                buffer += c;

                //PROJECT DETAILS
                if (PROJECTNAME.IsMatch(buffer))
                {
                    projName = PROJECTNAME.Match(buffer).Groups[1].ToString();
                    buffer = "";
                }

                if (TILESIZE.IsMatch(buffer))
                {
                    tileSize = Int32.Parse(TILESIZE.Match(buffer).Groups[1].ToString());
                    buffer = "";
                }

                if (LEVELHEIGHT.IsMatch(buffer))
                {
                    levelHeight = Int32.Parse(LEVELHEIGHT.Match(buffer).Groups[1].ToString());
                    buffer = "";
                }

                if (LEVELWIDTH.IsMatch(buffer))
                {
                    levelWidth = Int32.Parse(LEVELWIDTH.Match(buffer).Groups[1].ToString());
                    buffer = "";
                }

                //SWITCH LAYER
                if (LAYER.IsMatch(buffer))
                {
                    var swiLayer = LAYER.Match(buffer).Groups[1].ToString();
                    curLayer = Int32.Parse(swiLayer);
                    buffer = "";
                    xIt = yIt = 0;
                    if (curLayer > projOps.layerCount)
                    {
                        break;
                    }
                }

                //NUMBER
                if (NUMBER.IsMatch(buffer))
                {
                    var num = NUMBER.Match(buffer).Groups[1].ToString();
                    layers.layers[curLayer-1].tiles[xIt, yIt] = new Tile(xIt, yIt, projOps, Int32.Parse(num));
                    yIt++;
                    if (yIt > levelWidth-1)
                    {
                        yIt = 0;
                        xIt++;
                    }
                    buffer = "";
                }

                //RESET BUFFER
                if (buffer == "" || buffer == "\n")
                    buffer = "";
            }

            return layers;
        }

        public static string SerializeLevel(Layers layers, Loading.ProjectOptions projOptions)
        {
            string s = "";

            s += $"[PROJECTNAME:{projOptions.name}]\n";
            s += $"[LEVELWIDTH:{projOptions.gridSize.width}]\n";
            s += $"[LEVELHEIGHT:{projOptions.gridSize.height}]\n";
            s += $"[TILESIZE:{projOptions.tileSize}]\n";

            uint curLayer = 0;
            foreach (TileGrid layer in layers.layers)
            {
                uint it = 1;
                s += $"[LAYER:{curLayer+1}]\n";
                foreach (Tile tile in layer.tiles)
                {
                    var t = tile.tileId.ToString();
                    if (t.Length == 1) { t = " " + t; }
                    s += $"{t},";
                    if (it == projOptions.gridSize.width)
                    {
                        it = 0;
                        s += "\n";
                    }
                    it++;
                }
                s += "\n";
                curLayer++;
            }

            return s;
        }
    }
}
