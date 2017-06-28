namespace Gu.Wpf.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class AngularTickBar : AngularBar
    {
        /// <summary>
        /// Identifies the <see cref="P:AngularTickBar.StrokeThickness" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(
            nameof(StrokeThickness),
            typeof(double),
            typeof(AngularTickBar),
            new FrameworkPropertyMetadata(
                1.0d,
                FrameworkPropertyMetadataOptions.AffectsRender,
                (d, _) => ((AngularTickBar)d).pen = null));

        /// <summary>
        /// Identifies the <see cref="P:AngularTickBar.Stroke" /> dependency property. This property is read-only.
        /// </summary>
        public static readonly DependencyProperty StrokeProperty = Shape.StrokeProperty.AddOwner(
            typeof(AngularTickBar),
            new FrameworkPropertyMetadata(
                default(Brush),
                FrameworkPropertyMetadataOptions.AffectsRender,
                (d, _) => ((AngularTickBar)d).pen = null));

        public static readonly DependencyProperty ThicknessProperty = AngularGauge.ThicknessProperty.AddOwner(
            typeof(AngularTickBar),
            new FrameworkPropertyMetadata(
                10.0d,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        private Pen pen;

        /// <summary>
        /// Gets or sets the <see cref="P:AngularTickBar.StrokeThickness" />
        /// The default is 1
        /// </summary>
        public double StrokeThickness
        {
            get => (double)this.GetValue(StrokeThicknessProperty);
            set => this.SetValue(StrokeThicknessProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="T:System.Windows.Media.Brush" /> that is used to draw the tick marks.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Windows.Media.Brush" /> to use to draw tick marks. The default value is null.
        /// </returns>
        public Brush Stroke
        {
            get => (Brush)this.GetValue(StrokeProperty);
            set => this.SetValue(StrokeProperty, value);
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

        private bool CanCreatePen
        {
            get
            {
                var strokeThickness = this.StrokeThickness;
                return this.Stroke == null ||
                       DoubleUtil.IsNaN(strokeThickness) ||
                       DoubleUtil.IsZero(strokeThickness);
            }
        }

        private Pen Pen
        {
            get
            {
                if (this.pen == null)
                {
                    if (!this.CanCreatePen)
                    {
                        this.pen = new Pen
                        {
                            Thickness = Math.Abs(this.StrokeThickness),
                            Brush = this.Stroke
                        };
                    }
                }

                return this.pen;
            }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (this.Thickness <= 0)
            {
                return default(Size);
            }

            var arc = new ArcInfo(default(Point), this.MinAngle, this.MaxAngle, this.Thickness, this.IsDirectionReversed);
            var rect = default(Rect);
            foreach (var tick in this.AllTicks)
            {
                var angle = TickHelper.ToAngle(tick, this.Minimum, this.Maximum, arc);
                rect.Union(arc.GetPoint(angle));
            }

            return rect.Size;
        }

        protected override void OnRender(DrawingContext dc)
        {
            if (this.Pen == null)
            {
                return;
            }

            var arc = ArcInfo.Fill(this.RenderSize, this.MinAngle, this.MaxAngle, this.IsDirectionReversed);
            foreach (var tick in this.AllTicks)
            {
                var angle = TickHelper.ToAngle(tick, this.Minimum, this.Maximum, arc);
                var po = arc.GetPoint(angle, -this.ReservedSpace / 2);
                var pi = arc.GetPoint(angle, (-this.ReservedSpace / 2) - this.Thickness);
                var line = new Line(po, pi);
                dc.DrawLine(this.Pen, line);
            }
        }
    }
}