namespace Gu.Wpf.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Controls.Primitives;

    public class LinearRange : Indicator
    {
        public static readonly DependencyProperty StartProperty = DependencyProperty.Register(
            nameof(Start),
            typeof(double),
            typeof(LinearRange),
            new FrameworkPropertyMetadata(
                double.NaN,
                FrameworkPropertyMetadataOptions.AffectsArrange,
                OnStartChanged));

        public static readonly DependencyProperty EndProperty = DependencyProperty.Register(
            nameof(End),
            typeof(double),
            typeof(LinearRange),
            new FrameworkPropertyMetadata(
                double.NaN,
                FrameworkPropertyMetadataOptions.AffectsArrange,
                OnEndChanged));

        public static readonly DependencyProperty PlacementProperty = LinearGauge.PlacementProperty.AddOwner(
            typeof(LinearRange),
            new FrameworkPropertyMetadata(
                TickBarPlacement.Bottom,
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.Inherits,
                OnPlacementChanged));

        static LinearRange()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(LinearRange),
                new FrameworkPropertyMetadata(typeof(LinearRange)));
        }

        public double Start
        {
            get => (double)this.GetValue(StartProperty);
            set => this.SetValue(StartProperty, value);
        }

        public double End
        {
            get => (double)this.GetValue(EndProperty);
            set => this.SetValue(EndProperty, value);
        }

        public TickBarPlacement Placement
        {
            get => (TickBarPlacement)this.GetValue(PlacementProperty);
            set => this.SetValue(PlacementProperty, value);
        }

        protected virtual void OnEndChanged(double newValue)
        {
            LinearPanel.SetEnd(this, newValue);
        }

        protected virtual void OnStartChanged(double newValue)
        {
            LinearPanel.SetStart(this, newValue);
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            if (double.IsNaN(this.Value))
            {
                return arrangeBounds;
            }

            var child = this.GetVisualChild(0) as UIElement;
            if (child == null)
            {
                return arrangeBounds;
            }

            var min = this.IsDirectionReversed ? this.Maximum : this.Minimum;
            var max = this.IsDirectionReversed ? this.Minimum : this.Maximum;
            var start = this.IsDirectionReversed ? this.End : this.Start;
            var end = this.IsDirectionReversed ? this.Start : this.End;

            var si = Interpolate.Linear(min, max, start);
            var ei = Interpolate.Linear(min, max, end);

            switch (this.Placement)
            {
                case TickBarPlacement.Left:
                case TickBarPlacement.Right:
                    child.Arrange(new Rect(new Point(0, si * arrangeBounds.Height), new Point(arrangeBounds.Width, ei * arrangeBounds.Height)));
                    break;
                case TickBarPlacement.Top:
                case TickBarPlacement.Bottom:
                    child.Arrange(new Rect(new Point(si * arrangeBounds.Width, 0), new Point(ei * arrangeBounds.Width, arrangeBounds.Height)));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return arrangeBounds;
        }

        protected virtual void OnPlacementChanged(TickBarPlacement oldValue, TickBarPlacement newValue)
        {
        }

        private static void OnStartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LinearRange)d).OnStartChanged((double)e.NewValue);
        }

        private static void OnEndChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LinearRange)d).OnEndChanged((double)e.NewValue);
        }

        private static void OnPlacementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LinearRange)d).OnPlacementChanged((TickBarPlacement)e.OldValue, (TickBarPlacement)e.NewValue);
        }
    }
}