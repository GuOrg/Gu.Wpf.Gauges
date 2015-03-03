namespace Gu.Gauges
{
    using System.Windows;

    public class LinearGauge : Gauge
    {
        public static readonly DependencyProperty AxisProperty = DependencyProperty.Register(
            "Axis",
            typeof(LinearAxis),
            typeof(LinearGauge),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange,
                OnAxisChanged));

        public static readonly DependencyProperty IndicatorsProperty = DependencyProperty.Register(
            "Indicators",
            typeof(LinearIndicators),
            typeof(LinearGauge),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange,
                OnIndicatorsChanged));

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

        protected virtual void OnAxisChanged(LinearAxis old, LinearAxis newAxis)
        {
        }

        protected virtual void OnIndicatorsChanged(LinearIndicators old, LinearIndicators newAxis)
        {
        }

        private static void OnAxisChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LinearGauge)d).OnAxisChanged((LinearAxis)e.OldValue, (LinearAxis)e.NewValue);
        }

        private static void OnIndicatorsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LinearGauge)d).OnIndicatorsChanged((LinearIndicators)e.OldValue, (LinearIndicators)e.NewValue);
        }
    }
}