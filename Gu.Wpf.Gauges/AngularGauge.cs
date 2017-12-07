namespace Gu.Wpf.Gauges
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;

    public class AngularGauge : Gauge
    {
        public static readonly DependencyProperty StartProperty = DependencyProperty.RegisterAttached(
            nameof(Start),
            typeof(Angle),
            typeof(AngularGauge),
            new FrameworkPropertyMetadata(
                Angle.DefaultStart,
                FrameworkPropertyMetadataOptions.Inherits,
                OnStartChanged));

        public static readonly DependencyProperty EndProperty = DependencyProperty.RegisterAttached(
            nameof(End),
            typeof(Angle),
            typeof(AngularGauge),
            new FrameworkPropertyMetadata(
                Angle.DefaultEnd,
                FrameworkPropertyMetadataOptions.Inherits,
                OnEndChanged));

        public static readonly DependencyProperty TextOrientationProperty = DependencyProperty.RegisterAttached(
            nameof(TextOrientation),
            typeof(TextOrientation),
            typeof(AngularGauge),
            new FrameworkPropertyMetadata(
                Defaults.TextOrientation,
                FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty TextOffsetProperty = DependencyProperty.RegisterAttached(
            nameof(TextOffset),
            typeof(double),
            typeof(AngularGauge),
            new FrameworkPropertyMetadata(
                0d,
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
        public Angle Start
        {
            get => (Angle)this.GetValue(StartProperty);
            set => this.SetValue(StartProperty, value);
        }

        /// <summary>
        /// Gets or sets the end angle of the arc.
        /// Degrees clockwise from the y axis.
        /// The default is 140
        /// </summary>
        public Angle End
        {
            get => (Angle)this.GetValue(EndProperty);
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

        public double TextOffset
        {
            get => (double)this.GetValue(TextOffsetProperty);
            set => this.SetValue(TextOffsetProperty, value);
        }

        public static void SetStart(DependencyObject element, Angle value)
        {
            element.SetValue(StartProperty, value);
        }

        public static Angle GetStart(DependencyObject element)
        {
            return (Angle)element.GetValue(StartProperty);
        }

        public static void SetEnd(DependencyObject element, Angle value)
        {
            element.SetValue(EndProperty, value);
        }

        public static Angle GetEnd(DependencyObject element)
        {
            return (Angle)element.GetValue(EndProperty);
        }

        public static void SetTextOrientation(DependencyObject element, TextOrientation value)
        {
            element.SetValue(TextOrientationProperty, value);
        }

        public static TextOrientation GetTextOrientation(DependencyObject element)
        {
            return (TextOrientation)element.GetValue(TextOrientationProperty);
        }

        public static void SetTextOffset(DependencyObject element, double value)
        {
            element.SetValue(TextOffsetProperty, value);
        }

        public static double GetTextOffset(DependencyObject element)
        {
            return (double)element.GetValue(TextOffsetProperty);
        }

        /// <summary>
        ///     This method is invoked when the <see cref="Start"/> property changes.
        /// </summary>
        /// <param name="oldValue">The old value of the <see cref="Start"/> property.</param>
        /// <param name="newValue">The new value of the <see cref="Start"/> property.</param>
        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        protected virtual void OnStartChanged(Angle oldValue, Angle newValue)
        {
        }

        /// <summary>
        ///     This method is invoked when the <see cref="End"/> property changes.
        /// </summary>
        /// <param name="oldValue">The old value of the <see cref="End"/> property.</param>
        /// <param name="newValue">The new value of the <see cref="End"/> property.</param>
        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        protected virtual void OnEndChanged(Angle oldValue, Angle newValue)
        {
        }

        private static void OnStartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is AngularGauge gauge)
            {
                gauge.OnStartChanged((Angle)e.OldValue, (Angle)e.NewValue);
            }
        }

        private static void OnEndChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is AngularGauge gauge)
            {
                gauge.OnEndChanged((Angle)e.OldValue, (Angle)e.NewValue);
            }
        }
    }
}
