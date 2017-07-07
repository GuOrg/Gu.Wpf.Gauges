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
        [TestCase(true, TickBarPlacement.Top)]
        [TestCase(false, TickBarPlacement.Top)]
        [TestCase(true, TickBarPlacement.Bottom)]
        [TestCase(false, TickBarPlacement.Bottom)]
        public void TickFrequencyOneHorizontal(bool isDirectionReversed, TickBarPlacement placement)
        {
            var tickBar = new LinearDotBar
            {
                Minimum = 0,
                Maximum = 10,
                TickFrequency = 1,
                Fill = Brushes.Red,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                TickDiameter = 5,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
            };
            ImageAssert.AreEqual(Properties.Resources.LinearDotBar_Min_0_Max_10_TickFrequency_1_Horizontal, tickBar);
        }

        [TestCase(true, TickBarPlacement.Top)]
        [TestCase(false, TickBarPlacement.Top)]
        [TestCase(true, TickBarPlacement.Bottom)]
        [TestCase(false, TickBarPlacement.Bottom)]
        public void TicksHorizontal(bool isDirectionReversed, TickBarPlacement placement)
        {
            var tickBar = new LinearDotBar
            {
                Minimum = 0,
                Maximum = 10,
                Ticks = new DoubleCollection(new double[] { 1, 2, 6 }),
                Fill = Brushes.Red,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                TickDiameter = 5,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
            };

            var expected = isDirectionReversed
                ? Properties.Resources.LinearDotBar_Min_0_Max_10_IsDirectionReversed_True_Ticks_1_2_6_Horizontal
                : Properties.Resources.LinearDotBar_Min_0_Max_10_IsDirectionReversed_False_Ticks_1_2_6_Horizontal;
            ImageAssert.AreEqual(expected, tickBar);
        }

        [TestCase(true, TickBarPlacement.Top)]
        [TestCase(false, TickBarPlacement.Top)]
        [TestCase(true, TickBarPlacement.Bottom)]
        [TestCase(false, TickBarPlacement.Bottom)]
        public void TicksAndFrequencyHorizontal(bool isDirectionReversed, TickBarPlacement placement)
        {
            var tickBar = new LinearDotBar
            {
                Minimum = 0,
                Maximum = 10,
                TickFrequency = 5,
                Ticks = new DoubleCollection(new double[] { 1, 2, 6 }),
                Fill = Brushes.Red,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                TickDiameter = 5,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
            };
            var expected = isDirectionReversed
                ? Properties.Resources.LinearDotBar_Min_0_Max_10_IsDirectionReversed_True_TickFrequency_5_Ticks_1_2_6_Horizontal
                : Properties.Resources.LinearDotBar_Min_0_Max_10_IsDirectionReversed_False_TickFrequency_5_Ticks_1_2_6_Horizontal;
            ImageAssert.AreEqual(expected, tickBar);
        }

        [TestCase(true, TickBarPlacement.Left)]
        [TestCase(false, TickBarPlacement.Left)]
        [TestCase(true, TickBarPlacement.Right)]
        [TestCase(false, TickBarPlacement.Right)]
        public void TickFrequencyOneVertical(bool isDirectionReversed, TickBarPlacement placement)
        {
            var tickBar = new LinearDotBar
            {
                Minimum = 0,
                Maximum = 10,
                TickFrequency = 1,
                Fill = Brushes.Red,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                TickDiameter = 5,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
            };
            ImageAssert.AreEqual(Properties.Resources.LinearDotBar_Min_0_Max_10_TickFrequency_1_Vertical, tickBar);
        }

        [TestCase(true, TickBarPlacement.Left)]
        [TestCase(false, TickBarPlacement.Left)]
        [TestCase(true, TickBarPlacement.Right)]
        [TestCase(false, TickBarPlacement.Right)]
        public void TicksVertical(bool isDirectionReversed, TickBarPlacement placement)
        {
            var tickBar = new LinearDotBar
            {
                Minimum = 0,
                Maximum = 10,
                Ticks = new DoubleCollection(new double[] { 1, 2, 6 }),
                Fill = Brushes.Red,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                TickDiameter = 5,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
            };
            var expected = isDirectionReversed
                ? Properties.Resources.LinearDotBar_Min_0_Max_10_IsDirectionReversed_True_Ticks_1_2_6_Vertical
                : Properties.Resources.LinearDotBar_Min_0_Max_10_IsDirectionReversed_False_Ticks_1_2_6_Vertical;
            ImageAssert.AreEqual(expected, tickBar);
        }

        [TestCase(true, TickBarPlacement.Left)]
        [TestCase(false, TickBarPlacement.Left)]
        [TestCase(true, TickBarPlacement.Right)]
        [TestCase(false, TickBarPlacement.Right)]
        public void TicksAndFrequencyVertical(bool isDirectionReversed, TickBarPlacement placement)
        {
            var tickBar = new LinearDotBar
            {
                Minimum = 0,
                Maximum = 10,
                TickFrequency = 5,
                Ticks = new DoubleCollection(new double[] { 1, 2, 6 }),
                Fill = Brushes.Red,
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                TickDiameter = 5,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
            };
            var expected = isDirectionReversed
                ? Properties.Resources.LinearDotBar_Min_0_Max_10_IsDirectionReversed_True_TickFrequency_5_Ticks_1_2_6_Vertical
                : Properties.Resources.LinearDotBar_Min_0_Max_10_IsDirectionReversed_False_TickFrequency_5_Ticks_1_2_6_Vertical;
            ImageAssert.AreEqual(expected, tickBar);
        }

        private static void SaveImage(LinearDotBar tickBar)
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
                ? new Size(7, 100)
                : new Size(100, 7);
            Directory.CreateDirectory(@"C:\Temp\LinearDotBar");
            tickBar.SaveImage(
                size,
                $@"C:\Temp\LinearDotBar\LinearDotBar_Min_{tickBar.Minimum}_Max_{tickBar.Maximum}{isReversed}{tickFrequency}{ticks}{orientation}.png");
        }
    }
}