using System;
using System.Globalization;
using System.Windows;

namespace Gu.Gauges.Tests
{
    internal static class PointExt
    {
        public static Point AsPoint(this string s)
        {
            var strings = s.Split(',');
            if (strings.Length != 2)
            {
                throw new ArgumentException("", "s");
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
                throw new ArgumentException("", "s");
            }
            var x = double.Parse(strings[0], CultureInfo.InvariantCulture);
            var y = double.Parse(strings[1], CultureInfo.InvariantCulture);
            return new Vector(x, y);
        }

        public static string ToString(this Point p,string format)
        {
            return string.Format("{0}, {1}",
                p.X.ToString(format, CultureInfo.InvariantCulture),
                p.Y.ToString(format, CultureInfo.InvariantCulture));
        }

        public static string ToString(this Vector v, string format)
        {
            return string.Format("{0}, {1}",
                v.X.ToString(format, CultureInfo.InvariantCulture),
                v.Y.ToString(format, CultureInfo.InvariantCulture));
        }
    }
}