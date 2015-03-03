namespace Gu.Gauges
{
    using System.Windows;

    public class AngularGauge : Gauge
    {
        public static readonly DependencyProperty AxisProperty = DependencyProperty.Register(
            "Axis",
            typeof(AngularAxis),
            typeof(AngularGauge),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange,
                OnAxisChanged));

        public static readonly DependencyProperty IndicatorsProperty = DependencyProperty.Register(
            "Indicators",
            typeof(AngularIndicators),
            typeof(AngularGauge),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange,
                OnIndicatorsChanged));

        static AngularGauge()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AngularGauge), new FrameworkPropertyMetadata(typeof(AngularGauge)));
        }

        public AngularAxis Axis
        {
            get { return (AngularAxis)this.GetValue(AxisProperty); }
            set { this.SetValue(AxisProperty, value); }
        }

        public AngularIndicators Indicators
        {
            get { return (AngularIndicators)this.GetValue(IndicatorsProperty); }
            set { this.SetValue(IndicatorsProperty, value); }
        }

        protected virtual void OnAxisChanged(AngularAxis old, AngularAxis newAxis)
        {
        }

        protected virtual void OnIndicatorsChanged(AngularIndicators old, AngularIndicators newAxis)
        {
        }

        private static void OnAxisChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((AngularGauge)d).OnAxisChanged((AngularAxis)e.OldValue, (AngularAxis)e.NewValue);
        }

        private static void OnIndicatorsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((AngularGauge)d).OnIndicatorsChanged((AngularIndicators)e.OldValue, (AngularIndicators)e.NewValue);
        }
    }
}
