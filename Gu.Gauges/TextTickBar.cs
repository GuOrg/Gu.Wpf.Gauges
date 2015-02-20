using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace Gu.Gauges
{
    /// <summary>
    ///     http://stackoverflow.com/a/3578214/1069200
    /// </summary>
    public class TextTickBar : TickBar
    {
        public static readonly DependencyProperty FontSizeProperty = Control.FontSizeProperty.AddOwner(
            typeof(TextTickBar),
            new FrameworkPropertyMetadata(
                SystemFonts.MessageFontSize,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty ContentStringFormatProperty = ContentControl.ContentStringFormatProperty.AddOwner(
                typeof(TextTickBar),
                new FrameworkPropertyMetadata(
                    "F0",
                    FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty FontFamilyProperty = Control.FontFamilyProperty.AddOwner(
            typeof(TextTickBar),
            new FrameworkPropertyMetadata(
                SystemFonts.MessageFontFamily,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

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
            TickFrequencyProperty.OverrideMetadata(
                typeof(TextTickBar),
                new FrameworkPropertyMetadata(
                    -1.0,
                    FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

            PlacementProperty.OverrideMetadata(
                typeof(TextTickBar),
                new FrameworkPropertyMetadata(
                    TickBarPlacement.Left,
                    FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
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

        protected override Size MeasureOverride(Size availableSize)
        {
            if (this.TickFrequency < 0 && !this.Ticks.Any())
            {
                return new Size(0, 0);
            }
            double w;
            double h;
            if (this.Placement == TickBarPlacement.Bottom || this.Placement == TickBarPlacement.Top)
            {
                w = availableSize.Width;
                h = Math.Ceiling(this.FontSize * this.FontFamily.LineSpacing);
            }
            else
            {
                w = this.TextTicks(this.TickFrequency)
                              .Concat(this.TextTicks(this.Ticks))
                              .Select(x => this.ToFormattedText(x.Value))
                              .Max(t => t.Width);
                h = availableSize.Height;
            }

            return new Size(w, h);
        }

        protected override void OnRender(DrawingContext dc)
        {
            IEnumerable<TextTick> textTicks = this.TextTicks(this.TickFrequency).Concat(this.TextTicks(this.Ticks))
                                                                           .ToArray();
            foreach (var textTick in textTicks)
            {
                var formattedText = this.ToFormattedText(textTick.Value);
                var point = new Point(textTick.ScreenX, textTick.ScreenY) + this.GetDrawOffset(formattedText);

                dc.DrawText(formattedText, point);
            }
        }

        private Vector GetDrawOffset(FormattedText formattedText)
        {
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
            return new Vector(offsetX, offsetY);
        }

        private FormattedText ToFormattedText(double value)
        {
            var formattedText = new FormattedText(
                value.ToString(this.ContentStringFormat, CultureInfo.CurrentUICulture),
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface(
                    this.FontFamily,
                    this.FontStyle,
                    this.FontWeight,
                    this.FontStretch),
                this.FontSize,
                this.Fill);
            return formattedText;
        }

        private IEnumerable<TextTick> TextTicks(double tickFrequency)
        {
            if (tickFrequency < 0)
            {
                yield break;
            }
            double range = this.Maximum - this.Minimum;
            double tickWidth = tickFrequency;
            var value = this.Minimum;
            while (value < (this.Maximum + tickWidth / 2))
            {
                double screenX = this.ValueToScreenX(value);
                double screenY = this.ValueToScreenY(value);
                yield return new TextTick(screenX, screenY, value);
                value += tickWidth;
            }
        }

        private IEnumerable<TextTick> TextTicks(IEnumerable<double> values)
        {
            return values.Select(value => new TextTick(this.ValueToScreenX(value), this.ValueToScreenY(value), value));
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
            return (1 - ((value - this.Minimum) / range)) * this.ActualHeight;
        }

        public class TextTick
        {
            public TextTick(double screenX, double screenY, double value)
            {
                this.ScreenX = screenX;
                this.ScreenY = screenY;
                this.Value = value;
            }

            public double ScreenX { get; private set; }

            public double ScreenY { get; private set; }

            public double Value { get; private set; }

            public override string ToString()
            {
                return string.Format("ScreenX: {0}, ScreenY: {1}, Value: {2}", this.ScreenX, this.ScreenY, this.Value);
            }
        }
    }
}