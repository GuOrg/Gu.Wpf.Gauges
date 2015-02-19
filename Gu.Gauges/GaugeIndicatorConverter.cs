using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Gu.Gauges
{
    public class GaugeIndicatorConverter : IValueConverter
    {
        public DataTemplate Circle { get; set; }

        public DataTemplate Rectangle { get; set; }

        public DataTemplate Triangle { get; set; }

        public DataTemplate Line { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var marker = (Marker)value;

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

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}