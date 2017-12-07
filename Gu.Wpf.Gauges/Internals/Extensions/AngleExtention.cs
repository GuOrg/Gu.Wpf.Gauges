namespace Gu.Wpf.Gauges
{
    internal static class AngleExtention
    {
        internal static bool IsUpperQuadrants(this Angle angle)
        {
            while (angle <= Angle.FromDegrees(0))
            {
                angle += Angle.FromDegrees(360);
            }

            while (angle > Angle.FromDegrees(360))
            {
                angle += Angle.FromDegrees(360);
            }

            return angle >= Angle.FromDegrees(270) || angle <= Angle.FromDegrees(90);
        }
    }
}