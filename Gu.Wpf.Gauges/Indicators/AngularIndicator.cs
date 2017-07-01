namespace Gu.Wpf.Gauges
{
    using System.Windows;

    public class AngularIndicator : ValueIndicator
    {
        static AngularIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AngularIndicator), new FrameworkPropertyMetadata(typeof(AngularIndicator)));
        }
    }
}