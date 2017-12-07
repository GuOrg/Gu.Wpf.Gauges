namespace Gu.Wpf.Gauges
{
    using System;
    using System.Windows;

    internal static class SizeExt
    {
        internal static bool IsInfinity(this Size size)
        {
            return double.IsInfinity(size.Width) ||
                   double.IsInfinity(size.Height);
        }

        internal static bool IsNanOrEmpty(this Size size)
        {
            return double.IsNaN(size.Width) ||
                   double.IsNaN(size.Height) ||
                   size.IsEmpty;
        }

        internal static Point MidPoint(this Size size)
        {
            return new Point(size.Width / 2, size.Height / 2);
        }

        internal static Size Deflate(this Size size, Thickness padding)
        {
            return new Size(
                Math.Max(0.0, size.Width - padding.Left - padding.Right),
                Math.Max(0.0, size.Height - padding.Top - padding.Bottom));
        }

        internal static Size Rotate(this Size size, Angle angle)
        {
            var rotatedHeightVector = new Vector(0, size.Height).Rotate(angle);
            var rotatedWidthVector = new Vector(size.Width, 0).Rotate(angle);
            return new Size(
                Math.Abs(rotatedWidthVector.X) + Math.Abs(rotatedHeightVector.X),
                Math.Abs(rotatedWidthVector.Y) + Math.Abs(rotatedHeightVector.Y));
        }
    }
}
