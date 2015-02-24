namespace Gu.Gauges.Tests
{
    using System;
    using System.Globalization;
    using System.Windows;

    using NUnit.Framework;

    public class ArcTests
    {
        [TestCase("0,0", 2, 0, "2,0")]
        [TestCase("0,0", 2, 90, "0,2")]
        [TestCase("0,0", 2, -90, "0,-2")]
        [TestCase("0,0", 2, 180, "-2,0")]
        [TestCase("0,0", 2, -180, "-2,0")]
        [TestCase("1,2", 2, 0, "3,2")]
        [TestCase("1,2", 2, 90, "1,4")]
        [TestCase("1,2", 2, -90, "1,0")]
        [TestCase("1,2", 2, 180, "-1,2")]
        [TestCase("1,2", 2, -180, "-1,2")]
        public void GetPoint(string cps, double radius, double angle, string eps)
        {
            var centre = Parse(cps);
            var arc = new Arc(centre, 0, 0, radius, false);
            var actual = arc.GetPoint(angle);
            var expected = Parse(eps);
            Assert.AreEqual(expected.X, actual.X, 1e-6);
            Assert.AreEqual(expected.Y, actual.Y, 1e-6);
        }

        [TestCase("0,0", 2, 1, 0, "3,0")]
        [TestCase("0,0", 2, -1, 0, "1,0")]
        [TestCase("0,0", 2, 1, 90, "0,3")]
        [TestCase("0,0", 2, -1, -90, "0,-1")]
        [TestCase("0,0", 2, 1, 180, "-3,0")]
        [TestCase("0,0", 2, -1, -180, "-1,0")]
        [TestCase("1,2", 2, 1, 0, "4,2")]
        [TestCase("1,2", 2, 1, 90, "1,5")]
        [TestCase("1,2", 2, -1, -90, "1,1")]
        [TestCase("1,2", 2, 1, 180, "-2,2")]
        [TestCase("1,2", 2, -1, -180, "0,2")]
        public void GetPoint(string cps, double radius, double offset, double angle, string eps)
        {
            var centre = Parse(cps);
            var arc = new Arc(centre, 0, 0, radius, false);
            var actual = arc.GetPoint(angle, offset);
            var expected = Parse(eps);
            Console.WriteLine(actual);
            Assert.AreEqual(expected.X, actual.X, 1e-6);
            Assert.AreEqual(expected.Y, actual.Y, 1e-6);
        }

        private static Point Parse(string pointString)
        {
            var strings = pointString.Split(',');
            if (strings.Length != 2)
            {
                throw new ArgumentException("", "pointString");
            }
            var x = double.Parse(strings[0], CultureInfo.InvariantCulture);
            var y = double.Parse(strings[1], CultureInfo.InvariantCulture);
            return new Point(x, y);
        }
    }
}
