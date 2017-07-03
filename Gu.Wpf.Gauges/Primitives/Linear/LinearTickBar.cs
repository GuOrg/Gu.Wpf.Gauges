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
            return new Size(thickness, thickness);
        }

        protected override void OnRender(DrawingContext dc)
        {
            Line CreateLine(double value)
            {
                var pos = this.PixelPosition(value);
                if (this.SnapsToDevicePixels)
                {
                    pos = Math.Round(pos, 0);
                }

                return this.Placement.IsHorizontal()
                    ? new Line(new Point(pos, 0), new Point(pos, this.ActualHeight))
                    : new Line(new Point(0, pos), new Point(this.ActualWidth, pos));
            }

            Rect CreateRect(double tick)
            {
                var pos = this.PixelPosition(tick);
                var strokeThickness = this.GetStrokeThickness();
                return this.Placement.IsHorizontal()
                    ? new Rect(
                        x: pos - (this.TickWidth / 2) + (strokeThickness / 2),
                        y: strokeThickness / 2,
                        width: this.TickWidth - strokeThickness,
                        height: this.ActualHeight - strokeThickness)
                    : new Rect(
                        x: strokeThickness / 2,
                        y: pos - (this.TickWidth / 2) + (strokeThickness / 2),
                        width: this.ActualWidth - strokeThickness,
                        height: this.TickWidth - strokeThickness);
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
                    var pos = this.PixelPosition(max);
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
                    dc.DrawRectangle(this.Fill, this.Pen, CreateRect(tick));
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
    }
}
