namespace Gu.Wpf.Gauges.Tests
{
    using NUnit.Framework;

    public class AngleTests
    {
        [TestCase("-1.2", -1.2)]
        [TestCase("-1.2°", -1.2)]
        [TestCase("-1.2°", -1.2)]
        [TestCase("-1.2 deg", -1.2)]
        [TestCase(" -1.2 deg", -1.2)]
        [TestCase("-1.2 Deg", -1.2)]
        [TestCase("-1.2 degrees", -1.2)]
        public void TryParseDegreesSuccess(string text, double expected)
        {
            Assert.AreEqual(true, Angle.TryParse(text, out Angle angle));
            Assert.AreEqual(expected, angle.Degrees);
            Assert.AreEqual(expected, Angle.Parse(text).Degrees);
        }

        [TestCase("-1.2 rad", -1.2)]
        [TestCase("-1.2 radians", -1.2)]
        [TestCase("-1.2 Radians", -1.2)]
        public void TryParseRadiansSuccess(string text, double expected)
        {
            Assert.AreEqual(true, Angle.TryParse(text, out Angle angle));
            Assert.AreEqual(expected, angle.Radians);
            Assert.AreEqual(expected, Angle.Parse(text).Radians);
        }

        [TestCase(1.2, 1.2, true)]
        [TestCase(0, 360, true)]
        public void Equals(double deg1, double deg2, bool expected)
        {
            var angle1 = Angle.FromDegrees(deg1);
            var angle2 = Angle.FromDegrees(deg2);
            Assert.AreEqual(expected, angle1 == angle2);
            Assert.AreEqual(expected, angle2 == angle1);
            Assert.AreEqual(!expected, angle1 != angle2);
            Assert.AreEqual(!expected, angle2 != angle1);
        }
    }
}
