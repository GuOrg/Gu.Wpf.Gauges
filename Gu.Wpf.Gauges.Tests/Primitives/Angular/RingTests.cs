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

            SaveImage(ring);
            ImageAssert.AreEqual(Properties.Resources.LinearBlockBar_Min_0_Max_10_TickFrequency_1_Horizontal, ring);
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
            SaveImage(ring);
            ImageAssert.AreEqual(Properties.Resources.LinearBlockBar_Min_0_Max_10_TickFrequency_1_Horizontal, ring);
        }

        private static void SaveImage(Ring ring)
        {
            Directory.CreateDirectory(@"C:\Temp\Ring");
            ring.SaveImage(
                new Size(30, 30),
                $@"C:\Temp\Ring\Ring_Thickness_{ring.Thickness}_StrokeThickness_{ring.StrokeThickness}.png");
        }
    }
}
