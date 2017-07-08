namespace Gu.Wpf.Gauges
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;

    public class AngularTextBar : TextTickBar
    {
        public static readonly DependencyProperty TextOrientationProperty = AngularGauge.TextOrientationProperty.AddOwner(
            typeof(AngularTextBar),
            new FrameworkPropertyMetadata(
                TextOrientation.Tangential,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

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
        /// Gets or sets the <see cref="T:Gu.Wpf.Gauges.TextOrientation" />
        /// Default is Tangential
        /// </summary>
        public TextOrientation TextOrientation
        {
            get => (TextOrientation)this.GetValue(TextOrientationProperty);
            set => this.SetValue(TextOrientationProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="P:AngularBar.MinAngle" />
        /// The default is -180
        /// </summary>
        public double MinAngle
        {
            get => (double)this.GetValue(MinAngleProperty);
            set => this.SetValue(MinAngleProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="P:AngularBar.MaxAngle" />
        /// The default is 0
        /// </summary>
        public double MaxAngle
        {
            get => (double)this.GetValue(MaxAngleProperty);
            set => this.SetValue(MaxAngleProperty, value);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            var rect = default(Rect);
            if (this.AllTexts != null)
            {
                foreach (var tickText in this.AllTexts)
                {
                    rect.Union(new Rect(tickText.Geometry.Bounds.Size));
                }
            }

            return rect.Size;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (this.AllTexts != null)
            {
                foreach (var tickText in this.AllTexts)
                {
                    tickText.TranslateTransform.SetCurrentValue(TranslateTransform.XProperty, 0.0d);
                    tickText.TranslateTransform.SetCurrentValue(TranslateTransform.YProperty, 0.0d);
                    var pos = this.PixelPosition(tickText, finalSize);
                    tickText.TranslateTransform.SetCurrentValue(TranslateTransform.XProperty, pos.X);
                    tickText.TranslateTransform.SetCurrentValue(TranslateTransform.YProperty, pos.Y);
                }
            }

            return finalSize;
        }

        protected override void OnRender(DrawingContext dc)
        {
            if (this.Foreground == null ||
                this.AllTexts == null ||
                this.AllTexts.Count == 0)
            {
                return;
            }

            foreach (var tickText in this.AllTexts)
            {
                dc.DrawGeometry(this.Foreground, null, tickText.Geometry);
            }
        }

        /// <summary>
        /// Get the interpolated pixel position for the value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="finalSize"></param>
        /// <returns></returns>
        protected virtual double PixelPosition(double value, Size finalSize)
        {
            var scale = Interpolate.Linear(this.Minimum, this.Maximum, value)
                                   .Clamp(0, 1);

            throw new NotImplementedException();
        }

        protected virtual Point PixelPosition(TickText tickText, Size finalSize)
        {
            var pos = this.PixelPosition(tickText.Value, finalSize);
            var bounds = tickText.Geometry.Bounds;
            throw new NotImplementedException();
        }

        protected virtual TickText CreateTickText(double value)
        {
            return new TickText(
                value,
                this.StringFormat ?? string.Empty,
                this.TypeFace,
                this.FontSize,
                this.Foreground,
                this.TextOrientation == TextOrientation.UseTransform ? this.TextTransform : null);
        }

        protected override void UpdateTexts()
        {
            if (this.AllTicks == null || this.AllTicks.Count == 0)
            {
                this.AllTexts = null;
                return;
            }

            this.AllTexts = this.AllTicks.Select(this.CreateTickText).ToArray();
        }
    }
}