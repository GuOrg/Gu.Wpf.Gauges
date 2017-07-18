namespace Gu.Wpf.Gauges.Tests.Internals
{
    using System.Linq;
    using System.Windows;
    using Gu.Wpf.Gauges.Tests.TestHelpers;
    using NUnit.Framework;

    public class TickHelperTests
    {
        [TestCase(0, 20, 10, TickSnap.TickFrequency, new double[] { 0, 10, 20 })]
        [TestCase(0, 20, 10, TickSnap.Minimum, new double[] { 0, 10, 20 })]
        [TestCase(0, 20, 10, TickSnap.Maximum, new double[] { 0, 10, 20 })]
        [TestCase(-20, 0, 10, TickSnap.TickFrequency, new double[] { -20, -10, 0 })]
        [TestCase(-20, 0, 10, TickSnap.Minimum, new double[] { -20, -10, 0 })]
        [TestCase(-20, 0, 10, TickSnap.Maximum, new double[] { -20, -10, 0 })]
        [TestCase(-10, 10, 10, TickSnap.TickFrequency, new double[] { -10, 0, 10 })]
        [TestCase(-10, 10, 10, TickSnap.Minimum, new double[] { -10, 0, 10 })]
        [TestCase(-10, 10, 10, TickSnap.Maximum, new double[] { -10, 0, 10 })]
        [TestCase(-1.2, 0.8, 1, TickSnap.TickFrequency, new double[] { -1, -0 })]
        [TestCase(0.8, 3, 1, TickSnap.TickFrequency, new double[] { 1, 2, 3 })]
        [TestCase(0.8, 3.2, 1, TickSnap.TickFrequency, new double[] { 1, 2, 3 })]
        [TestCase(-1.2, 0.8, 1, TickSnap.Minimum, new double[] { -1.2, -0.2, 0.8 })]
        [TestCase(-1, 1.2, 1, TickSnap.Maximum, new double[] { -0.8, 0.2, 1.2 })]
        public void CreateTicks(double min, double max, double frequency, TickSnap tickSnap, double[] expected)
        {
            var ticks = Ticks.Create(min, max, frequency, tickSnap).ToArray();
            CollectionAssert.AreEqual(expected, ticks, DoubleComparer.Default);
        }

        [TestCase(0, 20, 0, TickSnap.TickFrequency, new double[0])]
        [TestCase(0, 20, 0, TickSnap.Minimum, new double[0])]
        [TestCase(0, 20, 0, TickSnap.Maximum, new double[0])]
        [TestCase(20, 0, 10, TickSnap.TickFrequency, new double[0])]
        [TestCase(20, 0, 10, TickSnap.Minimum, new double[0])]
        [TestCase(20, 0, 10, TickSnap.Maximum, new double[0])]
        public void CreateReturnsEmptyWhen(double min, double max, double frequency, TickSnap tickSnap, double[] expected)
        {
            CollectionAssert.IsEmpty(Ticks.Create(min, max, frequency, tickSnap));
        }

        [TestCase(0, 0, 1, 0, 0)]
        [TestCase(1, 0, 1, 1, 0)]
        [TestCase(0, -1, 1, 0.5, 0)]
        [TestCase(1, -1, 1, 1, 0)]
        public void ToPos(double tick, double min, double max, double expectedX, double expectedY)
        {
            var point = Ticks.ToPos(tick, min, max, new Line(new Point(0, 0), new Point(1, 0)));
            Assert.AreEqual(expectedX, point.X);
            Assert.AreEqual(expectedY, point.Y);
        }

        [TestCase(0, 0, 1, 0)]
        [TestCase(1, 0, 1, 1)]
        [TestCase(0, -1, 1, 0.5)]
        [TestCase(1, -1, 1, 1)]
        public void ToAngle(double tick, double min, double max, double expected)
        {
            var actual = Ticks.ToAngle(tick, min, max, new ArcInfo(new Point(0, 0), 1, 0, 1));
            Assert.AreEqual(expected, actual);
        }
    }
}
