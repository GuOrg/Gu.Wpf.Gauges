namespace Gu.Wpf.Gauges
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Media;

    public class Arc : AngularGeometryBar
    {
        protected override Geometry DefiningGeometry => throw new InvalidOperationException("Uses OnRender");

        protected override Size MeasureOverride(Size constraint)
        {
            throw new NotImplementedException();
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            throw new NotImplementedException();
            return finalSize;
        }

        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        protected override void OnRender(DrawingContext dc)
        {
            throw new NotImplementedException();
        }
    }
}