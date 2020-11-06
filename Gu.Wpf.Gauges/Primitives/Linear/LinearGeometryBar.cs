namespace Gu.Wpf.Gauges
{
    using System.Windows;
    using System.Windows.Controls.Primitives;

    /// <summary>
    /// Base class for creating linear geometry.
    /// </summary>
    public abstract class LinearGeometryBar : GeometryBar
    {
        /// <summary>
        /// Identifies the <see cref="P:LinearGeometryBar.Placement" /> dependency property. This property is read-only.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:LinearGeometryBar.Placement" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty PlacementProperty = LinearGauge.PlacementProperty.AddOwner(
            typeof(LinearGeometryBar),
            new FrameworkPropertyMetadata(
                TickBarPlacement.Bottom,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty PaddingProperty = DependencyProperty.Register(
            nameof(Padding),
            typeof(Thickness),
            typeof(LinearGeometryBar),
            new FrameworkPropertyMetadata(
                default(Thickness),
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));

        private static readonly DependencyPropertyKey OverflowPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(Overflow),
            typeof(Thickness),
            typeof(LinearGeometryBar),
            new PropertyMetadata(
                default(Thickness),
                null,
                CoerceOverflow));

        public static readonly DependencyProperty OverflowProperty = OverflowPropertyKey.DependencyProperty;

        /// <summary>
        /// Gets or sets where tick marks appear  relative to a <see cref="T:System.Windows.Controls.Primitives.Track" /> of a <see cref="T:System.Windows.Controls.Slider" /> control.
        /// </summary>
        /// <returns>
        /// A <see cref="T:TickBarPlacement" /> enumeration value that identifies the position of the <see cref="T:LinearTextTickBar" /> in the <see cref="T:System.Windows.Style" /> layout of a <see cref="T:System.Windows.Controls.Slider" />. The default value is <see cref="F:LinearGeometryBar.Top" />.
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

        /// <summary>
        /// Get the value if not NaN, returns Maximum otherwise.
        /// </summary>
        protected double EffectiveValue => double.IsNaN(this.Value)
            ? this.Maximum
            : this.Value;

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

        private static object CoerceOverflow(DependencyObject d, object basevalue)
        {
            ((LinearGeometryBar)d).RegisterOverflow((Thickness)basevalue);
            return basevalue;
        }
    }
}
