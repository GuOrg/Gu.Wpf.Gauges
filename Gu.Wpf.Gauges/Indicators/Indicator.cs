namespace Gu.Wpf.Gauges
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;

    [DefaultEvent("ValueChanged")]
    [DefaultProperty("Value")]
    public class Indicator : ContentControl
    {
        /// <summary>
        /// Event correspond to Value changed event
        /// </summary>
        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent(
            nameof(ValueChanged),
            RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<double>),
            typeof(Indicator));

        public static readonly DependencyProperty ValueProperty = Gauge.ValueProperty.AddOwner(
            typeof(Indicator),
            new FrameworkPropertyMetadata(
                double.NaN,
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.Inherits,
                OnValueChanged));

        /// <summary>
        /// Identifies the <see cref="P:Bar.Minimum" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:Bar.Minimum" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty MinimumProperty = Gauge.MinimumProperty.AddOwner(
            typeof(Indicator),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:Bar.Maximum" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:Bar.Maximum" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty MaximumProperty = Gauge.MaximumProperty.AddOwner(
            typeof(Indicator),
            new FrameworkPropertyMetadata(
                1.0,
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:Bar.IsDirectionReversed" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsDirectionReversedProperty = Gauge.IsDirectionReversedProperty.AddOwner(
            typeof(Indicator),
            new FrameworkPropertyMetadata(
                false,
                FrameworkPropertyMetadataOptions.Inherits));

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
        /// Gets or sets the <see cref="P:Bar.Minimum" />
        /// The default is 0
        /// </summary>
        public double Minimum
        {
            get => (double)this.GetValue(MinimumProperty);
            set => this.SetValue(MinimumProperty, value);
        }

        /// <summary>
        /// Gets or sets the highest possible <see cref="P:Bar.Maximum" /> of the range element.
        /// </summary>
        /// <returns>
        /// The highest possible <see cref="P:Bar.Maximum" /> of the range element. The default is 1.
        /// </returns>
        public double Maximum
        {
            get => (double)this.GetValue(MaximumProperty);
            set => this.SetValue(MaximumProperty, value);
        }

        /// <summary>
        /// Gets or sets the direction of increasing value.
        /// </summary>
        /// <returns>
        /// true if the direction of increasing value is to the left for a horizontal tickbar or down for a vertical tickbar; otherwise, false.
        /// The default is false.
        /// </returns>
        public bool IsDirectionReversed
        {
            get => (bool)this.GetValue(IsDirectionReversedProperty);
            set => this.SetValue(IsDirectionReversedProperty, value);
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
            ((Indicator)d).OnValueChanged((double)e.OldValue, (double)e.NewValue);
        }
    }
}