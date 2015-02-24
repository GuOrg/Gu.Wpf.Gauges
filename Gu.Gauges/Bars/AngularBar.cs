namespace Gu.Gauges
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    public class AngularBar : FrameworkElement
    {
        private static readonly DependencyPropertyKey DiameterPropertyKey = DependencyProperty.RegisterReadOnly(
                "Diameter",
                typeof(double),
                typeof(AngularTickBar),
                new PropertyMetadata(default(double)));

        public static readonly DependencyProperty DiameterProperty = DiameterPropertyKey.DependencyProperty;

        public static readonly DependencyProperty MinAngleProperty = DependencyProperty.Register(
            "MinAngle",
            typeof(double),
            typeof(AngularBar),
            new FrameworkPropertyMetadata(
                -180.0,
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty MaxAngleProperty = DependencyProperty.Register(
            "MaxAngle",
            typeof(double),
            typeof(AngularBar),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Identifies the <see cref="P:AngularBar.Minimum" /> dependency property. 
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:AngularBar.Minimum" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty MinimumProperty = RangeBase.MinimumProperty.AddOwner(
            typeof(AngularBar),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Identifies the <see cref="P:AngularBar.Maximum" /> dependency property. 
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:AngularBar.Maximum" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty MaximumProperty = RangeBase.MaximumProperty.AddOwner(
            typeof(AngularBar),
            new FrameworkPropertyMetadata(
                1.0,
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Identifies the <see cref="P:AngularBar.ReservedSpace" /> dependency property. This property is read-only.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:AngularBar.ReservedSpace" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty ReservedSpaceProperty = TickBar.ReservedSpaceProperty.AddOwner(
            typeof(AngularBar),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Identifies the <see cref="P:AngularBar.TickFrequency" /> dependency property. 
        /// </summary>
        public static readonly DependencyProperty TickFrequencyProperty = Slider.TickFrequencyProperty.AddOwner(
            typeof(AngularBar),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Identifies the <see cref="P:AngularBar.Ticks" /> dependency property. 
        /// </summary>
        public static readonly DependencyProperty TicksProperty = Slider.TicksProperty.AddOwner(
            typeof(AngularBar),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Identifies the <see cref="P:LinearTickBar.IsDirectionReversed" /> dependency property. 
        /// </summary>
        public static readonly DependencyProperty IsDirectionReversedProperty = Slider.IsDirectionReversedProperty.AddOwner(
            typeof(AngularBar),
            new FrameworkPropertyMetadata(
                false,
                FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Gets or sets the effective diameter ie ActualWidth - ReservedSpace
        /// The default is -180
        /// </summary>
        public double Diameter
        {
            get { return (double)this.GetValue(DiameterProperty); }
            protected set { this.SetValue(DiameterPropertyKey, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="P:AngularBar.MinAngle" />
        /// The default is -180
        /// </summary>
        public double MinAngle
        {
            get { return (double)this.GetValue(MinAngleProperty); }
            set { this.SetValue(MinAngleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="P:AngularBar.MaxAngle" />
        /// The default is 0
        /// </summary>
        public double MaxAngle
        {
            get { return (double)this.GetValue(MaxAngleProperty); }
            set { this.SetValue(MaxAngleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="P:AngularBar.Minimum" />
        /// The default is 0
        /// </summary>
        public double Minimum
        {
            get { return (double)this.GetValue(MinimumProperty); }
            set { this.SetValue(MinimumProperty, value); }
        }

        /// <summary>
        /// Gets or sets the highest possible <see cref="P:AngularBar.Value" /> of the range element.  
        /// </summary>
        /// <returns>
        /// The highest possible <see cref="P:AngularBar.Value" /> of the range element. The default is 1.
        /// </returns>
        public double Maximum
        {
            get { return (double)this.GetValue(MaximumProperty); }
            set { this.SetValue(MaximumProperty, value); }
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
        /// Gets or sets a space buffer for the area that contains the tick marks that are specified for a <see cref="T:AngularBar" />.  
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
        /// Gets or sets the positions of the tick marks to display for a <see cref="T:AngularBar" />. 
        /// </summary>
        /// <returns>
        /// A set of tick marks to display for a <see cref="T:AngularBar" />. The default is null.
        /// </returns>
        public DoubleCollection Ticks
        {
            get { return (DoubleCollection)this.GetValue(TicksProperty); }
            set { this.SetValue(TicksProperty, value); }
        }
    }
}