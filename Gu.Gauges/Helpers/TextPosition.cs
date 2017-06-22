namespace Gu.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    internal struct TextPosition
    {
        private readonly Size textSize;
        private readonly TextPositionOptions textPositionOptions;
        private readonly Point point;
        private readonly double angle;

        public TextPosition(FormattedText text, TextPositionOptions textPositionOptions, Point point, double angle)
            : this(new Size(text.Width, text.Height), textPositionOptions, point, angle)
        {
        }

        public TextPosition(Size textSize, TextPositionOptions textPositionOptions, Point point, double angle)
        {
            this.textSize = textSize;
            this.textPositionOptions = textPositionOptions;
            this.point = point;
            this.angle = angle;
        }

        public Point Point
        {
            get
            {
                var offset = this.Offset.Rotate(this.RotationAngle);
                return this.point + offset;
            }
        }

        public bool IsTransformed => this.textPositionOptions.Orientation != TextOrientation.Horizontal;

        public Transform Transform
        {
            get
            {
                switch (this.textPositionOptions.Orientation)
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

        public Rect TransformedBounds
        {
            get
            {
                var rect = new Rect(this.Point, this.textSize);
                if (this.IsTransformed)
                {
                    rect.Transform(this.Transform.Value);
                }

                return rect;
            }
        }

        private Vector Offset => this.textSize.Offset(this.textPositionOptions.Vertical, this.textPositionOptions.Horizontal);

        private double RotationAngle
        {
            get
            {
                switch (this.textPositionOptions.Orientation)
                {
                    case TextOrientation.VerticalDown:
                        return 90;
                    case TextOrientation.VerticalUp:
                        return -90;
                    case TextOrientation.Tangential:
                        return this.angle + 90;
                    case TextOrientation.Horizontal:
                        return 0;
                    case TextOrientation.RadialOut:
                        return this.angle;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
