namespace Gu.Wpf.Gauges
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Media;

    public struct ArcInfo
    {
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

        public ArcInfo(Point center, double radius, double start, double end)
        {
            this.Center = center;
            this.Radius = radius;
            this.Start = start;
            this.End = end;
        }

        public Point Center { get; }

        public double Radius { get; }

        public double Start { get; }

        public double End { get; }

        public Point StartPoint => this.GetPoint(this.Start);

        public Point EndPoint => this.GetPoint(this.End);

        public IEnumerable<Point> QuadrantPoints
        {
            get
            {
                var q = this.Start - (this.Start % 90);
                if (this.Start < this.End)
                {
                    while (q < this.End)
                    {
                        yield return this.GetPoint(q);
                        q += 90;
                    }
                }
                else
                {
                    while (q > this.End)
                    {
                        yield return this.GetPoint(q);
                        q -= 90;
                    }
                }
            }
        }

        public static ArcInfo Parse(string text)
        {
            var texts = text.Split(' ');
            if (texts.Length != 4)
            {
                throw new FormatException($"Could not parse {text}");
            }

            return new ArcInfo(
                Point.Parse(texts[0]),
                double.Parse(texts[1], CultureInfo.InvariantCulture),
                double.Parse(texts[2], CultureInfo.InvariantCulture),
                double.Parse(texts[3], CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Create an arc that fits the <paramref name="availableSize"/>
        /// </summary>
        public static ArcInfo Fit(Size availableSize, double startAngle, double endAngle, bool isDirectionReversed)
        {
            if (isDirectionReversed)
            {
                return Fit(availableSize, endAngle, startAngle, isDirectionReversed: false);
            }

            var bounds = new Rect(availableSize);
            if (TryGetCenterAndRadius(bounds, startAngle, endAngle, out Point center, out double r))
            {
                return new ArcInfo(center, r, startAngle, endAngle);
            }

            return default(ArcInfo);
        }

        /// <summary>
        /// Create an arc that fits the <paramref name="availableSize"/>
        /// </summary>
        public static ArcInfo Fit(Size availableSize, Thickness padding, double startAngle, double endAngle, bool isDirectionReversed)
        {
            if (isDirectionReversed)
            {
                return Fit(availableSize, padding, endAngle, startAngle, isDirectionReversed: false);
            }

            var bounds = new Rect(availableSize).Deflate(padding);
            if (TryGetCenterAndRadius(bounds, startAngle, endAngle, out Point center, out double r))
            {
                return new ArcInfo(center, r, startAngle, endAngle);
            }

            return default(ArcInfo);
        }

        public static bool TryGetCenterAndRadius(Size size, double startAngle, double endAngle, out Point center, out double radius)
        {
            if (size.Width == 0 ||
                double.IsNaN(size.Width) ||
                size.Height == 0 ||
                double.IsNaN(size.Height))
            {
                center = default(Point);
                radius = 0;
                return false;
            }

            return TryGetCenterAndRadius(new Rect(size), startAngle, endAngle, out center, out radius);
        }

        public static bool TryGetCenterAndRadius(Rect bounds, double startAngle, double endAngle, out Point center, out double radius)
        {
            if (bounds.Width == 0 ||
                double.IsNaN(bounds.Width) ||
                bounds.Height == 0 ||
                double.IsNaN(bounds.Height))
            {
                center = default(Point);
                radius = 0;
                return false;
            }

            var p0 = new Point(0, 0);
            var unitArc = new ArcInfo(p0, 1, startAngle, endAngle);
            var rect = default(Rect);
            var ps = unitArc.StartPoint;
            rect.Union(ps);
            rect.Union(unitArc.EndPoint);
            foreach (var quadrant in unitArc.QuadrantPoints)
            {
                rect.Union(quadrant);
            }

            var wf = bounds.Width / rect.Width;
            var hf = bounds.Height / rect.Height;
            radius = Math.Min(wf, hf);
            rect.Scale(radius, radius);
            var v = rect.FindTranslationToCenter(bounds);
            center = p0 + v;
            return true;
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

        public ArcInfo OffsetWith(double offset)
        {
            return new ArcInfo(this.Center, this.Start, this.End, this.Radius + offset, isDirectionReversed: false);
        }

        public override string ToString()
        {
            return $"Center: {this.Center}, Radius: {this.Radius}, Start: {this.Start}, End: {this.End}";
        }
    }
}
