using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Gu.Gauges
{
    public class BlockBar : FrameworkElement
    {
        public static readonly DependencyProperty DivisionsProperty = DependencyProperty.Register(
            "Divisions",
            typeof(double),
            typeof(BlockBar),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty FillProperty = Shape.FillProperty.AddOwner(
            typeof(BlockBar),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty StrokeProperty = Shape.StrokeProperty.AddOwner(
            typeof(BlockBar),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty StrokeThicknessProperty = Shape.StrokeThicknessProperty.AddOwner(
            typeof(BlockBar),
            new FrameworkPropertyMetadata(
                default(double), 
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty GapProperty = DependencyProperty.Register(
            "Gap",
            typeof(double),
            typeof(BlockBar),
            new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.AffectsRender));

        public double Divisions
        {
            get { return (double)this.GetValue(DivisionsProperty); }
            set { this.SetValue(DivisionsProperty, value); }
        }

        public Brush Fill
        {
            get { return (Brush)this.GetValue(FillProperty); }
            set { this.SetValue(FillProperty, value); }
        }

        public Brush Stroke
        {
            get { return (Brush)this.GetValue(StrokeProperty); }
            set { this.SetValue(StrokeProperty, value); }
        }

        public double StrokeThickness
        {
            get { return (double)this.GetValue(StrokeThicknessProperty); }
            set { this.SetValue(StrokeThicknessProperty, value); }
        }

        public double Gap
        {
            get { return (double)this.GetValue(GapProperty); }
            set { this.SetValue(GapProperty, value); }
        }

        protected override void OnRender(DrawingContext dc)
        {
            double totalGap = this.ActualWidth * (this.Divisions - 1) * this.Gap / 100.0;
            double width = (this.ActualWidth - totalGap) / this.Divisions;
            double x = 0;
            while (x < this.ActualWidth)
            {
                dc.DrawRectangle(
                    this.Fill,
                    new Pen(this.Stroke, this.StrokeThickness),
                    new Rect(x, 0, width, this.ActualHeight));
                x += width + this.Gap;
            }
        }
    }
}