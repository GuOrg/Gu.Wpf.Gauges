namespace Gu.Wpf.Gauges
{
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class AngularTickBar : AngularBar
    {
        /// <summary>
        /// Identifies the <see cref="P:AngularTickBar.PenWidth" />�dependency property.
        /// </summary>
        public static readonly DependencyProperty PenWidthProperty = DependencyProperty.Register(
            nameof(PenWidth),
            typeof(double),
            typeof(AngularTickBar),
            new FrameworkPropertyMetadata(
                1.0,
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Identifies the <see cref="P:AngularTickBar.Fill" />�dependency property. This property is read-only.
        /// </summary>
        public static readonly DependencyProperty FillProperty = Shape.FillProperty.AddOwner(
            typeof(AngularTickBar),
            new FrameworkPropertyMetadata(
                default(Brush),
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty ThicknessProperty = AngularGauge.ThicknessProperty.AddOwner(
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
            get => (double)this.GetValue(PenWidthProperty);
            set => this.SetValue(PenWidthProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="T:System.Windows.Media.Brush" /> that is used to draw the tick marks.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Media.Brush" /> to use to draw tick marks. The default value is null.
        /// </returns>
        public Brush Fill
        {
            get => (Brush)this.GetValue(FillProperty);
            set => this.SetValue(FillProperty, value);
        }

        /// <summary>
        /// Gets or sets the length of the ticks.
        /// The default value is 10.
        /// </summary>
        public double Thickness
        {
            get => (double)this.GetValue(ThicknessProperty);
            set => this.SetValue(ThicknessProperty, value);
        }

        protected override void OnRender(DrawingContext dc)
        {
            var pen = new Pen(this.Fill, this.PenWidth);
            pen.Freeze();
            var arc = ArcInfo.Fill(this.RenderSize, this.MinAngle, this.MaxAngle, this.IsDirectionReversed);
            for (int i = 0; i < this.AllTicks.Count; i++)
            {
                var tick = this.AllTicks[i];
                var angle = TickHelper.ToAngle(tick, this.Minimum, this.Maximum, arc);
                var po = arc.GetPoint(angle, -this.ReservedSpace / 2);
                var pi = arc.GetPoint(angle, (-this.ReservedSpace / 2) - this.Thickness);
                var line = new Line(po, pi);
                dc.DrawLine(pen, line);
            }
        }
    }
}