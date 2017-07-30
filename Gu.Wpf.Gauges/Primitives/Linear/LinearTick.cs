namespace Gu.Wpf.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    /// <summary>
    /// A tick bar that draws a tick at the position of Value
    /// </summary>
    public class LinearTick : LinearGeometryBar
    {
        /// <summary>
        /// Identifies the <see cref="P:LinearTickBar.TickWidth" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty TickWidthProperty = DependencyProperty.Register(
            nameof(TickWidth),
            typeof(double),
            typeof(LinearTick),
            new FrameworkPropertyMetadata(
                1.0d,
                FrameworkPropertyMetadataOptions.AffectsRender,
                (d, e) => ((LinearTick)d).ResetPen()));

        static LinearTick()
        {
            StrokeProperty.OverrideMetadata(
                typeof(LinearTick),
                new FrameworkPropertyMetadata(
                    Brushes.Black,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender,
                    (d, e) => ((LinearTick)d).ResetPen()));
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

        /// <summary>
        /// Create a line for the tick at the position.
        /// This is useful when stroke thickness &gt; thickness.
        /// </summary>
        public static Line CreateLine(double position, TickBarPlacement placement, Size arrangeSize, bool snapsToDevicePixels)
        {
            var line = placement.IsHorizontal()
                ? new Line(new Point(position, 0), new Point(position, arrangeSize.Height))
                : new Line(new Point(0, position), new Point(arrangeSize.Width, position));
            return snapsToDevicePixels
                ? new Line(line.StartPoint.Round(0), line.EndPoint.Round(0))
                : line;
        }

        /// <summary>
        /// Create a <see cref="Rect"/> for the tick at the position.
        /// This is useful when stroke thickness &lt; thickness.
        /// </summary>
        public static Rect CreateRect(double position, TickBarPlacement placement, Size arrangeSize, double tickWidth, double strokeThickness, bool snapsToDevicePixels)
        {
            var rect = placement.IsHorizontal()
                ? new Rect(
                    x: position - (tickWidth / 2),
                    y: strokeThickness / 2,
                    width: tickWidth,
                    height: arrangeSize.Height - strokeThickness)
                : new Rect(
                    x: strokeThickness / 2,
                    y: position - (tickWidth / 2),
                    width: arrangeSize.Width - strokeThickness,
                    height: tickWidth);

            return snapsToDevicePixels
                ? new Rect(rect.TopLeft.Round(0), rect.BottomRight.Round(0))
                : rect;
        }

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
            if ((this.Pen == null && this.Fill == null) ||
                double.IsNaN(this.Value))
            {
                return;
            }

            if (this.TickWidth > 0)
            {
                dc.DrawRectangle(this.Fill, this.Pen, this.CreateRect(this.Value, this.RenderSize));
            }
            else
            {
                dc.DrawLine(this.Pen, this.CreateLine(this.Value, this.RenderSize));
            }
        }

        protected virtual Line CreateLine(double value, Size arrangeSize) => CreateLine(
            this.PixelPosition(value, arrangeSize),
            this.Placement,
            arrangeSize,
            this.SnapsToDevicePixels);

        protected virtual Rect CreateRect(double tick, Size arrangeSize) => CreateRect(
            this.PixelPosition(tick, arrangeSize),
            this.Placement,
            arrangeSize,
            this.TickWidth,
            this.GetStrokeThickness(),
            this.SnapsToDevicePixels);
    }
}
