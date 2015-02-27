namespace Gu.Gauges.Tests.Bars
{
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    using Gu.Gauges.Tests.Helpers;

    using NUnit.Framework;

    [RequiresSTA]
    public class AngularTickBarTests
    {
        private static readonly MethodInfo MeasureOverrideMethod = typeof(AngularTextBar).GetMethod("MeasureOverride", BindingFlags.NonPublic | BindingFlags.Instance);

        [TestCase("100, 100", "100, 100")]
        public void MeasureOverride(string size, string expected)
        {
            var textBar = new AngularTextBar
                              {
                                  MinAngle = -180,
                                  MaxAngle = 0,
                                  Minimum = 0,
                                  Maximum = 100,
                                  Placement = TickBarPlacement.Left,
                                  Ticks = new DoubleCollection(new[] { 100.0 })
                              };
            var desiredSize = (Size)MeasureOverrideMethod.Invoke(textBar, new object[] { size.AsSize() });
            Assert.AreEqual(expected, desiredSize.ToString("F0"));
        }
    }
}
