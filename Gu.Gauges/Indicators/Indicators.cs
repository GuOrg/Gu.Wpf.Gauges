namespace Gu.Gauges
{
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;
    using System.Windows.Media;

    [ContentProperty("Items")]
    public class Indicators<T> : Control
        where T : Axis
    {
        private static readonly DependencyPropertyKey GaugePropertyKey = DependencyProperty.RegisterReadOnly(
            "Gauge",
            typeof(Gauge<T>),
            typeof(Indicators<T>),
            new PropertyMetadata(
                default(Gauge<T>), 
                OnGaugeChanged));

        public static readonly DependencyProperty GaugeProperty = GaugePropertyKey.DependencyProperty;
        private readonly ObservableCollection<Indicator<T>> items = new ObservableCollection<Indicator<T>>();

        public Gauge<T> Gauge
        {
            get { return (Gauge<T>)this.GetValue(GaugeProperty); }
            protected set { this.SetValue(GaugePropertyKey, value); }
        }

        public ObservableCollection<Indicator<T>> Items
        {
            get
            {
                return this.items;
            }
        }

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            this.Gauge = null;
            var parent = VisualTreeHelper.GetParent(this);
            while (parent != null)
            {
                var gauge = parent as Gauge<T>;
                if (gauge != null)
                {
                    this.Gauge = gauge;
                    break;
                }
                parent = VisualTreeHelper.GetParent(parent);
            }

            base.OnVisualParentChanged(oldParent);
        }

        protected virtual void OnGaugeChanged(Gauge<T> old, Gauge<T> newValue)
        {
        }

        private static void OnGaugeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Indicators<T>)d).OnGaugeChanged((Gauge<T>)e.OldValue, (Gauge<T>)e.NewValue);
        }
    }
}