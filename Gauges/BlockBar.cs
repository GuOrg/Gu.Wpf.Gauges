namespace Gauges
{
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class BlockBar : FrameworkElement
    {
        public static readonly DependencyProperty DivisionsProperty = DependencyProperty.Register(
            "Divisions",
            typeof(double),
            typeof(BlockBar),
           new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty FillProperty = Shape.FillProperty.AddOwner(
            typeof(BlockBar),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty StrokeProperty = Shape.StrokeProperty.AddOwner(
            typeof(BlockBar),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty StrokeThicknessProperty = Shape.StrokeThicknessProperty.AddOwner(
            typeof(BlockBar),
            new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty GapProperty = DependencyProperty.Register(
            "Gap",
            typeof(double),
            typeof(BlockBar),
            new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.AffectsRender));

        public double Divisions
        {
            get
            {
                return (double)GetValue(DivisionsProperty);
            }
            set
            {
                SetValue(DivisionsProperty, value);
            }
        }

        public Brush Fill
        {
            get
            {
                return (Brush)this.GetValue(FillProperty);
            }
            set
            {
                this.SetValue(FillProperty, value);
            }
        }

        public Brush Stroke
        {
            get
            {
                return (Brush)this.GetValue(StrokeProperty);
            }
            set
            {
                this.SetValue(StrokeProperty, value);
            }
        }

        public double StrokeThickness
        {
            get
            {
                return (double)this.GetValue(StrokeThicknessProperty);
            }
            set
            {
                this.SetValue(StrokeThicknessProperty, value);
            }
        }

        public double Gap
        {
            get
            {
                return (double)GetValue(GapProperty);
            }
            set
            {
                SetValue(GapProperty, value);
            }
        }

        protected override void OnRender(System.Windows.Media.DrawingContext dc)
        {
            var totalGap = ActualWidth * (Divisions - 1) * Gap / 100.0;
            var width = (ActualWidth - totalGap) / Divisions;
            double x = 0;
            var linearGradientBrush = Fill as LinearGradientBrush;

            while (x < ActualWidth)
            {

                dc.DrawRectangle(Fill, new Pen(Stroke, StrokeThickness), new Rect(x, 0, width, ActualHeight));
                x += width + Gap;
            }
        }
    }
}