using System.Linq;

namespace Gu.Gauges
{
    using System.Globalization;
    using System.Windows;
    using System.Windows.Media;

    public class AngularTextBar : AngularBar
    {
        protected override void OnRender(DrawingContext dc)
        {
            var midPoint = new Point(this.ActualWidth / 2, this.ActualHeight / 2);
            var pi = new Point(this.ActualWidth - this.ReservedSpace, midPoint.Y);
            var ticks = TickHelper.CreateTicks(this.Minimum, this.Maximum, this.TickFrequency).Concat(this.Ticks ?? Enumerable.Empty<double>());
            foreach (var tick in ticks)
            {
                if (tick < this.Minimum || tick > this.Maximum)
                {
                    continue;
                }
                var angle = TickHelper.ToAngle(tick, this.Minimum, this.Maximum, this.MinAngle, this.MaxAngle);
                var rotateTransform = new RotateTransform(angle, midPoint.X, midPoint.Y);
                var p1 = rotateTransform.Transform(pi);
                var text = this.ToText(tick, angle);
                //dc.PushTransform(rotateTransform);
                dc.DrawText(text, p1);
            }
        }

        private FormattedText ToText(double tick, double angle)
        {
            return new FormattedText(tick.ToString(CultureInfo.CurrentUICulture), CultureInfo.CurrentUICulture, FlowDirection.LeftToRight, new Typeface("Arial"), 12, Brushes.Black);
        }
    }
}