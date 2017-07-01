namespace Gu.Wpf.Gauges.Tests.Internals
{
    using System.Windows;
    using NUnit.Framework;

    public class RectExtTest
    {
        [TestCase("1 2 3 4", 0, 4)]
        [TestCase("1 2 3 4", 2, 4)]
        [TestCase("1 2 3 4", 8, 8)]
        public void SetLeft(string rs, double value, double expected)
        {
            var rect = Rect.Parse(rs);
            RectExt.SetLeft(ref rect, value);
            Assert.AreEqual(expected, rect.Right);
            Assert.AreEqual(value, rect.Left);
        }

        [TestCase("1 2 3 4", 0, 4)]
        [TestCase("1 2 3 4", 2, 4)]
        [TestCase("1 2 3 4", 8, 8)]
        public void WithLeft(string rs, double value, double expected)
        {
            var rect = Rect.Parse(rs);
            var actual = rect.WithLeft(value);
            Assert.AreEqual(expected, actual.Right);
            Assert.AreEqual(value, actual.Left);
        }

        [TestCase("1 2 3 4", 0, 1)]
        [TestCase("1 2 3 4", 2, 2)]
        public void TrimLeft(string rs, double value, double expected)
        {
            var rect = Rect.Parse(rs);
            var actual = rect.TrimLeft(value);
            Assert.AreEqual(rect.Right, actual.Right);
            Assert.AreEqual(expected, actual.Left);
        }

        [TestCase("1 2 3 4", 0, 0)]
        [TestCase("1 2 3 4", 2, 1)]
        [TestCase("1 2 3 4", 3, 1)]
        [TestCase("1 2 3 4", 10, 1)]
        public void SetRight(string rs, double value, double expected)
        {
            var rect = Rect.Parse(rs);
            RectExt.SetRight(ref rect, value);
            Assert.AreEqual(expected, rect.Left);
            Assert.AreEqual(value, rect.Right);
        }

        [TestCase("1 2 3 4", 2)]
        [TestCase("1 2 3 4", 3)]
        public void WithRight(string rs, double value)
        {
            var rect = Rect.Parse(rs);
            var actual = rect.WithRight(value);
            Assert.AreEqual(rect.Left, actual.Left);
            Assert.AreEqual(value, actual.Right);
        }

        [TestCase("1 2 3 4", 2, 2)]
        [TestCase("1 2 3 4", 7, 4)]
        public void TrimRight(string rs, double value, double expected)
        {
            var rect = Rect.Parse(rs);
            var actual = rect.TrimRight(value);
            Assert.AreEqual(rect.Left, actual.Left);
            Assert.AreEqual(expected, actual.Right);
        }

        [TestCase("1 2 3 4", 0, 6)]
        [TestCase("1 2 3 4", 3, 6)]
        [TestCase("1 2 3 4", 10, 10)]
        public void SetTop(string rs, double value, double expected)
        {
            var rect = Rect.Parse(rs);
            RectExt.SetTop(ref rect, value);
            Assert.AreEqual(expected, rect.Bottom);
            Assert.AreEqual(value, rect.Top);
        }

        [TestCase("1 2 3 4", 2)]
        [TestCase("1 2 3 4", 3)]
        public void WithTop(string rs, double value)
        {
            var rect = Rect.Parse(rs);
            var actual = rect.WithTop(value);
            Assert.AreEqual(rect.Bottom, actual.Bottom);
            Assert.AreEqual(value, actual.Top);
        }

        [TestCase("1 2 3 4", 1, 2)]
        [TestCase("1 2 3 4", 2, 2)]
        [TestCase("1 2 3 4", 5, 5)]
        public void TrimTop(string rs, double value, double expected)
        {
            var rect = Rect.Parse(rs);
            var actual = rect.TrimTop(value);
            Assert.AreEqual(rect.Bottom, actual.Bottom);
            Assert.AreEqual(expected, actual.Top);
        }

        [TestCase("1 2 3 4", 0, 0)]
        [TestCase("1 2 3 4", 3, 2)]
        [TestCase("1 2 3 4", 10, 2)]
        public void SetBottom(string rs, double value, double expected)
        {
            var rect = Rect.Parse(rs);
            RectExt.SetBottom(ref rect, value);
            Assert.AreEqual(expected, rect.Top);
            Assert.AreEqual(value, rect.Bottom);
        }

        [TestCase("1 2 3 4", 4)]
        [TestCase("1 2 3 4", 5)]
        public void WithBottom(string rs, double value)
        {
            var rect = Rect.Parse(rs);
            var actual = rect.WithBottom(value);
            Assert.AreEqual(rect.Top, actual.Top);
            Assert.AreEqual(value, actual.Bottom);
        }

        [TestCase("1 2 3 4", 2, 2)]
        [TestCase("1 2 3 4", 3, 3)]
        [TestCase("1 2 3 4", 7, 6)]
        public void TrimBottom(string rs, double value, double expected)
        {
            var rect = Rect.Parse(rs);
            var actual = rect.TrimBottom(value);
            Assert.AreEqual(rect.Top, actual.Top);
            Assert.AreEqual(expected, actual.Bottom);
        }
    }
}
