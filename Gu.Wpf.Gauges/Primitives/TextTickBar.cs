namespace Gu.Wpf.Gauges
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Media;

    /// <summary>
    /// Base class for a tick bar that renders ticks as text.
    /// </summary>
    public abstract class TextTickBar : TickBarBase
    {
#pragma warning disable SA1202 // Elements must be ordered by access

        public static readonly DependencyProperty PaddingProperty = DependencyProperty.Register(
            nameof(Padding),
            typeof(Thickness),
            typeof(TextTickBar),
            new FrameworkPropertyMetadata(
                default(Thickness),
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));

        private static readonly DependencyPropertyKey OverflowPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(Overflow),
            typeof(Thickness),
            typeof(TextTickBar),
            new PropertyMetadata(default(Thickness)));

        public static readonly DependencyProperty OverflowProperty = OverflowPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey AllTextsPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(AllTexts),
            typeof(IReadOnlyList<TickText>),
            typeof(TextTickBar),
            new FrameworkPropertyMetadata(
                default(IReadOnlyList<TickText>),
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty AllTextsProperty = AllTextsPropertyKey.DependencyProperty;

        public static readonly DependencyProperty TextTransformProperty = DependencyProperty.Register(
            nameof(TextTransform),
            typeof(Transform),
            typeof(TextTickBar),
            new FrameworkPropertyMetadata(
                default(Transform),
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender));

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
                FrameworkPropertyMetadataOptions.Inherits,
                (d, _) => ((TextTickBar)d).UpdateTypeFace()));

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
                FrameworkPropertyMetadataOptions.Inherits,
                (d, _) => ((TextTickBar)d).UpdateTypeFace()));

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
                FrameworkPropertyMetadataOptions.Inherits,
                (d, _) => ((TextTickBar)d).UpdateTypeFace()));

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
                FrameworkPropertyMetadataOptions.Inherits,
                (d, _) => ((TextTickBar)d).UpdateTypeFace()));

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
                FrameworkPropertyMetadataOptions.Inherits,
                (d, _) => ((TextTickBar)d).UpdateTexts()));

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
        public static readonly DependencyProperty StringFormatProperty = DependencyProperty.Register(
            "StringFormat",
            typeof(string),
            typeof(TextTickBar),
            new PropertyMetadata(
                default(string),
                (d, _) => ((TextTickBar)d).UpdateTexts()));

        private Typeface typeFace;
#pragma warning restore SA1202 // Elements must be ordered by access

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

        public IReadOnlyList<TickText> AllTexts
        {
            get => (IReadOnlyList<TickText>)this.GetValue(AllTextsProperty);
            protected set => this.SetValue(AllTextsPropertyKey, value);
        }

        public Transform TextTransform
        {
            get => (Transform)this.GetValue(TextTransformProperty);
            set => this.SetValue(TextTransformProperty, value);
        }

        /// <summary>
        /// Gets or sets the preferred top-level font family for the content of the element.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Media.FontFamily" /> object that specifies the preferred font family, or a primary preferred font family with one or more fallback font families. The default is the font determined by the <see cref="P:System.Windows.SystemFonts.MessageFontFamily" /> value.
        /// </returns>
        public FontFamily FontFamily
        {
            get => (FontFamily)this.GetValue(FontFamilyProperty);
            set => this.SetValue(FontFamilyProperty, value);
        }

        /// <summary>
        /// Gets or sets the font style for the content of the element.
        /// </summary>
        /// <returns>
        /// A member of the <see cref="T:System.Windows.FontStyles" /> class that specifies the desired font style. The default is determined by the <see cref="P:System.Windows.SystemFonts.MessageFontStyle" /> value.
        /// </returns>
        public FontStyle FontStyle
        {
            get => (FontStyle)this.GetValue(FontStyleProperty);
            set => this.SetValue(FontStyleProperty, value);
        }

        /// <summary>
        /// Gets or sets the top-level font weight for the content of the element.
        /// </summary>
        /// <returns>
        /// A member of the <see cref="T:System.Windows.FontWeights" /> class that specifies the desired font weight. The default value is determined by the <see cref="P:System.Windows.SystemFonts.MessageFontWeight" /> value.
        /// </returns>
        public FontWeight FontWeight
        {
            get => (FontWeight)this.GetValue(FontWeightProperty);
            set => this.SetValue(FontWeightProperty, value);
        }

        /// <summary>
        /// Gets or sets the font-stretching characteristics for the content of the element.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.FontStretch" /> structure that specifies the desired font-stretching characteristics to use. The default is <see cref="P:System.Windows.FontStretches.Normal" />.
        /// </returns>
        public FontStretch FontStretch
        {
            get => (FontStretch)this.GetValue(FontStretchProperty);
            set => this.SetValue(FontStretchProperty, value);
        }

        /// <summary>
        /// Gets or sets the font size for the content of the element.
        /// </summary>
        /// <returns>
        /// The desired font size to use in device independent pixels,  greater than 0.001 and less than or equal to 35791.  The default depends on current system settings and depends on the <see cref="P:System.Windows.SystemFonts.MessageFontSize" /> value.
        /// </returns>
        public double FontSize
        {
            get => (double)this.GetValue(FontSizeProperty);
            set => this.SetValue(FontSizeProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="T:System.Windows.Media.Brush" /> to apply to the content of the element.
        /// </summary>
        /// <returns>
        /// The brush used to apply to the text contents. The default is <see cref="P:System.Windows.Media.Brushes.Black" />.
        /// </returns>
        public Brush Foreground
        {
            get => (Brush)this.GetValue(ForegroundProperty);
            set => this.SetValue(ForegroundProperty, value);
        }

        /// <summary>
        /// Gets or sets a collection of text effects to apply to the content of the element.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Media.TextEffectCollection" /> containing one or more <see cref="T:System.Windows.Media.TextEffect" /> objects that define effects to apply to the content in this element. The default is null (not an empty collection).
        /// </returns>
        public TextEffectCollection TextEffects
        {
            get => (TextEffectCollection)this.GetValue(TextEffectsProperty);
            set => this.SetValue(TextEffectsProperty, value);
        }

        /// <summary>
        /// Gets or sets a composite string that specifies how to format the <see cref="P:TextTickBar.ContentStringFormat" /> property if it is displayed as a string.
        /// </summary>
        /// <returns>
        /// A composite string that specifies how to format the <see cref="P:TextTickBar.ContentStringFormat" /> property if it is displayed as a string.
        /// </returns>
        public string StringFormat
        {
            get => (string)this.GetValue(StringFormatProperty);
            set => this.SetValue(StringFormatProperty, value);
        }

        protected Typeface TypeFace => this.typeFace ??
                                       (this.typeFace = new Typeface(
                                           this.FontFamily,
                                           this.FontStyle,
                                           this.FontWeight,
                                           this.FontStretch));

        protected void UpdateTypeFace()
        {
            this.typeFace = null;
            this.UpdateTexts();
        }

        protected override void UpdateTicks()
        {
            base.UpdateTicks();
            this.UpdateTexts();
        }

        protected abstract void UpdateTexts();
    }
}