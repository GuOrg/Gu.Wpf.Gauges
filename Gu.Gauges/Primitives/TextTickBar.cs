namespace Gu.Gauges
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Documents;
    using System.Windows.Media;

    /// <summary>
    /// http://stackoverflow.com/a/3578214/1069200
    /// </summary>
    public class TextTickBar : Bar, ITextFormat
    {
        public static readonly DependencyProperty TextOrientationProperty = DependencyProperty.Register(
            "TextOrientation",
            typeof(TextOrientation),
            typeof(TextTickBar),
            new FrameworkPropertyMetadata(
                TextOrientation.Tangential,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Identifies the <see cref="P:TextTickBar.FontFamily" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:TextTickBar.FontFamily" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty FontFamilyProperty = TextElement.FontFamilyProperty.AddOwner(
            typeof(TextTickBar),
            new FrameworkPropertyMetadata(
               new FontFamily("Segoe UI"),
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:TextTickBar.FontStyle" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:TextTickBar.FontStyle" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty FontStyleProperty = TextElement.FontStyleProperty.AddOwner(
            typeof(TextTickBar),
            new FrameworkPropertyMetadata(
                FontStyles.Normal,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:TextTickBar.FontWeight" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:TextTickBar.FontWeight" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty FontWeightProperty = TextElement.FontWeightProperty.AddOwner(
            typeof(TextTickBar),
            new FrameworkPropertyMetadata(
                FontWeights.Normal,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:TextTickBar.FontStretch" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:TextTickBar.FontStretch" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty FontStretchProperty = TextElement.FontStretchProperty.AddOwner(
            typeof(TextTickBar),
            new FrameworkPropertyMetadata(
                FontStretches.Normal,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:TextTickBar.FontSize" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:TextTickBar.FontSize" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty FontSizeProperty = TextElement.FontSizeProperty.AddOwner(
            typeof(TextTickBar),
            new FrameworkPropertyMetadata(
                12.0,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:TextTickBar.Foreground" /> dependency property. 
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:TextTickBar.Foreground" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty ForegroundProperty = TextElement.ForegroundProperty.AddOwner(
            typeof(TextTickBar),
            new FrameworkPropertyMetadata(
               Brushes.Black,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        /// <summary>
        /// Identifies the <see cref="P:TextTickBar.TextEffects" /> dependency property. 
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:TextTickBar.TextEffects" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty TextEffectsProperty = TextElement.TextEffectsProperty.AddOwner(
            typeof(TextTickBar),
            new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="P:TextTickBar.ContentStringFormat" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:TextTickBar.ContentStringFormat" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty ContentStringFormatProperty = ContentControl.ContentStringFormatProperty.AddOwner(
            typeof(TextTickBar),
            new FrameworkPropertyMetadata(
                default(String),
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        private static readonly DependencyPropertyKey TextSpacePropertyKey = DependencyProperty.RegisterReadOnly(
            "TextSpace",
            typeof(double),
            typeof(TextTickBar),
            new PropertyMetadata(default(double)));

        public static readonly DependencyProperty TextSpaceProperty = TextSpacePropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey TextSpaceMarginPropertyKey = DependencyProperty.RegisterReadOnly(
            "TextSpaceMargin",
            typeof(Thickness),
            typeof(TextTickBar),
            new PropertyMetadata(default(Thickness)));

        public static readonly DependencyProperty TextSpaceMarginProperty = TextSpaceMarginPropertyKey.DependencyProperty;

        protected FormattedText[] AllTexts { get; private set; }

        /// <summary>
        /// Gets or sets the <see cref="T:Gu.Gauges.TextOrientation" />
        /// Default is Tangential
        /// </summary>
        public TextOrientation TextOrientation
        {
            get { return (TextOrientation)this.GetValue(TextOrientationProperty); }
            set { this.SetValue(TextOrientationProperty, value); }
        }

        /// <summary>
        /// Gets or sets the preferred top-level font family for the content of the element.  
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Media.FontFamily" /> object that specifies the preferred font family, or a primary preferred font family with one or more fallback font families. The default is the font determined by the <see cref="P:System.Windows.SystemFonts.MessageFontFamily" /> value.
        /// </returns>
        public FontFamily FontFamily
        {
            get { return (FontFamily)this.GetValue(FontFamilyProperty); }
            set { this.SetValue(FontFamilyProperty, value); }
        }

        /// <summary>
        /// Gets or sets the font style for the content of the element.  
        /// </summary>
        /// <returns>
        /// A member of the <see cref="T:System.Windows.FontStyles" /> class that specifies the desired font style. The default is determined by the <see cref="P:System.Windows.SystemFonts.MessageFontStyle" /> value.
        /// </returns>
        public FontStyle FontStyle
        {
            get { return (FontStyle)this.GetValue(FontStyleProperty); }
            set { this.SetValue(FontStyleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the top-level font weight for the content of the element.  
        /// </summary>
        /// <returns>
        /// A member of the <see cref="T:System.Windows.FontWeights" /> class that specifies the desired font weight. The default value is determined by the <see cref="P:System.Windows.SystemFonts.MessageFontWeight" /> value.
        /// </returns>
        public FontWeight FontWeight
        {
            get { return (FontWeight)this.GetValue(FontWeightProperty); }
            set { this.SetValue(FontWeightProperty, value); }
        }

        /// <summary>
        /// Gets or sets the font-stretching characteristics for the content of the element.  
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.FontStretch" /> structure that specifies the desired font-stretching characteristics to use. The default is <see cref="P:System.Windows.FontStretches.Normal" />.
        /// </returns>
        public FontStretch FontStretch
        {
            get { return (FontStretch)this.GetValue(FontStretchProperty); }
            set { this.SetValue(FontStretchProperty, value); }
        }

        /// <summary>
        /// Gets or sets the font size for the content of the element.  
        /// </summary>
        /// <returns>
        /// The desired font size to use in device independent pixels,  greater than 0.001 and less than or equal to 35791.  The default depends on current system settings and depends on the <see cref="P:System.Windows.SystemFonts.MessageFontSize" /> value.
        /// </returns>
        public double FontSize
        {
            get { return (double)this.GetValue(FontSizeProperty); }
            set { this.SetValue(FontSizeProperty, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="T:System.Windows.Media.Brush" /> to apply to the content of the element.  
        /// </summary>
        /// <returns>
        /// The brush used to apply to the text contents. The default is <see cref="P:System.Windows.Media.Brushes.Black" />.
        /// </returns>
        public Brush Foreground
        {
            get { return (Brush)this.GetValue(ForegroundProperty); }
            set { this.SetValue(ForegroundProperty, value); }
        }

        /// <summary>
        /// Gets or sets a collection of text effects to apply to the content of the element.  
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Media.TextEffectCollection" /> containing one or more <see cref="T:System.Windows.Media.TextEffect" /> objects that define effects to apply to the content in this element. The default is null (not an empty collection).
        /// </returns>
        public TextEffectCollection TextEffects
        {
            get { return (TextEffectCollection)this.GetValue(TextEffectsProperty); }
            set { this.SetValue(TextEffectsProperty, value); }
        }

        /// <summary>
        /// Gets or sets a composite string that specifies how to format the <see cref="P:TextTickBar.ContentStringFormat" /> property if it is displayed as a string.
        /// </summary>
        /// <returns>
        /// A composite string that specifies how to format the <see cref="P:TextTickBar.ContentStringFormat" /> property if it is displayed as a string.
        /// </returns>
        public string ContentStringFormat
        {
            get { return (string)this.GetValue(ContentStringFormatProperty); }
            set { this.SetValue(ContentStringFormatProperty, value); }
        }

        /// <summary>
        /// Gets the Reserved space due to the text
        /// </summary>
        public double TextSpace
        {
            get { return (double)this.GetValue(TextSpaceProperty); }
            protected set { this.SetValue(TextSpacePropertyKey, value); }
        }

        public Thickness TextSpaceMargin
        {
            get { return (Thickness)this.GetValue(TextSpaceMarginProperty); }
            protected set { this.SetValue(TextSpaceMarginPropertyKey, value); }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (this.AllTexts == null || !this.AllTexts.Any())
            {
                return new Size(0, 0);
            }
            var textHeight = Math.Ceiling(this.FontSize * this.FontFamily.LineSpacing);

            double w = 0;
            double h = 0;
            switch (this.TextOrientation)
            {

                case TextOrientation.VerticalUp:
                case TextOrientation.VerticalDown:
                    w = textHeight;
                    h = this.AllTexts.Max(t => t.Width);
                    this.TextSpace = textHeight;
                    break;
                case TextOrientation.Horizontal:
                case TextOrientation.Tangential:
                case TextOrientation.RadialOut:
                    w = this.AllTexts.Max(x => x.Width);
                    h = textHeight;
                    this.TextSpace = w;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            var margin = this.TextSpace / 2;
            switch (this.Placement)
            {
                case TickBarPlacement.Left:
                case TickBarPlacement.Right:
                    this.TextSpaceMargin = new Thickness(0, margin, 0, margin);
                    break;
                case TickBarPlacement.Top:
                case TickBarPlacement.Bottom:
                    this.TextSpaceMargin = new Thickness(margin, 0, margin, 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            var size = new Size(w, h);
            if (size.IsInvalid())
            {
                return new Size(0, 0);
            }
            return size;
        }

        protected override void OnRender(DrawingContext dc)
        {
            if (this.Foreground == null)
            {
                return;
            }
            var line = new Line(this.ActualWidth, this.ActualHeight, this.ReservedSpace, this.Placement, this.IsDirectionReversed);
            for (int i = 0; i < this.AllTicks.Count; i++)
            {
                var tick = this.AllTicks[i];
                var pos = TickHelper.ToPos(tick, this.Minimum, this.Maximum, line);
                var text = this.AllTexts[i];
                var textPosition = new TextPosition(text, this.Placement, this.TextOrientation, pos, 0);
                dc.DrawText(text, textPosition);
            }
        }

        protected override void OnTicksChanged()
        {
            base.OnTicksChanged();
            var typeFace = this.TypeFace();
            this.AllTexts = this.AllTicks.Select(x => TextHelper.AsFormattedText(x, this, typeFace))
                                         .ToArray();
        }
    }
}