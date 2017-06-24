namespace Gu.Wpf.Gauges
{
    using System.Windows;

    internal static class RectExt
    {
        internal static Point MidPoint(this Rect rect)
        {
            var v = rect.TopRight - rect.BottomLeft;
            var offset = 0.5 * v;
            return rect.BottomLeft + offset;
        }

        internal static Vector FindTranslationToCenter(this Rect rect, Size size)
        {
            var rectMid = rect.MidPoint();
            var sizeMid = size.MidPoint();
            return sizeMid - rectMid;
        }

        internal static bool IsNaN(this Rect rect)
        {
            return double.IsNaN(rect.X) ||
                   double.IsNaN(rect.Y) ||
                   double.IsNaN(rect.Width) ||
                   double.IsNaN(rect.Height);
        }

        internal static bool IsInfinity(this Rect rect)
        {
            return double.IsInfinity(rect.X) ||
                   double.IsInfinity(rect.Y) ||
                   double.IsInfinity(rect.Width) ||
                   double.IsInfinity(rect.Height);
        }
    }
}
