namespace Gu.Wpf.Gauges.Tests.TestHelpers
{
    using System.Collections.Generic;

    public class DoubleComparer : Comparer<double>
    {
        public new static DoubleComparer Default = new DoubleComparer();

        public override int Compare(double x, double y)
        {
            return DoubleUtil.AreClose(x, y) ? 0 : x.CompareTo(y);
        }
    }
}
