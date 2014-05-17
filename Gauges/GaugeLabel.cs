namespace Gauges
{
    using System.Windows;

    public class GaugeLabel : DependencyObject
    {
        public string Text { get; set; }
        public double Value { get; set; }
    }
}
