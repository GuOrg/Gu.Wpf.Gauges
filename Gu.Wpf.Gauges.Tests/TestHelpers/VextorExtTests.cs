namespace Gu.Wpf.Gauges.Tests.Helpers
{
    using System.Windows.Media;

    using NUnit.Framework;

    public class VextorExtTests
    {
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
        public void Rotate(string vs, double angle)
        {
            var v = vs.AsVector();
            var transform = new RotateTransform(angle);
            var expected = transform.Value.Transform(v);
            var actual = v.Rotate(angle);
            Assert.AreEqual(expected.ToString("F2"), actual.ToString("F2"));
        }
    }
}
