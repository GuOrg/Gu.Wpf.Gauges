namespace Gu.Gauges
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class BlockBar : FrameworkElement
    {
        /// <summary>
        /// Identifies the <see cref="P:BlockBar.Value" /> dependency property. 
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:BlockBar.Value" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty ValueProperty = RangeBase.ValueProperty.AddOwner(
            typeof(BlockBar),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Identifies the <see cref="P:BlockBar.Minimum" /> dependency property. 
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:BlockBar.Minimum" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty MinimumProperty = RangeBase.MinimumProperty.AddOwner(
            typeof(BlockBar),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Identifies the <see cref="P:BlockBar.Maximum" /> dependency property. 
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:BlockBar.Maximum" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty MaximumProperty = RangeBase.MaximumProperty.AddOwner(
            typeof(BlockBar),
            new FrameworkPropertyMetadata(
                1.0,
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Identifies the <see cref="P:BlockBar.IsDirectionReversed" /> dependency property. 
        /// </summary>
        public static readonly DependencyProperty IsDirectionReversedProperty = Slider.IsDirectionReversedProperty.AddOwner(
            typeof(BlockBar),
            new FrameworkPropertyMetadata(
                false,
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Identifies the <see cref="P:BlockBar.ReservedSpace" /> dependency property. This property is read-only.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:BlockBar.ReservedSpace" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty ReservedSpaceProperty = TickBar.ReservedSpaceProperty.AddOwner(
            typeof(BlockBar),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Identifies the <see cref="P:BlockBar.Placement" /> dependency property. This property is read-only.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:BlockBar.Placement" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty PlacementProperty = TickBar.PlacementProperty.AddOwner(
            typeof(BlockBar),
            new FrameworkPropertyMetadata(
                TickBarPlacement.Bottom,
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Identifies the <see cref="P:BlockBar.TickFrequency" /> dependency property. 
        /// </summary>
        public static readonly DependencyProperty TickFrequencyProperty = Slider.TickFrequencyProperty.AddOwner(
            typeof(BlockBar),
            new FrameworkPropertyMetadata(
                1.0,
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Identifies the <see cref="P:BlockBar.Ticks" /> dependency property. 
        /// </summary>
        public static readonly DependencyProperty TicksProperty = Slider.TicksProperty.AddOwner(
            typeof(BlockBar),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty FillProperty = Shape.FillProperty.AddOwner(
            typeof(BlockBar),
            new FrameworkPropertyMetadata(
                Brushes.HotPink,
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
            new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Gets or sets the current magnitude of the range control.  
        /// </summary>
        /// <returns>
        /// The current magnitude of the range control. The default is 0.
        /// </returns>
        public double Value
        {
            get { return (double)this.GetValue(ValueProperty); }
            set { this.SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="P:BlockBar.Minimum" />
        /// The default is 0
        /// </summary>
        public double Minimum
        {
            get { return (double)this.GetValue(MinimumProperty); }
            set { this.SetValue(MinimumProperty, value); }
        }

        /// <summary>
        /// Gets or sets the highest possible <see cref="P:BlockBar.Value" /> of the range element.  
        /// </summary>
        /// <returns>
        /// The highest possible <see cref="P:BlockBar.Value" /> of the range element. The default is 1.
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
        /// Gets or sets a space buffer for the area that contains the tick marks that are specified for a <see cref="T:BlockBar" />.  
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
        /// A <see cref="T:BlockBarPlacement" /> enumeration value that identifies the position of the <see cref="T:BlockBar" /> in the <see cref="T:System.Windows.Style" /> layout of a <see cref="T:System.Windows.Controls.Slider" />. The default value is <see cref="F:BlockBarPlacement.Top" />.
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
        /// Gets or sets the positions of the tick marks to display for a <see cref="T:BlockBar" />. 
        /// </summary>
        /// <returns>
        /// A set of tick marks to display for a <see cref="T:BlockBar" />. The default is null.
        /// </returns>
        public DoubleCollection Ticks
        {
            get { return (DoubleCollection)this.GetValue(TicksProperty); }
            set { this.SetValue(TicksProperty, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="T:System.Windows.Media.Brush" /> that specifies how the shape's interior is painted. 
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Media.Brush" /> that describes how the shape's interior is painted. The default is HotPink.
        /// </returns>
        public Brush Fill
        {
            get { return (Brush)this.GetValue(FillProperty); }
            set { this.SetValue(FillProperty, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="T:System.Windows.Media.Brush" /> that specifies how the <see cref="T:BlockBar" /> outline is painted. 
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Media.Brush" /> that specifies how the <see cref="T:BlockBar" /> outline is painted. The default is null.
        /// </returns>
        public Brush Stroke
        {
            get { return (Brush)this.GetValue(StrokeProperty); }
            set { this.SetValue(StrokeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the width of the <see cref="T:BlockBar" /> outline. 
        /// </summary>
        /// <returns>
        /// The width of the <see cref="T:BlockBar" /> outline.
        /// </returns>
        public double StrokeThickness
        {
            get { return (double)this.GetValue(StrokeThicknessProperty); }
            set { this.SetValue(StrokeThicknessProperty, value); }
        }

        /// <summary>
        /// Gets or sets the gap  in pixels between blocks. Default is 1.0
        /// </summary>
        public double Gap
        {
            get { return (double)this.GetValue(GapProperty); }
            set { this.SetValue(GapProperty, value); }
        }

        protected override void OnRender(DrawingContext dc)
        {
            var pen = new Pen(this.Stroke, this.StrokeThickness);
            pen.Freeze();

            var ticks = TickHelper.CreateTicks(this.Minimum, this.Maximum, this.TickFrequency).Concat(this.Ticks ?? Enumerable.Empty<double>()).OrderBy(t => t);
            var line = new Line(this.ActualWidth, this.ActualHeight, this.ReservedSpace, this.Placement, this.IsDirectionReversed);
            var previous = line.StartPoint;
            Vector offset = new Vector(0, 0);
            Vector gap = new Vector(0, 0);
            switch (this.Placement)
            {
                case TickBarPlacement.Left:
                case TickBarPlacement.Right:
                    offset = new Vector(this.ActualWidth, 0);
                    gap = new Vector(0, -1 * this.Gap / 2);
                    break;
                case TickBarPlacement.Top:
                case TickBarPlacement.Bottom:
                    offset = new Vector(0, this.ActualHeight);
                    gap = new Vector(this.Gap / 2, 0);
                    break;
            }
            if (this.IsDirectionReversed)
            {
                gap = -1 * gap;
            }
            foreach (var tick in ticks)
            {
                if (tick <= this.Minimum || tick > this.Maximum)
                {
                    continue;
                }
                if (tick > this.Value)
                {
                    var p = TickHelper.ToPos(this.Value, this.Minimum, this.Maximum, line);
                    var r = new Rect(previous, p + offset);
                    dc.DrawRectangle(this.Fill, pen, r);
                    break;
                }

                var pos = TickHelper.ToPos(tick, this.Minimum, this.Maximum, line);
                var rect = new Rect(previous, pos + offset - gap);
                dc.DrawRectangle(this.Fill, pen, rect);
                previous = pos + gap;
            }
        }
    }
}