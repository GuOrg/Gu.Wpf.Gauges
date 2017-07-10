namespace Gu.Wpf.Gauges
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;

    internal static class ThicknessExt
    {
        internal static double Width(this Thickness thickness) => thickness.Left + thickness.Right;

        internal static double Height(this Thickness thickness) => thickness.Top + thickness.Bottom;

        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        internal static bool IsZero(this Thickness thickness) => thickness.Left == 0 &&
                                                                 thickness.Right == 0 &&
                                                                 thickness.Top == 0 &&
                                                                 thickness.Bottom == 0;
    }
}