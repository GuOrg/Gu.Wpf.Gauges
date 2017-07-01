namespace Gu.Wpf.Gauges
{
    using System;
    using System.Windows;

    internal static class RectExt
    {
        internal static void SetLeft(ref Rect rect, double left)
        {
            var right = rect.Right;
            rect.X = left;
            rect.Width = Math.Max(0, right - left);
        }

        internal static Rect TrimLeft(this Rect rect, double left)
        {
            if (rect.Left < left)
            {
                SetLeft(ref rect, left);
            }

            return rect;
        }

        internal static Rect WithLeft(this Rect rect, double left)
        {
            SetLeft(ref rect, left);
            return rect;
        }

        internal static void SetRight(ref Rect rect, double right)
        {
            rect.X = Math.Min(rect.Left, right);
            rect.Width = Math.Max(0, right - rect.X);
        }

        internal static Rect TrimRight(this Rect rect, double right)
        {
            if (rect.Right > right)
            {
                SetRight(ref rect, right);
            }

            return rect;
        }

        internal static Rect WithRight(this Rect rect, double right)
        {
            rect.Width = Math.Max(0, right - rect.Left);
            return rect;
        }

        internal static void SetTop(ref Rect rect, double top)
        {
            var bottom = rect.Bottom;
            rect.Y = top;
            rect.Height = Math.Max(0, bottom - top);
        }

        internal static Rect TrimTop(this Rect rect, double top)
        {
            if (rect.Top < top)
            {
                SetTop(ref rect, top);
            }

            return rect;
        }

        internal static Rect WithTop(this Rect rect, double top)
        {
            var bottom = rect.Bottom;
            rect.Y = top;
            rect.Height = Math.Max(0, bottom - top);
            return rect;
        }

        internal static void SetBottom(ref Rect rect, double bottom)
        {
            rect.Y = Math.Min(rect.Top, bottom);
            rect.Height = Math.Max(0, bottom - rect.Top);
        }

        internal static Rect TrimBottom(this Rect rect, double bottom)
        {
            if (rect.Bottom > bottom)
            {
                SetBottom(ref rect, bottom);
            }

            return rect;
        }

        internal static Rect WithBottom(this Rect rect, double bottom)
        {
            rect.Height = Math.Max(0, bottom - rect.Top);
            return rect;
        }

        internal static Point MidPoint(this Rect rect)
        {
            var v = rect.TopRight - rect.BottomLeft;
            var offset = 0.5 * v;
            return rect.BottomLeft + offset;
        }

        internal static Vector FindTranslationToCenter(this Rect rect, Size size)
        {
            var rectMid = rect.MidPoint();
            var sizeMid = size.MidPoint();
            return sizeMid - rectMid;
        }

        internal static bool IsNaN(this Rect rect)
        {
            return double.IsNaN(rect.X) ||
                   double.IsNaN(rect.Y) ||
                   double.IsNaN(rect.Width) ||
                   double.IsNaN(rect.Height);
        }

        internal static bool IsInfinity(this Rect rect)
        {
            return double.IsInfinity(rect.X) ||
                   double.IsInfinity(rect.Y) ||
                   double.IsInfinity(rect.Width) ||
                   double.IsInfinity(rect.Height);
        }
    }
}
