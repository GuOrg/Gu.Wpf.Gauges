namespace Gu.Gauges
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    public class LinearTextTickBar : TextTickBar
    {
        /// <summary>
        /// Identifies the <see cref="P:Bar.Placement" /> dependency property. This property is read-only.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:Bar.Placement" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty PlacementProperty = TickBar.PlacementProperty.AddOwner(
            typeof(LinearTextTickBar),
            new FrameworkPropertyMetadata(
                TickBarPlacement.Bottom,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));


        /// <summary>
        /// Gets or sets where tick marks appear  relative to a <see cref="T:System.Windows.Controls.Primitives.Track" /> of a <see cref="T:System.Windows.Controls.Slider" /> control.  
        /// </summary>
        /// <returns>
        /// A <see cref="T:TickBarPlacement" /> enumeration value that identifies the position of the <see cref="T:LinearTextTickBar" /> in the <see cref="T:System.Windows.Style" /> layout of a <see cref="T:System.Windows.Controls.Slider" />. The default value is <see cref="F:Bar.Top" />.
        /// </returns>
        public TickBarPlacement Placement
        {
            get { return (TickBarPlacement)this.GetValue(PlacementProperty); }
            set { this.SetValue(PlacementProperty, value); }
        }


        protected override Size MeasureOverride(Size availableSize)
        {
            if (this.AllTexts == null || !this.AllTexts.Any())
            {
                return new Size(0, 0);
            }
            var textHeight = Math.Ceiling(this.FontSize * this.FontFamily.LineSpacing);

            double w = 0;
            double h = 0;
            switch (this.TextOrientation)
            {

                case TextOrientation.VerticalUp:
                case TextOrientation.VerticalDown:
                    w = textHeight;
                    h = this.AllTexts.Max(t => t.Width);
                    this.TextSpace = textHeight;
                    break;
                case TextOrientation.Horizontal:
                case TextOrientation.Tangential:
                case TextOrientation.RadialOut:
                    w = this.AllTexts.Max(x => x.Width);
                    h = textHeight;
                    this.TextSpace = w;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            var margin = this.TextSpace / 2;
            switch (this.Placement)
            {
                case TickBarPlacement.Left:
                case TickBarPlacement.Right:
                    this.TextSpaceMargin = new Thickness(0, margin, 0, margin);
                    break;
                case TickBarPlacement.Top:
                case TickBarPlacement.Bottom:
                    this.TextSpaceMargin = new Thickness(margin, 0, margin, 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            var size = new Size(w, h);
            if (size.IsInvalid())
            {
                return new Size(0, 0);
            }
            return size;
        }

        protected override void OnRender(DrawingContext dc)
        {
            if (this.Foreground == null)
            {
                return;
            }
            var line = new Line(this.ActualWidth, this.ActualHeight, this.ReservedSpace, this.Placement, this.IsDirectionReversed);
            for (int i = 0; i < this.AllTicks.Count; i++)
            {
                var tick = this.AllTicks[i];
                var pos = TickHelper.ToPos(tick, this.Minimum, this.Maximum, line);
                var text = this.AllTexts[i];
                var textPosition = new TextPosition(text, new TextPositionOptions(this.Placement, this.TextOrientation), pos, 0);
                dc.DrawText(text, textPosition);
            }
        }
    }
}