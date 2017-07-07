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

        protected override void OnRender(DrawingContext dc)
        {
            if (this.Pen == null ||
                this.EffectiveValue == this.Minimum)
            {
                return;
            }

            var strokeThickness = this.GetStrokeThickness();
            var start = this.Placement.IsHorizontal()
                ? new Point(this.PixelPosition(this.Minimum), strokeThickness / 2)
                : new Point(strokeThickness / 2, this.PixelPosition(this.Minimum));
            var end = this.Placement.IsHorizontal()
                ? new Point(this.PixelPosition(this.EffectiveValue), strokeThickness / 2)
                : new Point(strokeThickness / 2, this.PixelPosition(this.EffectiveValue));

            dc.DrawLine(this.Pen, start, end);
        }

        /// <summary>
        /// Get the interpolated pixel position for the value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual double PixelPosition(double value)
        {
            var scale = Interpolate.Linear(this.Minimum, this.Maximum, value)
                                   .Clamp(0, 1);

            if (this.Placement.IsHorizontal())
            {
                var pos = scale * this.ActualWidth;
                return this.IsDirectionReversed
                    ? this.ActualWidth - pos
                    : pos;
            }
            else
            {
                var pos = scale * this.ActualHeight;
                return this.IsDirectionReversed
                    ? pos
                    : this.ActualHeight - pos;
            }
        }
    }
}