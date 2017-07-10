namespace Gu.Wpf.Gauges
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    /// <summary>
    /// Linear tick bar that renders ticks as text.
    /// </summary>
    public class LinearTextBar : TextTickBar
    {
        /// <summary>
        /// Identifies the <see cref="P:LinearTextBar.Placement" /> dependency property. This property is read-only.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:Bar.Placement" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty PlacementProperty = LinearGauge.PlacementProperty.AddOwner(
            typeof(LinearTextBar),
            new FrameworkPropertyMetadata(
                TickBarPlacement.Bottom,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty PaddingProperty =
            DependencyProperty.Register(
                nameof(Padding),
                typeof(Thickness),
                typeof(LinearTextBar),
                new FrameworkPropertyMetadata(
                    default(Thickness),
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Gets or sets where tick marks appear  relative to a <see cref="T:System.Windows.Controls.Primitives.Track" /> of a <see cref="T:System.Windows.Controls.Slider" /> control.
        /// </summary>
        /// <returns>
        /// A <see cref="T:TickBarPlacement" /> enumeration value that identifies the position of the <see cref="T:LinearTextBar" /> in the <see cref="T:System.Windows.Style" /> layout of a <see cref="T:System.Windows.Controls.Slider" />. The default value is <see cref="F:Bar.Top" />.
        /// </returns>
        public TickBarPlacement Placement
        {
            get => (TickBarPlacement)this.GetValue(PlacementProperty);
            set => this.SetValue(PlacementProperty, value);
        }

        public Thickness Padding
        {
            get => (Thickness)this.GetValue(PaddingProperty);
            set => this.SetValue(PaddingProperty, value);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var rect = default(Rect);
            if (this.AllTexts != null)
            {
                foreach (var tickText in this.AllTexts)
                {
                    rect.Union(new Rect(tickText.Geometry.Bounds.Size));
                }
            }

            rect.Inflate(this.Padding.Left + this.Padding.Right, this.Padding.Top + this.Padding.Bottom);
            return rect.Size;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var rect = default(Rect);
            if (this.AllTexts != null)
            {
                foreach (var tickText in this.AllTexts)
                {
                    tickText.TranslateTransform.SetCurrentValue(TranslateTransform.XProperty, 0.0d);
                    tickText.TranslateTransform.SetCurrentValue(TranslateTransform.YProperty, 0.0d);
                    var pos = this.PixelPosition(tickText, finalSize);
                    tickText.TranslateTransform.SetCurrentValue(TranslateTransform.XProperty, pos.X);
                    tickText.TranslateTransform.SetCurrentValue(TranslateTransform.YProperty, pos.Y);
                    rect.Union(tickText.Geometry.Bounds);
                }
            }

            this.Overflow = new Thickness(
                Math.Max(0, -rect.Left),
                Math.Max(0, -rect.Top),
                Math.Max(0, rect.Right - finalSize.Width),
                Math.Max(0, rect.Bottom - finalSize.Height));
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
                dc.DrawGeometry(this.Foreground, null, tickText.Geometry);
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
            var step = Interpolate.Linear(this.Minimum, this.Maximum, value)
                                  .Clamp(0, 1);

            if (this.Placement.IsHorizontal())
            {
                var pos = step.Interpolate(this.Padding.Left, finalSize.Width - this.Padding.Right);
                return this.IsDirectionReversed
                    ? finalSize.Width - pos
                    : pos;
            }
            else
            {
                var pos = step.Interpolate(this.Padding.Bottom, finalSize.Height - this.Padding.Top);
                return this.IsDirectionReversed
                    ? pos
                    : finalSize.Height - pos;
            }
        }

        protected virtual Point PixelPosition(TickText tickText, Size finalSize)
        {
            var pos = this.PixelPosition(tickText.Value, finalSize);
            var bounds = tickText.Geometry.Bounds;
            if (this.Placement.IsHorizontal())
            {
                var x = -bounds.Left;
                switch (this.HorizontalTextAlignment)
                {
                    case HorizontalTextAlignment.Left:
                        x += pos;
                        break;
                    case HorizontalTextAlignment.Center:
                        x += pos - (bounds.Width / 2);
                        break;
                    case HorizontalTextAlignment.Right:
                        x += pos - bounds.Width;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                var y = -bounds.Top;
                switch (this.VerticalTextAlignment)
                {
                    case VerticalTextAlignment.Top:
                        y += this.Padding.Top;
                        break;
                    case VerticalTextAlignment.Center:
                        y += (finalSize.Height - bounds.Height) / 2;
                        break;
                    case VerticalTextAlignment.Bottom:
                        y += finalSize.Height - bounds.Height - this.Padding.Bottom;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return new Point(x, y);
            }
            else
            {
                var x = -bounds.Left;
                switch (this.HorizontalTextAlignment)
                {
                    case HorizontalTextAlignment.Left:
                        break;
                    case HorizontalTextAlignment.Center:
                        x += (finalSize.Width - bounds.Width) / 2;
                        break;
                    case HorizontalTextAlignment.Right:
                        x += finalSize.Width - bounds.Width;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                var y = -bounds.Top;
                switch (this.VerticalTextAlignment)
                {
                    case VerticalTextAlignment.Top:
                        y += pos;
                        break;
                    case VerticalTextAlignment.Center:
                        y += pos - (bounds.Height / 2);
                        break;
                    case VerticalTextAlignment.Bottom:
                        y += pos - bounds.Height;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return new Point(x, y);
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