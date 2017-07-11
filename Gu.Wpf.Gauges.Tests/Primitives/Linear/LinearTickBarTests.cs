namespace Gu.Wpf.Gauges.Tests.Primitives.Linear
{
    using System.IO;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using Gu.Wpf.Gauges.Tests.TestHelpers;
    using NUnit.Framework;

    [Apartment(ApartmentState.STA)]
    public class LinearTickBarTests
    {
        private static readonly ThicknessConverter ThicknessConverter = new ThicknessConverter();

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
        [TestCase(TickBarPlacement.Left, true, 2, 1, 1, null, "1")]
        [TestCase(TickBarPlacement.Left, false, 2, 1, 1, null, "1")]
        [TestCase(TickBarPlacement.Left, true, 2, 1, 1, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Left, false, 2, 1, 1, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Left, true, 3, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Left, false, 3, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Left, true, 3, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Left, false, 3, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Left, true, 3, 1, 1, null, "1")]
        [TestCase(TickBarPlacement.Left, false, 3, 1, 1, null, "1")]
        [TestCase(TickBarPlacement.Left, true, 3, 1, 1, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Left, false, 3, 1, 1, "1 2 6", "1")]
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
        [TestCase(TickBarPlacement.Right, true, 2, 1, 1, null, "1")]
        [TestCase(TickBarPlacement.Right, false, 2, 1, 1, null, "1")]
        [TestCase(TickBarPlacement.Right, true, 2, 1, 1, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Right, false, 2, 1, 1, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Right, true, 3, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Right, false, 3, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Right, true, 3, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Right, false, 3, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Right, true, 3, 1, 1, null, "1")]
        [TestCase(TickBarPlacement.Right, false, 3, 1, 1, null, "1")]
        [TestCase(TickBarPlacement.Right, true, 3, 1, 1, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Right, false, 3, 1, 1, "1 2 6", "1")]
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
        [TestCase(TickBarPlacement.Top, true, 2, 1, 1, null, "1")]
        [TestCase(TickBarPlacement.Top, false, 2, 1, 1, null, "1")]
        [TestCase(TickBarPlacement.Top, true, 2, 1, 1, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Top, false, 2, 1, 1, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Top, true, 3, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Top, false, 3, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Top, true, 3, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Top, false, 3, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Top, true, 3, 1, 1, null, "1")]
        [TestCase(TickBarPlacement.Top, false, 3, 1, 1, null, "1")]
        [TestCase(TickBarPlacement.Top, true, 3, 1, 1, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Top, false, 3, 1, 1, "1 2 6", "1")]
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
        [TestCase(TickBarPlacement.Bottom, true, 2, 1, 1, null, "1")]
        [TestCase(TickBarPlacement.Bottom, false, 2, 1, 1, null, "1")]
        [TestCase(TickBarPlacement.Bottom, true, 2, 1, 1, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Bottom, false, 2, 1, 1, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Bottom, true, 3, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Bottom, false, 3, 1, 1, null, null)]
        [TestCase(TickBarPlacement.Bottom, true, 3, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Bottom, false, 3, 1, 1, "1 2 6", null)]
        [TestCase(TickBarPlacement.Bottom, true, 3, 1, 1, null, "1")]
        [TestCase(TickBarPlacement.Bottom, false, 3, 1, 1, null, "1")]
        [TestCase(TickBarPlacement.Bottom, true, 3, 1, 1, "1 2 6", "1")]
        [TestCase(TickBarPlacement.Bottom, false, 3, 1, 1, "1 2 6", "1")]
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
                Padding = string.IsNullOrEmpty(padding) ? default(Thickness) : (Thickness)ThicknessConverter.ConvertFrom(padding)
            };

            ImageAssert.AreEqual(GetFileName(tickBar), tickBar);
        }

        [TestCase(TickBarPlacement.Left, false, 1, 1, "0 0 0 0", "0,0.5,0,0.5")]
        [TestCase(TickBarPlacement.Left, false, 2, 1, "0 0 0 0", "0,1,0,1")]
        [TestCase(TickBarPlacement.Left, false, 2, 0, "0 1 0 1", "0,0,0,0")]
        [TestCase(TickBarPlacement.Left, false, 2, 1, "0 1 0 1", "0,0,0,0")]
        [TestCase(TickBarPlacement.Left, false, 3, 1, "0 1 0 1", "0,0.5,0,0.5")]
        [TestCase(TickBarPlacement.Left, true, 1, 1, "0 0 0 0", "0,0.5,0,0.5")]
        [TestCase(TickBarPlacement.Left, true, 2, 1, "0 0 0 0", "0,1,0,1")]
        [TestCase(TickBarPlacement.Left, true, 2, 0, "0 1 0 1", "0,0,0,0")]
        [TestCase(TickBarPlacement.Left, true, 2, 1, "0 1 0 1", "0,0,0,0")]
        [TestCase(TickBarPlacement.Left, true, 3, 1, "0 1 0 1", "0,0.5,0,0.5")]
        [TestCase(TickBarPlacement.Right, false, 1, 1, "0 0 0 0", "0,0.5,0,0.5")]
        [TestCase(TickBarPlacement.Right, false, 2, 1, "0 0 0 0", "0,1,0,1")]
        [TestCase(TickBarPlacement.Right, false, 2, 0, "0 1 0 1", "0,0,0,0")]
        [TestCase(TickBarPlacement.Right, false, 2, 1, "0 1 0 1", "0,0,0,0")]
        [TestCase(TickBarPlacement.Right, false, 3, 1, "0 1 0 1", "0,0.5,0,0.5")]
        [TestCase(TickBarPlacement.Right, true, 1, 1, "0 0 0 0", "0,0.5,0,0.5")]
        [TestCase(TickBarPlacement.Right, true, 2, 1, "0 0 0 0", "0,1,0,1")]
        [TestCase(TickBarPlacement.Right, true, 2, 0, "0 1 0 1", "0,0,0,0")]
        [TestCase(TickBarPlacement.Right, true, 2, 1, "0 1 0 1", "0,0,0,0")]
        [TestCase(TickBarPlacement.Right, true, 3, 1, "0 1 0 1", "0,0.5,0,0.5")]
        [TestCase(TickBarPlacement.Bottom, false, 1, 1, "0 0 0 0", "0.5,0,0.5,0")]
        [TestCase(TickBarPlacement.Bottom, false, 2, 1, "0 0 0 0", "1,0,1,0")]
        [TestCase(TickBarPlacement.Bottom, false, 2, 0, "1 0 1 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Bottom, false, 2, 1, "1 0 1 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Bottom, false, 3, 1, "1 0 1 0", "0.5,0,0.5,0")]
        [TestCase(TickBarPlacement.Bottom, true, 1, 1, "0 0 0 0", "0.5,0,0.5,0")]
        [TestCase(TickBarPlacement.Bottom, true, 2, 1, "0 0 0 0", "1,0,1,0")]
        [TestCase(TickBarPlacement.Bottom, true, 2, 0, "1 0 1 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Bottom, true, 2, 1, "1 0 1 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Bottom, true, 3, 1, "1 0 1 0", "0.5,0,0.5,0")]
        [TestCase(TickBarPlacement.Top, false, 1, 1, "0 0 0 0", "0.5,0,0.5,0")]
        [TestCase(TickBarPlacement.Top, false, 2, 1, "0 0 0 0", "1,0,1,0")]
        [TestCase(TickBarPlacement.Top, false, 2, 0, "1 0 1 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Top, false, 2, 1, "1 0 1 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Top, false, 3, 1, "1 0 1 0", "0.5,0,0.5,0")]
        [TestCase(TickBarPlacement.Top, true, 1, 1, "0 0 0 0", "0.5,0,0.5,0")]
        [TestCase(TickBarPlacement.Top, true, 2, 1, "0 0 0 0", "1,0,1,0")]
        [TestCase(TickBarPlacement.Top, true, 2, 0, "1 0 1 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Top, true, 2, 1, "1 0 1 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Top, true, 3, 1, "1 0 1 0", "0.5,0,0.5,0")]
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
                Padding = (Thickness)ThicknessConverter.ConvertFrom(padding)
            };

            tickBar.Arrange(new Rect(new Size(10, 10)));
            Assert.AreEqual(expected, tickBar.Overflow.ToString());
        }

        private static string GetFileName(LinearTickBar tickBar)
        {
            var ticks = tickBar.Ticks != null
                ? $"_Ticks_{tickBar.Ticks}"
                : string.Empty;

            var isReversed = tickBar.Ticks != null
                ? $"_IsDirectionReversed_{tickBar.IsDirectionReversed}"
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

            return $@"LinearTickBar_Min_{tickBar.Minimum}_Max_{tickBar.Maximum}{padding}_TickWidth_{tickBar.TickWidth}_StrokeThickness_{tickBar.StrokeThickness}{isReversed}{tickFrequency}{ticks}{orientation}.png"
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