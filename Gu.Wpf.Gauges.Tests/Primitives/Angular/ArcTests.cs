namespace Gu.Wpf.Gauges.Tests.Primitives.Angular
{
    using System.IO;
    using System.Threading;
    using System.Windows;
    using System.Windows.Media;
    using Gu.Wpf.Gauges.Tests.Helpers;
    using Gu.Wpf.Gauges.Tests.TestHelpers;
    using NUnit.Framework;

    [Explicit]
    [Apartment(ApartmentState.STA)]
    public class ArcTests
    {
        [Test]
        public void NoStroke()
        {
            var arc = new Arc
                       {
                           Fill = Brushes.Black,
                           StrokeThickness = 0,
                           Thickness = 10,
                       };

            ImageAssert.AreEqual(GetFileName(arc), arc);
        }

        [Test]
        public void WithStroke()
        {
            var arc = new Arc
                       {
                           Fill = Brushes.Black,
                           Stroke = Brushes.Red,
                           StrokeThickness = 1,
                           Thickness = 10,
                       };

            ImageAssert.AreEqual(GetFileName(arc), arc);
        }

        [TestCase(false, 1, 1, "0 0 0 0", "0,0,0,1")]
        [TestCase(false, 4, 1, "0 0 0 0", "0,0,0,2")]
        [TestCase(false, 2, 0, "0 0 0 1", "0,0,0,0")]
        [TestCase(false, 4, 1, "0 0 0 1", "0,0,0,1")]
        [TestCase(false, 4, 1, "0 1 0 1", "0,1,0,1")]
        public void Overflow(bool isDirectionReversed, double tickWidth, double strokeThickness, string padding, string expected)
        {
            var tickBar = new Arc
                          {
                              StrokeThickness = strokeThickness,
                              Minimum = 0,
                              Maximum = 10,
                              Stroke = Brushes.Black,
                              Fill = Brushes.Red,
                              IsDirectionReversed = isDirectionReversed,
                              Padding = padding.AsThickness(),
                          };

            var gauge = new AngularGauge { Content = tickBar };
            gauge.Arrange(new Rect(new Size(10, 10)));
            Assert.AreEqual(expected, tickBar.Overflow.ToString());
            Assert.AreEqual(expected, gauge.ContentOverflow.ToString());

            gauge.Measure(new Size(10, 10));
            gauge.Arrange(new Rect(new Size(10, 10)));
            Assert.AreEqual(expected, tickBar.Overflow.ToString());
            Assert.AreEqual(expected, gauge.ContentOverflow.ToString());
        }

        private static string GetFileName(Arc arc)
        {
            return $"Arc_Thickness_{arc.Thickness}_StrokeThickness_{arc.StrokeThickness}.png";
        }

        private static void SaveImage(Arc arc)
        {
            var directory = Directory.CreateDirectory($@"C:\Temp\Arc");
            arc.SaveImage(
                new Size(30, 30),
                Path.Combine(directory.FullName, GetFileName(arc)));
        }
    }
}