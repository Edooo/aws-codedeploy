using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Playground.Areas.Danb.Models
{
    public class SweeperModel
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        private int[,] map;
        private const int MAP_MINE = 1;
        private const int MAP_VISITED = 2;
        private const int MAP_EDGE = 8;


        public SweeperModel(int width, int height)
        {
            Width = width;
            Height = height;
            map = new int[width,height];
            for(int x=0; x< Width; x++)
            {
                for(int y=0; y< Height; y++)
                {
                    map[x, y] = 0;
                }
            }
        }

        public void DebugForceMineAt(int x, int y)
        {
            map[x, y] |= MAP_MINE;
        }

        private int MapValueAt(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height)
            {
                return MAP_EDGE;
            }
            return map[x, y];
        }

        public int NeighborMines(int x, int y)
        {
            int count = 0;
            for(int a=x-1; a<=x+1; a++)
            {
                for(int b=y-1; b<=y+1; b++)
                {
                    if (a != x || b != y) // ignore the center square
                    {
                        if ((MapValueAt(a, b) & MAP_MINE) != 0)
                        {
                            count++;
                        }
                    }
                }
            }
            return count;
        }
    }
}