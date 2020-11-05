namespace Gu.Wpf.Gauges
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Media;

    public class LinearBlockBar : LinearGeometryTickBar
    {
        public static readonly DependencyProperty TickGapProperty = DependencyProperty.Register(
            nameof(TickGap),
            typeof(double),
            typeof(LinearBlockBar),
            new FrameworkPropertyMetadata(
                1.0d,
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Gets or sets the gap  in pixels between blocks. Default is 1.0
        /// </summary>
        public double TickGap
        {
            get => (double)this.GetValue(TickGapProperty);
            set => this.SetValue(TickGapProperty, value);
        }

        protected override Geometry DefiningGeometry => throw new InvalidOperationException("Uses OnRender");

        protected override Size ArrangeOverride(Size finalSize)
        {
            var w = this.GetStrokeThickness() / 2;
            this.Overflow = this.Placement.IsHorizontal()
                ? new Thickness(Math.Max(0, w - this.Padding.Left), 0, Math.Max(0, w - this.Padding.Right), 0)
                : new Thickness(0, Math.Max(0, w - this.Padding.Top), 0, Math.Max(0, w - this.Padding.Bottom));

            return finalSize;
        }

        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        protected override void OnRender(DrawingContext dc)
        {
            Rect CreateBar()
            {
                var rect = new Rect(this.RenderSize);
                var strokeThickness = this.GetStrokeThickness();
                rect.Inflate(-strokeThickness / 2, -strokeThickness / 2);
                var value = this.EffectiveValue;
                var pos = this.PixelPosition(value, this.RenderSize);
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

            Rect Split(ref Rect barRect, double tickValue)
            {
                var pos = this.PixelPosition(tickValue, this.RenderSize);
                var offset = (this.TickGap / 2) + (this.GetStrokeThickness() / 2);
                if (this.Placement.IsHorizontal())
                {
                    if (this.IsDirectionReversed)
                    {
                        var tick = barRect.TrimLeft(pos + offset);
                        RectExt.SetRight(ref barRect, pos - offset);
                        return tick;
                    }
                    else
                    {
                        var tick = barRect.TrimRight(pos - offset);
                        RectExt.SetLeft(ref barRect, pos + offset);
                        return tick;
                    }
                }
                else
                {
                    if (this.IsDirectionReversed)
                    {
                        var tick = barRect.TrimBottom(pos - offset);
                        RectExt.SetTop(ref barRect, pos + offset);
                        return tick;
                    }
                    else
                    {
                        var tick = barRect.TrimTop(pos + offset);
                        RectExt.SetBottom(ref barRect, pos - offset);
                        return tick;
                    }
                }
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
                    if (rect.Width <= this.GetStrokeThickness())
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
                    if (rect.Height <= this.GetStrokeThickness())
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
                (this.Fill is null && this.Stroke is null))
            {
                return;
            }

            var bar = CreateBar();
            if (this.AllTicks != null)
            {
                foreach (var tick in this.AllTicks)
                {
                    if (tick == this.Maximum ||
                        bar.Width == 0 ||
                        bar.Height == 0)
                    {
                        break;
                    }

                    if (tick == this.Minimum)
                    {
                        continue;
                    }

                    var tickRect = Split(ref bar, tick);
                    Draw(ref tickRect);
                    if (tick > this.EffectiveValue)
                    {
                        break;
                    }
                }
            }

            Draw(ref bar);
        }
    }
}