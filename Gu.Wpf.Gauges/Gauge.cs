namespace Gu.Wpf.Gauges
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;

    public partial class Gauge : ContentControl
    {
        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent(
            nameof(ValueChanged),
            RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<double>),
            typeof(Gauge));

        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached(
            nameof(Value),
            typeof(double),
            typeof(Gauge),
            new PropertyMetadata(
                double.NaN,
                OnValueChanged));
#pragma warning disable SA1202 // Elements must be ordered by access
        private static readonly DependencyPropertyKey ContentOverflowPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(ContentOverflow),
            typeof(Thickness),
            typeof(Gauge),
            new FrameworkPropertyMetadata(
                default(Thickness),
                FrameworkPropertyMetadataOptions.AffectsArrange));

        public static readonly DependencyProperty ContentOverflowProperty = ContentOverflowPropertyKey.DependencyProperty;
#pragma warning restore SA1202 // Elements must be ordered by access

        private Thickness contentOverflow = default(Thickness);

        /// <summary>
        /// Add / Remove ValueChangedEvent handler
        /// </summary>
        [Category("Behavior")]
        public event RoutedPropertyChangedEventHandler<double> ValueChanged
        {
            add => this.AddHandler(ValueChangedEvent, value);
            remove => this.RemoveHandler(ValueChangedEvent, value);
        }

        public double Value
        {
            get => (double)this.GetValue(ValueProperty);
            set => this.SetValue(ValueProperty, value);
        }

        /// <summary>
        /// The aggregate overflow of all children
        /// </summary>
        public Thickness ContentOverflow
        {
            get => (Thickness)this.GetValue(ContentOverflowProperty);
            protected set => this.SetValue(ContentOverflowPropertyKey, value);
        }

        public void RegisterOverflow(Thickness overflow)
        {
            this.contentOverflow = this.contentOverflow.Union(overflow);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            this.contentOverflow = default(Thickness);
            return base.MeasureOverride(constraint);
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            var size = base.ArrangeOverride(arrangeBounds);
            this.ContentOverflow = this.contentOverflow;
            return size;
        }

        /// <summary>
        ///     This method is invoked when the <see cref="Value"/> property changes.
        /// </summary>
        /// <param name="oldValue">The old value of the Value property.</param>
        /// <param name="newValue">The new value of the Value property.</param>
        protected virtual void OnValueChanged(double oldValue, double newValue)
        {
            this.RaiseEvent(new RoutedPropertyChangedEventArgs<double>(oldValue, newValue) { RoutedEvent = ValueChangedEvent });
        }

        /// <summary>
        ///     This method is invoked when the <see cref="Minimum"/> property changes.
        /// </summary>
        /// <param name="oldMinimum">The old value of the Minimum property.</param>
        /// <param name="newMinimum">The new value of the Minimum property.</param>
        protected virtual void OnMinimumChanged(double oldMinimum, double newMinimum)
        {
        }

        /// <summary>
        ///     This method is invoked when the <see cref="Maximum"/> property changes.
        /// </summary>
        /// <param name="oldMaximum">The old value of the Maximum property.</param>
        /// <param name="newMaximum">The new value of the Maximum property.</param>
        protected virtual void OnMaximumChanged(double oldMaximum, double newMaximum)
        {
        }

        /// <summary>
        ///     This method is invoked when the <see cref="IsDirectionReversed"/> property changes.
        /// </summary>
        /// <param name="oldValue">The old value of the Maximum property.</param>
        /// <param name="newValue">The new value of the Maximum property.</param>
        protected virtual void OnIsDirectionReversedChanged(bool oldValue, bool newValue)
        {
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Gauge gauge)
            {
                gauge.OnValueChanged((double)e.OldValue, (double)e.NewValue);
            }
        }
    }
}