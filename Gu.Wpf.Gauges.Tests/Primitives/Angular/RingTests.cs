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
        [Test]
        public void NoStroke()
        {
            var ring = new Ring
            {
                Fill = Brushes.Black,
                StrokeThickness = 0,
                Thickness = 10,
            };

            ImageAssert.AreEqual(GetFileName(ring), ring);
        }

        [Test]
        public void WithStroke()
        {
            var ring = new Ring
            {
                Fill = Brushes.Black,
                Stroke = Brushes.Red,
                StrokeThickness = 1,
                Thickness = 10,
            };

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
