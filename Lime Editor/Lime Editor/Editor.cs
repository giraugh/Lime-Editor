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

        public class Layers
        {
            public TileGrid[] layers;
            public Layers(uint layerNum, Loading.gridSize gridSize, Loading.ProjectOptions projOps)
            {
                layers = new TileGrid[layerNum];
                for (uint i = 0; i < layerNum; i++)
                {
                    layers[i] = new TileGrid(gridSize, projOps);
                }
            }
        }

        public class TileGrid
        {
            public TileGrid(Loading.gridSize size, Loading.ProjectOptions projOps)
            {
                this.size = size;
                this.tiles = new Tile[this.size.width, this.size.height];

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
            public Tile(int x, int y, Loading.ProjectOptions projOps, int id)
            {
                this.position = new Loading.Vector2(x, y);
                this.tileId = id;
                this.projOps = projOps;
            }
            public Tile(Loading.Vector2 position, Loading.ProjectOptions projOps)
            {
                this.position = position;
                this.projOps = projOps;
                this.tileId = -1;
            }
            public Tile(Loading.Vector2 position, Loading.ProjectOptions projOps, int id)
            {
                this.position = position;
                this.projOps = projOps;
                this.tileId = id;
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
