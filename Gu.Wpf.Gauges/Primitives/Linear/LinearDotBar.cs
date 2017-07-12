namespace Gu.Wpf.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class LinearDotBar : LinearGeometryBar
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
            var thickness = Math.Abs(this.TickDiameter);
            return new Size(thickness, thickness);
        }

        protected override void OnRender(DrawingContext dc)
        {
            Point CenterPoint(double tick)
            {
                var pos = this.PixelPosition(tick, this.RenderSize);
                if (this.Placement.IsHorizontal())
                {
                    return new Point(pos, this.ActualHeight / 2);
                }
                else
                {
                    return new Point(this.ActualWidth / 2, pos);
                }
            }

            if (this.Pen == null ||
                this.AllTicks == null ||
                this.EffectiveValue == this.Minimum)
            {
                return;
            }

            var r = (this.TickDiameter - this.GetStrokeThickness()) / 2;
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

            foreach (var tick in this.AllTicks)
            {
                dc.DrawEllipse(this.Fill, this.Pen, CenterPoint(tick), r, r);
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
