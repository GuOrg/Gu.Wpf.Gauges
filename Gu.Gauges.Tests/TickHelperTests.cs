namespace Gu.Gauges.Tests
{
    using System.Linq;
    using NUnit.Framework;

    public class TickHelperTests
    {
        [TestCase(0, 20, 10, new double[] { 0, 10, 20 })]
        [TestCase(0, 20, 0, new double[0])]
        [TestCase(20, 0, 10, new double[] { 0, 10, 20 })]
        [TestCase(-20, 0, 10, new double[] { -20, -10, 0 })]
        [TestCase(-10, 10, 10, new double[] { -10, 0, 10 })]
        public void CreateTicks(double min, double max, double freq, double[] expected)
        {
            var ticks = TickHelper.CreateTicks(min, max, freq).ToArray();
            CollectionAssert.AreEqual(expected, ticks);
        }

        [Test]
        public void ToPos(double tick, double min, double max)
        {
            Assert.Fail();
             //TickHelper.ToPos(tick,min,max)
        }
    }
}
