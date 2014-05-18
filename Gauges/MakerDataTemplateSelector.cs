using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Gauges
{
    public class GaugeIndicatorConverter : IValueConverter
    {
        public DataTemplate Circle { get; set; }
        public DataTemplate Rectangle { get; set; }
        public DataTemplate Triangle { get; set; }
        public DataTemplate Line { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var marker = (Marker)value;

            switch (marker)
            {
                case Marker.Circle:
                    return Circle;
                case Marker.Rectangle:
                    return Rectangle;
                case Marker.Triangle:
                    return Triangle;
                case Marker.Line:
                    return Line;
                default:
                    break;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

   public class MakerDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Circle { get; set; }
        public DataTemplate Rectangle { get; set; }
        public DataTemplate Triangle { get; set; }
        public DataTemplate Line { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var marker = (Marker)item;
            switch (marker)
            {
                case Marker.Circle:
                    return Circle;
                case Marker.Rectangle:
                    return Rectangle;
                case Marker.Triangle:
                    return Triangle;
                case Marker.Line:
                    return Line;
                default:
                    break;
            }

            return null;
        }
    }

}
