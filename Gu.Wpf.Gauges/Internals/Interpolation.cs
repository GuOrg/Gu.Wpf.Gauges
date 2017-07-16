namespace Gu.Wpf.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Controls.Primitives;

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

        internal Point Interpolate(ArcInfo arc)
        {
            var angle = Gu.Wpf.Gauges.Interpolate.Linear(arc.Start, arc.End, this);
            return arc.GetPoint(angle);
        }

        internal double Interpolate(Size size, Thickness padding, TickBarPlacement placement, bool isDirectionReversed) => placement.IsHorizontal()
            ? this.InterpolateHorizontal(size, padding, isDirectionReversed)
            : this.InterpolateVertical(size, padding, isDirectionReversed);

        internal double InterpolateVertical(Size size, Thickness padding, bool isDirectionReversed) => this.Interpolate(size.Height - padding.Bottom, padding.Top, isDirectionReversed);

        internal double InterpolateHorizontal(Size size, Thickness padding, bool isDirectionReversed) => this.Interpolate(padding.Left, size.Width - padding.Right, isDirectionReversed);
    }
}