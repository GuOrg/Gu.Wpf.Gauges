namespace Gu.Wpf.Gauges
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;

    public class Indicator : ContentControl
    {
        /// <summary>
        /// Identifies the <see cref="P:Bar.Minimum" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:Bar.Minimum" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty MinimumProperty = Gauge.MinimumProperty.AddOwner(
            typeof(Indicator),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:Bar.Maximum" /> dependency property.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:Bar.Maximum" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty MaximumProperty = Gauge.MaximumProperty.AddOwner(
            typeof(Indicator),
            new FrameworkPropertyMetadata(
                1.0,
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:Bar.IsDirectionReversed" /> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsDirectionReversedProperty = Gauge.IsDirectionReversedProperty.AddOwner(
            typeof(Indicator),
            new FrameworkPropertyMetadata(
                false,
                FrameworkPropertyMetadataOptions.Inherits));

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

        protected UIElement VisualChild => (UIElement)this.GetVisualChild(0);
    }
}