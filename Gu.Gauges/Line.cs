using System.Windows.Controls.Primitives;

namespace Gu.Gauges
{
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

        public Line(LinearTickBar tickbar)
        {
            Point p1;
            Point p2;
            if (tickbar.Placement == TickBarPlacement.Bottom || tickbar.Placement == TickBarPlacement.Top)
            {
                p1 = new Point(tickbar.ReservedSpace / 2, 0);
                p2 = new Point(tickbar.ActualWidth - tickbar.ReservedSpace / 2, 0);
            }
            else
            {
                p1 = new Point(0, tickbar.ReservedSpace / 2);
                p2 = new Point(0, tickbar.ActualHeight - tickbar.ReservedSpace / 2);
            }
            if (tickbar.IsDirectionReversed)
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