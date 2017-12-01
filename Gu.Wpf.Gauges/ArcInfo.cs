namespace Gu.Wpf.Gauges
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Media;

    public struct ArcInfo
    {
        public ArcInfo(Point center, double radius, Angle start, Angle end)
        {
            this.Center = center;
            this.Radius = radius;
            this.Start = start;
            this.End = end;
        }

        public Point Center { get; }

        public double Radius { get; }

        /// <summary>
        /// Gets or sets the start angle of the arc.
        /// Degrees clockwise from the y axis.
        /// The default is -140
        /// </summary>
        public Angle Start { get; }

        /// <summary>
        /// Gets or sets the end angle of the arc.
        /// Degrees clockwise from the y axis.
        /// The default is 140
        /// </summary>
        public Angle End { get; }

        public Point StartPoint => this.GetPoint(this.Start);

        public Point EndPoint => this.GetPoint(this.End);

        public IEnumerable<Point> QuadrantPoints
        {
            get
            {
                if (this.Start > this.End)
                {

                    var q = this.End - Angle.FromDegrees(this.End.Degrees % 90);
                    while (q <= this.Start)
                    {
                        yield return this.GetPoint(q);
                        q += Angle.FromDegrees(90);
                    }
                }
                else
                {
                    var q = this.Start - Angle.FromDegrees(this.Start.Degrees % 90);
                    while (q <= this.End)
                    {
                        yield return this.GetPoint(q);
                        q += Angle.FromDegrees(90);
                    }
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
                Angle.Parse(texts[2]),
                Angle.Parse(texts[3]));
        }

        /// <summary>
        /// Create an arc that fits the <paramref name="availableSize"/>
        /// </summary>
        [Obsolete("Don't use this")]
        public static ArcInfo Fit(Size availableSize, Angle start, Angle end)
        {
            var bounds = new Rect(availableSize);
            if (TryGetCenterAndRadius(bounds, start, end, out Point center, out double r))
            {
                return new ArcInfo(center, r, start, end);
            }

            return default(ArcInfo);
        }

        /// <summary>
        /// Create an arc that fits the <paramref name="availableSize"/>
        /// </summary>
        public static ArcInfo Fit(Size availableSize, Thickness padding, Angle start, Angle end)
        {
            var bounds = new Rect(availableSize).Deflate(padding);
            if (TryGetCenterAndRadius(bounds, start, end, out Point center, out double r))
            {
                return new ArcInfo(center, r, start, end);
            }

            return default(ArcInfo);
        }

        public static bool TryGetCenterAndRadius(Size size, Angle start, Angle end, out Point center, out double radius)
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

            return TryGetCenterAndRadius(new Rect(size), start, end, out center, out radius);
        }

        public static bool TryGetCenterAndRadius(Rect bounds, Angle start, Angle end, out Point center, out double radius)
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
            var unitArc = new ArcInfo(p0, 1, start, end);
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
        public Point GetPoint(Angle angle) => this.GetPointAtRadiusOffset(angle, 0);

        /// <summary>
        /// Get a unit vector with the tangent direction at <paramref name="angle"/>.
        /// </summary>
        /// <param name="angle">Angle in degrees</param>
        public Vector GetTangent(Angle angle)
        {
            return new Vector(1, 0).Rotate(angle);
        }

        /// <summary>
        /// Get the point at <paramref name="angle"/> on an arc with same center as this.
        /// </summary>
        /// <param name="angle">Angle in degrees</param>
        /// <param name="offset">The radial offset positive meaning bigger radius.</param>
        /// <returns></returns>
        public Point GetPointAtRadiusOffset(Angle angle, double offset) => this.GetPointAtRadius(angle, this.Radius + offset);

        /// <summary>
        /// Get the point an arc with same center as this.
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="offset">The offset arc length positive clockwise</param>
        /// <returns></returns>
        public Point GetPointAtArcOffset(Angle angle, double offset) => this.GetPointAtRadius(angle + this.GetDelta(offset), this.Radius);

        /// <summary>
        /// Get the point at <paramref name="angle"/> on an arc with same center as this.
        /// </summary>
        /// <param name="angle">Angle in degrees</param>
        /// <param name="radius">The radius.</param>
        public Point GetPointAtRadius(Angle angle, double radius)
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

        public Angle GetAngle(Point point)
        {
            return Angle.Between(new Vector(0, -1), point - this.Center);
        }

        public Thickness Overflow(double w, Thickness padding)
        {
            var rect = default(Rect);
            rect.Union(this.StartPoint - (w * this.GetTangent(this.Start)));
            rect.Union(this.StartPoint + (w * this.GetTangent(this.End)));
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
        public Angle GetDelta(double arcLength)
        {
            return Angle.FromRadians(arcLength / this.Radius);
        }

        /// <summary>
        /// Returns the angle <paramref name="arcLength"/> corresponds to on the circumference
        /// value = radius * central angle
        /// </summary>
        public Angle GetDelta(double arcLength, double radius)
        {
            if (radius <= 0)
            {
                return this.GetDelta(arcLength);
            }

            return Angle.FromRadians(arcLength / radius);
        }

        /// <summary>
        /// Create an <see cref="ArcSegment"/> with same center point as this arc.
        /// </summary>
        public IEnumerable<ArcSegment> CreateArcSegments(Angle start, Angle end, double radius, bool isStroked)
        {
            var fullRotations = Math.Truncate(Math.Abs((end - start).Degrees / 360));
            for (int i = 0; i < fullRotations; i++)
            {
                foreach (var arcSegment in this.GetCircle(start, radius, isStroked))
                {
                    yield return arcSegment;
                }
            }

            var finalSegmentStartAngle = Angle.FromDegrees(start.Degrees % 360);
            var finalSegmentEndAngle = Angle.FromDegrees(end.Degrees % 360);
            if (!DoubleUtil.AreCloseWithoutSign(finalSegmentStartAngle, finalSegmentEndAngle))
            {
                yield return this.CreateArcSegment(finalSegmentStartAngle, finalSegmentEndAngle, radius, isStroked);
            }
        }

        public override string ToString()
        {
            return $"{this.Center.X.ToString(CultureInfo.InvariantCulture)}, {this.Center.Y.ToString(CultureInfo.InvariantCulture)} {this.Radius.ToString(CultureInfo.InvariantCulture)} {this.Start.ToString(CultureInfo.InvariantCulture)} {this.End.ToString(CultureInfo.InvariantCulture)}";
        }

        private SweepDirection SweepDirection(Angle fromAngle, Angle toAngle)
        {
            return toAngle.Degrees > fromAngle.Degrees
                ? System.Windows.Media.SweepDirection.Clockwise
                : System.Windows.Media.SweepDirection.Counterclockwise;
        }

        private Rect Bounds()
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

        private IEnumerable<ArcSegment> GetCircle(Angle start, double radius, bool isStroked)
        {
            var midAngle = start + Angle.FromDegrees(180);
            var endAngle = midAngle + Angle.FromDegrees(180);
            yield return this.CreateArcSegment(start, midAngle, radius, isStroked);
            yield return this.CreateArcSegment(midAngle, endAngle, radius, isStroked);
        }

        private ArcSegment CreateArcSegment(Angle start, Angle end, double radius, bool isStroked)
        {
            var rotationAngle = Angle.Zero; // Since this is only used for circle rotation angle has no meaning. The rotation would rotate the main axis of an ellipse
            var sweepAngle = end - start;
            var isLargeArc = Math.Abs(sweepAngle.Degrees) > 180;
            var sweepDirection = this.SweepDirection(start, end);
            var endPoint = this.GetPointAtRadius(end, radius);
            return new ArcSegment(endPoint, new Size(radius, radius), Math.Abs(rotationAngle.Degrees), isLargeArc, sweepDirection, isStroked);
        }
    }
}
