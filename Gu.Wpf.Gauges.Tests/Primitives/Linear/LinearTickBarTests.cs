namespace Gu.Wpf.Gauges.Tests.Primitives.Linear
{
    using System.IO;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using Gu.Wpf.Gauges.Tests.Helpers;
    using Gu.Wpf.Gauges.Tests.TestHelpers;
    using NUnit.Framework;

    [Apartment(ApartmentState.STA)]
    public class LinearTickBarTests
    {
        [TestCase(TickBarPlacement.Left, true, 1, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Left, false, 1, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Left, true, 1, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Left, false, 1, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Left, true, 1, 1, 1, null, "1")]
        [TestCase(TickBarPlacement.Left, false, 1, 1, 1, null, "1")]
        [TestCase(TickBarPlacement.Left, true, 1, 1, 1, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Left, false, 1, 1, 1, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Left, true, 2, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Left, false, 2, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Left, true, 2, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Left, false, 2, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Left, true, 2, 1, 1, null, "1.5")]
        [TestCase(TickBarPlacement.Left, false, 2, 1, 1, null, "1.5")]
        [TestCase(TickBarPlacement.Left, true, 2, 1, 1, "1 2 6", "1.5")]
        [TestCase(TickBarPlacement.Left, false, 2, 1, 1, "1 2 6", "1.5")]
        [TestCase(TickBarPlacement.Left, true, 3, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Left, false, 3, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Left, true, 3, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Left, false, 3, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Left, true, 3, 1, 1, null, "2")]
        [TestCase(TickBarPlacement.Left, false, 3, 1, 1, null, "2")]
        [TestCase(TickBarPlacement.Left, true, 3, 1, 1, "1 2 6", "2")]
        [TestCase(TickBarPlacement.Left, false, 3, 1, 1, "1 2 6", "2")]
        [TestCase(TickBarPlacement.Right, true, 1, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Right, false, 1, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Right, true, 1, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Right, false, 1, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Right, true, 1, 1, 1, null, "1")]
        [TestCase(TickBarPlacement.Right, false, 1, 1, 1, null, "1")]
        [TestCase(TickBarPlacement.Right, true, 1, 1, 1, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Right, false, 1, 1, 1, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Right, true, 2, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Right, false, 2, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Right, true, 2, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Right, false, 2, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Right, true, 2, 1, 1, null, "1.5")]
        [TestCase(TickBarPlacement.Right, false, 2, 1, 1, null, "1.5")]
        [TestCase(TickBarPlacement.Right, true, 2, 1, 1, "1 2 6", "1.5")]
        [TestCase(TickBarPlacement.Right, false, 2, 1, 1, "1 2 6", "1.5")]
        [TestCase(TickBarPlacement.Right, true, 3, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Right, false, 3, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Right, true, 3, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Right, false, 3, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Right, true, 3, 1, 1, null, "2")]
        [TestCase(TickBarPlacement.Right, false, 3, 1, 1, null, "2")]
        [TestCase(TickBarPlacement.Right, true, 3, 1, 1, "1 2 6", "2")]
        [TestCase(TickBarPlacement.Right, false, 3, 1, 1, "1 2 6", "2")]
        [TestCase(TickBarPlacement.Top, true, 1, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Top, false, 1, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Top, true, 1, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Top, false, 1, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Top, true, 1, 1, 1, null, "1")]
        [TestCase(TickBarPlacement.Top, false, 1, 1, 1, null, "1")]
        [TestCase(TickBarPlacement.Top, true, 1, 1, 1, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Top, false, 1, 1, 1, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Top, true, 2, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Top, false, 2, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Top, true, 2, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Top, false, 2, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Top, true, 2, 1, 1, null, "1.5")]
        [TestCase(TickBarPlacement.Top, false, 2, 1, 1, null, "1.5")]
        [TestCase(TickBarPlacement.Top, true, 2, 1, 1, "1 2 6", "1.5")]
        [TestCase(TickBarPlacement.Top, false, 2, 1, 1, "1 2 6", "1.5")]
        [TestCase(TickBarPlacement.Top, true, 3, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Top, false, 3, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Top, true, 3, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Top, false, 3, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Top, true, 3, 1, 1, null, "2")]
        [TestCase(TickBarPlacement.Top, false, 3, 1, 1, null, "2")]
        [TestCase(TickBarPlacement.Top, true, 3, 1, 1, "1 2 6", "2")]
        [TestCase(TickBarPlacement.Top, false, 3, 1, 1, "1 2 6", "2")]
        [TestCase(TickBarPlacement.Bottom, true, 1, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Bottom, false, 1, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Bottom, true, 1, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Bottom, false, 1, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Bottom, true, 1, 1, 1, null, "1")]
        [TestCase(TickBarPlacement.Bottom, false, 1, 1, 1, null, "1")]
        [TestCase(TickBarPlacement.Bottom, true, 1, 1, 1, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Bottom, false, 1, 1, 1, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Bottom, true, 2, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Bottom, false, 2, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Bottom, true, 2, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Bottom, false, 2, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Bottom, true, 2, 1, 1, null, "1.5")]
        [TestCase(TickBarPlacement.Bottom, false, 2, 1, 1, null, "1.5")]
        [TestCase(TickBarPlacement.Bottom, true, 2, 1, 1, "1 2 6", "1.5")]
        [TestCase(TickBarPlacement.Bottom, false, 2, 1, 1, "1 2 6", "1.5")]
        [TestCase(TickBarPlacement.Bottom, true, 3, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Bottom, false, 3, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Bottom, true, 3, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Bottom, false, 3, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Bottom, true, 3, 1, 1, null, "2")]
        [TestCase(TickBarPlacement.Bottom, false, 3, 1, 1, null, "2")]
        [TestCase(TickBarPlacement.Bottom, true, 3, 1, 1, "1 2 6", "2")]
        [TestCase(TickBarPlacement.Bottom, false, 3, 1, 1, "1 2 6", "2")]
        [TestCase(TickBarPlacement.Bottom, false, 3, 2, 0, "1 2 6", "1")]
        public void Render(TickBarPlacement placement, bool isDirectionReversed, double tickWidth, double strokeThickness, double tickFrequency, string ticks, string padding)
        {
            var tickBar = new LinearTickBar
            {
                StrokeThickness = strokeThickness,
                Minimum = 0,
                Maximum = 10,
                TickFrequency = tickFrequency,
                Ticks = string.IsNullOrEmpty(ticks) ? new DoubleCollection() : DoubleCollection.Parse(ticks),
                TickWidth = tickWidth,
                Stroke = Brushes.Black,
                Fill = Brushes.Red,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
                Padding = padding.AsThickness(),
            };

            ImageAssert.AreEqual(GetFileName(tickBar), tickBar);
        }

        [TestCase(TickBarPlacement.Left, 0, false, 1, 1, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Left, 0, true, 1, 1, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Left, 5, false, 1, 1, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Left, 5, true, 1, 1, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Left, 10, false, 1, 1, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Left, 10, true, 1, 1, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Left, 0, false, 3, 2, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Left, 0, true, 3, 2, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Left, 5, false, 3, 2, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Left, 5, true, 3, 2, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Left, 10, false, 3, 2, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Left, 10, true, 3, 2, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Right, 0, false, 1, 1, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Right, 0, true, 1, 1, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Right, 5, false, 1, 1, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Right, 5, true, 1, 1, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Right, 10, false, 1, 1, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Right, 10, true, 1, 1, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Right, 0, false, 3, 2, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Right, 0, true, 3, 2, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Right, 5, false, 3, 2, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Right, 5, true, 3, 2, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Right, 10, false, 3, 2, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Right, 10, true, 3, 2, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Bottom, 0, false, 1, 1, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Bottom, 0, true, 1, 1, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Bottom, 5, false, 1, 1, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Bottom, 5, true, 1, 1, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Bottom, 10, false, 1, 1, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Bottom, 10, true, 1, 1, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Bottom, 0, false, 3, 2, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Bottom, 0, true, 3, 2, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Bottom, 5, false, 3, 2, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Bottom, 5, true, 3, 2, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Bottom, 10, false, 3, 2, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Bottom, 10, true, 3, 2, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Top, 0, false, 1, 1, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Top, 0, true, 1, 1, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Top, 5, false, 1, 1, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Top, 5, true, 1, 1, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Top, 10, false, 1, 1, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Top, 10, true, 1, 1, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Top, 0, false, 3, 2, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Top, 0, true, 3, 2, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Top, 5, false, 3, 2, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Top, 5, true, 3, 2, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Top, 10, false, 3, 2, 0, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Top, 10, true, 3, 2, 0, "1 2 6", "1")]
        public void RenderWithValue(TickBarPlacement placement, double value, bool isDirectionReversed, double tickWidth, double strokeThickness, double tickFrequency, string ticks, string padding)
        {
            var tickBar = new LinearTickBar
            {
                StrokeThickness = strokeThickness,
                Minimum = 0,
                Maximum = 10,
                Value = value,
                TickFrequency = tickFrequency,
                Ticks = string.IsNullOrEmpty(ticks) ? new DoubleCollection() : DoubleCollection.Parse(ticks),
                TickWidth = tickWidth,
                Stroke = Brushes.Black,
                Fill = Brushes.Red,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
                Padding = padding.AsThickness(),
            };

            ImageAssert.AreEqual(GetFileName(tickBar), tickBar);
        }

        [TestCase(TickBarPlacement.Left, false, 1, 1, "0 0 0 0", "0,0.5,0,0.5")]
        [TestCase(TickBarPlacement.Left, false, 2, 1, "0 0 0 0", "0,1.5,0,1.5")]
        [TestCase(TickBarPlacement.Left, false, 2, 0, "0 1 0 1", "0,0,0,0")]
        [TestCase(TickBarPlacement.Left, false, 2, 1, "0 1 0 1", "0,0.5,0,0.5")]
        [TestCase(TickBarPlacement.Left, false, 3, 1, "0 1 0 1", "0,1,0,1")]
        [TestCase(TickBarPlacement.Left, true, 1, 1, "0 0 0 0", "0,0.5,0,0.5")]
        [TestCase(TickBarPlacement.Left, true, 2, 1, "0 0 0 0", "0,1.5,0,1.5")]
        [TestCase(TickBarPlacement.Left, true, 2, 0, "0 1 0 1", "0,0,0,0")]
        [TestCase(TickBarPlacement.Left, true, 2, 1, "0 1 0 1", "0,0.5,0,0.5")]
        [TestCase(TickBarPlacement.Left, true, 3, 1, "0 1 0 1", "0,1,0,1")]
        [TestCase(TickBarPlacement.Right, false, 1, 1, "0 0 0 0", "0,0.5,0,0.5")]
        [TestCase(TickBarPlacement.Right, false, 2, 1, "0 0 0 0", "0,1.5,0,1.5")]
        [TestCase(TickBarPlacement.Right, false, 2, 0, "0 1 0 1", "0,0,0,0")]
        [TestCase(TickBarPlacement.Right, false, 2, 1, "0 1 0 1", "0,0.5,0,0.5")]
        [TestCase(TickBarPlacement.Right, false, 3, 1, "0 1 0 1", "0,1,0,1")]
        [TestCase(TickBarPlacement.Right, true, 1, 1, "0 0 0 0", "0,0.5,0,0.5")]
        [TestCase(TickBarPlacement.Right, true, 2, 1, "0 0 0 0", "0,1.5,0,1.5")]
        [TestCase(TickBarPlacement.Right, true, 2, 0, "0 1 0 1", "0,0,0,0")]
        [TestCase(TickBarPlacement.Right, true, 2, 1, "0 1 0 1", "0,0.5,0,0.5")]
        [TestCase(TickBarPlacement.Right, true, 3, 1, "0 1 0 1", "0,1,0,1")]
        [TestCase(TickBarPlacement.Bottom, false, 1, 1, "0 0 0 0", "0.5,0,0.5,0")]
        [TestCase(TickBarPlacement.Bottom, false, 2, 1, "0 0 0 0", "1.5,0,1.5,0")]
        [TestCase(TickBarPlacement.Bottom, false, 2, 0, "1 0 1 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Bottom, false, 2, 1, "1 0 1 0", "0.5,0,0.5,0")]
        [TestCase(TickBarPlacement.Bottom, false, 3, 1, "1 0 1 0", "1,0,1,0")]
        [TestCase(TickBarPlacement.Bottom, true, 1, 1, "0 0 0 0", "0.5,0,0.5,0")]
        [TestCase(TickBarPlacement.Bottom, true, 2, 1, "0 0 0 0", "1.5,0,1.5,0")]
        [TestCase(TickBarPlacement.Bottom, true, 2, 0, "1 0 1 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Bottom, true, 2, 1, "1 0 1 0", "0.5,0,0.5,0")]
        [TestCase(TickBarPlacement.Bottom, true, 3, 1, "1 0 1 0", "1,0,1,0")]
        [TestCase(TickBarPlacement.Top, false, 1, 1, "0 0 0 0", "0.5,0,0.5,0")]
        [TestCase(TickBarPlacement.Top, false, 2, 1, "0 0 0 0", "1.5,0,1.5,0")]
        [TestCase(TickBarPlacement.Top, false, 2, 0, "1 0 1 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Top, false, 2, 1, "1 0 1 0", "0.5,0,0.5,0")]
        [TestCase(TickBarPlacement.Top, false, 3, 1, "1 0 1 0", "1,0,1,0")]
        [TestCase(TickBarPlacement.Top, true, 1, 1, "0 0 0 0", "0.5,0,0.5,0")]
        [TestCase(TickBarPlacement.Top, true, 2, 1, "0 0 0 0", "1.5,0,1.5,0")]
        [TestCase(TickBarPlacement.Top, true, 2, 0, "1 0 1 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Top, true, 2, 1, "1 0 1 0", "0.5,0,0.5,0")]
        [TestCase(TickBarPlacement.Top, true, 3, 1, "1 0 1 0", "1,0,1,0")]
        public void Overflow(TickBarPlacement placement, bool isDirectionReversed, double tickWidth, double strokeThickness, string padding, string expected)
        {
            var tickBar = new LinearTickBar
            {
                StrokeThickness = strokeThickness,
                Minimum = 0,
                Maximum = 10,
                TickFrequency = 1,
                TickWidth = tickWidth,
                Stroke = Brushes.Black,
                Fill = Brushes.Red,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
                Padding = padding.AsThickness(),
            };

            var gauge = new LinearGauge { Content = tickBar };
            gauge.Arrange(new Rect(new Size(10, 10)));
            Assert.AreEqual(expected, gauge.ContentOverflow.ToString());
            Assert.AreEqual(expected, tickBar.Overflow.ToString());

            gauge.Measure(new Size(10, 10));
            gauge.Arrange(new Rect(new Size(10, 10)));
            Assert.AreEqual(expected, gauge.ContentOverflow.ToString());
            Assert.AreEqual(expected, tickBar.Overflow.ToString());
        }

        private static string GetFileName(LinearTickBar tickBar)
        {
            var ticks = tickBar.Ticks != null
                ? $"_Ticks_{tickBar.Ticks}"
                : string.Empty;

            var tickFrequency = tickBar.TickFrequency > 0
                ? $"_TickFrequency_{tickBar.TickFrequency}"
                : string.Empty;

            var orientation = tickBar.Placement == TickBarPlacement.Left || tickBar.Placement == TickBarPlacement.Right
                ? "_Vertical"
                : "_Horizontal";

            var padding = tickBar.Padding.IsZero()
                ? string.Empty
                : $"_Padding_{tickBar.Padding}";

            var value = double.IsNaN(tickBar.Value) || tickBar.Value == tickBar.Maximum
                ? string.Empty
                : $"_Value_{tickBar.Value}";

            return $@"LinearTickBar_Min_{tickBar.Minimum}_Max_{tickBar.Maximum}{value}_IsDirectionReversed_{tickBar.IsDirectionReversed}{padding}_TickWidth_{tickBar.TickWidth}_StrokeThickness_{tickBar.StrokeThickness}{tickFrequency}{ticks}{orientation}.png"
                .Replace(" ", "_");
        }

        private static void SaveImage(LinearTickBar tickBar)
        {
            var size = tickBar.Placement == TickBarPlacement.Left || tickBar.Placement == TickBarPlacement.Right
                ? new Size(10, 100)
                : new Size(100, 10);
            Directory.CreateDirectory(@"C:\Temp\LinearTickBar");
            tickBar.SaveImage(size, $@"C:\Temp\LinearTickBar\{GetFileName(tickBar)}");
        }
    }
}