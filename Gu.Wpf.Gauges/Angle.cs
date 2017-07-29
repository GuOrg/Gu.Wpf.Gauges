namespace Gu.Wpf.Gauges
{
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(AngleTypeConverter))]
    public struct Angle : IEquatable<Angle>
    {
        public const double DegToRad = Math.PI / 180;
        public const double RadToDeg = 180 / Math.PI;

        private Angle(double degrees)
        {
            this.Degrees = degrees;
        }

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

        public static bool operator ==(Angle left, Angle right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Angle left, Angle right)
        {
            return !left.Equals(right);
        }

        public static Angle FromDegrees(double value) => new Angle(value);

        public static Angle FromRadians(double radians)
        {
            return new Angle(RadToDeg * radians);
        }

        public bool Equals(Angle other)
        {
            return this.Degrees.Equals(other.Degrees);
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
    }
}
