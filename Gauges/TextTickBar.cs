namespace Gauges
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Documents;
    using System.Windows.Media;

    /// <summary>
    ///     http://stackoverflow.com/a/3578214/1069200
    /// </summary>
    public class TextTickBar : TickBar
    {
        public static readonly DependencyProperty FontSizeProperty = Control.FontSizeProperty.AddOwner(
            typeof(TextTickBar),
            new FrameworkPropertyMetadata(
                SystemFonts.MessageFontSize,
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty ContentStringFormatProperty = ContentControl
            .ContentStringFormatProperty.AddOwner(
                typeof(TextTickBar),
                new FrameworkPropertyMetadata(
                    "F0",
                    FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty FontFamilyProperty = Control.FontFamilyProperty.AddOwner(
            typeof(TextTickBar),
            new FrameworkPropertyMetadata(
                SystemFonts.MessageFontFamily,
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty FontStyleProperty = Control.FontStyleProperty.AddOwner(
            typeof(TextTickBar),
            new FrameworkPropertyMetadata(
                SystemFonts.MessageFontStyle,
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty FontWeightProperty = Control.FontWeightProperty.AddOwner(
            typeof(TextTickBar),
            new FrameworkPropertyMetadata(
                SystemFonts.MessageFontWeight,
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty FontStretchProperty = Control.FontStretchProperty.AddOwner(
            typeof(TextTickBar),
            new FrameworkPropertyMetadata(
                TextElement.FontStretchProperty.DefaultMetadata.DefaultValue,
                FrameworkPropertyMetadataOptions.AffectsRender));

        static TextTickBar()
        {
            TickBar.TickFrequencyProperty.OverrideMetadata(typeof(TextTickBar), new FrameworkPropertyMetadata(-1.0, FrameworkPropertyMetadataOptions.AffectsRender));
        }

        public double FontSize
        {
            get { return (double)this.GetValue(FontSizeProperty); }
            set { this.SetValue(FontSizeProperty, value); }
        }

        public string ContentStringFormat
        {
            get { return (string)this.GetValue(ContentStringFormatProperty); }
            set { this.SetValue(ContentStringFormatProperty, value); }
        }

        public FontFamily FontFamily
        {
            get { return (FontFamily)this.GetValue(FontFamilyProperty); }
            set { this.SetValue(FontFamilyProperty, value); }
        }

        public FontStyle FontStyle
        {
            get { return (FontStyle)this.GetValue(FontStyleProperty); }
            set { this.SetValue(FontStyleProperty, value); }
        }

        public FontWeight FontWeight
        {
            get { return (FontWeight)this.GetValue(FontWeightProperty); }
            set { this.SetValue(FontWeightProperty, value); }
        }

        public FontStretch FontStretch
        {
            get { return (FontStretch)this.GetValue(FontStretchProperty); }
            set { this.SetValue(FontStretchProperty, value); }
        }

        protected override void OnRender(DrawingContext dc)
        {
            IEnumerable<TextTick> textTicks = TextTicks(this.TickFrequency).Concat(this.TextTicks(this.Ticks))
                                                                           .ToArray();
            foreach (var textTick in textTicks)
            {
                var formattedText = new FormattedText(
                    textTick.Text,
                    CultureInfo.CurrentUICulture,
                    FlowDirection.LeftToRight,
                    new Typeface(
                        this.FontFamily,
                        this.FontStyle,
                        this.FontWeight,
                        this.FontStretch),
                    this.FontSize,
                    this.Fill);
                var offsetX = 0.0;
                var offsetY = 0.0;
                switch (this.Placement)
                {
                    case TickBarPlacement.Left:
                        offsetX = 0;
                        offsetY = -1 * formattedText.Height / 2;
                        break;
                    case TickBarPlacement.Top:
                        offsetX = -1 * formattedText.Width / 2;
                        offsetY = -1 * formattedText.Height;
                        break;
                    case TickBarPlacement.Right:
                        offsetX = -1 * formattedText.Width;
                        offsetY = -1 * formattedText.Height / 2;
                        break;
                    case TickBarPlacement.Bottom:
                        offsetX = -1 * formattedText.Width / 2;
                        offsetY = 0;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                dc.DrawText(formattedText, new Point(textTick.ScreenX + offsetX, textTick.ScreenY + offsetY));
            }
        }

        private IEnumerable<TextTick> TextTicks(double tickFrequency)
        {
            if (tickFrequency < 0)
            {
                yield break;
            }
            double range = this.Maximum - this.Minimum;
            double tickWidth = range * tickFrequency / 100;
            var value = this.Minimum;
            while (value < (Maximum + tickWidth / 2))
            {
                double screenX = this.ValueToScreenX(value);
                double screenY = this.ValueToScreenY(value);
                yield return new TextTick(screenX, screenY, value.ToString(this.ContentStringFormat));
                value += tickWidth;
            }
        }
        private IEnumerable<TextTick> TextTicks(IEnumerable<double> values)
        {
            return values.Select(value => new TextTick(this.ValueToScreenX(value), this.ValueToScreenY(value), value.ToString(this.ContentStringFormat, CultureInfo.CurrentUICulture)));
        }

        private double ValueToScreenX(double value)
        {
            if (this.Placement == TickBarPlacement.Left)
            {
                return 0;
            }
            if (this.Placement == TickBarPlacement.Right)
            {
                return this.ActualWidth;
            }
            double range = this.Maximum - this.Minimum;
            return ((value - this.Minimum) / range) * this.ActualWidth;
        }

        private double ValueToScreenY(double value)
        {
            if (this.Placement == TickBarPlacement.Bottom)
            {
                return 0;
            }
            if (this.Placement == TickBarPlacement.Top)
            {
                return this.ActualHeight;
            }
            double range = this.Maximum - this.Minimum;
            return ((value - this.Minimum) / range) * this.ActualHeight;
        }

        public class TextTick
        {
            public TextTick(double screenX, double screenY, string text)
            {
                this.ScreenX = screenX;
                this.ScreenY = screenY;
                this.Text = text;
            }

            public double ScreenX { get; private set; }

            public double ScreenY { get; private set; }

            public string Text { get; private set; }

            public override string ToString()
            {
                return string.Format("ScreenX: {0}, ScreenY: {1}, Text: {2}", this.ScreenX, this.ScreenY, this.Text);
            }
        }
    }
}