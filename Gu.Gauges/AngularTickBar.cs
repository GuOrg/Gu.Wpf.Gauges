namespace Gu.Gauges
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class AngularTickBar : AngularBar
    {
        public static readonly DependencyProperty StrokeProperty = Shape.StrokeProperty.AddOwner(
            typeof(AngularTickBar),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty StrokeThicknessProperty = Shape.StrokeThicknessProperty.AddOwner(
            typeof(AngularTickBar),
            new FrameworkPropertyMetadata(
                default(double),
                FrameworkPropertyMetadataOptions.AffectsRender));

        static AngularTickBar()
        {
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

        protected override void OnRender(DrawingContext dc)
        {
            var pen = new Pen(this.Stroke, this.StrokeThickness);
            var midPoint = new Point(this.ActualWidth / 2, this.ActualHeight / 2);
            var pi = new Point(this.ActualWidth - this.ReservedSpace, midPoint.Y);
            var po = new Point(this.ActualWidth, midPoint.Y);
            var ticks = TickHelper.CreateTicks(this.Minimum, this.Maximum, this.TickFrequency).Concat(this.Ticks ?? Enumerable.Empty<double>());
            foreach (var tick in ticks)
            {
                if (tick < this.Minimum || tick > this.Maximum)
                {
                    continue;
                }
                var angle = TickHelper.ToAngle(tick, this.Minimum, this.Maximum, this.MinAngle, this.MaxAngle);
                var rotateTransform = new RotateTransform(angle, midPoint.X, midPoint.Y);
                var p1 = rotateTransform.Transform(pi);
                var p2 = rotateTransform.Transform(po);
                dc.DrawLine(pen, p1, p2);
            }
        }
    }
}