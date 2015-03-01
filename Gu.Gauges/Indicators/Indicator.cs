namespace Gu.Gauges
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class Indicator<T> : Control where T : Axis
    {
        public static readonly DependencyProperty AxisProperty = DependencyProperty.Register(
            "Axis",
            typeof(T),
            typeof(Indicator<T>),
            new PropertyMetadata(default(T)));

        public T Axis
        {
            get { return (T)this.GetValue(AxisProperty); }
            set { this.SetValue(AxisProperty, value); }
        }

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            base.OnVisualParentChanged(oldParent);
        }
    }
}
