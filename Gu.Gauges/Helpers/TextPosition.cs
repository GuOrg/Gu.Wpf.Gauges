namespace Gu.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    internal struct TextPosition
    {
        internal readonly Rect TextRect;
        internal readonly TickBarPlacement Placement;
        internal readonly TextOrientation Orientation;
        private readonly Point point;
        internal readonly double Angle;

        public TextPosition(FormattedText text, TickBarPlacement placement, TextOrientation orientation, Point point, double angle)
        {
            this.TextRect = new Rect(point, new Vector(text.Width, -1 * text.Height));
            this.Placement = placement;
            this.Orientation = orientation;
            this.point = point;
            this.Angle = angle;
        }

        public Point Point
        {
            get
            {
                var offset = this.Offset.Rotate(-1 * this.RotationAngle);
                return this.point + offset;
                switch (this.Placement)
                {
                    case TickBarPlacement.Left:
                        switch (this.Orientation)
                        {
                            case TextOrientation.Horizontal:
                                return new Point(this.TextRect.Left, this.TextRect.Bottom - this.TextRect.Height / 2);
                            case TextOrientation.VerticalUp:
                                return new Point(this.TextRect.Left, this.TextRect.Bottom + this.TextRect.Width / 2);
                            case TextOrientation.VerticalDown:
                                return new Point(this.TextRect.Left, this.TextRect.Height / 2);
                            case TextOrientation.Tangential:
                                return new Point(this.TextRect.Left, this.TextRect.Height / 2);
                            case TextOrientation.RadialOut:
                                return new Point(this.TextRect.Left, this.TextRect.Height / 2);
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        break;
                    case TickBarPlacement.Top:
                        switch (this.Orientation)
                        {
                            case TextOrientation.Horizontal:
                                return new Point(this.TextRect.Left - this.TextRect.Width / 2, this.TextRect.Bottom);
                            case TextOrientation.VerticalUp:
                                return new Point(this.TextRect.Left - this.TextRect.Height / 2, this.TextRect.Bottom + this.TextRect.Width);
                            case TextOrientation.VerticalDown:
                                break;
                            case TextOrientation.Tangential:
                                break;
                            case TextOrientation.RadialOut:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        break;
                    case TickBarPlacement.Right:
                        switch (this.Orientation)
                        {
                            case TextOrientation.Horizontal:
                                return new Point(this.TextRect.Left - this.TextRect.Width, this.TextRect.Bottom - this.TextRect.Height / 2);
                            case TextOrientation.VerticalUp:
                                return new Point(this.TextRect.Right - this.TextRect.Height, this.TextRect.Bottom + this.TextRect.Width / 2);
                            case TextOrientation.VerticalDown:
                                break;
                            case TextOrientation.Tangential:
                                break;
                            case TextOrientation.RadialOut:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        break;
                    case TickBarPlacement.Bottom:
                        switch (this.Orientation)
                        {
                            case TextOrientation.Horizontal:
                                return new Point(this.TextRect.Left - this.TextRect.Width / 2, this.TextRect.Top);
                            case TextOrientation.VerticalUp:
                                return new Point(this.TextRect.Left - this.TextRect.Height / 2, this.TextRect.Bottom);
                            case TextOrientation.VerticalDown:
                                break;
                            case TextOrientation.Tangential:
                                break;
                            case TextOrientation.RadialOut:
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                return new Point(this.TextRect.Left, this.TextRect.Height / 2);
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

        private Vector Offset
        {
            get
            {
                switch (this.Orientation)
                {
                    case TextOrientation.VerticalDown:
                        return new Vector(this.TextRect.Width, this.TextRect.Height / 2);
                    case TextOrientation.Tangential:
                    case TextOrientation.Horizontal:
                        switch (this.Placement)
                        {
                            case TickBarPlacement.Left:
                                return this.Left.Mid;
                            case TickBarPlacement.Top:
                                return this.Mid.Top;
                            case TickBarPlacement.Right:
                                return this.Right.Mid;
                            case TickBarPlacement.Bottom:
                                return this.Mid.Bottom;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    case TextOrientation.RadialOut:
                    case TextOrientation.VerticalUp:
                        switch (this.Placement)
                        {
                            case TickBarPlacement.Left:
                                return this.Mid.Top;
                            case TickBarPlacement.Top:
                                return this.Right.Mid;
                            case TickBarPlacement.Right:
                                return this.Mid.Bottom;
                            case TickBarPlacement.Bottom:
                                return this.Left.Mid;
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

        private VectorBuilder Left
        {
            get { return new VectorBuilder(0, this); }
        }

        private VectorBuilder Mid
        {
            get { return new VectorBuilder(-this.TextRect.Width / 2, this); }
        }

        private VectorBuilder Right
        {
            get { return new VectorBuilder(-this.TextRect.Width, this); }
        }

        internal struct VectorBuilder
        {
            private readonly TextPosition textPosition;
            private readonly double x;

            public VectorBuilder(double x, TextPosition textPosition)
                : this()
            {
                this.textPosition = textPosition;
                this.x = x;
            }

            public Vector Top
            {
                get { return new Vector(this.x, this.textPosition.TextRect.Height); }
            }

            public Vector Mid
            {
                get { return new Vector(this.x, this.textPosition.TextRect.Height / 2); }
            }

            public Vector Bottom
            {
                get { return new Vector(this.x, 0); }
            }
        }
    }
}
