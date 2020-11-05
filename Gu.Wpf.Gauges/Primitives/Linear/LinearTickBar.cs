namespace Gu.Wpf.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    /// <summary>
    /// A tick bar that draws ticks similar to <see cref="System.Windows.Controls.Primitives.TickBar"/>
    /// </summary>
    public class LinearTickBar : LinearGeometryTickBar
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
                FrameworkPropertyMetadataOptions.AffectsRender,
                (d, e) => ((LinearTickBar)d).ResetPen()));

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

        protected override double GetStrokeThickness()
        {
            var strokeThickness = base.GetStrokeThickness();
            if (this.TickWidth <= 2 * strokeThickness)
            {
                return this.TickWidth;
            }

            return strokeThickness;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var w = Math.Max(Math.Abs(this.TickWidth), this.GetStrokeThickness());
            var size = this.Placement.IsHorizontal()
                ? new Size(0, w)
                : new Size(w, 0);
            return new Rect(size).Inflate(this.Padding).Size;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var strokeThickness = this.GetStrokeThickness();
            var w = this.TickWidth > strokeThickness
                ? (this.TickWidth + strokeThickness) / 2
                : strokeThickness / 2;
            this.Overflow = this.Placement.IsHorizontal()
                ? new Thickness(Math.Max(0, w - this.Padding.Left), 0, Math.Max(0, w - this.Padding.Right), 0)
                : new Thickness(0, Math.Max(0, w - this.Padding.Top), 0, Math.Max(0, w - this.Padding.Bottom));

            return finalSize;
        }

        protected override void OnRender(DrawingContext dc)
        {
            if ((this.Pen is null && this.Fill is null) ||
                this.AllTicks is null ||
                DoubleUtil.AreClose(this.EffectiveValue, this.Minimum))
            {
                return;
            }

            var max = this.EffectiveValue;
            var strokeThickness = this.GetStrokeThickness();
            if (max < this.Maximum)
            {
                var rect = new Rect(this.RenderSize);
                var w = this.TickWidth > strokeThickness
                    ? strokeThickness + this.TickWidth
                    : strokeThickness;
                rect.Inflate(new Size(w, w));
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

            if (this.TickWidth <= strokeThickness)
            {
                foreach (var tick in this.AllTicks)
                {
                    if (tick > max)
                    {
                        break;
                    }

                    dc.DrawLine(this.Pen, this.CreateLine(tick, this.RenderSize));
                }
            }
            else
            {
                foreach (var tick in this.AllTicks)
                {
                    dc.DrawRectangle(this.Fill, this.Pen, this.CreateRect(tick, this.RenderSize));
                    if (tick > max)
                    {
                        break;
                    }
                }
            }

            if (max < this.Maximum)
            {
                dc.Pop();
            }
        }

        protected virtual Line CreateLine(double tick, Size arrangeSize) => LinearTick.CreateLine(
            this.PixelPosition(tick, arrangeSize),
            this.Placement,
            arrangeSize,
            this.SnapsToDevicePixels);

        protected virtual Rect CreateRect(double tick, Size arrangeSize) => LinearTick.CreateRect(
            this.PixelPosition(tick, arrangeSize),
            this.Placement,
            arrangeSize,
            this.TickWidth,
            this.GetStrokeThickness(),
            this.SnapsToDevicePixels);
    }
}
