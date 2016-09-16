using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Lime_Editor
{
    static class Editor
    {
        public class TileGrid
        {
            public TileGrid(Loading.gridSize size, string Project, Loading.ProjectOptions projOps)
            {
                this.size = size;
                this.tiles = new Tile[this.size.width, this.size.height];
                if (projOps.zoomFactor != 0)
                    this.zoomFactor = projOps.zoomFactor;
                else
                    this.zoomFactor = 1;

                //Initialize grid, with default tiles
                for (uint i = 0;i<this.size.width;i++)
                {
                    for (uint j = 0; j < this.size.height; j++)
                    {
                        tiles[i,j] = new Tile((int)i, (int)j, projOps);
                    }
                }
            }

            public Loading.gridSize size;
            public Tile[,] tiles;
            public float zoomFactor;
        }
        public class Tile
        {
            public Tile(){}
            public Tile(int x, int y, Loading.ProjectOptions projOps)
            {
                this.position = new Loading.Vector2(x, y);
                this.tileId = -1;
                this.projOps = projOps;
            }
            public Tile(Loading.Vector2 position)
            {
                this.position = position;
            }
            public Image image
            {
                get
                {
                    if (tileId >= 0)
                        return projOps.images.Images[tileId];
                    else
                        return null;
                }
            }
            public Loading.Vector2 position;
            public int tileId;
            private Loading.ProjectOptions projOps;

        }
    }
}
