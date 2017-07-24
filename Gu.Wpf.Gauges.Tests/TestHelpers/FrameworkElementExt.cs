namespace Gu.Wpf.Gauges.Tests
{
    using System.Reflection;
    using System.Windows;

    public static class FrameworkElementExt
    {
        // ReSharper disable once UnusedMember.Global
        public static Size MeasureOverride<T>(this T element, Size availableSize)
            where T : FrameworkElement
        {
            var measureOverrideMethod = typeof(T).GetMethod(nameof(MeasureOverride), BindingFlags.NonPublic | BindingFlags.Instance);
            var desiredSize = (Size)measureOverrideMethod.Invoke(element, new object[] { availableSize });
            return desiredSize;
        }
    }
}