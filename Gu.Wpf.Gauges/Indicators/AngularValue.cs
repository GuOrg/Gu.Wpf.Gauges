namespace Gu.Wpf.Gauges
{
    using System.Windows;

    public class AngularValue : AngularIndicator
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
nameof(Value),
            typeof(double),
            typeof(AngularValue),
            new PropertyMetadata(double.NaN, OnValueChanged));

        static AngularValue()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AngularValue), new FrameworkPropertyMetadata(typeof(AngularValue)));
        }

        public double Value
        {
            get => (double)this.GetValue(ValueProperty);
            set => this.SetValue(ValueProperty, value);
        }

        protected virtual void OnValueChanged(double newValue)
        {
            AngularPanel.SetAtValue(this, newValue);
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((AngularValue)d).OnValueChanged((double)e.NewValue);
        }
    }
}