namespace Gu.Wpf.Gauges
{
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(AngleTypeConverter))]
    public struct Angle
    {
        private const double DegToRad = Math.PI / 180;

        private Angle(double degrees)
        {
            this.Degrees = degrees;
        }

        public double Degrees { get; }

        public double Radians => DegToRad * this.Degrees;

        public static Angle FromDegrees(double value) => new Angle(value);
    }
}
