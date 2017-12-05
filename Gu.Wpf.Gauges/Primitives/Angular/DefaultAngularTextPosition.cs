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
                    tickText.Transform = new RotateTransform(angle.Degrees);
                    //compVector = compVector.Rotate(angle);
                    break;
                case TextOrientation.Horizontal:
                    tickText.Transform = MatrixTransform.Identity;
                    break;
                case TextOrientation.UseTransform:
                    tickText.Transform = textBar.TextTransform;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var textGeometry = tickText.BuildGeometry(new Point(0, 0));

            var compVector = textGeometry.Bounds.TopLeft.ToVector();

            var pos = arc.GetUpperLeftPointAtOffset(tickText.Geometry.Bounds.Size, angle, 0);
            var pos2 = arc.GetPointAtRadius(angle, arc.Radius);

            tickText.TranslateTransform.SetCurrentValue(TranslateTransform.XProperty, pos.X - compVector.X);
            tickText.TranslateTransform.SetCurrentValue(TranslateTransform.YProperty, pos.Y - compVector.Y);
            return default(Thickness);
        }
    }
}