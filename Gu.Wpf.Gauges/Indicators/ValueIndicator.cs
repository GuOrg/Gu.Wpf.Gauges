namespace Gu.Wpf.Gauges
{
    using System.ComponentModel;
    using System.Windows;

    [DefaultEvent("ValueChanged")]
    [DefaultProperty("Value")]
    public class ValueIndicator : Indicator
    {
        /// <summary>
        /// Event correspond to Value changed event
        /// </summary>
        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent(
            nameof(ValueChanged),
            RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<double>),
            typeof(ValueIndicator));

        public static readonly DependencyProperty ValueProperty = Gauge.ValueProperty.AddOwner(
            typeof(ValueIndicator),
            new FrameworkPropertyMetadata(
                double.NaN,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange,
                OnValueChanged));

        /// <summary>
        /// Add / Remove ValueChangedEvent handler
        /// </summary>
        [Category("Behavior")]
        public event RoutedPropertyChangedEventHandler<double> ValueChanged
        {
            add => this.AddHandler(ValueChangedEvent, value);
            remove => this.RemoveHandler(ValueChangedEvent, value);
        }

        public double Value
        {
            get => (double)this.GetValue(ValueProperty);
            set => this.SetValue(ValueProperty, value);
        }

        /// <summary>
        ///     This method is invoked when the Value property changes.
        /// </summary>
        /// <param name="oldValue">The old value of the Value property.</param>
        /// <param name="newValue">The new value of the Value property.</param>
        protected virtual void OnValueChanged(double oldValue, double newValue)
        {
            this.RaiseEvent(new RoutedPropertyChangedEventArgs<double>(oldValue, newValue) { RoutedEvent = ValueChangedEvent });
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ValueIndicator)d).OnValueChanged((double)e.OldValue, (double)e.NewValue);
        }
    }
}