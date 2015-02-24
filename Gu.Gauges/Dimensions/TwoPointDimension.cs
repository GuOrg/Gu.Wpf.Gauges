namespace Gu.Gauges
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public abstract class TwoPointDimension : Control
    {
        public static readonly DependencyProperty P1Property = DependencyProperty.Register(
            "P1",
            typeof(Point),
            typeof(TwoPointDimension),
            new FrameworkPropertyMetadata(
                new Point(double.NaN, double.NaN),
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty P2Property = DependencyProperty.Register(
            "P2",
            typeof(Point),
            typeof(TwoPointDimension),
            new FrameworkPropertyMetadata(
                new Point(double.NaN, double.NaN),
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty OffsetPointProperty = DependencyProperty.Register(
            "OffsetPoint",
            typeof(Point?),
            typeof(TwoPointDimension),
            new FrameworkPropertyMetadata(
                new Point(double.NaN, double.NaN),
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty OffsetProperty = DependencyProperty.Register(
            "Offset",
            typeof(double),
            typeof(TwoPointDimension),
            new FrameworkPropertyMetadata(
                12.0,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty OffsetDirectionProperty = DependencyProperty.Register(
            "OffsetDirection",
            typeof(Vector),
            typeof(TwoPointDimension),
            new FrameworkPropertyMetadata(
                new Vector(double.NaN, double.NaN),
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register(
            "Scale",
            typeof(double),
            typeof(TwoPointDimension),
            new FrameworkPropertyMetadata(
                1.0,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        private static readonly DependencyPropertyKey DimensionLineP1PropertyKey = DependencyProperty.RegisterReadOnly(
            "DimensionLineP1",
            typeof(Point),
            typeof(TwoPointDimension),
            new FrameworkPropertyMetadata(
                default(Point),
                FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty DimensionLineP1Property = DimensionLineP1PropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey DimensionLineP2PropertyKey = DependencyProperty.RegisterReadOnly(
            "DimensionLineP2",
            typeof(Point),
            typeof(TwoPointDimension),
            new FrameworkPropertyMetadata(
                default(Point),
                FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty DimensionLineP2Property = DimensionLineP2PropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey BoundaryLine1P1PropertyKey = DependencyProperty.RegisterReadOnly(
            "BoundaryLine1P1",
            typeof(Point),
            typeof(TwoPointDimension),
            new FrameworkPropertyMetadata(
                default(Point),
                FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty BoundaryLine1P1Property = BoundaryLine1P1PropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey BoundaryLine1P2PropertyKey = DependencyProperty.RegisterReadOnly(
            "BoundaryLine1P2",
            typeof(Point),
            typeof(TwoPointDimension),
            new FrameworkPropertyMetadata(
                default(Point),
                FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty BoundaryLine1P2Property = BoundaryLine1P2PropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey BoundaryLine2P1PropertyKey = DependencyProperty.RegisterReadOnly(
            "BoundaryLine2P1",
            typeof(Point),
            typeof(TwoPointDimension),
            new FrameworkPropertyMetadata(
                default(Point),
                FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty BoundaryLine2P1Property = BoundaryLine2P1PropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey BoundaryLine2P2PropertyKey = DependencyProperty.RegisterReadOnly(
            "BoundaryLine2P2",
            typeof(Point),
            typeof(TwoPointDimension),
            new FrameworkPropertyMetadata(
                default(Point),
                FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty BoundaryLine2P2Property = BoundaryLine2P2PropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey ArrowHead1DirectionPropertyKey = DependencyProperty
            .RegisterReadOnly(
                "ArrowHead1Direction",
                typeof(Vector),
                typeof(TwoPointDimension),
                new FrameworkPropertyMetadata(
                    default(Vector),
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty ArrowHead1DirectionProperty = ArrowHead1DirectionPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey ArrowHead2DirectionPropertyKey = DependencyProperty.RegisterReadOnly(
                "ArrowHead2Direction",
                typeof(Vector),
                typeof(TwoPointDimension),
                new FrameworkPropertyMetadata(
                    default(Vector),
                    FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty ArrowHead2DirectionProperty = ArrowHead2DirectionPropertyKey.DependencyProperty;
        
        protected const double OriginOffset = 1;
        protected const double Extension = 2;
        protected static readonly RotateTransform Rotate90Cw = new RotateTransform(90);
        protected static readonly RotateTransform Rotate90Ccw = new RotateTransform(-90);

        static TwoPointDimension()
        {
            Shape.StrokeThicknessProperty.AddOwner(
                typeof(TwoPointDimension),
                new FrameworkPropertyMetadata(
                    0.5,
                    FrameworkPropertyMetadataOptions.AffectsRender));
        }

        [TypeConverter(typeof(PointConverter))]
        public Point P1
        {
            get
            {
                return (Point)this.GetValue(P1Property);
            }
            set
            {
                this.SetValue(P1Property, value);
            }
        }
        [TypeConverter(typeof(PointConverter))]
        public Point P2
        {
            get
            {
                return (Point)this.GetValue(P2Property);
            }
            set
            {
                this.SetValue(P2Property, value);
            }
        }
        [TypeConverter(typeof(PointConverter))]
        public Point OffsetPoint
        {
            get
            {
                return (Point)this.GetValue(OffsetPointProperty);
            }
            set
            {
                this.SetValue(OffsetPointProperty, value);
            }
        }
        [TypeConverter(typeof(LengthConverter))]
        public double Offset
        {
            get
            {
                return (double)this.GetValue(OffsetProperty);
            }
            set
            {
                this.SetValue(OffsetProperty, value);
            }
        }
        [TypeConverter(typeof(VectorConverter))]
        public Vector OffsetDirection
        {
            get
            {
                return (Vector)this.GetValue(OffsetDirectionProperty);
            }
            set
            {
                this.SetValue(OffsetDirectionProperty, value);
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
        public Point DimensionLineP1
        {
            get
            {
                return (Point)this.GetValue(DimensionLineP1Property);
            }
            protected set
            {
                this.SetValue(DimensionLineP1PropertyKey, value);
            }
        }
        public Point DimensionLineP2
        {
            get
            {
                return (Point)this.GetValue(DimensionLineP2Property);
            }
            protected set
            {
                this.SetValue(DimensionLineP2PropertyKey, value);
            }
        }
        public Point BoundaryLine1P1
        {
            get
            {
                return (Point)this.GetValue(BoundaryLine1P1Property);
            }
            protected set
            {
                this.SetValue(BoundaryLine1P1PropertyKey, value);
            }
        }
        public Point BoundaryLine1P2
        {
            get
            {
                return (Point)this.GetValue(BoundaryLine1P2Property);
            }
            protected set
            {
                this.SetValue(BoundaryLine1P2PropertyKey, value);
            }
        }
        public Point BoundaryLine2P1
        {
            get
            {
                return (Point)this.GetValue(BoundaryLine2P1Property);
            }
            protected set
            {
                this.SetValue(BoundaryLine2P1PropertyKey, value);
            }
        }
        public Point BoundaryLine2P2
        {
            get
            {
                return (Point)this.GetValue(BoundaryLine2P2Property);
            }
            protected set
            {
                this.SetValue(BoundaryLine2P2PropertyKey, value);
            }
        }
        public Vector ArrowHead1Direction
        {
            get
            {
                return (Vector)this.GetValue(ArrowHead1DirectionProperty);
            }
            protected set
            {
                this.SetValue(ArrowHead1DirectionPropertyKey, value);
            }
        }
        public Vector ArrowHead2Direction
        {
            get
            {
                return (Vector)this.GetValue(ArrowHead2DirectionProperty);
            }
            protected set
            {
                this.SetValue(ArrowHead2DirectionPropertyKey, value);
            }
        }
    }
}