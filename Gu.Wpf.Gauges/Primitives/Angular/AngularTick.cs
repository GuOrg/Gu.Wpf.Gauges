namespace Gu.Wpf.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// A tick bar that draws a tick at the position of Value
    /// </summary>
    public class AngularTick : AngularGeometryBar
    {
        /// <summary>
        /// Identifies the <see cref="P:AngularTickBar.TickWidth" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty TickWidthProperty = DependencyProperty.Register(
            nameof(TickWidth),
            typeof(double),
            typeof(AngularTick),
            new FrameworkPropertyMetadata(
                1.0d,
                FrameworkPropertyMetadataOptions.AffectsRender,
                (d, e) => ((AngularTick)d).ResetPen()));

        public static readonly DependencyProperty TickShapeProperty = DependencyProperty.Register(
            nameof(TickShape),
            typeof(TickShape),
            typeof(AngularTick),
            new FrameworkPropertyMetadata(
                default(TickShape),
                FrameworkPropertyMetadataOptions.AffectsRender,
                (d, e) => ((AngularTick)d).ResetPen()));

        static AngularTick()
        {
            StrokeProperty.OverrideMetadata(
                typeof(AngularTick),
                new FrameworkPropertyMetadata(
                    Brushes.Black,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender,
                    (d, e) => ((AngularTick)d).ResetPen()));
        }

        /// <summary>
        /// Gets or sets the <see cref="P:AngularTickBar.TickWidth" />
        /// The default is 1
        /// </summary>
        public double TickWidth
        {
            get => (double)this.GetValue(TickWidthProperty);
            set => this.SetValue(TickWidthProperty, value);
        }

        /// <summary>
        /// Specifies if ticks are drawn as rectangles or arcs.
        /// </summary>
        public TickShape TickShape
        {
            get => (TickShape)this.GetValue(TickShapeProperty);
            set => this.SetValue(TickShapeProperty, value);
        }

        protected override Geometry DefiningGeometry => throw new InvalidOperationException("Uses OnRender");

        public static PathFigure CreateTick(
            ArcInfo arc,
            Angle start,
            Angle end,
            TickShape tickShape,
            double thickness,
            double strokeThickness)
        {
            switch (tickShape)
            {
                case TickShape.Arc:
                    return CreateArcPathFigure(arc, start, end, thickness, strokeThickness);
                case TickShape.Rectangle:
                    {
                        var outerStartPoint = arc.GetPointAtRadiusOffset(start, -strokeThickness / 2);
                        var outerEndPoint = arc.GetPointAtRadiusOffset(end, -strokeThickness / 2);
                        var innerCenterPoint = arc.GetPointAtRadiusOffset((start + end) / 2, -thickness + (strokeThickness / 2));
                        var v = outerStartPoint - outerEndPoint;
                        var innerStartPoint = innerCenterPoint - (v / 2);
                        var innerEndPoint = innerStartPoint + v;
                        var isStroked = DoubleUtil.GreaterThan(strokeThickness, 0);
                        return new PathFigure(
                            innerEndPoint,
                            new[]
                            {
                            new LineSegment(outerStartPoint, isStroked),
                            new LineSegment(outerEndPoint, isStroked),
                            new LineSegment(innerStartPoint, isStroked),
                            },
                            closed: true);
                    }

                case TickShape.RingSection:
                    {
                        var po1 = arc.GetPointAtRadiusOffset(start, -strokeThickness / 2);
                        var po2 = arc.GetPointAtRadiusOffset(end, -strokeThickness / 2);
                        var ip = arc.GetPointAtRadiusOffset((start + end) / 2, -thickness + (strokeThickness / 2));
                        var v = (po1 - po2) / 2;
                        var ri = arc.Radius - thickness + (strokeThickness / 2);
                        var ai1 = arc.GetAngle(ip - v);
                        var pi1 = arc.GetPointAtRadius(ai1, ri);
                        var deltaI = 2 * Angle.Between(ip - arc.Center, pi1 - arc.Center);
                        var ai2 = ai1 - deltaI;
                        var isStroked = DoubleUtil.GreaterThan(strokeThickness, 0);
                        return new PathFigure(
                            po1,
                            new PathSegment[]
                            {
                            arc.CreateArcSegment(
                                start,
                                end,
                                arc.Radius - (strokeThickness / 2),
                                strokeThickness > 0),
                            new LineSegment(pi1, isStroked),
                            arc.CreateArcSegment(
                                ai1,
                                ai2,
                                ri,
                                strokeThickness > 0),
                            },
                            closed: true);
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static PathFigure CreateTick(
            ArcInfo arc,
            Angle angle,
            TickShape tickShape,
            double tickWidth,
            double thickness,
            double strokeThickness)
        {
            var w = tickWidth - strokeThickness;
            var delta = arc.GetDelta(w / 2);
            if (!IsFilled(tickWidth, thickness, strokeThickness) &&
                tickShape == TickShape.Arc)
            {
                return CreateArcPathFigure(arc, angle - delta, angle + delta, thickness, strokeThickness);
            }

            return CreateTick(arc, angle - delta, angle + delta, tickShape, thickness, strokeThickness);
        }

        public static bool IsFilled(double tickWidth, double thickness, double strokeThickness)
        {
            return thickness > strokeThickness &&
                   tickWidth > strokeThickness;
        }

        public static PathFigure CreateArcPathFigure(ArcInfo arc, Angle startAngle, Angle endAngle, double thickness, double strokeThickness)
        {
            if (strokeThickness > thickness ||
                double.IsInfinity(strokeThickness))
            {
                return CreateArcPathFigure(arc, startAngle, endAngle, thickness, 0);
            }

            var op1 = arc.GetPointAtRadiusOffset(startAngle, -strokeThickness / 2);
            var figure = new PathFigure { StartPoint = op1 };
            var isStroked = DoubleUtil.GreaterThan(strokeThickness, 0);
            var ro = arc.Radius - (strokeThickness / 2);
            figure.Segments.Add(arc.CreateArcSegment(startAngle, endAngle, ro, isStroked));
            if (DoubleUtil.LessThanOrClose(thickness, strokeThickness))
            {
                figure.IsClosed = false;
                figure.IsFilled = false;
                return figure;
            }

            if (thickness >= arc.Radius)
            {
                if (DoubleUtil.AreClose(strokeThickness, 0))
                {
                    figure.Segments.Add(new LineSegment(arc.Center, isStroked));
                }
                else
                {
                    var mid = (startAngle + endAngle) / 2;
                    var a = endAngle - mid;
                    var ri = strokeThickness / (2 * Math.Sin(Math.Abs(a.Radians)));
                    figure.Segments.Add(new LineSegment(arc.GetPointAtRadius(mid, ri), isStroked));
                }
            }
            else
            {
                var ip2 = arc.GetPointAtRadiusOffset(endAngle, (strokeThickness / 2) - thickness);
                figure.Segments.Add(new LineSegment(ip2, isStroked));
                if (thickness < arc.Radius)
                {
                    var ri = arc.Radius - thickness + (strokeThickness / 2);
                    figure.Segments.Add(arc.CreateArcSegment(endAngle, startAngle, ri, isStroked));
                }
            }

            figure.IsClosed = true;
            return figure;
        }

        protected override double GetStrokeThickness()
        {
            var strokeThickness = base.GetStrokeThickness();
            if (this.TickWidth <= 2 * strokeThickness)
            {
                return this.TickWidth;
            }

            return strokeThickness;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (DoubleUtil.LessThanOrClose(this.Thickness, 0) ||
                double.IsInfinity(this.Thickness))
            {
                return default(Size);
            }

            var arc = new ArcInfo(default(Point), this.Thickness, this.Start, this.End);
            var rect = default(Rect);
            rect.Union(arc.StartPoint);
            rect.Union(arc.EndPoint);
            foreach (var quadrant in arc.QuadrantPoints)
            {
                rect.Union(quadrant);
            }

            return rect.Inflate(this.Padding).Size;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var strokeThickness = this.GetStrokeThickness();
            var w = this.TickWidth > strokeThickness
                ? this.TickWidth / 2
                : strokeThickness / 2;
            var arc = new ArcInfo(default(Point), 1, this.Start, this.End);
            this.Overflow = arc.Overflow(w, this.Padding);
            return finalSize;
        }

        protected override void OnRender(DrawingContext dc)
        {
            if (this.Pen == null && this.Fill == null)
            {
                return;
            }

            var arc = ArcInfo.Fit(this.RenderSize, this.Padding, this.Start, this.End);
            if (DoubleUtil.AreClose(arc.Radius, 0))
            {
                return;
            }

            var strokeThickness = this.GetStrokeThickness();
            if (this.TickWidth <= strokeThickness)
            {
                var angle = Interpolate.Linear(this.Minimum, this.Maximum, this.Value)
                                       .Clamp(0, 1)
                                       .Interpolate(this.Start, this.End, this.IsDirectionReversed);
                var po = arc.GetPoint(angle);
                var pi = arc.GetPointAtRadiusOffset(angle, -this.Thickness);
                dc.DrawLine(this.Pen, po, pi);
            }
            else
            {
                var geometry = new PathGeometry();
                geometry.Figures.Add(this.CreateTick(arc, this.Value, strokeThickness));

                if (IsFilled(this.TickWidth, this.Thickness, strokeThickness))
                {
                    dc.DrawGeometry(this.Fill, this.Pen, geometry);
                }
                else
                {
                    dc.DrawGeometry(this.Stroke, null, geometry);
                }
            }
        }

        /// <summary>
        /// Create a <see cref="PathFigure"/> for the current tick.
        /// </summary>
        /// <param name="arc">The bounding arc.</param>
        /// <param name="value">The tick value.</param>
        /// <param name="strokeThickness">The stroke thickness.</param>
        protected virtual PathFigure CreateTick(ArcInfo arc, double value, double strokeThickness)
        {
            var angle = Interpolate.Linear(this.Minimum, this.Maximum, value)
                                   .Clamp(0, 1)
                                   .Interpolate(this.Start, this.End, this.IsDirectionReversed);
            return CreateTick(arc, angle, this.TickShape, this.TickWidth, this.Thickness, strokeThickness);
        }
    }
}
