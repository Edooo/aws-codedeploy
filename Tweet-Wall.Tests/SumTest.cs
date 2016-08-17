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
            int actual = 3 + 7;

            Assert.AreEqual( 10, actual );
        }
    }
}
