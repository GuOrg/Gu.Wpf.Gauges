namespace Gu.Wpf.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class DefaultAngularTextPosition : AngularTextPosition
    {
        public static DefaultAngularTextPosition Instance { get; } = new DefaultAngularTextPosition();

        /// <inheritdoc />
        public override Thickness ArrangeTick(TickText tickText, ArcInfo arc, AngularTextBar textBar)
        {
            var angle = Interpolate.Linear(textBar.Minimum, textBar.Maximum, tickText.Value)
                                   .Clamp(0, 1)
                                   .Interpolate(textBar.Start, textBar.End, textBar.IsDirectionReversed);
            switch (textBar.TextOrientation)
            {
                case TextOrientation.Tangential:
                    tickText.Transform = new RotateTransform(angle.Degrees + 90);
                    break;
                case TextOrientation.TangentialFlipped:
                    tickText.Transform = new RotateTransform(angle.Degrees - 90);
                    break;
                case TextOrientation.RadialOut:
                    tickText.Transform = new RotateTransform(angle.Degrees);
                    break;
                case TextOrientation.RadialIn:
                    tickText.Transform = new RotateTransform(angle.Degrees - 180);
                    break;
                case TextOrientation.UseTransform:
                    tickText.Transform = textBar.TextTransform;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var pos = arc.GetPoint(angle);
            tickText.TranslateTransform.SetCurrentValue(TranslateTransform.XProperty, pos.X);
            tickText.TranslateTransform.SetCurrentValue(TranslateTransform.YProperty, pos.Y);
            return default(Thickness);
        }
    }
}