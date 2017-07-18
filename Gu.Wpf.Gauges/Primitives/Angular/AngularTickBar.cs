namespace Gu.Wpf.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class AngularTickBar : AngularGeometryBar
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
            new PropertyMetadata(default(TickShape)));

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

        protected override Size MeasureOverride(Size availableSize)
        {
            if (this.Thickness <= 0 ||
                this.AllTicks == null)
            {
                return default(Size);
            }

            var arc = new ArcInfo(default(Point), this.MinAngle, this.MaxAngle, this.Thickness, this.IsDirectionReversed);
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
                ? (this.TickWidth + strokeThickness) / 2
                : strokeThickness / 2;
            throw new NotImplementedException();
            //this.Overflow = this.Placement.IsHorizontal()
            //    ? new Thickness(Math.Max(0, w - this.Padding.Left), 0, Math.Max(0, w - this.Padding.Right), 0)
            //    : new Thickness(0, Math.Max(0, w - this.Padding.Top), 0, Math.Max(0, w - this.Padding.Bottom));

            return finalSize;
        }

        protected override void OnRender(DrawingContext dc)
        {
            if (this.Pen == null ||
                this.AllTicks == null)
            {
                return;
            }

            var arc = ArcInfo.Fit(this.RenderSize, this.MinAngle, this.MaxAngle, this.IsDirectionReversed);
            foreach (var tick in this.AllTicks)
            {
                var angle = Gauges.Ticks.ToAngle(tick, this.Minimum, this.Maximum, arc);
                var po = arc.GetPoint(angle, 0);
                var pi = arc.GetPoint(angle, -this.Thickness);
                var line = new Line(po, pi);
                dc.DrawLine(this.Pen, line);
            }
        }
    }
}