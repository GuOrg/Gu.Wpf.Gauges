namespace Gu.Wpf.Gauges
{
    using System.Windows;
    using System.Windows.Controls.Primitives;

    public class LinearAxis : Axis
    {
        public static readonly DependencyProperty PlacementProperty = TickBar.PlacementProperty.AddOwner(
            typeof(LinearAxis),
            new FrameworkPropertyMetadata(
                default(TickBarPlacement),
                FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty PenWidthProperty = LinearTickBar.PenWidthProperty.AddOwner(
            typeof(LinearAxis),
            new FrameworkPropertyMetadata(
                1.0,
                FrameworkPropertyMetadataOptions.AffectsRender));

        static LinearAxis()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinearAxis), new FrameworkPropertyMetadata(typeof(LinearAxis)));
        }

        public double PenWidth
        {
            get => (double)this.GetValue(PenWidthProperty);
            set => this.SetValue(PenWidthProperty, value);
        }

        /// <summary>
        /// Gets or sets the textplacement
        /// </summary>
        public TickBarPlacement Placement
        {
            get => (TickBarPlacement)this.GetValue(PlacementProperty);
            set => this.SetValue(PlacementProperty, value);
        }
    }
}
