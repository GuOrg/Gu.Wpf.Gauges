namespace Gu.Wpf.Gauges
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;

    public class AngularBlockBar : AngularGeometryBar
    {
        public static readonly DependencyProperty TickGapProperty = DependencyProperty.Register(
            nameof(TickGap),
            typeof(double),
            typeof(AngularBlockBar),
            new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty TickShapeProperty = DependencyProperty.Register(
            nameof(TickShape),
            typeof(TickShape),
            typeof(AngularBlockBar),
            new PropertyMetadata(default(TickShape)));

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
            var thickness = Math.Abs(this.StrokeThickness + this.Thickness);
            return new Size(thickness, thickness);
        }

        protected override void OnRender(DrawingContext dc)
        {
            if ((this.Stroke == null && this.Fill == null) || this.AllTicks == null)
            {
                return;
            }

            var ticks = this.AllTicks
                            .Concat(new[] { this.Value })
                            .OrderBy(t => t);
            var arc = ArcInfo.Fit(this.RenderSize, this.Padding, this.Start, this.End);
            var previous = arc.StartAngle;
            var gap = this.IsDirectionReversed ? -1 * this.TickGap : this.TickGap;

            foreach (var tick in ticks)
            {
                if (tick > this.Value)
                {
                    var a = Gauges.Ticks.ToAngle(this.Value, this.Minimum, this.Maximum, arc);
                    var block = ArcBlock(arc, previous, a, this.Thickness);
                    dc.DrawGeometry(this.Fill, this.Pen, block);
                    break;
                }

                var angle = Gauges.Ticks.ToAngle(tick, this.Minimum, this.Maximum, arc);
                if (previous != angle)
                {
                    var arcBlock = ArcBlock(arc, previous, angle - gap, this.Thickness);
                    dc.DrawGeometry(this.Fill, this.Pen, arcBlock);
                }

                previous = angle + gap;
            }
        }

        private static PathGeometry ArcBlock(ArcInfo arcInfo, double fromAngle, double toAngle, double tickLength)
        {
            var geometry = new PathGeometry();
            var figure = new PathFigure();

            geometry.Figures.Add(figure);
            var op1 = arcInfo.GetPoint(fromAngle);
            var ip1 = arcInfo.GetPoint(fromAngle, -1 * tickLength);
            var op2 = arcInfo.GetPoint(toAngle);
            var ip2 = arcInfo.GetPoint(toAngle, -1 * tickLength);

            figure.StartPoint = op1;
            var rotationAngle = toAngle - fromAngle;
            var isLargeArc = arcInfo.IsLargeAngle(fromAngle, toAngle);
            var sweepDirection = arcInfo.SweepDirection_(fromAngle, toAngle);
            figure.Segments.Add(new ArcSegment(op2, new Size(arcInfo.Radius, arcInfo.Radius), rotationAngle, isLargeArc, sweepDirection, isStroked: true));
            figure.Segments.Add(new LineSegment(ip2, isStroked: true));
            sweepDirection = arcInfo.SweepDirection_(toAngle, fromAngle);
            var ri = arcInfo.Radius - tickLength;
            if (ri < 0)
            {
                ri = 0;
            }

            figure.Segments.Add(new ArcSegment(ip1, new Size(ri, ri), rotationAngle, isLargeArc, sweepDirection, isStroked: true));
            figure.Segments.Add(new LineSegment(op1, isStroked: true));
            figure.IsClosed = true;
            figure.Freeze();
            geometry.Freeze();
            return geometry;
        }
    }
}