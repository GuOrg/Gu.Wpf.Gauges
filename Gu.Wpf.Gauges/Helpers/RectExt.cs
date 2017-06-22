namespace Gu.Wpf.Gauges.Helpers
{
    using System.Windows;

    public static class RectExt
    {
        public static Point MidPoint(this Rect rect)
        {
            var v = rect.TopRight - rect.BottomLeft;
            var offset = 0.5 * v;
            return rect.BottomLeft + offset;
        }

        public static Vector FindTranslationToCenter(this Rect rect, Size size)
        {
            var rectMid = rect.MidPoint();
            var sizeMid = size.MidPoint();
            return sizeMid - rectMid;
        }
    }
}
