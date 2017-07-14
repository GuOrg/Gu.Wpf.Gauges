namespace Gu.Wpf.Gauges
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Media;

    public struct ArcInfo
    {
        internal readonly Point Center;
        internal readonly double Radius;
        internal readonly double Start;
        internal readonly double End;

        public ArcInfo(Point center, double start, double end, double radius, bool isDirectionReversed)
            : this()
        {
            this.Center = center;
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

        public static ArcInfo Fill(Size availableSize, double start, double end, bool isDirectionReversed)
        {
            var fill = Fill(availableSize, start, end);
            return new ArcInfo(fill.Center, start, end, fill.Radius, isDirectionReversed);
        }

        public static ArcInfo Fill(Size availableSize, Thickness padding, double start, double end, bool isDirectionReversed)
        {
            throw new NotImplementedException("Use padding and add tests.");
            var fill = Fill(availableSize, start, end);
            return new ArcInfo(fill.Center, start, end, fill.Radius, isDirectionReversed);
        }

        public Point GetPoint(double angle)
        {
            var v0 = new Vector(this.Radius, 0);
            var rotate = v0.Rotate(angle);
            var p = this.Center + rotate;
            return p;
        }

        public Point GetPoint(double angle, double offset)
        {
            var v0 = new Vector(this.Radius + offset, 0);
            var rotate = v0.Rotate(angle);
            var p = this.Center + rotate;
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
            var q = start - (start % 90);
            while (q < end)
            {
                yield return this.GetPoint(q);
                q += 90;
            }
        }

        public ArcInfo OffsetWith(double offset)
        {
            return new ArcInfo(this.Center, this.Start, this.End, this.Radius + offset, isDirectionReversed: false);
        }

        public override string ToString()
        {
            return $"Center: {this.Center}, Radius: {this.Radius}, Start: {this.Start}, End: {this.End}";
        }

        internal static ArcInfo Fill(Size availableSize, double start, double end)
        {
            if (availableSize.Width == 0 ||
                double.IsNaN(availableSize.Width) ||
                availableSize.Height == 0 ||
                double.IsNaN(availableSize.Height))
            {
                return new ArcInfo(new Point(0, 0), start, end, 0, isDirectionReversed: false);
            }

            var p0 = new Point(0, 0);
            var arc = new ArcInfo(p0, start, end, 1, isDirectionReversed: false);
            var rect = default(Rect);
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
            return new ArcInfo(p0 + v, start, end, r, isDirectionReversed: false);
        }
    }
}
