namespace Gu.Gauges
{
    using System.Windows;

    public class LinearGauge : Gauge<LinearAxis>
    {
        public static readonly DependencyProperty AxisProperty = DependencyProperty.Register(
            "Axis",
            typeof(LinearAxis),
            typeof(LinearGauge),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

        public static readonly DependencyProperty IndicatorsProperty = DependencyProperty.Register(
            "Indicators",
            typeof(LinearIndicators),
            typeof(LinearGauge),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

        static LinearGauge()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinearGauge), new FrameworkPropertyMetadata(typeof(LinearGauge)));
        }

        public LinearAxis Axis
        {
            get { return (LinearAxis)this.GetValue(AxisProperty); }
            set { this.SetValue(AxisProperty, value); }
        }

        public LinearIndicators Indicators
        {
            get { return (LinearIndicators)this.GetValue(IndicatorsProperty); }
            set { this.SetValue(IndicatorsProperty, value); }
        }
    }
}