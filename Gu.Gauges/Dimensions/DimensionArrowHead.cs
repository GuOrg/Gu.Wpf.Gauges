namespace Gu.Gauges
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class DimensionArrowHead : Shape
    {
        public static readonly DependencyProperty PointProperty = DependencyProperty.Register(
            "Point",
            typeof(Point),
            typeof(DimensionArrowHead),
            new FrameworkPropertyMetadata(
                new Point(double.NaN, double.NaN),
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty DirectionProperty = DependencyProperty.Register(
            "Direction",
            typeof(Vector),
            typeof(DimensionArrowHead),
            new FrameworkPropertyMetadata(
                new Vector(double.NaN, double.NaN),
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register(
            "Scale",
            typeof(double),
            typeof(DimensionArrowHead),
            new FrameworkPropertyMetadata(
                1.0,
                FrameworkPropertyMetadataOptions.AffectsRender));

        private const double TerminatorLength = 1.8 * 2;
        private const double TerminatorWidth = 1 * 2;
        private static readonly RotateTransform Rotate90Cw = new RotateTransform(90);

        [TypeConverter(typeof(PointConverter))]
        public Point Point
        {
            get
            {
                return (Point)this.GetValue(PointProperty);
            }
            set
            {
                this.SetValue(PointProperty, value);
            }
        }
        [TypeConverter(typeof(VectorConverter))]
        public Vector Direction
        {
            get
            {
                return (Vector)this.GetValue(DirectionProperty);
            }
            set
            {
                this.SetValue(DirectionProperty, value);
            }
        }
        [TypeConverter(typeof(LengthConverter))]
        public double Scale
        {
            get
            {
                return (double)this.GetValue(ScaleProperty);
            }
            set
            {
                this.SetValue(ScaleProperty, value);
            }
        }
        protected override Geometry DefiningGeometry
        {
            get
            {
                var geometry = new StreamGeometry
                               {
                                   FillRule = FillRule.EvenOdd
                               };

                using (StreamGeometryContext context = geometry.Open())
                {
                    var tp = this.Point - (this.Scale * TerminatorLength * this.Direction);
                    var dir = Vector.Multiply(this.Direction, Rotate90Cw.Value);
                    var p1 = tp + (((this.Scale * TerminatorWidth) / 2) * dir);
                    var p2 = tp - ((this.Scale * TerminatorWidth / 2) * dir);
                    context.BeginFigure(this.Point, true, false);
                    context.LineTo(p1, true, true);
                    context.LineTo(p2, true, true);
                    context.LineTo(this.Point, true, true);
                }
                geometry.Freeze();

                return geometry;
            }
        }
    }
}