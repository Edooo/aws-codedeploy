using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Playground.Areas.Danb.Models;
using NUnit.Framework;
using Playground.Models;

namespace Playground.Tests.Danb
{
    [TestFixture]
    class AnimalCageTest
    {
        
        [TearDown]
        public void AnimalCage_TearDown()
        {
            IBDateTime.ForcedNow = null;
        }

        [Test]
        public void AnimalCage_CreateEmpty()
        {
            var cage = new AnimalCage();
            Assert.True(!cage.IsOccupied);
        }

        [Test]
        public void AnimalCage_AddToEmptyCage()
        {
            var cage = new AnimalCage();
            Assert.True(cage.Add(Animal.MakeMonkey(3)));
            Assert.True(cage.IsOccupied);
        }

        [Test]
        public void AnimalCage_AddSameToNonEmptyCage()
        {
            var cage = new AnimalCage();
            Assert.True(cage.Add(Animal.MakeMonkey(3)));
            Assert.True(cage.Add(Animal.MakeMonkey(4)));
            Assert.True(cage.IsOccupied);
        }

        [Test]
        public void AnimalCage_AddDifferentToNonEmptyCage()
        {
            var cage = new AnimalCage();
            Assert.True(cage.Add(Animal.MakeTiger(3)));
            Assert.False(cage.Add(Animal.MakeMonkey(4)));
            Assert.False(cage.Add(Animal.MakeZebra(3)));
            Assert.True(cage.IsOccupied);
        }

        [Test]
        public void AnimalCage_AddWithinTimeRestriction_Fails()
        {
            var cage = new AnimalCage();
            Assert.True(cage.Add(Animal.MakeTiger(3)));
            DateTime begin = DateTime.Now;
            DateTime end = begin.AddSeconds(3);
            cage.TimeRestrictions(begin, end);
            Assert.False(cage.Add(Animal.MakeTiger(3)));
            Assert.True(cage.IsOccupied);
        }

        [Test]
        public void AnimalCage_AddOutsideTimeRestriction_Succeeds()
        {
            var cage = new AnimalCage();
            Assert.True(cage.Add(Animal.MakeTiger(3)));
            DateTime begin = DateTime.Now.AddSeconds(300);
            DateTime end = begin.AddSeconds(3);
            cage.TimeRestrictions(begin, end);
            Assert.True(cage.Add(Animal.MakeTiger(3)));
            Assert.True(cage.IsOccupied);
        }

        [Test]
        public void AnimalCage_AddWithinTimeRestrictionInjection_Fails()
        {
            var cage = new AnimalCage();
            DateTime begin = new DateTime(2000, 12, 25, 2, 0, 0, 0);  // 2AM
            DateTime end = new DateTime(2000, 12, 25, 6, 0, 0, 0);  // 6AM
            cage.TimeRestrictions(begin, end);
            IBDateTime.ForcedNow = new DateTime(2015, 3, 12, 4, 15, 32); // 4:15:32
            Assert.False(cage.Add(Animal.MakeTiger(3)));
        }

        [Test]
        public void AnimalCage_AddOutsideTimeRestrictionInjection_Succeeds()
        {
            var cage = new AnimalCage();
            DateTime begin = new DateTime(2000, 12, 25, 2, 0, 0, 0);  // 2AM
            DateTime end = new DateTime(2000, 12, 25, 6, 0, 0, 0);  // 6AM
            cage.TimeRestrictions(begin, end);
            IBDateTime.ForcedNow = new DateTime(2015, 3, 12, 6, 15, 32); // 6:15:32
            Assert.True(cage.Add(Animal.MakeTiger(3)));
            IBDateTime.ForcedNow = new DateTime(2015, 3, 12, 1, 15, 32); // 1:15:32
            Assert.True(cage.Add(Animal.MakeTiger(3)));
        }
    }
}
