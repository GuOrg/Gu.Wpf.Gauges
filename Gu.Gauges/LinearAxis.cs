namespace Gu.Gauges
{
    using System.Windows;

    public class LinearAxis : Axis
    {
        static LinearAxis()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinearAxis), new FrameworkPropertyMetadata(typeof(LinearAxis)));
        }
    }
}
