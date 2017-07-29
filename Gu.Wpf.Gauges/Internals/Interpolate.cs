namespace Gu.Wpf.Gauges
{
    internal static class Interpolate
    {
        /// <summary>
        /// Returns (value - min) / (max - min)
        /// </summary>
        /// <returns>A value between 0 and 1 if <paramref name="value"/> is between <paramref name="min"/> and <paramref name="max"/> </returns>
        internal static Interpolation Linear(double min, double max, double value)
        {
            if (DoubleUtil.AreClose(min, max))
            {
                return Interpolation.Zero;
            }

            return new Interpolation((value - min) / (max - min));
        }

        /// <summary>
        /// Returns min + (linear.Value * (max - min))
        /// </summary>
        internal static double Linear(double min, double max, Interpolation linear)
        {
            if (DoubleUtil.AreClose(min, max))
            {
                return min;
            }

            return min + (linear.Value * (max - min));
        }

        /// <summary>
        /// Returns start + (linear.Value * (max - start))
        /// </summary>
        internal static Angle Linear(Angle start, Angle end, Interpolation linear)
        {
            if (DoubleUtil.AreClose(start.Degrees, end.Degrees))
            {
                return start;
            }

            return Angle.FromDegrees(start.Degrees + (linear.Value * (end.Degrees - start.Degrees)));
        }
    }
}
