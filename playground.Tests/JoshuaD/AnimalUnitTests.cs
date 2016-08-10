using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using Playground.Areas.JoshuaD.Models;

namespace Playground.Tests.JoshuaD
{
    /// <summary>
    /// Test the <see cref="AnimalModel"/>
    /// </summary>
    [TestFixture]
    class AnimalUnitTests
    {
        /// <summary>
        /// Test that the name is properly set when the name constructor is called.
        /// </summary>
        [Test]
        public void Constructor_SupplyName_NameSet()
        {
            string animalName = "Horse";

            AnimalModel horse = new AnimalModel(animalName);

            Assert.AreEqual(animalName, horse.Name);
        }
    }
}
