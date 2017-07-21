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
                ? (this.TickWidth + strokeThickness) / 2
                : strokeThickness / 2;
            var arc = new ArcInfo(default(Point), 1, this.Start, this.End);
            var rect = default(Rect);
            rect.Union(default(Point) - (w * arc.GetTangent(this.Start)));
            rect.Union(default(Point) + (w * arc.GetTangent(this.End)));
            rect = rect.Deflate(this.Padding);
            this.Overflow = new Thickness(
                Math.Max(0, rect.Left),
                Math.Max(0, rect.Top),
                Math.Max(0, rect.Right),
                Math.Max(0, rect.Bottom));

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
                var figure = arc.Inflate(w)
                                .CreatePathFigure(
                                    this.IsDirectionReversed ? this.End : this.Start,
                                    effectiveAngle,
                                    2 * w,
                                    isStroked: false);
                geometry.Figures.Add(figure);
                figure.Freeze();
                geometry.Freeze();
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
                    var pi = arc.GetPoint(angle, -this.Thickness);
                    dc.DrawLine(this.Pen, po, pi);
                }
            }
            else
            {
                throw new NotImplementedException();
                //foreach (var tick in this.AllTicks)
                //{
                //    dc.DrawRectangle(this.Fill, this.Pen, this.CreateRect(tick, this.RenderSize));
                //    if (tick > max)
                //    {
                //        break;
                //    }
                //}
            }

            if (max < this.Maximum)
            {
                dc.Pop();
            }
        }
    }
}