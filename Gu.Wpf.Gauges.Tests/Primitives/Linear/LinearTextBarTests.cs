namespace Gu.Wpf.Gauges.Tests.Primitives.Linear
{
    using System.Drawing;
    using System.IO;
    using System.Threading;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using Gu.Wpf.Gauges.Tests.TestHelpers;
    using NUnit.Framework;
    using Brushes = System.Windows.Media.Brushes;
    using Size = System.Windows.Size;

    [Apartment(ApartmentState.STA)]
    public class LinearTextBarTests
    {
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Left, VerticalTextAlignment.Top, false)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Center, VerticalTextAlignment.Top, false)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Right, VerticalTextAlignment.Top, false)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Left, VerticalTextAlignment.Center, false)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Center, VerticalTextAlignment.Center, false)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Right, VerticalTextAlignment.Center, false)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Left, VerticalTextAlignment.Bottom, false)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Center, VerticalTextAlignment.Bottom, false)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Right, VerticalTextAlignment.Bottom, false)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Left, VerticalTextAlignment.Top, true)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Center, VerticalTextAlignment.Top, true)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Right, VerticalTextAlignment.Top, true)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Left, VerticalTextAlignment.Center, true)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Center, VerticalTextAlignment.Center, true)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Right, VerticalTextAlignment.Center, true)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Left, VerticalTextAlignment.Bottom, true)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Center, VerticalTextAlignment.Bottom, true)]
        [TestCase(TickBarPlacement.Top, HorizontalTextAlignment.Right, VerticalTextAlignment.Bottom, true)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Left, VerticalTextAlignment.Top, false)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Center, VerticalTextAlignment.Top, false)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Right, VerticalTextAlignment.Top, false)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Left, VerticalTextAlignment.Center, false)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Center, VerticalTextAlignment.Center, false)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Right, VerticalTextAlignment.Center, false)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Left, VerticalTextAlignment.Bottom, false)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Center, VerticalTextAlignment.Bottom, false)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Right, VerticalTextAlignment.Bottom, false)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Left, VerticalTextAlignment.Top, true)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Center, VerticalTextAlignment.Top, true)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Right, VerticalTextAlignment.Top, true)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Left, VerticalTextAlignment.Center, true)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Center, VerticalTextAlignment.Center, true)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Right, VerticalTextAlignment.Center, true)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Left, VerticalTextAlignment.Bottom, true)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Center, VerticalTextAlignment.Bottom, true)]
        [TestCase(TickBarPlacement.Bottom, HorizontalTextAlignment.Right, VerticalTextAlignment.Bottom, true)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Left, VerticalTextAlignment.Top, false)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Center, VerticalTextAlignment.Top, false)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Right, VerticalTextAlignment.Top, false)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Left, VerticalTextAlignment.Center, false)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Center, VerticalTextAlignment.Center, false)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Right, VerticalTextAlignment.Center, false)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Left, VerticalTextAlignment.Bottom, false)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Center, VerticalTextAlignment.Bottom, false)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Right, VerticalTextAlignment.Bottom, false)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Left, VerticalTextAlignment.Top, true)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Center, VerticalTextAlignment.Top, true)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Right, VerticalTextAlignment.Top, true)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Left, VerticalTextAlignment.Center, true)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Center, VerticalTextAlignment.Center, true)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Right, VerticalTextAlignment.Center, true)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Left, VerticalTextAlignment.Bottom, true)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Center, VerticalTextAlignment.Bottom, true)]
        [TestCase(TickBarPlacement.Left, HorizontalTextAlignment.Right, VerticalTextAlignment.Bottom, true)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Left, VerticalTextAlignment.Top, false)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Center, VerticalTextAlignment.Top, false)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Right, VerticalTextAlignment.Top, false)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Left, VerticalTextAlignment.Center, false)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Center, VerticalTextAlignment.Center, false)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Right, VerticalTextAlignment.Center, false)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Left, VerticalTextAlignment.Bottom, false)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Center, VerticalTextAlignment.Bottom, false)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Right, VerticalTextAlignment.Bottom, false)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Left, VerticalTextAlignment.Top, true)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Center, VerticalTextAlignment.Top, true)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Right, VerticalTextAlignment.Top, true)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Left, VerticalTextAlignment.Center, true)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Center, VerticalTextAlignment.Center, true)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Right, VerticalTextAlignment.Center, true)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Left, VerticalTextAlignment.Bottom, true)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Center, VerticalTextAlignment.Bottom, true)]
        [TestCase(TickBarPlacement.Right, HorizontalTextAlignment.Right, VerticalTextAlignment.Bottom, true)]
        public void Render(TickBarPlacement placement, HorizontalTextAlignment horizontalTextAlignment, VerticalTextAlignment verticalTextAlignment, bool isDirectionReversed)
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
            };

            var expected = (Bitmap)Properties.Resources.ResourceManager.GetObject(GetName(tickBar));
            ImageAssert.AreEqual(expected, tickBar);
        }

        private static string GetName(LinearTextBar tickBar)
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

            return $@"LinearTextBar_Placement_Min_{tickBar.Minimum}_Max_{tickBar.Maximum}_{tickBar.Placement}_HorizontalTextAlignment_{tickBar.HorizontalTextAlignment}_VerticalTextAlignment_{tickBar.VerticalTextAlignment}{isReversed}{tickFrequency}{ticks}".Replace(" ", "_");
        }

        private static void SaveImage(LinearTextBar tickBar)
        {
            Directory.CreateDirectory(@"C:\Temp\LinearTextBar");
            var size = tickBar.Placement.IsHorizontal()
                ? new Size(100, 15)
                : new Size(15, 100);
            tickBar.SaveImage(size, $@"C:\Temp\LinearTextBar\{GetName(tickBar)}.png");
        }
    }
}