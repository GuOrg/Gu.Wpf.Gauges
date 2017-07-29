namespace Gu.Wpf.Gauges
{
    using System.Windows;
    using System.Windows.Controls.Primitives;

    public struct Interpolation
    {
        public static Interpolation Zero = new Interpolation(0);

        public readonly double Value;

        public Interpolation(double value)
        {
            this.Value = value;
        }

        public Interpolation Clamp(double min, double max)
        {
            return new Interpolation(this.Value.Clamp(min, max));
        }

        public double Interpolate(double min, double max) => Gauges.Interpolate.Linear(min, max, this);

        public double Interpolate(double min, double max, bool isDirectionReversed) => isDirectionReversed
            ? Gauges.Interpolate.Linear(max, min, this)
            : Gauges.Interpolate.Linear(min, max, this);

        public Angle Interpolate(Angle start, Angle end, bool isDirectionReversed) => isDirectionReversed
            ? Gauges.Interpolate.Linear(end, start, this)
            : Gauges.Interpolate.Linear(start, end, this);

        public Point Interpolate(ArcInfo arc, bool isDirectionReversed)
        {
            var angle = isDirectionReversed
                ? Gauges.Interpolate.Linear(arc.End, arc.Start, this)
                : Gauges.Interpolate.Linear(arc.Start, arc.End, this);
            return arc.GetPoint(angle);
        }

        public double Interpolate(Size size, Thickness padding, TickBarPlacement placement, bool isDirectionReversed) => placement.IsHorizontal()
            ? this.InterpolateHorizontal(size, padding, isDirectionReversed)
            : this.InterpolateVertical(size, padding, isDirectionReversed);

        public double InterpolateVertical(Size size, Thickness padding, bool isDirectionReversed) => this.Interpolate(size.Height - padding.Bottom, padding.Top, isDirectionReversed);

        public double InterpolateHorizontal(Size size, Thickness padding, bool isDirectionReversed) => this.Interpolate(padding.Left, size.Width - padding.Right, isDirectionReversed);

        public override string ToString()
        {
            return $"{this.Value}";
        }
    }
}