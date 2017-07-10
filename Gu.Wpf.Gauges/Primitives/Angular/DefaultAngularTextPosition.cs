namespace Gu.Wpf.Gauges
{
    using System.Windows;

    public class DefaultAngularTextPosition : AngularTextPosition
    {
        public static DefaultAngularTextPosition Instance { get; } = new DefaultAngularTextPosition();

        public override void ArrangeTick(TickText tickText, Size arrangeSize, AngularTextBar textBar)
        {
            throw new System.NotImplementedException();
        }
    }
}