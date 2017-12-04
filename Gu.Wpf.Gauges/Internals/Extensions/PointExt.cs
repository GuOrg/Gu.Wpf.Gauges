namespace Gu.Wpf.Gauges
{
    using System;
    using System.Windows;

    internal static class PointExt
    {
        internal static Point Round(this Point p, int decimals)
        {
            return new Point(Math.Round(p.X, decimals), Math.Round(p.Y, decimals));
        }

        internal static Vector ToVector(this Point p)
        {
            return new Vector(p.X, p.Y);
        }
    }
}