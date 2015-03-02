namespace Gu.Gauges
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    using Gu.Gauges.Helpers;

    public class Axis : Control
    {
        /// <summary>
        /// Identifies the <see cref="P:Axis.Minimum" /> dependency property. 
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:Axis.Minimum" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty MinimumProperty = RangeBase.MinimumProperty.AddOwner(
            typeof(Axis),
            new PropertyMetadata(0.0));

        /// <summary>
        /// Identifies the <see cref="P:Axis.Maximum" /> dependency property. 
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:Axis.Maximum" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty MaximumProperty = RangeBase.MaximumProperty.AddOwner(
            typeof(Axis),
            new PropertyMetadata(1.0));

        public static readonly DependencyProperty ShowLabelsProperty = DependencyProperty.Register(
            "ShowLabels",
            typeof(bool),
            typeof(Axis),
            new PropertyMetadata(true));

        public static readonly DependencyProperty MajorTickFrequencyProperty = DependencyProperty.Register(
            "MajorTickFrequency",
            typeof(double),
            typeof(Axis),
            new FrameworkPropertyMetadata(default(double)));

        public static readonly DependencyProperty MajorTicksProperty = DependencyProperty.Register(
            "MajorTicks",
            typeof(DoubleCollection),
            typeof(Axis),
            new PropertyMetadata(default(DoubleCollection)));

        public static readonly DependencyProperty MinorTickFrequencyProperty = DependencyProperty.Register(
            "MinorTickFrequency",
            typeof(double),
            typeof(Axis),
            new PropertyMetadata(default(double)));

        public static readonly DependencyProperty TextOrientationProperty = DependencyProperty.Register(
            "TextOrientation",
            typeof(TextOrientation),
            typeof(Axis),
            new FrameworkPropertyMetadata(TextOrientation.Horizontal, FrameworkPropertyMetadataOptions.Inherits));

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
                FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty MinReservedSpaceProperty = DependencyProperty.RegisterAttached(
            "MinReservedSpace", 
            typeof (double),
            typeof (Axis), 
            new PropertyMetadata(default(double), OnMinReservedSpaceChanged));

        /// <summary>
        /// Identifies the <see cref="P:Axis.IsDirectionReversed" /> dependency property. 
        /// </summary>
        public static readonly DependencyProperty IsDirectionReversedProperty = Slider.IsDirectionReversedProperty.AddOwner(
            typeof(Axis),
            new FrameworkPropertyMetadata(
                false,
                FrameworkPropertyMetadataOptions.Inherits));

        private readonly WeakDictionary<DependencyObject,double> minReservedSpaces = new WeakDictionary<DependencyObject, double>(); 

        /// <summary>
        /// Gets or sets the <see cref="P:Axis.Minimum" /> possible <see cref="P:Axis.Value" /> of the range element.  
        /// </summary>
        /// <returns>
        /// <see cref="P:Axis.Minimum" /> possible <see cref="P:Axis.Value" /> of the range element. The default is 0.
        /// </returns>
        public double Minimum
        {
            get { return (double) this.GetValue(MinimumProperty); }
            set { this.SetValue(MinimumProperty, value); }
        }

        /// <summary>
        /// Gets or sets the highest possible <see cref="P:Axis.Value" /> of the range element.  
        /// </summary>
        /// <returns>
        /// The highest possible <see cref="P:Axis.Value" /> of the range element. The default is 1.
        /// </returns>
        public double Maximum
        {
            get { return (double) this.GetValue(MaximumProperty); }
            set { this.SetValue(MaximumProperty, value); }
        }

        /// <summary>
        /// Gets or sets if textlabels should be visible
        /// </summary>
        public bool ShowLabels
        {
            get { return (bool)this.GetValue(ShowLabelsProperty); }
            set { this.SetValue(ShowLabelsProperty, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="T:Gu.Gauges.TextOrientation" />
        /// Default is Horizontal
        /// </summary>
        public TextOrientation TextOrientation
        {
            get { return (TextOrientation)this.GetValue(TextOrientationProperty); }
            set { this.SetValue(TextOrientationProperty, value); }
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

        public double MajorTickFrequency
        {
            get { return (double)this.GetValue(MajorTickFrequencyProperty); }
            set { this.SetValue(MajorTickFrequencyProperty, value); }
        }

        public DoubleCollection MajorTicks
        {
            get { return (DoubleCollection)this.GetValue(MajorTicksProperty); }
            set { this.SetValue(MajorTicksProperty, value); }
        }

        public double MinorTickFrequency
        {
            get { return (double)this.GetValue(MinorTickFrequencyProperty); }
            set { this.SetValue(MinorTickFrequencyProperty, value); }
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
                axis.minReservedSpaces.AddOrUpdate(d,(double) e.NewValue);
                axis.ReservedSpace = axis.minReservedSpaces.Max(x => x.Value);
            }
        }
    }
}