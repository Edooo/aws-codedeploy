using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Playground.Areas.Danb.Models;
using System.Diagnostics;
using System.Threading;


namespace Playground.Tests.Danb
{
    [TestFixture]
    class ConwayModelTest
    {
        //[Test]
        //public void ConwayModel_TryIt()
        //{
        //    var model = new ConwayModel(10,10);
        //    model.DebugFillWithRandom();
        //    for(int n=0; n<100; n++)
        //    {
        //        model.DebugDumpBoard();
        //        WriteLine("");
        //        model.Tick();
        //        Thread.Sleep(500);
        //    }
        //

        [Test]
        public void ConwayModel_CreateEmpty()
        {
            var model = new ConwayModel(5,5);
            for(int x=0; x<5; x++)
            {
                for(int y=0; y<5; y++)
                {
                    Assert.False(model.IsCellAliveAt(0, 0));
                }
            }
        }

        [Test]
        public void ConwayModel_RunTick_OneDie()
        {
            var model = new ConwayModel(5, 5);
            model.SetValueAt(1, 1, 1);
            Assert.True(model.IsCellAliveAt(1, 1));
            model.Tick();
            Assert.False(model.IsCellAliveAt(1, 1));
        }

        [Test]
        public void ConwayModel_RunTick_RepeatingPattern()
        {
            var model = new ConwayModel(5, 5);
            model.SetValueAt(2, 2, 1);
            model.SetValueAt(3, 2, 1);
            model.SetValueAt(4, 2, 1);
            model.Tick();
            Assert.False(model.IsCellAliveAt(2, 2));
            Assert.False(model.IsCellAliveAt(4, 2));
            Assert.True(model.IsCellAliveAt(3, 1));
            Assert.True(model.IsCellAliveAt(3, 3));
            model.Tick();
            Assert.True(model.IsCellAliveAt(2, 2));
            Assert.True(model.IsCellAliveAt(4, 2));
            Assert.False(model.IsCellAliveAt(3, 1));
            Assert.False(model.IsCellAliveAt(3, 3));
        }

        [Test]
        public void ConwayModel_RunTick_Solid()
        {
            var model = new ConwayModel(5, 5);
            for(int x=0; x<model.Width; x++)
            {
                for(int y=0; y<model.Height; y++)
                {
                    model.SetValueAt(x,y, 1);
                }
            }
            model.Tick();
            for (int x = 0; x < model.Width; x++)
            {
                for (int y = 0; y < model.Height; y++)
                {
                    if ((x == 0 && y == 0) || (x == model.Width-1 && y == 0) || (x == model.Width - 1 && y == model.Height-1) || (x == 0 && y == model.Height-1))
                    {
                        Assert.True(model.IsCellAliveAt(x, y));
                    }
                    else
                    {
                        Assert.False(model.IsCellAliveAt(x, y));
                    }
                }
            }
        }
    }
}
