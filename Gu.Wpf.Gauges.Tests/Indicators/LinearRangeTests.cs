namespace Gu.Wpf.Gauges.Tests.Indicators
{
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using NUnit.Framework;

    [Apartment(ApartmentState.STA)]
    public class LinearRangeTests
    {
        [TestCase(TickBarPlacement.Left, true, 10, 20)]
        [TestCase(TickBarPlacement.Left, false, 10, 20)]
        [TestCase(TickBarPlacement.Right, true, 10, 20)]
        [TestCase(TickBarPlacement.Right, false, 10, 20)]
        [TestCase(TickBarPlacement.Top, true, 10, 20)]
        [TestCase(TickBarPlacement.Top, false, 10, 20)]
        [TestCase(TickBarPlacement.Bottom, true, 10, 20)]
        [TestCase(TickBarPlacement.Bottom, false, 10, 20)]
        public void Render(TickBarPlacement placement, bool isDirectionReversed, double start, double end)
        {
            var range = new LinearRange
            {
                Background = Brushes.Black,
                Start = start,
                End = end
            };

            var gauge = new LinearGauge
            {
                Minimum = 0,
                Maximum = 100,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
                Content = range
            };

            ImageAssert.AreEqual(GetFileName(range), gauge);
        }

        private static string GetFileName(LinearRange range)
        {
            var orientation = range.Placement.IsHorizontal()
                ? "Orientation_Horizontal"
                : "Orientation_Vertical";
            return $"LinearRange_{orientation}_Min_{range.Minimum}_Max_{range.Maximum}_Start_{range.Start}_End_{range.End}_IsDirectionReversed_{range.IsDirectionReversed}.png";
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private static void SaveImage(LinearGauge gauge)
        {
            Directory.CreateDirectory($@"C:\Temp\LinearRange");
            gauge.SaveImage(
                gauge.Placement.IsHorizontal() ? new Size(100, 10) : new Size(10, 100),
                $@"C:\Temp\LinearRange\{GetFileName((LinearRange)gauge.Content)}");
        }
    }
}
