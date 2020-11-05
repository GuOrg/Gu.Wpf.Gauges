namespace Gu.Wpf.Gauges
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public abstract class GeometryBar : FrameworkElement
    {
        /// <summary>Identifies the <see cref="Value"/> dependency property.</summary>
        public static readonly DependencyProperty ValueProperty = Gauge.ValueProperty.AddOwner(
            typeof(GeometryBar),
            new FrameworkPropertyMetadata(
                double.NaN,
                FrameworkPropertyMetadataOptions.AffectsRender,
                (d, e) => ((GeometryBar)d).OnValueChanged((double)e.OldValue, (double)e.NewValue)));

        /// <summary>Identifies the <see cref="Minimum"/> dependency property.</summary>
        public static readonly DependencyProperty MinimumProperty = Gauge.MinimumProperty.AddOwner(
            typeof(GeometryBar),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>Identifies the <see cref="Maximum"/> dependency property.</summary>
        public static readonly DependencyProperty MaximumProperty = Gauge.MaximumProperty.AddOwner(
            typeof(GeometryBar),
            new FrameworkPropertyMetadata(
                1.0,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>Identifies the <see cref="IsDirectionReversed"/> dependency property.</summary>
        public static readonly DependencyProperty IsDirectionReversedProperty = Gauge.IsDirectionReversedProperty.AddOwner(
            typeof(GeometryBar),
            new FrameworkPropertyMetadata(
                false,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>Identifies the <see cref="Fill"/> dependency property.</summary>
        public static readonly DependencyProperty FillProperty = Shape.FillProperty.AddOwner(
            typeof(GeometryBar),
            new FrameworkPropertyMetadata(
                default(Brush),
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>Identifies the <see cref="Stroke"/> dependency property.</summary>
        public static readonly DependencyProperty StrokeProperty = Shape.StrokeProperty.AddOwner(
            typeof(GeometryBar),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender,
                (d, e) => ((GeometryBar)d).ResetPen()));

        /// <summary>Identifies the <see cref="StrokeThickness"/> dependency property.</summary>
        public static readonly DependencyProperty StrokeThicknessProperty = Shape.StrokeThicknessProperty.AddOwner(
            typeof(GeometryBar),
            new FrameworkPropertyMetadata(
                1.0d,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                (d, e) => ((GeometryBar)d).pen = null));

        /// <summary>Identifies the <see cref="StrokeStartLineCap"/> dependency property.</summary>
        public static readonly DependencyProperty StrokeStartLineCapProperty = DependencyProperty.Register(
            nameof(StrokeStartLineCap),
            typeof(PenLineCap),
            typeof(GeometryBar),
            new FrameworkPropertyMetadata(
                PenLineCap.Flat,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                (d, e) => ((GeometryBar)d).pen = null),
            ValidateEnums.IsPenLineCapValid);

        /// <summary>Identifies the <see cref="StrokeEndLineCap"/> dependency property.</summary>
        public static readonly DependencyProperty StrokeEndLineCapProperty = DependencyProperty.Register(
            nameof(StrokeEndLineCap),
            typeof(PenLineCap),
            typeof(GeometryBar),
            new FrameworkPropertyMetadata(
                PenLineCap.Flat,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                (d, e) => ((GeometryBar)d).pen = null),
            ValidateEnums.IsPenLineCapValid);

        /// <summary>
        /// StrokeDashCap property
        /// PenLineCap.Square is probably a better default but keeping the same default as <see cref="Shape"/>.
        /// </summary>
        public static readonly DependencyProperty StrokeDashCapProperty =
            DependencyProperty.Register(
                nameof(StrokeDashCap),
                typeof(PenLineCap),
                typeof(GeometryBar),
                new FrameworkPropertyMetadata(
                    PenLineCap.Flat,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    (d, e) => ((GeometryBar)d).pen = null),
                ValidateEnums.IsPenLineCapValid);

        /// <summary>Identifies the <see cref="StrokeLineJoin"/> dependency property.</summary>
        public static readonly DependencyProperty StrokeLineJoinProperty =
            DependencyProperty.Register(
                nameof(StrokeLineJoin),
                typeof(PenLineJoin),
                typeof(GeometryBar),
                new FrameworkPropertyMetadata(
                    PenLineJoin.Miter,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    (d, e) => ((GeometryBar)d).pen = null),
                ValidateEnums.IsPenLineJoinValid);

        /// <summary>Identifies the <see cref="StrokeMiterLimit"/> dependency property.</summary>
        public static readonly DependencyProperty StrokeMiterLimitProperty =
            DependencyProperty.Register(
                nameof(StrokeMiterLimit),
                typeof(double),
                typeof(GeometryBar),
                new FrameworkPropertyMetadata(
                    10.0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    (d, e) => ((GeometryBar)d).pen = null));

        /// <summary>Identifies the <see cref="StrokeDashOffset"/> dependency property.</summary>
        public static readonly DependencyProperty StrokeDashOffsetProperty =
            DependencyProperty.Register(
                nameof(StrokeDashOffset),
                typeof(double),
                typeof(GeometryBar),
                new FrameworkPropertyMetadata(
                    0.0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    (d, e) => ((GeometryBar)d).pen = null));

        /// <summary>Identifies the <see cref="StrokeDashArray"/> dependency property.</summary>
        public static readonly DependencyProperty StrokeDashArrayProperty =
            DependencyProperty.Register(
                nameof(StrokeDashArray),
                typeof(DoubleCollection),
                typeof(GeometryBar),
                new FrameworkPropertyMetadata(
                    default(DoubleCollection),
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    (d, e) => ((GeometryBar)d).pen = null));

        private Pen? pen;

        protected GeometryBar()
        {
            this.StrokeDashArray = new DoubleCollection();
        }

        /// <summary>
        /// Gets or sets the current magnitude of the range control.
        /// </summary>
        /// <returns>
        /// The current magnitude of the range control. The default is 0.
        /// </returns>
        public double Value
        {
            get => (double)this.GetValue(ValueProperty);
            set => this.SetValue(ValueProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="P:Bar.Minimum" />
        /// The default is 0
        /// </summary>
        public double Minimum
        {
            get => (double)this.GetValue(MinimumProperty);
            set => this.SetValue(MinimumProperty, value);
        }

        /// <summary>
        /// Gets or sets the highest possible <see cref="P:Bar.Maximum" /> of the range element.
        /// </summary>
        /// <returns>
        /// The highest possible <see cref="P:Bar.Maximum" /> of the range element. The default is 1.
        /// </returns>
        public double Maximum
        {
            get => (double)this.GetValue(MaximumProperty);
            set => this.SetValue(MaximumProperty, value);
        }

        /// <summary>
        /// Gets or sets the direction of increasing value.
        /// </summary>
        /// <returns>
        /// true if the direction of increasing value is to the left for a horizontal tickbar or down for a vertical tickbar; otherwise, false.
        /// The default is false.
        /// </returns>
        public bool IsDirectionReversed
        {
            get => (bool)this.GetValue(IsDirectionReversedProperty);
            set => this.SetValue(IsDirectionReversedProperty, value);
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
                return (this.Stroke is null) || DoubleUtil.IsNaN(strokeThickness) || DoubleUtil.IsZero(strokeThickness);
            }
        }

        protected Pen Pen
        {
            get
            {
                if (this.pen is null)
                {
                    if (!this.CanCreatePen)
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
                                           : DashStyles.Solid,
                                   };
                    }
                }

                return this.pen;
            }
        }

        protected virtual double GetStrokeThickness()
        {
            return this.CanCreatePen ? 0 : Math.Abs(this.StrokeThickness);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var strokeThickness = this.GetStrokeThickness();
            return new Size(strokeThickness, strokeThickness);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var geometry = this.DefiningGeometry;
            if (geometry is null ||
                ReferenceEquals(geometry, Geometry.Empty))
            {
                return;
            }

            drawingContext.DrawGeometry(this.Fill, this.Pen, geometry);
        }

        protected void ResetPen()
        {
            this.pen = null;
        }

        /// <summary>This method is invoked when the <see cref="ValueProperty"/> changes.</summary>
        /// <param name="oldValue">The old value of <see cref="ValueProperty"/>.</param>
        /// <param name="newValue">The new value of <see cref="ValueProperty"/>.</param>
        protected virtual void OnValueChanged(double oldValue, double newValue)
        {
        }
    }
}
