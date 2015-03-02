namespace Gu.Gauges
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    public class AngularPanel : Panel
    {
        /// <summary>
        /// Identifies the <see cref="P:AngularPanel.Minimum" /> dependency property. 
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:AngularPanel.Minimum" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty MinimumProperty = RangeBase.MinimumProperty.AddOwner(
            typeof(AngularPanel),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:AngularPanel.Maximum" /> dependency property. 
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:AngularPanel.Maximum" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty MaximumProperty = RangeBase.MaximumProperty.AddOwner(
            typeof(AngularPanel),
            new FrameworkPropertyMetadata(
                1.0,
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:AngularPanel.IsDirectionReversed" /> dependency property. 
        /// </summary>
        public static readonly DependencyProperty IsDirectionReversedProperty = Slider.IsDirectionReversedProperty.AddOwner(
            typeof(AngularPanel),
            new FrameworkPropertyMetadata(
                false,
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:AngularPanel.ReservedSpace" /> dependency property. This property is read-only.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:AngularPanel.ReservedSpace" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty ReservedSpaceProperty = TickBar.ReservedSpaceProperty.AddOwner(
            typeof(AngularPanel),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty StartAngleProperty = DependencyProperty.RegisterAttached(
            "StartAngle",
            typeof(double),
            typeof(AngularPanel),
            new PropertyMetadata(double.NaN, OnPositionChanged));

        public static readonly DependencyProperty EndAngleProperty = DependencyProperty.RegisterAttached(
            "EndAngle",
            typeof(double),
            typeof(AngularPanel),
            new PropertyMetadata(double.NaN, OnPositionChanged));


        public static readonly DependencyProperty AtValueProperty = DependencyProperty.RegisterAttached(
            "AtValue",
            typeof(double),
            typeof(AngularPanel),
            new PropertyMetadata(double.NaN, OnPositionChanged));

        /// <summary>
        /// Gets or sets the <see cref="P:AngularPanel.Minimum" />
        /// The default is 0
        /// </summary>
        public double Minimum
        {
            get { return (double)this.GetValue(MinimumProperty); }
            set { this.SetValue(MinimumProperty, value); }
        }

        /// <summary>
        /// Gets or sets the highest possible <see cref="P:AngularPanel.Maximum" /> of the range element.  
        /// </summary>
        /// <returns>
        /// The highest possible <see cref="P:AngularPanel.Maximum" /> of the range element. The default is 1.
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

        public static void SetStartAngle(DependencyObject element, double value)
        {
            element.SetValue(StartAngleProperty, value);
        }

        public static double GetStartAngle(DependencyObject element)
        {
            return (double)element.GetValue(StartAngleProperty);
        }

        public static void SetEndAngle(DependencyObject element, double value)
        {
            element.SetValue(EndAngleProperty, value);
        }

        public static double GetEndAngle(DependencyObject element)
        {
            return (double)element.GetValue(EndAngleProperty);
        }

        public static void SetAtValue(DependencyObject element, double value)
        {
            element.SetValue(AtValueProperty, value);
        }

        public static double GetAtValue(DependencyObject element)
        {
            return (double)element.GetValue(AtValueProperty);
        }

        private static void OnPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uie = d as UIElement;
            if (uie != null)
            {
                var panel = VisualTreeHelper.GetParent(uie) as AngularPanel;
                if (panel != null)
                {
                    panel.InvalidateArrange();
                }
            }
        }
    }
}