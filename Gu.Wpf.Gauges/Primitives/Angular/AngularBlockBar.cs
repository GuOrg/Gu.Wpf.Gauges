namespace Gu.Wpf.Gauges
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Media;

    public class AngularBlockBar : AngularGeometryTickBar
    {
        public static readonly DependencyProperty TickGapProperty = DependencyProperty.Register(
            nameof(TickGap),
            typeof(double),
            typeof(AngularBlockBar),
            new FrameworkPropertyMetadata(
                1.0,
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty TickShapeProperty = DependencyProperty.Register(
            nameof(TickShape),
            typeof(TickShape),
            typeof(AngularBlockBar),
            new FrameworkPropertyMetadata(
                default(TickShape),
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Gets or sets the gap  in degrees between blocks. Default is 1.0
        /// </summary>
        public double TickGap
        {
            get => (double)this.GetValue(TickGapProperty);
            set => this.SetValue(TickGapProperty, value);
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

        protected override Size MeasureOverride(Size availableSize)
        {
            var strokeThickness = this.GetStrokeThickness();
            var thickness = Math.Max(Math.Abs(this.Thickness), strokeThickness);
            if (double.IsInfinity(thickness))
            {
                if (double.IsInfinity(strokeThickness))
                {
                    return default(Size);
                }

                return new Size(strokeThickness, strokeThickness);
            }

            return new Size(thickness, thickness);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var strokeThickness = this.GetStrokeThickness();
            var arc = new ArcInfo(default(Point), 1, this.Start, this.End);
            this.Overflow = arc.Overflow(strokeThickness / 2, this.Padding);
            return finalSize;
        }

        protected override void OnRender(DrawingContext dc)
        {
            if ((this.Pen is null && this.Fill is null) ||
                this.AllTicks is null ||
                DoubleUtil.AreClose(this.EffectiveValue, this.Minimum))
            {
                return;
            }

            var arc = ArcInfo.Fit(this.RenderSize, this.Padding, this.Start, this.End);
            var effectiveValue = this.EffectiveValue;
            var strokeThickness = this.GetStrokeThickness().Clamp(0, arc.Radius);
            var ticksGeometry = new PathGeometry();
            var prevoius = this.Minimum;
            foreach (var tick in this.AllTicks)
            {
                if (DoubleUtil.AreClose(tick, this.Minimum) ||
                    DoubleUtil.AreClose(tick, prevoius))
                {
                    continue;
                }

                ticksGeometry.Figures.Add(this.CreateTick(arc, prevoius, Math.Min(effectiveValue, tick), strokeThickness));
                prevoius = tick;
                if (tick >= effectiveValue)
                {
                    break;
                }
            }

            if (prevoius < effectiveValue)
            {
                ticksGeometry.Figures.Add(this.CreateTick(arc, prevoius, effectiveValue, strokeThickness));
            }

            dc.DrawGeometry(this.Fill, this.Pen, ticksGeometry);
        }

        protected virtual PathFigure CreateTick(ArcInfo arc, double from, double value, double strokeThickness)
        {
            Angle Adjust(Angle angle, Angle gap)
            {
                if (this.IsDirectionReversed)
                {
                    gap = -gap;
                }

                return DoubleUtil.AreClose(angle, this.Start) ||
                       DoubleUtil.AreClose(angle, this.End)
                    ? angle
                    : (angle + gap);
            }

            var startAngle = Interpolate.Linear(this.Minimum, this.Maximum, @from)
                                        .Interpolate(this.Start, this.End, this.IsDirectionReversed);

            var endAngle = Interpolate.Linear(this.Minimum, this.Maximum, value)
                                      .Interpolate(this.Start, this.End, this.IsDirectionReversed);

            var w = (this.TickGap + strokeThickness) / 2;
            if (this.TickShape == TickShape.Arc)
            {
                var gapAngle = arc.GetDelta(w);
                return AngularTick.CreateTick(
                    arc,
                    Adjust(startAngle, gapAngle),
                    Adjust(endAngle, -gapAngle),
                    this.TickShape,
                    this.Thickness,
                    strokeThickness);
            }

            var outerRadius = Math.Max(0, arc.Radius - (strokeThickness / 2));
            var outerGapAngle = arc.GetDelta(w, outerRadius);
            var outerStartAngle = Adjust(startAngle, outerGapAngle);
            var outerEndAngle = Adjust(endAngle, -outerGapAngle);
            var outerStartPoint = arc.GetPointAtRadius(outerStartAngle, outerRadius);

            var isStroked = DoubleUtil.GreaterThan(strokeThickness, 0);

            if (double.IsInfinity(this.Thickness))
            {
                var innerPoint = arc.GetPointAtRadius((startAngle + endAngle) / 2, strokeThickness / Math.Sin((outerStartAngle - outerEndAngle).Radians));
                return new PathFigure(
                    outerStartPoint,
                    new List<PathSegment>(arc.CreateArcSegments(
                                              outerStartAngle,
                                              outerEndAngle,
                                              outerRadius,
                                              strokeThickness > 0))
                    {
                        new LineSegment(innerPoint, isStroked),
                    },
                    closed: true);
            }

            var innerRadius = Math.Max(0, arc.Radius - this.Thickness + (strokeThickness / 2));
            var innerGapAngle = arc.GetDelta(w, innerRadius);
            var innerStartAngle = Adjust(startAngle, innerGapAngle);
            var innerEndAngle = Adjust(endAngle, -innerGapAngle);
            var innerEndPoint = arc.GetPointAtRadius(innerEndAngle, innerRadius);

            var segments = new List<PathSegment>();
            segments.AddRange(arc.CreateArcSegments(
                                  outerStartAngle,
                                  outerEndAngle,
                                  outerRadius,
                                  strokeThickness > 0));
            segments.Add(new LineSegment(innerEndPoint, isStroked));
            segments.AddRange(arc.CreateArcSegments(
                                  innerEndAngle,
                                  innerStartAngle,
                                  innerRadius,
                                  strokeThickness > 0));
            return new PathFigure(
                outerStartPoint,
                segments,
                closed: true);
        }
    }
}