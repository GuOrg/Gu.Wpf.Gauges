namespace Gu.Wpf.Gauges
{
    using System.Windows.Controls.Primitives;

    internal static class TickBarPlacementExt
    {
        internal static bool IsHorizontal(this TickBarPlacement placement) => placement == TickBarPlacement.Top ||
                                                                              placement == TickBarPlacement.Bottom;
    }
}