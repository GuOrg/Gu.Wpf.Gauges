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

            tickText.Transform = Transform.Identity;
            var textGeometry = tickText.BuildGeometry(new Point(0, 0));
            var compVector = textGeometry.Bounds.TopLeft.ToVector();
            var rotatedCompVector = new Vector(compVector.X, compVector.Y);
            var diffVector = new Vector(0, 0);
            var textAngle = Angle.Zero;

            switch (textBar.TextOrientation)
            {
                case TextOrientation.Tangential:
                    textAngle = angle.IsUpperQuadrants() ? angle : angle + Angle.FromDegrees(180);
                    tickText.Transform = new RotateTransform(textAngle.Degrees);
                    rotatedCompVector = compVector.Rotate(textAngle);
                    var vectorFromCenterToUpperLeft = new Vector(textGeometry.Bounds.Width / 2, textGeometry.Bounds.Height / 2);
                    var rotatedCenterVectorComp = vectorFromCenterToUpperLeft.Rotate(textAngle);
                    diffVector = vectorFromCenterToUpperLeft - rotatedCenterVectorComp;
                    break;
                case TextOrientation.Horizontal:
                    tickText.Transform = Transform.Identity;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var pos = arc.GetUpperLeftPointAtOffset(textGeometry.Bounds.Size.Rotate(textAngle), angle, 0);
            tickText.TranslateTransform.SetCurrentValue(TranslateTransform.XProperty, pos.X + diffVector.X - rotatedCompVector.X);
            tickText.TranslateTransform.SetCurrentValue(TranslateTransform.YProperty, pos.Y + diffVector.Y - rotatedCompVector.Y);
            return default(Thickness);
        }
    }
}