namespace Gu.Gauges.Tests.Bars
{
    using System.Reflection;
    using System.Windows;

    public static class FrameworkElementExt
    {
        public static Size MeasureOverride<T>(this T element, Size availableSize) where T : FrameworkElement
        {
            var measureOverrideMethod = typeof(T).GetMethod("MeasureOverride", BindingFlags.NonPublic | BindingFlags.Instance);
            var desiredSize = (Size)measureOverrideMethod.Invoke(element, new object[] { availableSize });
            return desiredSize;
        }
    }
}