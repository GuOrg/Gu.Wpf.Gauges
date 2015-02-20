using System;
using System.Collections.Generic;

namespace Gu.Gauges
{
    internal static class AngleHelper
    {
        internal static IEnumerable<double> CreateTicks(double minimum, double maximum, double tickFrequency)
        {
            if (tickFrequency <= 0)
            {
                yield break;
            }

            var min = Math.Min(minimum, maximum);
            var max = Math.Max(minimum, maximum);
            var threshold = 0.1 * tickFrequency + max;
            for (var v = min; v < threshold; v += tickFrequency)
            {
                yield return v;
            }
        }

        internal static double ToAngle(double tick, double minimum, double maximum, double minAngle, double maxAngle)
        {
            var dv = (tick - minimum) / (maximum - minimum);
            var a = dv * (maxAngle - minAngle) + minAngle;
            return a;
        }
    }
}
