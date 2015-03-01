namespace Gu.Gauges
{
    using System.Windows;

    public class LinearValue : CenteredIndicator
    {
        static LinearValue()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinearValue), new FrameworkPropertyMetadata(typeof(LinearValue)));
        }
        protected override void OnValueChanged(double newValue)
        {
            LinearPanel.SetAtValue(this, newValue);
        }
    }
}