namespace Gu.Wpf.Gauges.Tests.Indicators
{
    using System.IO;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using Gu.Wpf.Gauges.Tests.TestHelpers;
    using NUnit.Framework;

    [Apartment(ApartmentState.STA)]
    public class LinearRangeTests
    {
        [TestCase(TickBarPlacement.Top)]
        [TestCase(TickBarPlacement.Bottom)]
        public void Horizontal(TickBarPlacement placement)
        {
            var gauge = new LinearGauge
            {
                Minimum = 0,
                Maximum = 100,
                Placement = placement,
                Content = new LinearRange
                {
                    Background = Brushes.Black,
                    Start = 10,
                    End = 20
                }
            };

            ImageAssert.AreEqual(Properties.Resources.LinearRange_Orientation_Horizontal_IsDirectionReversed_False, gauge);
        }

        [TestCase(TickBarPlacement.Top)]
        [TestCase(TickBarPlacement.Bottom)]
        public void HorizontalReversed(TickBarPlacement placement)
        {
            var gauge = new LinearGauge
            {
                Minimum = 0,
                Maximum = 100,
                Placement = placement,
                IsDirectionReversed = true,
                Content = new LinearRange
                {
                    Background = Brushes.Black,
                    Start = 10,
                    End = 20
                }
            };

            ImageAssert.AreEqual(Properties.Resources.LinearRange_Orientation_Horizontal_IsDirectionReversed_True, gauge);
        }

        [TestCase(TickBarPlacement.Left)]
        [TestCase(TickBarPlacement.Right)]
        public void Vertical(TickBarPlacement placement)
        {
            var gauge = new LinearGauge
            {
                Minimum = 0,
                Maximum = 100,
                Placement = placement,
                Content = new LinearRange
                {
                    Background = Brushes.Black,
                    Start = 10,
                    End = 20
                }
            };

            ImageAssert.AreEqual(Properties.Resources.LinearRange_Orientation_Vertical_IsDirectionReversed_False, gauge);
        }

        [TestCase(TickBarPlacement.Left)]
        [TestCase(TickBarPlacement.Right)]
        public void VerticalReversed(TickBarPlacement placement)
        {
            var gauge = new LinearGauge
            {
                Minimum = 0,
                Maximum = 100,
                Placement = placement,
                IsDirectionReversed = true,
                Content = new LinearRange
                {
                    Background = Brushes.Black,
                    Start = 10,
                    End = 20
                }
            };

            ImageAssert.AreEqual(Properties.Resources.LinearRange_Orientation_Vertical_IsDirectionReversed_True, gauge);
        }

        private static void SaveImage(LinearGauge gauge)
        {
            Directory.CreateDirectory($@"C:\Temp\LinearRange");
            var orientation = gauge.Placement.IsHorizontal()
                ? "Orientation_Horizontal"
                : "Orientation_Vertical";
            gauge.SaveImage(
                gauge.Placement.IsHorizontal() ? new Size(10, 5) : new Size(5, 40),
                $@"C:\Temp\LinearRange\LinearRange_{orientation}_IsDirectionReversed_{gauge.IsDirectionReversed}.png");
        }
    }
}
