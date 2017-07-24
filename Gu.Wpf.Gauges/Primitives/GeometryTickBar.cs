namespace Gu.Wpf.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public abstract class GeometryTickBar : TickBarBase
    {
        public static readonly DependencyProperty FillProperty = Shape.FillProperty.AddOwner(
            typeof(GeometryTickBar),
            new FrameworkPropertyMetadata(
                default(Brush),
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty StrokeProperty = Shape.StrokeProperty.AddOwner(
            typeof(GeometryTickBar),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender,
                (d, e) => ((GeometryTickBar)d).ResetPen()));

        public static readonly DependencyProperty StrokeThicknessProperty = Shape.StrokeThicknessProperty.AddOwner(
            typeof(GeometryTickBar),
            new FrameworkPropertyMetadata(
                1.0d,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                (d, e) => ((GeometryTickBar)d).pen = null));

        /// <summary>
        /// StrokeStartLineCap property
        /// </summary>
        public static readonly DependencyProperty StrokeStartLineCapProperty = DependencyProperty.Register(
            "StrokeStartLineCap",
            typeof(PenLineCap),
            typeof(GeometryTickBar),
            new FrameworkPropertyMetadata(
                PenLineCap.Flat,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                (d, e) => ((GeometryTickBar)d).pen = null),
            ValidateEnums.IsPenLineCapValid);

        /// <summary>
        /// StrokeEndLineCap property
        /// </summary>
        public static readonly DependencyProperty StrokeEndLineCapProperty = DependencyProperty.Register(
            "StrokeEndLineCap",
            typeof(PenLineCap),
            typeof(GeometryTickBar),
            new FrameworkPropertyMetadata(
                PenLineCap.Flat,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                (d, e) => ((GeometryTickBar)d).pen = null),
            ValidateEnums.IsPenLineCapValid);

        /// <summary>
        /// StrokeDashCap property
        /// </summary>
        public static readonly DependencyProperty StrokeDashCapProperty =
            DependencyProperty.Register(
                "StrokeDashCap",
                typeof(PenLineCap),
                typeof(GeometryTickBar),
                new FrameworkPropertyMetadata(
                    PenLineCap.Flat,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    (d, e) => ((GeometryTickBar)d).pen = null),
                ValidateEnums.IsPenLineCapValid);

        /// <summary>
        /// StrokeLineJoin property
        /// </summary>
        public static readonly DependencyProperty StrokeLineJoinProperty =
            DependencyProperty.Register(
                "StrokeLineJoin",
                typeof(PenLineJoin),
                typeof(GeometryTickBar),
                new FrameworkPropertyMetadata(
                    PenLineJoin.Miter,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    (d, e) => ((GeometryTickBar)d).pen = null),
                ValidateEnums.IsPenLineJoinValid);

        /// <summary>
        /// StrokeMiterLimit property
        /// </summary>
        public static readonly DependencyProperty StrokeMiterLimitProperty =
            DependencyProperty.Register(
                "StrokeMiterLimit",
                typeof(double),
                typeof(GeometryTickBar),
                new FrameworkPropertyMetadata(
                    10.0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    (d, e) => ((GeometryTickBar)d).pen = null));

        /// <summary>
        /// StrokeDashOffset property
        /// </summary>
        public static readonly DependencyProperty StrokeDashOffsetProperty =
            DependencyProperty.Register(
                "StrokeDashOffset",
                typeof(double),
                typeof(GeometryTickBar),
                new FrameworkPropertyMetadata(
                    0.0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    (d, e) => ((GeometryTickBar)d).pen = null));

        /// <summary>
        /// StrokeDashArray property
        /// </summary>
        public static readonly DependencyProperty StrokeDashArrayProperty =
            DependencyProperty.Register(
                "StrokeDashArray",
                typeof(DoubleCollection),
                typeof(GeometryTickBar),
                new FrameworkPropertyMetadata(
                    default(DoubleCollection),
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    (d, e) => ((GeometryTickBar)d).pen = null));

        private Pen pen;

        protected GeometryTickBar()
        {
            this.StrokeDashArray = new DoubleCollection();
        }

        /// <summary>
        /// Gets or sets the <see cref="T:System.Windows.Media.Brush" /> that specifies how the shape's interior is painted.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Media.Brush" /> that describes how the shape's interior is painted. The default is HotPink.
        /// </returns>
        public Brush Fill
        {
            get => (Brush)this.GetValue(FillProperty);
            set => this.SetValue(FillProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="T:System.Windows.Media.Brush" /> that specifies how the <see cref="T:BlockBar" /> outline is painted.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Media.Brush" /> that specifies how the <see cref="T:BlockBar" /> outline is painted. The default is null.
        /// </returns>
        public Brush Stroke
        {
            get => (Brush)this.GetValue(StrokeProperty);
            set => this.SetValue(StrokeProperty, value);
        }

        /// <summary>
        /// Gets or sets the width of the <see cref="T:BlockBar" /> outline.
        /// </summary>
        /// <returns>
        /// The width of the <see cref="T:BlockBar" /> outline.
        /// </returns>
        public double StrokeThickness
        {
            get => (double)this.GetValue(StrokeThicknessProperty);
            set => this.SetValue(StrokeThicknessProperty, value);
        }

        /// <summary>
        /// StrokeStartLineCap property
        /// </summary>
        public PenLineCap StrokeStartLineCap
        {
            get => (PenLineCap)this.GetValue(StrokeStartLineCapProperty);
            set => this.SetValue(StrokeStartLineCapProperty, value);
        }

        /// <summary>
        /// StrokeEndLineCap property
        /// </summary>
        public PenLineCap StrokeEndLineCap
        {
            get => (PenLineCap)this.GetValue(StrokeEndLineCapProperty);
            set => this.SetValue(StrokeEndLineCapProperty, value);
        }

        /// <summary>
        /// StrokeDashCap property
        /// </summary>
        public PenLineCap StrokeDashCap
        {
            get => (PenLineCap)this.GetValue(StrokeDashCapProperty);
            set => this.SetValue(StrokeDashCapProperty, value);
        }

        /// <summary>
        /// StrokeLineJoin property
        /// </summary>
        public PenLineJoin StrokeLineJoin
        {
            get => (PenLineJoin)this.GetValue(StrokeLineJoinProperty);
            set => this.SetValue(StrokeLineJoinProperty, value);
        }

        /// <summary>
        /// StrokeDashOffset property
        /// </summary>
        public double StrokeDashOffset
        {
            get => (double)this.GetValue(StrokeDashOffsetProperty);
            set => this.SetValue(StrokeDashOffsetProperty, value);
        }

        /// <summary>
        /// StrokeMiterLimit property
        /// </summary>
        public double StrokeMiterLimit
        {
            get => (double)this.GetValue(StrokeMiterLimitProperty);
            set => this.SetValue(StrokeMiterLimitProperty, value);
        }

        /// <summary>
        /// StrokeDashArray property
        /// </summary>
        public DoubleCollection StrokeDashArray
        {
            get => (DoubleCollection)this.GetValue(StrokeDashArrayProperty);
            set => this.SetValue(StrokeDashArrayProperty, value);
        }

        /// <summary>
        /// Get the geometry that defines this shape
        /// </summary>
        protected abstract Geometry DefiningGeometry { get; }

        protected bool CanCreatePen
        {
            get
            {
                var strokeThickness = this.StrokeThickness;
                return (this.Stroke != null) &&
                       !DoubleUtil.IsNaN(strokeThickness) &&
                       !DoubleUtil.IsZero(strokeThickness);
            }
        }

        protected Pen Pen
        {
            get
            {
                if (this.pen == null)
                {
                    if (this.CanCreatePen)
                    {
                        // This pen is internal to the system and
                        // must not participate in freezable treeness
                        this.pen = new Pen
                        {
                            //// CanBeInheritanceContext = false;
                            Thickness = this.GetStrokeThickness(),
                            Brush = this.Stroke,
                            StartLineCap = this.StrokeStartLineCap,
                            EndLineCap = this.StrokeEndLineCap,
                            DashCap = this.StrokeDashCap,
                            LineJoin = this.StrokeLineJoin,
                            MiterLimit = this.StrokeMiterLimit,
                            DashStyle = this.StrokeDashCap != PenLineCap.Flat && this.StrokeDashArray.Count > 0
                                           ? new DashStyle(this.StrokeDashArray, this.StrokeDashOffset)
                                           : DashStyles.Solid
                        };
                    }
                }

                return this.pen;
            }
        }

        protected virtual double GetStrokeThickness()
        {
            return this.CanCreatePen
                ? Math.Abs(this.StrokeThickness)
                : 0;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var strokeThickness = this.GetStrokeThickness();
            return new Size(strokeThickness, strokeThickness);
        }

        protected override void OnRender(DrawingContext dc)
        {
            var geometry = this.DefiningGeometry;
            if (geometry == null ||
                ReferenceEquals(geometry, Geometry.Empty))
            {
                return;
            }

            dc.DrawGeometry(this.Fill, this.Pen, geometry);
        }

        protected void ResetPen()
        {
            this.pen = null;
        }
    }
}