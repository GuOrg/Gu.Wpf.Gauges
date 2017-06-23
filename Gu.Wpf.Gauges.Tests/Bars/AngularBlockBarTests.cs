namespace Gu.Wpf.Gauges.Tests.Bars
{
    using System.Threading;
    using Gu.Wpf.Gauges.Tests.Helpers;

    using NUnit.Framework;

    [Apartment(ApartmentState.STA)]
    public class AngularBlockBarTests
    {
        [TestCase("100, 100", -90, 0, 100, false, "10, 10")]
        [TestCase("100, 100", -90, 0, 100, true, "10, 10")]
        [TestCase("100, 100", 0, 0, 100, false, "10, 0")]
        [TestCase("100, 100", 90, 90, 100, false, "0, 10")]
        [TestCase("100, 100", -180, 0, 100, false, "20, 10")]
        public void MeasureOverride(string size, double minAngle, double maxAngle, double value, bool isDirectionReversed, string expected)
        {
            var bar = new AngularBlockBar
                              {
                                  MinAngle = minAngle,
                                  MaxAngle = maxAngle,
                                  Minimum = 0,
                                  Maximum = 100,
                                  Value = value,
                                  IsDirectionReversed = isDirectionReversed
                              };
            var availableSize = size.AsSize();
            var desiredSize = bar.MeasureOverride(availableSize);
            Assert.AreEqual(expected, desiredSize.ToString("F0"));
        }
    }
}