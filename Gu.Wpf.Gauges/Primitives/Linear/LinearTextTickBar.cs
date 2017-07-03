namespace Gu.Wpf.Gauges
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
        public static readonly DependencyProperty PlacementProperty = LinearGauge.PlacementProperty.AddOwner(
            typeof(LinearTextTickBar),
            new FrameworkPropertyMetadata(
                TickBarPlacement.Bottom,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty TextTransformProperty = DependencyProperty.Register(
            nameof(TextTransform),
            typeof(Transform),
            typeof(LinearTextTickBar),
            new FrameworkPropertyMetadata(
                default(Transform),
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Gets or sets where tick marks appear  relative to a <see cref="T:System.Windows.Controls.Primitives.Track" /> of a <see cref="T:System.Windows.Controls.Slider" /> control.
        /// </summary>
        /// <returns>
        /// A <see cref="T:TickBarPlacement" /> enumeration value that identifies the position of the <see cref="T:LinearTextTickBar" /> in the <see cref="T:System.Windows.Style" /> layout of a <see cref="T:System.Windows.Controls.Slider" />. The default value is <see cref="F:Bar.Top" />.
        /// </returns>
        public TickBarPlacement Placement
        {
            get => (TickBarPlacement)this.GetValue(PlacementProperty);
            set => this.SetValue(PlacementProperty, value);
        }

        public Transform TextTransform
        {
            get => (Transform)this.GetValue(TextTransformProperty);
            set => this.SetValue(TextTransformProperty, value);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            if (this.AllTexts == null ||
                this.AllTexts.Count == 0)
            {
                return new Size(0, 0);
            }

            throw new NotImplementedException();
            //var textHeight = Math.Ceiling(this.FontSize * this.FontFamily.LineSpacing);
            //double w = 0;
            //double h = 0;
            //switch (this.TextOrientation)
            //{
            //    case TextOrientation.VerticalUp:
            //    case TextOrientation.VerticalDown:
            //        w = textHeight;
            //        h = this.AllTexts_.Max(t => t.Width);
            //        this.TextSpace = textHeight;
            //        break;
            //    case TextOrientation.Horizontal:
            //    case TextOrientation.Tangential:
            //    case TextOrientation.RadialOut:
            //        w = this.AllTexts_.Max(x => x.Width);
            //        h = textHeight;
            //        this.TextSpace = w;
            //        break;
            //    default:
            //        throw new ArgumentOutOfRangeException();
            //}

            //var margin = this.TextSpace / 2;
            //switch (this.Placement)
            //{
            //    case TickBarPlacement.Left:
            //    case TickBarPlacement.Right:
            //        this.TextSpaceMargin = new Thickness(0, margin, 0, margin);
            //        break;
            //    case TickBarPlacement.Top:
            //    case TickBarPlacement.Bottom:
            //        this.TextSpaceMargin = new Thickness(margin, 0, margin, 0);
            //        break;
            //    default:
            //        throw new ArgumentOutOfRangeException();
            //}

            //var size = new Size(w, h);
            //if (size.IsNanOrEmpty())
            //{
            //    return new Size(0, 0);
            //}

            //return size;
        }

        protected override void OnRender(DrawingContext dc)
        {
            if (this.Foreground == null ||
                this.AllTicks == null)
            {
                return;
            }

            throw new NotImplementedException();
            //var line = new Line(this.ActualWidth, this.ActualHeight, 0, this.Placement, this.IsDirectionReversed);
            //for (var i = 0; i < this.AllTicks.Count; i++)
            //{
                var pos = Gauges.Ticks.ToPos(tick, this.Minimum, this.Maximum, line);
            //    var pos = TickHelper.ToPos(tick, this.Minimum, this.Maximum, line);
            //    var text = this.AllTexts_[i];
            //    switch (this.HorizontalTextAlignment)
            //    {
            //        case HorizontalTextAlignment.Left:
            //            break;
            //        case HorizontalTextAlignment.Center:
            //            switch (this.TextOrientation)
            //            {
            //                case TextOrientation.Horizontal:
            //                    pos.Offset((this.ActualWidth - text.Width) / 2, 0);
            //                    break;
            //                case TextOrientation.VerticalUp:
            //                    break;
            //                case TextOrientation.VerticalDown:
            //                    break;
            //                case TextOrientation.Tangential:
            //                    break;
            //                case TextOrientation.RadialOut:
            //                    break;
            //                default:
            //                    throw new ArgumentOutOfRangeException();
            //            }

            //            break;
            //        case HorizontalTextAlignment.Right:
            //            switch (this.TextOrientation)
            //            {
            //                case TextOrientation.Horizontal:
            //                    pos.Offset(this.ActualWidth - text.Width, 0);
            //                    break;
            //                case TextOrientation.VerticalUp:
            //                    break;
            //                case TextOrientation.VerticalDown:
            //                    break;
            //                case TextOrientation.Tangential:
            //                    break;
            //                case TextOrientation.RadialOut:
            //                    break;
            //                default:
            //                    throw new ArgumentOutOfRangeException();
            //            }

            //            break;
            //        default:
            //            throw new ArgumentOutOfRangeException();
            //    }

            //    var textPosition = new TextPosition(text, new TextPositionOptions(this.Placement, this.TextOrientation), pos, 0);
            //    dc.DrawText(text, textPosition);
            //}
        }

        protected override void UpdateTexts()
        {
            if (this.AllTicks == null || this.AllTicks.Count == 0)
            {
                this.AllTexts = null;
                return;
            }

            this.AllTexts = this.AllTicks.Select(
                x => new TickText(
                    x,
                    this.StringFormat,
                    this.TypeFace,
                    this.FontSize,
                    this.Foreground,
                    ,
                    this.TextTransform));
        }
    }
}