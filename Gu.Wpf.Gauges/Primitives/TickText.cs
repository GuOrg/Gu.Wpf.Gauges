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
            Transform transform)
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

        public Transform Transform { get; }
    }
}