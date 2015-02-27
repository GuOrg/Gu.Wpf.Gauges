namespace Gu.Gauges
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    public class AngularTickBar : AngularBar
    {
        /// <summary>
        /// Identifies the <see cref="P:AngularTickBar.PenWidth" /> dependency property. 
        /// </summary>
        public static readonly DependencyProperty PenWidthProperty = DependencyProperty.Register(
            "PenWidth",
            typeof(double),
            typeof(AngularTickBar),
            new FrameworkPropertyMetadata(
                1.0,
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Identifies the <see cref="P:AngularTickBar.Fill" /> dependency property. This property is read-only.
        /// </summary>
        public static readonly DependencyProperty FillProperty = TickBar.FillProperty.AddOwner(
            typeof(AngularTickBar),
            new FrameworkPropertyMetadata(
                default(Brush),
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty TickLengthProperty = DependencyProperty.Register(
            "TickLength",
            typeof(double),
            typeof(AngularTickBar),
            new FrameworkPropertyMetadata(
                10.0,
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Gets or sets the <see cref="P:AngularTickBar.PenWidth" />
        /// The default is 1
        /// </summary>
        public double PenWidth
        {
            get { return (double)this.GetValue(PenWidthProperty); }
            set { this.SetValue(PenWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="T:System.Windows.Media.Brush" /> that is used to draw the tick marks.  
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Media.Brush" /> to use to draw tick marks. The default value is null.
        /// </returns>
        public Brush Fill
        {
            get { return (Brush)this.GetValue(FillProperty); }
            set { this.SetValue(FillProperty, value); }
        }

        /// <summary>
        /// Gets or sets the length of the ticks. 
        /// The default value is 10.
        /// </summary>
        public double TickLength
        {
            get { return (double)this.GetValue(TickLengthProperty); }
            set { this.SetValue(TickLengthProperty, value); }
        }

        protected override void OnRender(DrawingContext dc)
        {
            var pen = new Pen(this.Fill, this.PenWidth);
            pen.Freeze();
            var midPoint = new Point(this.ActualWidth / 2, this.ActualHeight / 2);
            var pi = new Point(this.ActualWidth - this.ReservedSpace - this.TickLength, midPoint.Y);
            var po = new Point(this.ActualWidth - this.ReservedSpace, midPoint.Y);
            var tickLine = new Line(pi, po);
            var arc = new Arc(midPoint, this.MinAngle, this.MaxAngle, this.ActualWidth - this.ReservedSpace, this.IsDirectionReversed);
            var transform = new RotateTransform(0, midPoint.X, midPoint.Y);
            foreach (var tick in this.AllTicks)
            {
                var angle = TickHelper.ToAngle(tick, this.Minimum, this.Maximum, arc);
                transform.Angle = angle;
                var l = transform.Transform(tickLine);
                dc.DrawLine(pen, l);
            }
            this.Diameter = 2 * (midPoint - po).Length;
        }
    }
}