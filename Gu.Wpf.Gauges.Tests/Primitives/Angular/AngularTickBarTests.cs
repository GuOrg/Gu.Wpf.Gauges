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
    public class AngularTickBarTests
    {
        [TestCase(true, 10, 1, 1, 1, null, null)]
        [TestCase(false, 10, 1, 1, 1, null, null)]
        [TestCase(true, 10, 1, 1, 1, "1 2 6", null)]
        [TestCase(false, 10, 1, 1, 1, "1 2 6", null)]
        [TestCase(true, 10, 1, 1, 1, null, "1")]
        [TestCase(false, 10, 1, 1, 1, null, "1")]
        [TestCase(true, 10, 1, 1, 1, "1 2 6", "1")]
        [TestCase(false, 10, 1, 1, 1, "1 2 6", "1")]
        [TestCase(true, 10, 2, 1, 1, null, null)]
        [TestCase(false, 10, 2, 1, 1, null, null)]
        [TestCase(true, 10, 2, 1, 1, "1 2 6", null)]
        [TestCase(false, 10, 2, 1, 1, "1 2 6", null)]
        [TestCase(true, 10, 2, 1, 1, null, "1.5")]
        [TestCase(false, 10, 2, 1, 1, null, "1.5")]
        [TestCase(true, 10, 2, 1, 1, "1 2 6", "1.5")]
        [TestCase(false, 10, 2, 1, 1, "1 2 6", "1.5")]
        [TestCase(true, 10, 3, 1, 1, null, null)]
        [TestCase(false, 10, 3, 1, 1, null, null)]
        [TestCase(true, 10, 3, 1, 1, "1 2 6", null)]
        [TestCase(false, 10, 3, 1, 1, "1 2 6", null)]
        [TestCase(true, 10, 3, 1, 1, null, "2")]
        [TestCase(false, 10, 3, 1, 1, null, "2")]
        [TestCase(true, 10, 3, 1, 1, "1 2 6", "2")]
        [TestCase(false, 10, 3, 1, 1, "1 2 6", "2")]
        [TestCase(true, 3, 3, 1, 3, "1 2 6", "2")]
        [TestCase(false, 3, 3, 1, 3, "1 2 6", "2")]
        public void Render(bool isDirectionReversed, double thickness, double tickWidth, double strokeThickness, double tickFrequency, string ticks, string padding)
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
                Thickness = thickness,
                Stroke = Brushes.Black,
                IsDirectionReversed = isDirectionReversed,
                Padding = padding.AsThickness(),
            };

            ImageAssert.AreEqual(GetFileName(tickBar), tickBar);
        }

        [TestCase(true, 10, 0, 1, 1, 1, null, null)]
        [TestCase(false, 10, 0, 1, 1, 1, null, null)]
        [TestCase(true, 10, 0, 1, 1, 1, "1 2 6", null)]
        [TestCase(false, 10, 0, 1, 1, 1, "1 2 6", null)]
        [TestCase(true, 10, 0, 1, 1, 1, null, "1")]
        [TestCase(false, 10, 0, 1, 1, 1, null, "1")]
        [TestCase(true, 10, 0, 1, 1, 1, "1 2 6", "1")]
        [TestCase(false, 10, 0, 1, 1, 1, "1 2 6", "1")]
        [TestCase(true, 10, 5, 1, 1, 1, null, null)]
        [TestCase(false, 10, 5, 1, 1, 1, null, null)]
        [TestCase(true, 10, 5, 1, 1, 1, "1 2 6", null)]
        [TestCase(false, 10, 5, 1, 1, 1, "1 2 6", null)]
        [TestCase(true, 10, 5, 1, 1, 1, null, "1")]
        [TestCase(false, 10, 5, 1, 1, 1, null, "1")]
        [TestCase(true, 10, 5, 1, 1, 1, "1 2 6", "1")]
        [TestCase(false, 10, 5, 1, 1, 1, "1 2 6", "1")]
        [TestCase(true, 10, 10, 1, 1, 1, null, null)]
        [TestCase(false, 10, 10, 1, 1, 1, null, null)]
        [TestCase(true, 10, 10, 1, 1, 1, "1 2 6", null)]
        [TestCase(false, 10, 10, 1, 1, 1, "1 2 6", null)]
        [TestCase(true, 10, 10, 1, 1, 1, null, "1")]
        [TestCase(false, 10, 10, 1, 1, 1, null, "1")]
        [TestCase(true, 10, 10, 1, 1, 1, "1 2 6", "1")]
        [TestCase(false, 10, 10, 1, 1, 1, "1 2 6", "1")]
        [TestCase(true, 10, 0, 2, 1, 1, null, null)]
        [TestCase(false, 10, 0, 2, 1, 1, null, null)]
        [TestCase(true, 10, 0, 2, 1, 1, "1 2 6", null)]
        [TestCase(false, 10, 0, 2, 1, 1, "1 2 6", null)]
        [TestCase(true, 10, 0, 2, 1, 1, null, "1.5")]
        [TestCase(false, 10, 0, 2, 1, 1, null, "1.5")]
        [TestCase(true, 10, 0, 2, 1, 1, "1 2 6", "1.5")]
        [TestCase(false, 10, 0, 2, 1, 1, "1 2 6", "1.5")]
        [TestCase(true, 10, 5, 2, 1, 1, null, null)]
        [TestCase(false, 10, 5, 2, 1, 1, null, null)]
        [TestCase(true, 10, 5, 2, 1, 1, "1 2 6", null)]
        [TestCase(false, 10, 5, 2, 1, 1, "1 2 6", null)]
        [TestCase(true, 10, 5, 2, 1, 1, null, "1.5")]
        [TestCase(false, 10, 5, 2, 1, 1, null, "1.5")]
        [TestCase(true, 10, 5, 2, 1, 1, "1 2 6", "1.5")]
        [TestCase(false, 10, 5, 2, 1, 1, "1 2 6", "1.5")]
        [TestCase(true, 10, 5, 2, 0, 3, "1 2 6", "2")]
        [TestCase(false, 10, 5, 2, 0, 3, "1 2 6", "2")]
        [TestCase(true, 10, 10, 2, 1, 1, null, null)]
        [TestCase(false, 10, 10, 2, 1, 1, null, null)]
        [TestCase(true, 10, 10, 2, 1, 1, "1 2 6", null)]
        [TestCase(false, 10, 10, 2, 1, 1, "1 2 6", null)]
        [TestCase(true, 10, 10, 2, 1, 1, null, "1.5")]
        [TestCase(false, 10, 10, 2, 1, 1, null, "1.5")]
        [TestCase(true, 10, 10, 2, 1, 1, "1 2 6", "1.5")]
        [TestCase(false, 10, 10, 2, 1, 1, "1 2 6", "1.5")]
        [TestCase(true, 10, 0, 3, 1, 1, null, null)]
        [TestCase(false, 10, 0, 3, 1, 1, null, null)]
        [TestCase(true, 10, 0, 3, 1, 1, "1 2 6", null)]
        [TestCase(false, 10, 0, 3, 1, 1, "1 2 6", null)]
        [TestCase(true, 10, 0, 3, 1, 1, null, "2")]
        [TestCase(false, 10, 0, 3, 1, 1, null, "2")]
        [TestCase(true, 10, 0, 3, 1, 1, "1 2 6", "2")]
        [TestCase(false, 10, 0, 3, 1, 1, "1 2 6", "2")]
        [TestCase(true, 10, 5, 3, 1, 1, null, null)]
        [TestCase(false, 10, 5, 3, 1, 1, null, null)]
        [TestCase(true, 10, 5, 3, 1, 1, "1 2 6", null)]
        [TestCase(false, 10, 5, 3, 1, 1, "1 2 6", null)]
        [TestCase(true, 10, 5, 3, 1, 1, null, "2")]
        [TestCase(false, 10, 5, 3, 1, 1, null, "2")]
        [TestCase(true, 10, 5, 3, 1, 1, "1 2 6", "2")]
        [TestCase(false, 10, 5, 3, 1, 1, "1 2 6", "2")]
        [TestCase(true, 10, 10, 3, 1, 1, null, null)]
        [TestCase(false, 10, 10, 3, 1, 1, null, null)]
        [TestCase(true, 10, 10, 3, 1, 1, "1 2 6", null)]
        [TestCase(false, 10, 10, 3, 1, 1, "1 2 6", null)]
        [TestCase(true, 10, 10, 3, 1, 1, null, "2")]
        [TestCase(false, 10, 10, 3, 1, 1, null, "2")]
        [TestCase(true, 10, 10, 3, 1, 1, "1 2 6", "2")]
        [TestCase(false, 10, 10, 3, 1, 1, "1 2 6", "2")]
        [TestCase(true, 10, 10, 3, 0, 3, "1 2 6", "2")]
        [TestCase(false, 10, 10, 3, 0, 3, "1 2 6", "2")]
        public void RenderWithValue(bool isDirectionReversed, double thickness, double value, double tickWidth, double strokeThickness, double tickFrequency, string ticks, string padding)
        {
            var tickBar = new AngularTickBar
            {
                StrokeThickness = strokeThickness,
                Minimum = 0,
                Maximum = 10,
                Value = value,
                TickFrequency = tickFrequency,
                Ticks = string.IsNullOrEmpty(ticks) ? new DoubleCollection() : DoubleCollection.Parse(ticks),
                Fill = Brushes.Red,
                TickWidth = tickWidth,
                Thickness = thickness,
                Stroke = Brushes.Black,
                IsDirectionReversed = isDirectionReversed,
                Padding = padding.AsThickness(),
            };

            ImageAssert.AreEqual(GetFileName(tickBar), tickBar);
        }

        [TestCase(false, 1, 1, "0 0 0 0", "0,0,0,1")]
        [TestCase(false, 4, 1, "0 0 0 0", "0,0,0,2")]
        [TestCase(false, 2, 0, "0 0 0 1", "0,0,0,0")]
        [TestCase(false, 4, 1, "0 0 0 1", "0,0,0,1")]
        [TestCase(false, 4, 1, "0 1 0 1", "0,1,0,1")]
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
            Assert.AreEqual(expected, tickBar.Overflow.ToString());
            Assert.AreEqual(expected, gauge.ContentOverflow.ToString());

            gauge.Measure(new Size(10, 10));
            gauge.Arrange(new Rect(new Size(10, 10)));
            Assert.AreEqual(expected, tickBar.Overflow.ToString());
            Assert.AreEqual(expected, gauge.ContentOverflow.ToString());
        }

        private static string GetFileName(AngularTickBar tickBar)
        {
            if (tickBar.Value == 0)
            {
                return "AngularTickBar_Value=0.png";
            }

            var ticks = tickBar.Ticks != null
                ? $"_Ticks_{tickBar.Ticks}"
                : string.Empty;

            var tickFrequency = tickBar.TickFrequency > 0
                ? $"_TickFrequency_{tickBar.TickFrequency}"
                : string.Empty;

            var padding = tickBar.Padding.IsZero()
                ? string.Empty
                : $"_Padding_{tickBar.Padding}";

            var value = double.IsNaN(tickBar.Value) || tickBar.Value == tickBar.Maximum
                ? string.Empty
                : $"_Value_{tickBar.Value}";
            var thickness = double.IsInfinity(tickBar.Thickness) ? "inf" : tickBar.Thickness.ToString(CultureInfo.InvariantCulture);
            return $@"AngularTickBar_Min_{tickBar.Minimum}_Max_{tickBar.Maximum}{value}_IsDirectionReversed_{tickBar.IsDirectionReversed}{padding}_TickWidth_{tickBar.TickWidth}_StrokeThickness_{tickBar.StrokeThickness}_Thickness_{thickness}{tickFrequency}{ticks}.png"
                .Replace(" ", "_");
        }

        private static void SaveImage(AngularTickBar tickBar)
        {
            Directory.CreateDirectory(@"C:\Temp\AngularTickBar");
            tickBar.SaveImage(new Size(100, 100), $@"C:\Temp\AngularTickBar\{GetFileName(tickBar)}");
        }
    }
}