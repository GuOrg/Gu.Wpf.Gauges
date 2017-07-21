namespace Gu.Wpf.Gauges
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Media;

    public class Ring : AngularGeometryBar
    {
        private double diameter;

        protected override Geometry DefiningGeometry => throw new InvalidOperationException("Uses OnRender");

        protected override Size MeasureOverride(Size constraint)
        {
            var d = 2 * (this.Thickness + this.GetStrokeThickness());
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
            if (DoubleUtil.AreClose(0, this.diameter))
            {
                return;
            }

            var geometry = this.CreateGeometry();
            if (!ReferenceEquals(geometry, Geometry.Empty))
            {
                dc.DrawGeometry(this.Fill, this.Pen, geometry);
            }
        }

        protected virtual Geometry CreateGeometry()
        {
            if (DoubleUtil.AreClose(0, this.diameter))
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