namespace Gu.Gauges
{
    using System.Windows;

    public class LinearValue : LinearIndicator
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
nameof(Value),
            typeof(double),
            typeof(LinearValue),
            new PropertyMetadata(double.NaN, OnValueChanged));

        static LinearValue()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinearValue), new FrameworkPropertyMetadata(typeof(LinearValue)));
        }

        public double Value
        {
            get => (double)this.GetValue(ValueProperty);
            set => this.SetValue(ValueProperty, value);
        }

        protected virtual void OnValueChanged(double newValue)
        {
            LinearPanel.SetAtValue(this, newValue);
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LinearValue)d).OnValueChanged((double)e.NewValue);
        }
    }
}