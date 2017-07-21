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

            var strokeThickness = this.GetStrokeThickness();
            foreach (var tick in ticks)
            {
                if (tick > this.Value)
                {
                    var a = Gauges.Ticks.ToAngle(this.Value, this.Minimum, this.Maximum, arc);
                    var block = ArcBlock(arc, previous, a, this.Thickness, strokeThickness);
                    dc.DrawGeometry(this.Fill, this.Pen, block);
                    break;
                }

                var angle = Gauges.Ticks.ToAngle(tick, this.Minimum, this.Maximum, arc);
                if (previous != angle)
                {
                    var arcBlock = ArcBlock(arc, previous, angle - gap, this.Thickness, strokeThickness);
                    dc.DrawGeometry(this.Fill, this.Pen, arcBlock);
                }

                previous = angle + gap;
            }
        }

        private static PathGeometry ArcBlock(ArcInfo arc, double fromAngle, double toAngle, double tickLength, double strokeThickness)
        {
            var geometry = new PathGeometry();
            var figure = arc.CreateArcPathFigure(fromAngle, toAngle, tickLength, strokeThickness);
            geometry.Figures.Add(figure);
            figure.Freeze();
            geometry.Freeze();
            return geometry;
        }
    }
}