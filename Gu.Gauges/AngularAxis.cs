namespace Gu.Gauges
{
    using System.Windows;

    public class AngularAxis : Axis
    {
        static AngularAxis()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AngularAxis), new FrameworkPropertyMetadata(typeof(AngularAxis)));
        }
    }
}