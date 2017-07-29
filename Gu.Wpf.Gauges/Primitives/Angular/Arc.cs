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

        public static Geometry CreateGeometry(ArcInfo arc, Angle start, Angle end, double thickness, double strokeThickness)
        {
            if (DoubleUtil.AreClose(start, end) ||
                (DoubleUtil.AreClose(thickness, 0) && DoubleUtil.AreClose(strokeThickness, 0)) ||
                DoubleUtil.AreClose(arc.Radius, 0))
            {
                return Geometry.Empty;
            }

            if (DoubleUtil.LessThanOrClose(thickness, strokeThickness))
            {
                return new PathGeometry(new[] { CreateArcPathFigure(arc, start, end, strokeThickness, strokeThickness) });
            }

            return new PathGeometry(new[] { CreateArcPathFigure(arc, start, end, thickness, strokeThickness) });
        }

        public static PathFigure CreateArcPathFigure(ArcInfo arc, Angle startAngle, Angle endAngle, double thickness, double strokeThickness)
        {
            if (strokeThickness > thickness)
            {
                return CreateArcPathFigure(arc, startAngle, endAngle, thickness, 0);
            }

            if (double.IsInfinity(strokeThickness))
            {
                strokeThickness = 0;
            }

            var op1 = arc.GetPointAtRadiusOffset(startAngle, -strokeThickness / 2);
            var figure = new PathFigure { StartPoint = op1 };
            var isStroked = DoubleUtil.GreaterThan(strokeThickness, 0);
            var ro = arc.Radius - (strokeThickness / 2);
            figure.Segments.Add(arc.CreateArcSegment(startAngle, endAngle, ro, isStroked));
            if (DoubleUtil.LessThanOrClose(thickness, strokeThickness))
            {
                figure.IsClosed = false;
                figure.IsFilled = false;
                return figure;
            }

            if (thickness >= arc.Radius)
            {
                figure.Segments.Add(new LineSegment(arc.Center, isStroked));
            }
            else
            {
                var ip2 = arc.GetPointAtRadiusOffset(endAngle, (strokeThickness / 2) - thickness);
                figure.Segments.Add(new LineSegment(ip2, isStroked));
                var ri = arc.Radius - thickness + (strokeThickness / 2);
                figure.Segments.Add(arc.CreateArcSegment(endAngle, startAngle, ri, isStroked));
            }

            figure.IsClosed = true;
            return figure;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var d = Math.Max(Math.Abs(this.Thickness), this.GetStrokeThickness());
            if (double.IsInfinity(d))
            {
                return default(Size);
            }

            return new Size(d, d);
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
                DoubleUtil.AreClose(this.End - this.Start, Angle.Zero))
            {
                var geometry = this.CreateRingGeometry();
                if (!ReferenceEquals(geometry, Geometry.Empty))
                {
                    dc.DrawGeometry(this.EffectiveFill, this.Pen, geometry);
                }

                return;
            }

            dc.DrawGeometry(this.EffectiveFill, this.Pen, this.CreateArcGeometry(value));
        }

        protected virtual Geometry CreateRingGeometry() => Ring.CreateGeometry(
            this.RenderSize,
            this.Thickness,
            this.GetStrokeThickness(),
            this.HorizontalAlignment,
            this.VerticalAlignment);

        protected virtual Geometry CreateArcGeometry(double value)
        {
            var arc = ArcInfo.Fit(this.RenderSize, this.Padding, this.Start, this.End);
            if (double.IsNaN(value) ||
                DoubleUtil.AreClose(value, this.Maximum))
            {
                return CreateGeometry(arc, this.Start, this.End, this.Thickness, this.GetStrokeThickness());
            }

            var from = this.IsDirectionReversed ? this.End : this.Start;
            var to = Interpolate.Linear(this.Minimum, this.Maximum, value)
                .Clamp(0, 1)
                .Interpolate(this.Start, this.End, this.IsDirectionReversed);
            return CreateGeometry(arc, from, to, this.Thickness, this.GetStrokeThickness());
        }
    }
}