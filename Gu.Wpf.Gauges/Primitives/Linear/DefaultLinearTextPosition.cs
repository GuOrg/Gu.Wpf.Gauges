namespace Gu.Wpf.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Controls.Primitives;

    public class DefaultLinearTextPosition : LinearTextPosition
    {
        private static readonly ExplicitLinearTextPosition Left = new ExplicitLinearTextPosition(HorizontalTextAlignment.Left, VerticalTextAlignment.Center);
        private static readonly ExplicitLinearTextPosition Right = new ExplicitLinearTextPosition(HorizontalTextAlignment.Right, VerticalTextAlignment.Center);
        private static readonly ExplicitLinearTextPosition Top = new ExplicitLinearTextPosition(HorizontalTextAlignment.Center, VerticalTextAlignment.Top);
        private static readonly ExplicitLinearTextPosition Bottom = new ExplicitLinearTextPosition(HorizontalTextAlignment.Center, VerticalTextAlignment.Bottom);

        public static DefaultLinearTextPosition Instance { get; } = new DefaultLinearTextPosition();

        public override void ArrangeTick(TickText tickText, Size arrangeSize, LinearTextBar textBar)
        {
            switch (textBar.Placement)
            {
                case TickBarPlacement.Left:
                    Left.ArrangeTick(tickText, arrangeSize, textBar);
                    break;
                case TickBarPlacement.Top:
                    Top.ArrangeTick(tickText, arrangeSize, textBar);
                    break;
                case TickBarPlacement.Right:
                    Right.ArrangeTick(tickText, arrangeSize, textBar);
                    break;
                case TickBarPlacement.Bottom:
                    Bottom.ArrangeTick(tickText, arrangeSize, textBar);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}