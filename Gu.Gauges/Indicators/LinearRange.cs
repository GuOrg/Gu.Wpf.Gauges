namespace Gu.Gauges
{
    using System.Windows;

    public class LinearRange : LinearIndicator
    {
        public static readonly DependencyProperty StartProperty = DependencyProperty.Register(
            nameof(Start),
            typeof(double),
            typeof(LinearRange),
            new PropertyMetadata(
                double.NaN,
                OnStartChanged));

        public static readonly DependencyProperty EndProperty = DependencyProperty.Register(
            nameof(End),
            typeof(double),
            typeof(LinearRange),
            new PropertyMetadata(double.NaN, OnEndChanged));

        static LinearRange()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinearRange),
                new FrameworkPropertyMetadata(typeof(LinearRange)));
        }

        public double Start
        {
            get => (double) this.GetValue(StartProperty);
            set => this.SetValue(StartProperty, value);
        }

        public double End
        {
            get => (double) this.GetValue(EndProperty);
            set => this.SetValue(EndProperty, value);
        }

        protected virtual void OnEndChanged(double newValue)
        {
            LinearPanel.SetEnd(this, newValue);
        }

        protected virtual void OnStartChanged(double newValue)
        {
            LinearPanel.SetStart(this, newValue);
        }

        private static void OnStartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LinearRange) d).OnStartChanged((double) e.NewValue);
        }

        private static void OnEndChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LinearRange) d).OnEndChanged((double) e.NewValue);
        }
    }
}