using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Playground.Areas.LukaM.Controllers;

namespace Playground.Tests.LukaM
{
    [TestFixture]
    class ChessTests
    {

        public void Test1()
        {
            Main m = new Main();
            m.StartMoving();
        }
    }
}
