namespace Gu.Wpf.Gauges
{
    using System.Linq;
    using System.Windows;

    public class LinearDimension : TwoPointDimension
    {
        private bool hasExplicitOffsetDirection;
        private bool hasExplicitOffsetPoint;

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
                    OnP1Changed));
            P2Property.AddOwner(
                typeof(LinearDimension),
                new FrameworkPropertyMetadata(
                    new Point(double.NaN, double.NaN),
                    FrameworkPropertyMetadataOptions.AffectsMeasure,
                    OnP2Changed));

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
                    OnScaleChanged));

            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(LinearDimension),
                new FrameworkPropertyMetadata(typeof(LinearDimension)));
        }

        private static void OnP1Changed(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            OnPointChanged(o, e);
        }

        private static void OnP2Changed(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            OnPointChanged(o, e);
        }

        private static void OnPointChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var dl = o as LinearDimension;
            if (dl is null || IsNan(dl.P1, dl.P2))
            {
                return;
            }

            if (!dl.hasExplicitOffsetDirection)
            {
                InitializeOffsetVector(dl);
            }

            if (!dl.hasExplicitOffsetPoint)
            {
                InitializeOffsetPoint(dl);
            }

            OnOffsetChanged(o, e);
        }

        private static void OnOffsetPointChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var dl = o as LinearDimension;
            if (dl is null)
            {
                return;
            }

            dl.hasExplicitOffsetPoint = true;
            OnOffsetChanged(o, e);
        }

        private static void OnOffsetDirectionChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var dl = o as LinearDimension;
            if (dl is null)
            {
                return;
            }

            dl.hasExplicitOffsetDirection = true;
            OnOffsetChanged(o, e);
        }

        private static void OnFlowDirectionChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var dl = o as LinearDimension;
            if (dl is null || IsNan(dl.OffsetDirection) || dl.hasExplicitOffsetDirection)
            {
                return;
            }

            dl.OffsetDirection.Negate();
        }

        private static void OnScaleChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            OnOffsetChanged(o, e);
        }

        private static void OnOffsetChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var dl = o as LinearDimension;
            if (dl?.OffsetPoint is null || IsNan(dl.P1, dl.P2, dl.OffsetPoint.Value) || IsNan(dl.OffsetDirection))
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
                dl.hasExplicitOffsetDirection = false;
            }
        }

        private static void InitializeOffsetPoint(LinearDimension dl)
        {
            var d = Vector.Multiply(dl.P1 - dl.P2, dl.OffsetDirection);
            Point op = d < 0
                ? dl.P2
                : dl.P1;
            if (dl.OffsetPoint is null)
            {
                return;
            }

            var offsetPoint = dl.OffsetPoint.Value;
            if (IsNan(offsetPoint) || (offsetPoint - op).Length > 1e-3)
            {
                dl.SetCurrentValue(OffsetPointProperty, dl.P2);
                dl.hasExplicitOffsetPoint = false;
            }
        }

        private static Point GetOffsetPoint(Point p, LinearDimension dl)
        {
            var op = dl.OffsetPoint + (dl.Scale * dl.Offset * dl.OffsetDirection);
            if (op != null)
            {
                var d = Vector.Multiply(op.Value - p, dl.OffsetDirection);
                return p + (d * dl.OffsetDirection);
            }

            return p;
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