namespace Gu.Wpf.Gauges
{
    using System.Windows;
    using System.Windows.Media;

    public abstract class AngularGeometryBar : GeometryBar
    {
#pragma warning disable SA1202 // Elements must be ordered by access

        public static readonly DependencyProperty ThicknessProperty = DependencyProperty.RegisterAttached(
            nameof(Thickness),
            typeof(double),
            typeof(AngularGeometryBar),
            new FrameworkPropertyMetadata(
                10.0d,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty StartProperty = AngularGauge.StartProperty.AddOwner(
            typeof(AngularGeometryBar),
            new FrameworkPropertyMetadata(
                Defaults.StartAngle,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty EndProperty = AngularGauge.EndProperty.AddOwner(
            typeof(AngularGeometryBar),
            new FrameworkPropertyMetadata(
                Defaults.EndAngle,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

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
        public double Start
        {
            get => (double)this.GetValue(StartProperty);
            set => this.SetValue(StartProperty, value);
        }

        /// <summary>
        /// Gets or sets the end angle of the arc.
        /// Degrees clockwise from the y axis.
        /// The default is 140
        /// </summary>
        public double End
        {
            get => (double)this.GetValue(EndProperty);
            set => this.SetValue(EndProperty, value);
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

        protected virtual Brush EffectiveFill => DoubleUtil.LessThanOrClose(this.Thickness, this.GetStrokeThickness())
            ? null
            : this.Fill;

        private static object CoerceOverflow(DependencyObject d, object basevalue)
        {
            ((UIElement)d).RegisterOverflow((Thickness)basevalue);
            return basevalue;
        }
    }
}