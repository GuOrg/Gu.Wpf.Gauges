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
                if (this.Placement.IsHorizontal())
                {
                    return new Rect(pos - (this.TickWidth / 2) - (strokeThickness / 2), strokeThickness / 2, this.TickWidth + strokeThickness, this.ActualHeight - strokeThickness);
                }
                else
                {
                    return new Rect(strokeThickness / 2, pos - (this.TickWidth / 2) - (strokeThickness / 2), this.ActualWidth - strokeThickness, this.TickWidth + strokeThickness);
                }
            }

            if (this.Pen == null ||
                this.AllTicks == null)
            {
                return;
            }

            if (this.TickWidth <= this.StrokeThickness)
            {
                foreach (var tick in this.AllTicks)
                {
                    dc.DrawLine(this.Pen, CreateLine(tick));
                }
            }
            else
            {
                foreach (var tick in this.AllTicks)
                {
                    dc.DrawRectangle(this.Fill, this.Pen, CreateRect(tick));
                }
            }
        }
    }
}
