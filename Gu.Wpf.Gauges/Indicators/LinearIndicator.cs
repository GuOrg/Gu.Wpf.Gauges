namespace Gu.Wpf.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;

    public class LinearIndicator : ValueIndicator
    {
        public static readonly DependencyProperty PlacementProperty = LinearGauge.PlacementProperty.AddOwner(
            typeof(LinearIndicator),
            new FrameworkPropertyMetadata(
                TickBarPlacement.Bottom,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.Inherits,
                OnPlacementChanged));

        private static readonly PropertyPath ValuePropertyPath = new PropertyPath(Gauge.ValueProperty);

        static LinearIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinearIndicator), new FrameworkPropertyMetadata(typeof(LinearIndicator)));
        }

        public TickBarPlacement Placement
        {
            get => (TickBarPlacement)this.GetValue(PlacementProperty);
            set => this.SetValue(PlacementProperty, value);
        }

        /// <inheritdoc />
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (BindingOperations.GetBinding(this, ValueProperty) == null &&
                double.IsNaN(this.Value))
            {
                var binding = new Binding
                {
                    RelativeSource = new RelativeSource(
                                      RelativeSourceMode.FindAncestor,
                                      typeof(LinearGauge),
                                      1),
                    Path = ValuePropertyPath
                };
                this.SetBinding(ValueProperty, binding);
            }
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
            if (double.IsNaN(this.Value))
            {
                return arrangeBounds;
            }

            if (this.VisualChild == null)
            {
                return arrangeBounds;
            }

            this.VisualChild.Arrange(this.ChildRect(this.PixelPosition(arrangeBounds)));
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
                    return new Point(this.Padding.Left, step.Interpolate(this.Padding.Top, arrangeBounds.Height - this.Padding.Bottom, this.IsDirectionReversed));
                case TickBarPlacement.Right:
                    return new Point(arrangeBounds.Width - this.Padding.Right, step.Interpolate(this.Padding.Top, arrangeBounds.Height - this.Padding.Bottom, this.IsDirectionReversed));
                case TickBarPlacement.Top:
                    return new Point(step.Interpolate(this.Padding.Left, arrangeBounds.Width - this.Padding.Right, this.IsDirectionReversed), this.Padding.Top);
                case TickBarPlacement.Bottom:
                    return new Point(step.Interpolate(this.Padding.Left, arrangeBounds.Width - this.Padding.Right, this.IsDirectionReversed), arrangeBounds.Height - this.Padding.Bottom);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected virtual void OnPlacementChanged(TickBarPlacement oldValue, TickBarPlacement newValue)
        {
        }

        private static void OnPlacementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LinearIndicator)d).OnPlacementChanged((TickBarPlacement)e.OldValue, (TickBarPlacement)e.NewValue);
        }
    }
}
