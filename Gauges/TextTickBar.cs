namespace Gauges
{
    using System.Globalization;
    using System.Net.Mime;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using System.Windows.Shapes;

    /// <summary>
    /// http://stackoverflow.com/a/3578214/1069200
    /// </summary>
    public class TextTickBar : TickBar
    {
        public static readonly DependencyProperty ForegroundProperty = Control.ForegroundProperty.AddOwner(
            typeof(TextTickBar),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsRender));

        public Brush Foreground
        {
            get { return (Brush)this.GetValue(ForegroundProperty); }
            set { this.SetValue(ForegroundProperty, value); }
        }


        public static readonly DependencyProperty FontSizeProperty = Control.FontSizeProperty.AddOwner(
            typeof(TextTickBar),
            new FrameworkPropertyMetadata(12.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double FontSize
        {
            get { return (double)this.GetValue(FontSizeProperty); }
            set { this.SetValue(FontSizeProperty, value); }
        }


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
                    FontSize,
                    Foreground);

                if (this.Minimum == i)
                    x = 0;
                else
                    x += this.ActualWidth / (num / this.TickFrequency);
                dc.DrawText(formattedText, new Point(x - formattedText.Width / 2, y));
            }
        }
    }
}
