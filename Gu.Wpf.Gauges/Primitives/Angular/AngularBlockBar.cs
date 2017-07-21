namespace Gu.Wpf.Gauges
{
    using System;
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
            var thickness = Math.Max(Math.Abs(this.Thickness), this.GetStrokeThickness());
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
            if ((this.Pen == null && this.Fill == null) ||
                this.AllTicks == null ||
                DoubleUtil.AreClose(this.EffectiveValue, this.Minimum))
            {
                return;
            }

            var arc = ArcInfo.Fit(this.RenderSize, this.Padding, this.Start, this.End);
            var value = this.EffectiveValue;
            var strokeThickness = this.GetStrokeThickness();
            var effectiveAngle = Interpolate.Linear(this.Minimum, this.Maximum, this.EffectiveValue)
                .Interpolate(this.Start, this.End, this.IsDirectionReversed);
            if (value < this.Maximum)
            {
                dc.PushClip(
                    Arc.CreateGeometry(
                        arc,
                        this.IsDirectionReversed ? this.End : this.Start,
                        effectiveAngle,
                        this.Thickness,
                        0));
            }

            var gapsGeometry = new PathGeometry();
            foreach (var tick in this.AllTicks)
            {
                gapsGeometry.Figures.Add(this.CreateTickGap(arc, tick, strokeThickness));
                if (tick > value)
                {
                    break;
                }
            }

            var arcGeometry = Arc.CreateGeometry(
                arc,
                this.IsDirectionReversed ? this.End : this.Start,
                effectiveAngle,
                this.Thickness,
                strokeThickness);
            dc.DrawGeometry(this.Fill, this.Pen, new CombinedGeometry(
                GeometryCombineMode.Exclude,
                arcGeometry,
                gapsGeometry));

            if (value < this.Maximum)
            {
                dc.Pop();
            }
        }

        protected virtual PathFigure CreateTickGap(ArcInfo arc, double value, double strokeThickness)
        {
            var interpolation = Interpolate.Linear(this.Minimum, this.Maximum, value)
                                           .Clamp(0, 1);
            var angle = interpolation.Interpolate(this.Start, this.End, this.IsDirectionReversed);
            switch (this.TickShape)
            {
                case TickShape.Arc:
                    var delta = arc.GetDelta((this.TickGap + strokeThickness) / 2);
                    return arc.CreateArcPathFigure(angle - delta, angle + delta, this.Thickness, 0);
                case TickShape.Rectangle:
                    var p = interpolation.Interpolate(arc, this.IsDirectionReversed);
                    var tangent = arc.GetTangent(angle);
                    var toCenter = arc.Center - p;
                    var w = this.TickGap + strokeThickness;
                    var p0 = p - (w / 2 * tangent);
                    var p1 = p0 + (w * tangent);
                    var p2 = p1 + toCenter;
                    var p3 = p2 - (w * tangent);
                    var p4 = p3 - toCenter;
                    return new PathFigure(
                        p0,
                        new[]
                        {
                            new LineSegment(p1, isStroked: false),
                            new LineSegment(p2, isStroked: false),
                            new LineSegment(p3, isStroked: false),
                            new LineSegment(p4, isStroked: false),
                        },
                        closed: true);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}