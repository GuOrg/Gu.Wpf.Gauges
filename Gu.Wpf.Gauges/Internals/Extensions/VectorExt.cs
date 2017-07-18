namespace Gu.Wpf.Gauges
{
    using System;
    using System.Windows;

    internal static class VectorExt
    {
        private const double DegToRad = Math.PI / 180;

        /// <summary>
        /// Rotates the vector clockwise
        /// </summary>
        public static Vector Rotate(this Vector v, double degrees)
        {
            return v.RotateRadians(degrees * DegToRad);
        }

        /// <summary>
        /// Rotates the vector clockwise
        /// </summary>
        public static Vector RotateRadians(this Vector v, double radians)
        {
            var ca = Math.Cos(-radians);
            var sa = Math.Sin(-radians);
            return new Vector((ca * v.X) - (sa * v.Y), (sa * v.X) + (ca * v.Y));
        }

        internal static Vector Round(this Vector v, int decimals)
        {
            return new Vector(Math.Round(v.X, decimals), Math.Round(v.Y, decimals));
        }
    }
}
