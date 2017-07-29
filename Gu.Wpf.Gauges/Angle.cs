namespace Gu.Wpf.Gauges
{
    using System;
    using System.ComponentModel;
    using System.Globalization;
    using System.Text.RegularExpressions;
    using System.Windows;

    [TypeConverter(typeof(AngleTypeConverter))]
    public struct Angle : IEquatable<Angle>
    {
        public const double DegToRad = Math.PI / 180;
        public const double RadToDeg = 180 / Math.PI;

        private Angle(double degrees)
        {
            this.Degrees = degrees;
        }

        /// <summary>
        /// -140 degrees
        /// </summary>
        public static Angle DefaultStart { get; } = FromDegrees(-140);

        /// <summary>
        /// +140 degrees
        /// </summary>
        public static Angle DefaultEnd { get; } = FromDegrees(140);

        public static Angle Zero { get; } = new Angle(0);

        public double Degrees { get; }

        public double Radians => DegToRad * this.Degrees;

        public static Angle operator +(Angle left, Angle right)
        {
            return new Angle(left.Degrees + right.Degrees);
        }

        public static Angle operator -(Angle left, Angle right)
        {
            return new Angle(left.Degrees - right.Degrees);
        }

        /// <summary>
        /// Returns an <see cref="Angle"/> whose quantity is the negated quantity of the specified instance.
        /// </summary>
        /// <returns>
        /// An <see cref="Angle"/> with the same numeric quantity as this instance, but the opposite sign.
        /// </returns>
        /// <param name="angle">An instance of <see cref="Angle"/></param>
        public static Angle operator -(Angle angle)
        {
            return new Angle(-1 * angle.Degrees);
        }

        public static Angle operator *(double left, Angle right)
        {
            return new Angle(left * right.Degrees);
        }

        public static Angle operator /(Angle left, double right)
        {
            return new Angle(left.Degrees / right);
        }

        public static bool operator ==(Angle left, Angle right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Angle left, Angle right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Indicates whether a specified <see cref="Angle"/> is less than another specified <see cref="Angle"/>.
        /// </summary>
        /// <returns>
        /// true if the quantity of <paramref name="left"/> is less than the quantity of <paramref name="right"/>; otherwise, false.
        /// </returns>
        /// <param name="left">The left instance of <see cref="Angle"/>.</param>
        /// <param name="right">The right instance of <see cref="Angle"/>.</param>
        public static bool operator <(Angle left, Angle right)
        {
            return left.Degrees < right.Degrees;
        }

        /// <summary>
        /// Indicates whether a specified <see cref="Angle"/> is greater than another specified <see cref="Angle"/>.
        /// </summary>
        /// <returns>
        /// true if the quantity of <paramref name="left"/> is greater than the quantity of <paramref name="right"/>; otherwise, false.
        /// </returns>
        /// <param name="left">The left instance of <see cref="Angle"/>.</param>
        /// <param name="right">The right instance of <see cref="Angle"/>.</param>
        public static bool operator >(Angle left, Angle right)
        {
            return left.Degrees > right.Degrees;
        }

        /// <summary>
        /// Indicates whether a specified <see cref="Angle"/> is less than or equal to another specified <see cref="Angle"/>.
        /// </summary>
        /// <returns>
        /// true if the quantity of <paramref name="left"/> is less than or equal to the quantity of <paramref name="right"/>; otherwise, false.
        /// </returns>
        /// <param name="left">The left instance of <see cref="Angle"/>.</param>
        /// <param name="right">The right instance of <see cref="Angle"/>.</param>
        public static bool operator <=(Angle left, Angle right)
        {
            return left.Degrees <= right.Degrees;
        }

        /// <summary>
        /// Indicates whether a specified <see cref="Angle"/> is greater than or equal to another specified <see cref="Angle"/>.
        /// </summary>
        /// <returns>
        /// true if the quantity of <paramref name="left"/> is greater than or equal to the quantity of <paramref name="right"/>; otherwise, false.
        /// </returns>
        /// <param name="left">The left instance of <see cref="Angle"/>.</param>
        /// <param name="right">The right instance of <see cref="Angle"/>.</param>
        public static bool operator >=(Angle left, Angle right)
        {
            return left.Degrees >= right.Degrees;
        }

        public static Angle FromDegrees(double value) => new Angle(value);

        public static Angle FromRadians(double radians)
        {
            return new Angle(RadToDeg * radians);
        }

        public static Angle Between(Vector v1, Vector v2)
        {
            return FromDegrees(Vector.AngleBetween(v1, v2));
        }

        public static Angle Parse(string text)
        {
            if (TryParse(text, out Angle angle))
            {
                return angle;
            }

            throw new FormatException($"Could not parse an angle from {text}");
        }

        public static bool TryParse(string text, out Angle angle)
        {
            if (double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out double d1))
            {
                angle = FromDegrees(d1);
                return true;
            }

            var match = Regex.Match(text, "(?<number>.+) ?(°|deg|degrees)", RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
            if (match.Success)
            {
                return TryParse(match.Groups["number"].Value, out angle);
            }

            match = Regex.Match(text, "(?<number>.+) ?(rad|radians)", RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
            if (match.Success &&
                double.TryParse(
                    match.Groups["number"].Value,
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out double d3))
            {
                angle = FromRadians(d3);
                return true;
            }

            angle = default(Angle);
            return false;
        }

        public bool Equals(Angle other)
        {
            return DoubleUtil.AreClose(this, other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is Angle && this.Equals((Angle)obj);
        }

        public override int GetHashCode()
        {
            return this.Degrees.GetHashCode();
        }

        public override string ToString()
        {
            return $"{this.Degrees}";
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return $"{this.Degrees.ToString(formatProvider)}";
        }

        public Angle Clamp(Angle start, Angle end)
        {
            return FromDegrees(DoubleUtil.Clamp(this.Degrees, start.Degrees, end.Degrees));
        }
    }
}
