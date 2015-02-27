using System.Windows.Controls.Primitives;

namespace Gu.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    public class LinearGauge : Gauge
    {
        static LinearGauge()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinearGauge), new FrameworkPropertyMetadata(typeof(LinearGauge)));
            MinimumProperty.OverrideMetadata(typeof(LinearGauge), new FrameworkPropertyMetadata(0.0, UpdateValuePos));
            MaximumProperty.OverrideMetadata(typeof(LinearGauge), new FrameworkPropertyMetadata(100.0, UpdateValuePos));
            ValueProperty.OverrideMetadata(typeof(LinearGauge), new FrameworkPropertyMetadata(0.0, UpdateValuePos));
        }

        public LinearGauge()
        {
            this.ValueTransform = new TranslateTransform(0, 0);
        }

        protected override void UpdateValuePos()
        {
            var actualWidth = this.ActualWidth - this.Padding.Left - this.Padding.Right;
            var actualHeight = this.ActualHeight - this.Padding.Top - this.Padding.Bottom;
            var line = new Line(actualWidth, actualHeight, 0, this.Placement, this.IsDirectionReversed);
            var pos = TickHelper.ToPos(this.Value, this.Minimum, this.Maximum, line);
            double x;
            double y;
            switch (this.Placement)
            {
                case TickBarPlacement.Left:
                case TickBarPlacement.Right:
                    x = 0;
                    y = pos.Y;
                    break;
                case TickBarPlacement.Top:
                case TickBarPlacement.Bottom:
                    x = pos.X;
                    y = 0;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            var xAnimation = new DoubleAnimation(x, TimeSpan.FromMilliseconds(100));
            this.ValueTransform.BeginAnimation(TranslateTransform.XProperty, xAnimation);

            var yAnimation = new DoubleAnimation(y, TimeSpan.FromMilliseconds(100));
            this.ValueTransform.BeginAnimation(TranslateTransform.YProperty, yAnimation);

            var valueAnimation = new DoubleAnimation(this.Value, TimeSpan.FromMilliseconds(100));
            this.BeginAnimation(AnimatedValueProxyProperty, valueAnimation);
        }
    }
}