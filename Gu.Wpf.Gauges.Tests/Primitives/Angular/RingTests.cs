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

            ImageAssert.AreEqual(Properties.Resources.Ring_Thickness_10_StrokeThickness_0, ring);
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

            ImageAssert.AreEqual(Properties.Resources.Ring_Thickness_10_StrokeThickness_1, ring);
        }

        private static void SaveImage(Ring ring)
        {
            Directory.CreateDirectory($@"C:\Temp\{ring.GetType().Name}");
            ring.SaveImage(
                new Size(30, 30),
                $@"C:\Temp\{ring.GetType().Name}\{ring.GetType().Name}_Thickness_{ring.Thickness}_StrokeThickness_{ring.StrokeThickness}.png");
        }
    }
}
