namespace Gu.Wpf.Gauges
{
   internal static class Linear
    {
        internal static double Interpolate(double min, double max, double value)
        {
            return (value - min) / (max - min);
        }
    }
}
