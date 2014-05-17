namespace Gauges
{
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    /// <summary>
    /// http://stackoverflow.com/a/3578214/1069200
    /// </summary>
    public class TextTickBar : TickBar
    {
        protected override void OnRender(System.Windows.Media.DrawingContext dc)
        {
            double num = this.Maximum - this.Minimum;
            double y = this.ReservedSpace * 0.5;
            FormattedText formattedText = null;
            double x = 0;
            for (double i = 0; i <= num; i += this.TickFrequency)
            {
                formattedText = new FormattedText(
                    i.ToString(CultureInfo.CurrentUICulture),
                    CultureInfo.CurrentUICulture,
                    FlowDirection.LeftToRight,
                    new Typeface("Verdana"),
                    8,
                    this.Fill);
                if (this.Minimum == i)
                    x = 0;
                else
                    x += this.ActualWidth / (num / this.TickFrequency);
                dc.DrawText(formattedText, new Point(x, 10));
            }
        }
    }
}
