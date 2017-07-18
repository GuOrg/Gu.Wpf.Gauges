namespace Gu.Wpf.Gauges
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;

    public class AngularRange : AngularIndicator
    {
        public static readonly DependencyProperty StartProperty = DependencyProperty.Register(
            nameof(Start),
            typeof(double),
            typeof(AngularRange),
            new PropertyMetadata(
                double.NaN,
                OnStartChanged));

        public static readonly DependencyProperty EndProperty = DependencyProperty.Register(
            nameof(End),
            typeof(double),
            typeof(AngularRange),
            new PropertyMetadata(
                double.NaN,
                OnEndChanged));

        static AngularRange()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(AngularRange),
                new FrameworkPropertyMetadata(typeof(AngularRange)));
        }

        public double Start
        {
            get => (double)this.GetValue(StartProperty);
            set => this.SetValue(StartProperty, value);
        }

        public double End
        {
            get => (double)this.GetValue(EndProperty);
            set => this.SetValue(EndProperty, value);
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        protected virtual void OnEndChanged(double oldValue, double newValue)
        {
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        protected virtual void OnStartChanged(double oldValue, double newValue)
        {
        }

        private static void OnStartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((AngularRange)d).OnStartChanged((double)e.OldValue, (double)e.NewValue);
        }

        private static void OnEndChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((AngularRange)d).OnEndChanged((double)e.OldValue, (double)e.NewValue);
        }
    }
}