namespace Gu.Gauges
{
    using System.Windows;

    public class LinearAxis : Axis
    {
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
            get { return (double)this.GetValue(PenWidthProperty); }
            set { this.SetValue(PenWidthProperty, value); }
        }
    }
}
