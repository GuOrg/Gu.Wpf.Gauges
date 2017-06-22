namespace Gu.Gauges
{
    using System.Windows;

    public static class LineExt
    {
        public static readonly DependencyProperty StartPointProperty = DependencyProperty.RegisterAttached(
            "StartPoint",
            typeof(Point),
            typeof(LineExt),
            new PropertyMetadata(default(Point), OnStartPointChanged));

        public static readonly DependencyProperty EndPointProperty = DependencyProperty.RegisterAttached(
            "EndPoint",
            typeof(Point),
            typeof(LineExt),
            new PropertyMetadata(default(Point), OnEndPointChanged));

        public static void SetEndPoint(System.Windows.Shapes.Line element, Point value)
        {
            element.SetValue(EndPointProperty, value);
        }

        public static Point GetEndPoint(System.Windows.Shapes.Line element)
        {
            return (Point)element.GetValue(EndPointProperty);
        }

        public static void SetStartPoint(System.Windows.Shapes.Line element, Point value)
        {
            element.SetValue(StartPointProperty, value);
        }

        public static Point GetStartPoint(System.Windows.Shapes.Line element)
        {
            return (Point)element.GetValue(StartPointProperty);
        }

        private static void OnEndPointChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var l = o as System.Windows.Shapes.Line;
            if (l == null)
            {
                return;
            }

            var p = (Point)e.NewValue;
            l.SetCurrentValue(System.Windows.Shapes.Line.X2Property, p.X);
            l.SetCurrentValue(System.Windows.Shapes.Line.Y2Property, p.Y);
        }

        private static void OnStartPointChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var l = o as System.Windows.Shapes.Line;
            if (l == null)
            {
                return;
            }

            var p = (Point)e.NewValue;
            l.SetCurrentValue(System.Windows.Shapes.Line.X1Property, p.X);
            l.SetCurrentValue(System.Windows.Shapes.Line.Y1Property, p.Y);
        }
    }
}