using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using playground.Areas.danb.Models;
using NUnit.Framework;

namespace playground.Tests.danb
{
    [TestFixture]
    class RockPaperScissorsModelTest
    {
        [Test]
        public void RockPaperScissors_Ties()
        {
            var rps = new RockPaperScissorsModel();
            var rvr = rps.RPS(RockPaperScissors.Rock, RockPaperScissors.Rock);
            Assert.True(rvr == 0);
            var pvp = rps.RPS(RockPaperScissors.Paper, RockPaperScissors.Paper);
            Assert.True(pvp == 0);
            var svs = rps.RPS(RockPaperScissors.Scissors, RockPaperScissors.Scissors);
            Assert.True(svs == 0);
        }

        [Test]
        public void RockPaperScissors_Rock()
        {
            var rps = new RockPaperScissorsModel();
            Assert.True(rps.RPS(RockPaperScissors.Rock, RockPaperScissors.Rock) == 0);
            Assert.True(rps.RPS(RockPaperScissors.Rock, RockPaperScissors.Paper) == 2);
            Assert.True(rps.RPS(RockPaperScissors.Rock, RockPaperScissors.Scissors) == 1);
        }

        [Test]
        public void RockPaperScissors_Paper()
        {
            var rps = new RockPaperScissorsModel();
            Assert.True(rps.RPS(RockPaperScissors.Paper, RockPaperScissors.Rock) == 1);
            Assert.True(rps.RPS(RockPaperScissors.Paper, RockPaperScissors.Paper) == 0);
            Assert.True(rps.RPS(RockPaperScissors.Paper, RockPaperScissors.Scissors) == 2);
        }

        [Test]
        public void RockPaperScissors_Scissors()
        {
            var rps = new RockPaperScissorsModel();
            Assert.True(rps.RPS(RockPaperScissors.Scissors, RockPaperScissors.Rock) == 2);
            Assert.True(rps.RPS(RockPaperScissors.Scissors, RockPaperScissors.Paper) == 1);
            Assert.True(rps.RPS(RockPaperScissors.Scissors, RockPaperScissors.Scissors) == 0);
        }
    }
}
