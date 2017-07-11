namespace Gu.Wpf.Gauges
{
    using System.Windows;

    internal struct Interpolation
    {
        internal static Interpolation Zero = new Interpolation(0);

        internal readonly double Value;

        internal Interpolation(double value)
        {
            this.Value = value;
        }

        internal Interpolation Clamp(double min, double max)
        {
            return new Interpolation(this.Value.Clamp(min, max));
        }

        internal double Interpolate(double min, double max) => Gu.Wpf.Gauges.Interpolate.Linear(min, max, this);

        internal double Interpolate(double min, double max, bool isDirectionReversed) => isDirectionReversed
            ? Gu.Wpf.Gauges.Interpolate.Linear(max, min, this)
            : Gu.Wpf.Gauges.Interpolate.Linear(min, max, this);

        internal double InterpolateVertical(Size size, Thickness padding, bool isDirectionReversed) => this.Interpolate(padding.Top, size.Height - padding.Bottom, isDirectionReversed);

        internal double InterpolateHorizontal(Size size, Thickness padding, bool isDirectionReversed) => this.Interpolate(padding.Left, size.Width - padding.Right, isDirectionReversed);
    }
}