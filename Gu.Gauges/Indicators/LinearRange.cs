namespace Gu.Gauges
{
    public class LinearRange : RangeIndicator<LinearAxis>
    {
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