namespace Gu.Wpf.Gauges
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;

    public partial class Gauge : ContentControl
    {
        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent(
            nameof(ValueChanged),
            RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<double>),
            typeof(Gauge));

        /// <summary>
        /// Add / Remove ValueChangedEvent handler
        /// </summary>
        [Category("Behavior")]
        public event RoutedPropertyChangedEventHandler<double> ValueChanged
        {
            add => this.AddHandler(ValueChangedEvent, value);
            remove => this.RemoveHandler(ValueChangedEvent, value);
        }

        /// <summary>
        ///     This method is invoked when the <see cref="Value"/> property changes.
        /// </summary>
        /// <param name="oldValue">The old value of the Value property.</param>
        /// <param name="newValue">The new value of the Value property.</param>
        protected virtual void OnValueChanged(double oldValue, double newValue)
        {
            this.RaiseEvent(new RoutedPropertyChangedEventArgs<double>(oldValue, newValue) { RoutedEvent = ValueChangedEvent });
        }

        /// <summary>
        ///     This method is invoked when the <see cref="Minimum"/> property changes.
        /// </summary>
        /// <param name="oldMinimum">The old value of the Minimum property.</param>
        /// <param name="newMinimum">The new value of the Minimum property.</param>
        protected virtual void OnMinimumChanged(double oldMinimum, double newMinimum)
        {
        }

        /// <summary>
        ///     This method is invoked when the <see cref="Maximum"/> property changes.
        /// </summary>
        /// <param name="oldMaximum">The old value of the Maximum property.</param>
        /// <param name="newMaximum">The new value of the Maximum property.</param>
        protected virtual void OnMaximumChanged(double oldMaximum, double newMaximum)
        {
        }

        /// <summary>
        ///     This method is invoked when the <see cref="IsDirectionReversed"/> property changes.
        /// </summary>
        /// <param name="oldValue">The old value of the Maximum property.</param>
        /// <param name="newValue">The new value of the Maximum property.</param>
        protected virtual void OnIsDirectionReversedChanged(bool oldValue, bool newValue)
        {
        }
    }
}