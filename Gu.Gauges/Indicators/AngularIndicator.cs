namespace Gu.Gauges
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    using Gu.Gauges.Helpers;

    public class AngularIndicator : ContentControl
    {
        private static readonly DependencyPropertyKey GaugePropertyKey = DependencyProperty.RegisterReadOnly(
            "Gauge",
            typeof(AngularGauge),
            typeof(AngularIndicator),
            new PropertyMetadata(null, OnGaugeChanged));

        public static readonly DependencyProperty GaugeProperty = GaugePropertyKey.DependencyProperty;

        public AngularGauge Gauge
        {
            get { return (AngularGauge)this.GetValue(GaugeProperty); }
            protected set { this.SetValue(GaugePropertyKey, value); }
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