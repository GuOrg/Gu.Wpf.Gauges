namespace Gu.Wpf.Gauges
{
    using System.Windows;
    using System.Windows.Media;

    public interface ITextFormat
    {
        /// <summary>
        /// Gets or sets the preferred top-level font family for the content of the element.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Media.FontFamily" /> object that specifies the preferred font family, or a primary preferred font family with one or more fallback font families. The default is the font determined by the <see cref="P:System.Windows.SystemFonts.MessageFontFamily" /> value.
        /// </returns>
        FontFamily FontFamily { get; }

        /// <summary>
        /// Gets or sets the font style for the content of the element.
        /// </summary>
        /// <returns>
        /// A member of the <see cref="T:System.Windows.FontStyles" /> class that specifies the desired font style. The default is determined by the <see cref="P:System.Windows.SystemFonts.MessageFontStyle" /> value.
        /// </returns>
        FontStyle FontStyle { get; }

        /// <summary>
        /// Gets or sets the top-level font weight for the content of the element.
        /// </summary>
        /// <returns>
        /// A member of the <see cref="T:System.Windows.FontWeights" /> class that specifies the desired font weight. The default value is determined by the <see cref="P:System.Windows.SystemFonts.MessageFontWeight" /> value.
        /// </returns>
        FontWeight FontWeight { get; }

        /// <summary>
        /// Gets or sets the font-stretching characteristics for the content of the element.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.FontStretch" /> structure that specifies the desired font-stretching characteristics to use. The default is <see cref="P:System.Windows.FontStretches.Normal" />.
        /// </returns>
        FontStretch FontStretch { get; }

        /// <summary>
        /// Gets or sets the font size for the content of the element.
        /// </summary>
        /// <returns>
        /// The desired font size to use in device independent pixels,  greater than 0.001 and less than or equal to 35791.  The default depends on current system settings and depends on the <see cref="P:System.Windows.SystemFonts.MessageFontSize" /> value.
        /// </returns>
        double FontSize { get; }

        /// <summary>
        /// Gets or sets the <see cref="T:System.Windows.Media.Brush" /> to apply to the content of the element.
        /// </summary>
        /// <returns>
        /// The brush used to apply to the text contents. The default is <see cref="P:System.Windows.Media.Brushes.Black" />.
        /// </returns>
        Brush Foreground { get; }

        /// <summary>
        /// Gets or sets a collection of text effects to apply to the content of the element.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Media.TextEffectCollection" /> containing one or more <see cref="T:System.Windows.Media.TextEffect" /> objects that define effects to apply to the content in this element. The default is null (not an empty collection).
        /// </returns>
        TextEffectCollection TextEffects { get; }

        /// <summary>
        /// Gets or sets a composite string that specifies how to format the <see cref="P:TextTickBar.Content" /> property if it is displayed as a string.
        /// </summary>
        /// <returns>
        /// A composite string that specifies how to format the <see cref="P:TextTickBar.Content" /> property if it is displayed as a string.
        /// </returns>
        string ContentStringFormat { get; }
    }
}