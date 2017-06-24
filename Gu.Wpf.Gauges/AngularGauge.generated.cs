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

        public static readonly DependencyProperty ThicknessProperty = DependencyProperty.RegisterAttached(
            nameof(Thickness),
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

        public double Thickness
        {
            get => (double)this.GetValue(ThicknessProperty);
            set => this.SetValue(ThicknessProperty, value);
        }

        public double TickGap
        {
            get => (double)this.GetValue(TickGapProperty);
            set => this.SetValue(TickGapProperty, value);
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

        public static void SetThickness(DependencyObject element, double value)
        {
            element.SetValue(ThicknessProperty, value);
        }

        public static double GetThickness(DependencyObject element)
        {
            return (double)element.GetValue(ThicknessProperty);
        }

        public static void SetTickGap(DependencyObject element, double value)
        {
            element.SetValue(TickGapProperty, value);
        }

        public static double GetTickGap(DependencyObject element)
        {
            return (double)element.GetValue(TickGapProperty);
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
