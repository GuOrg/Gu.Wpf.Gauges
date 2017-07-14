namespace Gu.Wpf.Gauges.Tests.Indicators
{
    using System.IO;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using Gu.Wpf.Gauges.Tests.TestHelpers;
    using NUnit.Framework;

    [Apartment(ApartmentState.STA)]
    public class LinearIndicatorTests
    {
        [TestCase(TickBarPlacement.Left, true, 0)]
        [TestCase(TickBarPlacement.Left, false, 0)]
        [TestCase(TickBarPlacement.Right, true, 0)]
        [TestCase(TickBarPlacement.Right, false, 0)]
        [TestCase(TickBarPlacement.Top, true, 0)]
        [TestCase(TickBarPlacement.Top, false, 0)]
        [TestCase(TickBarPlacement.Bottom, true, 0)]
        [TestCase(TickBarPlacement.Bottom, false, 0)]
        [TestCase(TickBarPlacement.Left, true, 10)]
        [TestCase(TickBarPlacement.Left, false, 10)]
        [TestCase(TickBarPlacement.Right, true, 10)]
        [TestCase(TickBarPlacement.Right, false, 10)]
        [TestCase(TickBarPlacement.Top, true, 10)]
        [TestCase(TickBarPlacement.Top, false, 10)]
        [TestCase(TickBarPlacement.Bottom, true, 10)]
        [TestCase(TickBarPlacement.Bottom, false, 10)]
        [TestCase(TickBarPlacement.Left, true, 100)]
        [TestCase(TickBarPlacement.Left, false, 100)]
        [TestCase(TickBarPlacement.Right, true, 100)]
        [TestCase(TickBarPlacement.Right, false, 100)]
        [TestCase(TickBarPlacement.Top, true, 100)]
        [TestCase(TickBarPlacement.Top, false, 100)]
        [TestCase(TickBarPlacement.Bottom, true, 100)]
        [TestCase(TickBarPlacement.Bottom, false, 100)]
        public void Render(TickBarPlacement placement, bool isDirectionReversed, double value)
        {
            var range = new LinearIndicator
            {
                Value = value,
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

        [TestCase(TickBarPlacement.Left, true, 0, "0,5,0,5")]
        [TestCase(TickBarPlacement.Left, false, 0, "0,5,0,5")]
        [TestCase(TickBarPlacement.Right, true, 0, "0,5,0,5")]
        [TestCase(TickBarPlacement.Right, false, 0, "0,5,0,5")]
        [TestCase(TickBarPlacement.Top, true, 0, "5,0,5,0")]
        [TestCase(TickBarPlacement.Top, false, 0, "5,0,5,0")]
        [TestCase(TickBarPlacement.Bottom, true, 0, "5,0,5,0")]
        [TestCase(TickBarPlacement.Bottom, false, 0, "5,0,5,0")]
        [TestCase(TickBarPlacement.Left, true, 10, "0,5,0,5")]
        [TestCase(TickBarPlacement.Left, false, 10, "0,5,0,5")]
        [TestCase(TickBarPlacement.Right, true, 10, "0,5,0,5")]
        [TestCase(TickBarPlacement.Right, false, 10, "0,5,0,5")]
        [TestCase(TickBarPlacement.Top, true, 10, "5,0,5,0")]
        [TestCase(TickBarPlacement.Top, false, 10, "5,0,5,0")]
        [TestCase(TickBarPlacement.Bottom, true, 10, "5,0,5,0")]
        [TestCase(TickBarPlacement.Bottom, false, 10, "5,0,5,0")]
        [TestCase(TickBarPlacement.Left, true, 100, "0,5,0,5")]
        [TestCase(TickBarPlacement.Left, false, 100, "0,5,0,5")]
        [TestCase(TickBarPlacement.Right, true, 100, "0,5,0,5")]
        [TestCase(TickBarPlacement.Right, false, 100, "0,5,0,5")]
        [TestCase(TickBarPlacement.Top, true, 100, "5,0,5,0")]
        [TestCase(TickBarPlacement.Top, false, 100, "5,0,5,0")]
        [TestCase(TickBarPlacement.Bottom, true, 100, "5,0,5,0")]
        [TestCase(TickBarPlacement.Bottom, false, 100, "5,0,5,0")]
        public void Overflow(TickBarPlacement placement, bool isDirectionReversed, double value, string expected)
        {
            var range = new LinearIndicator
            {
                Value = value,
                Style = StyleHelper.DefaultStyle<LinearIndicator>()
            };

            var gauge = new LinearGauge
            {
                Minimum = 0,
                Maximum = 100,
                Value = value,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
                Content = range,
                Style = StyleHelper.DefaultStyle<LinearGauge>()
            };

            gauge.Arrange(new Rect(new Size(10, 10)));
            Assert.AreEqual(expected, gauge.ContentOverflow.ToString());
            Assert.AreEqual(expected, range.Overflow.ToString());

            gauge.Measure(new Size(10, 10));
            gauge.Arrange(new Rect(new Size(10, 10)));
            Assert.AreEqual(expected, gauge.ContentOverflow.ToString());
            Assert.AreEqual(expected, range.Overflow.ToString());
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