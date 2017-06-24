namespace Gu.Wpf.Gauges
{
    using System.Windows;

    public class AngularGauge : Gauge
    {
        static AngularGauge()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AngularGauge), new FrameworkPropertyMetadata(typeof(AngularGauge)));
        }
    }
}
