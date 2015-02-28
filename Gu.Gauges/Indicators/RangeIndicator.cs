namespace Gu.Gauges.Indicators
{
    using System.Windows;

    public class RangeIndicator<T> : Indicator<T>
        where T : Axis
    {
        public static readonly DependencyProperty StartProperty = DependencyProperty.Register(
            "Start", typeof(double),
            typeof(RangeIndicator<T>),
            new PropertyMetadata(default(double)));

        public static readonly DependencyProperty EndProperty = DependencyProperty.Register(
            "End", 
            typeof(double), 
            typeof(RangeIndicator<T>), 
            new PropertyMetadata(default(double)));

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
    }
}