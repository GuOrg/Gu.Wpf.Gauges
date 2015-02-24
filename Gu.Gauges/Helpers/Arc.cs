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
            var p0 = this.Centre + new Vector(this.Radius, 0);
            var transform = new RotateTransform(angle, this.Centre.X, this.Centre.Y);
            var p = transform.Transform(p0);
            return p;
        }

        public Point GetPoint(double angle, double offset)
        {
            var p0 = this.Centre + new Vector(this.Radius + offset, 0);
            var transform = new RotateTransform(angle, this.Centre.X, this.Centre.Y);
            var p = transform.Transform(p0);
            return p;
        }

        public bool IsLargeAngle(double fromAngle, double toAngle)
        {
            var delta = toAngle - fromAngle;
            if (delta < 180)
            {
                return false;
            }
            return true;
        }

        public SweepDirection SweepDirection(double fromAngle, double toAngle)
        {
            var delta = toAngle - fromAngle;
            return delta >= 0
                       ? System.Windows.Media.SweepDirection.Clockwise
                       : System.Windows.Media.SweepDirection.Counterclockwise;
        }
    }
}
