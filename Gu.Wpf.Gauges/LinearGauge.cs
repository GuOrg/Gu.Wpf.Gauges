namespace Gu.Wpf.Gauges
{
    using System.Windows;
    using System.Windows.Controls.Primitives;

    public class LinearGauge : Gauge
    {
        public static readonly DependencyProperty PlacementProperty = DependencyProperty.RegisterAttached(
            "Placement",
            typeof(TickBarPlacement),
            typeof(LinearGauge),
            new FrameworkPropertyMetadata(
                TickBarPlacement.Bottom,
                FrameworkPropertyMetadataOptions.Inherits));

        static LinearGauge()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinearGauge), new FrameworkPropertyMetadata(typeof(LinearGauge)));
        }

        public static void SetPlacement(DependencyObject element, TickBarPlacement placement)
        {
            element.SetValue(PlacementProperty, placement);
        }

        public static TickBarPlacement GetPlacement(DependencyObject element)
        {
            return (TickBarPlacement)element.GetValue(PlacementProperty);
        }
    }
}