namespace Gu.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    public class AngularGauge : RangeBase
    {
        private static readonly DependencyPropertyKey AngleTransformPropertyKey = DependencyProperty.RegisterReadOnly(
                "AngleTransform",
                typeof(RotateTransform),
                typeof(AngularGauge),
                new PropertyMetadata(default(RotateTransform)));

        public static readonly DependencyProperty AngleTransformProperty = AngleTransformPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey TextSpacePropertyKey = DependencyProperty.RegisterReadOnly(
            "TextSpace",
            typeof(double),
            typeof(AngularGauge),
            new PropertyMetadata(default(double)));

        public static readonly DependencyProperty TextSpaceProperty = TextSpacePropertyKey.DependencyProperty;

        public static readonly DependencyProperty MinAngleProperty = AngularBar.MinAngleProperty.AddOwner(
            typeof(AngularGauge),
            new PropertyMetadata(
                -180.0,
                UpdateValueAngle));

        public static readonly DependencyProperty MaxAngleProperty = AngularBar.MaxAngleProperty.AddOwner(
            typeof(AngularGauge),
            new PropertyMetadata(
                0.0,
                UpdateValueAngle));

        /// <summary>
        /// Identifies the <see cref="P:AngularGauge.IsDirectionReversed" /> dependency property. 
        /// </summary>
        public static readonly DependencyProperty IsDirectionReversedProperty = Slider.IsDirectionReversedProperty.AddOwner(
            typeof(AngularGauge),
            new PropertyMetadata(
                false,
                UpdateValueAngle));

        public static readonly DependencyProperty MajorTickFrequencyProperty = LinearGauge.MajorTickFrequencyProperty.AddOwner(typeof(AngularGauge));

        public static readonly DependencyProperty MajorTicksProperty = LinearGauge.MajorTicksProperty.AddOwner(typeof(AngularGauge));

        public static readonly DependencyProperty MinorTickFrequencyProperty = LinearGauge.MinorTickFrequencyProperty.AddOwner(typeof(AngularGauge));

        static AngularGauge()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AngularGauge), new FrameworkPropertyMetadata(typeof(AngularGauge)));
            MinimumProperty.OverrideMetadata(typeof(AngularGauge), new FrameworkPropertyMetadata(0.0, UpdateValueAngle));
            MaximumProperty.OverrideMetadata(typeof(AngularGauge), new FrameworkPropertyMetadata(100.0, UpdateValueAngle));
            ValueProperty.OverrideMetadata(typeof(AngularGauge), new FrameworkPropertyMetadata(0.0, UpdateValueAngle));
            FontSizeProperty.OverrideMetadata(typeof(AngularGauge), new FrameworkPropertyMetadata(12.0, UpdateTextSpace));
        }

        public AngularGauge()
        {
            this.AngleTransform = new RotateTransform(0, 0, 0);
            this.TextSpace = 1.5 * this.FontSize;
        }

        public double TextSpace
        {
            get { return (double)this.GetValue(TextSpaceProperty); }
            protected set { this.SetValue(TextSpacePropertyKey, value); }
        }

        public RotateTransform AngleTransform
        {
            get { return (RotateTransform)this.GetValue(AngleTransformProperty); }
            protected set { this.SetValue(AngleTransformPropertyKey, value); }
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

        /// <summary>
        /// Gets or sets the direction of increasing value. 
        /// </summary>
        /// <returns>
        /// true if the direction of increasing value is to the left for a horizontal tickbar or down for a vertical tickbar; otherwise, false. 
        /// The default is false.
        /// </returns>
        public bool IsDirectionReversed
        {
            get { return (bool)this.GetValue(IsDirectionReversedProperty); }
            set { this.SetValue(IsDirectionReversedProperty, value); }
        }

        public double MajorTickFrequency
        {
            get { return (double)this.GetValue(MajorTickFrequencyProperty); }
            set { this.SetValue(MajorTickFrequencyProperty, value); }
        }

        public DoubleCollection MajorTicks
        {
            get { return (DoubleCollection)this.GetValue(MajorTicksProperty); }
            set { this.SetValue(MajorTicksProperty, value); }
        }

        public double MinorTickFrequency
        {
            get { return (double)this.GetValue(MinorTickFrequencyProperty); }
            set { this.SetValue(MinorTickFrequencyProperty, value); }
        }

        private static void UpdateValueAngle(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var gauge = (AngularGauge)d;
            var arc = new Arc(new Point(gauge.ActualWidth / 2, gauge.ActualHeight / 2), gauge.MinAngle, gauge.MaxAngle, 1,
                gauge.IsDirectionReversed);
            var angle = TickHelper.ToAngle(gauge.Value, gauge.Minimum, gauge.Maximum, arc);
            //gauge.AngleTransform.CenterX = gauge.ActualWidth / 2;
            //gauge.AngleTransform.CenterY = gauge.ActualHeight / 2;
            var angleAnimation = new DoubleAnimation(angle, TimeSpan.FromMilliseconds(100));
            gauge.AngleTransform.BeginAnimation(RotateTransform.AngleProperty, angleAnimation);
            //gauge.ValueAngle = angle;
        }

        private static void UpdateTextSpace(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var gauge = (AngularGauge)d;
            gauge.TextSpace = gauge.FontSize * 1.5;
        }
    }
}
