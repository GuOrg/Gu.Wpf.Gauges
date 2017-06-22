namespace Gu.Gauges
{
    using System.Windows;
    using System.Windows.Markup;

    [ContentProperty("Items")]
    public class AngularIndicators : Indicators<AngularGauge>
    {
#pragma warning disable SA1202 // Elements must be ordered by access
        private static readonly DependencyPropertyKey ItemsPropertyKey = DependencyProperty.RegisterReadOnly(
nameof(Items),
            typeof(AngularIndicatorsCollection),
            typeof(AngularIndicators),
            new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty ItemsProperty = ItemsPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey GaugePropertyKey = DependencyProperty.RegisterReadOnly(
nameof(Gauge),
            typeof(AngularGauge),
            typeof(AngularIndicators),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty GaugeProperty = GaugePropertyKey.DependencyProperty;
#pragma warning restore SA1202 // Elements must be ordered by access

        static AngularIndicators()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AngularIndicators), new FrameworkPropertyMetadata(typeof(AngularIndicators)));
        }

        public AngularIndicators()
        {
            this.Items = new AngularIndicatorsCollection();
        }

        public AngularIndicatorsCollection Items
        {
            get => (AngularIndicatorsCollection)this.GetValue(ItemsProperty);
            protected set => this.SetValue(ItemsPropertyKey, value);
        }

        public AngularGauge Gauge
        {
            get => (AngularGauge)this.GetValue(GaugeProperty);
            protected set => this.SetValue(GaugePropertyKey, value);
        }

        protected override void OnGaugeChanged(AngularGauge newValue)
        {
            this.Gauge = newValue;
        }
    }
}