namespace Gu.Wpf.Gauges
{
   internal static class Interpolate
    {
        internal static double Linear(double min, double max, double value)
        {
            if (DoubleUtil.AreClose(min, max))
            {
                return 0;
            }

            return (value - min) / (max - min);
        }
    }
}
