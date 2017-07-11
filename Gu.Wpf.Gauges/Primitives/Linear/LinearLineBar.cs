namespace Gu.Wpf.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using Gu.Wpf.Gauges.Primitives.Linear;

    public class LinearLineBar : LineBar
    {
#pragma warning disable SA1202 // Elements must be ordered by access
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
#pragma warning restore SA1202 // Elements must be ordered by access

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
                var tickBounds = default(Rect);
                if (this.StrokeStartLineCap != PenLineCap.Flat)
                {
                    var position = this.PixelPosition(this.Minimum, finalSize);
                    if (this.Placement.IsHorizontal())
                    {
                        if (this.IsDirectionReversed)
                        {
                            RectExt.SetRight(ref tickBounds, Math.Max(0, (strokeThickness / 2) + position));
                        }
                        else
                        {
                            RectExt.SetLeft(ref tickBounds, Math.Min(0, position - (strokeThickness / 2)));
                        }
                    }
                    else
                    {
                        if (this.IsDirectionReversed)
                        {
                            RectExt.SetTop(ref tickBounds, Math.Max(0, (strokeThickness / 2) + position));
                        }
                        else
                        {
                            RectExt.SetBottom(ref tickBounds, Math.Max(0, (strokeThickness / 2) - position));
                        }
                    }
                }

                if (this.StrokeEndLineCap != PenLineCap.Flat)
                {
                    var position = this.PixelPosition(this.EffectiveValue, finalSize);
                    if (this.Placement.IsHorizontal())
                    {
                        if (this.IsDirectionReversed)
                        {
                            RectExt.SetLeft(ref tickBounds, Math.Min(0, position - (strokeThickness / 2)));
                        }
                        else
                        {
                            RectExt.SetRight(ref tickBounds, Math.Max(0, (strokeThickness / 2) + position));
                        }
                    }
                    else
                    {
                        if (this.IsDirectionReversed)
                        {
                            RectExt.SetBottom(ref tickBounds, Math.Max(0, (strokeThickness / 2) - position));
                        }
                        else
                        {
                            RectExt.SetTop(ref tickBounds, Math.Max(0, (strokeThickness / 2) + position));
                        }
                    }
                }

                this.Overflow = this.Placement.IsHorizontal()
                    ? new Thickness(Math.Max(0, -tickBounds.Left), 0, Math.Max(0, tickBounds.Right - finalSize.Width), 0)
                    : new Thickness(0, Math.Max(0, -tickBounds.Top), 0, Math.Max(0, tickBounds.Bottom - finalSize.Height));
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
            if (this.Pen == null ||
                this.EffectiveValue == this.Minimum)
            {
                return;
            }

            var strokeThickness = this.GetStrokeThickness();
            var start = this.Placement.IsHorizontal()
                ? new Point(this.PixelPosition(this.Minimum, this.RenderSize), strokeThickness / 2)
                : new Point(strokeThickness / 2, this.PixelPosition(this.EffectiveValue, this.RenderSize));
            var end = this.Placement.IsHorizontal()
                ? new Point(this.PixelPosition(this.EffectiveValue, this.RenderSize), strokeThickness / 2)
                : new Point(strokeThickness / 2, this.PixelPosition(this.Minimum, this.RenderSize));

            dc.DrawLine(this.Pen, start, end);
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