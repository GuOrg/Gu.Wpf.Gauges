namespace Gauges
{
    using System.ComponentModel;
    using System.Security.Cryptography.X509Certificates;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class BlockBar : FrameworkElement
    {
        public static readonly DependencyProperty TickFrequencyProperty = TickBar.TickFrequencyProperty.AddOwner(
            typeof(BlockBar),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty FillProperty = Shape.FillProperty.AddOwner(
            typeof(BlockBar),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty StrokeProperty = Shape.StrokeProperty.AddOwner(
            typeof (BlockBar),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty StrokeThicknessProperty = Shape.StrokeThicknessProperty.AddOwner(
            typeof (BlockBar),
            new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty GapProperty = DependencyProperty.Register(
            "Gap",
            typeof(double),
            typeof(BlockBar),
            new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.AffectsRender));

        [Bindable(true)]
        public double TickFrequency
        {
            get
            {
                return (double)this.GetValue(TickFrequencyProperty);
            }
            set
            {
                this.SetValue(TickFrequencyProperty, value);
            }
        }

        [Bindable(true)]
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

        [Bindable(true)]
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

        [Bindable(true)]
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
            var ticks = (100.0 / TickFrequency);
            var totalGap = ActualWidth * (ticks - 1) * Gap / 100.0;
            var width = (ActualWidth - totalGap) / ticks;
            double x = 0;
            while (x < ActualWidth)
            {
                dc.DrawRectangle(Fill, new Pen(Stroke, StrokeThickness), new Rect(x, 0, width, ActualHeight));
                x += width + Gap;
            }
        }
    }
}