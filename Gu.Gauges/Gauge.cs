namespace Gu.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media.Animation;

    public abstract class Gauge : Control, IGauge
    {
#pragma warning disable SA1202 // Elements must be ordered by access
        /// <summary>
        /// Identifies the <see cref="P:Gauge.Value" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:Gauge.Value" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty ValueProperty = RangeBase.ValueProperty.AddOwner(
            typeof(Gauge),
            new PropertyMetadata(
                0.0,
                OnValueChanged));

        private static readonly DependencyPropertyKey AnimatedValuePropertyKey = DependencyProperty.RegisterReadOnly(
nameof(AnimatedValue),
            typeof(double),
            typeof(Gauge),
            new FrameworkPropertyMetadata(default(double)));

        // A proxy that is used for animating. Sets the value of the readonly property on change.
        protected static readonly DependencyProperty AnimatedValueProxyProperty = DependencyProperty.Register(
            "AnimatedValueProxy",
            typeof(double),
            typeof(Gauge),
            new PropertyMetadata(
                0.0,
                OnAnimatedValueProxyChanged));

        public static readonly DependencyProperty AnimatedValueProperty = AnimatedValuePropertyKey.DependencyProperty;
#pragma warning restore SA1202 // Elements must be ordered by access

        /// <summary>
        /// Gets or sets the current magnitude of the range control.
        /// </summary>
        /// <returns>
        /// The current magnitude of the range control. The default is 0.
        /// </returns>
        public double Value
        {
            get => (double)this.GetValue(ValueProperty);
            set => this.SetValue(ValueProperty, value);
        }

        /// <summary>
        /// Gets the value with animated transitions.
        /// </summary>
        public double AnimatedValue
        {
            get => (double)this.GetValue(AnimatedValueProperty);
            protected set => this.SetValue(AnimatedValuePropertyKey, value);
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var gauge = (Gauge)d;
            gauge.AnimateValue();
        }

        private static void OnAnimatedValueProxyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(AnimatedValuePropertyKey, e.NewValue);
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
    }
}