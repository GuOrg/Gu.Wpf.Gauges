namespace Gu.Wpf.Gauges
{
    using System.Windows;

    public partial class LinearGauge : Gauge
    {
        static LinearGauge()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinearGauge), new FrameworkPropertyMetadata(typeof(LinearGauge)));
        }
    }
}