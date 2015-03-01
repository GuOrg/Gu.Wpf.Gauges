namespace Gu.Gauges
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media.Animation;

    public class Gauge<T> : Control where T : Axis
    {
        public static readonly DependencyProperty AxisProperty = DependencyProperty.Register(
            "Axis",
            typeof(T),
            typeof(Gauge<T>),
            new PropertyMetadata(default(T)));

        public static readonly DependencyProperty IndicatorsProperty = DependencyProperty.Register(
            "Indicators",
            typeof(Indicators<T>),
            typeof(Gauge<T>),
            new PropertyMetadata(default(Indicators<T>)));

        /// <summary>
        /// Identifies the <see cref="P:Gauge.Value" /> dependency property. 
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:Gauge.Value" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty ValueProperty = RangeBase.ValueProperty.AddOwner(
            typeof(Gauge<T>),
            new PropertyMetadata(0.0, AnimateValue));

        private static readonly DependencyPropertyKey AnimatedValuePropertyKey = DependencyProperty.RegisterReadOnly(
            "AnimatedValue",
            typeof(double),
            typeof(Gauge<T>),
            new FrameworkPropertyMetadata(default(double)));

        // A proxy that is used for animating. Sets the value of the readonly property on change.
        protected static readonly DependencyProperty AnimatedValueProxyProperty = DependencyProperty.Register(
            "AnimatedValueProxy",
            typeof(double),
            typeof(Gauge<T>),
            new PropertyMetadata(0.0, OnAnimatedValueProxyChanged));

        public static readonly DependencyProperty AnimatedValueProperty = AnimatedValuePropertyKey.DependencyProperty;

        public T Axis
        {
            get { return (T)this.GetValue(AxisProperty); }
            set { this.SetValue(AxisProperty, value); }
        }

        public Indicators<T> Indicators
        {
            get { return (Indicators<T>)this.GetValue(IndicatorsProperty); }
            set { this.SetValue(IndicatorsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the current magnitude of the range control.  
        /// </summary>
        /// <returns>
        /// The current magnitude of the range control. The default is 0.
        /// </returns>
        public double Value
        {
            get { return (double)this.GetValue(ValueProperty); }
            set { this.SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Gets the value with animated transitions.
        /// </summary>
        public double AnimatedValue
        {
            get { return (double)this.GetValue(AnimatedValueProperty); }
            protected set { this.SetValue(AnimatedValuePropertyKey, value); }
        }

        private static void AnimateValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var gauge = (Gauge<T>)d;
            gauge.AnimateValue();
        }

        private void AnimateValue()
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