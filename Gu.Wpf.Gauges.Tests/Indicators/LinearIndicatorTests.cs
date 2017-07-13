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
    public class LinearIndicatorTests
    {
        [TestCase(TickBarPlacement.Left, true, 10)]
        [TestCase(TickBarPlacement.Left, false, 10)]
        [TestCase(TickBarPlacement.Right, true, 10)]
        [TestCase(TickBarPlacement.Right, false, 10)]
        [TestCase(TickBarPlacement.Top, true, 10)]
        [TestCase(TickBarPlacement.Top, false, 10)]
        [TestCase(TickBarPlacement.Bottom, true, 10)]
        [TestCase(TickBarPlacement.Bottom, false, 10)]
        public void Render(TickBarPlacement placement, bool isDirectionReversed, double value)
        {
            var range = new LinearIndicator
            {
                Background = Brushes.Black,
                Style = StyleHelper.DefaultStyle<LinearIndicator>()
            };

            var gauge = new LinearGauge
            {
                Minimum = 0,
                Maximum = 100,
                Value = value,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
                Content = range
            };

            ImageAssert.AreEqual(GetFileName(range), gauge);
        }

        private static string GetFileName(LinearIndicator indicator)
        {
            return $"LinearIndicator_Placement_{indicator.Placement}_Min_{indicator.Minimum}_Max_{indicator.Maximum}_Value_{indicator.Value}_IsDirectionReversed_{indicator.IsDirectionReversed}.png";
        }

        private static void SaveImage(LinearGauge gauge)
        {
            Directory.CreateDirectory($@"C:\Temp\LinearIndicator");

            gauge.SaveImage(
                gauge.Placement.IsHorizontal() ? new Size(100, 10) : new Size(10, 100),
                $@"C:\Temp\LinearIndicator\{GetFileName((LinearIndicator)gauge.Content)}");
        }
    }
}