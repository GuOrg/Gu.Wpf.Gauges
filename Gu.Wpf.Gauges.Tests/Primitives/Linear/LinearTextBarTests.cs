namespace Gu.Wpf.Gauges.Tests.Primitives.Linear
{
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using Gu.Wpf.Gauges.Tests.TestHelpers;
    using NUnit.Framework;
    using Brushes = System.Windows.Media.Brushes;
    using Size = System.Windows.Size;

    [Apartment(ApartmentState.STA)]
    public class LinearTextBarTests
    {
        private static readonly ThicknessConverter ThicknessConverter = new ThicknessConverter();

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
        public void Render(TickBarPlacement placement, HorizontalTextAlignment horizontalTextAlignment, VerticalTextAlignment verticalTextAlignment, bool isDirectionReversed, string padding)
        {
            var tickBar = new LinearTextBar
            {
                Minimum = 0,
                Maximum = 10,
                TickFrequency = 5,
                Ticks = new DoubleCollection(new double[] { 1, 2, 6 }),
                Foreground = Brushes.Black,
                Placement = placement,
                HorizontalTextAlignment = horizontalTextAlignment,
                VerticalTextAlignment = verticalTextAlignment,
                IsDirectionReversed = isDirectionReversed,
                Padding = string.IsNullOrEmpty(padding) ? default(Thickness) : (Thickness)ThicknessConverter.ConvertFrom(padding)
            };

            ImageAssert.AreEqual(GetFileName(tickBar), tickBar);
        }

        private static string GetFileName(LinearTextBar tickBar)
        {
            var ticks = tickBar.Ticks != null
                ? $"_Ticks_{tickBar.Ticks}"
                : string.Empty;

            var padding = tickBar.Padding.Left == 0 && tickBar.Padding.Right == 0 && tickBar.Padding.Top == 0 && tickBar.Padding.Bottom == 0
                ? string.Empty
                : $"_Padding_{tickBar.Padding}";

            var isReversed = tickBar.Ticks != null
                ? $"_IsDirectionReversed_{tickBar.IsDirectionReversed}"
                : string.Empty;

            var tickFrequency = tickBar.TickFrequency > 0
                ? $"_TickFrequency_{tickBar.TickFrequency}"
                : string.Empty;

            return $@"LinearTextBar_Placement_Min_{tickBar.Minimum}_Max_{tickBar.Maximum}_{tickBar.Placement}_HorizontalTextAlignment_{tickBar.HorizontalTextAlignment}_VerticalTextAlignment_{tickBar.VerticalTextAlignment}{isReversed}{tickFrequency}{ticks}{padding}.png"
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