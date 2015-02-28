namespace Gu.Gauges
{
    using System;
    using System.Windows;

    internal static class PointExt
    {
        internal static Point Round(this Point p, int decimals)
        {
            return new Point(Math.Round(p.X, decimals), Math.Round(p.Y, decimals));
        }
    }
}