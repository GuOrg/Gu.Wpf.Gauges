namespace Gu.Wpf.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    /// <summary>
    /// A tick bar that draws circles.
    /// </summary>
    public class LinearDotBar : LinearGeometryTickBar
    {
        /// <summary>
        /// Identifies the <see cref="P:LinearTickBar.TickDiameter" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty TickDiameterProperty = DependencyProperty.Register(
            nameof(TickDiameter),
            typeof(double),
            typeof(LinearDotBar),
            new FrameworkPropertyMetadata(
                1.0d,
                FrameworkPropertyMetadataOptions.AffectsRender));

        static LinearDotBar()
        {
            StrokeProperty.OverrideMetadata(
                typeof(LinearDotBar),
                new FrameworkPropertyMetadata(
                    Brushes.Black,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender,
                    (d, e) => ((LinearDotBar)d).ResetPen()));
        }

        /// <summary>
        /// Gets or sets the <see cref="P:LinearTickBar.TickDiameter" />
        /// The default is 1
        /// </summary>
        public double TickDiameter
        {
            get => (double)this.GetValue(TickDiameterProperty);
            set => this.SetValue(TickDiameterProperty, value);
        }

        protected override Geometry DefiningGeometry => throw new InvalidOperationException("Uses OnRender");

        protected override Size MeasureOverride(Size availableSize)
        {
            var thickness = Math.Abs(this.TickDiameter) + this.GetStrokeThickness();
            return new Rect(0, 0, thickness, thickness).Inflate(this.Padding).Size;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var strokeThickness = this.GetStrokeThickness();
            var startPos = this.CenterPoint(this.Minimum, finalSize);
            var endPos = this.CenterPoint(this.Maximum, finalSize);
            var line = new Line(startPos, endPos);
            line = line.OffsetStart((this.TickDiameter / 2) + (strokeThickness / 2));
            line = line.OffsetEnd((this.TickDiameter / 2) + (strokeThickness / 2));
            if (this.Placement.IsHorizontal())
            {
                this.Overflow = new Thickness(
                    Math.Max(0, -Math.Min(line.StartPoint.X, line.EndPoint.X)),
                    0,
                    Math.Max(0, Math.Max(line.StartPoint.X, line.EndPoint.X) - finalSize.Width),
                    0);
            }
            else
            {
                this.Overflow = new Thickness(
                    0,
                    Math.Max(0, -Math.Min(line.StartPoint.Y, line.EndPoint.Y)),
                    0,
                    Math.Max(0, Math.Max(line.StartPoint.Y, line.EndPoint.Y) - finalSize.Height));
            }

            return finalSize;
        }

        protected override void OnRender(DrawingContext dc)
        {
            if ((this.Pen == null && this.Fill == null) ||
                this.AllTicks == null ||
                DoubleUtil.AreClose(this.EffectiveValue, this.Minimum))
            {
                return;
            }

            var max = this.EffectiveValue;
            if (max < this.Maximum)
            {
                var rect = new Rect(this.RenderSize);
                rect.Inflate(new Size(this.TickDiameter, this.TickDiameter));
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

            var r = this.TickDiameter / 2;

            foreach (var tick in this.AllTicks)
            {
                dc.DrawEllipse(this.Fill, this.Pen, this.CenterPoint(tick, this.RenderSize), r, r);
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

        protected virtual Point CenterPoint(double tick, Size renderSize)
        {
            var pos = this.PixelPosition(tick, renderSize);
            var strokeThickness = this.GetStrokeThickness();
            switch (this.Placement)
            {
                case TickBarPlacement.Left:
                    {
                        var x = this.Padding.Left + (strokeThickness / 2) + (this.TickDiameter / 2);
                        return new Point(x, pos);
                    }

                case TickBarPlacement.Top:
                    {
                        var y = this.Padding.Top + (strokeThickness / 2) + (this.TickDiameter / 2);
                        return new Point(pos, y);
                    }

                case TickBarPlacement.Right:
                    {
                        var x = renderSize.Width - this.Padding.Right - (strokeThickness / 2) - (this.TickDiameter / 2);
                        return new Point(x, pos);
                    }

                case TickBarPlacement.Bottom:
                    {
                        var y = renderSize.Height - this.Padding.Bottom - (strokeThickness / 2) - +(this.TickDiameter / 2);
                        return new Point(pos, y);
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
