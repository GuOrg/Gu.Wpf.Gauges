namespace Gu.Wpf.Gauges
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    public class LinearTextTickBar : TextTickBar
    {
        /// <summary>
        /// Identifies the <see cref="P:Bar.Placement" /> dependency property. This property is read-only.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:Bar.Placement" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty PlacementProperty = LinearGauge.PlacementProperty.AddOwner(
            typeof(LinearTextTickBar),
            new FrameworkPropertyMetadata(
                TickBarPlacement.Bottom,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty TextTransformProperty = DependencyProperty.Register(
            nameof(TextTransform),
            typeof(Transform),
            typeof(LinearTextTickBar),
            new FrameworkPropertyMetadata(
                default(Transform),
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Gets or sets where tick marks appear  relative to a <see cref="T:System.Windows.Controls.Primitives.Track" /> of a <see cref="T:System.Windows.Controls.Slider" /> control.
        /// </summary>
        /// <returns>
        /// A <see cref="T:TickBarPlacement" /> enumeration value that identifies the position of the <see cref="T:LinearTextTickBar" /> in the <see cref="T:System.Windows.Style" /> layout of a <see cref="T:System.Windows.Controls.Slider" />. The default value is <see cref="F:Bar.Top" />.
        /// </returns>
        public TickBarPlacement Placement
        {
            get => (TickBarPlacement)this.GetValue(PlacementProperty);
            set => this.SetValue(PlacementProperty, value);
        }

        public Transform TextTransform
        {
            get => (Transform)this.GetValue(TextTransformProperty);
            set => this.SetValue(TextTransformProperty, value);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var rect = default(Rect);
            if (this.AllTexts != null)
            {
                foreach (var tickText in this.AllTexts)
                {
                    var p = new Point(tickText.Width, tickText.Height);
                    if (tickText.Transform != null)
                    {
                        p = tickText.Transform.Transform(p);
                    }

                    rect.Union(p);
                }
            }

            return rect.Size;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this.AllTexts != null)
            {
                foreach (var tickText in this.AllTexts)
                {
                    tickText.Point = this.PixelPosition(tickText, finalSize);
                }
            }

            return finalSize;
        }

        protected override void OnRender(DrawingContext dc)
        {
            if (this.Foreground == null ||
                this.AllTexts == null ||
                this.AllTexts.Count == 0)
            {
                return;
            }

            foreach (var tickText in this.AllTexts)
            {
                if (tickText.Transform != null)
                {
                var pos = Gauges.Ticks.ToPos(tick, this.Minimum, this.Maximum, line);
                }

                dc.DrawText(tickText, tickText.Point);

                if (tickText.Transform != null)
                {
                    dc.Pop();
                }
            }
        }

        /// <summary>
        /// Get the interpolated pixel position for the value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="finalSize"></param>
        /// <returns></returns>
        protected virtual double PixelPosition(double value, Size finalSize)
        {
            var scale = Interpolate.Linear(this.Minimum, this.Maximum, value)
                                   .Clamp(0, 1);

            if (this.Placement.IsHorizontal())
            {
                var pos = scale * finalSize.Width;
                return this.IsDirectionReversed
                    ? finalSize.Width - pos
                    : pos;
            }
            else
            {
                var pos = scale * finalSize.Height;
                return this.IsDirectionReversed
                    ? pos
                    : finalSize.Height - pos;
            }
        }

        protected virtual Point PixelPosition(TickText tickText, Size finalSize)
        {
            var geometry = tickText.BuildGeometry(default(Point));
            geometry.SetCurrentValue(Geometry.TransformProperty, tickText.Transform);
            var bounds = geometry.Bounds;
            var pos = this.PixelPosition(tickText.Value, finalSize);
            if (this.Placement.IsHorizontal())
            {
                switch (this.HorizontalTextAlignment)
                {
                    case HorizontalTextAlignment.Left:
                        tickText.TextAlignment = TextAlignment.Left;
                        break;
                    case HorizontalTextAlignment.Center:
                        tickText.TextAlignment = TextAlignment.Center;
                        break;
                    case HorizontalTextAlignment.Right:
                        tickText.TextAlignment = TextAlignment.Right;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                var y = 0.0;
                switch (this.VerticalTextAlignment)
                {
                    case VerticalTextAlignment.Top:
                        y += tickText.Extent;
                        break;
                    case VerticalTextAlignment.Center:
                        y += tickText.Extent / 2;
                        break;
                    case VerticalTextAlignment.Bottom:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return new Point(pos, y);
            }
            else
            {
                var x = 0.0;
                switch (this.HorizontalTextAlignment)
                {
                    case HorizontalTextAlignment.Left:
                        break;
                    case HorizontalTextAlignment.Center:
                        x += tickText.Width / 2;
                        break;
                    case HorizontalTextAlignment.Right:
                        x += tickText.Width;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                switch (this.VerticalTextAlignment)
                {
                    case VerticalTextAlignment.Top:
                        pos += tickText.Extent;
                        break;
                    case VerticalTextAlignment.Center:
                        pos += tickText.Extent / 2;
                        break;
                    case VerticalTextAlignment.Bottom:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return new Point(x, pos);
            }
        }

        protected virtual TickText CreateTickText(double value)
        {
            return new TickText(
                value,
                this.StringFormat ?? string.Empty,
                this.TypeFace,
                this.FontSize,
                this.Foreground,
                this.TextTransform);
        }

        protected override void UpdateTexts()
        {
            if (this.AllTicks == null || this.AllTicks.Count == 0)
            {
                this.AllTexts = null;
                return;
            }

            this.AllTexts = this.AllTicks.Select(this.CreateTickText).ToArray();
        }
    }
}