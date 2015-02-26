namespace Gu.Gauges
{
    using System.Windows.Media;

    internal static class DrawingContextExt
    {
        internal static void DrawLine(this DrawingContext dc, Pen pen, Line l)
        {
            dc.DrawLine(pen, l.StartPoint, l.EndPoint);
        }

        internal static void DrawText(this DrawingContext dc, FormattedText text, TextPosition textPosition)
        {
            //dc.DrawEllipse(Brushes.Black, null, textPosition.Point, 2, 2); // Used to debug
            if (textPosition.IsTransformed)
            {
                dc.PushTransform(textPosition.Transform);
            }
            dc.DrawText(text, textPosition.Point);
            if (textPosition.IsTransformed)
            {
                dc.Pop();
            }
        }
    }
}