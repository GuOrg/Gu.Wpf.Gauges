using System.Windows;

namespace Gu.Gauges
{
    public class CenteredIndicator : Indicator<LinearAxis>
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof(double),
            typeof(CenteredIndicator),
            new PropertyMetadata(double.NaN, OnValueChanged));

        public double Value
        {
            get { return (double)this.GetValue(ValueProperty); }
            set { this.SetValue(ValueProperty, value); }
        }

        protected virtual void OnValueChanged(double newValue)
        {
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CenteredIndicator)d).OnValueChanged((double)e.NewValue);
        }
    }

    public class LinearValue : CenteredIndicator
    {
        protected override void OnValueChanged(double newValue)
        {
            LinearPanel.SetAtValue(this, newValue);
        }
    }
}
