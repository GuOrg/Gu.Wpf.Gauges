namespace Gu.Gauges
{
    using System.Linq;
    using System.Windows;

    public class LinearDimension : TwoPointDimension
    {
        private bool _hasExplicitOffsetDirection = false;
        private bool _hasExplicitOffsetPoint = false;

        static LinearDimension()
        {
            FlowDirectionProperty.AddOwner(
                typeof(LinearDimension),
                new FrameworkPropertyMetadata(
                    default(FlowDirection),
                    FrameworkPropertyMetadataOptions.AffectsMeasure,
                    OnFlowDirectionChanged));

            P1Property.AddOwner(
                typeof(LinearDimension),
                new FrameworkPropertyMetadata(
                    new Point(double.NaN, double.NaN),
                    FrameworkPropertyMetadataOptions.AffectsMeasure,
                    OnPointChanged));
            P2Property.AddOwner(
                typeof(LinearDimension),
                new FrameworkPropertyMetadata(
                    new Point(double.NaN, double.NaN),
                    FrameworkPropertyMetadataOptions.AffectsMeasure,
                    OnPointChanged));

            OffsetPointProperty.AddOwner(
                typeof(LinearDimension),
                new FrameworkPropertyMetadata(
                    new Point(double.NaN, double.NaN),
                    FrameworkPropertyMetadataOptions.AffectsMeasure,
                    OnOffsetPointChanged));

            OffsetProperty.AddOwner(
                typeof(LinearDimension),
                new FrameworkPropertyMetadata(
                    12.0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure,
                    OnOffsetChanged));

            OffsetDirectionProperty.AddOwner(
                typeof(LinearDimension),
                new FrameworkPropertyMetadata(
                    new Vector(double.NaN, double.NaN),
                    FrameworkPropertyMetadataOptions.AffectsMeasure,
                    OnOffsetDirectionChanged));

            ScaleProperty.AddOwner(
                typeof(LinearDimension),
                new FrameworkPropertyMetadata(
                    1.0,
                    FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    OnOffsetChanged));

            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(LinearDimension),
                new FrameworkPropertyMetadata(typeof(LinearDimension)));
        }

        private static void OnPointChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var dl = o as LinearDimension;
            if (dl == null || IsNan(dl.P1, dl.P2))
            {
                return;
            }
            if (!dl._hasExplicitOffsetDirection)
            {
                InitializeOffsetVector(dl);
            }
            if (!dl._hasExplicitOffsetPoint)
            {
                InitializeOffsetPoint(dl);
            }
            OnOffsetChanged(o, e);
        }

        private static void OnOffsetPointChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var dl = o as LinearDimension;
            if (dl == null)
            {
                return;
            }
            dl._hasExplicitOffsetPoint = true;
            OnOffsetChanged(o, e);
        }

        private static void OnOffsetDirectionChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var dl = o as LinearDimension;
            if (dl == null)
            {
                return;
            }
            dl._hasExplicitOffsetDirection = true;
            OnOffsetChanged(o, e);
        }

        private static void OnFlowDirectionChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var dl = o as LinearDimension;
            if (dl == null || IsNan(dl.OffsetDirection) || dl._hasExplicitOffsetDirection)
            {
                return;
            }
            dl.OffsetDirection.Negate();
        }

        private static void OnOffsetChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var dl = o as LinearDimension;
            if (dl == null || IsNan(dl.P1, dl.P2, dl.OffsetPoint) || IsNan(dl.OffsetDirection))
            {
                return;
            }

            dl.DimensionLineP1 = GetOffsetPoint(dl.P1, dl);
            dl.DimensionLineP2 = GetOffsetPoint(dl.P2, dl);

            dl.BoundaryLine1P1 = dl.P1 + (dl.Scale * OriginOffset * dl.OffsetDirection);
            dl.BoundaryLine1P2 = dl.DimensionLineP1 + (dl.Scale * Extension * dl.OffsetDirection);

            dl.BoundaryLine2P1 = dl.P2 + (dl.Scale * OriginOffset * dl.OffsetDirection);
            dl.BoundaryLine2P2 = dl.DimensionLineP2 + (dl.Scale * Extension * dl.OffsetDirection);
            UpdateArrowHeadDirections(dl);
        }

        private static void InitializeOffsetVector(LinearDimension dl)
        {
            var v = dl.P1 - dl.P2;
            v.Normalize();
            var dir = Vector.Multiply(v, Rotate90Cw.Value);
            if (!dl.OffsetDirection.Equals(dir))
            {
                dl.SetCurrentValue(OffsetDirectionProperty, dir);
                dl._hasExplicitOffsetDirection = false;
            }
        }

        private static void InitializeOffsetPoint(LinearDimension dl)
        {
            var d = Vector.Multiply(dl.P1 - dl.P2, dl.OffsetDirection);
            Point op = d < 0
                ? dl.P2
                : dl.P1;
            if (IsNan(dl.OffsetPoint) || (dl.OffsetPoint - op).Length > 1e-3)
            {
                dl.SetCurrentValue(OffsetPointProperty, dl.P2);
                dl._hasExplicitOffsetPoint = false;
            }
        }

        private static Point GetOffsetPoint(Point p, LinearDimension dl)
        {
            var op = dl.OffsetPoint + (dl.Scale * dl.Offset * dl.OffsetDirection);
            var d = Vector.Multiply(op - p, dl.OffsetDirection);
            return p + (d * dl.OffsetDirection);
        }

        private static void UpdateArrowHeadDirections(LinearDimension dl)
        {
            var v = dl.DimensionLineP1 - dl.DimensionLineP2;
            v.Normalize();
            dl.ArrowHead1Direction = v;
            v.Negate();
            dl.ArrowHead2Direction = v;
        }

        private static bool IsNan(params Point[] points)
        {
            return points.Select(p => new[] { p.X, p.Y })
                         .SelectMany(x => x)
                         .Any(double.IsNaN);
        }

        private static bool IsNan(params Vector[] vectors)
        {
            return vectors.Select(p => new[] { p.X, p.Y })
                          .SelectMany(x => x)
                          .Any(double.IsNaN);
        }
    }
}