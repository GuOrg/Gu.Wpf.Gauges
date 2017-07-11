namespace Gu.Wpf.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class LinearTickBar : LinearGeometryBar
    {
        /// <summary>
        /// Identifies the <see cref="P:LinearTickBar.TickWidth" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty TickWidthProperty = DependencyProperty.Register(
            nameof(TickWidth),
            typeof(double),
            typeof(LinearTickBar),
            new FrameworkPropertyMetadata(
                1.0d,
                FrameworkPropertyMetadataOptions.AffectsRender));

        static LinearTickBar()
        {
            StrokeProperty.OverrideMetadata(
                typeof(LinearTickBar),
                new FrameworkPropertyMetadata(
                    Brushes.Black,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender,
                    (d, e) => ((LinearTickBar)d).ResetPen()));
        }

        /// <summary>
        /// Gets or sets the <see cref="P:LinearTickBar.TickWidth" />
        /// The default is 1
        /// </summary>
        public double TickWidth
        {
            get => (double)this.GetValue(TickWidthProperty);
            set => this.SetValue(TickWidthProperty, value);
        }

        protected override Geometry DefiningGeometry => throw new InvalidOperationException("Uses OnRender");

        protected override Size MeasureOverride(Size availableSize)
        {
            var thickness = Math.Abs(this.TickWidth);
            return this.Placement.IsHorizontal() ? new Size(0, thickness) : new Size(thickness, 0);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this.AllTicks != null)
            {
                var tickBounds = default(Rect);
                var strokeThickness = this.Placement.IsHorizontal()
                    ? new Size(this.GetStrokeThickness() / 2, 0)
                    : new Size(0, this.GetStrokeThickness() / 2);
                var rect = this.CreateRect(this.AllTicks[0], finalSize);
                rect.Inflate(strokeThickness);
                tickBounds.Union(rect);
                if (this.EffectiveValue == this.Maximum)
                {
                    rect = this.CreateRect(this.AllTicks[this.AllTicks.Count - 1], finalSize);
                    rect.Inflate(strokeThickness);
                    tickBounds.Union(rect);
                }

                this.Overflow = this.Placement.IsHorizontal()
                    ? new Thickness(Math.Max(0, -tickBounds.Left), 0, Math.Max(0, tickBounds.Right - finalSize.Width), 0)
                    : new Thickness(0, Math.Max(0, -tickBounds.Top), 0, Math.Max(0, tickBounds.Bottom - finalSize.Height));
            }

            return finalSize;
        }

        protected override void OnRender(DrawingContext dc)
        {
            Line CreateLine(double value)
            {
                var pos = this.PixelPosition(value, this.RenderSize);
                if (this.SnapsToDevicePixels)
                {
                    pos = Math.Round(pos, 0);
                }

                return this.Placement.IsHorizontal()
                    ? new Line(new Point(pos, 0), new Point(pos, this.ActualHeight))
                    : new Line(new Point(0, pos), new Point(this.ActualWidth, pos));
            }

            if (this.Pen == null ||
                this.AllTicks == null ||
                this.EffectiveValue == this.Minimum)
            {
                return;
            }

            var max = this.EffectiveValue;
            if (this.TickWidth <= this.StrokeThickness)
            {
                foreach (var tick in this.AllTicks)
                {
                    if (tick > max)
                    {
                        break;
                    }

                    dc.DrawLine(this.Pen, CreateLine(tick));
                }
            }
            else
            {
                if (max < this.Maximum)
                {
                    var rect = new Rect(this.RenderSize);
                    rect.Inflate(new Size(this.TickWidth, this.TickWidth));
                    var pos = this.PixelPosition(max, this.RenderSize);
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

                    dc.PushClip(new RectangleGeometry(rect));
                }

                foreach (var tick in this.AllTicks)
                {
                    dc.DrawRectangle(this.Fill, this.Pen, this.CreateRect(tick, this.RenderSize));
                    if (tick > max)
                    {
                        break;
                    }
                }

                if (max < this.Maximum)
                {
                    dc.Pop();
                }
            }
        }

        protected virtual Rect CreateRect(double tick, Size arrangeSize)
        {
            var position = this.PixelPosition(tick, arrangeSize);
            var strokeThickness = this.GetStrokeThickness();
            return this.Placement.IsHorizontal()
                ? new Rect(
                    x: position - (this.TickWidth / 2) + (strokeThickness / 2),
                    y: strokeThickness / 2,
                    width: this.TickWidth - strokeThickness,
                    height: arrangeSize.Height - strokeThickness)
                : new Rect(
                    x: strokeThickness / 2,
                    y: position - (this.TickWidth / 2) + (strokeThickness / 2),
                    width: arrangeSize.Width - strokeThickness,
                    height: this.TickWidth - strokeThickness);
        }
    }
}
