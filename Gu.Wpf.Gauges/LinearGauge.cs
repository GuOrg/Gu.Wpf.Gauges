namespace Gu.Wpf.Gauges
{
    using System.Windows;

    public class LinearGauge : Gauge
    {
        public static readonly DependencyProperty AxisProperty = DependencyProperty.Register(
            nameof(Axis),
            typeof(LinearAxis),
            typeof(LinearGauge),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange,
                OnAxisChanged));

        public static readonly DependencyProperty IndicatorsProperty = DependencyProperty.Register(
            nameof(Indicators),
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
            get => (LinearAxis)this.GetValue(AxisProperty);
            set => this.SetValue(AxisProperty, value);
        }

        public LinearIndicators Indicators
        {
            get => (LinearIndicators)this.GetValue(IndicatorsProperty);
            set => this.SetValue(IndicatorsProperty, value);
        }

        protected virtual void OnAxisChanged(LinearAxis oldAxis, LinearAxis newAxis)
        {
        }

        protected virtual void OnIndicatorsChanged(LinearIndicators oldIndicators, LinearIndicators newIndicators)
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