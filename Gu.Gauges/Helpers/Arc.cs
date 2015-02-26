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
            var v0 = new Vector(this.Radius, 0);
            var rotate = v0.Rotate(angle);
            var p = this.Centre + rotate;
            return p;
        }

        public Point GetPoint(double angle, double offset)
        {
            var v0 = new Vector(this.Radius + offset, 0);
            var rotate = v0.Rotate(angle);
            var p = this.Centre + rotate;
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

        public override string ToString()
        {
            return string.Format("Centre: {0}, Radius: {1}, Start: {2}, End: {3}", this.Centre, this.Radius, this.Start, this.End);
        }
    }
}
