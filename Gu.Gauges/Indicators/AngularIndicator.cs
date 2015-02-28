namespace Gu.Gauges.Indicators
{
    using System.Windows;

    public class AngularIndicator : Indicator
    {
        public static readonly DependencyProperty MinAngleProperty = AngularBar.MinAngleProperty.AddOwner(
            typeof(AngularIndicator),
            new FrameworkPropertyMetadata(
                -180.0,
                FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty MaxAngleProperty = AngularBar.MaxAngleProperty.AddOwner(
                typeof(AngularIndicator),
                new FrameworkPropertyMetadata(
                    0.0,
                    FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Gets or sets the <see cref="P:AngularIndicator.MinAngle" />
        /// The default is -180
        /// </summary>
        public double MinAngle
        {
            get
            {
                return (double)this.GetValue(MinAngleProperty);
            }
            set
            {
                this.SetValue(MinAngleProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="P:AngularIndicator.MaxAngle" />
        /// The default is 0
        /// </summary>
        public double MaxAngle
        {
            get
            {
                return (double)this.GetValue(MaxAngleProperty);
            }
            set
            {
                this.SetValue(MaxAngleProperty, value);
            }
        }
    }
}