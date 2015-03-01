namespace Gu.Gauges
{
    using System.Windows;

    public class RangeIndicator<T> : Indicator<T>
        where T : Axis
    {
        public static readonly DependencyProperty StartProperty = DependencyProperty.Register(
            "Start", typeof(double),
            typeof(RangeIndicator<T>),
            new PropertyMetadata(double.NaN, OnStartChanged));

        public static readonly DependencyProperty EndProperty = DependencyProperty.Register(
            "End", 
            typeof(double), 
            typeof(RangeIndicator<T>),
            new PropertyMetadata(double.NaN, OnEndChanged));

        public double Start
        {
            get { return (double)this.GetValue(StartProperty); }
            set { this.SetValue(StartProperty, value); }
        }

        public double End
        {
            get { return (double)this.GetValue(EndProperty); }
            set { this.SetValue(EndProperty, value); }
        }

        protected virtual void OnStartChanged(double newValue)
        {
        }

        protected virtual void OnEndChanged(double newValue)
        {
        }

        private static void OnStartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
           ( (RangeIndicator<T>)d).OnStartChanged((double) e.NewValue);
        }

        private static void OnEndChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((RangeIndicator<T>)d).OnEndChanged((double)e.NewValue);

        }
    }
}