namespace Gu.Wpf.Gauges.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using NUnit.Framework;

    [Apartment(ApartmentState.STA)]
    public class LinearGaugeTests
    {
        [TestCase(TickBarPlacement.Left, false, 0)]
        [TestCase(TickBarPlacement.Left, true, 0)]
        [TestCase(TickBarPlacement.Left, false, 50)]
        [TestCase(TickBarPlacement.Left, true, 50)]
        [TestCase(TickBarPlacement.Left, false, 100)]
        [TestCase(TickBarPlacement.Left, true, 100)]
        [TestCase(TickBarPlacement.Right, false, 0)]
        [TestCase(TickBarPlacement.Right, true, 0)]
        [TestCase(TickBarPlacement.Right, false, 50)]
        [TestCase(TickBarPlacement.Right, true, 50)]
        [TestCase(TickBarPlacement.Right, false, 100)]
        [TestCase(TickBarPlacement.Right, true, 100)]
        [TestCase(TickBarPlacement.Bottom, false, 0)]
        [TestCase(TickBarPlacement.Bottom, true, 0)]
        [TestCase(TickBarPlacement.Bottom, false, 50)]
        [TestCase(TickBarPlacement.Bottom, true, 50)]
        [TestCase(TickBarPlacement.Bottom, false, 100)]
        [TestCase(TickBarPlacement.Bottom, true, 100)]
        [TestCase(TickBarPlacement.Top, false, 0)]
        [TestCase(TickBarPlacement.Top, true, 0)]
        [TestCase(TickBarPlacement.Top, false, 50)]
        [TestCase(TickBarPlacement.Top, true, 50)]
        [TestCase(TickBarPlacement.Top, false, 100)]
        [TestCase(TickBarPlacement.Top, true, 100)]
        public void Render(TickBarPlacement placement, bool isDirectionReversed, double value)
        {
            var gauge = new LinearGauge
            {
                Minimum = 0,
                Maximum = 100,
                Value = value,
                Placement = placement,
                IsDirectionReversed = isDirectionReversed,
                MajorTickFrequency = 25,
                MajorTicks = new DoubleCollection { 15 },
                MinorTickFrequency = 10,
                MinorTicks = new DoubleCollection { 8 },
                FontFamily = new FontFamily("Arial"), // Seoge UI is measured differently on Win 7 and Win 10 for some reason
                FontSize = 12,
                Style = StyleHelper.DefaultStyle<LinearGauge>(),
            };

            ImageAssert.AreEqual(GetFileName(gauge), gauge);
        }

        private static string GetFileName(LinearGauge gauge)
        {
            return $@"LinearGauge_Placement_{gauge.Placement}_Min_{gauge.Minimum}_Max_{gauge.Maximum}_Value{gauge.Value}_IsDirectionReversed_{gauge.IsDirectionReversed}.png"
                .Replace(" ", "_");
        }

        private static Size GetSize(LinearGauge gauge)
        {
            return gauge.Placement.IsHorizontal()
                ? new Size(200, 40)
                : new Size(40, 200);
        }

        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        private static void SaveImage(LinearGauge gauge)
        {
            Directory.CreateDirectory(@"C:\Temp\LinearGauge");
            gauge.SaveImage(GetSize(gauge), $@"C:\Temp\LinearGauge\{GetFileName(gauge)}");
        }
    }
}
