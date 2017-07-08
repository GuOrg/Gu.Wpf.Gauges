namespace Gu.Wpf.Gauges
{
    using System.Windows.Media;

    internal static class DrawingContextExt
    {
        internal static void DrawLine(this DrawingContext dc, Pen pen, Line l)
        {
            dc.DrawLine(pen, l.StartPoint, l.EndPoint);
        }
    }
}