namespace Gu.Wpf.Gauges
{
    using System.Windows.Media;

    internal static class RotateTransformExt
    {
        internal static Line Transform(this RotateTransform transform, Line line)
        {
            var p1 = transform.Transform(line.StartPoint);
            var p2 = transform.Transform(line.EndPoint);
            return new Line(p1, p2);
        }
    }
}
