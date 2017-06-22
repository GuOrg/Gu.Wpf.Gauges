namespace Gu.Wpf.Gauges.Tests.Helpers
{
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    using NUnit.Framework;

    public class TextPositionTests
    {
        private readonly FormattedText text = new FormattedText("100", CultureInfo.InvariantCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 12, Brushes.Black);

        [TestCase(TickBarPlacement.Left, "0, -7")]
        [TestCase(TickBarPlacement.Top, "-10, 0")]
        [TestCase(TickBarPlacement.Right, "-20, -7")]
        [TestCase(TickBarPlacement.Bottom, "-10, -14")]
        public void HorizontalPoint(TickBarPlacement placement, string expected)
        {
            var textPositionOptions = new TextPositionOptions(placement, TextOrientation.Horizontal);
            var textSize = new Size(this.text.Width, this.text.Height);
            var textPosition = new TextPosition(textSize, textPositionOptions, new Point(0, 0), 0);
            Assert.AreEqual(expected, textPosition.Point.ToString("F0"));
        }

        [TestCase(TickBarPlacement.Left, "0, 10")]
        [TestCase(TickBarPlacement.Top, "-7, 20")]
        [TestCase(TickBarPlacement.Right, "-14, 10")]
        [TestCase(TickBarPlacement.Bottom, "-7, 0")]
        public void VerticalUpPoint(TickBarPlacement placement, string expected)
        {
            var textPositionOptions = new TextPositionOptions(placement, TextOrientation.VerticalUp);
            var textSize = new Size(this.text.Width, this.text.Height);
            var textPosition = new TextPosition(textSize, textPositionOptions, new Point(0, 0), 0);
            Assert.AreEqual(expected, textPosition.Point.ToString("F0"));
        }
    }
}
