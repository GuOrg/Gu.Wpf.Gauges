using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Gu.Gauges
{
    public class AngularTextBar : TickBar
    {
        public static readonly DependencyProperty MinAngleProperty = DependencyProperty.Register(
            "MinAngle",
            typeof(double),
            typeof(AngularTextBar),
            new FrameworkPropertyMetadata(
                default(double),
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty MaxAngleProperty = DependencyProperty.Register(
            "MaxAngle",
            typeof(double),
            typeof(AngularTextBar),
            new FrameworkPropertyMetadata(
                default(double),
                FrameworkPropertyMetadataOptions.AffectsRender));

        public double MinAngle
        {
            get { return (double)this.GetValue(MinAngleProperty); }
            set { this.SetValue(MinAngleProperty, value); }
        }

        public double MaxAngle
        {
            get { return (double)this.GetValue(MaxAngleProperty); }
            set { this.SetValue(MaxAngleProperty, value); }
        }

        protected override void OnRender(DrawingContext dc)
        {
            var midPoint = new Point(this.ActualWidth / 2, this.ActualHeight / 2);
            var pi = new Point(this.ActualWidth - this.ReservedSpace, midPoint.Y);
            var ticks = this.Ticks.Concat(AngleHelper.CreateTicks(this.Minimum, this.Maximum, this.TickFrequency));
            foreach (var tick in ticks)
            {
                if (tick < this.Minimum || tick > this.Maximum)
                {
                    continue;
                }
                var angle = AngleHelper.ToAngle(tick, this.Minimum, this.Maximum, this.MinAngle, this.MaxAngle);
                var rotateTransform = new RotateTransform(angle, midPoint.X, midPoint.Y);
                var p1 = rotateTransform.Transform(pi);
                var text = this.ToText(tick, angle);
                //dc.PushTransform(rotateTransform);
                dc.DrawText(text, p1);
            }
        }

        private FormattedText ToText(double tick, double angle)
        {
            return new FormattedText(tick.ToString(), CultureInfo.CurrentUICulture, FlowDirection.LeftToRight, new Typeface("Arial"), 12, Brushes.Black);
        }
    }
}