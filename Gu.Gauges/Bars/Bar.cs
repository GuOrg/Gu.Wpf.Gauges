namespace Gu.Gauges
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    public class Bar : FrameworkElement
    {
        /// <summary>
        /// Identifies the <see cref="P:Bar.Minimum" /> dependency property. 
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:Bar.Minimum" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty MinimumProperty = RangeBase.MinimumProperty.AddOwner(
            typeof(Bar),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.Inherits,
                OnMinimumChanged));

        /// <summary>
        /// Identifies the <see cref="P:Bar.Maximum" /> dependency property. 
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:Bar.Maximum" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty MaximumProperty = RangeBase.MaximumProperty.AddOwner(
            typeof(Bar),
            new FrameworkPropertyMetadata(
                1.0,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.Inherits,
                OnMaximumChanged));

        /// <summary>
        /// Identifies the <see cref="P:BlockBar.Placement" /> dependency property. This property is read-only.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:BlockBar.Placement" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty PlacementProperty = TickBar.PlacementProperty.AddOwner(
            typeof(Bar),
            new FrameworkPropertyMetadata(
                TickBarPlacement.Bottom,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

        /// <summary>
        /// Identifies the <see cref="P:Bar.ReservedSpace" /> dependency property. This property is read-only.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:Bar.ReservedSpace" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty ReservedSpaceProperty = TickBar.ReservedSpaceProperty.AddOwner(
            typeof(Bar),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange));

        /// <summary>
        /// Identifies the <see cref="P:Bar.TickFrequency" /> dependency property. 
        /// </summary>
        public static readonly DependencyProperty TickFrequencyProperty = Slider.TickFrequencyProperty.AddOwner(
            typeof(Bar),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.Inherits,
                OnTickFrequencyChanged));

        /// <summary>
        /// Identifies the <see cref="P:Bar.Ticks" /> dependency property. 
        /// </summary>
        public static readonly DependencyProperty TicksProperty = Slider.TicksProperty.AddOwner(
            typeof(Bar),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.Inherits,
                OnTicksChanged));

        /// <summary>
        /// Identifies the <see cref="P:LinearTickBar.IsDirectionReversed" /> dependency property. 
        /// </summary>
        public static readonly DependencyProperty IsDirectionReversedProperty = Slider.IsDirectionReversedProperty.AddOwner(
            typeof(Bar),
            new FrameworkPropertyMetadata(
                false,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.Inherits));

        private double[] allTicks;

        /// <summary>
        /// Gets or sets the <see cref="P:Bar.Minimum" />
        /// The default is 0
        /// </summary>
        public double Minimum
        {
            get { return (double)this.GetValue(MinimumProperty); }
            set { this.SetValue(MinimumProperty, value); }
        }

        /// <summary>
        /// Gets or sets the highest possible <see cref="P:Bar.Value" /> of the range element.  
        /// </summary>
        /// <returns>
        /// The highest possible <see cref="P:Bar.Value" /> of the range element. The default is 1.
        /// </returns>
        public double Maximum
        {
            get { return (double)this.GetValue(MaximumProperty); }
            set { this.SetValue(MaximumProperty, value); }
        }

        /// <summary>
        /// Gets or sets where tick marks appear  relative to a <see cref="T:System.Windows.Controls.Primitives.Track" /> of a <see cref="T:System.Windows.Controls.Slider" /> control.  
        /// </summary>
        /// <returns>
        /// A <see cref="T:BlockBarPlacement" /> enumeration value that identifies the position of the <see cref="T:BlockBar" /> in the <see cref="T:System.Windows.Style" /> layout of a <see cref="T:System.Windows.Controls.Slider" />. The default value is <see cref="F:BlockBarPlacement.Top" />.
        /// </returns>
        public TickBarPlacement Placement
        {
            get { return (TickBarPlacement)this.GetValue(PlacementProperty); }
            set { this.SetValue(PlacementProperty, value); }
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
            get { return (bool)this.GetValue(IsDirectionReversedProperty); }
            set { this.SetValue(IsDirectionReversedProperty, value); }
        }

        /// <summary>
        /// Gets or sets a space buffer for the area that contains the tick marks that are specified for a <see cref="T:Bar" />.  
        /// </summary>
        /// <returns>
        /// A value that represents the total buffer area on either side of the row or column of tick marks. The default value is zero (0.0).
        /// </returns>
        public double ReservedSpace
        {
            get { return (double)this.GetValue(ReservedSpaceProperty); }
            set { this.SetValue(ReservedSpaceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the interval between tick marks.  
        /// </summary>
        /// <returns>
        /// The distance between tick marks. The default is (0).
        /// </returns>
        public double TickFrequency
        {
            get { return (double)this.GetValue(TickFrequencyProperty); }
            set { this.SetValue(TickFrequencyProperty, value); }
        }

        /// <summary>
        /// Gets or sets the positions of the tick marks to display for a <see cref="T:Bar" />. 
        /// </summary>
        /// <returns>
        /// A set of tick marks to display for a <see cref="T:Bar" />. The default is null.
        /// </returns>
        public DoubleCollection Ticks
        {
            get { return (DoubleCollection)this.GetValue(TicksProperty); }
            set { this.SetValue(TicksProperty, value); }
        }

        protected IReadOnlyList<double> AllTicks
        {
            get
            {
                return this.allTicks;
            }
        }

        protected virtual void OnTicksChanged()
        {
            this.allTicks = TickHelper.CreateTicks(this.Minimum, this.Maximum, this.TickFrequency)
                                      .Concat(this.Ticks ?? Enumerable.Empty<double>())
                                      .Where(x => x >= this.Minimum && x <= this.Maximum)
                                      .OrderBy(x => x)
                                      .ToArray();
        }

        private static void OnMinimumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bar = (Bar)d;
            bar.OnTicksChanged();
        }

        private static void OnMaximumChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bar = (Bar)d;
            bar.OnTicksChanged();
        }

        private static void OnTicksChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bar = (Bar)d;
            var oldTicks = e.OldValue as DoubleCollection;
            if (oldTicks != null)
            {
                oldTicks.Changed -= bar.OnTickCollectionChanged;
            }
            var newTicks = e.NewValue as DoubleCollection;
            if (newTicks != null)
            {
                newTicks.Changed += bar.OnTickCollectionChanged;
            }
            bar.OnTicksChanged();
        }

        private void OnTickCollectionChanged(object sender, EventArgs eventArgs)
        {
            this.OnTicksChanged();
        }

        private static void OnTickFrequencyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var bar = (Bar)d;
            bar.OnTicksChanged();
        }
    }
}