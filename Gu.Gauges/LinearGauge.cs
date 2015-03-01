namespace Gu.Gauges
{
    using System.Windows;

    public class LinearGauge : Gauge<LinearAxis>
    {
        static LinearGauge()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinearGauge), new FrameworkPropertyMetadata(typeof(LinearGauge)));
        }

        public LinearGauge()
        {
            this.Indicators = new LinearIndicators();
        }
    }
}