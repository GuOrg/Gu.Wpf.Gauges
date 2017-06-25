namespace Gu.Wpf.Gauges
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class AngularBlockBar : AngularBar
    {
        /// <summary>
        /// Identifies the <see cref="P:AngularBlockBar.Value" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:AngularBlockBar.Value" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty ValueProperty = Gauge.ValueProperty.AddOwner(
            typeof(AngularBlockBar),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty FillProperty = Shape.FillProperty.AddOwner(
            typeof(AngularBlockBar),
            new FrameworkPropertyMetadata(
                default(Brush),
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty StrokeProperty = Shape.StrokeProperty.AddOwner(
            typeof(AngularBlockBar),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty StrokeThicknessProperty = Shape.StrokeThicknessProperty.AddOwner(
            typeof(AngularBlockBar),
            new FrameworkPropertyMetadata(
                default(double),
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty TickGapProperty = AngularGauge.TickGapProperty.AddOwner(
            typeof(AngularBlockBar),
            new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty ThicknessProperty = AngularGauge.ThicknessProperty.AddOwner(
            typeof(AngularBlockBar),
            new FrameworkPropertyMetadata(
                10.0,
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Gets or sets the current magnitude of the range control.
        /// </summary>
        /// <returns>
        /// The current magnitude of the range control. The default is 0.
        /// </returns>
        public double Value
        {
            get => (double)this.GetValue(ValueProperty);
            set => this.SetValue(ValueProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="T:System.Windows.Media.Brush" /> that specifies how the shape's interior is painted.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Media.Brush" /> that describes how the shape's interior is painted. The default is HotPink.
        /// </returns>
        public Brush Fill
        {
            get => (Brush)this.GetValue(FillProperty);
            set => this.SetValue(FillProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="T:System.Windows.Media.Brush" /> that specifies how the <see cref="T:AngularBlockBar" /> outline is painted.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Media.Brush" /> that specifies how the <see cref="T:AngularBlockBar" /> outline is painted. The default is null.
        /// </returns>
        public Brush Stroke
        {
            get => (Brush)this.GetValue(StrokeProperty);
            set => this.SetValue(StrokeProperty, value);
        }

        /// <summary>
        /// Gets or sets the width of the <see cref="T:AngularBlockBar" /> outline.
        /// </summary>
        /// <returns>
        /// The width of the <see cref="T:AngularBlockBar" /> outline.
        /// </returns>
        public double StrokeThickness
        {
            get => (double)this.GetValue(StrokeThicknessProperty);
            set => this.SetValue(StrokeThicknessProperty, value);
        }

        /// <summary>
        /// Gets or sets the gap  in degrees between blocks. Default is 1.0
        /// </summary>
        public double TickGap
        {
            get => (double)this.GetValue(TickGapProperty);
            set => this.SetValue(TickGapProperty, value);
        }

        /// <summary>
        /// Gets or sets the length of the ticks.
        /// The default value is 10.
        /// </summary>
        public double Thickness
        {
            get => (double)this.GetValue(ThicknessProperty);
            set => this.SetValue(ThicknessProperty, value);
        }

        protected override void OnRender(DrawingContext dc)
        {
            var pen = new Pen(this.Stroke, this.StrokeThickness);
            pen.Freeze();

            var ticks = this.AllTicks
                            .Concat(new[] { this.Value })
                            .OrderBy(t => t);
            var arc = ArcInfo.Fill(this.RenderSize, this.MinAngle, this.MaxAngle, this.IsDirectionReversed);
            arc = arc.OffsetWith(-1 * this.ReservedSpace / 2);
            var previous = arc.Start;
            var gap = this.IsDirectionReversed ? -1 * this.TickGap : this.TickGap;

            foreach (var tick in ticks)
            {
                if (tick > this.Value)
                {
                    var a = TickHelper.ToAngle(this.Value, this.Minimum, this.Maximum, arc);
                    var block = ArcBlock(arc, previous, a, this.Thickness);
                    dc.DrawGeometry(this.Fill, pen, block);
                    break;
                }

                var angle = TickHelper.ToAngle(tick, this.Minimum, this.Maximum, arc);
                if (previous != angle)
                {
                    var arcBlock = ArcBlock(arc, previous, angle - gap, this.Thickness);
                    dc.DrawGeometry(this.Fill, pen, arcBlock);
                }

                previous = angle + gap;
            }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var rect = default(Rect);
            var arc = new ArcInfo(new Point(0, 0), this.MinAngle, this.MaxAngle, (this.ReservedSpace / 2) + this.Thickness, this.IsDirectionReversed);
            rect.Union(arc.GetPoint(arc.Start));
            var a = TickHelper.ToAngle(this.Value, this.Minimum, this.Maximum, arc);
            rect.Union(arc.GetPoint(a));
            foreach (var p in arc.GetQuadrants(arc.Start, a))
            {
                rect.Union(p);
            }

            System.Diagnostics.Debug.Assert(!rect.IsNaN(), "!rect.IsNaN()");
            System.Diagnostics.Debug.Assert(!rect.IsInfinity(), "!rect.IsInfinity()");
            return rect.Size;
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
            var sweepDirection = arcInfo.SweepDirection(fromAngle, toAngle);
            figure.Segments.Add(new ArcSegment(op2, new Size(arcInfo.Radius, arcInfo.Radius), rotationAngle, isLargeArc, sweepDirection, isStroked: true));
            figure.Segments.Add(new LineSegment(ip2, isStroked: true));
            sweepDirection = arcInfo.SweepDirection(toAngle, fromAngle);
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