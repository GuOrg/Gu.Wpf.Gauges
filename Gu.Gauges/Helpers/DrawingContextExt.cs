using System.Windows.Media;

namespace Gu.Gauges
{
    internal static class DrawingContextExt
    {
        internal static void DrawLine(this DrawingContext dc, Pen pen, Line l)
        {
            dc.DrawLine(pen, l.StartPoint, l.EndPoint);
        }
    }
}