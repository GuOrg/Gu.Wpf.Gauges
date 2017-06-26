namespace Gu.Wpf.Gauges.Tests.Primitives.Linear
{
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using Gu.Wpf.Gauges.Tests.TestHelpers;
    using NUnit.Framework;

    [Apartment(ApartmentState.STA)]
    public class LinearTickBarTests
    {
        [TestCase(TickBarPlacement.Bottom)]
        [TestCase(TickBarPlacement.Top)]
        public void TickFrequencyOneHorizontal(TickBarPlacement placement)
        {
            var tickBar = new LinearTickBar
                          {
                              Minimum = 0,
                              Maximum = 10,
                              TickFrequency = 1,
                              Fill = Brushes.Black,
                              Placement = placement
                          };

            ImageAssert.AreEqual(Properties.Resources.LinearTickBar_Min_0_Max_10_TickFrequency_1_Horizontal, tickBar);
        }

        [TestCase(TickBarPlacement.Bottom)]
        [TestCase(TickBarPlacement.Top)]
        public void TicksHorizontal(TickBarPlacement placement)
        {
            var tickBar = new LinearTickBar
                          {
                              Minimum = 0,
                              Maximum = 10,
                              Ticks = new DoubleCollection(new double[] { 1, 2, 6 }),
                              Fill = Brushes.Black,
                              Placement = placement
                          };

            ImageAssert.AreEqual(Properties.Resources.LinearTickBar_Min_0_Max_10_TickFrequency_0_Ticks_1_2_6_Horizontal, tickBar);
        }

        [TestCase(TickBarPlacement.Left)]
        [TestCase(TickBarPlacement.Right)]
        public void TickFrequencyOneVertical(TickBarPlacement placement)
        {
            var tickBar = new LinearTickBar
                          {
                              Minimum = 0,
                              Maximum = 10,
                              TickFrequency = 1,
                              Fill = Brushes.Black,
                              Placement = placement
                          };

            ImageAssert.AreEqual(Properties.Resources.LinearTickBar_Min_0_Max_10_TickFrequency_1_Vertical, tickBar);
        }

        [TestCase(TickBarPlacement.Left)]
        [TestCase(TickBarPlacement.Right)]
        public void TicksVertical(TickBarPlacement placement)
        {
            var tickBar = new LinearTickBar
                          {
                              Minimum = 0,
                              Maximum = 10,
                              Ticks = new DoubleCollection(new double[] { 1, 2, 6 }),
                              Fill = Brushes.Black,
                              Placement = placement
                          };

            ImageAssert.AreEqual(Properties.Resources.LinearTickBar_Min_0_Max_10_TickFrequency_0_Ticks_1_2_6_Vertical, tickBar);
        }

        private static void SaveImage(LinearTickBar tickBar)
        {
            var ticks = tickBar.Ticks != null
                ? $"_Ticks_{tickBar.Ticks}"
                : string.Empty;
            var orientation = tickBar.Placement == TickBarPlacement.Left || tickBar.Placement == TickBarPlacement.Right
                ? "_Vertical"
                : "_Horizontal";

            var size = tickBar.Placement == TickBarPlacement.Left || tickBar.Placement == TickBarPlacement.Right
                ? new Size(5, 40)
                : new Size(40, 5);
            tickBar.SaveImage(size, $@"C:\Temp\LinearTickBar_Min_{tickBar.Minimum}_Max_{tickBar.Maximum}_TickFrequency_{tickBar.TickFrequency}{ticks}{orientation}.png");
        }
    }
}