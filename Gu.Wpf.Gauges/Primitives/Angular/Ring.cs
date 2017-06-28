namespace Gu.Wpf.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class Ring : Shape
    {
        public static readonly DependencyProperty ThicknessProperty = AngularGauge.ThicknessProperty.AddOwner(
            typeof(Ring),
            new FrameworkPropertyMetadata(
                10.0,
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Gets or sets the length of the ticks.
        /// The default value is 10.
        /// </summary>
        public double Thickness
        {
            get => (double)this.GetValue(ThicknessProperty);
            set => this.SetValue(ThicknessProperty, value);
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                if (this.RenderSize.IsEmpty)
                {
                    return Geometry.Empty;
                }

                var r = Math.Min(this.RenderSize.Width, this.RenderSize.Height) / 2;
                var ri = r - this.Thickness;
                var center = new Point(r, r);
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

        protected override Size MeasureOverride(Size constraint)
        {
            return new Size(this.Thickness, this.Thickness);
        }
    }
}