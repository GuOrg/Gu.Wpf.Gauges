namespace Gu.Wpf.Gauges
{
    using System.Globalization;
    using System.Windows;
    using System.Windows.Media;

    public class TickText : FormattedText
    {
        public TickText(
            double tick,
            string format,
            Typeface typeface,
            double emSize,
            Brush foreground,
            Point point,
            RotateTransform transform)
            : base(
                string.Format(format, tick, CultureInfo.CurrentUICulture),
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                typeface,
                emSize,
                foreground)
        {
            this.Point = point;
            this.Transform = transform;
        }

        public Point Point { get; }

        public RotateTransform Transform { get; }
    }
}