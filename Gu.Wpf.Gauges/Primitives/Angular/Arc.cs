namespace Gu.Wpf.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class Arc : AngularGeometryBar
    {
        /// <summary>
        /// Identifies the <see cref="P:LinearGeometryBar.Value" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:LinearGeometryBar.Value" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty ValueProperty = Gauge.ValueProperty.AddOwner(
            typeof(Arc),
            new FrameworkPropertyMetadata(
                double.NaN,
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
        /// Get the value if not NaN, returns Maximum otherwise.
        /// </summary>
        protected double EffectiveValue => double.IsNaN(this.Value)
            ? this.Maximum
            : this.Value;

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
            var value = this.EffectiveValue;
            if ((this.Pen == null && this.Fill == null) ||
                DoubleUtil.AreClose(value, this.Minimum))
            {
                return;
            }

            if (DoubleUtil.AreClose(value, this.Maximum) &&
                DoubleUtil.AreClose(this.End - this.Start, 360))
            {
                var geometry = this.CreateEllipseGeometry(this.RenderSize);
                if (!ReferenceEquals(geometry, Geometry.Empty))
                {
                    dc.DrawGeometry(this.Fill, this.Pen, geometry);
                }

                return;
            }

            var arc = ArcInfo.Fit(this.RenderSize, this.Padding, this.Start, this.End);
            dc.DrawGeometry(this.Fill, this.Pen, this.CreateArcGeometry(arc, value));
        }

        protected virtual Geometry CreateEllipseGeometry(Size finalSize)
        {
            var diameter = Math.Min(finalSize.Width, finalSize.Height);
            if (DoubleUtil.AreClose(0, diameter))
            {
                return Geometry.Empty;
            }

            var strokeThickness = this.GetStrokeThickness();
            var r = (diameter - strokeThickness) / 2;
            var ri = r - this.Thickness;
            var cx = this.HorizontalAlignment == HorizontalAlignment.Stretch
                ? this.RenderSize.Width / 2
                : r + (strokeThickness / 2);
            var cy = this.VerticalAlignment == VerticalAlignment.Stretch
                ? this.RenderSize.Height / 2
                : r + (strokeThickness / 2);
            var center = new Point(cx, cy);
            if (this.Thickness <= 0 || ri <= 0)
            {
                return new EllipseGeometry(center, r, r);
            }

            return new CombinedGeometry(
                GeometryCombineMode.Xor,
                new EllipseGeometry(
                    center,
                    r,
                    r),
                new EllipseGeometry(
                    center,
                    ri,
                    ri));
        }

        protected virtual PathGeometry CreateArcGeometry(ArcInfo arc, double value)
        {
            var from = Interpolation.Zero.Interpolate(this.Start, this.End, this.IsDirectionReversed);
            var to = Interpolate.Linear(this.Minimum, this.Maximum, value)
                                .Clamp(0, 1)
                                .Interpolate(this.Start, this.End, this.IsDirectionReversed);
            var strokeThickness = this.GetStrokeThickness();
            var figure = arc.CreateArcPathFigure(from, to, this.Thickness, strokeThickness);
            return new PathGeometry(new[] { figure });
        }
    }
}