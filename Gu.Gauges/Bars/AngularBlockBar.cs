namespace Gu.Gauges
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
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
        public static readonly DependencyProperty ValueProperty = RangeBase.ValueProperty.AddOwner(
            typeof(AngularBlockBar),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty FillProperty = Shape.FillProperty.AddOwner(
            typeof(AngularBlockBar),
            new FrameworkPropertyMetadata(
                Brushes.HotPink,
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

        public static readonly DependencyProperty GapProperty = DependencyProperty.Register(
            "Gap",
            typeof(double),
            typeof(AngularBlockBar),
            new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static DependencyProperty TickLengthProperty =
            AngularTickBar.TickLengthProperty.AddOwner(
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
            get { return (double)this.GetValue(ValueProperty); }
            set { this.SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="T:System.Windows.Media.Brush" /> that specifies how the shape's interior is painted. 
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Media.Brush" /> that describes how the shape's interior is painted. The default is HotPink.
        /// </returns>
        public Brush Fill
        {
            get { return (Brush)this.GetValue(FillProperty); }
            set { this.SetValue(FillProperty, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="T:System.Windows.Media.Brush" /> that specifies how the <see cref="T:AngularBlockBar" /> outline is painted. 
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Media.Brush" /> that specifies how the <see cref="T:AngularBlockBar" /> outline is painted. The default is null.
        /// </returns>
        public Brush Stroke
        {
            get { return (Brush)this.GetValue(StrokeProperty); }
            set { this.SetValue(StrokeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the width of the <see cref="T:AngularBlockBar" /> outline. 
        /// </summary>
        /// <returns>
        /// The width of the <see cref="T:AngularBlockBar" /> outline.
        /// </returns>
        public double StrokeThickness
        {
            get { return (double)this.GetValue(StrokeThicknessProperty); }
            set { this.SetValue(StrokeThicknessProperty, value); }
        }

        /// <summary>
        /// Gets or sets the gap  in degrees between blocks. Default is 1.0
        /// </summary>
        public double Gap
        {
            get { return (double)this.GetValue(GapProperty); }
            set { this.SetValue(GapProperty, value); }
        }

        /// <summary>
        /// Gets or sets the length of the ticks. 
        /// The default value is 10.
        /// </summary>
        public double TickLength
        {
            get { return (double)this.GetValue(TickLengthProperty); }
            set { this.SetValue(TickLengthProperty, value); }
        }

        protected override void OnRender(DrawingContext dc)
        {
            var pen = new Pen(this.Stroke, this.StrokeThickness);
            pen.Freeze();

            var ticks = TickHelper.CreateTicks(this.Minimum, this.Maximum, this.TickFrequency)
                                  .Concat(this.Ticks ?? Enumerable.Empty<double>())
                                  .Concat(new[] { this.Value })
                                  .OrderBy(t => t);
            var arc = new Arc(new Point(this.ActualWidth / 2, this.ActualHeight / 2), this.MinAngle, this.MaxAngle, this.ActualWidth / 2 - this.ReservedSpace / 2, this.IsDirectionReversed);
            var previous = arc.Start;
            var gap = this.IsDirectionReversed ? -1 * this.Gap : this.Gap;

            foreach (var tick in ticks)
            {
                if (tick <= this.Minimum || tick > this.Maximum)
                {
                    continue;
                }
                if (tick > this.Value)
                {
                    var a = TickHelper.ToAngle(this.Value, this.Minimum, this.Maximum, arc);
                    var block = ArcBlock(arc, previous, a, this.TickLength);
                    dc.DrawGeometry(this.Fill, pen, block);
                    break;
                }

                var angle = TickHelper.ToAngle(tick, this.Minimum, this.Maximum, arc);
                var arcBlock = ArcBlock(arc, previous, angle - gap, this.TickLength);
                dc.DrawGeometry(this.Fill, pen, arcBlock);
                previous = angle + gap;
            }
        }

        private static PathGeometry ArcBlock(Arc arc, double fromAngle, double toAngle, double tickLength)
        {
            PathGeometry geometry = new PathGeometry();
            PathFigure figure = new PathFigure();

            geometry.Figures.Add(figure);
            var op1 = arc.GetPoint(fromAngle);
            var ip1 = arc.GetPoint(fromAngle, -1 * tickLength);
            var op2 = arc.GetPoint(toAngle);
            var ip2 = arc.GetPoint(toAngle, -1 * tickLength);

            figure.StartPoint = op1;
            var rotationAngle = toAngle - fromAngle;
            var isLargeArc = arc.IsLargeAngle(fromAngle, toAngle);
            var sweepDirection = arc.SweepDirection(fromAngle, toAngle);
            figure.Segments.Add(new ArcSegment(op2, new Size(arc.Radius, arc.Radius), rotationAngle, isLargeArc, sweepDirection, true));
            figure.Segments.Add(new LineSegment(ip2, true));
            sweepDirection = arc.SweepDirection(toAngle, fromAngle);
            figure.Segments.Add(new ArcSegment(ip1, new Size(arc.Radius - tickLength, arc.Radius - tickLength), rotationAngle, isLargeArc, sweepDirection, true));
            figure.Segments.Add(new LineSegment(op1, true));
            figure.IsClosed = true;
            figure.Freeze();
            geometry.Freeze();
            return geometry;
        }
    }
}