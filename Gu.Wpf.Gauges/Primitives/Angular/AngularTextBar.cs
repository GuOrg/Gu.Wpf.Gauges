namespace Gu.Wpf.Gauges
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;

    public class AngularTextBar : TextTickBar
    {
        public static readonly DependencyProperty TextOrientationProperty = AngularGauge.TextOrientationProperty.AddOwner(
            typeof(AngularTextBar),
            new FrameworkPropertyMetadata(
                Defaults.TextOrientation,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty TextPositionProperty = DependencyProperty.Register(
            nameof(TextPosition),
            typeof(AngularTextPosition),
            typeof(AngularTextBar),
            new FrameworkPropertyMetadata(
                AngularTextPosition.Default,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender,
                OnTextPositionChanged,
                CoerceTextPosition));

        public static readonly DependencyProperty StartProperty = AngularGauge.StartProperty.AddOwner(
            typeof(AngularTextBar),
            new FrameworkPropertyMetadata(
                Angle.DefaultStart,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty EndProperty = AngularGauge.EndProperty.AddOwner(
            typeof(AngularTextBar),
            new FrameworkPropertyMetadata(
                Angle.DefaultEnd,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Gets or sets the <see cref="T:Gu.Wpf.Gauges.TextOrientation" />
        /// Default is Tangential
        /// </summary>
        public TextOrientation TextOrientation
        {
            get => (TextOrientation)this.GetValue(TextOrientationProperty);
            set => this.SetValue(TextOrientationProperty, value);
        }

        /// <summary>
        /// Controls how each tick is arranged.
        /// </summary>
        public AngularTextPosition TextPosition
        {
            get => (AngularTextPosition)this.GetValue(TextPositionProperty);
            set => this.SetValue(TextPositionProperty, value);
        }

        /// <summary>
        /// Gets or sets the start angle of the arc.
        /// Degrees clockwise from the y axis.
        /// The default is -140
        /// </summary>
        public Angle Start
        {
            get => (Angle)this.GetValue(StartProperty);
            set => this.SetValue(StartProperty, value);
        }

        /// <summary>
        /// Gets or sets the end angle of the arc.
        /// Degrees clockwise from the y axis.
        /// The default is 140
        /// </summary>
        public Angle End
        {
            get => (Angle)this.GetValue(EndProperty);
            set => this.SetValue(EndProperty, value);
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

            return rect.Inflate(this.Padding).Size;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this.AllTexts != null)
            {
                var overflow = default(Thickness);
                var arc = ArcInfo.Fit(finalSize, this.Padding, this.Start, this.End);
                foreach (var tickText in this.AllTexts)
                {
                    overflow = overflow.Union(this.TextPosition.ArrangeTick(tickText, arc, this));
                }

                this.Overflow = new Thickness(
                    left: RoundUp(overflow.Left),
                    top: RoundUp(overflow.Top),
                    right: RoundUp(overflow.Right),
                    bottom: RoundUp(overflow.Bottom));
            }
            else
            {
                this.Overflow = default(Thickness);
            }

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
                null);
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
            var textBar = (AngularTextBar)d;
            if (e.OldValue is AngularTextPosition oldValue)
            {
                oldValue.ArrangeDirty -= textBar.OnTextPositionArrange;
            }

            if (e.NewValue is AngularTextPosition newValue)
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