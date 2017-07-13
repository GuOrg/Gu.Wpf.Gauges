namespace Gu.Wpf.Gauges
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using Gu.Wpf.Gauges.Primitives.Linear;

    /// <summary>
    /// Linear tick bar that renders ticks as text.
    /// </summary>
    public class LinearTextBar : TextTickBar
    {
#pragma warning disable SA1202 // Elements must be ordered by access
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

        public static readonly DependencyProperty TextPositionProperty = DependencyProperty.Register(
            nameof(TextPosition),
            typeof(LinearTextPosition),
            typeof(LinearTextBar),
            new FrameworkPropertyMetadata(
                LinearTextPosition.Default,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender,
                OnTextPositionChanged,
                CoerceTextPosition));

        public static readonly DependencyProperty PaddingProperty = DependencyProperty.Register(
            nameof(Padding),
            typeof(Thickness),
            typeof(LinearTextBar),
            new FrameworkPropertyMetadata(
                default(Thickness),
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));

        private static readonly DependencyPropertyKey OverflowPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(Overflow),
            typeof(Thickness),
            typeof(LinearTextBar),
            new PropertyMetadata(default(Thickness)));

        public static readonly DependencyProperty OverflowProperty = OverflowPropertyKey.DependencyProperty;
#pragma warning restore SA1202 // Elements must be ordered by access

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

        /// <summary>
        /// Controls how each tick is arranged.
        /// </summary>
        public LinearTextPosition TextPosition
        {
            get => (LinearTextPosition)this.GetValue(TextPositionProperty);
            set => this.SetValue(TextPositionProperty, value);
        }

        public Thickness Padding
        {
            get => (Thickness)this.GetValue(PaddingProperty);
            set => this.SetValue(PaddingProperty, value);
        }

        /// <summary>
        /// Gets a <see cref="Thickness"/> with values indicating how much the control draws outside its bounds.
        /// </summary>
        public Thickness Overflow
        {
            get => (Thickness)this.GetValue(OverflowProperty);
            protected set => this.SetValue(OverflowPropertyKey, value);
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
                    this.TextPosition.ArrangeTick(tickText, finalSize, this);
                    rect.Union(tickText.Geometry.Bounds);
                }
            }

            this.Overflow = this.Placement.IsHorizontal()
                ? new Thickness(Math.Max(0, RoundUp(-rect.Left)), 0, Math.Max(0, RoundUp(rect.Right - finalSize.Width)), 0)
                : new Thickness(0, Math.Max(0, RoundUp(-rect.Top)), 0, Math.Max(0, RoundUp(rect.Bottom - finalSize.Height)));
            this.RegisterOverflow(this.Overflow);
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

        private static void OnTextPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var textBar = (LinearTextBar)d;
            if (e.OldValue is LinearTextPosition oldValue)
            {
                oldValue.ArrangeDirty -= textBar.OnTextPositionArrange;
            }

            if (e.NewValue is LinearTextPosition newValue)
            {
                newValue.ArrangeDirty += textBar.OnTextPositionArrange;
            }
        }

        private static object CoerceTextPosition(DependencyObject d, object basevalue)
        {
            return basevalue ?? LinearTextPosition.Default;
        }

        private static double RoundUp(double value)
        {
            var n = Math.Ceiling(value / 0.5);
            return n * 0.5;
        }

        private void OnTextPositionArrange(object sender, EventArgs e)
        {
            this.InvalidateArrange();
        }
    }
}