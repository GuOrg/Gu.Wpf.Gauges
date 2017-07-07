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
            this.Geometry = this.BuildGeometry(default(Point));
            var transformGroup = new TransformGroup();
            if (transform != null)
            {
                transformGroup.Children.Add(transform);
            }

            transformGroup.Children.Add(this.TranslateTransform);
            this.Geometry.Transform = transformGroup;
        }

        public double Value { get; }

        public Geometry Geometry { get; }

        public TranslateTransform TranslateTransform { get; } = new TranslateTransform();

        private static string Format(string format, double value, CultureInfo culture) => string.IsNullOrEmpty(format)
            ? value.ToString(culture)
            : string.Format(format, value, culture);
    }
}