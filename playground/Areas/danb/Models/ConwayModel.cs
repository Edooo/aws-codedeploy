using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace Playground.Areas.Danb.Models
{
    public class ConwayModel
    {

        public int Width
        {
            get; private set;
        }
        public int Height
        {
            get; private set;
        }
        private int[,] Board;
        private Random rnd = new Random();

        public ConwayModel(int width, int height)
        {
            Width = width;
            Height = height;
            Board = new int[Width, Height];
        }

        public bool IsCellAliveAt(int x, int y)
        {
            return (GetValueAt(x, y) & 0x01) != 0;
        }

        public void Tick()
        {
            // calculate all "next tick" values from "current tick" values
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    SetNextTickValueAt(x, y, 0);
                    int n = CountNeighbors(x, y);
                    if (IsCellAliveAt(x, y))
                    {   // cell is alive
                        if (n == 2 || n == 3)
                        {
                            // stay alive
                            SetNextTickValueAt(x, y, 1);
                        }
                        else
                        {   // cell died
                            SetNextTickValueAt(x, y, 0);
                        }
                    }
                    else
                    {   // cell is dead
                        if (n == 3)
                        {   // birth
                            SetNextTickValueAt(x, y, 1);
                        }
                    }
                }
            }

            // copy "next tick values" to "current tick values"
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if ((GetValueAt(x, y) & 0x02) == 0)
                    {
                        SetValueAt(x, y, 0);
                    } else
                    {
                        SetValueAt(x, y, 1);
                    }
                }
            }
        }

        private int CountNeighbors(int x, int y)
        {
            int numberNeighbors = 0;
            for(int dx=-1; dx<=1; dx++)
            {
                for(int dy=-1; dy<=1; dy++)
                {
                    if (dx == 0 && dy == 0)
                    {

                    } else
                    {
                        if (IsCellAliveAt(x + dx, y + dy)) numberNeighbors++;
                    }
                }
            }
            return numberNeighbors;
        }

        private int GetValueAt(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Width || y >= Height) return 0;
            return Board[x, y];
        }

        public void SetValueAt(int x, int y, int value)
        {
            Debug.Assert(x >= 0 && y >= 0);
            Debug.Assert(x < Width && y < Height);
            Board[x, y] = value;
        }

        void SetNextTickValueAt(int x, int y, int value)
        {
            if (value != 0)
            {
                Board[x, y] |= 0x02;
            }
            else
            {
                Board[x, y] &= ~0x02;
            }
        }

        public void DebugDumpBoard()
        {
            for(int y=0; y<Height; y++)
            {
                for(int x=0; x<Width; x++)
                {
                    Debug.Write("" + (IsCellAliveAt(x, y) ? "X " : ". "));
                }
                Debug.WriteLine("");
            }
        }

        public void DebugFillWithRandom()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Board[x, y] = rnd.NextDouble() > 0.4 ? 1 : 0;
                }
            }
        }
    }
}