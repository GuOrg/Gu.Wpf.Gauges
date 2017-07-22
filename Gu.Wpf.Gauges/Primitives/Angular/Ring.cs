namespace Gu.Wpf.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class Ring : AngularGeometryBar
    {
        protected override Geometry DefiningGeometry => throw new InvalidOperationException("Uses OnRender");

        public static Geometry CreateGeometry(
            Size finalSize,
            double thickness,
            double strokeThickness,
            HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment verticalAlignment = VerticalAlignment.Center)
        {
            var diameter = Math.Min(finalSize.Width, finalSize.Height);
            if (DoubleUtil.AreClose(0, diameter))
            {
                return Geometry.Empty;
            }

            var r = (diameter - strokeThickness) / 2;
            var ri = r - thickness;
            var cx = horizontalAlignment == HorizontalAlignment.Stretch
                ? finalSize.Width / 2
                : r + (strokeThickness / 2);
            var cy = verticalAlignment == VerticalAlignment.Stretch
                ? finalSize.Height / 2
                : r + (strokeThickness / 2);
            var center = new Point(cx, cy);
            if (thickness <= 0 ||
                ri <= 0 ||
                DoubleUtil.LessThanOrClose(thickness, strokeThickness))
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

        protected override Size MeasureOverride(Size constraint)
        {
            var d = 2 * (this.Thickness + this.GetStrokeThickness());
            if (double.IsInfinity(d))
            {
                return default(Size);
            }

            return new Size(d, d);
        }

        protected override void OnRender(DrawingContext dc)
        {
            var geometry = this.CreateGeometry();
            if (!ReferenceEquals(geometry, Geometry.Empty))
            {
                dc.DrawGeometry(
                    DoubleUtil.LessThanOrClose(this.Thickness, this.GetStrokeThickness()) ? null : this.Fill,
                    this.Pen,
                    geometry);
            }
        }

        protected virtual Geometry CreateGeometry() => CreateGeometry(
            this.RenderSize,
            this.Thickness,
            this.GetStrokeThickness(),
            this.HorizontalAlignment,
            this.VerticalAlignment);
    }
}