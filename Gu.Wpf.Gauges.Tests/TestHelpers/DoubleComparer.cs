namespace Gu.Wpf.Gauges.Tests
{
    using System.Collections.Generic;

    public class DoubleComparer : Comparer<double>
    {
#pragma warning disable SA1206 // Declaration keywords must follow order
        public new static DoubleComparer Default { get; } = new DoubleComparer();
#pragma warning restore SA1206 // Declaration keywords must follow order

        public override int Compare(double x, double y)
        {
            return DoubleUtil.AreClose(x, y) ? 0 : x.CompareTo(y);
        }
    }
}
