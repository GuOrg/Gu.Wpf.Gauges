using System.Windows;
using System.Windows.Controls;

namespace Gu.Gauges
{
    /// <summary>
    /// Marker DataTemplate Selector
    /// </summary>
    public class MakerDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Circle { get; set; }

        public DataTemplate Rectangle { get; set; }

        public DataTemplate Triangle { get; set; }

        public DataTemplate Line { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
                return null;

            var marker = (Marker)item;
            switch (marker)
            {
                case Marker.Circle:
                    return this.Circle;

                case Marker.Rectangle:
                    return this.Rectangle;

                case Marker.Triangle:
                    return this.Triangle;

                case Marker.Line:
                    return this.Line;

                default:
                    break;
            }

            return null;
        }
    }
}