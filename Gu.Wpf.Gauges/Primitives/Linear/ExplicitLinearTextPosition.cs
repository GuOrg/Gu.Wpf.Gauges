namespace Gu.Wpf.Gauges
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Markup;
    using System.Windows.Media;

    public class ExplicitLinearTextPosition : LinearTextPosition, INotifyPropertyChanged
    {
        private HorizontalTextAlignment horizontal;
        private VerticalTextAlignment vertical;

        public ExplicitLinearTextPosition()
        {
        }

        public ExplicitLinearTextPosition(HorizontalTextAlignment horizontal, VerticalTextAlignment vertical)
        {
            this.Horizontal = horizontal;
            this.Vertical = vertical;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [ConstructorArgument("horizontal")]
        public HorizontalTextAlignment Horizontal
        {
            get => this.horizontal;

            set
            {
                if (value == this.horizontal)
                {
                    return;
                }

                this.horizontal = value;
                this.OnPropertyChanged();
                this.OnArrangeDirty();
            }
        }

        [ConstructorArgument("vertical")]
        public VerticalTextAlignment Vertical
        {
            get => this.vertical;

            set
            {
                if (value == this.vertical)
                {
                    return;
                }

                this.vertical = value;
                this.OnPropertyChanged();
                this.OnArrangeDirty();
            }
        }

        public override void ArrangeTick(TickText tickText, Size arrangeSize, LinearTextBar textBar)
        {
            var pos = this.PixelPosition(tickText.Value, arrangeSize, textBar);
            var bounds = tickText.Geometry.Bounds;
            if (tickText.TranslateTransform != null)
            {
                bounds.Offset(-new Vector(tickText.TranslateTransform.X, tickText.TranslateTransform.Y));
            }

            if (textBar.Placement.IsHorizontal())
            {
                var x = -bounds.Left;
                switch (this.Horizontal)
                {
                    case HorizontalTextAlignment.Left:
                        x += pos;
                        break;
                    case HorizontalTextAlignment.Center:
                        x += pos - (bounds.Width / 2);
                        break;
                    case HorizontalTextAlignment.Right:
                        x += pos - bounds.Width;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                var y = -bounds.Top;
                switch (this.Vertical)
                {
                    case VerticalTextAlignment.Top:
                        y += textBar.Padding.Top;
                        break;
                    case VerticalTextAlignment.Center:
                        y += (arrangeSize.Height - bounds.Height) / 2;
                        break;
                    case VerticalTextAlignment.Bottom:
                        y += arrangeSize.Height - bounds.Height - textBar.Padding.Bottom;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (tickText.TranslateTransform is null)
                {
                    return;
                }

                tickText.TranslateTransform.SetCurrentValue(TranslateTransform.XProperty, x);
                tickText.TranslateTransform.SetCurrentValue(TranslateTransform.YProperty, y);
            }
            else
            {
                var x = -bounds.Left;
                switch (this.Horizontal)
                {
                    case HorizontalTextAlignment.Left:
                        x += textBar.Padding.Left;
                        break;
                    case HorizontalTextAlignment.Center:
                        x += (arrangeSize.Width - bounds.Width) / 2;
                        break;
                    case HorizontalTextAlignment.Right:
                        x += arrangeSize.Width - bounds.Width - textBar.Padding.Right;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                var y = -bounds.Top;
                switch (this.Vertical)
                {
                    case VerticalTextAlignment.Top:
                        y += pos;
                        break;
                    case VerticalTextAlignment.Center:
                        y += pos - (bounds.Height / 2);
                        break;
                    case VerticalTextAlignment.Bottom:
                        y += pos - bounds.Height;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (tickText.TranslateTransform is null)
                {
                    return;
                }

                tickText.TranslateTransform.SetCurrentValue(TranslateTransform.XProperty, x);
                tickText.TranslateTransform.SetCurrentValue(TranslateTransform.YProperty, y);
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Get the interpolated pixel position for the value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="finalSize"></param>
        /// <param name="textBar">The <see cref="LinearTextBar"/> to generate the tick for.</param>
        protected virtual double PixelPosition(double value, Size finalSize, LinearTextBar textBar)
        {
            var interpolation = Interpolate.Linear(textBar.Minimum, textBar.Maximum, value)
                                           .Clamp(0, 1);

            if (textBar.Placement.IsHorizontal())
            {
                var pos = interpolation.Interpolate(textBar.Padding.Left, finalSize.Width - textBar.Padding.Right);
                return textBar.IsDirectionReversed
                    ? finalSize.Width - pos
                    : pos;
            }
            else
            {
                var pos = interpolation.Interpolate(textBar.Padding.Bottom, finalSize.Height - textBar.Padding.Top);
                return textBar.IsDirectionReversed
                    ? pos
                    : finalSize.Height - pos;
            }
        }
    }
}