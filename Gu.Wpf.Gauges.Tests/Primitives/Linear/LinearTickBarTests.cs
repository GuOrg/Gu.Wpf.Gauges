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
        [TestCase(true, TickBarPlacement.Top)]
        [TestCase(false, TickBarPlacement.Top)]
        [TestCase(true, TickBarPlacement.Bottom)]
        [TestCase(false, TickBarPlacement.Bottom)]
        public void TickFrequencyOneHorizontal(bool isDirectionReversed, TickBarPlacement placement)
        {
            var tickBar = new LinearTickBar
            {
                Minimum = 0,
                Maximum = 10,
                TickFrequency = 1,
                Fill = Brushes.Black,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
            };

            //// SaveImage(tickBar);
            ImageAssert.AreEqual(Properties.Resources.LinearTickBar_Min_0_Max_10_TickFrequency_1_Horizontal, tickBar);
        }

        [TestCase(true, TickBarPlacement.Top)]
        [TestCase(false, TickBarPlacement.Top)]
        [TestCase(true, TickBarPlacement.Bottom)]
        [TestCase(false, TickBarPlacement.Bottom)]
        public void TicksHorizontal(bool isDirectionReversed, TickBarPlacement placement)
        {
            var tickBar = new LinearTickBar
            {
                Minimum = 0,
                Maximum = 10,
                Ticks = new DoubleCollection(new double[] { 1, 2, 6 }),
                Fill = Brushes.Black,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
            };

            //// SaveImage(tickBar);
            var expected = isDirectionReversed
                ? Properties.Resources.LinearTickBar_Min_0_Max_10_IsDirectionReversed_True_Ticks_1_2_6_Horizontal
                : Properties.Resources.LinearTickBar_Min_0_Max_10_IsDirectionReversed_False_Ticks_1_2_6_Horizontal;
            ImageAssert.AreEqual(expected, tickBar);
        }

        [TestCase(true, TickBarPlacement.Top)]
        [TestCase(false, TickBarPlacement.Top)]
        [TestCase(true, TickBarPlacement.Bottom)]
        [TestCase(false, TickBarPlacement.Bottom)]
        public void TicksAndFrequencyHorizontal(bool isDirectionReversed, TickBarPlacement placement)
        {
            var tickBar = new LinearTickBar
            {
                Minimum = 0,
                Maximum = 10,
                TickFrequency = 5,
                Ticks = new DoubleCollection(new double[] { 1, 2, 6 }),
                Fill = Brushes.Black,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
            };

            //// SaveImage(tickBar);
            var expected = isDirectionReversed
                ? Properties.Resources.LinearTickBar_Min_0_Max_10_IsDirectionReversed_True_TickFrequency_5_Ticks_1_2_6_Horizontal
                : Properties.Resources.LinearTickBar_Min_0_Max_10_IsDirectionReversed_False_TickFrequency_5_Ticks_1_2_6_Horizontal;
            ImageAssert.AreEqual(expected, tickBar);
        }

        [TestCase(true, TickBarPlacement.Left)]
        [TestCase(false, TickBarPlacement.Left)]
        [TestCase(true, TickBarPlacement.Right)]
        [TestCase(false, TickBarPlacement.Right)]
        public void TickFrequencyOneVertical(bool isDirectionReversed, TickBarPlacement placement)
        {
            var tickBar = new LinearTickBar
            {
                Minimum = 0,
                Maximum = 10,
                TickFrequency = 1,
                Fill = Brushes.Black,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
            };

            SaveImage(tickBar);
            ImageAssert.AreEqual(Properties.Resources.LinearTickBar_Min_0_Max_10_TickFrequency_1_Vertical, tickBar);
        }

        [TestCase(true, TickBarPlacement.Left)]
        [TestCase(false, TickBarPlacement.Left)]
        [TestCase(true, TickBarPlacement.Right)]
        [TestCase(false, TickBarPlacement.Right)]
        public void TicksVertical(bool isDirectionReversed, TickBarPlacement placement)
        {
            var tickBar = new LinearTickBar
            {
                Minimum = 0,
                Maximum = 10,
                Ticks = new DoubleCollection(new double[] { 1, 2, 6 }),
                Fill = Brushes.Black,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
            };

             SaveImage(tickBar);
            var expected = isDirectionReversed
                ? Properties.Resources.LinearTickBar_Min_0_Max_10_IsDirectionReversed_True_Ticks_1_2_6_Vertical
                : Properties.Resources.LinearTickBar_Min_0_Max_10_IsDirectionReversed_False_Ticks_1_2_6_Vertical;
            ImageAssert.AreEqual(expected, tickBar);
        }

        [TestCase(true, TickBarPlacement.Left)]
        [TestCase(false, TickBarPlacement.Left)]
        [TestCase(true, TickBarPlacement.Right)]
        [TestCase(false, TickBarPlacement.Right)]
        public void TicksAndFrequencyVertical(bool isDirectionReversed, TickBarPlacement placement)
        {
            var tickBar = new LinearTickBar
            {
                Minimum = 0,
                Maximum = 10,
                TickFrequency = 5,
                Ticks = new DoubleCollection(new double[] { 1, 2, 6 }),
                Fill = Brushes.Black,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
            };

            //// SaveImage(tickBar);
            var expected = isDirectionReversed
                ? Properties.Resources.LinearTickBar_Min_0_Max_10_IsDirectionReversed_True_TickFrequency_5_Ticks_1_2_6_Vertical
                : Properties.Resources.LinearTickBar_Min_0_Max_10_IsDirectionReversed_False_TickFrequency_5_Ticks_1_2_6_Vertical;
            ImageAssert.AreEqual(expected, tickBar);
        }

        private static void SaveImage(LinearTickBar tickBar)
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

            var size = tickBar.Placement == TickBarPlacement.Left || tickBar.Placement == TickBarPlacement.Right
                ? new Size(5, 40)
                : new Size(40, 5);
            Directory.CreateDirectory(@"C:\Temp\LinearTickBar");
            tickBar.SaveImage(size, $@"C:\Temp\LinearTickBar\LinearTickBar_Min_{tickBar.Minimum}_Max_{tickBar.Maximum}{isReversed}{tickFrequency}{ticks}{orientation}.png");
        }
    }
}