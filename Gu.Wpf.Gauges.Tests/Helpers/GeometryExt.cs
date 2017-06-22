namespace Gu.Wpf.Gauges.Tests.Helpers
{
    using System;
    using System.Globalization;
    using System.Windows;

    internal static class GeometryExt
    {
        public static Point AsPoint(this string s)
        {
            var strings = s.Split(',');
            if (strings.Length != 2)
            {
                throw new ArgumentException(string.Empty, nameof(s));
            }

            var x = double.Parse(strings[0], CultureInfo.InvariantCulture);
            var y = double.Parse(strings[1], CultureInfo.InvariantCulture);
            return new Point(x, y);
        }

        public static Vector AsVector(this string s)
        {
            var strings = s.Split(',');
            if (strings.Length != 2)
            {
                throw new ArgumentException(string.Empty, nameof(s));
            }

            var x = double.Parse(strings[0], CultureInfo.InvariantCulture);
            var y = double.Parse(strings[1], CultureInfo.InvariantCulture);
            return new Vector(x, y);
        }

        public static Size AsSize(this string s)
        {
            var strings = s.Split(',');
            if (strings.Length != 2)
            {
                throw new ArgumentException(string.Empty, nameof(s));
            }

            var width = double.Parse(strings[0], CultureInfo.InvariantCulture);
            var height = double.Parse(strings[1], CultureInfo.InvariantCulture);
            return new Size(width, height);
        }

        public static string ToString(this Point p, string format)
        {
            return $"{p.X.ToString(format, CultureInfo.InvariantCulture)}, {p.Y.ToString(format, CultureInfo.InvariantCulture)}";
        }

        public static string ToString(this Vector v, string format)
        {
            return
                $"{v.X.ToString(format, CultureInfo.InvariantCulture)}, {v.Y.ToString(format, CultureInfo.InvariantCulture)}";
        }

        public static string ToString(this Size size, string format)
        {
            return
                $"{size.Width.ToString(format, CultureInfo.InvariantCulture)}, {size.Height.ToString(format, CultureInfo.InvariantCulture)}";
        }
    }
}