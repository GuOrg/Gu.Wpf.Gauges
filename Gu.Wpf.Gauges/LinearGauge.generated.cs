namespace Gu.Wpf.Gauges
{
    using System.Windows;
    using System.Windows.Controls.Primitives;

    public partial class LinearGauge
    {
        public static readonly DependencyProperty PlacementProperty = DependencyProperty.RegisterAttached(
            nameof(Placement),
            typeof(TickBarPlacement),
            typeof(LinearGauge),
            new FrameworkPropertyMetadata(
                TickBarPlacement.Bottom,
                FrameworkPropertyMetadataOptions.Inherits));

        public TickBarPlacement Placement
        {
            get => (TickBarPlacement)this.GetValue(PlacementProperty);
            set => this.SetValue(PlacementProperty, value);
        }

        public static void SetPlacement(DependencyObject element, TickBarPlacement value)
        {
            element.SetValue(PlacementProperty, value);
        }

        public static TickBarPlacement GetPlacement(DependencyObject element)
        {
            return (TickBarPlacement)element.GetValue(PlacementProperty);
        }
    }
}
