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
                return this.VisualChild.DesiredSize;
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

            Line l1;
            Line l2;
            switch (this.Placement)
            {
                case TickBarPlacement.Left:
                case TickBarPlacement.Right:
                    l1 = new Line(arrangeBounds, 0, TickBarPlacement.Left, this.IsDirectionReversed);
                    l2 = new Line(arrangeBounds, 0, TickBarPlacement.Right, this.IsDirectionReversed);
                    break;
                case TickBarPlacement.Top:
                case TickBarPlacement.Bottom:
                    l1 = new Line(arrangeBounds, 0, TickBarPlacement.Top, this.IsDirectionReversed);
                    l2 = new Line(arrangeBounds, 0, TickBarPlacement.Bottom, this.IsDirectionReversed);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var p1 = l1.Interpolate(this.Value, this.Minimum, this.Maximum);
            var p2 = l2.Interpolate(this.Value, this.Minimum, this.Maximum);
            var w2 = this.VisualChild.DesiredSize.Width / 2;
            var h2 = this.VisualChild.DesiredSize.Height / 2;
            var ps = new Point(p1.X - w2, p1.Y - h2);
            var pe = new Point(p2.X + w2, p2.Y + h2);
            var rect = new Rect(arrangeBounds);
            var rect1 = new Rect(ps, pe);
            rect.Intersect(rect1);
            this.VisualChild.Arrange(rect.IsEmpty ? default(Rect) : rect);
            return arrangeBounds;
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
