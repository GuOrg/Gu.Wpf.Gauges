namespace Gu.Wpf.Gauges.Tests.Helpers
{
    using System.Linq;
    using System.Windows;

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

        [TestCase(0, 0, 1, 0, 0)]
        [TestCase(1, 0, 1, 1, 0)]
        [TestCase(0, -1, 1, 0.5, 0)]
        [TestCase(1, -1, 1, 1, 0)]
        public void ToPos(double tick, double min, double max, double expectedX, double expectedY)
        {
            var point = TickHelper.ToPos(tick, min, max, new Line(new Point(0, 0), new Point(1, 0)));
            Assert.AreEqual(expectedX, point.X);
            Assert.AreEqual(expectedY, point.Y);
        }

        [TestCase(0, 0, 1, 0)]
        [TestCase(1, 0, 1, 1)]
        [TestCase(0, -1, 1, 0.5)]
        [TestCase(1, -1, 1, 1)]
        public void ToAngle(double tick, double min, double max, double expected)
        {
            var actual = TickHelper.ToAngle(tick, min, max, new Arc(new Point(0, 0), 0, 1, 1, isDirectionReversed: false));
            Assert.AreEqual(expected, actual);
        }
    }
}
