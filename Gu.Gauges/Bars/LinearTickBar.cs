namespace Gu.Gauges
{
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    public class LinearTickBar : Bar
    {
        /// <summary>
        /// Identifies the <see cref="P:LinearTickBar.PenWidth" /> dependency property. 
        /// </summary>
        public static readonly DependencyProperty PenWidthProperty = DependencyProperty.Register(
            "PenWidth",
            typeof(double),
            typeof(LinearTickBar),
            new FrameworkPropertyMetadata(
                1.0,
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Identifies the <see cref="P:LinearTickBar.Fill" /> dependency property. This property is read-only.
        /// </summary>
        public static readonly DependencyProperty FillProperty = TickBar.FillProperty.AddOwner(
            typeof(LinearTickBar),
            new FrameworkPropertyMetadata(
                default(Brush),
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Gets or sets the <see cref="P:LinearTickBar.PenWidth" />
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

        public Geometry RenderedGeometry
        {
            get
            {
                var line = new Line(this.ActualWidth, this.ActualHeight, this.ReservedSpace, this.Placement, this.IsDirectionReversed);
                Vector offset = new Vector(0, 0);
                switch (this.Placement)
                {
                    case TickBarPlacement.Left:
                        offset = new Vector(this.ActualWidth, 0);
                        break;
                    case TickBarPlacement.Right:
                        offset = new Vector(-1 * this.ActualWidth, 0);
                        break;
                    case TickBarPlacement.Top:
                        offset = new Vector(0, this.ActualHeight);
                        break;
                    case TickBarPlacement.Bottom:
                        offset = new Vector(0, -1 * this.ActualHeight);
                        break;
                }

                var geometry = new StreamGeometry();
                using (var context = geometry.Open())
                {
                    foreach (var tick in this.AllTicks)
                    {
                        var p = TickHelper.ToPos(tick, this.Minimum, this.Maximum, line);
                        context.BeginFigure(p, false, false);
                        context.LineTo(p + offset, true, false);
                    }
                }
                return geometry;
            }
        }

        protected override void OnRender(DrawingContext dc)
        {
            dc.DrawGeometry(null, new Pen(this.Fill, this.PenWidth), this.RenderedGeometry);
        }
    }
}
