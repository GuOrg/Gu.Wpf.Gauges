namespace Gu.Wpf.Gauges
{
    using System.Windows;

    /// <summary>
    /// Base class for creating angular tick bars that renders ticks.
    /// </summary>
    public abstract class AngularGeometryTickBar : GeometryTickBar
    {
        public static readonly DependencyProperty ThicknessProperty = DependencyProperty.RegisterAttached(
            nameof(Thickness),
            typeof(double),
            typeof(AngularGeometryTickBar),
            new FrameworkPropertyMetadata(
                10.0d,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty StartProperty = AngularGauge.StartProperty.AddOwner(
            typeof(AngularGeometryTickBar),
            new FrameworkPropertyMetadata(
                Angle.DefaultStart,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty EndProperty = AngularGauge.EndProperty.AddOwner(
            typeof(AngularGeometryTickBar),
            new FrameworkPropertyMetadata(
                Angle.DefaultEnd,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:LinearGeometryBar.Value" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:LinearGeometryBar.Value" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty ValueProperty = Gauge.ValueProperty.AddOwner(
            typeof(AngularGeometryTickBar),
            new FrameworkPropertyMetadata(
                double.NaN,
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty PaddingProperty = DependencyProperty.Register(
            nameof(Padding),
            typeof(Thickness),
            typeof(AngularGeometryTickBar),
            new FrameworkPropertyMetadata(
                default(Thickness),
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));

        private static readonly DependencyPropertyKey OverflowPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(Overflow),
            typeof(Thickness),
            typeof(AngularGeometryTickBar),
            new PropertyMetadata(
                default(Thickness),
                null,
                CoerceOverflow));

        public static readonly DependencyProperty OverflowProperty = OverflowPropertyKey.DependencyProperty;

        public double Thickness
        {
            get => (double)this.GetValue(ThicknessProperty);
            set => this.SetValue(ThicknessProperty, value);
        }

        /// <summary>
        /// Gets or sets the start angle of the arc.
        /// Degrees clockwise from the y axis.
        /// The default is -140
        /// </summary>
        public Angle Start
        {
            get => (Angle)this.GetValue(StartProperty);
            set => this.SetValue(StartProperty, value);
        }

        /// <summary>
        /// Gets or sets the end angle of the arc.
        /// Degrees clockwise from the y axis.
        /// The default is 140
        /// </summary>
        public Angle End
        {
            get => (Angle)this.GetValue(EndProperty);
            set => this.SetValue(EndProperty, value);
        }

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
        protected virtual Point PixelPosition(double value, Size size)
        {
            var arc = ArcInfo.Fit(size, this.Padding, this.Start, this.End);
            return this.PixelPosition(value, arc);
        }

        /// <summary>
        /// Get the interpolated pixel position for the value.
        /// </summary>
        protected virtual Point PixelPosition(double value, ArcInfo arc)
        {
            var interpolation = Interpolate.Linear(this.Minimum, this.Maximum, value);
            return interpolation.Interpolate(arc, this.IsDirectionReversed);
        }

        private static object CoerceOverflow(DependencyObject d, object basevalue)
        {
            ((AngularGeometryTickBar)d).RegisterOverflow((Thickness)basevalue);
            return basevalue;
        }
    }
}
