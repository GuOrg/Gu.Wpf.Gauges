namespace Gu.Wpf.Gauges
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Controls.Primitives;

    public class LinearIndicator : ValueIndicator
    {
#pragma warning disable SA1202 // Elements must be ordered by access
        public static readonly DependencyProperty PlacementProperty = LinearGauge.PlacementProperty.AddOwner(
            typeof(LinearIndicator),
            new FrameworkPropertyMetadata(
                TickBarPlacement.Bottom,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.Inherits,
                OnPlacementChanged));

        private static readonly DependencyPropertyKey OverflowPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(Overflow),
            typeof(Thickness),
            typeof(LinearIndicator),
            new PropertyMetadata(default(Thickness)));

        public static readonly DependencyProperty OverflowProperty = OverflowPropertyKey.DependencyProperty;
#pragma warning restore SA1202 // Elements must be ordered by access

        static LinearIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinearIndicator), new FrameworkPropertyMetadata(typeof(LinearIndicator)));
        }

        public TickBarPlacement Placement
        {
            get => (TickBarPlacement)this.GetValue(PlacementProperty);
            set => this.SetValue(PlacementProperty, value);
        }

        /// <summary>
        /// Gets a <see cref="Thickness"/> with values indicating how much the control draws outside its bounds.
        /// </summary>
        public Thickness Overflow
        {
            get => (Thickness)this.GetValue(OverflowProperty);
            protected set => this.SetValue(OverflowPropertyKey, value);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            if (this.VisualChild != null)
            {
                this.VisualChild.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                return this.Placement.IsHorizontal()
                    ? new Size(0, this.VisualChild.DesiredSize.Height)
                    : new Size(this.VisualChild.DesiredSize.Width, 0);
            }

            return default(Size);
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            var child = this.VisualChild;
            if (!double.IsNaN(this.Value) &&
                child != null)
            {
                child.Arrange(this.ChildRect(this.PixelPosition(arrangeBounds)));
                var w = this.Placement.IsHorizontal()
                    ? child.RenderSize.Width / 2
                    : child.RenderSize.Height / 2;

                this.Overflow = this.Placement.IsHorizontal()
                    ? new Thickness(Math.Max(0, w - this.Padding.Left), 0, Math.Max(0, w - this.Padding.Right), 0)
                    : new Thickness(0, Math.Max(0, w - this.Padding.Top), 0, Math.Max(0, w - this.Padding.Bottom));
            }
            else
            {
                this.Overflow = default(Thickness);
            }

            this.RegisterOverflow(this.Overflow);
            return arrangeBounds;
        }

        protected Rect ChildRect(Point position)
        {
            var size = this.VisualChild.DesiredSize;
            switch (this.Placement)
            {
                case TickBarPlacement.Left:
                    return new Rect(position.X, position.Y - (size.Height / 2), size.Width, size.Height);
                case TickBarPlacement.Right:
                    return new Rect(position.X - size.Width, position.Y - (size.Height / 2), size.Width, size.Height);
                case TickBarPlacement.Top:
                    return new Rect(position.X - (size.Width / 2), position.Y, size.Width, size.Height);
                case TickBarPlacement.Bottom:
                    return new Rect(position.X - (size.Width / 2), position.Y - size.Height, size.Width, size.Height);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected Point PixelPosition(Size arrangeBounds)
        {
            var step = Interpolate.Linear(this.Minimum, this.Maximum, this.Value)
                                  .Clamp(0, 1);
            switch (this.Placement)
            {
                case TickBarPlacement.Left:
                    return new Point(this.Padding.Left, step.InterpolateVertical(arrangeBounds, this.Padding, this.IsDirectionReversed));
                case TickBarPlacement.Right:
                    return new Point(arrangeBounds.Width - this.Padding.Right, step.InterpolateVertical(arrangeBounds, this.Padding, this.IsDirectionReversed));
                case TickBarPlacement.Top:
                    return new Point(step.InterpolateHorizontal(arrangeBounds, this.Padding, this.IsDirectionReversed), this.Padding.Top);
                case TickBarPlacement.Bottom:
                    return new Point(step.InterpolateHorizontal(arrangeBounds, this.Padding, this.IsDirectionReversed), arrangeBounds.Height - this.Padding.Bottom);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        protected virtual void OnPlacementChanged(TickBarPlacement oldValue, TickBarPlacement newValue)
        {
        }

        private static void OnPlacementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LinearIndicator)d).OnPlacementChanged((TickBarPlacement)e.OldValue, (TickBarPlacement)e.NewValue);
        }
    }
}
