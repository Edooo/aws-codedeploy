using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace Tweet_Wall.Tests
{
    [TestFixture]
    public class SumTest
    {
        [Test]
        public void AssertThatSumOfTwoNumbersIsCorrect()
        {
            int actual = 3 + 6;

            Assert.AreEqual( 9, actual );
        }
    }
}
