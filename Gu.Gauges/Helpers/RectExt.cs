namespace Gu.Gauges
{
    using System.Windows;

    internal static class RectExt
    {
        internal static double MidX(this Rect rect)
        {
            return rect.Left + rect.Width/2;
        }

        internal static double MidY(this Rect rect)
        {
            return rect.Bottom + rect.Height / 2;
        }
    }
}
