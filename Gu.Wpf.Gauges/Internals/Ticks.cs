namespace Gu.Wpf.Gauges
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class Ticks
    {
        internal static IEnumerable<double> Create(double minimum, double maximum, double tickFrequency, TickSnap tickSnap)
        {
            if (tickFrequency <= 0 ||
                maximum <= minimum)
            {
                return Enumerable.Empty<double>();
            }

            switch (tickSnap)
            {
                case TickSnap.TickFrequency:
                    {
                        var snappedMin = Math.Ceiling(minimum / tickFrequency) * tickFrequency;
                        return Create(snappedMin, maximum, tickFrequency);
                    }

                case TickSnap.Minimum:
                    {
                        return Create(minimum, maximum, tickFrequency);
                    }

                case TickSnap.Maximum:
                    {
                        var n = Math.Floor((maximum - minimum) / tickFrequency);
                        var snappedMin = maximum - (n * tickFrequency);
                        return Create(snappedMin, maximum, tickFrequency);
                    }

                default:
                    throw new ArgumentOutOfRangeException(nameof(tickSnap), tickSnap, null);
            }
        }

        internal static IEnumerable<double> Create(double from, double to, double step)
        {
            while (DoubleUtil.LessThanOrClose(from, to))
            {
                yield return from;
                from += step;
            }
        }
    }
}
