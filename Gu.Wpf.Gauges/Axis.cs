namespace Gu.Wpf.Gauges
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    public class Axis : Control, ISupportInitialize
    {
        /// <summary>
        /// Identifies the <see cref="P:Axis.Minimum" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:Axis.Minimum" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty MinimumProperty = Gauge.MinimumProperty.AddOwner(
            typeof(Axis),
            new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:Axis.Maximum" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:Axis.Maximum" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty MaximumProperty = Gauge.MaximumProperty.AddOwner(
            typeof(Axis),
            new FrameworkPropertyMetadata(1.0, FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty ShowLabelsProperty = DependencyProperty.Register(
            nameof(ShowLabels),
            typeof(bool),
            typeof(Axis),
            new PropertyMetadata(true));

        public static readonly DependencyProperty MajorTickFrequencyProperty = Gauge.MajorTickFrequencyProperty.AddOwner(
            typeof(Axis),
            new FrameworkPropertyMetadata(default(double)));

        public static readonly DependencyProperty MajorTicksProperty = Gauge.MajorTicksProperty.AddOwner(
            typeof(Axis),
            new FrameworkPropertyMetadata(default(DoubleCollection)));

        public static readonly DependencyProperty MinorTickFrequencyProperty = Gauge.MinorTickFrequencyProperty.AddOwner(
            typeof(Axis),
            new FrameworkPropertyMetadata(default(double)));

        public static readonly DependencyProperty MinorTicksProperty = Gauge.MinorTicksProperty.AddOwner(
            typeof(Axis),
            new FrameworkPropertyMetadata(default(DoubleCollection)));

        public static readonly DependencyProperty TextOrientationProperty = Gauge.TextOrientationProperty.AddOwner(
            typeof(Axis),
            new FrameworkPropertyMetadata(TextOrientation.Horizontal, FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:Axis.IsDirectionReversed" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsDirectionReversedProperty = Gauge.IsDirectionReversedProperty.AddOwner(
            typeof(Axis),
            new FrameworkPropertyMetadata(
                false,
                FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:Axis.ReservedSpace" /> dependency property. This property is read-only.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:Axis.ReservedSpace" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty ReservedSpaceProperty = TickBar.ReservedSpaceProperty.AddOwner(
            typeof(Axis),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.Inherits,
                null,
                CoerceReservedSpace));

        public static readonly DependencyProperty MinReservedSpaceProperty = DependencyProperty.RegisterAttached(
            "MinReservedSpace",
            typeof(double),
            typeof(Axis),
            new PropertyMetadata(default(double), OnMinReservedSpaceChanged));

        private readonly WeakDictionary<DependencyObject, double> minReservedSpaces = new WeakDictionary<DependencyObject, double>();

        /// <summary>
        /// Gets or sets the <see cref="P:Axis.Minimum" /> possible <see cref="P:Axis.Value" /> of the range element.
        /// </summary>
        /// <returns>
        /// <see cref="P:Axis.Minimum" /> possible <see cref="P:Axis.Value" /> of the range element. The default is 0.
        /// </returns>
        public double Minimum
        {
            get => (double)this.GetValue(MinimumProperty);
            set => this.SetValue(MinimumProperty, value);
        }

        /// <summary>
        /// Gets or sets the highest possible <see cref="P:Axis.Value" /> of the range element.
        /// </summary>
        /// <returns>
        /// The highest possible <see cref="P:Axis.Value" /> of the range element. The default is 1.
        /// </returns>
        public double Maximum
        {
            get => (double)this.GetValue(MaximumProperty);
            set => this.SetValue(MaximumProperty, value);
        }

        /// <summary>
        /// Gets or sets if textlabels should be visible
        /// </summary>
        public bool ShowLabels
        {
            get => (bool)this.GetValue(ShowLabelsProperty);
            set => this.SetValue(ShowLabelsProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="T:Gu.Wpf.Gauges.TextOrientation" />
        /// Default is Horizontal
        /// </summary>
        public TextOrientation TextOrientation
        {
            get => (TextOrientation)this.GetValue(TextOrientationProperty);
            set => this.SetValue(TextOrientationProperty, value);
        }

        /// <summary>
        /// Gets or sets a space buffer for the area that contains the tick marks that are specified for a <see cref="T:Bar" />.
        /// </summary>
        /// <returns>
        /// A value that represents the total buffer area on either side of the row or column of tick marks. The default value is zero (0.0).
        /// </returns>
        public double ReservedSpace
        {
            get => (double)this.GetValue(ReservedSpaceProperty);
            set => this.SetValue(ReservedSpaceProperty, value);
        }

        public double MajorTickFrequency
        {
            get => (double)this.GetValue(MajorTickFrequencyProperty);
            set => this.SetValue(MajorTickFrequencyProperty, value);
        }

        public DoubleCollection MajorTicks
        {
            get => (DoubleCollection)this.GetValue(MajorTicksProperty);
            set => this.SetValue(MajorTicksProperty, value);
        }

        public double MinorTickFrequency
        {
            get => (double)this.GetValue(MinorTickFrequencyProperty);
            set => this.SetValue(MinorTickFrequencyProperty, value);
        }

        public DoubleCollection MinorTicks
        {
            get => (DoubleCollection)this.GetValue(MinorTicksProperty);
            set => this.SetValue(MinorTicksProperty, value);
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

        public static void SetMinReservedSpace(DependencyObject element, double value)
        {
            element.SetValue(MinReservedSpaceProperty, value);
        }

        public static double GetMinReservedSpace(DependencyObject element)
        {
            return (double)element.GetValue(MinReservedSpaceProperty);
        }

        private static void OnMinReservedSpaceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var axis = d.VisualAncestors().OfType<Axis>().FirstOrDefault();
            if (axis != null)
            {
                axis.minReservedSpaces.AddOrUpdate(d, (double)e.NewValue);
                axis.SetCurrentValue(ReservedSpaceProperty, axis.minReservedSpaces.Max(x => x.Value));
            }
        }

        private static object CoerceReservedSpace(DependencyObject d, object basevalue)
        {
            var axis = (Axis)d;
            var min = axis.minReservedSpaces.Any() ? axis.minReservedSpaces.Min(x => x.Value) : 0;
            var value = (double)basevalue;
            return Math.Max(value, min);
        }
    }
}