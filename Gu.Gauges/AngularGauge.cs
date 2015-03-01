namespace Gu.Gauges
{
    using System.Windows;

    public class AngularGauge : Gauge<AngularAxis>
    {
        static AngularGauge()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AngularGauge), new FrameworkPropertyMetadata(typeof(AngularGauge)));
        }
    }
}
