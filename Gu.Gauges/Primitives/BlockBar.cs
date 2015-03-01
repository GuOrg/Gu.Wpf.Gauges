namespace Gu.Gauges
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class BlockBar : Bar
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

            var ticks = this.AllTicks.Concat(new[] { this.Value })
                            .OrderBy(t => t);
            var line = new Line(this.ActualWidth, this.ActualHeight, this.ReservedSpace, this.Placement, this.IsDirectionReversed);
            var previous = line.StartPoint;
            Vector offset = new Vector(0, 0);
            Vector gap = new Vector(0, 0);
            switch (this.Placement)
            {
                case TickBarPlacement.Left:
                    offset = new Vector(this.ActualWidth, 0);
                    gap = new Vector(0, -1 * this.Gap / 2);
                    break;
                case TickBarPlacement.Right:
                    offset = new Vector(-1 * this.ActualWidth, 0);
                    gap = new Vector(0, -1 * this.Gap / 2);
                    break;
                case TickBarPlacement.Top:
                    offset = new Vector(0, this.ActualHeight);
                    gap = new Vector(this.Gap / 2, 0);
                    break;
                case TickBarPlacement.Bottom:
                    offset = new Vector(0, -1 * this.ActualHeight);
                    gap = new Vector(this.Gap / 2, 0);
                    break;
            }
            if (this.IsDirectionReversed)
            {
                gap = -1 * gap;
            }
            foreach (var tick in ticks)
            {
                if (tick > this.Value)
                {
                    var p = TickHelper.ToPos(this.Value, this.Minimum, this.Maximum, line);
                    var r = new Rect(previous, p);
                    dc.DrawRectangle(this.Fill, pen, r);
                    break;
                }

                var pos = TickHelper.ToPos(tick, this.Minimum, this.Maximum, line);
                var rect = new Rect(previous, pos + offset - gap);
                dc.DrawRectangle(this.Fill, pen, rect);
                previous = pos + gap;
            }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            return availableSize;
        }
    }
}