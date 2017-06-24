namespace Gu.Wpf.Gauges
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    [DefaultEvent(nameof(ValueChanged))]
    [DefaultProperty("Value")]
    public class Gauge : ContentControl
    {
        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent(
            nameof(ValueChanged),
            RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<double>),
            typeof(Gauge));

        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached(
            "Value",
            typeof(double),
            typeof(Gauge),
            new FrameworkPropertyMetadata(
                double.NaN,
                FrameworkPropertyMetadataOptions.Inherits,
                OnValueChanged));

        public static readonly DependencyProperty MinimumProperty = DependencyProperty.RegisterAttached(
            "Minimum",
            typeof(double),
            typeof(Gauge),
            new FrameworkPropertyMetadata(
                default(double),
                FrameworkPropertyMetadataOptions.Inherits,
                OnMinimumChanged));

        public static readonly DependencyProperty MaximumProperty = DependencyProperty.RegisterAttached(
            "Maximum",
            typeof(double),
            typeof(Gauge),
            new FrameworkPropertyMetadata(
                default(double),
                FrameworkPropertyMetadataOptions.Inherits,
                OnMaximumChanged));

        public static readonly DependencyProperty IsDirectionReversedProperty = DependencyProperty.RegisterAttached(
            "IsDirectionReversed",
            typeof(bool),
            typeof(Gauge),
            new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty MajorTickFrequencyProperty = DependencyProperty.RegisterAttached(
            nameof(MajorTickFrequency),
            typeof(double),
            typeof(Gauge),
            new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty MajorTicksProperty = DependencyProperty.RegisterAttached(
            nameof(MajorTicks),
            typeof(DoubleCollection),
            typeof(Gauge),
            new FrameworkPropertyMetadata(default(DoubleCollection), FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty MinorTickFrequencyProperty = DependencyProperty.RegisterAttached(
            nameof(MinorTickFrequency),
            typeof(double),
            typeof(Gauge),
            new FrameworkPropertyMetadata(default(double), FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Add / Remove ValueChangedEvent handler
        /// </summary>
        [Category("Behavior")]
        public event RoutedPropertyChangedEventHandler<double> ValueChanged
        {
            add => this.AddHandler(ValueChangedEvent, value);
            remove => this.RemoveHandler(ValueChangedEvent, value);
        }

        public double Value
        {
            get => (double)this.GetValue(ValueProperty);
            set => this.SetValue(ValueProperty, value);
        }

        public double Minimum
        {
            get => (double)this.GetValue(MinimumProperty);
            set => this.SetValue(MinimumProperty, value);
        }

        public double Maximum
        {
            get => (double)this.GetValue(MaximumProperty);
            set => this.SetValue(MaximumProperty, value);
        }

        public double MajorTickFrequency
        {
            get => (double)this.GetValue(MajorTickFrequencyProperty);
            set => this.SetValue(MajorTickFrequencyProperty, value);
        }

        public DoubleCollection MajorTicks
        {
            get => (DoubleCollection)this.GetValue(MajorTicksProperty);
            set => this.SetValue(MajorTicksProperty, value);
        }

        public double MinorTickFrequency
        {
            get => (double)this.GetValue(MinorTickFrequencyProperty);
            set => this.SetValue(MinorTickFrequencyProperty, value);
        }

        public static void SetValue(DependencyObject element, double value)
        {
            element.SetValue(ValueProperty, value);
        }

        public static double GetValue(DependencyObject element)
        {
            return (double)element.GetValue(ValueProperty);
        }

        public static void SetMinimum(DependencyObject element, double value)
        {
            element.SetValue(MinimumProperty, value);
        }

        public static double GetMinimum(DependencyObject element)
        {
            return (double)element.GetValue(MinimumProperty);
        }

        public static void SetMaximum(DependencyObject element, double value)
        {
            element.SetValue(MaximumProperty, value);
        }

        public static double GetMaximum(DependencyObject element)
        {
            return (double)element.GetValue(MaximumProperty);
        }

        public static void SetIsDirectionReversed(DependencyObject element, bool value)
        {
            element.SetValue(IsDirectionReversedProperty, value);
        }

        public static bool GetIsDirectionReversed(DependencyObject element)
        {
            return (bool)element.GetValue(IsDirectionReversedProperty);
        }

        /// <summary>
        ///     This method is invoked when the Value property changes.
        /// </summary>
        /// <param name="oldValue">The old value of the Value property.</param>
        /// <param name="newValue">The new value of the Value property.</param>
        protected virtual void OnValueChanged(double oldValue, double newValue)
        {
            this.RaiseEvent(new RoutedPropertyChangedEventArgs<double>(oldValue, newValue) { RoutedEvent = ValueChangedEvent });
        }

        /// <summary>
        ///     This method is invoked when the Minimum property changes.
        /// </summary>
        /// <param name="oldMinimum">The old value of the Minimum property.</param>
        /// <param name="newMinimum">The new value of the Minimum property.</param>
        protected virtual void OnMinimumChanged(double oldMinimum, double newMinimum)
        {
        }

        /// <summary>
        ///     This method is invoked when the Maximum property changes.
        /// </summary>
        /// <param name="oldMaximum">The old value of the Maximum property.</param>
        /// <param name="newMaximum">The new value of the Maximum property.</param>
        protected virtual void OnMaximumChanged(double oldMaximum, double newMaximum)
        {
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Gauge gauge)
            {
                gauge.OnValueChanged((double)e.OldValue, (double)e.NewValue);
            }
        }

        private static void OnMinimumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Gauge gauge)
            {
                gauge.OnMinimumChanged((double)e.OldValue, (double)e.NewValue);
            }
        }

        private static void OnMaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Gauge gauge)
            {
                gauge.OnMaximumChanged((double)e.OldValue, (double)e.NewValue);
            }
        }
    }
}