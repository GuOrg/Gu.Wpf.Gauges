namespace Gu.Wpf.Gauges
{
    using System;
    using System.Windows;

    internal static class VectorExt
    {
        /// <summary>
        /// Rotates the vector clockwise
        /// </summary>
        public static Vector Rotate(this Vector v, Angle angle)
        {
            return v.RotateRadians(angle.Radians);
        }

        /// <summary>
        /// Rotates the vector counterclockwise
        /// </summary>
        public static Vector RotateRadians(this Vector v, double radians)
        {
            var ca = Math.Cos(radians);
            var sa = Math.Sin(radians);
            return new Vector((ca * v.X) - (sa * v.Y), (sa * v.X) + (ca * v.Y));
        }

        internal static Vector Round(this Vector v, int decimals)
        {
            return new Vector(Math.Round(v.X, decimals), Math.Round(v.Y, decimals));
        }

        internal static Angle Angle(this Vector v)
        {
            return Gauges.Angle.FromRadians(Math.Atan2(v.Y, v.X));
        }

        internal static Angle GaugeAngle(this Vector v) // Has zero at -Y
        {
            return Gauges.Angle.FromRadians(Math.Atan2(v.Y, v.X) + (Math.PI / 2));
        }

        internal static Point ToPoint(this Vector v)
        {
            return new Point(v.X, v.Y);
        }
    }
}
