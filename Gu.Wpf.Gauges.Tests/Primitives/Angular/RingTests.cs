namespace Gu.Wpf.Gauges.Tests.Primitives.Angular
{
    using System.IO;
    using System.Threading;
    using System.Windows;
    using System.Windows.Media;
    using Gu.Wpf.Gauges.Tests.TestHelpers;
    using NUnit.Framework;

    [Apartment(ApartmentState.STA)]
    public class RingTests
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(double.PositiveInfinity)]
        [TestCase(25)]
        public void NoStroke(double thickness)
        {
            var ring = new Ring
            {
                Fill = Brushes.Black,
                StrokeThickness = 0,
                Thickness = thickness,
            };

            SaveImage(ring);
            ImageAssert.AreEqual(GetFileName(ring), ring);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(double.PositiveInfinity)]
        [TestCase(25)]
        public void WithStroke(double thickness)
        {
            var ring = new Ring
            {
                Fill = Brushes.Black,
                Stroke = Brushes.Red,
                StrokeThickness = 1,
                Thickness = thickness,
            };

            SaveImage(ring);
            ImageAssert.AreEqual(GetFileName(ring), ring);
        }

        private static string GetFileName(Ring ring)
        {
            return $"Ring_Thickness_{ring.Thickness}_StrokeThickness_{ring.StrokeThickness}.png";
        }

        private static void SaveImage(Ring ring)
        {
            var directory = Directory.CreateDirectory($@"C:\Temp\Ring");
            ring.SaveImage(
                new Size(30, 30),
               Path.Combine(directory.FullName, GetFileName(ring)));
        }
    }
}
