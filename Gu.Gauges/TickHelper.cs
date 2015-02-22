using System.Windows;

namespace Gu.Gauges
{
    using System;
    using System.Collections.Generic;

    internal static class TickHelper
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

        internal static double ToPos(double tick, double minimum, double maximum, double min, double max)
        {
            var dv = (tick - minimum) / (maximum - minimum);
            var a = dv * (max - min) + min;
            return a;
        }

        internal static Point ToPos(double tick, double minimum, double maximum, Line line)
        {
            var dv = (tick - minimum) / (maximum - minimum);
            var v = line.EndPoint - line.StartPoint;
            return line.StartPoint + dv * v;
        }
    }
}
