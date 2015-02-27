namespace Gu.Gauges
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
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
        /// Identifies the <see cref="P:LinearTickBar.Placement" /> dependency property. This property is read-only.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:LinearTickBar.Placement" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty PlacementProperty = TickBar.PlacementProperty.AddOwner(
            typeof(LinearTickBar),
            new FrameworkPropertyMetadata(
                TickBarPlacement.Bottom,
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
        /// Gets or sets where tick marks appear  relative to a <see cref="T:System.Windows.Controls.Primitives.Track" /> of a <see cref="T:System.Windows.Controls.Slider" /> control.  
        /// </summary>
        /// <returns>
        /// A <see cref="T:LinearTickBarPlacement" /> enumeration value that identifies the position of the <see cref="T:LinearTickBar" /> in the <see cref="T:System.Windows.Style" /> layout of a <see cref="T:System.Windows.Controls.Slider" />. The default value is <see cref="F:LinearTickBarPlacement.Top" />.
        /// </returns>
        public TickBarPlacement Placement
        {
            get { return (TickBarPlacement)this.GetValue(PlacementProperty); }
            set { this.SetValue(PlacementProperty, value); }
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

        protected override void OnRender(DrawingContext dc)
        {
            if (this.Fill == null)
            {
                return;
            }
            var pen = new Pen(this.Fill, this.PenWidth);
            pen.Freeze();
            var ticks = TickHelper.CreateTicks(this.Minimum, this.Maximum, this.TickFrequency).Concat(this.Ticks ?? Enumerable.Empty<double>());
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
            foreach (var tick in ticks)
            {
                if (tick < this.Minimum || tick > this.Maximum)
                {
                    continue;
                }
                var pos = TickHelper.ToPos(tick, this.Minimum, this.Maximum, line);
                dc.DrawLine(pen, pos, pos + offset);
            }
        }
    }
}
