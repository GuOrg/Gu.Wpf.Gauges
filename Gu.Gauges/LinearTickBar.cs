namespace Gu.Gauges
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    public class LinearTickBar : FrameworkElement
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
        /// Identifies the <see cref="P:LinearTickBar.Minimum" /> dependency property. 
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:LinearTickBar.Minimum" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty MinimumProperty = RangeBase.MinimumProperty.AddOwner(
            typeof(LinearTickBar),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Identifies the <see cref="P:LinearTickBar.Maximum" /> dependency property. 
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:LinearTickBar.Maximum" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty MaximumProperty = RangeBase.MaximumProperty.AddOwner(
            typeof(LinearTickBar),
            new FrameworkPropertyMetadata(
                1.0,
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Identifies the <see cref="P:LinearTickBar.IsDirectionReversed" /> dependency property. 
        /// </summary>
        public static readonly DependencyProperty IsDirectionReversedProperty = Slider.IsDirectionReversedProperty.AddOwner(
            typeof(LinearTickBar),
            new FrameworkPropertyMetadata(
                false,
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Identifies the <see cref="P:LinearTickBar.ReservedSpace" /> dependency property. This property is read-only.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:LinearTickBar.ReservedSpace" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty ReservedSpaceProperty = TickBar.ReservedSpaceProperty.AddOwner(
            typeof(LinearTickBar),
            new FrameworkPropertyMetadata(
                0.0,
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
        /// Identifies the <see cref="P:LinearTickBar.TickFrequency" /> dependency property. 
        /// </summary>
        public static readonly DependencyProperty TickFrequencyProperty = Slider.TickFrequencyProperty.AddOwner(
            typeof(LinearTickBar),
            new FrameworkPropertyMetadata(
                1.0,
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Identifies the <see cref="P:LinearTickBar.Ticks" /> dependency property. 
        /// </summary>
        public static readonly DependencyProperty TicksProperty = Slider.TicksProperty.AddOwner(
            typeof(LinearTickBar),
            new FrameworkPropertyMetadata(
                null,
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
        /// Gets or sets the <see cref="P:LinearTickBar.Minimum" />
        /// The default is 0
        /// </summary>
        public double Minimum
        {
            get { return (double)this.GetValue(MinimumProperty); }
            set { this.SetValue(MinimumProperty, value); }
        }

        /// <summary>
        /// Gets or sets the highest possible <see cref="P:LinearTickBar.Value" /> of the range element.  
        /// </summary>
        /// <returns>
        /// The highest possible <see cref="P:LinearTickBar.Value" /> of the range element. The default is 1.
        /// </returns>
        public double Maximum
        {
            get { return (double)this.GetValue(MaximumProperty); }
            set { this.SetValue(MaximumProperty, value); }
        }

        /// <summary>
        /// Gets or sets the direction of increasing value. 
        /// </summary>
        /// <returns>
        /// true if the direction of increasing value is to the left for a horizontal tickbar or down for a vertical tickbar; otherwise, false. 
        /// The default is false.
        /// </returns>
        public bool IsDirectionReversed
        {
            get { return (bool)this.GetValue(IsDirectionReversedProperty); }
            set { this.SetValue(IsDirectionReversedProperty, value); }
        }

        /// <summary>
        /// Gets or sets a space buffer for the area that contains the tick marks that are specified for a <see cref="T:LinearTickBar" />.  
        /// </summary>
        /// <returns>
        /// A value that represents the total buffer area on either side of the row or column of tick marks. The default value is zero (0.0).
        /// </returns>
        public double ReservedSpace
        {
            get { return (double)this.GetValue(ReservedSpaceProperty); }
            set { this.SetValue(ReservedSpaceProperty, value); }
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
        /// Gets or sets the interval between tick marks.  
        /// </summary>
        /// <returns>
        /// The distance between tick marks. The default is (1.0).
        /// </returns>
        public double TickFrequency
        {
            get { return (double)this.GetValue(TickFrequencyProperty); }
            set { this.SetValue(TickFrequencyProperty, value); }
        }

        /// <summary>
        /// Gets or sets the positions of the tick marks to display for a <see cref="T:LinearTickBar" />. 
        /// </summary>
        /// <returns>
        /// A set of tick marks to display for a <see cref="T:LinearTickBar" />. The default is null.
        /// </returns>
        public DoubleCollection Ticks
        {
            get { return (DoubleCollection)this.GetValue(TicksProperty); }
            set { this.SetValue(TicksProperty, value); }
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

            var ticks = TickHelper.CreateTicks(this.Minimum, this.Maximum, this.TickFrequency).Concat(this.Ticks ?? Enumerable.Empty<double>());
            var line = new Line(this);
            Vector offset = new Vector(0, 0);
            switch (this.Placement)
            {
                case TickBarPlacement.Left:
                case TickBarPlacement.Right:
                    offset = new Vector(this.ActualWidth, 0);
                    break;
                case TickBarPlacement.Top:
                case TickBarPlacement.Bottom:
                    offset = new Vector(0, this.ActualHeight);
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
