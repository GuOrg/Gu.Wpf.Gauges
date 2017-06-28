namespace Gu.Wpf.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Controls.Primitives;

    public class LinearRange : LinearIndicator
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

            var si = Interpolate.Linear(this.Minimum, this.Maximum, this.Start);
            var ei = Interpolate.Linear(this.Minimum, this.Maximum, this.End);

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

        private static void OnStartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LinearRange)d).OnStartChanged((double)e.NewValue);
        }

        private static void OnEndChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LinearRange)d).OnEndChanged((double)e.NewValue);
        }
    }
}