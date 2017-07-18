namespace Gu.Wpf.Gauges.Tests.Primitives.Angular
{
    using System.IO;
    using System.Threading;
    using System.Windows;
    using System.Windows.Media;
    using Gu.Wpf.Gauges.Tests.Helpers;
    using Gu.Wpf.Gauges.Tests.TestHelpers;
    using NUnit.Framework;

    [Apartment(ApartmentState.STA)]
    public class AngularTickBarTests
    {
        [TestCase(true, 1, 1, 1, null, null)]
        [TestCase(false, 1, 1, 1, null, null)]
        [TestCase(true, 1, 1, 1, "1 2 6", null)]
        [TestCase(false, 1, 1, 1, "1 2 6", null)]
        [TestCase(true, 1, 1, 1, null, "1")]
        [TestCase(false, 1, 1, 1, null, "1")]
        [TestCase(true, 1, 1, 1, "1 2 6", "1")]
        [TestCase(false, 1, 1, 1, "1 2 6", "1")]
        [TestCase(true, 2, 1, 1, null, null)]
        [TestCase(false, 2, 1, 1, null, null)]
        [TestCase(true, 2, 1, 1, "1 2 6", null)]
        [TestCase(false, 2, 1, 1, "1 2 6", null)]
        [TestCase(true, 2, 1, 1, null, "1.5")]
        [TestCase(false, 2, 1, 1, null, "1.5")]
        [TestCase(true, 2, 1, 1, "1 2 6", "1.5")]
        [TestCase(false, 2, 1, 1, "1 2 6", "1.5")]
        [TestCase(true, 3, 1, 1, null, null)]
        [TestCase(false, 3, 1, 1, null, null)]
        [TestCase(true, 3, 1, 1, "1 2 6", null)]
        [TestCase(false, 3, 1, 1, "1 2 6", null)]
        [TestCase(true, 3, 1, 1, null, "2")]
        [TestCase(false, 3, 1, 1, null, "2")]
        [TestCase(true, 3, 1, 1, "1 2 6", "2")]
        [TestCase(false, 3, 1, 1, "1 2 6", "2")]
        public void Render(bool isDirectionReversed, double tickWidth, double strokeThickness, double tickFrequency, string ticks, string padding)
        {
            var tickBar = new AngularTickBar
            {
                StrokeThickness = strokeThickness,
                Minimum = 0,
                Maximum = 10,
                TickFrequency = tickFrequency,
                Ticks = string.IsNullOrEmpty(ticks) ? new DoubleCollection() : DoubleCollection.Parse(ticks),
                Fill = Brushes.Red,
                TickWidth = tickWidth,
                Stroke = Brushes.Black,
                IsDirectionReversed = isDirectionReversed,
                Padding = padding.AsThickness(),
            };

            Assert.Inconclusive();
            ImageAssert.AreEqual(GetFileName(tickBar), tickBar);
        }

        [TestCase(false, 1, 1, "0 0 0 0", "0,0.5,0,0.5")]
        [TestCase(false, 2, 1, "0 0 0 0", "0,1.5,0,1.5")]
        [TestCase(false, 2, 0, "0 1 0 1", "0,0,0,0")]
        [TestCase(false, 2, 1, "0 1 0 1", "0,0.5,0,0.5")]
        [TestCase(false, 3, 1, "0 1 0 1", "0,1,0,1")]
        public void Overflow(bool isDirectionReversed, double tickWidth, double strokeThickness, string padding, string expected)
        {
            var tickBar = new AngularTickBar
            {
                StrokeThickness = strokeThickness,
                Minimum = 0,
                Maximum = 10,
                TickFrequency = 1,
                TickWidth = tickWidth,
                Stroke = Brushes.Black,
                Fill = Brushes.Red,
                IsDirectionReversed = isDirectionReversed,
                Padding = padding.AsThickness(),
            };

            var gauge = new AngularGauge { Content = tickBar };
            gauge.Arrange(new Rect(new Size(10, 10)));
            Assert.AreEqual(expected, gauge.ContentOverflow.ToString());
            Assert.AreEqual(expected, tickBar.Overflow.ToString());

            gauge.Measure(new Size(10, 10));
            gauge.Arrange(new Rect(new Size(10, 10)));
            Assert.AreEqual(expected, gauge.ContentOverflow.ToString());
            Assert.AreEqual(expected, tickBar.Overflow.ToString());
        }

        private static string GetFileName(AngularTickBar tickBar)
        {
            var ticks = tickBar.Ticks != null
                ? $"_Ticks_{tickBar.Ticks}"
                : string.Empty;

            var tickFrequency = tickBar.TickFrequency > 0
                ? $"_TickFrequency_{tickBar.TickFrequency}"
                : string.Empty;

            var padding = tickBar.Padding.IsZero()
                ? string.Empty
                : $"_Padding_{tickBar.Padding}";

            return $@"AngularTickBar_Min_{tickBar.Minimum}_Max_{tickBar.Maximum}_IsDirectionReversed_{tickBar.IsDirectionReversed}{padding}_TickWidth_{tickBar.TickWidth}_StrokeThickness_{tickBar.StrokeThickness}{tickFrequency}{ticks}.png"
                .Replace(" ", "_");
        }

        private static void SaveImage(AngularTickBar tickBar)
        {
            Directory.CreateDirectory(@"C:\Temp\AngularTickBar");
            tickBar.SaveImage(new Size(100, 100), $@"C:\Temp\AngularTickBar\{GetFileName(tickBar)}");
        }
    }
}