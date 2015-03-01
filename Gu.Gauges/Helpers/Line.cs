using System;

namespace Gu.Gauges
{
    using System.Windows.Controls.Primitives;
    using System.Windows;

    internal struct Line
    {
        internal readonly Point StartPoint;
        internal readonly Point EndPoint;

        public Line(Point startPoint, Point endPoint)
            : this()
        {
            this.StartPoint = startPoint;
            this.EndPoint = endPoint;
        }

        public Line(Point startPoint, Point endPoint, int decimals = 0)
            : this()
        {
            this.StartPoint = startPoint.Round(decimals);
            this.EndPoint = endPoint.Round(decimals);
        }

        public Line(Size size, double reservedSpace, TickBarPlacement placement, bool isDirectionReversed)
            :this(size.Width, size.Height,reservedSpace, placement, isDirectionReversed)
        {
        }

        public Line(double actualWidth, double actualHeight, double reservedSpace, TickBarPlacement placement, bool isDirectionReversed)
        {
            Point p1;
            Point p2;
            switch (placement)
            {
                case TickBarPlacement.Left:
                    p1 = new Point(0, actualHeight - reservedSpace / 2);
                    p2 = new Point(0, reservedSpace / 2);
                    break;
                case TickBarPlacement.Top:
                    p1 = new Point(reservedSpace / 2, 0);
                    p2 = new Point(actualWidth - reservedSpace / 2, 0);
                    break;
                case TickBarPlacement.Right:
                    p1 = new Point(actualWidth, actualHeight - reservedSpace / 2);
                    p2 = new Point(actualWidth, reservedSpace / 2);
                    break;
                case TickBarPlacement.Bottom:
                    p1 = new Point(reservedSpace / 2, actualHeight);
                    p2 = new Point(actualWidth - reservedSpace / 2, actualHeight);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("placement");
            }
            if (isDirectionReversed)
            {
                this.StartPoint = p2;
                this.EndPoint = p1;
            }
            else
            {
                this.StartPoint = p1;
                this.EndPoint = p2;
            }
        }

        public double Length
        {
            get
            {
                var v = this.EndPoint - this.StartPoint;
                return v.Length;
            }
        }

        public Point Interpolate(double tick, double minimum, double maximum)
        {
            var dv = (tick - minimum) / (maximum - minimum);
            var v = this.EndPoint - this.StartPoint;
            return this.StartPoint + dv * v;
        }

        public override string ToString()
        {
            return string.Format("StartPoint: ({0}, {1}), EndPoint: ({2}, {3}), Length: {4}",
                this.StartPoint.X,
                this.StartPoint.Y,
                this.EndPoint.X,
                this.EndPoint.Y,
                this.Length);
        }
    }
}