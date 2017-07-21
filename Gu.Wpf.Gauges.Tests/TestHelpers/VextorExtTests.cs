namespace Gu.Wpf.Gauges.Tests.Helpers
{
    using System;
    using System.Windows.Media;

    using NUnit.Framework;

    public class VextorExtTests
    {
        [TestCase("1,0", 0, "1, 0")]
        [TestCase("1,0", 90, "0, 1")]
        [TestCase("1,0", -90, "0, -1")]
        public void Rotate(string vs, double angle, string expected)
        {
            var actual = vs.AsVector().Rotate(angle);
            Assert.AreEqual(expected, actual.ToString("F0"));
        }

        [TestCase("1,2", 0)]
        [TestCase("1,2", 90)]
        [TestCase("1,2", -90)]
        [TestCase("1,2", 180)]
        [TestCase("-1,2", 270)]
        [TestCase("-1,2", 0)]
        [TestCase("-1,2", 90)]
        [TestCase("-1,2", -90)]
        [TestCase("-1,2", 180)]
        [TestCase("-1,2", 270)]
        public void RotateRadians(string vs, double angle)
        {
            var v = vs.AsVector();
            var transform = new RotateTransform(angle);
            var expected = transform.Value.Transform(v);
            var actual = v.RotateRadians(angle * Math.PI / 180);
            Assert.AreEqual(expected.ToString("F2"), actual.ToString("F2"));
        }
    }
}
