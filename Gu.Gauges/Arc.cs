namespace Gu.Gauges
{
    using System.Windows;

    internal struct Arc
    {
        internal readonly Point Centre;
        internal readonly double Radius;
        internal readonly double Start;
        internal readonly double End;

        public Arc(Point centre, double start, double end, double radius, bool isDirectionReversed)
            : this()
        {
            this.Centre = centre;
            this.Radius = radius;
            if (isDirectionReversed)
            {
                this.Start = end;
                this.End = start;
            }
            else
            {
                this.Start = start;
                this.End = end;
            }
        }
    }
}
