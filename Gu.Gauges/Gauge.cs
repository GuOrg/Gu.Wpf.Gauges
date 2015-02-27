namespace Gu.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    public class Gauge : RangeBase
    {
        private static readonly DependencyPropertyKey ValueTransformPropertyKey = DependencyProperty.RegisterReadOnly(
            "ValueTransform",
            typeof(Transform),
            typeof(Gauge),
            new PropertyMetadata(default(TranslateTransform)));

        public static readonly DependencyProperty ValueTransformProperty = ValueTransformPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey AnimatedValuePropertyKey = DependencyProperty.RegisterReadOnly(
            "AnimatedValue",
            typeof(double),
            typeof(Gauge),
            new FrameworkPropertyMetadata(default(double)));

        // A proxy that is used for animating. Sets the value of the readonly property on change.
        protected static readonly DependencyProperty AnimatedValueProxyProperty = DependencyProperty.Register(
            "AnimatedValueProxy",
            typeof(double),
            typeof(Gauge),
            new PropertyMetadata(0.0, OnAnimatedValueProxyChanged));

        public static readonly DependencyProperty AnimatedValueProperty = AnimatedValuePropertyKey.DependencyProperty;

        public static readonly DependencyProperty ShowLabelsProperty = DependencyProperty.Register(
            "ShowLabels",
            typeof(bool),
            typeof(Gauge),
            new PropertyMetadata(true));

        public static readonly DependencyProperty MajorTickFrequencyProperty = DependencyProperty.Register(
            "MajorTickFrequency",
            typeof(double),
            typeof(Gauge),
            new FrameworkPropertyMetadata(default(double)));

        public static readonly DependencyProperty MajorTicksProperty = DependencyProperty.Register(
            "MajorTicks",
            typeof(DoubleCollection),
            typeof(Gauge),
            new PropertyMetadata(default(DoubleCollection)));

        public static readonly DependencyProperty MinorTickFrequencyProperty = DependencyProperty.Register(
            "MinorTickFrequency",
            typeof(double),
            typeof(Gauge),
            new PropertyMetadata(default(double)));

        public static readonly DependencyProperty TextOrientationProperty = DependencyProperty.Register(
            "TextOrientation",
            typeof(TextOrientation),
            typeof(Gauge),
            new FrameworkPropertyMetadata(TextOrientation.Horizontal, FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty PlacementProperty = TickBar.PlacementProperty.AddOwner(
            typeof(Gauge),
            new FrameworkPropertyMetadata(
                default(TickBarPlacement),
                FrameworkPropertyMetadataOptions.Inherits,
                UpdateValuePos));

        /// <summary>
        /// Identifies the <see cref="P:Gauge.IsDirectionReversed" /> dependency property. 
        /// </summary>
        public static readonly DependencyProperty IsDirectionReversedProperty = Slider.IsDirectionReversedProperty.AddOwner(
            typeof(Gauge),
            new FrameworkPropertyMetadata(
                false,
                FrameworkPropertyMetadataOptions.Inherits,
                UpdateValuePos));

        /// <summary>
        /// Gets the transform to the position that corresponds to the current value
        /// </summary>
        public Transform ValueTransform
        {
            get { return (Transform)this.GetValue(ValueTransformProperty); }
            protected set { this.SetValue(ValueTransformPropertyKey, value); }
        }

        /// <summary>
        /// Gets the value with animated transitions.
        /// </summary>
        public double AnimatedValue
        {
            get { return (double)this.GetValue(AnimatedValueProperty); }
            protected set { this.SetValue(AnimatedValuePropertyKey, value); }
        }

        /// <summary>
        /// Gets or sets if textlabels should be visible
        /// </summary>
        public bool ShowLabels
        {
            get { return (bool)this.GetValue(ShowLabelsProperty); }
            set { this.SetValue(ShowLabelsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="T:Gu.Gauges.TextOrientation" />
        /// Default is Horizontal
        /// </summary>
        public TextOrientation TextOrientation
        {
            get { return (TextOrientation)this.GetValue(TextOrientationProperty); }
            set { this.SetValue(TextOrientationProperty, value); }
        }

        /// <summary>
        /// Gets or sets the textplacement
        /// </summary>
        public TickBarPlacement Placement
        {
            get { return (TickBarPlacement)this.GetValue(PlacementProperty); }
            set { this.SetValue(PlacementProperty, value); }
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

        protected static void UpdateValuePos(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var gauge = (Gauge)d;
            gauge.UpdateValuePos();
        }

        protected virtual void UpdateValuePos()
        {
            if (double.IsNaN(this.Value))
            {
                return;
            }
            var valueAnimation = new DoubleAnimation(this.Value, TimeSpan.FromMilliseconds(100));
            this.BeginAnimation(AnimatedValueProxyProperty, valueAnimation);
        }

        private static void OnAnimatedValueProxyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(AnimatedValuePropertyKey, e.NewValue);
        }
    }
}