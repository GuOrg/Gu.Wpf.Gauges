namespace Gu.Gauges
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    using Gu.Gauges.Helpers;

    public class AngularIndicator : ContentControl
    {
#pragma warning disable SA1202 // Elements must be ordered by access

        private static readonly DependencyPropertyKey GaugePropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(Gauge),
            typeof(AngularGauge),
            typeof(AngularIndicator),
            new PropertyMetadata(null, OnGaugeChanged));

        public static readonly DependencyProperty GaugeProperty = GaugePropertyKey.DependencyProperty;
#pragma warning restore SA1202 // Elements must be ordered by access

        public AngularGauge Gauge
        {
            get => (AngularGauge)this.GetValue(GaugeProperty);
            protected set => this.SetValue(GaugePropertyKey, value);
        }

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            this.Gauge = this.VisualAncestors().OfType<AngularGauge>().FirstOrDefault();
            base.OnVisualParentChanged(oldParent);
        }

        protected virtual void OnGaugeChanged(AngularGauge old, AngularGauge newValue)
        {
        }

        private static void OnGaugeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((AngularIndicator)d).OnGaugeChanged((AngularGauge)e.OldValue, (AngularGauge)e.NewValue);
        }
    }
}