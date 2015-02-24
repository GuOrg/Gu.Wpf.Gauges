namespace Gu.Gauges
{
    using System.Windows;
    using System.Windows.Media;

    internal struct Arc
    {
        internal readonly Point Centre;
        internal readonly double Radius;
        internal readonly double Start;
        internal readonly double End;

        public Arc(Point centre, double start, double end, double radius, bool isDirectionReversed)
            : this()
        {
            this.Centre = centre;
            this.Radius = radius;
            if (isDirectionReversed)
            {
                this.Start = end;
                this.End = start;
            }
            else
            {
                this.Start = start;
                this.End = end;
            }
        }

        public Point GetPoint(double angle)
        {
            var p = new Point(this.Radius, 0);
            var transform = new RotateTransform(angle, this.Centre.X, this.Centre.Y);
            return transform.Transform(p);
        }

        public Point GetPoint(double angle, double offset)
        {
            var p = new Point(this.Radius + offset, 0);
            var transform = new RotateTransform(angle, this.Centre.X, this.Centre.Y);
            return transform.Transform(p);
        }
    }
}
