namespace Gu.Wpf.Gauges
{
    using System.Windows;
    using System.Windows.Controls.Primitives;

    public class LinearAxis : Axis
    {
        public static readonly DependencyProperty PlacementProperty = LinearGauge.PlacementProperty.AddOwner(
            typeof(LinearAxis),
            new FrameworkPropertyMetadata(
                default(TickBarPlacement),
                FrameworkPropertyMetadataOptions.Inherits));

        static LinearAxis()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinearAxis), new FrameworkPropertyMetadata(typeof(LinearAxis)));
        }

        /// <summary>
        /// Gets or sets the placement
        /// </summary>
        public TickBarPlacement Placement
        {
            get => (TickBarPlacement)this.GetValue(PlacementProperty);
            set => this.SetValue(PlacementProperty, value);
        }
    }
}
