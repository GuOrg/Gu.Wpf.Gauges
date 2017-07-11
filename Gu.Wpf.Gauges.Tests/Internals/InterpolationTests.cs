namespace Gu.Wpf.Gauges.Tests.Internals
{
    using NUnit.Framework;

    public class InterpolationTests
    {
        [TestCase(0, 0, 10, false, 0)]
        [TestCase(0, 0, 10, true, 10)]
        [TestCase(0.5, 0, 10, false, 5)]
        [TestCase(0.5, 0, 10, true, 5)]
        [TestCase(1, 0, 10, false, 10)]
        [TestCase(1, 0, 10, true, 0)]
        public void Interpolate(double value, double min, double max, bool isDirectionReversed, double expected)
        {
            var interpolation = new Interpolation(value);
            Assert.AreEqual(expected, interpolation.Interpolate(min, max, isDirectionReversed));
        }
    }
}