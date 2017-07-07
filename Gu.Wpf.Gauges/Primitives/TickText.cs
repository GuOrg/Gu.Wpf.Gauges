namespace Gu.Wpf.Gauges
{
    using System.Globalization;
    using System.Windows;
    using System.Windows.Media;

    public class TickText : FormattedText
    {
        private TransformGroup transformGroup;

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
            this.transformGroup = new TransformGroup();
            this.transformGroup.Children.Add(transform ?? Transform.Identity);
            this.transformGroup.Children.Add(this.TranslateTransform);
            this.Geometry.Transform = this.transformGroup;
        }

        public double Value { get; }

        public Geometry Geometry { get; }

        public Transform Transform
        {
            get => this.transformGroup.Children[0];
            set => this.transformGroup.Children[0] = value ?? Transform.Identity;
        }

        public TranslateTransform TranslateTransform { get; } = new TranslateTransform();

        private static string Format(string format, double value, CultureInfo culture) => string.IsNullOrEmpty(format)
            ? value.ToString(culture)
            : string.Format(format, value, culture);
    }
}