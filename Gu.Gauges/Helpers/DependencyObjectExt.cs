namespace Gu.Gauges.Helpers
{
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Media;

    public static class DependencyObjectExt
    {
        public static IEnumerable<DependencyObject> VisualAncestors(this DependencyObject o)
        {
            var parent = VisualTreeHelper.GetParent(o);
            while (parent != null)
            {
                yield return parent;
                parent = VisualTreeHelper.GetParent(parent);
            }
        }
    }
}
