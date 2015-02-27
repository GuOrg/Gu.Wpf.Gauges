namespace Gu.Gauges.Tests.Bars
{
    using System.Reflection;
    using System.Windows;

    using Gu.Gauges.Tests.Helpers;

    using NUnit.Framework;

    [RequiresSTA]
    public class AngularBlockBarTests
    {
        private static readonly MethodInfo MeasureOverrideMethod = typeof(AngularBlockBar).GetMethod("MeasureOverride", BindingFlags.NonPublic | BindingFlags.Instance);

        [TestCase("100, 100", -90, 0, 100, false, "100, 100")]
        [TestCase("100, 100", -90, 0, 100, true, "100, 100")]
        [TestCase("100, 100", 0, 0, 100, false, "100, 0")]
        [TestCase("100, 100", 90, 90, 100, false, "0, 100")]
        [TestCase("100, 100", -180, 0, 100, false, "100, 50")]
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
            var desiredSize = (Size)MeasureOverrideMethod.Invoke(bar, new object[] { size.AsSize() });
            Assert.AreEqual(expected, desiredSize.ToString("F0"));
        }
    }
}