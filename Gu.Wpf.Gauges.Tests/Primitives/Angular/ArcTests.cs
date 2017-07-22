namespace Gu.Wpf.Gauges.Tests.Primitives.Angular
{
    using System.Globalization;
    using System.IO;
    using System.Threading;
    using System.Windows;
    using System.Windows.Media;
    using Gu.Wpf.Gauges.Tests.Helpers;
    using Gu.Wpf.Gauges.Tests.TestHelpers;
    using NUnit.Framework;

    [Apartment(ApartmentState.STA)]
    public class ArcTests
    {
        [TestCase(0, 90, 0)]
        [TestCase(0, 90, 2)]
        [TestCase(0, 90, 10)]
        [TestCase(0, 90, 30)]
        [TestCase(0, 90, double.PositiveInfinity)]
        [TestCase(-140, 140, 0)]
        [TestCase(-140, 140, 2)]
        [TestCase(-140, 140, 10)]
        [TestCase(-140, 140, 30)]
        [TestCase(-140, 140, double.PositiveInfinity)]
        public void RenderNoStroke(double start, double end, double thickness)
        {
            var arc = new Arc
            {
                Minimum = 0,
                Maximum = 10,
                Fill = Brushes.Black,
                Start = start,
                End = end,
                Thickness = thickness,
            };

            ImageAssert.AreEqual(GetFileName(arc), arc);
        }

        [TestCase(0, 90, 0, 2)]
        [TestCase(0, 90, 2, 2)]
        [TestCase(0, 90, 10, 2)]
        [TestCase(0, 90, 30, 2)]
        [TestCase(0, 90, double.PositiveInfinity, 2)]
        [TestCase(-140, 140, 0, 2)]
        [TestCase(-140, 140, 2, 2)]
        [TestCase(-140, 140, 10, 2)]
        [TestCase(-140, 140, 30, 2)]
        [TestCase(-140, 140, double.PositiveInfinity, 2)]
        public void RenderWithStroke(double start, double end, double thickness, double strokeThickness)
        {
            var arc = new Arc
            {
                Minimum = 0,
                Maximum = 10,
                Fill = Brushes.Red,
                Stroke = Brushes.Black,
                StrokeDashArray = new DoubleCollection(new[] { 0.0, 1 }),
                StrokeDashCap = PenLineCap.Round,
                Start = start,
                End = end,
                StrokeThickness = strokeThickness,
                Thickness = thickness,
            };

            ImageAssert.AreEqual(GetFileName(arc), arc);
        }

        [TestCase(0, true, 0, 90, 10, 2)]
        [TestCase(0, true, 0, 90, 2, 2)]
        [TestCase(0, true, -140, 140, 10, 2)]
        [TestCase(0, true, -140, 140, 2, 2)]
        [TestCase(0, false, 0, 90, 10, 2)]
        [TestCase(0, false, 0, 90, 2, 2)]
        [TestCase(0, false, -140, 140, 10, 2)]
        [TestCase(0, false, -140, 140, 2, 2)]
        [TestCase(5, true, 0, 90, 10, 2)]
        [TestCase(5, true, 0, 90, 2, 2)]
        [TestCase(5, true, -140, 140, 10, 2)]
        [TestCase(5, true, -140, 140, 2, 2)]
        [TestCase(5, false, 0, 90, 10, 2)]
        [TestCase(5, false, 0, 90, 2, 2)]
        [TestCase(5, false, -140, 140, 10, 2)]
        [TestCase(5, false, -140, 140, 2, 2)]
        [TestCase(10, true, 0, 90, 10, 2)]
        [TestCase(10, true, 0, 90, 2, 2)]
        [TestCase(10, true, -140, 140, 10, 2)]
        [TestCase(10, true, -140, 140, 2, 2)]
        [TestCase(10, false, 0, 90, 10, 2)]
        [TestCase(10, false, 0, 90, 2, 2)]
        [TestCase(10, false, -140, 140, 10, 2)]
        [TestCase(10, false, -140, 140, 2, 2)]
        public void RenderWithStrokeAndValue(double value, bool isDirectionReversed, double start, double end, double thickness, double strokeThickness)
        {
            var arc = new Arc
            {
                Minimum = 0,
                Maximum = 10,
                Value = value,
                IsDirectionReversed = isDirectionReversed,
                Fill = Brushes.Red,
                Thickness = thickness,
                StrokeThickness = strokeThickness,
                Stroke = Brushes.Black,
                StrokeDashArray = new DoubleCollection(new[] { 0.0, 1 }),
                StrokeDashCap = PenLineCap.Round,
                Start = start,
                End = end,
            };

            ImageAssert.AreEqual(GetFileName(arc), arc);
        }

        [TestCase(false, 1, 1, "0 0 0 0", "0,0,0,1")]
        [TestCase(true, 1, 1, "0 0 0 0", "0,0,0,1")]
        [TestCase(false, 4, 1, "0 0 0 0", "0,0,0,1")]
        [TestCase(false, 2, 0, "0 0 0 1", "0,0,0,0")]
        [TestCase(false, 4, 2, "0 0 0 1", "0,0,0,0")]
        [TestCase(true, 4, 2, "0 0 0 1", "0,0,0,0")]
        public void Overflow(bool isDirectionReversed, double tickWidth, double strokeThickness, string padding, string expected)
        {
            var tickBar = new Arc
            {
                StrokeThickness = strokeThickness,
                Start = -140,
                End = 140,
                Minimum = 0,
                Maximum = 10,
                Stroke = Brushes.Black,
                Fill = Brushes.Red,
                IsDirectionReversed = isDirectionReversed,
                Padding = padding.AsThickness(),
            };

            var gauge = new AngularGauge { Content = tickBar };
            gauge.Arrange(new Rect(new Size(10, 10)));
            Assert.AreEqual(expected, tickBar.Overflow.ToString());
            Assert.AreEqual(expected, gauge.ContentOverflow.ToString());

            gauge.Measure(new Size(10, 10));
            gauge.Arrange(new Rect(new Size(10, 10)));
            Assert.AreEqual(expected, tickBar.Overflow.ToString());
            Assert.AreEqual(expected, gauge.ContentOverflow.ToString());
        }

        private static string GetFileName(Arc arc)
        {
            if (double.IsNaN(arc.Value) ||
                DoubleUtil.AreClose(arc.Value, arc.Maximum))
            {
                return $"Arc_Start_{arc.Start}_End_{arc.End}_Thickness_{arc.Thickness}_StrokeThickness_{arc.StrokeThickness}.png";
            }

            return DoubleUtil.IsZero(arc.Value)
                ? $"Arc_Value_0.png"
                : $"Arc_Value_{arc.Value}_Min_{arc.Minimum}_Max_{arc.Maximum}_IsDirectionReversed_{arc.IsDirectionReversed}_Start_{arc.Start}_End_{arc.End}_Thickness_{(double.IsInfinity(arc.Thickness) ? "inf" : arc.Thickness.ToString(CultureInfo.InvariantCulture))}_StrokeThickness_{arc.StrokeThickness}.png";
        }

        private static void SaveImage(Arc arc)
        {
            var directory = Directory.CreateDirectory($@"C:\Temp\Arc");
            arc.SaveImage(
                new Size(30, 30),
                Path.Combine(directory.FullName, GetFileName(arc)));
        }
    }
}