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
    using Brushes = System.Windows.Media.Brushes;
    using Size = System.Windows.Size;

    [Apartment(ApartmentState.STA)]
    public class LinearTextBarTests
    {
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Left, VerticalTextAlignment.Top, false, null)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Center, VerticalTextAlignment.Top, false, null)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Right, VerticalTextAlignment.Top, false, null)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Left, VerticalTextAlignment.Center, false, null)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Center, VerticalTextAlignment.Center, false, null)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Right, VerticalTextAlignment.Center, false, null)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Left, VerticalTextAlignment.Bottom, false, null)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Center, VerticalTextAlignment.Bottom, false, null)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Right, VerticalTextAlignment.Bottom, false, null)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Left, VerticalTextAlignment.Top, true, null)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Center, VerticalTextAlignment.Top, true, null)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Right, VerticalTextAlignment.Top, true, null)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Left, VerticalTextAlignment.Center, true, null)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Center, VerticalTextAlignment.Center, true, null)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Right, VerticalTextAlignment.Center, true, null)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Left, VerticalTextAlignment.Bottom, true, null)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Center, VerticalTextAlignment.Bottom, true, null)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Right, VerticalTextAlignment.Bottom, true, null)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Left, VerticalTextAlignment.Top, false, "1")]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Center, VerticalTextAlignment.Top, false, "1")]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Right, VerticalTextAlignment.Top, false, "1")]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Left, VerticalTextAlignment.Center, false, "1")]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Center, VerticalTextAlignment.Center, false, "1")]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Right, VerticalTextAlignment.Center, false, "1")]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Left, VerticalTextAlignment.Bottom, false, "1")]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Center, VerticalTextAlignment.Bottom, false, "1")]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Right, VerticalTextAlignment.Bottom, false, "1")]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Left, VerticalTextAlignment.Top, true, "1")]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Center, VerticalTextAlignment.Top, true, "1")]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Right, VerticalTextAlignment.Top, true, "1")]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Left, VerticalTextAlignment.Center, true, "1")]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Center, VerticalTextAlignment.Center, true, "1")]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Right, VerticalTextAlignment.Center, true, "1")]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Left, VerticalTextAlignment.Bottom, true, "1")]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Center, VerticalTextAlignment.Bottom, true, "1")]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Right, VerticalTextAlignment.Bottom, true, "1")]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Left, VerticalTextAlignment.Top, false, null)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Center, VerticalTextAlignment.Top, false, null)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Right, VerticalTextAlignment.Top, false, null)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Left, VerticalTextAlignment.Center, false, null)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Center, VerticalTextAlignment.Center, false, null)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Right, VerticalTextAlignment.Center, false, null)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Left, VerticalTextAlignment.Bottom, false, null)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Center, VerticalTextAlignment.Bottom, false, null)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Right, VerticalTextAlignment.Bottom, false, null)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Left, VerticalTextAlignment.Top, true, null)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Center, VerticalTextAlignment.Top, true, null)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Right, VerticalTextAlignment.Top, true, null)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Left, VerticalTextAlignment.Center, true, null)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Center, VerticalTextAlignment.Center, true, null)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Right, VerticalTextAlignment.Center, true, null)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Left, VerticalTextAlignment.Bottom, true, null)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Center, VerticalTextAlignment.Bottom, true, null)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Right, VerticalTextAlignment.Bottom, true, null)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Left, VerticalTextAlignment.Top, false, "1")]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Center, VerticalTextAlignment.Top, false, "1")]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Right, VerticalTextAlignment.Top, false, "1")]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Left, VerticalTextAlignment.Center, false, "1")]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Center, VerticalTextAlignment.Center, false, "1")]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Right, VerticalTextAlignment.Center, false, "1")]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Left, VerticalTextAlignment.Bottom, false, "1")]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Center, VerticalTextAlignment.Bottom, false, "1")]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Right, VerticalTextAlignment.Bottom, false, "1")]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Left, VerticalTextAlignment.Top, true, "1")]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Center, VerticalTextAlignment.Top, true, "1")]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Right, VerticalTextAlignment.Top, true, "1")]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Left, VerticalTextAlignment.Center, true, "1")]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Center, VerticalTextAlignment.Center, true, "1")]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Right, VerticalTextAlignment.Center, true, "1")]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Left, VerticalTextAlignment.Bottom, true, "1")]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Center, VerticalTextAlignment.Bottom, true, "1")]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Right, VerticalTextAlignment.Bottom, true, "1")]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Left, VerticalTextAlignment.Top, false, null)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Center, VerticalTextAlignment.Top, false, null)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Right, VerticalTextAlignment.Top, false, null)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Left, VerticalTextAlignment.Center, false, null)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Center, VerticalTextAlignment.Center, false, null)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Right, VerticalTextAlignment.Center, false, null)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Left, VerticalTextAlignment.Bottom, false, null)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Center, VerticalTextAlignment.Bottom, false, null)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Right, VerticalTextAlignment.Bottom, false, null)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Left, VerticalTextAlignment.Top, true, null)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Center, VerticalTextAlignment.Top, true, null)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Right, VerticalTextAlignment.Top, true, null)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Left, VerticalTextAlignment.Center, true, null)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Center, VerticalTextAlignment.Center, true, null)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Right, VerticalTextAlignment.Center, true, null)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Left, VerticalTextAlignment.Bottom, true, null)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Center, VerticalTextAlignment.Bottom, true, null)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Right, VerticalTextAlignment.Bottom, true, null)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Left, VerticalTextAlignment.Top, false, "1")]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Center, VerticalTextAlignment.Top, false, "1")]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Right, VerticalTextAlignment.Top, false, "1")]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Left, VerticalTextAlignment.Center, false, "1")]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Center, VerticalTextAlignment.Center, false, "1")]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Right, VerticalTextAlignment.Center, false, "1")]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Left, VerticalTextAlignment.Bottom, false, "1")]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Center, VerticalTextAlignment.Bottom, false, "1")]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Right, VerticalTextAlignment.Bottom, false, "1")]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Left, VerticalTextAlignment.Top, true, "1")]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Center, VerticalTextAlignment.Top, true, "1")]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Right, VerticalTextAlignment.Top, true, "1")]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Left, VerticalTextAlignment.Center, true, "1")]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Center, VerticalTextAlignment.Center, true, "1")]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Right, VerticalTextAlignment.Center, true, "1")]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Left, VerticalTextAlignment.Bottom, true, "1")]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Center, VerticalTextAlignment.Bottom, true, "1")]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Right, VerticalTextAlignment.Bottom, true, "1")]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Left, VerticalTextAlignment.Top, false, null)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Center, VerticalTextAlignment.Top, false, null)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Right, VerticalTextAlignment.Top, false, null)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Left, VerticalTextAlignment.Center, false, null)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Center, VerticalTextAlignment.Center, false, null)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Right, VerticalTextAlignment.Center, false, null)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Left, VerticalTextAlignment.Bottom, false, null)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Center, VerticalTextAlignment.Bottom, false, null)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Right, VerticalTextAlignment.Bottom, false, null)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Left, VerticalTextAlignment.Top, true, null)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Center, VerticalTextAlignment.Top, true, null)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Right, VerticalTextAlignment.Top, true, null)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Left, VerticalTextAlignment.Center, true, null)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Center, VerticalTextAlignment.Center, true, null)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Right, VerticalTextAlignment.Center, true, null)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Left, VerticalTextAlignment.Bottom, true, null)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Center, VerticalTextAlignment.Bottom, true, null)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Right, VerticalTextAlignment.Bottom, true, null)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Left, VerticalTextAlignment.Top, false, "1")]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Center, VerticalTextAlignment.Top, false, "1")]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Right, VerticalTextAlignment.Top, false, "1")]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Left, VerticalTextAlignment.Center, false, "1")]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Center, VerticalTextAlignment.Center, false, "1")]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Right, VerticalTextAlignment.Center, false, "1")]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Left, VerticalTextAlignment.Bottom, false, "1")]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Center, VerticalTextAlignment.Bottom, false, "1")]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Right, VerticalTextAlignment.Bottom, false, "1")]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Left, VerticalTextAlignment.Top, true, "1")]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Center, VerticalTextAlignment.Top, true, "1")]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Right, VerticalTextAlignment.Top, true, "1")]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Left, VerticalTextAlignment.Center, true, "1")]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Center, VerticalTextAlignment.Center, true, "1")]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Right, VerticalTextAlignment.Center, true, "1")]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Left, VerticalTextAlignment.Bottom, true, "1")]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Center, VerticalTextAlignment.Bottom, true, "1")]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Right, VerticalTextAlignment.Bottom, true, "1")]
        public void RenderExplicitTextPosition(TickBarPlacement placement, HorizontalTextAlignment horizontalTextAlignment, VerticalTextAlignment verticalTextAlignment, bool isDirectionReversed, string padding)
        {
            var tickBar = new LinearTextBar
            {
                Minimum = 0,
                Maximum = 10,
                TickFrequency = 5,
                Ticks = new DoubleCollection(new double[] { 1, 2, 6 }),
                Foreground = Brushes.Black,
                Placement = placement,
                TextPosition = new ExplicitLinearTextPosition(horizontalTextAlignment, verticalTextAlignment),
                IsDirectionReversed = isDirectionReversed,
                Padding = padding.AsThickness(),
            };

            ImageAssert.AreEqual(GetFileName(tickBar), tickBar);
        }

        [TestCase(TickBarPlacement.Left, false, null)]
        [TestCase(TickBarPlacement.Left, true, null)]
        [TestCase(TickBarPlacement.Left, false, "1")]
        [TestCase(TickBarPlacement.Left, true, "1")]
        [TestCase(TickBarPlacement.Right, false, null)]
        [TestCase(TickBarPlacement.Right, true, null)]
        [TestCase(TickBarPlacement.Right, false, "1")]
        [TestCase(TickBarPlacement.Right, true, "1")]
        [TestCase(TickBarPlacement.Top, false, null)]
        [TestCase(TickBarPlacement.Top, true, null)]
        [TestCase(TickBarPlacement.Top, false, "1")]
        [TestCase(TickBarPlacement.Top, true, "1")]
        [TestCase(TickBarPlacement.Bottom, false, null)]
        [TestCase(TickBarPlacement.Bottom, true, null)]
        [TestCase(TickBarPlacement.Bottom, false, "1")]
        [TestCase(TickBarPlacement.Bottom, true, "1")]
        public void RenderDefaultTextPosition(TickBarPlacement placement, bool isDirectionReversed, string padding)
        {
            var tickBar = new LinearTextBar
            {
                Minimum = 0,
                Maximum = 10,
                TickFrequency = 5,
                Ticks = new DoubleCollection(new double[] { 1, 2, 6 }),
                Foreground = Brushes.Black,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
                Padding = padding.AsThickness(),
            };

            ImageAssert.AreEqual(GetFileName(tickBar), tickBar);
        }

        [TestCase(TickBarPlacement.Left, false, "0 0 0 0", "0,4.5,0,4.5")]
        [TestCase(TickBarPlacement.Left, false, "0 1 0 1", "0,3.5,0,3.5")]
        [TestCase(TickBarPlacement.Left, true, "0 0 0 0", "0,4.5,0,4.5")]
        [TestCase(TickBarPlacement.Left, true, "0 1 0 1", "0,3.5,0,3.5")]
        [TestCase(TickBarPlacement.Right, false, "0 0 0 0", "0,4.5,0,4.5")]
        [TestCase(TickBarPlacement.Right, false, "0 1 0 1", "0,3.5,0,3.5")]
        [TestCase(TickBarPlacement.Right, true, "0 0 0 0", "0,4.5,0,4.5")]
        [TestCase(TickBarPlacement.Right, true, "0 1 0 1", "0,3.5,0,3.5")]
        [TestCase(TickBarPlacement.Bottom, false, "0 0 0 0", "3,0,6,0")]
        [TestCase(TickBarPlacement.Bottom, false, "1 0 1 0", "2,0,5,0")]
        [TestCase(TickBarPlacement.Bottom, true, "0 0 0 0", "6,0,3,0")]
        [TestCase(TickBarPlacement.Bottom, true, "1 0 1 0", "5,0,2,0")]
        [TestCase(TickBarPlacement.Top, false, "0 0 0 0", "3,0,6,0")]
        [TestCase(TickBarPlacement.Top, false, "1 0 1 0", "2,0,5,0")]
        [TestCase(TickBarPlacement.Top, true, "0 0 0 0", "6,0,3,0")]
        [TestCase(TickBarPlacement.Top, true, "1 0 1 0", "5,0,2,0")]
        public void Overflow(TickBarPlacement placement, bool isDirectionReversed, string padding, string expected)
        {
            var tickBar = new LinearTextBar
            {
                Minimum = 0,
                Maximum = 10,
                TickFrequency = 1,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
                Padding = padding.AsThickness(),
            };

            var gauge = new LinearGauge { Content = tickBar };
            gauge.Arrange(new Rect(new Size(10, 10)));
            Assert.AreEqual(expected, gauge.ContentOverflow.ToString());
            Assert.AreEqual(expected, tickBar.Overflow.ToString());
        }

        private static string GetFileName(LinearTextBar tickBar)
        {
            var ticks = tickBar.Ticks != null
                ? $"_Ticks_{tickBar.Ticks}"
                : string.Empty;

            var padding = tickBar.Padding.IsZero()
                ? string.Empty
                : $"_Padding_{tickBar.Padding}";

            var tickFrequency = tickBar.TickFrequency > 0
                ? $"_TickFrequency_{tickBar.TickFrequency}"
                : string.Empty;

            var textPosition = tickBar.TextPosition is ExplicitLinearTextPosition explicitPos
                ? $"_Explicit_{explicitPos.Horizontal}_{explicitPos.Vertical}"
                : "_Default";

            return $@"LinearTextBar_Placement_{tickBar.Placement}_Min_{tickBar.Minimum}_Max_{tickBar.Maximum}_IsDirectionReversed_{tickBar.IsDirectionReversed}{textPosition}{tickFrequency}{ticks}{padding}.png"
                   .Replace(" ", "_");
        }

        private static void SaveImage(LinearTextBar tickBar)
        {
            Directory.CreateDirectory(@"C:\Temp\LinearTextBar");
            var size = tickBar.Placement.IsHorizontal()
                ? new Size(100, 15)
                : new Size(15, 100);
            tickBar.SaveImage(size, $@"C:\Temp\LinearTextBar\{GetFileName(tickBar)}");
        }
    }
}