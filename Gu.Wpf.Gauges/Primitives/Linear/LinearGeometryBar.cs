namespace Gu.Wpf.Gauges
{
    using System.Windows;
    using System.Windows.Controls.Primitives;

    public abstract class LinearGeometryBar : GeometryBar
    {
        /// <summary>
        /// Identifies the <see cref="P:LinearBlockBar.Placement" /> dependency property. This property is read-only.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:LinearBlockBar.Placement" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty PlacementProperty = LinearGauge.PlacementProperty.AddOwner(
            typeof(LinearGeometryBar),
            new FrameworkPropertyMetadata(
                TickBarPlacement.Bottom,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Gets or sets where tick marks appear  relative to a <see cref="T:System.Windows.Controls.Primitives.Track" /> of a <see cref="T:System.Windows.Controls.Slider" /> control.
        /// </summary>
        /// <returns>
        /// A <see cref="T:TickBarPlacement" /> enumeration value that identifies the position of the <see cref="T:LinearTextTickBar" /> in the <see cref="T:System.Windows.Style" /> layout of a <see cref="T:System.Windows.Controls.Slider" />. The default value is <see cref="F:LinearBlockBar.Top" />.
        /// </returns>
        public TickBarPlacement Placement
        {
            get => (TickBarPlacement)this.GetValue(PlacementProperty);
            set => this.SetValue(PlacementProperty, value);
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

            var strokeThickness = this.GetStrokeThickness();
            if (this.Placement.IsHorizontal())
            {
                var pos = (strokeThickness / 2) + (scale * (this.ActualWidth - strokeThickness));
                return this.IsDirectionReversed
                    ? this.ActualWidth - pos
                    : pos;
            }
            else
            {
                var pos = (strokeThickness / 2) + (scale * (this.ActualHeight - strokeThickness));
                return this.IsDirectionReversed
                    ? pos
                    : this.ActualHeight - pos;
            }
        }
    }
}