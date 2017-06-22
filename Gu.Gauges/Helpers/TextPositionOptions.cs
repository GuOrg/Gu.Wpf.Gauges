namespace Gu.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Controls.Primitives;

    internal struct TextPositionOptions
    {
        internal readonly Vertical Vertical;
        internal readonly Horizontal Horizontal;
        internal readonly TextOrientation Orientation;
        private static readonly double Treshold = Math.Sin(1 * Math.PI / 180);

        public TextPositionOptions(TickBarPlacement placement, TextOrientation orientation)
        {
            this.Orientation = orientation;
            switch (placement)
            {
                case TickBarPlacement.Left:
                    switch (orientation)
                    {
                        case TextOrientation.RadialOut:
                        case TextOrientation.Tangential:
                        case TextOrientation.Horizontal:
                            this.Vertical = Vertical.Mid;
                            this.Horizontal = Horizontal.Left;
                            break;
                        case TextOrientation.VerticalUp:
                            this.Vertical = Vertical.Top;
                            this.Horizontal = Horizontal.Center;
                            break;
                        case TextOrientation.VerticalDown:
                            this.Vertical = Vertical.Bottom;
                            this.Horizontal = Horizontal.Center;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(orientation));
                    }

                    break;
                case TickBarPlacement.Top:
                    switch (orientation)
                    {
                        case TextOrientation.RadialOut:
                        case TextOrientation.Tangential:
                        case TextOrientation.Horizontal:
                            this.Vertical = Vertical.Top;
                            this.Horizontal = Horizontal.Center;
                            break;
                        case TextOrientation.VerticalUp:
                            this.Vertical = Vertical.Mid;
                            this.Horizontal = Horizontal.Right;
                            break;
                        case TextOrientation.VerticalDown:
                            this.Vertical = Vertical.Mid;
                            this.Horizontal = Horizontal.Left;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(orientation));
                    }

                    break;
                case TickBarPlacement.Right:
                    switch (orientation)
                    {
                        case TextOrientation.RadialOut:
                        case TextOrientation.Tangential:
                        case TextOrientation.Horizontal:
                            this.Vertical = Vertical.Mid;
                            this.Horizontal = Horizontal.Right;
                            break;
                        case TextOrientation.VerticalUp:
                            this.Vertical = Vertical.Bottom;
                            this.Horizontal = Horizontal.Center;
                            break;
                        case TextOrientation.VerticalDown:
                            this.Vertical = Vertical.Top;
                            this.Horizontal = Horizontal.Center;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(orientation));
                    }

                    break;
                case TickBarPlacement.Bottom:
                    switch (orientation)
                    {
                        case TextOrientation.RadialOut:
                        case TextOrientation.Tangential:
                        case TextOrientation.Horizontal:
                            this.Vertical = Vertical.Bottom;
                            this.Horizontal = Horizontal.Center;
                            break;
                        case TextOrientation.VerticalUp:
                            this.Vertical = Vertical.Mid;
                            this.Horizontal = Horizontal.Left;
                            break;
                        case TextOrientation.VerticalDown:
                            this.Vertical = Vertical.Mid;
                            this.Horizontal = Horizontal.Right;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(orientation));
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(placement));
            }
        }

        public TextPositionOptions(TextOrientation orientation, double angle)
        {
            var rotate = new Vector(1, 0).Rotate(angle);
            this.Orientation = orientation;
            switch (this.Orientation)
            {
                case TextOrientation.VerticalDown:
                    if (rotate.X > Treshold)
                    {
                        this.Vertical = Vertical.Bottom;
                    }
                    else if (Math.Abs(rotate.X) < Treshold)
                    {
                        this.Vertical = Vertical.Mid;
                    }
                    else
                    {
                        this.Vertical = Vertical.Top;
                    }

                    if (rotate.Y > Treshold)
                    {
                        this.Horizontal = Horizontal.Left;
                    }
                    else if (Math.Abs(rotate.Y) < Treshold)
                    {
                        this.Horizontal = Horizontal.Center;
                    }
                    else
                    {
                        this.Horizontal = Horizontal.Right;
                    }

                    break;
                case TextOrientation.Tangential:
                    this.Vertical = Vertical.Bottom;
                    this.Horizontal = Horizontal.Center;

                    break;
                case TextOrientation.Horizontal:
                    if (rotate.X > Treshold)
                    {
                        this.Horizontal = Horizontal.Left;
                    }
                    else if (Math.Abs(rotate.X) < Treshold)
                    {
                        this.Horizontal = Horizontal.Center;
                    }
                    else
                    {
                        this.Horizontal = Horizontal.Right;
                    }

                    if (rotate.Y > Treshold)
                    {
                        this.Vertical = Vertical.Top;
                    }
                    else if (Math.Abs(rotate.Y) < Treshold)
                    {
                        this.Vertical = Vertical.Mid;
                    }
                    else
                    {
                        this.Vertical = Vertical.Bottom;
                    }

                    break;
                case TextOrientation.RadialOut:
                    this.Vertical = Vertical.Mid;
                    this.Horizontal = Horizontal.Left;
                    break;
                case TextOrientation.VerticalUp:
                    if (rotate.X > Treshold)
                    {
                        this.Vertical = Vertical.Top;
                    }
                    else if (Math.Abs(rotate.X) < Treshold)
                    {
                        this.Vertical = Vertical.Mid;
                    }
                    else
                    {
                        this.Vertical = Vertical.Bottom;
                    }

                    if (rotate.Y > Treshold)
                    {
                        this.Horizontal = Horizontal.Right;
                    }
                    else if (Math.Abs(rotate.Y) < Treshold)
                    {
                        this.Horizontal = Horizontal.Center;
                    }
                    else
                    {
                        this.Horizontal = Horizontal.Left;
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}