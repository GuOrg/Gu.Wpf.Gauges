namespace Gu.Wpf.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    public class LinearLineBar : LineBar
    {
        /// <summary>
        /// Identifies the <see cref="P:LinearLineBar.Value" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:LinearLineBar.Value" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty ValueProperty = Gauge.ValueProperty.AddOwner(
            typeof(LinearLineBar),
            new FrameworkPropertyMetadata(
                double.NaN,
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Identifies the <see cref="P:LinearLineBar.Placement" /> dependency property. This property is read-only.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:LinearLineBar.Placement" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty PlacementProperty = LinearGauge.PlacementProperty.AddOwner(
            typeof(LinearLineBar),
            new FrameworkPropertyMetadata(
                TickBarPlacement.Bottom,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty PaddingProperty = DependencyProperty.Register(
            nameof(Padding),
            typeof(Thickness),
            typeof(LinearLineBar),
            new FrameworkPropertyMetadata(
                default(Thickness),
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));

        private static readonly DependencyPropertyKey OverflowPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(Overflow),
            typeof(Thickness),
            typeof(LinearLineBar),
            new PropertyMetadata(default(Thickness)));

        public static readonly DependencyProperty OverflowProperty = OverflowPropertyKey.DependencyProperty;

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
        /// Gets or sets where tick marks appear  relative to a <see cref="T:System.Windows.Controls.Primitives.Track" /> of a <see cref="T:System.Windows.Controls.Slider" /> control.
        /// </summary>
        /// <returns>
        /// A <see cref="T:TickBarPlacement" /> enumeration value that identifies the position of the <see cref="T:LinearTextTickBar" /> in the <see cref="T:System.Windows.Style" /> layout of a <see cref="T:System.Windows.Controls.Slider" />. The default value is <see cref="F:LinearLineBar.Top" />.
        /// </returns>
        public TickBarPlacement Placement
        {
            get => (TickBarPlacement)this.GetValue(PlacementProperty);
            set => this.SetValue(PlacementProperty, value);
        }

        public Thickness Padding
        {
            get => (Thickness)this.GetValue(PaddingProperty);
            set => this.SetValue(PaddingProperty, value);
        }

        /// <summary>
        /// Gets a <see cref="Thickness"/> with values indicating how much the control draws outside its bounds.
        /// </summary>
        public Thickness Overflow
        {
            get => (Thickness)this.GetValue(OverflowProperty);
            protected set => this.SetValue(OverflowPropertyKey, value);
        }

        protected double EffectiveValue => double.IsNaN(this.Value)
            ? this.Maximum
            : this.Value;

        protected override Geometry DefiningGeometry => throw new InvalidOperationException("Uses OnRender");

        protected override Size MeasureOverride(Size availableSize)
        {
            if (this.Placement.IsHorizontal())
            {
                return new Size(0, this.GetStrokeThickness());
            }

            return new Size(this.GetStrokeThickness(), 0);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var strokeThickness = this.GetStrokeThickness();
            if (strokeThickness > 0)
            {
                var line = this.CreateLine(this.Maximum, finalSize);
                if (this.StrokeStartLineCap != PenLineCap.Flat)
                {
                    line = line.OffsetStart(strokeThickness / 2);
                }

                if (this.StrokeEndLineCap != PenLineCap.Flat)
                {
                    line = line.OffsetEnd(strokeThickness / 2);
                }

                if (this.Placement.IsHorizontal())
                {
                    this.Overflow = new Thickness(
                        Math.Max(0, -Math.Min(line.StartPoint.X, line.EndPoint.X)),
                        0,
                        Math.Max(0, Math.Max(line.StartPoint.X, line.EndPoint.X) - finalSize.Width),
                        0);
                }
                else
                {
                    this.Overflow = new Thickness(
                        0,
                        Math.Max(0, -Math.Min(line.StartPoint.Y, line.EndPoint.Y)),
                        0,
                        Math.Max(0, Math.Max(line.StartPoint.Y, line.EndPoint.Y) - finalSize.Height));
                }
            }
            else
            {
                this.Overflow = default(Thickness);
            }

            this.RegisterOverflow(this.Overflow);
            return finalSize;
        }

        protected override void OnRender(DrawingContext dc)
        {
            if (this.Pen is null ||
               DoubleUtil.AreClose(this.EffectiveValue, this.Minimum))
            {
                return;
            }

            var line = this.CreateLine(this.EffectiveValue, this.RenderSize);
            dc.DrawLine(this.Pen, line.StartPoint, line.EndPoint);
        }

        protected virtual Line CreateLine(double value, Size renderSize)
        {
            var strokeThickness = this.GetStrokeThickness();
            var startPos = this.PixelPosition(this.Minimum, renderSize);
            var endPos = this.PixelPosition(value, renderSize);
            switch (this.Placement)
            {
                case TickBarPlacement.Left:
                    {
                        var x = this.Padding.Left + (strokeThickness / 2);
                        return new Line(
                            new Point(x, startPos),
                            new Point(x, endPos));
                    }

                case TickBarPlacement.Top:
                    {
                        var y = this.Padding.Top + (strokeThickness / 2);
                        return new Line(
                            new Point(startPos, y),
                            new Point(endPos, y));
                    }

                case TickBarPlacement.Right:
                    {
                        var x = renderSize.Width - this.Padding.Right - (strokeThickness / 2);
                        return new Line(
                            new Point(x, startPos),
                            new Point(x, endPos));
                    }

                case TickBarPlacement.Bottom:
                    {
                        var y = renderSize.Height - this.Padding.Bottom - (strokeThickness / 2);
                        return new Line(
                            new Point(startPos, y),
                            new Point(endPos, y));
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Get the interpolated pixel position for the value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="size">The render size.</param>
        protected virtual double PixelPosition(double value, Size size)
        {
            var interpolation = Interpolate.Linear(this.Minimum, this.Maximum, value);
            return interpolation.Interpolate(size, this.Padding, this.Placement, this.IsDirectionReversed);
        }
    }
}
