namespace Gu.Gauges
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Media;

    using Gu.Gauges.Helpers;

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

        public IEnumerable<Point> GetQuadrants(double start, double end)
        {
            var q = start - start % 90;
            while (q < end)
            {
                yield return this.GetPoint(q);
                q += 90;
            }
        }

        public Arc OffsetWith(double offset)
        {
            return new Arc(this.Centre, this.Start, this.End, this.Radius + offset, isDirectionReversed: false);
        }

        public override string ToString()
        {
            return string.Format("Centre: {0}, Radius: {1}, Start: {2}, End: {3}", this.Centre, this.Radius, this.Start, this.End);
        }

        public static Arc Fill(Size availableSize, double start, double end, bool isDirectionReversed)
        {
            var fill = Fill(availableSize, start, end);
            return new Arc(fill.Centre, start, end, fill.Radius, isDirectionReversed);
        }

        internal static Arc Fill(Size availableSize, double start, double end)
        {
            if (availableSize.Width == 0 ||
                double.IsNaN(availableSize.Width) ||
                availableSize.Height == 0 ||
                double.IsNaN(availableSize.Height))
            {
                return new Arc(new Point(0, 0), start, end, 0, isDirectionReversed: false);
            }

            var p0 = new Point(0, 0);
            var arc = new Arc(p0, start, end, 1, isDirectionReversed: false);
            var rect = new Rect();
            var ps = arc.GetPoint(start);
            rect.Union(ps);
            rect.Union(arc.GetPoint(end));
            foreach (var quadrant in arc.GetQuadrants(start, end))
            {
                rect.Union(quadrant);
            }
            var wf = availableSize.Width / rect.Width;
            var hf = availableSize.Height / rect.Height;
            var r = Math.Min(wf, hf);
            rect.Scale(r, r);
            var v = rect.FindTranslationToCenter(availableSize);
            return new Arc(p0 + v, start, end, r, isDirectionReversed: false);
        }
    }
}
