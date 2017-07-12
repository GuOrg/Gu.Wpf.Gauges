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
    public class LinearDotBarTests
    {
        private static readonly ThicknessConverter ThicknessConverter = new ThicknessConverter();

        [TestCase(TickBarPlacement.Left, true, double.NaN, null)]
        [TestCase(TickBarPlacement.Left, false, double.NaN, null)]
        [TestCase(TickBarPlacement.Left, true, double.NaN, "0,6")]
        [TestCase(TickBarPlacement.Left, false, double.NaN, "0,6")]
        [TestCase(TickBarPlacement.Left, true, 40, null)]
        [TestCase(TickBarPlacement.Left, false, 40, null)]
        [TestCase(TickBarPlacement.Left, true, 40, "0,6")]
        [TestCase(TickBarPlacement.Left, false, 40, "0,6")]
        [TestCase(TickBarPlacement.Right, true, double.NaN, null)]
        [TestCase(TickBarPlacement.Right, false, double.NaN, null)]
        [TestCase(TickBarPlacement.Right, true, double.NaN, "0,6")]
        [TestCase(TickBarPlacement.Right, false, double.NaN, "0,6")]
        [TestCase(TickBarPlacement.Right, true, 40, null)]
        [TestCase(TickBarPlacement.Right, false, 40, null)]
        [TestCase(TickBarPlacement.Right, true, 40, "0,6")]
        [TestCase(TickBarPlacement.Right, false, 40, "0,6")]
        [TestCase(TickBarPlacement.Top, true, double.NaN, null)]
        [TestCase(TickBarPlacement.Top, false, double.NaN, null)]
        [TestCase(TickBarPlacement.Top, true, double.NaN, "6,0")]
        [TestCase(TickBarPlacement.Top, false, double.NaN, "6,0")]
        [TestCase(TickBarPlacement.Top, true, 40, null)]
        [TestCase(TickBarPlacement.Top, false, 40, null)]
        [TestCase(TickBarPlacement.Top, true, 40, "6,0")]
        [TestCase(TickBarPlacement.Top, false, 40, "6,0")]
        [TestCase(TickBarPlacement.Bottom, true, double.NaN, null)]
        [TestCase(TickBarPlacement.Bottom, false, double.NaN, null)]
        [TestCase(TickBarPlacement.Bottom, true, double.NaN, "6,0")]
        [TestCase(TickBarPlacement.Bottom, false, double.NaN, "6,0")]
        [TestCase(TickBarPlacement.Bottom, true, 40, null)]
        [TestCase(TickBarPlacement.Bottom, false, 40, null)]
        [TestCase(TickBarPlacement.Bottom, true, 40, "6,0")]
        [TestCase(TickBarPlacement.Bottom, false, 40, "6,0")]
        public void Render(TickBarPlacement placement, bool isDirectionReversed, double value, string padding)
        {
            var tickBar = new LinearDotBar
            {
                Minimum = 0,
                Maximum = 100,
                Value = value,
                TickFrequency = 25,
                Ticks = new DoubleCollection(new double[] { 10, 20, 60 }),
                Fill = Brushes.Red,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                TickDiameter = 5,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
                Padding = string.IsNullOrEmpty(padding) ? default(Thickness) : (Thickness)ThicknessConverter.ConvertFrom(padding),
            };

            ImageAssert.AreEqual(GetFileName(tickBar), tickBar);
        }

        [TestCase(TickBarPlacement.Left, false, 2, 1, "0 0 0 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Left, true, 2, 1, "0 0 0 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Left, false, 2, 1, "0 0 0 0", "0,1,0,1")]
        [TestCase(TickBarPlacement.Left, true, 2, 1, "0 0 0 0", "0,1,0,1")]
        [TestCase(TickBarPlacement.Left, false, 2, 1, "0 0 0 0", "0,0,0,1")]
        [TestCase(TickBarPlacement.Left, true, 2, 1, "0 0 0 0", "0,1,0,0")]
        [TestCase(TickBarPlacement.Left, false, 2, 1, "0 0 0 0", "0,1,0,0")]
        [TestCase(TickBarPlacement.Left, true, 2, 1, "0 0 0 0", "0,0,0,1")]
        [TestCase(TickBarPlacement.Left, false, 2, 1, "0 0 0 1", "0,0,0,0")]
        [TestCase(TickBarPlacement.Left, true, 2, 1, "0 1 0 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Left, false, 2, 1, "0 1 0 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Left, true, 2, 1, "0 0 0 1", "0,0,0,0")]
        [TestCase(TickBarPlacement.Bottom, false, 2, 1, "0 0 0 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Bottom, true, 2, 1, "0 0 0 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Bottom, false, 2, 1, "0 0 0 0", "1,0,0,0")]
        [TestCase(TickBarPlacement.Bottom, true, 2, 1, "0 0 0 0", "0,0,1,0")]
        [TestCase(TickBarPlacement.Bottom, false, 2, 1, "0 0 0 0", "0,0,1,0")]
        [TestCase(TickBarPlacement.Bottom, true, 2, 1, "0 0 0 0", "1,0,0,0")]
        [TestCase(TickBarPlacement.Bottom, false, 2, 1, "1 0 0 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Bottom, true, 2, 1, "0 0 1 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Bottom, false, 2, 1, "0 0 1 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Bottom, true, 2, 1, "1 0 0 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Top, false, 2, 1, "0 0 0 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Top, true, 2, 1, "0 0 0 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Top, false, 2, 1, "0 0 0 0", "1,0,0,0")]
        [TestCase(TickBarPlacement.Top, true, 2, 1, "0 0 0 0", "0,0,1,0")]
        [TestCase(TickBarPlacement.Top, false, 2, 1, "0 0 0 0", "0,0,1,0")]
        [TestCase(TickBarPlacement.Top, true, 2, 1, "0 0 0 0", "1,0,0,0")]
        [TestCase(TickBarPlacement.Top, false, 2, 1, "1 0 0 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Top, true, 2, 1, "0 0 1 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Top, false, 2, 1, "0 0 1 0", "0,0,0,0")]
        [TestCase(TickBarPlacement.Top, true, 2, 1, "1 0 0 0", "0,0,0,0")]
        public void Overflow(TickBarPlacement placement, bool isDirectionReversed, double strokeThickness, double tickDiameter, string padding, string expected)
        {
            var tickBar = new LinearDotBar
            {
                StrokeThickness = strokeThickness,
                TickDiameter = tickDiameter,
                Minimum = 0,
                Maximum = 10,
                Stroke = Brushes.Black,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
                Padding = (Thickness)ThicknessConverter.ConvertFrom(padding)
            };

            var gauge = new LinearGauge { Content = tickBar };
            gauge.Arrange(new Rect(new Size(10, 10)));
            Assert.AreEqual(expected, gauge.ContentOverflow.ToString());
            Assert.AreEqual(expected, tickBar.Overflow.ToString());
        }

        private static string GetFileName(LinearDotBar tickBar)
        {
            var ticks = tickBar.Ticks != null
                ? $"_Ticks_{tickBar.Ticks}"
                : string.Empty;

            var padding = tickBar.Padding.IsZero()
                ? string.Empty
                : $"_Padding_{tickBar.Padding}";

            var value = double.IsNaN(tickBar.Value)
                ? string.Empty
                : $"_Value_{tickBar.Value}";

            return $@"LinearDotBar_Placement_{tickBar.Placement}_IsDirectionReversed_{tickBar.IsDirectionReversed}_Min_{tickBar.Minimum}_Max_{tickBar.Maximum}{value}{padding}{ticks}_TickFrequency_{tickBar.TickFrequency}_TickDiameter_{tickBar.TickDiameter}_StrokeThickness_{tickBar.StrokeThickness}.png"
                .Replace(" ", "_");
        }

        private static void SaveImage(LinearDotBar tickBar)
        {
            var size = tickBar.Placement.IsHorizontal()
                ? new Size(100, 10)
                : new Size(10, 100);
            Directory.CreateDirectory(@"C:\Temp\LinearDotBar");
            tickBar.SaveImage(
                size,
                $@"C:\Temp\LinearDotBar\{GetFileName(tickBar)}");
        }
    }
}