namespace Gu.Wpf.Gauges
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    public abstract class Gauge : Control
    {
        /// <summary>
        /// Identifies the <see cref="P:Gauge.Value" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:Gauge.Value" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty ValueProperty = RangeBase.ValueProperty.AddOwner(
            typeof(Gauge),
            new PropertyMetadata(0.0));

        /// <summary>
        /// Gets or sets the current magnitude of the range control.
        /// </summary>
        /// <returns>
        /// The current magnitude of the range control. The default is 0.
        /// </returns>
        public double Value
        {
            get => (double)this.GetValue(ValueProperty);
            set => this.SetValue(ValueProperty, value);
        }
    }
}