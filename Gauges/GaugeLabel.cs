namespace Gauges
{
    using System.Windows;

    public class GaugeLabel : DependencyObject
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text",
            typeof(string),
            typeof(GaugeLabel),
            new PropertyMetadata(default(string)));

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value",
            typeof(double),
            typeof(GaugeLabel),
            new PropertyMetadata(default(double)));

        public static readonly DependencyProperty TextStringformatProperty = DependencyProperty.Register(
            "TextStringformat", typeof(string), typeof(GaugeLabel), new PropertyMetadata(default(string)));

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }

        public double Value
        {
            get
            {
                return (double)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }

        public string TextStringformat
        {
            get
            {
                return (string)GetValue(TextStringformatProperty);
            }
            set
            {
                SetValue(TextStringformatProperty, value);
            }
        }
    }
}
