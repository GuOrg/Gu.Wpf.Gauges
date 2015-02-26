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
            var line = new Line(this.ActualWidth - this.Padding.Left - this.Padding.Right, this.ActualHeight - this.Padding.Top - this.Padding.Bottom, 0, this.Placement, this.IsDirectionReversed);
            var pos = TickHelper.ToPos(this.Value, this.Minimum, this.Maximum, line);
            var xAnimation = new DoubleAnimation(pos.X, TimeSpan.FromMilliseconds(100));
            var yAnimation = new DoubleAnimation(pos.Y, TimeSpan.FromMilliseconds(100));
            this.ValueTransform.BeginAnimation(TranslateTransform.XProperty, xAnimation);
            this.ValueTransform.BeginAnimation(TranslateTransform.YProperty, yAnimation);
            var valueAnimation = new DoubleAnimation(this.Value, TimeSpan.FromMilliseconds(100));
            this.BeginAnimation(AnimatedValueProxyProperty, valueAnimation);
        }
    }
}