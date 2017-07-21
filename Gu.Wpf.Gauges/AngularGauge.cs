namespace Gu.Wpf.Gauges
{
    using System.Windows;

    public class AngularGauge : Gauge
    {
        public static readonly DependencyProperty StartProperty = DependencyProperty.RegisterAttached(
            nameof(Start),
            typeof(double),
            typeof(AngularGauge),
            new FrameworkPropertyMetadata(
                Defaults.StartAngle,
                FrameworkPropertyMetadataOptions.Inherits,
                OnStartChanged));

        public static readonly DependencyProperty EndProperty = DependencyProperty.RegisterAttached(
            nameof(End),
            typeof(double),
            typeof(AngularGauge),
            new FrameworkPropertyMetadata(
                Defaults.EndAngle,
                FrameworkPropertyMetadataOptions.Inherits,
                OnEndChanged));

        public static readonly DependencyProperty TextOrientationProperty = DependencyProperty.RegisterAttached(
            nameof(TextOrientation),
            typeof(TextOrientation),
            typeof(AngularGauge),
            new FrameworkPropertyMetadata(
                Defaults.TextOrientation,
                FrameworkPropertyMetadataOptions.Inherits));

        static AngularGauge()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AngularGauge), new FrameworkPropertyMetadata(typeof(AngularGauge)));
        }

        /// <summary>
        /// Gets or sets the start angle of the arc.
        /// Degrees clockwise from the y axis.
        /// The default is -140
        /// </summary>
        public double Start
        {
            get => (double)this.GetValue(StartProperty);
            set => this.SetValue(StartProperty, value);
        }

        /// <summary>
        /// Gets or sets the end angle of the arc.
        /// Degrees clockwise from the y axis.
        /// The default is 140
        /// </summary>
        public double End
        {
            get => (double)this.GetValue(EndProperty);
            set => this.SetValue(EndProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="T:Gu.Wpf.Gauges.TextOrientation" />
        /// Default is Tangential
        /// </summary>
        public TextOrientation TextOrientation
        {
            get => (TextOrientation)this.GetValue(TextOrientationProperty);
            set => this.SetValue(TextOrientationProperty, value);
        }

        public static void SetStart(DependencyObject element, double value)
        {
            element.SetValue(StartProperty, value);
        }

        public static double GetStart(DependencyObject element)
        {
            return (double)element.GetValue(StartProperty);
        }

        public static void SetEnd(DependencyObject element, double value)
        {
            element.SetValue(EndProperty, value);
        }

        public static double GetEnd(DependencyObject element)
        {
            return (double)element.GetValue(EndProperty);
        }

        public static void SetTextOrientation(DependencyObject element, TextOrientation value)
        {
            element.SetValue(TextOrientationProperty, value);
        }

        public static TextOrientation GetTextOrientation(DependencyObject element)
        {
            return (TextOrientation)element.GetValue(TextOrientationProperty);
        }

        /// <summary>
        ///     This method is invoked when the <see cref="Start"/> property changes.
        /// </summary>
        /// <param name="oldStartAngle">The old value of the <see cref="Start"/> property.</param>
        /// <param name="newStartAngle">The new value of the <see cref="Start"/> property.</param>
        protected virtual void OnStartChanged(double oldStartAngle, double newStartAngle)
        {
        }

        /// <summary>
        ///     This method is invoked when the <see cref="End"/> property changes.
        /// </summary>
        /// <param name="oldEndAngle">The old value of the <see cref="End"/> property.</param>
        /// <param name="newEndAngle">The new value of the <see cref="End"/> property.</param>
        protected virtual void OnEndChanged(double oldEndAngle, double newEndAngle)
        {
        }

        private static void OnStartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is AngularGauge gauge)
            {
                gauge.OnStartChanged((double)e.OldValue, (double)e.NewValue);
            }
        }

        private static void OnEndChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is AngularGauge gauge)
            {
                gauge.OnEndChanged((double)e.OldValue, (double)e.NewValue);
            }
        }
    }
}
