namespace Gu.Gauges
{
    using System.Windows;

    public class LinearValue : LinearIndicator
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof(double),
            typeof(LinearValue),
            new PropertyMetadata(double.NaN, OnValueChanged));

        public double Value
        {
            get { return (double)this.GetValue(ValueProperty); }
            set { this.SetValue(ValueProperty, value); }
        }

        static LinearValue()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinearValue), new FrameworkPropertyMetadata(typeof(LinearValue)));
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