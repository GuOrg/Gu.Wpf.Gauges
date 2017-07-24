namespace Gu.Wpf.Gauges
{
    using System.Windows;
    using System.Windows.Media;

    public static class Overflow
    {
        public static void RegisterOverflow(this UIElement element, Thickness overflow)
        {
            while (element != null)
            {
                if (element is Gauge gauge)
                {
                    gauge.RegisterOverflow(overflow);
                    return;
                }

                element = VisualTreeHelper.GetParent(element) as UIElement;
            }
        }
    }
}
