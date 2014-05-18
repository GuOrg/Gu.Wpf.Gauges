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
            get { return (string)this.GetValue(TextProperty); }
            set { this.SetValue(TextProperty, value); }
        }

        public double Value
        {
            get { return (double)this.GetValue(ValueProperty); }
            set { this.SetValue(ValueProperty, value); }
        }

        public string TextStringformat
        {
            get { return (string)this.GetValue(TextStringformatProperty); }
            set { this.SetValue(TextStringformatProperty, value); }
        }
    }
}