using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Playground.Areas.Danb.Models;
using NUnit.Framework;

namespace Playground.Tests.Danb
{
    [TestFixture]
    class SweeperTest
    {
        [Test]
        public void SweeperCreate()
        {
            var m = new SweeperModel(6, 7);
            Assert.True(m.Width == 6);
            Assert.True(m.Height == 7);
        }

        [Test]
        public void SweeperEmptyMapHasNoMines()
        {
            var m = new SweeperModel(6, 7);
            for(int x=0; x<m.Width; x++)
            {
                for(int y=0; y<m.Height; y++)
                {
                    Assert.True(m.NeighborMines(x,y) == 0);
                }
            }
        }

        [Test]
        public void SweeperMap1MineNeighbors()
        {
            var m = new SweeperModel(6, 7);
            m.DebugForceMineAt(3, 3);

            int mineCount = 0;
            for (int x = 0; x < m.Width; x++)
            {
                for (int y = 0; y < m.Height; y++)
                {
                    mineCount += m.NeighborMines(x, y);
                }
            }
            Assert.True(mineCount == 8, "MineCount = " + mineCount);
            Assert.True(m.NeighborMines(2, 2) == 1);
            Assert.True(m.NeighborMines(3,3) == 0);
        }

    }
}
