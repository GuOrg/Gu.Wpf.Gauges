namespace Gu.Wpf.Gauges
{
    using System.Windows;
    using Gu.Wpf.Gauges.Primitives.Linear;

    public abstract class AngularGeometryBar : GeometryBar
    {
#pragma warning disable SA1202 // Elements must be ordered by access

        public static readonly DependencyProperty MinAngleProperty = AngularGauge.MinAngleProperty.AddOwner(
            typeof(AngularGeometryBar),
            new FrameworkPropertyMetadata(
                -180.0,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty MaxAngleProperty = AngularGauge.MaxAngleProperty.AddOwner(
            typeof(AngularGeometryBar),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:LinearGeometryBar.Value" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:LinearGeometryBar.Value" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty ValueProperty = Gauge.ValueProperty.AddOwner(
            typeof(AngularGeometryBar),
            new FrameworkPropertyMetadata(
                double.NaN,
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty PaddingProperty = DependencyProperty.Register(
            nameof(Padding),
            typeof(Thickness),
            typeof(AngularGeometryBar),
            new FrameworkPropertyMetadata(
                default(Thickness),
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));

        private static readonly DependencyPropertyKey OverflowPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(Overflow),
            typeof(Thickness),
            typeof(AngularGeometryBar),
            new PropertyMetadata(
                default(Thickness),
                null,
                CoerceOverflow));

        public static readonly DependencyProperty OverflowProperty = OverflowPropertyKey.DependencyProperty;
#pragma warning restore SA1202 // Elements must be ordered by access

        /// <summary>
        /// Gets or sets the <see cref="P:AngularBar.MinAngle" />
        /// The default is -180
        /// </summary>
        public double MinAngle
        {
            get => (double)this.GetValue(MinAngleProperty);
            set => this.SetValue(MinAngleProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="P:AngularBar.MaxAngle" />
        /// The default is 0
        /// </summary>
        public double MaxAngle
        {
            get => (double)this.GetValue(MaxAngleProperty);
            set => this.SetValue(MaxAngleProperty, value);
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
            var arc = ArcInfo.Fit(size, this.Padding, this.MinAngle, this.MaxAngle, this.IsDirectionReversed);
            return this.PixelPosition(value, arc);
        }

        /// <summary>
        /// Get the interpolated pixel position for the value.
        /// </summary>
        protected virtual Point PixelPosition(double value, ArcInfo arc)
        {
            var interpolation = Interpolate.Linear(this.Minimum, this.Maximum, value);
            return interpolation.Interpolate(arc);
        }

        private static object CoerceOverflow(DependencyObject d, object basevalue)
        {
            ((UIElement)d).RegisterOverflow((Thickness)basevalue);
            return basevalue;
        }
    }
}