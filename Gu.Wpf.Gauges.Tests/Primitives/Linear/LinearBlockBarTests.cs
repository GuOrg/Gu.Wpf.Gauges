namespace Gu.Wpf.Gauges.Tests.Primitives.Linear
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using NUnit.Framework;

    [Apartment(ApartmentState.STA)]
    public class LinearBlockBarTests
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
        public void Render(TickBarPlacement placement, bool isDirectionReversed, double tickGap, double strokeThickness, double tickFrequency, string ticks, string padding)
        {
            var tickBar = new LinearBlockBar
            {
                StrokeThickness = strokeThickness,
                Minimum = 0,
                Maximum = 10,
                TickFrequency = tickFrequency,
                Ticks = string.IsNullOrEmpty(ticks) ? new DoubleCollection() : DoubleCollection.Parse(ticks),
                TickGap = tickGap,
                Stroke = Brushes.Black,
                Fill = Brushes.Red,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
                Padding = padding.AsThickness(),
            };

            ImageAssert.AreEqual(GetFileName(tickBar), tickBar);
        }

        [TestCase(TickBarPlacement.Left, true, 5.95)]
        [TestCase(TickBarPlacement.Left, false, 5.95)]
        [TestCase(TickBarPlacement.Right, true, 5.95)]
        [TestCase(TickBarPlacement.Right, false, 5.95)]
        [TestCase(TickBarPlacement.Left, true, 6)]
        [TestCase(TickBarPlacement.Left, false, 6)]
        [TestCase(TickBarPlacement.Right, true, 6)]
        [TestCase(TickBarPlacement.Right, false, 6)]
        [TestCase(TickBarPlacement.Left, true, 6.05)]
        [TestCase(TickBarPlacement.Left, false, 6.05)]
        [TestCase(TickBarPlacement.Right, true, 6.05)]
        [TestCase(TickBarPlacement.Right, false, 6.05)]
        [TestCase(TickBarPlacement.Top, true, 5.95)]
        [TestCase(TickBarPlacement.Top, false, 5.95)]
        [TestCase(TickBarPlacement.Bottom, true, 5.95)]
        [TestCase(TickBarPlacement.Bottom, false, 5.95)]
        [TestCase(TickBarPlacement.Top, true, 6)]
        [TestCase(TickBarPlacement.Top, false, 6)]
        [TestCase(TickBarPlacement.Bottom, true, 6)]
        [TestCase(TickBarPlacement.Bottom, false, 6)]
        [TestCase(TickBarPlacement.Top, true, 6.05)]
        [TestCase(TickBarPlacement.Top, false, 6.05)]
        [TestCase(TickBarPlacement.Bottom, true, 6.05)]
        [TestCase(TickBarPlacement.Bottom, false, 6.05)]
        public void RenderWithValue(TickBarPlacement placement, bool isDirectionReversed, double value)
        {
            var blockBar = new LinearBlockBar
            {
                Minimum = 0,
                Maximum = 10,
                Ticks = new DoubleCollection(new double[] { 1, 2, 6 }),
                Fill = Brushes.Black,
                Stroke = Brushes.Red,
                StrokeThickness = 1,
                TickGap = 1,
                Value = value,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
            };

            ImageAssert.AreEqual(GetFileName(blockBar), blockBar);
        }

        [TestCase(TickBarPlacement.Left, true, 1, "0 0 0 0", "0,0.5,0,0.5")]
        [TestCase(TickBarPlacement.Left, true, 2, "0 0 0 0", "0,1,0,1")]
        [TestCase(TickBarPlacement.Left, true, 1, "0 0.5 0 0.5", "0,0,0,0")]
        [TestCase(TickBarPlacement.Left, true, 1, "0 1 0 1", "0,0,0,0")]
        [TestCase(TickBarPlacement.Left, true, 0, "0 0 0 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Left, false, 1, "0 0 0 0", "0,0.5,0,0.5")]
        [TestCase(TickBarPlacement.Left, false, 2, "0 0 0 0", "0,1,0,1")]
        [TestCase(TickBarPlacement.Left, false, 1, "0 0.5 0 0.5", "0,0,0,0")]
        [TestCase(TickBarPlacement.Left, false, 1, "0 1 0 1", "0,0,0,0")]
        [TestCase(TickBarPlacement.Left, false, 0, "0 0 0 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Right, true, 1, "0 0 0 0", "0,0.5,0,0.5")]
        [TestCase(TickBarPlacement.Right, true, 2, "0 0 0 0", "0,1,0,1")]
        [TestCase(TickBarPlacement.Right, true, 1, "0 0.5 0 0.5", "0,0,0,0")]
        [TestCase(TickBarPlacement.Right, true, 1, "0 1 0 1", "0,0,0,0")]
        [TestCase(TickBarPlacement.Right, true, 0, "0 0 0 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Right, false, 1, "0 0 0 0", "0,0.5,0,0.5")]
        [TestCase(TickBarPlacement.Right, false, 2, "0 0 0 0", "0,1,0,1")]
        [TestCase(TickBarPlacement.Right, false, 1, "0 0.5 0 0.5", "0,0,0,0")]
        [TestCase(TickBarPlacement.Right, false, 1, "0 1 0 1", "0,0,0,0")]
        [TestCase(TickBarPlacement.Right, false, 0, "0 0 0 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Top, true, 1, "0 0 0 0", "0.5,0,0.5,0")]
        [TestCase(TickBarPlacement.Top, true, 2, "0 0 0 0", "1,0,1,0")]
        [TestCase(TickBarPlacement.Top, true, 1, "0.5 0 0.5 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Top, true, 1, "1 0 1 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Top, true, 0, "0 0 0 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Top, false, 1, "0 0 0 0", "0.5,0,0.5,0")]
        [TestCase(TickBarPlacement.Top, false, 2, "0 0 0 0", "1,0,1,0")]
        [TestCase(TickBarPlacement.Top, false, 1, "0.5 0 0.5 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Top, false, 1, "1 0 1 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Top, false, 0, "0 0 0 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Bottom, true, 1, "0 0 0 0", "0.5,0,0.5,0")]
        [TestCase(TickBarPlacement.Bottom, true, 2, "0 0 0 0", "1,0,1,0")]
        [TestCase(TickBarPlacement.Bottom, true, 1, "0.5 0 0.5 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Bottom, true, 1, "1 0 1 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Bottom, true, 0, "0 0 0 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Bottom, false, 1, "0 0 0 0", "0.5,0,0.5,0")]
        [TestCase(TickBarPlacement.Bottom, false, 2, "0 0 0 0", "1,0,1,0")]
        [TestCase(TickBarPlacement.Bottom, false, 1, "0.5 0 0.5 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Bottom, false, 1, "1 0 1 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Bottom, false, 0, "0 0 0 0", "0,0,0,0")]
        public void Overflow(TickBarPlacement placement, bool isDirectionReversed, double strokeThickness, string padding, string expected)
        {
            var tickBar = new LinearBlockBar
            {
                StrokeThickness = strokeThickness,
                Minimum = 0,
                Maximum = 10,
                TickFrequency = 1,
                TickGap = 3,
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

        private static string GetFileName(LinearBlockBar tickBar)
        {
            var value = double.IsNaN(tickBar.Value)
                ? string.Empty
                : $"_Value_{Math.Round(tickBar.Value, 0)}";

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

            return $@"LinearBlockBar_Min_{tickBar.Minimum}_Max_{tickBar.Maximum}{value}_IsDirectionReversed_{tickBar.IsDirectionReversed}{padding}_TickGap_{tickBar.TickGap}_StrokeThickness_{tickBar.StrokeThickness}{tickFrequency}{ticks}{orientation}.png"
                .Replace(" ", "_");
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private static void SaveImage(LinearBlockBar tickBar)
        {
            var size = tickBar.Placement.IsHorizontal()
                ? new Size(100, 10)
                : new Size(10, 100);
            Directory.CreateDirectory(@"C:\Temp\LinearBlockBar");
            tickBar.SaveImage(size, $@"C:\Temp\LinearBlockBar\{GetFileName(tickBar)}");
        }
    }
}