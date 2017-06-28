namespace Gu.Wpf.Gauges
{
    using System.Windows;

    public class AngularIndicator : Indicator
    {
        static AngularIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AngularIndicator), new FrameworkPropertyMetadata(typeof(AngularIndicator)));
        }
    }
}