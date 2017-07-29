namespace Gu.Wpf.Gauges.Tests.Internals
{
    using NUnit.Framework;

    public class DoubleUtilTests
    {
        [TestCase(1, 1, true)]
        [TestCase(0, 1, false)]
        public void AreClose(double x, double max, bool expected)
        {
            Assert.AreEqual(expected, DoubleUtil.AreClose(x, max));
        }

        [TestCase(0, 0, true)]
        [TestCase(0, 360, true)]
        [TestCase(1, 1, true)]
        [TestCase(0, 1, false)]
        public void AreCloseAngle(double x, double max, bool expected)
        {
            Assert.AreEqual(expected, DoubleUtil.AreClose(Angle.FromDegrees(x), Angle.FromDegrees(max)));
        }

        [TestCase(0, 1, true)]
        [TestCase(1, 1, false)]
        [TestCase(2, 1, false)]
        public void LessThan(double x, double max, bool expected)
        {
            Assert.AreEqual(expected, DoubleUtil.LessThan(x, max));
        }

        [TestCase(0, 1, true)]
        [TestCase(1, 1, true)]
        [TestCase(2, 1, false)]
        public void LessThanOrClose(double x, double max, bool expected)
        {
            Assert.AreEqual(expected, DoubleUtil.LessThanOrClose(x, max));
        }

        [TestCase(0, 1, false)]
        [TestCase(1, 1, false)]
        [TestCase(2, 1, true)]
        public void GreaterThan(double x, double min, bool expected)
        {
            Assert.AreEqual(expected, DoubleUtil.GreaterThan(x, min));
        }

        [TestCase(0, 1, false)]
        [TestCase(1, 1, true)]
        [TestCase(2, 1, true)]
        public void GreaterThanOrClose(double x, double min, bool expected)
        {
            Assert.AreEqual(expected, DoubleUtil.GreaterThanOrClose(x, min));
        }
    }
}
