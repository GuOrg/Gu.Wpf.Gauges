namespace Gu.Wpf.Gauges
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class Ring : FrameworkElement
    {
        public static readonly DependencyProperty ThicknessProperty = AngularGeometryBar.ThicknessProperty.AddOwner(
            typeof(Ring),
            new FrameworkPropertyMetadata(
                10.0,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty FillProperty = Shape.FillProperty.AddOwner(
            typeof(Ring),
            new FrameworkPropertyMetadata(
                default(Brush),
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty StrokeProperty = Shape.StrokeProperty.AddOwner(
            typeof(Ring),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender,
                (d, e) => ((Ring)d).pen = null));

        public static readonly DependencyProperty StrokeThicknessProperty = Shape.StrokeThicknessProperty.AddOwner(
            typeof(Ring),
            new FrameworkPropertyMetadata(
                1.0d,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                (d, e) => ((Ring)d).pen = null));

        /// <summary>
        /// StrokeStartLineCap property
        /// </summary>
        public static readonly DependencyProperty StrokeStartLineCapProperty = DependencyProperty.Register(
            "StrokeStartLineCap",
            typeof(PenLineCap),
            typeof(Ring),
            new FrameworkPropertyMetadata(
                PenLineCap.Flat,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                (d, e) => ((Ring)d).pen = null),
            ValidateEnums.IsPenLineCapValid);

        /// <summary>
        /// StrokeEndLineCap property
        /// </summary>
        public static readonly DependencyProperty StrokeEndLineCapProperty = DependencyProperty.Register(
            "StrokeEndLineCap",
            typeof(PenLineCap),
            typeof(Ring),
            new FrameworkPropertyMetadata(
                PenLineCap.Flat,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                (d, e) => ((Ring)d).pen = null),
            ValidateEnums.IsPenLineCapValid);

        /// <summary>
        /// StrokeDashCap property
        /// </summary>
        public static readonly DependencyProperty StrokeDashCapProperty =
            DependencyProperty.Register(
                "StrokeDashCap",
                typeof(PenLineCap),
                typeof(Ring),
                new FrameworkPropertyMetadata(
                    PenLineCap.Flat,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    (d, e) => ((Ring)d).pen = null),
                ValidateEnums.IsPenLineCapValid);

        /// <summary>
        /// StrokeLineJoin property
        /// </summary>
        public static readonly DependencyProperty StrokeLineJoinProperty =
            DependencyProperty.Register(
                "StrokeLineJoin",
                typeof(PenLineJoin),
                typeof(Ring),
                new FrameworkPropertyMetadata(
                    PenLineJoin.Miter,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    (d, e) => ((Ring)d).pen = null),
                ValidateEnums.IsPenLineJoinValid);

        /// <summary>
        /// StrokeMiterLimit property
        /// </summary>
        public static readonly DependencyProperty StrokeMiterLimitProperty =
            DependencyProperty.Register(
                "StrokeMiterLimit",
                typeof(double),
                typeof(Ring),
                new FrameworkPropertyMetadata(
                    10.0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    (d, e) => ((Ring)d).pen = null));

        /// <summary>
        /// StrokeDashOffset property
        /// </summary>
        public static readonly DependencyProperty StrokeDashOffsetProperty =
            DependencyProperty.Register(
                "StrokeDashOffset",
                typeof(double),
                typeof(Ring),
                new FrameworkPropertyMetadata(
                    0.0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    (d, e) => ((Ring)d).pen = null));

        /// <summary>
        /// StrokeDashArray property
        /// </summary>
        public static readonly DependencyProperty StrokeDashArrayProperty =
            DependencyProperty.Register(
                "StrokeDashArray",
                typeof(DoubleCollection),
                typeof(Ring),
                new FrameworkPropertyMetadata(
                    default(DoubleCollection),
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    (d, e) => ((Ring)d).pen = null));

        private Pen pen;
        private double diameter;

        public Ring()
        {
            this.StrokeDashArray = new DoubleCollection();
        }

        /// <summary>
        /// Gets or sets the length of the ticks.
        /// The default value is 10.
        /// </summary>
        public double Thickness
        {
            get => (double)this.GetValue(ThicknessProperty);
            set => this.SetValue(ThicknessProperty, value);
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

        protected bool CanCreatePen
        {
            get
            {
                var strokeThickness = this.StrokeThickness;
                return this.Stroke == null ||
                       DoubleUtil.IsNaN(strokeThickness) ||
                       DoubleUtil.IsZero(strokeThickness);
            }
        }

        protected Pen Pen
        {
            get
            {
                if (this.pen == null)
                {
                    if (!this.CanCreatePen)
                    {
                        // This pen is internal to the system and
                        // must not participate in freezable treeness
                        this.pen = new Pen
                        {
                            //// CanBeInheritanceContext = false;
                            Thickness = Math.Abs(this.StrokeThickness),
                            Brush = this.Stroke,
                            StartLineCap = this.StrokeStartLineCap,
                            EndLineCap = this.StrokeEndLineCap,
                            DashCap = this.StrokeDashCap,
                            LineJoin = this.StrokeLineJoin,
                            MiterLimit = this.StrokeMiterLimit,
                            DashStyle = this.StrokeDashOffset != 0.0 && this.StrokeDashArray.Count > 0
                                           ? new DashStyle(this.StrokeDashArray, this.StrokeDashOffset)
                                           : DashStyles.Solid
                        };
                    }
                }

                return this.pen;
            }
        }

        protected double GetStrokeThickness()
        {
            return this.CanCreatePen ? 0 : Math.Abs(this.StrokeThickness);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            var width = constraint.Width;
            var height = constraint.Height;
            if (double.IsInfinity(width) && double.IsInfinity(height))
            {
                return new Size(2 * this.Thickness, 2 * this.Thickness);
            }

            var d = Math.Min(width, height);
            return new Size(d, d);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            this.diameter = Math.Min(finalSize.Width, finalSize.Height);
            return finalSize;
        }

        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        protected override void OnRender(DrawingContext dc)
        {
            if (this.diameter == 0)
            {
                return;
            }

            var geometry = this.CreateGeometry();
            if (!ReferenceEquals(geometry, Geometry.Empty))
            {
                dc.DrawGeometry(this.Fill, this.Pen, geometry);
            }
        }

        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        protected virtual Geometry CreateGeometry()
        {
            if (this.diameter == 0)
            {
                return Geometry.Empty;
            }

            var strokeThickness = this.GetStrokeThickness();
            var r = (this.diameter - strokeThickness) / 2;
            var ri = r - this.Thickness;
            var cx = this.HorizontalAlignment == HorizontalAlignment.Stretch
                ? this.RenderSize.Width / 2
                : r + (strokeThickness / 2);
            var cy = this.VerticalAlignment == VerticalAlignment.Stretch
                ? this.RenderSize.Height / 2
                : r + (strokeThickness / 2);
            var center = new Point(cx, cy);
            if (this.Thickness <= 0 || ri <= 0)
            {
                return new EllipseGeometry(center, r, r);
            }

            return new CombinedGeometry(
                GeometryCombineMode.Xor,
                new EllipseGeometry(
                    center,
                    r,
                    r),
                new EllipseGeometry(
                    center,
                    ri,
                    ri));
        }
    }
}