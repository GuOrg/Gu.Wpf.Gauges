namespace Gu.Gauges
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    public static class TextHelper
    {
        internal static FormattedText AsFormattedText(double value, ITextFormat textFormat)
        {
            var formattedText = new FormattedText(
                value.ToString(textFormat.ContentStringFormat, CultureInfo.CurrentUICulture),
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface(
                    textFormat.FontFamily,
                    textFormat.FontStyle,
                    textFormat.FontWeight,
                    textFormat.FontStretch),
                textFormat.FontSize,
                textFormat.Foreground);
            return formattedText;
        }

        internal static Vector GetDrawOffset(FormattedText formattedText, TickBarPlacement placement, double actualWidth, double actualHeight)
        {
            var offsetX = 0.0;
            var offsetY = 0.0;
            switch (placement)
            {
                case TickBarPlacement.Left:
                    offsetX = 0;
                    offsetY = -1 * formattedText.Height / 2;
                    break;
                case TickBarPlacement.Top:
                    offsetX = -1 * formattedText.Width / 2;
                    offsetY = 0;
                    break;
                case TickBarPlacement.Right:
                    offsetX = -1 * formattedText.Width + actualWidth;
                    offsetY = -1 * formattedText.Height / 2;
                    break;
                case TickBarPlacement.Bottom:
                    offsetX = -1 * formattedText.Width / 2;
                    offsetY = actualHeight - formattedText.Height;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return new Vector(offsetX, offsetY);
        }
    }
}
