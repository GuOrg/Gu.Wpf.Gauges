namespace Gu.Wpf.Gauges
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Media;

    public struct ArcInfo
    {
        public ArcInfo(Point center, double radius, double startAngle, double endAngle)
        {
            this.Center = center;
            this.Radius = radius;
            this.StartAngle = startAngle;
            this.EndAngle = endAngle;
        }

        public Point Center { get; }

        public double Radius { get; }

        /// <summary>
        /// Gets or sets the start angle of the arc.
        /// Degrees clockwise from the y axis.
        /// The default is -140
        /// </summary>
        public double StartAngle { get; }

        /// <summary>
        /// Gets or sets the end angle of the arc.
        /// Degrees clockwise from the y axis.
        /// The default is 140
        /// </summary>
        public double EndAngle { get; }

        public Point StartPoint => this.GetPoint(this.StartAngle);

        public Point EndPoint => this.GetPoint(this.EndAngle);

        public IEnumerable<Point> QuadrantPoints
        {
            get
            {
                var q = this.StartAngle - (this.StartAngle % 90);
                while (q <= this.EndAngle)
                {
                    yield return this.GetPoint(q);
                    q += 90;
                }
            }
        }

        /// <summary>
        /// Expects the format: 0,0 1 -90 90
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
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
        [Obsolete("Don't use this")]
        public static ArcInfo Fit(Size availableSize, double startAngle, double endAngle)
        {
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
        public static ArcInfo Fit(Size availableSize, Thickness padding, double startAngle, double endAngle)
        {
            var bounds = new Rect(availableSize).Deflate(padding);
            if (TryGetCenterAndRadius(bounds, startAngle, endAngle, out Point center, out double r))
            {
                return new ArcInfo(center, r, startAngle, endAngle);
            }

            return default(ArcInfo);
        }

        public static bool TryGetCenterAndRadius(Size size, double startAngle, double endAngle, out Point center, out double radius)
        {
            if (DoubleUtil.IsZero(size.Width) ||
                double.IsNaN(size.Width) ||
                DoubleUtil.IsZero(size.Height) ||
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
            if (DoubleUtil.IsZero(bounds.Width) ||
                double.IsNaN(bounds.Width) ||
                DoubleUtil.IsZero(bounds.Height) ||
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
            var v = bounds.MidPoint() - rect.MidPoint();
            center = p0 + v;
            return true;
        }

        /// <summary>
        /// Get the point at <paramref name="angle"/> on the arc.
        /// </summary>
        /// <param name="angle">Angle in degrees</param>
        public Point GetPoint(double angle) => this.GetPointAtRadiusOffset(angle, 0);

        /// <summary>
        /// Get a unit vector with the tangent direction at <paramref name="angle"/>.
        /// </summary>
        /// <param name="angle">Angle in degrees</param>
        public Vector GetTangent(double angle)
        {
            return new Vector(1, 0).Rotate(angle);
        }

        /// <summary>
        /// Get the point at <paramref name="angle"/> on an arc with same center as this.
        /// </summary>
        /// <param name="angle">Angle in degrees</param>
        /// <param name="offset">The radial offset positive meaning bigger radius.</param>
        /// <returns></returns>
        public Point GetPointAtRadiusOffset(double angle, double offset) => this.GetPointAtRadius(angle, this.Radius + offset);

        /// <summary>
        /// Get the point an arc with same center as this.
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="offset">The offset arc length positive clockwise</param>
        /// <returns></returns>
        public Point GetPointAtArcOffset(double angle, double offset) => this.GetPointAtRadius(angle + this.GetDelta(offset), this.Radius);

        /// <summary>
        /// Get the point at <paramref name="angle"/> on an arc with same center as this.
        /// </summary>
        /// <param name="angle">Angle in degrees</param>
        /// <param name="radius">The radius.</param>
        public Point GetPointAtRadius(double angle, double radius)
        {
            if (DoubleUtil.LessThanOrClose(radius, 0))
            {
                return this.Center;
            }

            var v0 = new Vector(0, -radius);
            var rotate = v0.Rotate(angle);
            var p = this.Center + rotate;
            return p;
        }

        public double GetAngle(Point point)
        {
            return Vector.AngleBetween(new Vector(0, -1), point - this.Center);
        }

        public SweepDirection SweepDirection(double fromAngle, double toAngle)
        {
            return toAngle > fromAngle
                       ? System.Windows.Media.SweepDirection.Clockwise
                       : System.Windows.Media.SweepDirection.Counterclockwise;
        }

        public Rect Bounds()
        {
            var rect = default(Rect);
            rect.Union(this.StartPoint);
            rect.Union(this.EndPoint);
            foreach (var quadrantPoint in this.QuadrantPoints)
            {
                rect.Union(quadrantPoint);
            }

            return rect;
        }

        public Thickness Overflow(double w, Thickness padding)
        {
            var rect = default(Rect);
            rect.Union(this.StartPoint - (w * this.GetTangent(this.StartAngle)));
            rect.Union(this.StartPoint + (w * this.GetTangent(this.EndAngle)));
            rect = rect.Deflate(padding);
            if (rect.IsZero())
            {
                return default(Thickness);
            }

            var bounds = this.Bounds();
            return new Thickness(
                Math.Max(0, Math.Ceiling(rect.Left)),
                Math.Max(0, Math.Ceiling(rect.Top)),
                Math.Max(0, Math.Ceiling(rect.Right - bounds.Right)),
                Math.Max(0, Math.Ceiling(rect.Bottom - bounds.Bottom)));
        }

        /// <summary>
        /// Returns the angle <paramref name="arcLength"/> corresponds to on the circumference
        /// value = radius * central angle
        /// </summary>
        public double GetDelta(double arcLength)
        {
            const double radToDeg = 180 / Math.PI;
            return radToDeg * arcLength / this.Radius;
        }

        /// <summary>
        /// Returns the angle <paramref name="arcLength"/> corresponds to on the circumference
        /// value = radius * central angle
        /// </summary>
        public double GetDelta(double arcLength, double radius)
        {
            const double radToDeg = 180 / Math.PI;
            if (radius <= 0)
            {
                return this.GetDelta(arcLength);
            }

            return radToDeg * arcLength / radius;
        }

        public ArcInfo Inflate(double value)
        {
            var delta = this.GetDelta(value);
            return new ArcInfo(
                this.Center,
                this.Radius + value,
                this.StartAngle - delta,
                this.EndAngle + delta);
        }

        public PathFigure CreateArcPathFigure(double startAngle, double endAngle, double thickness, double strokeThickness)
        {
            if (strokeThickness > thickness)
            {
                return this.CreateArcPathFigure(startAngle, endAngle, thickness, 0);
            }

            if (double.IsInfinity(strokeThickness))
            {
                strokeThickness = 0;
            }

            var op1 = this.GetPointAtRadiusOffset(startAngle, -strokeThickness / 2);
            var figure = new PathFigure { StartPoint = op1 };
            var isStroked = DoubleUtil.GreaterThan(strokeThickness, 0);
            var ro = this.Radius - (strokeThickness / 2);
            figure.Segments.Add(this.CreateArcSegment(startAngle, endAngle, ro, isStroked));
            if (DoubleUtil.LessThanOrClose(thickness, strokeThickness))
            {
                figure.IsClosed = false;
                figure.IsFilled = false;
                return figure;
            }

            var ip2 = double.IsInfinity(thickness)
                ? this.Center
                : this.GetPointAtRadiusOffset(endAngle, (strokeThickness / 2) - thickness);
            figure.Segments.Add(new LineSegment(ip2, isStroked));
            if (thickness < this.Radius)
            {
                var ri = this.Radius - thickness + (strokeThickness / 2);
                figure.Segments.Add(this.CreateArcSegment(endAngle, startAngle, ri, isStroked));
            }

            figure.IsClosed = true;
            return figure;
        }

        /// <summary>
        /// Create an <see cref="ArcSegment"/> for this arc.
        /// </summary>
        public ArcSegment CreateArcSegment(double fromAngle, double toAngle, bool isStroked) => this.CreateArcSegment(
            fromAngle,
            toAngle,
            this.Radius,
            isStroked);

        /// <summary>
        /// Create an <see cref="ArcSegment"/> with same center point as this arc.
        /// </summary>
        public ArcSegment CreateArcSegment(double fromAngle, double toAngle, double radius, bool isStroked)
        {
            var rotationAngle = toAngle - fromAngle;
            var isLargeArc = Math.Abs(rotationAngle) > 180;
            var sweepDirection = this.SweepDirection(fromAngle, toAngle);
            var endPoint = this.GetPointAtRadius(toAngle, radius);
            return new ArcSegment(endPoint, new Size(radius, radius), Math.Abs(rotationAngle), isLargeArc, sweepDirection, isStroked);
        }

        public override string ToString()
        {
            return $"{this.Center.X.ToString(CultureInfo.InvariantCulture)}, {this.Center.Y.ToString(CultureInfo.InvariantCulture)} {this.Radius.ToString(CultureInfo.InvariantCulture)} {this.StartAngle.ToString(CultureInfo.InvariantCulture)} {this.EndAngle.ToString(CultureInfo.InvariantCulture)}";
        }
    }
}
