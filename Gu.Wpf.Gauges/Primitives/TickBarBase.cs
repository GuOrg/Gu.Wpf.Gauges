namespace Gu.Wpf.Gauges
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class TickBarBase : FrameworkElement
    {
#pragma warning disable SA1202 // Elements must be ordered by access

        /// <summary>
        /// Identifies the <see cref="P:Bar.Minimum" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:Bar.Minimum" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty MinimumProperty = Gauge.MinimumProperty.AddOwner(
            typeof(TickBarBase),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits,
                (d, _) => ((TickBarBase)d).UpdateTicks()));

        /// <summary>
        /// Identifies the <see cref="P:Bar.Maximum" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:Bar.Maximum" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty MaximumProperty = Gauge.MaximumProperty.AddOwner(
            typeof(TickBarBase),
            new FrameworkPropertyMetadata(
                1.0,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits,
                (d, _) => ((TickBarBase)d).UpdateTicks()));

        /// <summary>
        /// Identifies the <see cref="P:Bar.IsDirectionReversed" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsDirectionReversedProperty = Gauge.IsDirectionReversedProperty.AddOwner(
            typeof(TickBarBase),
            new FrameworkPropertyMetadata(
                false,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:Bar.TickFrequency" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty TickFrequencyProperty = Slider.TickFrequencyProperty.AddOwner(
            typeof(TickBarBase),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits,
                (d, _) => ((TickBarBase)d).UpdateTicks()));

        public static readonly DependencyProperty SnapTicksToProperty = DependencyProperty.Register(
            nameof(SnapTicksTo),
            typeof(TickSnap),
            typeof(TickBarBase),
            new FrameworkPropertyMetadata(
                TickSnap.TickFrequency,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits,
                (d, _) => ((TickBarBase)d).UpdateTicks()));

        /// <summary>
        /// Identifies the <see cref="P:Bar.Ticks" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty TicksProperty = Slider.TicksProperty.AddOwner(
            typeof(TickBarBase),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits,
                OnTicksChanged));

        private static readonly DependencyPropertyKey AllTicksPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(AllTicks),
            typeof(IReadOnlyList<double>),
            typeof(TickBarBase),
            new FrameworkPropertyMetadata(
                default(IReadOnlyList<double>),
                FrameworkPropertyMetadataOptions.AffectsRender,
                OnAllTicksChanged));

        public static readonly DependencyProperty AllTicksProperty = AllTicksPropertyKey.DependencyProperty;

        public static readonly DependencyProperty ExcludeTicksProperty = DependencyProperty.Register(
            nameof(ExcludeTicks),
            typeof(IEnumerable<double>),
            typeof(TickBarBase),
            new PropertyMetadata(
                default(IEnumerable<double>),
                OnExcludeTicksChanged));

#pragma warning restore SA1202 // Elements must be ordered by access

        /// <summary>
        /// Gets or sets the <see cref="P:Bar.Minimum" />
        /// The default is 0
        /// </summary>
        public double Minimum
        {
            get => (double)this.GetValue(MinimumProperty);
            set => this.SetValue(MinimumProperty, value);
        }

        /// <summary>
        /// Gets or sets the highest possible <see cref="P:Bar.Maximum" /> of the range element.
        /// </summary>
        /// <returns>
        /// The highest possible <see cref="P:Bar.Maximum" /> of the range element. The default is 1.
        /// </returns>
        public double Maximum
        {
            get => (double)this.GetValue(MaximumProperty);
            set => this.SetValue(MaximumProperty, value);
        }

        /// <summary>
        /// Gets or sets the direction of increasing value.
        /// </summary>
        /// <returns>
        /// true if the direction of increasing value is to the left for a horizontal tickbar or down for a vertical tickbar; otherwise, false.
        /// The default is false.
        /// </returns>
        public bool IsDirectionReversed
        {
            get => (bool)this.GetValue(IsDirectionReversedProperty);
            set => this.SetValue(IsDirectionReversedProperty, value);
        }

        /// <summary>
        /// Gets or sets the interval between tick marks.
        /// </summary>
        /// <returns>
        /// The distance between tick marks. The default is (0).
        /// </returns>
        public double TickFrequency
        {
            get => (double)this.GetValue(TickFrequencyProperty);
            set => this.SetValue(TickFrequencyProperty, value);
        }

        /// <summary>
        /// Control snapping of ticks generated with <see cref="TickFrequency"/>
        /// </summary>
        public TickSnap SnapTicksTo
        {
            get => (TickSnap)this.GetValue(SnapTicksToProperty);
            set => this.SetValue(SnapTicksToProperty, value);
        }

        /// <summary>
        /// Gets or sets the positions of the tick marks to display for a <see cref="T:Bar" />.
        /// </summary>
        /// <returns>
        /// A set of tick marks to display for a <see cref="T:Bar" />. The default is null.
        /// </returns>
        public DoubleCollection Ticks
        {
            get => (DoubleCollection)this.GetValue(TicksProperty);
            set => this.SetValue(TicksProperty, value);
        }

        public IReadOnlyList<double> AllTicks
        {
            get => (IReadOnlyList<double>)this.GetValue(AllTicksProperty);
            protected set => this.SetValue(AllTicksPropertyKey, value);
        }

        /// <summary>
        /// Ticks to not render.
        /// </summary>
        public IEnumerable<double> ExcludeTicks
        {
            get => (IEnumerable<double>)this.GetValue(ExcludeTicksProperty);
            set => this.SetValue(ExcludeTicksProperty, value);
        }

        protected virtual void UpdateTicks()
        {
            this.AllTicks = Gauges.Ticks.Create(this.Minimum, this.Maximum, this.TickFrequency, this.SnapTicksTo)
                                      .Concat(this.Ticks ?? Enumerable.Empty<double>())
                                      .Where(x => x >= this.Minimum && x <= this.Maximum)
                                      .Except(this.ExcludeTicks ?? Enumerable.Empty<double>())
                                      .OrderBy(x => x)
                                      .ToArray();
        }

        protected virtual void OnAllTicksChanged(IReadOnlyList<double> oldValue, IReadOnlyList<double> newValue)
        {
        }

        private static void OnTicksChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bar = (TickBarBase)d;
            if (e.OldValue is DoubleCollection oldTicks &&
                !oldTicks.IsFrozen)
            {
                oldTicks.Changed -= bar.OnTickCollectionChanged;
            }

            if (e.NewValue is DoubleCollection newTicks &&
                !newTicks.IsFrozen)
            {
                newTicks.Changed += bar.OnTickCollectionChanged;
            }

            bar.UpdateTicks();
        }

        private static void OnAllTicksChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TickBarBase)d).OnAllTicksChanged((IReadOnlyList<double>)e.OldValue, (IReadOnlyList<double>)e.NewValue);
        }

        private static void OnExcludeTicksChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bar = (TickBarBase)d;
            if (e.OldValue is INotifyCollectionChanged oldTicks)
            {
                oldTicks.CollectionChanged -= bar.OnTickCollectionChanged;
            }

            if (e.NewValue is INotifyCollectionChanged newTicks)
            {
                newTicks.CollectionChanged += bar.OnTickCollectionChanged;
            }

            bar.UpdateTicks();
        }

        private void OnTickCollectionChanged(object sender, EventArgs eventArgs)
        {
            this.UpdateTicks();
        }
    }
}