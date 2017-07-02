namespace Gu.Wpf.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    public class LinearTickBar : TickBarBase
    {
        /// <summary>
        /// Identifies the <see cref="P:Bar.Placement" /> dependency property. This property is read-only.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:Bar.Placement" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty PlacementProperty = LinearGauge.PlacementProperty.AddOwner(
            typeof(LinearTickBar),
            new FrameworkPropertyMetadata(
                TickBarPlacement.Bottom,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:LinearTickBar.PenWidth" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty PenWidthProperty = DependencyProperty.Register(
            nameof(PenWidth),
            typeof(double),
            typeof(LinearTickBar),
            new FrameworkPropertyMetadata(
                1.0d,
                FrameworkPropertyMetadataOptions.AffectsRender,
                (d, _) => ((LinearTickBar)d).pen = null));

        /// <summary>
        /// Identifies the <see cref="P:LinearTickBar.Fill" /> dependency property. This property is read-only.
        /// </summary>
        public static readonly DependencyProperty FillProperty = TickBar.FillProperty.AddOwner(
            typeof(LinearTickBar),
            new FrameworkPropertyMetadata(
                default(Brush),
                FrameworkPropertyMetadataOptions.AffectsRender,
                (d, _) => ((LinearTickBar)d).pen = null));

        private Pen pen;

        /// <summary>
        /// Gets or sets where tick marks appear  relative to a <see cref="T:System.Windows.Controls.Primitives.Track" /> of a <see cref="T:System.Windows.Controls.Slider" /> control.
        /// </summary>
        /// <returns>
        /// A <see cref="T:TickBarPlacement" /> enumeration value that identifies the position of the <see cref="T:LinearTickBar" /> in the <see cref="T:System.Windows.Style" /> layout of a <see cref="T:System.Windows.Controls.Slider" />. The default value is <see cref="F:Bar.Top" />.
        /// </returns>
        public TickBarPlacement Placement
        {
            get => (TickBarPlacement)this.GetValue(PlacementProperty);
            set => this.SetValue(PlacementProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="P:LinearTickBar.PenWidth" />
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

        private Pen Pen
        {
            get
            {
                if (this.pen == null)
                {
                    if (this.Fill != null && this.PenWidth > 0)
                    {
                        this.pen = new Pen(this.Fill, this.PenWidth);
                    }
                }

                return this.pen;
            }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var thickness = Math.Abs(this.PenWidth);
            return new Size(thickness, thickness);
        }

        protected override void OnRender(DrawingContext dc)
        {
            double PixelPosition(double value)
            {
                var scale = Interpolate.Linear(this.Minimum, this.Maximum, value)
                                       .Clamp(0, 1);
                if (this.Placement.IsHorizontal())
                {
                    var pos = (this.PenWidth / 2) + (scale * (this.ActualWidth - this.PenWidth));
                    return this.IsDirectionReversed
                        ? this.ActualWidth - pos
                        : pos;
                }
                else
                {
                    var pos = (this.PenWidth / 2) + (scale * (this.ActualHeight - this.PenWidth));
                    return this.IsDirectionReversed
                        ? pos
                        : this.ActualHeight - pos;
                }
            }

            Line CreateLine(double value)
            {
                var pos = PixelPosition(value);
                if (this.SnapsToDevicePixels)
                {
                    pos = Math.Round(pos, 0);
                }

                return this.Placement.IsHorizontal()
                    ? new Line(new Point(pos, 0), new Point(pos, this.ActualHeight))
                    : new Line(new Point(0, pos), new Point(this.ActualWidth, pos));
            }

            if (this.Pen == null)
            {
                return;
            }

            foreach (var tick in this.AllTicks)
            {
                dc.DrawLine(this.Pen, CreateLine(tick));
            }
        }
    }
}
