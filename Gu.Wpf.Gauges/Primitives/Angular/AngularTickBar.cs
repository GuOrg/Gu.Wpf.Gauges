namespace Gu.Wpf.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class AngularTickBar : AngularGeometryTickBar
    {
        /// <summary>
        /// Identifies the <see cref="P:LinearTickBar.TickWidth" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty TickWidthProperty = DependencyProperty.Register(
            nameof(TickWidth),
            typeof(double),
            typeof(AngularTickBar),
            new FrameworkPropertyMetadata(
                1.0d,
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty TickShapeProperty = DependencyProperty.Register(
            nameof(TickShape),
            typeof(TickShape),
            typeof(AngularTickBar),
            new FrameworkPropertyMetadata(
                default(TickShape),
                FrameworkPropertyMetadataOptions.AffectsRender,
                (d, e) => ((AngularTickBar)d).ResetPen()));

        static AngularTickBar()
        {
            StrokeProperty.OverrideMetadata(
                typeof(AngularTickBar),
                new FrameworkPropertyMetadata(
                    Brushes.Black,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender,
                    (d, e) => ((AngularTickBar)d).ResetPen()));
        }

        /// <summary>
        /// Gets or sets the <see cref="P:LinearTickBar.TickWidth" />
        /// The default is 1.
        /// For TickShape.Arc the arc length of the outer diameter.
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
            if (this.Thickness <= 0 ||
                this.AllTicks == null)
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
            if ((this.Pen == null && this.Fill == null) ||
                this.AllTicks == null ||
                DoubleUtil.AreClose(this.EffectiveValue, this.Minimum))
            {
                return;
            }

            var arc = ArcInfo.Fit(this.RenderSize, this.Padding, this.Start, this.End);
            var max = this.EffectiveValue;
            var strokeThickness = this.GetStrokeThickness();
            if (max < this.Maximum)
            {
                var effectiveAngle = Interpolate.Linear(this.Minimum, this.Maximum, this.EffectiveValue)
                                                .Interpolate(this.Start, this.End, this.IsDirectionReversed);
                var geometry = new PathGeometry();
                var w = this.TickWidth > strokeThickness
                    ? (this.TickWidth + strokeThickness) / 2
                    : strokeThickness / 2;
                var delta = arc.GetDelta(w, arc.Radius - this.Thickness);
                var inflated = new ArcInfo(
                    arc.Center,
                    arc.Radius + w,
                    arc.StartAngle - delta,
                    arc.EndAngle + delta);
                var figure = inflated.CreateArcPathFigure(
                                    this.IsDirectionReversed ? inflated.EndAngle : inflated.StartAngle,
                                    effectiveAngle,
                                    inflated.Radius,
                                    0);
                geometry.Figures.Add(figure);
                dc.PushClip(geometry);
            }

            if (this.TickWidth <= strokeThickness)
            {
                foreach (var tick in this.AllTicks)
                {
                    var angle = Interpolate.Linear(this.Minimum, this.Maximum, tick)
                                           .Clamp(0, 1)
                                           .Interpolate(this.Start, this.End, this.IsDirectionReversed);
                    var po = arc.GetPoint(angle);
                    var pi = arc.GetOffsetPoint(angle, -this.Thickness);
                    dc.DrawLine(this.Pen, po, pi);
                }
            }
            else
            {
                var geometry = new PathGeometry();
                foreach (var tick in this.AllTicks)
                {
                    geometry.Figures.Add(this.CreateTick(arc, tick, this.Pen != null));
                    if (tick > max)
                    {
                        break;
                    }
                }

                dc.DrawGeometry(this.Fill, this.Pen, geometry);
            }

            if (max < this.Maximum)
            {
                dc.Pop();
            }
        }

        protected virtual PathFigure CreateTick(ArcInfo arc, double value, bool isStroked)
        {
            var interpolation = Interpolate.Linear(this.Minimum, this.Maximum, value)
                                           .Clamp(0, 1);
            var angle = interpolation.Interpolate(this.Start, this.End, this.IsDirectionReversed);
            var strokeThickness = this.GetStrokeThickness();
            switch (this.TickShape)
            {
                case TickShape.Arc:
                    var delta = arc.GetDelta((this.TickWidth - strokeThickness) / 2);
                    return arc.CreateArcPathFigure(angle - delta, angle + delta, this.Thickness, strokeThickness);
                case TickShape.Rectangle:
                    var p = interpolation.Interpolate(arc, this.IsDirectionReversed);
                    var tangent = arc.GetTangent(angle);
                    var toCenter = arc.Center - p;
                    toCenter.Normalize();
                    var w = this.TickWidth - strokeThickness;
                    var p0 = p - (w / 2 * tangent) + (strokeThickness / 2 * toCenter);
                    var p1 = p0 + (w * tangent);
                    var p2 = p1 + (this.Thickness * toCenter);
                    var p3 = p2 - (w * tangent);
                    var p4 = p3 - (this.Thickness * toCenter);
                    return new PathFigure(
                        p0,
                        new[]
                        {
                            new LineSegment(p1, isStroked),
                            new LineSegment(p2, isStroked),
                            new LineSegment(p3, isStroked),
                            new LineSegment(p4, isStroked),
                        },
                        closed: true);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}