namespace Gu.Gauges
{
    using System.Windows;

    public class LinearIndicators : Indicators<LinearAxis>
    {
        static LinearIndicators()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinearIndicators), new FrameworkPropertyMetadata(typeof(LinearIndicators)));
        }
    }
}
