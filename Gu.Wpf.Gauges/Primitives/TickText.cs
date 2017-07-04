namespace Gu.Wpf.Gauges
{
    using System.Globalization;
    using System.Windows;
    using System.Windows.Media;

    public class TickText : FormattedText
    {
        public TickText(
            double value,
            string format,
            Typeface typeface,
            double emSize,
            Brush foreground,
            Transform transform)
            : base(
                Format(format, value, CultureInfo.CurrentUICulture),
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                typeface,
                emSize,
                foreground)
        {
            this.Value = value;
            this.Transform = transform;
        }

        public double Value { get; }

        public Transform Transform { get; }

        public Point Point { get; set; }

        private static string Format(string format, double value, CultureInfo culture) => string.IsNullOrEmpty(format)
            ? value.ToString(culture)
            : string.Format(format, value, culture);
    }
}