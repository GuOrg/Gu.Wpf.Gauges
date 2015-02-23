using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;

namespace Gu.Gauges
{
    using System.Windows;
    using System.Windows.Controls.Primitives;

    public class AngularGauge : RangeBase
    {
        private static readonly DependencyPropertyKey ValueAnglePropertyKey  = DependencyProperty.RegisterReadOnly(
            "ValueAngleProperty",
            typeof (double),
            typeof (AngularGauge),
            new PropertyMetadata(default(double)));

        public static readonly DependencyProperty ValueAngleProperty = ValueAnglePropertyKey.DependencyProperty;

        public static readonly DependencyProperty MinAngleProperty = AngularBar.MinAngleProperty.AddOwner(
            typeof (AngularGauge),
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
            MinimumProperty.OverrideMetadata(typeof(AngularGauge), new PropertyMetadata(0, UpdateValueAngle));
            MaximumProperty.OverrideMetadata(typeof(AngularGauge), new PropertyMetadata(100, UpdateValueAngle));
            ValueProperty.OverrideMetadata(typeof(AngularGauge), new PropertyMetadata(0, UpdateValueAngle));
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

        /// <summary>
        /// The angle that corresponds to the current value
        /// </summary>
        public double ValueAngle
        {
            get { return (double)this.GetValue(ValueAngleProperty); }
            protected set { this.SetValue(ValueAnglePropertyKey, value); }
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
            var gauge = (AngularGauge) d;
            var arc = new Arc(new Point(gauge.ActualWidth/2, gauge.ActualHeight/2), gauge.MinAngle, gauge.MaxAngle, 1,
                gauge.IsDirectionReversed);
            var angle = TickHelper.ToAngle(gauge.Value, gauge.Minimum, gauge.Maximum, arc);
            gauge.ValueAngle = angle;
        }
    }
}
