namespace Gu.Wpf.Gauges.Tests
{
    using System;
    using System.Globalization;
    using System.Windows;

    internal static class GeometryExt
    {
        public static Point AsPoint(this string text)
        {
            var strings = text.Split(',');
            if (strings.Length != 2)
            {
                throw new ArgumentException(string.Empty, nameof(text));
            }

            var x = double.Parse(strings[0], CultureInfo.InvariantCulture);
            var y = double.Parse(strings[1], CultureInfo.InvariantCulture);
            return new Point(x, y);
        }

        public static Vector AsVector(this string text)
        {
            var strings = text.Split(',');
            if (strings.Length != 2)
            {
                throw new ArgumentException(string.Empty, nameof(text));
            }

            var x = double.Parse(strings[0], CultureInfo.InvariantCulture);
            var y = double.Parse(strings[1], CultureInfo.InvariantCulture);
            return new Vector(x, y);
        }

        public static Size AsSize(this string text)
        {
            var strings = text.Split(',');
            if (strings.Length != 2)
            {
                throw new ArgumentException(string.Empty, nameof(text));
            }

            var width = double.Parse(strings[0], CultureInfo.InvariantCulture);
            var height = double.Parse(strings[1], CultureInfo.InvariantCulture);
            return new Size(width, height);
        }

        public static Thickness AsThickness(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return default(Thickness);
            }

            var strings = text.Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (strings.Length == 1)
            {
                return new Thickness(double.Parse(strings[0], CultureInfo.InvariantCulture));
            }

            if (strings.Length == 2)
            {
                var d0 = double.Parse(strings[0], CultureInfo.InvariantCulture);
                var d1 = double.Parse(strings[1], CultureInfo.InvariantCulture);
                return new Thickness(d0, d1, d0, d1);
            }

            if (strings.Length == 4)
            {
                return new Thickness(
                    double.Parse(strings[0], CultureInfo.InvariantCulture),
                    double.Parse(strings[1], CultureInfo.InvariantCulture),
                    double.Parse(strings[2], CultureInfo.InvariantCulture),
                    double.Parse(strings[3], CultureInfo.InvariantCulture));
            }

            throw new FormatException($"Could not parse {text}");
        }

        public static string ToString(this Point p, string format)
        {
            return $"{p.X.ToString(format, CultureInfo.InvariantCulture)}, {p.Y.ToString(format, CultureInfo.InvariantCulture)}";
        }

        public static string ToString(this Vector v, string format)
        {
            return $"{v.X.ToString(format, CultureInfo.InvariantCulture)}, {v.Y.ToString(format, CultureInfo.InvariantCulture)}";
        }

        public static string ToString(this Size size, string format)
        {
            return $"{size.Width.ToString(format, CultureInfo.InvariantCulture)}, {size.Height.ToString(format, CultureInfo.InvariantCulture)}";
        }
    }
}