using System.Windows;

namespace Gu.Gauges
{
    public class AngularRange : RangeIndicator<AngularAxis>
    {
        static AngularRange()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AngularRange),new FrameworkPropertyMetadata(typeof(AngularRange)));
        }
    }
}