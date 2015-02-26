namespace Gu.Gauges.Tests
{
    using System;
    using NUnit.Framework;

    public class ArcTests
    {
        [TestCase("0,0", 2, 0, "2, 0")]
        [TestCase("0,0", 2, 90, "0, 2")]
        [TestCase("0,0", 2, -90, "0, -2")]
        [TestCase("0,0", 2, 180, "-2, 0")]
        [TestCase("0,0", 2, -180, "-2, 0")]
        [TestCase("1,2", 2, 0, "3, 2")]
        [TestCase("1,2", 2, 90, "1, 4")]
        [TestCase("1,2", 2, -90, "1, 0")]
        [TestCase("1,2", 2, 180, "-1, 2")]
        [TestCase("1,2", 2, -180, "-1, 2")]
        public void GetPoint(string cps, double radius, double angle, string expected)
        {
            var centre = cps.AsPoint();
            var arc = new Arc(centre, 0, 0, radius, false);
            var actual = arc.GetPoint(angle);
            Assert.AreEqual(expected, actual.ToString("F0"));
        }

        [TestCase("0,0", 2, 1, 0, "3, 0")]
        [TestCase("0,0", 2, -1, 0, "1, 0")]
        [TestCase("0,0", 2, 1, 90, "0, 3")]
        [TestCase("0,0", 2, -1, -90, "0, -1")]
        [TestCase("0,0", 2, 1, 180, "-3, 0")]
        [TestCase("0,0", 2, -1, -180, "-1, 0")]
        [TestCase("1,2", 2, 1, 0, "4, 2")]
        [TestCase("1,2", 2, 1, 90, "1, 5")]
        [TestCase("1,2", 2, -1, -90, "1, 1")]
        [TestCase("1,2", 2, 1, 180, "-2, 2")]
        [TestCase("1,2", 2, -1, -180, "0, 2")]
        public void GetPoint(string cps, double radius, double offset, double angle, string expected)
        {
            var centre = cps.AsPoint();
            var arc = new Arc(centre, 0, 0, radius, false);
            var actual = arc.GetPoint(angle, offset);
            Assert.AreEqual(expected, actual.ToString("F0"));
        }
    }
}
