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
    public class LinearLineBarTests
    {
        [TestCase(true, TickBarPlacement.Top)]
        [TestCase(false, TickBarPlacement.Top)]
        [TestCase(true, TickBarPlacement.Bottom)]
        [TestCase(false, TickBarPlacement.Bottom)]
        public void Horizontal(bool isDirectionReversed, TickBarPlacement placement)
        {
            var tickBar = new LinearLineBar
                          {
                              Minimum = 0,
                              Maximum = 10,
                              Stroke = Brushes.Black,
                              StrokeThickness = 1,
                              Placement = placement,
                              IsDirectionReversed = isDirectionReversed,
                          };
            ImageAssert.AreEqual(Properties.Resources.LinearLineBar_Min_0_Max_10_Value_NaN_Horizontal, tickBar);
        }

        [TestCase(true, TickBarPlacement.Left)]
        [TestCase(false, TickBarPlacement.Left)]
        [TestCase(true, TickBarPlacement.Right)]
        [TestCase(false, TickBarPlacement.Right)]
        public void Vertical(bool isDirectionReversed, TickBarPlacement placement)
        {
            var tickBar = new LinearLineBar
                          {
                              Minimum = 0,
                              Maximum = 10,
                              Stroke = Brushes.Black,
                              StrokeThickness = 1,
                              Placement = placement,
                              IsDirectionReversed = isDirectionReversed,
                          };
            ImageAssert.AreEqual(Properties.Resources.LinearLineBar_Min_0_Max_10_Value_NaN_Vertical, tickBar);
        }

        private static void SaveImage(LinearLineBar tickBar)
        {
            var orientation = tickBar.Placement == TickBarPlacement.Left || tickBar.Placement == TickBarPlacement.Right
                ? "_Vertical"
                : "_Horizontal";

            var size = tickBar.Placement == TickBarPlacement.Left || tickBar.Placement == TickBarPlacement.Right
                ? new Size(5, 100)
                : new Size(100, 5);
            Directory.CreateDirectory(@"C:\Temp\LinearLineBar");
            tickBar.SaveImage(
                size,
                $@"C:\Temp\LinearLineBar\LinearLineBar_Min_{tickBar.Minimum}_Max_{tickBar.Maximum}_Value_{tickBar.Value}{orientation}.png");
        }
    }
}