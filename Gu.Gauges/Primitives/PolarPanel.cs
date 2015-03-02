namespace Gu.Gauges
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class PolarPanel : Panel
    {
        public static readonly DependencyProperty RadiusProperty = DependencyProperty.RegisterAttached(
            "Radius",
            typeof(double),
            typeof(PolarPanel),
            new PropertyMetadata(double.NaN, OnPositionChanged));

        public static readonly DependencyProperty AngleProperty = DependencyProperty.RegisterAttached(
            "Angle",
            typeof(double),
            typeof(PolarPanel),
            new PropertyMetadata(double.NaN, OnPositionChanged));

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
                if (child == null)
                {
                    continue;
                }
                child.Measure(childConstraint);
            }

            return new Size();
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            var arc = Arc.Fill(arrangeSize, 0, 360);
            arc = new Arc(arc.Centre, 0, 360, 0, false);
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
                    child.Arrange(new Rect());
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

        private static void OnPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uie = d as UIElement;
            if (uie != null)
            {
                var panel = VisualTreeHelper.GetParent(uie) as PolarPanel;
                if (panel != null)
                {
                    panel.InvalidateArrange();
                }
            }
        }
    }
}
