namespace Gu.Wpf.Gauges
{
   internal static class Interpolate
    {
        internal static double Linear(double min, double max, double value)
        {
            return (value - min) / (max - min);
        }
    }
}
