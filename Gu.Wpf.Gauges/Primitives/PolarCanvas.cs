namespace Gu.Wpf.Gauges
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class PolarCanvas : Panel
    {
        public static readonly DependencyProperty RadiusProperty = DependencyProperty.RegisterAttached(
            "Radius",
            typeof(double),
            typeof(PolarCanvas),
            new PropertyMetadata(double.NaN, OnRadiusChanged));

        public static readonly DependencyProperty AngleProperty = DependencyProperty.RegisterAttached(
            "Angle",
            typeof(double),
            typeof(PolarCanvas),
            new PropertyMetadata(double.NaN, OnAngleChanged));

        public static void SetRadius(DependencyObject element, double value)
        {
            element.SetValue(RadiusProperty, value);
        }

        public static double GetRadius(DependencyObject element)
        {
            return (double)element.GetValue(RadiusProperty);
        }

        public static void SetAngle(DependencyObject element, double value)
        {
            element.SetValue(AngleProperty, value);
        }

        public static double GetAngle(DependencyObject element)
        {
            return (double)element.GetValue(AngleProperty);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            Size childConstraint = new Size(double.PositiveInfinity, double.PositiveInfinity);

            foreach (UIElement child in this.InternalChildren)
            {
                child?.Measure(childConstraint);
            }

            return default(Size);
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            var arc = ArcInfo.Fill(arrangeSize, 0, 360);
            arc = new ArcInfo(arc.Center, 0, 360, 0, isDirectionReversed: false);
            foreach (UIElement child in this.InternalChildren)
            {
                if (child == null)
                {
                    continue;
                }

                double r = GetRadius(child);
                double a = GetAngle(child);

                if (double.IsNaN(r) || double.IsNaN(a))
                {
                    child.Arrange(default(Rect));
                }
                else
                {
                    var p = arc.GetPoint(a, r);
                    var mp = child.DesiredSize.MidPoint();
                    var v = new Vector(mp.X, mp.Y);
                    var cp = p - v;
                    child.Arrange(new Rect(cp, child.DesiredSize));
                }
            }

            return arrangeSize;
        }

        private static void OnAngleChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            OnPositionChanged(dependencyObject);
        }

        private static void OnRadiusChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            OnPositionChanged(dependencyObject);
        }

        private static void OnPositionChanged(DependencyObject d)
        {
            var uie = d as UIElement;
            if (uie == null)
            {
                return;
            }

            var panel = VisualTreeHelper.GetParent(uie) as PolarCanvas;
            panel?.InvalidateArrange();
        }
    }
}
