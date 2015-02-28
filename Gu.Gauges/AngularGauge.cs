namespace Gu.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    public class AngularGauge : Gauge
    {
        public static readonly DependencyProperty MinAngleProperty = AngularBar.MinAngleProperty.AddOwner(
            typeof(AngularGauge),
            new FrameworkPropertyMetadata(
                -180.0,
                FrameworkPropertyMetadataOptions.Inherits,
                UpdateValuePos));

        public static readonly DependencyProperty MaxAngleProperty = AngularBar.MaxAngleProperty.AddOwner(
            typeof (AngularGauge),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.Inherits,
                UpdateValuePos));

        static AngularGauge()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AngularGauge), new FrameworkPropertyMetadata(typeof(AngularGauge)));
            MinimumProperty.OverrideMetadata(typeof(AngularGauge), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.Inherits, UpdateValuePos));
            MaximumProperty.OverrideMetadata(typeof(AngularGauge), new FrameworkPropertyMetadata(100.0, FrameworkPropertyMetadataOptions.Inherits, UpdateValuePos));
            ValueProperty.OverrideMetadata(typeof(AngularGauge), new FrameworkPropertyMetadata(0.0, UpdateValuePos));
            FontSizeProperty.OverrideMetadata(typeof(AngularGauge), new FrameworkPropertyMetadata(12.0, FrameworkPropertyMetadataOptions.Inherits));
            TextOrientationProperty.OverrideMetadata(typeof(AngularGauge), new FrameworkPropertyMetadata(TextOrientation.Tangential, FrameworkPropertyMetadataOptions.Inherits));
        }

        public AngularGauge()
        {
            this.ValueTransform = new RotateTransform(0, 0, 0);
        }

        /// <summary>
        /// Gets or sets the <see cref="P:AngularGauge.MinAngle" />
        /// The default is -180
        /// </summary>
        public double MinAngle
        {
            get { return (double)this.GetValue(MinAngleProperty); }
            set { this.SetValue(MinAngleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="P:AngularGauge.MaxAngle" />
        /// The default is 0
        /// </summary>
        public double MaxAngle
        {
            get { return (double)this.GetValue(MaxAngleProperty); }
            set { this.SetValue(MaxAngleProperty, value); }
        }

        protected override void UpdateValuePos()
        {
            var arc = new Arc(new Point(this.ActualWidth / 2, this.ActualHeight / 2), this.MinAngle, this.MaxAngle, 1, this.IsDirectionReversed);
            var angle = TickHelper.ToAngle(this.Value, this.Minimum, this.Maximum, arc);
            //this.AngleTransform.CenterX = this.ActualWidth / 2;
            //this.AngleTransform.CenterY = this.ActualHeight / 2;
            var angleAnimation = new DoubleAnimation(angle, TimeSpan.FromMilliseconds(100));
            this.ValueTransform.BeginAnimation(RotateTransform.AngleProperty, angleAnimation);
            var valueAnimation = new DoubleAnimation(this.Value, TimeSpan.FromMilliseconds(100));
            this.BeginAnimation(AnimatedValueProxyProperty, valueAnimation);
        }
    }
}
