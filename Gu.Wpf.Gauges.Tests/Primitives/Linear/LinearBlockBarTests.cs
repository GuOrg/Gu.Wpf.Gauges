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
    public class LinearBlockBarTests
    {
        [TestCase(true, TickBarPlacement.Top)]
        [TestCase(false, TickBarPlacement.Top)]
        [TestCase(true, TickBarPlacement.Bottom)]
        [TestCase(false, TickBarPlacement.Bottom)]
        public void TickFrequencyOneHorizontal(bool isDirectionReversed, TickBarPlacement placement)
        {
            var blockBar = new LinearBlockBar
            {
                Minimum = 0,
                Maximum = 10,
                TickFrequency = 1,
                Fill = Brushes.Black,
                Stroke = Brushes.Red,
                StrokeThickness = 1,
                TickGap = 1,
                Value = 10,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
            };

            ImageAssert.AreEqual(Properties.Resources.LinearBlockBar_Min_0_Max_10_TickFrequency_1_Horizontal, blockBar);
        }

        [TestCase(true, TickBarPlacement.Top)]
        [TestCase(false, TickBarPlacement.Top)]
        [TestCase(true, TickBarPlacement.Bottom)]
        [TestCase(false, TickBarPlacement.Bottom)]
        public void TicksHorizontal(bool isDirectionReversed, TickBarPlacement placement)
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
                Value = 10,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
            };
            SaveImage(blockBar);
            var expected = isDirectionReversed
                ? Properties.Resources.LinearBlockBar_Min_0_Max_10_IsDirectionReversed_True_Ticks_1_2_6_Horizontal
                : Properties.Resources.LinearBlockBar_Min_0_Max_10_IsDirectionReversed_False_Ticks_1_2_6_Horizontal;
            ImageAssert.AreEqual(expected, blockBar);
        }

        [TestCase(true, TickBarPlacement.Top)]
        [TestCase(false, TickBarPlacement.Top)]
        [TestCase(true, TickBarPlacement.Bottom)]
        [TestCase(false, TickBarPlacement.Bottom)]
        public void TicksAndFrequencyHorizontal(bool isDirectionReversed, TickBarPlacement placement)
        {
            var blockBar = new LinearBlockBar
            {
                Minimum = 0,
                Maximum = 10,
                TickFrequency = 5,
                Ticks = new DoubleCollection(new double[] { 1, 2, 6 }),
                Fill = Brushes.Black,
                Stroke = Brushes.Red,
                StrokeThickness = 1,
                TickGap = 1,
                Value = 10,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
            };

            SaveImage(blockBar);
            var expected = isDirectionReversed
                ? Properties.Resources.LinearBlockBar_Min_0_Max_10_IsDirectionReversed_True_TickFrequency_5_Ticks_1_2_6_Horizontal
                : Properties.Resources.LinearBlockBar_Min_0_Max_10_IsDirectionReversed_False_TickFrequency_5_Ticks_1_2_6_Horizontal;
            ImageAssert.AreEqual(expected, blockBar);
        }

        [TestCase(true, TickBarPlacement.Left)]
        [TestCase(false, TickBarPlacement.Left)]
        [TestCase(true, TickBarPlacement.Right)]
        [TestCase(false, TickBarPlacement.Right)]
        public void TickFrequencyOneVertical(bool isDirectionReversed, TickBarPlacement placement)
        {
            var blockBar = new LinearBlockBar
            {
                Minimum = 0,
                Maximum = 10,
                TickFrequency = 1,
                Fill = Brushes.Black,
                Stroke = Brushes.Red,
                StrokeThickness = 1,
                TickGap = 1,
                Value = 10,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
            };

            SaveImage(blockBar);
            ImageAssert.AreEqual(Properties.Resources.LinearBlockBar_Min_0_Max_10_TickFrequency_1_Vertical, blockBar);
        }

        [TestCase(true, TickBarPlacement.Left)]
        [TestCase(false, TickBarPlacement.Left)]
        [TestCase(true, TickBarPlacement.Right)]
        [TestCase(false, TickBarPlacement.Right)]
        public void TicksVertical(bool isDirectionReversed, TickBarPlacement placement)
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
                Value = 10,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
            };

            SaveImage(blockBar);
            var expected = isDirectionReversed
                ? Properties.Resources.LinearBlockBar_Min_0_Max_10_IsDirectionReversed_True_Ticks_1_2_6_Vertical
                : Properties.Resources.LinearBlockBar_Min_0_Max_10_IsDirectionReversed_False_Ticks_1_2_6_Vertical;
            ImageAssert.AreEqual(expected, blockBar);
        }

        [TestCase(true, TickBarPlacement.Left)]
        [TestCase(false, TickBarPlacement.Left)]
        [TestCase(true, TickBarPlacement.Right)]
        [TestCase(false, TickBarPlacement.Right)]
        public void TicksAndFrequencyVertical(bool isDirectionReversed, TickBarPlacement placement)
        {
            var blockBar = new LinearBlockBar
            {
                Minimum = 0,
                Maximum = 10,
                TickFrequency = 5,
                Ticks = new DoubleCollection(new double[] { 1, 2, 6 }),
                Fill = Brushes.Black,
                Stroke = Brushes.Red,
                StrokeThickness = 1,
                TickGap = 1,
                Value = 10,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
            };

            SaveImage(blockBar);
            var expected = isDirectionReversed
                ? Properties.Resources.LinearBlockBar_Min_0_Max_10_IsDirectionReversed_True_TickFrequency_5_Ticks_1_2_6_Vertical
                : Properties.Resources.LinearBlockBar_Min_0_Max_10_IsDirectionReversed_False_TickFrequency_5_Ticks_1_2_6_Vertical;
            ImageAssert.AreEqual(expected, blockBar);
        }

        private static void SaveImage(LinearBlockBar blockBar)
        {
            var ticks = blockBar.Ticks != null
                ? $"_Ticks_{blockBar.Ticks}"
                : string.Empty;

            var isReversed = blockBar.Ticks != null
                ? $"_IsDirectionReversed_{blockBar.IsDirectionReversed}"
                : string.Empty;

            var tickFrequency = blockBar.TickFrequency > 0
                ? $"_TickFrequency_{blockBar.TickFrequency}"
                : string.Empty;

            var orientation = blockBar.Placement == TickBarPlacement.Left ||
                              blockBar.Placement == TickBarPlacement.Right
                ? "_Vertical"
                : "_Horizontal";

            var size = blockBar.Placement == TickBarPlacement.Left || blockBar.Placement == TickBarPlacement.Right
                ? new Size(5, 40)
                : new Size(40, 5);
            Directory.CreateDirectory(@"C:\Temp\LinearBlockBar");
            blockBar.SaveImage(
                size,
                $@"C:\Temp\LinearBlockBar\LinearBlockBar_Min_{blockBar.Minimum}_Max_{blockBar.Maximum}{isReversed}{tickFrequency}{ticks}{orientation}.png");
        }
    }
}