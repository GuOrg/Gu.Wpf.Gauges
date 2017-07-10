namespace Gu.Wpf.Gauges
{
    using System.Windows;

    internal static class ThicknessExt
    {
        internal static double Width(this Thickness thickness) => thickness.Left + thickness.Right;

        internal static double Height(this Thickness thickness) => thickness.Top + thickness.Bottom;
    }
}