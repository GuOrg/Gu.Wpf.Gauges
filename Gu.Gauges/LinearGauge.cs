namespace Gu.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    public class LinearGauge : RangeBase
    {
        private static readonly DependencyPropertyKey ValueTransformPropertyKey = DependencyProperty.RegisterReadOnly(
            "ValueTransform",
            typeof(TranslateTransform),
            typeof(LinearGauge),
            new PropertyMetadata(default(TranslateTransform)));

        public static readonly DependencyProperty ValueTransformProperty = ValueTransformPropertyKey.DependencyProperty;

        public static readonly DependencyProperty ShowLabelsProperty = DependencyProperty.Register(
            "ShowLabels",
            typeof(bool),
            typeof(LinearGauge),
            new PropertyMetadata(true));

        public static readonly DependencyProperty MajorTickFrequencyProperty = DependencyProperty.Register(
            "MajorTickFrequency",
            typeof(double),
            typeof(LinearGauge),
            new PropertyMetadata(default(double)));

        public static readonly DependencyProperty MajorTicksProperty = DependencyProperty.Register(
            "MajorTicks",
            typeof(DoubleCollection),
            typeof(LinearGauge),
            new PropertyMetadata(default(DoubleCollection)));

        public static readonly DependencyProperty MinorTickFrequencyProperty = DependencyProperty.Register(
            "MinorTickFrequency",
            typeof(double),
            typeof(LinearGauge),
            new PropertyMetadata(default(double)));

        public static readonly DependencyProperty PlacementProperty = TickBar.PlacementProperty.AddOwner(
            typeof(LinearGauge),
            new PropertyMetadata(
                default(TickBarPlacement),
                UpdateValuePos));

        /// <summary>
        /// Identifies the <see cref="P:LinearGauge.IsDirectionReversed" /> dependency property. 
        /// </summary>
        public static readonly DependencyProperty IsDirectionReversedProperty = Slider.IsDirectionReversedProperty.AddOwner(
            typeof(LinearGauge),
            new FrameworkPropertyMetadata(
                false,
                UpdateValuePos));

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

        public TranslateTransform ValueTransform
        {
            get { return (TranslateTransform)this.GetValue(ValueTransformProperty); }
            protected set { this.SetValue(ValueTransformPropertyKey, value); }
        }

        public bool ShowLabels
        {
            get { return (bool)this.GetValue(ShowLabelsProperty); }
            set { this.SetValue(ShowLabelsProperty, value); }
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

        public TickBarPlacement Placement
        {
            get { return (TickBarPlacement)this.GetValue(PlacementProperty); }
            set { this.SetValue(PlacementProperty, value); }
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

        private static void UpdateValuePos(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var gauge = (LinearGauge)d;
            var line = new Line(gauge.ActualWidth - gauge.Padding.Left - gauge.Padding.Right, gauge.ActualHeight - gauge.Padding.Top - gauge.Padding.Bottom, 0, gauge.Placement, gauge.IsDirectionReversed);
            var pos = TickHelper.ToPos(gauge.Value, gauge.Minimum, gauge.Maximum, line);
            var xAnimation = new DoubleAnimation(pos.X, TimeSpan.FromMilliseconds(100));
            var yAnimation = new DoubleAnimation(pos.Y, TimeSpan.FromMilliseconds(100));
            gauge.ValueTransform.BeginAnimation(TranslateTransform.XProperty, xAnimation);
            gauge.ValueTransform.BeginAnimation(TranslateTransform.YProperty, yAnimation);
            //gauge.ValueTransform.X = pos.X;
            //gauge.ValueTransform.Y = pos.Y;
        }
    }
}