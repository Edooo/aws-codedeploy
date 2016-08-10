using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using NUnit.Framework;

using Playground.Areas.JoshuaD.Models;

namespace Playground.Tests.JoshuaD
{
    [TestFixture]
    class CageUnitTests
    {
        /// <summary>
        /// Cage Factory.
        /// </summary>
        /// <returns>New instance of a cage.</returns>
        private CageModel MakeCage()
        {
            return new CageModel();
        }
        /// <summary>
        /// Create a Tiger animal.
        /// </summary>
        /// <returns>A Tiger.</returns>
        private AnimalModel MakeTiger()
        {
            return new AnimalModel("Tiger");
        }
        /// <summary>
        /// return a <see cref="iHourGetter"/> that returns an invalid datetime object.
        /// </summary>
        /// <returns><see cref="iHourGetter"/> with a invalid datetime object.</returns>
        private iHourGetter HourGetterFactoryNoon()
        {
            return new FakeHoureGetter() { HourValue = 12 };
        }
        /// <summary>
        /// Create a <see cref="iHourGetter"/> with the supplied <paramref name="dateTimeValue">value</paramref>. 
        /// </summary>
        /// <param name="dateTimeValue">The DateTime to set the getter to have.</param>
        /// <returns>instance of <see cref="iHourGetter"/> with the time set to <paramref name="dateTimeValue">value</paramref>.</returns>
        private iHourGetter HourGetterFactory(DateTime dateTimeValue)
        {
            return new FakeHoureGetter() { HourValue = dateTimeValue.Hour };
        }
        /// <summary>
        /// Gets an hour factory set to midnight.
        /// </summary>
        /// <returns>Hour Factory set to midnight.</returns>
        private iHourGetter HourGetterFactoryOneAM()
        {
            return new FakeHoureGetter() { HourValue = 1 };
        }

        /// <summary>
        /// Test if a true is returned when an animal is added to the cage.
        /// </summary>
        [Test]
        public void Add_UnOccupied_ReturnTrue()
        {
            var cage = MakeCage();

            Assert.IsTrue(cage.Add(MakeTiger(), HourGetterFactoryOneAM()));
        }
        /// <summary>
        /// Test that a false is returned if an animal is already in the cage.
        /// </summary>
        [Test]
        public void Add_AlreadyOccupied_ReturnFalse()
        {
            var cage = MakeCage();
            cage.Add(MakeTiger(), HourGetterFactoryOneAM());

            Assert.IsFalse(cage.Add(new AnimalModel("Sasquatch"), HourGetterFactoryOneAM()));
        }
        /// <summary>
        /// Test if the state changes when an animal is added to the cage.
        /// </summary>
        [Test]
        public void IsOccupied_AddAnimal_StateIsTrue()
        {
            var cage = MakeCage();
            cage.Add(MakeTiger(), HourGetterFactoryOneAM());

            Assert.IsTrue(cage.IsOccupied);
        }
        /// <summary>
        /// Test to see if the Argument null exception is properly thrown when <b>null</b> animal added to cage.
        /// </summary>
        [Test]
        public void Add_NullAnimal_ThrowArgumentNullException()
        {
            var cage = MakeCage();

            Assert.Throws<ArgumentNullException>(() => cage.Add(null, HourGetterFactoryOneAM()));
        }
        /// <summary>
        /// Add method should throw argument null exception if default <see cref="AnimalModel"/> is supplied.
        /// </summary>
        [Test]
        public void Add_DefaultAnimal_ThrowArgumentNullException()
        {
            var cage = MakeCage();

            Assert.Throws<ArgumentNullException>(() => cage.Add(default(AnimalModel), HourGetterFactoryOneAM()));
        }
        /// <summary>
        /// Animals can only be added to the zoon between 8 pm and 8 am.
        /// </summary>
        [Test]
        public void Add_InvalidHour_ReturnFalse()
        {
            var cage = MakeCage();

            Assert.IsFalse(cage.Add(MakeTiger(), HourGetterFactoryNoon()));
        }
        /// <summary>
        /// Animals can only be added to the zoon between 8 pm and 8 am.
        /// </summary>
        [Test]
        public void Add_ValidHour_ReturnTrue()
        {
            var cage = MakeCage();

            Assert.IsTrue(cage.Add(MakeTiger(), HourGetterFactoryOneAM()));
        }
    }
}
