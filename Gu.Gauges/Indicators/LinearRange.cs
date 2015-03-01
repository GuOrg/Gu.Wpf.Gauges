namespace Gu.Gauges
{
    using System.Windows;

    public class LinearRange : RangeIndicator<LinearAxis>
    {
        static LinearRange()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinearRange), new FrameworkPropertyMetadata(typeof(LinearRange)));
        }

        protected override void OnEndChanged(double newValue)
        {
            LinearPanel.SetEnd(this, newValue);
        }

        protected override void OnStartChanged(double newValue)
        {
            LinearPanel.SetStart(this, newValue);
        }
    }
}