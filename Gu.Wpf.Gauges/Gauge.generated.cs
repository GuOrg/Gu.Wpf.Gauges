namespace Gu.Wpf.Gauges
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Media;

    [DefaultEvent(nameof(ValueChanged))]
    [DefaultProperty("Value")]
    public partial class Gauge
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached(
            nameof(Value),
            typeof(double),
            typeof(Gauge),
            new FrameworkPropertyMetadata(
                double.NaN,
                FrameworkPropertyMetadataOptions.Inherits,
                OnValueChanged));

        public static readonly DependencyProperty MinimumProperty = DependencyProperty.RegisterAttached(
            nameof(Minimum),
            typeof(double),
            typeof(Gauge),
            new FrameworkPropertyMetadata(
                0.0d,
                FrameworkPropertyMetadataOptions.Inherits,
                OnMinimumChanged));

        public static readonly DependencyProperty MaximumProperty = DependencyProperty.RegisterAttached(
            nameof(Maximum),
            typeof(double),
            typeof(Gauge),
            new FrameworkPropertyMetadata(
                10.0d,
                FrameworkPropertyMetadataOptions.Inherits,
                OnMaximumChanged));

        public static readonly DependencyProperty IsDirectionReversedProperty = DependencyProperty.RegisterAttached(
            nameof(IsDirectionReversed),
            typeof(bool),
            typeof(Gauge),
            new FrameworkPropertyMetadata(
                default(bool),
                FrameworkPropertyMetadataOptions.Inherits,
                OnIsDirectionReversedChanged));

        public static readonly DependencyProperty MajorTickFrequencyProperty = DependencyProperty.RegisterAttached(
            nameof(MajorTickFrequency),
            typeof(double),
            typeof(Gauge),
            new FrameworkPropertyMetadata(
                default(double),
                FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty MajorTicksProperty = DependencyProperty.RegisterAttached(
            nameof(MajorTicks),
            typeof(DoubleCollection),
            typeof(Gauge),
            new FrameworkPropertyMetadata(
                default(DoubleCollection),
                FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty MinorTickFrequencyProperty = DependencyProperty.RegisterAttached(
            nameof(MinorTickFrequency),
            typeof(double),
            typeof(Gauge),
            new FrameworkPropertyMetadata(
                default(double),
                FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty MinorTicksProperty = DependencyProperty.RegisterAttached(
            nameof(MinorTicks),
            typeof(DoubleCollection),
            typeof(Gauge),
            new FrameworkPropertyMetadata(
                default(DoubleCollection),
                FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty ThicknessProperty = DependencyProperty.RegisterAttached(
            nameof(Thickness),
            typeof(double),
            typeof(Gauge),
            new FrameworkPropertyMetadata(
                10.0d,
                FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty TextOrientationProperty = DependencyProperty.RegisterAttached(
            nameof(TextOrientation),
            typeof(TextOrientation),
            typeof(Gauge),
            new FrameworkPropertyMetadata(
                TextOrientation.Horizontal,
                FrameworkPropertyMetadataOptions.Inherits));

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

        public bool IsDirectionReversed
        {
            get => (bool)this.GetValue(IsDirectionReversedProperty);
            set => this.SetValue(IsDirectionReversedProperty, value);
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

        public DoubleCollection MinorTicks
        {
            get => (DoubleCollection)this.GetValue(MinorTicksProperty);
            set => this.SetValue(MinorTicksProperty, value);
        }

        public double Thickness
        {
            get => (double)this.GetValue(ThicknessProperty);
            set => this.SetValue(ThicknessProperty, value);
        }

        public TextOrientation TextOrientation
        {
            get => (TextOrientation)this.GetValue(TextOrientationProperty);
            set => this.SetValue(TextOrientationProperty, value);
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

        public static void SetMajorTickFrequency(DependencyObject element, double value)
        {
            element.SetValue(MajorTickFrequencyProperty, value);
        }

        public static double GetMajorTickFrequency(DependencyObject element)
        {
            return (double)element.GetValue(MajorTickFrequencyProperty);
        }

        public static void SetMajorTicks(DependencyObject element, DoubleCollection value)
        {
            element.SetValue(MajorTicksProperty, value);
        }

        public static DoubleCollection GetMajorTicks(DependencyObject element)
        {
            return (DoubleCollection)element.GetValue(MajorTicksProperty);
        }

        public static void SetMinorTickFrequency(DependencyObject element, double value)
        {
            element.SetValue(MinorTickFrequencyProperty, value);
        }

        public static double GetMinorTickFrequency(DependencyObject element)
        {
            return (double)element.GetValue(MinorTickFrequencyProperty);
        }

        public static void SetMinorTicks(DependencyObject element, DoubleCollection value)
        {
            element.SetValue(MinorTicksProperty, value);
        }

        public static DoubleCollection GetMinorTicks(DependencyObject element)
        {
            return (DoubleCollection)element.GetValue(MinorTicksProperty);
        }

        public static void SetThickness(DependencyObject element, double value)
        {
            element.SetValue(ThicknessProperty, value);
        }

        public static double GetThickness(DependencyObject element)
        {
            return (double)element.GetValue(ThicknessProperty);
        }

        public static void SetTextOrientation(DependencyObject element, TextOrientation value)
        {
            element.SetValue(TextOrientationProperty, value);
        }

        public static TextOrientation GetTextOrientation(DependencyObject element)
        {
            return (TextOrientation)element.GetValue(TextOrientationProperty);
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

        private static void OnIsDirectionReversedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Gauge gauge)
            {
                gauge.OnIsDirectionReversedChanged((bool)e.OldValue, (bool)e.NewValue);
            }
        }
    }
}
