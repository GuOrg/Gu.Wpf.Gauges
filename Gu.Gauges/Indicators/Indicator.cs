using System.Linq;
using Gu.Gauges.Helpers;

namespace Gu.Gauges
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class Indicator<T> : ContentControl where T : Axis
    {
        private static readonly DependencyPropertyKey GaugePropertyKey = DependencyProperty.RegisterReadOnly(
            "Gauge",
            typeof(Gauge<T>),
            typeof(Indicator<T>),
            new PropertyMetadata(default(Gauge<T>), OnGaugeChanged));

        public static readonly DependencyProperty GaugeProperty = GaugePropertyKey.DependencyProperty;

        public Gauge<T> Gauge
        {
            get { return (Gauge<T>)this.GetValue(GaugeProperty); }
            protected set { this.SetValue(GaugePropertyKey, value); }
        }

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            this.Gauge = this.VisualAncestors().OfType<Gauge<T>>().FirstOrDefault();
            base.OnVisualParentChanged(oldParent);
        }

        protected virtual void OnGaugeChanged(Gauge<T> old, Gauge<T> newValue)
        {
        }

        private static void OnGaugeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Indicator<T>)d).OnGaugeChanged((Gauge<T>)e.OldValue, (Gauge<T>)e.NewValue);
        }
    }
}
