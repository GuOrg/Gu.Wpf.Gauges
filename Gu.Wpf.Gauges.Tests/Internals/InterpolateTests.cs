namespace Gu.Wpf.Gauges.Tests.Internals
{
    using NUnit.Framework;

    public class InterpolateTests
    {
        [TestCase(0, 0, 0, 0)]
        [TestCase(0, 1, 0, 0)]
        [TestCase(0, 1, 0.5, 0.5)]
        [TestCase(0, 1, 1, 1)]
        [TestCase(0, 10, 0, 0)]
        [TestCase(0, 10, 5, 0.5)]
        [TestCase(0, 10, 10, 1)]
        [TestCase(-1, 1, 0, 0.5)]
        [TestCase(-1, 1, -1, 0)]
        [TestCase(-1, 1, 1, 1)]
        public void Linear(double min, double max, double value, double expected)
        {
            Assert.AreEqual(expected, Interpolate.Linear(min, max, value).Value, 1E-6);
        }
    }
}
