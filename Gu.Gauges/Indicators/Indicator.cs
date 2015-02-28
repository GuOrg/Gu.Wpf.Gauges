namespace Gu.Gauges.Indicators
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    public class Indicator : FrameworkElement
    {
        /// <summary>
        /// Identifies the <see cref="P:Indicator.Value" /> dependency property. 
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:Indicator.Value" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty ValueProperty = RangeBase.ValueProperty.AddOwner(
            typeof(Indicator),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:Indicator.Minimum" /> dependency property. 
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:Indicator.Minimum" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty MinimumProperty = RangeBase.MinimumProperty.AddOwner(
            typeof(Indicator),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:Indicator.Maximum" /> dependency property. 
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:Indicator.Maximum" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty MaximumProperty = RangeBase.MaximumProperty.AddOwner(
            typeof(Indicator),
            new FrameworkPropertyMetadata(
                1.0,
                FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:BlockIndicator.Placement" /> dependency property. This property is read-only.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:BlockIndicator.Placement" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty PlacementProperty = Bar.PlacementProperty.AddOwner(
            typeof(Indicator),
            new FrameworkPropertyMetadata(
                TickBarPlacement.Bottom,
                FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:Indicator.ReservedSpace" /> dependency property. This property is read-only.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:Indicator.ReservedSpace" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty ReservedSpaceProperty = TickBar.ReservedSpaceProperty.AddOwner(
            typeof(Indicator),
            new FrameworkPropertyMetadata(
                0.0));

        /// <summary>
        /// Identifies the <see cref="P:Indicator.IsDirectionReversed" /> dependency property. 
        /// </summary>
        public static readonly DependencyProperty IsDirectionReversedProperty = Slider.IsDirectionReversedProperty.AddOwner(
            typeof(Indicator),
            new FrameworkPropertyMetadata(
                false,
                FrameworkPropertyMetadataOptions.Inherits));


        /// <summary>
        /// Gets or sets the highest possible <see cref="P:Indicator.Value" /> of the range element.  
        /// </summary>
        /// <returns>
        /// The highest possible <see cref="P:Indicator.Value" /> of the range element. The default is 1.
        /// </returns>
        public double Value
        {
            get { return (double)this.GetValue(ValueProperty); }
            set { this.SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="P:Indicator.Minimum" />
        /// The default is 0
        /// </summary>
        public double Minimum
        {
            get { return (double)this.GetValue(MinimumProperty); }
            set { this.SetValue(MinimumProperty, value); }
        }

        /// <summary>
        /// Gets or sets the highest possible <see cref="P:Indicator.Maximum" /> of the range element.  
        /// </summary>
        /// <returns>
        /// The highest possible <see cref="P:Indicator.Maximum" /> of the range element. The default is 1.
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
        /// A <see cref="T:TickBarPlacement" /> enumeration value that identifies the position of the <see cref="T:Indicator" /> in the <see cref="T:System.Windows.Style" /> layout of a <see cref="T:System.Windows.Controls.Slider" />. The default value is <see cref="F:TickBarPlacement.Top" />.
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
        /// Gets or sets a space buffer for the area that contains the tick marks that are specified for a <see cref="T:Indicator" />.  
        /// </summary>
        /// <returns>
        /// A value that represents the total buffer area on either side of the row or column of tick marks. The default value is zero (0.0).
        /// </returns>
        public double ReservedSpace
        {
            get { return (double)this.GetValue(ReservedSpaceProperty); }
            set { this.SetValue(ReservedSpaceProperty, value); }
        }
    }
}
