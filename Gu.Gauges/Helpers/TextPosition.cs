namespace Gu.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    internal struct TextPosition
    {
        internal readonly Size TextSize;
        internal readonly TickBarPlacement Placement;
        internal readonly TextOrientation Orientation;
        private readonly Point point;
        internal readonly double Angle;
        public TextPosition(FormattedText text, TickBarPlacement placement, TextOrientation orientation, Point point, double angle)
            : this(new Size(text.Width, text.Height), placement, orientation, point, angle)
        {
        }

        public TextPosition(Size textSize, TickBarPlacement placement, TextOrientation orientation, Point point, double angle)
        {
            this.TextSize = textSize;
            this.Placement = placement;
            this.Orientation = orientation;
            this.point = point;
            this.Angle = angle;
        }

        public Point Point
        {
            get
            {
                var offset = this.Offset.Rotate(this.RotationAngle);
                return this.point + offset;
            }
        }

        public bool IsTransformed
        {
            get { return this.Orientation != TextOrientation.Horizontal; }
        }

        public Transform Transform
        {
            get
            {
                //return Transform.Identity;
                switch (this.Orientation)
                {
                    case TextOrientation.Horizontal:
                        return Transform.Identity;
                    case TextOrientation.VerticalUp:
                    case TextOrientation.VerticalDown:
                    case TextOrientation.Tangential:
                    case TextOrientation.RadialOut:
                        return new RotateTransform(this.RotationAngle, this.Point.X, this.Point.Y);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public Rect Bounds
        {
            get
            {
                var rect = new Rect(this.Point, this.TextSize);
                if (this.IsTransformed)
                {
                    rect.Transform(this.Transform.Value);
                }
                return rect;
            }
        }

        private Vector Offset
        {
            get
            {
                switch (this.Orientation)
                {
                    case TextOrientation.VerticalDown:
                        switch (this.Placement)
                        {
                            case TickBarPlacement.Left:
                                return this.TextSize.Offset(Vertical.Bottom, Horizontal.Center);
                            case TickBarPlacement.Top:
                                return this.TextSize.Offset(Vertical.Mid, Horizontal.Left);
                            case TickBarPlacement.Right:
                                return this.TextSize.Offset(Vertical.Top, Horizontal.Center);
                            case TickBarPlacement.Bottom:
                                return this.TextSize.Offset(Vertical.Mid, Horizontal.Right);
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    case TextOrientation.Tangential:
                        switch (this.Placement)
                        {
                            case TickBarPlacement.Left:
                                return this.TextSize.Offset(Vertical.Mid, Horizontal.Left);
                            case TickBarPlacement.Top:
                                return this.TextSize.Offset(Vertical.Top, Horizontal.Center);
                            case TickBarPlacement.Right:
                                return this.TextSize.Offset(Vertical.Mid, Horizontal.Right);
                            case TickBarPlacement.Bottom:
                                return this.TextSize.Offset(Vertical.Bottom, Horizontal.Center);
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    case TextOrientation.Horizontal:
                        switch (this.Placement)
                        {
                            case TickBarPlacement.Left:
                                return this.TextSize.Offset(Vertical.Mid, Horizontal.Left);
                            case TickBarPlacement.Top:
                                return this.TextSize.Offset(Vertical.Top, Horizontal.Center);
                            case TickBarPlacement.Right:
                                return this.TextSize.Offset(Vertical.Mid, Horizontal.Right);
                            case TickBarPlacement.Bottom:
                                return this.TextSize.Offset(Vertical.Bottom, Horizontal.Center);
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    case TextOrientation.RadialOut:
                    case TextOrientation.VerticalUp:
                        switch (this.Placement)
                        {
                            case TickBarPlacement.Left:
                                return this.TextSize.Offset(Vertical.Top, Horizontal.Center);
                            case TickBarPlacement.Top:
                                return this.TextSize.Offset(Vertical.Mid, Horizontal.Right);
                            case TickBarPlacement.Right:
                                return this.TextSize.Offset(Vertical.Bottom, Horizontal.Center);
                            case TickBarPlacement.Bottom:
                                return this.TextSize.Offset(Vertical.Mid, Horizontal.Left);
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private double RotationAngle
        {
            get
            {
                switch (this.Orientation)
                {
                    case TextOrientation.VerticalDown:
                        return 90;
                    case TextOrientation.VerticalUp:
                        return -90;
                    case TextOrientation.Tangential:
                        return this.Angle + 90;
                    case TextOrientation.Horizontal:
                        return 0;
                    case TextOrientation.RadialOut:
                        return this.Angle;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
