namespace Gu.Wpf.Gauges
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    public class LinearBlockBar : GeometryBar
    {
        /// <summary>
        /// Identifies the <see cref="P:LinearBlockBar.Placement" /> dependency property. This property is read-only.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:LinearBlockBar.Placement" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty PlacementProperty = LinearGauge.PlacementProperty.AddOwner(
            typeof(LinearBlockBar),
            new FrameworkPropertyMetadata(
                TickBarPlacement.Bottom,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:BlockBar.Value" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:BlockBar.Value" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty ValueProperty = Gauge.ValueProperty.AddOwner(
            typeof(LinearBlockBar),
            new FrameworkPropertyMetadata(
                0.0d,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty TickGapProperty = DependencyProperty.Register(
            nameof(TickGap),
            typeof(double),
            typeof(LinearBlockBar),
            new FrameworkPropertyMetadata(
                1.0d,
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));

        static LinearBlockBar()
        {
            SnapsToDevicePixelsProperty.OverrideMetadata(typeof(LinearBlockBar), new FrameworkPropertyMetadata(true));
        }

        /// <summary>
        /// Gets or sets where tick marks appear  relative to a <see cref="T:System.Windows.Controls.Primitives.Track" /> of a <see cref="T:System.Windows.Controls.Slider" /> control.
        /// </summary>
        /// <returns>
        /// A <see cref="T:TickBarPlacement" /> enumeration value that identifies the position of the <see cref="T:LinearTextTickBar" /> in the <see cref="T:System.Windows.Style" /> layout of a <see cref="T:System.Windows.Controls.Slider" />. The default value is <see cref="F:LinearBlockBar.Top" />.
        /// </returns>
        public TickBarPlacement Placement
        {
            get => (TickBarPlacement)this.GetValue(PlacementProperty);
            set => this.SetValue(PlacementProperty, value);
        }

        /// <summary>
        /// Gets or sets the current magnitude of the range control.
        /// </summary>
        /// <returns>
        /// The current magnitude of the range control. The default is 0.
        /// </returns>
        public double Value
        {
            get => (double)this.GetValue(ValueProperty);
            set => this.SetValue(ValueProperty, value);
        }

        /// <summary>
        /// Gets or sets the gap  in pixels between blocks. Default is 1.0
        /// </summary>
        public double TickGap
        {
            get => (double)this.GetValue(TickGapProperty);
            set => this.SetValue(TickGapProperty, value);
        }

        protected override Geometry DefiningGeometry => throw new InvalidOperationException("Uses OnRender");

        protected override void OnRender(DrawingContext dc)
        {
            double PixelPosition(double value)
            {
                var scale = Interpolate.Linear(this.Minimum, this.Maximum, value)
                                       .Clamp(0, 1);
                if (this.Placement.IsHorizontal())
                {
                    var pos = (this.StrokeThickness / 2) + (scale * (this.ActualWidth - this.StrokeThickness));
                    return this.IsDirectionReversed
                        ? this.ActualWidth - pos
                        : pos;
                }
                else
                {
                    var pos = (this.StrokeThickness / 2) + (scale * (this.ActualHeight - this.StrokeThickness));
                    return this.IsDirectionReversed
                        ? this.ActualHeight - pos
                        : pos;
                }
            }

            Rect CreateBar()
            {
                var rect = new Rect(this.RenderSize);
                rect.Inflate(-this.StrokeThickness / 2, -this.StrokeThickness / 2);
                var pos = PixelPosition(this.Value);
                if (this.Placement.IsHorizontal())
                {
                    if (this.IsDirectionReversed)
                    {
                        RectExt.SetLeft(ref rect, pos);
                    }
                    else
                    {
                        RectExt.SetRight(ref rect, pos);
                    }
                }
                else
                {
                    if (this.IsDirectionReversed)
                    {
                        RectExt.SetBottom(ref rect, pos);
                    }
                    else
                    {
                        RectExt.SetTop(ref rect, pos);
                    }
                }

                return rect;
            }

            void Draw(ref Rect rect)
            {
                if (this.SnapsToDevicePixels)
                {
                    var right = rect.Right;
                    var bottom = rect.Bottom;
                    rect.X = Math.Round(rect.X);
                    rect.Y = Math.Round(rect.Y);
                    rect.Width = Math.Round(right - rect.X);
                    rect.Height = Math.Round(bottom - rect.Y);
                }

                if (this.Placement.IsHorizontal())
                {
                    if (rect.Width <= this.StrokeThickness)
                    {
                        if (!DoubleUtil.IsZero(rect.Width))
                        {
                            dc.DrawLine(this.Pen, rect.BottomLeft, rect.TopLeft);
                        }
                    }
                    else
                    {
                        dc.DrawRectangle(this.Fill, this.Pen, rect);
                    }
                }
                else
                {
                    if (rect.Height <= this.StrokeThickness)
                    {
                        if (!DoubleUtil.IsZero(rect.Height))
                        {
                            dc.DrawLine(this.Pen, rect.BottomLeft, rect.BottomRight);
                        }
                    }
                    else
                    {
                        dc.DrawRectangle(this.Fill, this.Pen, rect);
                    }
                }
            }

            if (this.Value == this.Minimum ||
                (this.Fill == null && this.Stroke == null))
            {
                return;
            }

            if (this.AllTicks.Count == 0)
            {
                var bar = CreateBar();
                Draw(ref bar);
                return;
            }

            var line = new Line(this.ActualWidth, this.ActualHeight, this.ReservedSpace, this.Placement, this.IsDirectionReversed);
            var previous = line.StartPoint;
            var offset = new Vector(0, 0);
            var gap = new Vector(0, 0);
            switch (this.Placement)
            {
                case TickBarPlacement.Left:
                    offset = new Vector(this.ActualWidth, 0);
                    gap = new Vector(0, -1 * this.TickGap / 2);
                    break;
                case TickBarPlacement.Right:
                    offset = new Vector(-1 * this.ActualWidth, 0);
                    gap = new Vector(0, -1 * this.TickGap / 2);
                    break;
                case TickBarPlacement.Top:
                    offset = new Vector(0, this.ActualHeight);
                    gap = new Vector(this.TickGap / 2, 0);
                    break;
                case TickBarPlacement.Bottom:
                    offset = new Vector(0, -1 * this.ActualHeight);
                    gap = new Vector(this.TickGap / 2, 0);
                    break;
            }

            if (this.IsDirectionReversed)
            {
                gap = -1 * gap;
            }

            var ticks = this.AllTicks
                            .Concat(new[] { this.Value })
                            .OrderBy(t => t);
            foreach (var tick in ticks)
            {
                if (tick == this.Minimum)
                {
                    continue;
                }

                if (tick > this.Value)
                {
                    var p = TickHelper.ToPos(this.Value, this.Minimum, this.Maximum, line);
                    var r = new Rect(previous, p);
                    dc.DrawRectangle(this.Fill, this.Pen, r);
                    break;
                }

                var pos = TickHelper.ToPos(tick, this.Minimum, this.Maximum, line);
                var rect = new Rect(previous, pos + offset - gap);
                dc.DrawRectangle(this.Fill, this.Pen, rect);
                previous = pos + gap;
            }
        }
    }
}