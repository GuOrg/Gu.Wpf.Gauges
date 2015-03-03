namespace Gu.Gauges
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;

    public class AngularTextBar : TextTickBar, ITextFormat
    {
        public static readonly DependencyProperty MinAngleProperty = AngularBar.MinAngleProperty.AddOwner(
            typeof(AngularTextBar),
            new FrameworkPropertyMetadata(
                -180.0,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty MaxAngleProperty = AngularBar.MaxAngleProperty.AddOwner(
            typeof(AngularTextBar),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Gets or sets the <see cref="P:AngularBar.MinAngle" />
        /// The default is -180
        /// </summary>
        public double MinAngle
        {
            get { return (double)this.GetValue(MinAngleProperty); }
            set { this.SetValue(MinAngleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="P:AngularBar.MaxAngle" />
        /// The default is 0
        /// </summary>
        public double MaxAngle
        {
            get { return (double)this.GetValue(MaxAngleProperty); }
            set { this.SetValue(MaxAngleProperty, value); }
        }

        protected override void OnRender(DrawingContext dc)
        {
            var arc = Arc.Fill(this.RenderSize, this.MinAngle, this.MaxAngle, this.IsDirectionReversed);
            for (int i = 0; i < this.AllTicks.Count; i++)
            {
                var tick = this.AllTicks[i];
                var text = this.AllTexts[i];
                var angle = TickHelper.ToAngle(tick, this.Minimum, this.Maximum, arc);
                var point = arc.GetPoint(angle, -this.ReservedSpace / 2 - this.TextSpace / 2);
                var textPosition = new TextPosition(text, new TextPositionOptions(this.TextOrientation, angle), point, angle);
                dc.DrawText(text, textPosition);
            }
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var bounds = new Rect();
            var midPoint = new Point(0, 0);
            var arc = new Arc(midPoint, this.MinAngle, this.MaxAngle, this.ReservedSpace, this.IsDirectionReversed);
            for (int i = 0; i < this.AllTicks.Count; i++)
            {
                var tick = this.AllTicks[i];
                var text = this.AllTexts[i];
                var angle = TickHelper.ToAngle(tick, this.Minimum, this.Maximum, arc);
                var point = arc.GetPoint(angle);
                var textPosition = new TextPosition(text, new TextPositionOptions(this.TextOrientation, angle), point, angle);
                bounds.Union(textPosition.TransformedBounds);
            }
            var points = new[] { bounds.TopLeft, bounds.TopRight, bounds.BottomRight, bounds.BottomLeft };

            this.TextSpace = 2 * points.Max(p => (p - midPoint).Length);
            return bounds.Size;
        }
    }
}