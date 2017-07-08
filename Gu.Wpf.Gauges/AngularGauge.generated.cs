namespace Gu.Wpf.Gauges
{
    using System.Windows;

    public partial class AngularGauge
    {
        public static readonly DependencyProperty MinAngleProperty = DependencyProperty.RegisterAttached(
            nameof(MinAngle),
            typeof(double),
            typeof(AngularGauge),
            new FrameworkPropertyMetadata(
                -180.0d,
                FrameworkPropertyMetadataOptions.Inherits,
                OnMinAngleChanged));

        public static readonly DependencyProperty MaxAngleProperty = DependencyProperty.RegisterAttached(
            nameof(MaxAngle),
            typeof(double),
            typeof(AngularGauge),
            new FrameworkPropertyMetadata(
                0.0d,
                FrameworkPropertyMetadataOptions.Inherits,
                OnMaxAngleChanged));

        public static readonly DependencyProperty TickLengthProperty = DependencyProperty.RegisterAttached(
            nameof(TickLength),
            typeof(double),
            typeof(AngularGauge),
            new FrameworkPropertyMetadata(
                0.0d,
                FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty TickGapProperty = DependencyProperty.RegisterAttached(
            nameof(TickGap),
            typeof(double),
            typeof(AngularGauge),
            new FrameworkPropertyMetadata(
                0.0d,
                FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty TextOrientationProperty = DependencyProperty.RegisterAttached(
            nameof(TextOrientation),
            typeof(TextOrientation),
            typeof(AngularGauge),
            new FrameworkPropertyMetadata(
                TextOrientation.Tangential,
                FrameworkPropertyMetadataOptions.Inherits));

        public double MinAngle
        {
            get => (double)this.GetValue(MinAngleProperty);
            set => this.SetValue(MinAngleProperty, value);
        }

        public double MaxAngle
        {
            get => (double)this.GetValue(MaxAngleProperty);
            set => this.SetValue(MaxAngleProperty, value);
        }

        public double TickLength
        {
            get => (double)this.GetValue(TickLengthProperty);
            set => this.SetValue(TickLengthProperty, value);
        }

        public double TickGap
        {
            get => (double)this.GetValue(TickGapProperty);
            set => this.SetValue(TickGapProperty, value);
        }

        public TextOrientation TextOrientation
        {
            get => (TextOrientation)this.GetValue(TextOrientationProperty);
            set => this.SetValue(TextOrientationProperty, value);
        }

        public static void SetMinAngle(DependencyObject element, double value)
        {
            element.SetValue(MinAngleProperty, value);
        }

        public static double GetMinAngle(DependencyObject element)
        {
            return (double)element.GetValue(MinAngleProperty);
        }

        public static void SetMaxAngle(DependencyObject element, double value)
        {
            element.SetValue(MaxAngleProperty, value);
        }

        public static double GetMaxAngle(DependencyObject element)
        {
            return (double)element.GetValue(MaxAngleProperty);
        }

        public static void SetTickLength(DependencyObject element, double value)
        {
            element.SetValue(TickLengthProperty, value);
        }

        public static double GetTickLength(DependencyObject element)
        {
            return (double)element.GetValue(TickLengthProperty);
        }

        public static void SetTickGap(DependencyObject element, double value)
        {
            element.SetValue(TickGapProperty, value);
        }

        public static double GetTickGap(DependencyObject element)
        {
            return (double)element.GetValue(TickGapProperty);
        }

        public static void SetTextOrientation(DependencyObject element, TextOrientation value)
        {
            element.SetValue(TextOrientationProperty, value);
        }

        public static TextOrientation GetTextOrientation(DependencyObject element)
        {
            return (TextOrientation)element.GetValue(TextOrientationProperty);
        }

        private static void OnMinAngleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is AngularGauge gauge)
            {
                gauge.OnMinAngleChanged((double)e.OldValue, (double)e.NewValue);
            }
        }

        private static void OnMaxAngleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is AngularGauge gauge)
            {
                gauge.OnMaxAngleChanged((double)e.OldValue, (double)e.NewValue);
            }
        }
    }
}
