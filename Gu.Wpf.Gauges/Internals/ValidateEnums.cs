namespace Gu.Wpf.Gauges
{
    using System.Windows.Media;

    /// <summary>
    /// http://referencesource.microsoft.com/#PresentationCore/Shared/MS/Internal/Generated/PenLineCapValidation.cs,8ca329617772d28e
    /// </summary>
    internal static class ValidateEnums
    {
        public static bool IsAlignmentXValid(object valueObject)
        {
            var alignmentX = (AlignmentX)valueObject;
            switch (alignmentX)
            {
                case AlignmentX.Left:
                case AlignmentX.Center:
                    return true;
                default:
                    return alignmentX == AlignmentX.Right;
            }
        }

        public static bool IsAlignmentYValid(object valueObject)
        {
            var alignmentY = (AlignmentY)valueObject;
            switch (alignmentY)
            {
                case AlignmentY.Top:
                case AlignmentY.Center:
                    return true;
                default:
                    return alignmentY == AlignmentY.Bottom;
            }
        }

        public static bool IsBrushMappingModeValid(object valueObject)
        {
            var brushMappingMode = (BrushMappingMode)valueObject;
            if (brushMappingMode != BrushMappingMode.Absolute)
            {
                return brushMappingMode == BrushMappingMode.RelativeToBoundingBox;
            }

            return true;
        }

        public static bool IsCachingHintValid(object valueObject)
        {
            var cachingHint = (CachingHint)valueObject;
            if (cachingHint != CachingHint.Unspecified)
            {
                return cachingHint == CachingHint.Cache;
            }

            return true;
        }

        public static bool IsColorInterpolationModeValid(object valueObject)
        {
            var interpolationMode = (ColorInterpolationMode)valueObject;
            if (interpolationMode != ColorInterpolationMode.ScRgbLinearInterpolation)
            {
                return interpolationMode == ColorInterpolationMode.SRgbLinearInterpolation;
            }

            return true;
        }

        public static bool IsGeometryCombineModeValid(object valueObject)
        {
            var geometryCombineMode = (GeometryCombineMode)valueObject;
            switch (geometryCombineMode)
            {
                case GeometryCombineMode.Union:
                case GeometryCombineMode.Intersect:
                case GeometryCombineMode.Xor:
                    return true;
                default:
                    return geometryCombineMode == GeometryCombineMode.Exclude;
            }
        }

        public static bool IsEdgeModeValid(object valueObject)
        {
            var edgeMode = (EdgeMode)valueObject;
            if (edgeMode != EdgeMode.Unspecified)
            {
                return edgeMode == EdgeMode.Aliased;
            }

            return true;
        }

        public static bool IsBitmapScalingModeValid(object valueObject)
        {
            var bitmapScalingMode = (BitmapScalingMode)valueObject;
            if (bitmapScalingMode != BitmapScalingMode.Unspecified && bitmapScalingMode != BitmapScalingMode.LowQuality && (bitmapScalingMode != BitmapScalingMode.HighQuality && bitmapScalingMode != BitmapScalingMode.LowQuality) && bitmapScalingMode != BitmapScalingMode.HighQuality)
            {
                return bitmapScalingMode == BitmapScalingMode.NearestNeighbor;
            }

            return true;
        }

        public static bool IsClearTypeHintValid(object valueObject)
        {
            var clearTypeHint = (ClearTypeHint)valueObject;
            if (clearTypeHint != ClearTypeHint.Auto)
            {
                return clearTypeHint == ClearTypeHint.Enabled;
            }

            return true;
        }

        public static bool IsTextRenderingModeValid(object valueObject)
        {
            var textRenderingMode = (TextRenderingMode)valueObject;
            switch (textRenderingMode)
            {
                case TextRenderingMode.Auto:
                case TextRenderingMode.Aliased:
                case TextRenderingMode.Grayscale:
                    return true;
                default:
                    return textRenderingMode == TextRenderingMode.ClearType;
            }
        }

        public static bool IsTextHintingModeValid(object valueObject)
        {
            var textHintingMode = (TextHintingMode)valueObject;
            switch (textHintingMode)
            {
                case TextHintingMode.Auto:
                case TextHintingMode.Fixed:
                    return true;
                default:
                    return textHintingMode == TextHintingMode.Animated;
            }
        }

        public static bool IsFillRuleValid(object valueObject)
        {
            var fillRule = (FillRule)valueObject;
            if (fillRule != FillRule.EvenOdd)
            {
                return fillRule == FillRule.Nonzero;
            }

            return true;
        }

        public static bool IsGradientSpreadMethodValid(object valueObject)
        {
            var gradientSpreadMethod = (GradientSpreadMethod)valueObject;
            switch (gradientSpreadMethod)
            {
                case GradientSpreadMethod.Pad:
                case GradientSpreadMethod.Reflect:
                    return true;
                default:
                    return gradientSpreadMethod == GradientSpreadMethod.Repeat;
            }
        }

        public static bool IsPenLineCapValid(object valueObject)
        {
            var penLineCap = (PenLineCap)valueObject;
            switch (penLineCap)
            {
                case PenLineCap.Flat:
                case PenLineCap.Square:
                case PenLineCap.Round:
                    return true;
                default:
                    return penLineCap == PenLineCap.Triangle;
            }
        }

        public static bool IsPenLineJoinValid(object valueObject)
        {
            var penLineJoin = (PenLineJoin)valueObject;
            switch (penLineJoin)
            {
                case PenLineJoin.Miter:
                case PenLineJoin.Bevel:
                    return true;
                default:
                    return penLineJoin == PenLineJoin.Round;
            }
        }

        public static bool IsStretchValid(object valueObject)
        {
            var stretch = (Stretch)valueObject;
            switch (stretch)
            {
                case Stretch.None:
                case Stretch.Fill:
                case Stretch.Uniform:
                    return true;
                default:
                    return stretch == Stretch.UniformToFill;
            }
        }

        public static bool IsTileModeValid(object valueObject)
        {
            var tileMode = (TileMode)valueObject;
            switch (tileMode)
            {
                case TileMode.None:
                case TileMode.Tile:
                case TileMode.FlipX:
                case TileMode.FlipY:
                    return true;
                default:
                    return tileMode == TileMode.FlipXY;
            }
        }

        public static bool IsSweepDirectionValid(object valueObject)
        {
            var sweepDirection = (SweepDirection)valueObject;
            if (sweepDirection != SweepDirection.Counterclockwise)
            {
                return sweepDirection == SweepDirection.Clockwise;
            }

            return true;
        }
    }
}