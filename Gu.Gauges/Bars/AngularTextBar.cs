using System.Windows.Controls.Primitives;

namespace Gu.Gauges
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Media;

    public class AngularTextBar : AngularBar, ITextFormat
    {
        public static readonly DependencyProperty TextOrientationProperty = DependencyProperty.Register(
            "TextOrientation",
            typeof(TextOrientation),
            typeof(AngularTextBar),
            new FrameworkPropertyMetadata(
                TextOrientation.Tangential,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Identifies the <see cref="P:AngularTextBar.FontFamily" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:AngularTextBar.FontFamily" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty FontFamilyProperty = TextElement.FontFamilyProperty.AddOwner(
            typeof(AngularTextBar),
            new FrameworkPropertyMetadata(
               new FontFamily("Segoe UI"),
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:AngularTextBar.FontStyle" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:AngularTextBar.FontStyle" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty FontStyleProperty = TextElement.FontStyleProperty.AddOwner(
            typeof(AngularTextBar),
            new FrameworkPropertyMetadata(
                FontStyles.Normal,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:AngularTextBar.FontWeight" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:AngularTextBar.FontWeight" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty FontWeightProperty = TextElement.FontWeightProperty.AddOwner(
            typeof(AngularTextBar),
            new FrameworkPropertyMetadata(
                FontWeights.Normal,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:AngularTextBar.FontStretch" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:AngularTextBar.FontStretch" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty FontStretchProperty = TextElement.FontStretchProperty.AddOwner(
            typeof(AngularTextBar),
            new FrameworkPropertyMetadata(
                FontStretches.Normal,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:AngularTextBar.FontSize" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:AngularTextBar.FontSize" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty FontSizeProperty = TextElement.FontSizeProperty.AddOwner(
            typeof(AngularTextBar),
            new FrameworkPropertyMetadata(
                12.0,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:AngularTextBar.Foreground" /> dependency property. 
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:AngularTextBar.Foreground" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty ForegroundProperty = TextElement.ForegroundProperty.AddOwner(
            typeof(AngularTextBar),
            new FrameworkPropertyMetadata(
               Brushes.Black,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender));

        /// <summary>
        /// Identifies the <see cref="P:AngularTextBar.TextEffects" /> dependency property. 
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:AngularTextBar.TextEffects" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty TextEffectsProperty = TextElement.TextEffectsProperty.AddOwner(
            typeof(AngularTextBar),
            new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="P:AngularTextBar.ContentStringFormat" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:AngularTextBar.ContentStringFormat" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty ContentStringFormatProperty = ContentControl.ContentStringFormatProperty.AddOwner(
            typeof(AngularTextBar),
            new FrameworkPropertyMetadata(
                default(String),
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

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
        /// Gets or sets a composite string that specifies how to format the <see cref="P:AngularTextBar.Content" /> property if it is displayed as a string.
        /// </summary>
        /// <returns>
        /// A composite string that specifies how to format the <see cref="P:AngularTextBar.Content" /> property if it is displayed as a string.
        /// </returns>
        public string ContentStringFormat
        {
            get { return (string)this.GetValue(ContentStringFormatProperty); }
            set { this.SetValue(ContentStringFormatProperty, value); }
        }

        protected override void OnRender(DrawingContext dc)
        {
            var midPoint = new Point(this.ActualWidth / 2, this.ActualHeight / 2);
            var p = new Point(midPoint.X, midPoint.Y - this.ActualHeight / 2 - this.ReservedSpace / 2);
            var arc = new Arc(midPoint, this.MinAngle, this.MaxAngle, this.ActualWidth - this.ReservedSpace, this.IsDirectionReversed);
            var ticks = TickHelper.CreateTicks(this.Minimum, this.Maximum, this.TickFrequency).Concat(this.Ticks ?? Enumerable.Empty<double>());

            foreach (var tick in ticks)
            {
                if (tick < this.Minimum || tick > this.Maximum)
                {
                    continue;
                }
                var angle = TickHelper.ToAngle(tick, this.Minimum, this.Maximum, arc);
                var text = TextHelper.AsFormattedText(tick, this);
                var textPosition = new TextPosition(text, TickBarPlacement.Top, this.TextOrientation, arc.GetPoint(angle), angle);
                dc.DrawText(text, textPosition);
            }
            this.Diameter = this.ActualWidth - this.ReservedSpace;
        }
    }
}