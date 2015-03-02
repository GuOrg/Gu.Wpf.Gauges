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
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

        public static readonly DependencyProperty IndicatorsProperty = DependencyProperty.Register(
            "Indicators",
            typeof(AngularIndicators),
            typeof(AngularGauge),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

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
    }
}
