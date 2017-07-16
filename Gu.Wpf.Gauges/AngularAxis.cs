namespace Gu.Wpf.Gauges
{
    using System.Windows;

    public class AngularAxis : Axis
    {
        public static readonly DependencyProperty MinAngleProperty = AngularGauge.MinAngleProperty.AddOwner(
            typeof(AngularAxis),
            new FrameworkPropertyMetadata(
                -180.0d,
                FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty MaxAngleProperty = AngularGauge.MaxAngleProperty.AddOwner(
            typeof(AngularAxis),
            new FrameworkPropertyMetadata(
                0.0d,
                FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty TextOrientationProperty = AngularGauge.TextOrientationProperty.AddOwner(
            typeof(AngularAxis),
            new FrameworkPropertyMetadata(
                TextOrientation.Tangential,
                FrameworkPropertyMetadataOptions.Inherits));

        static AngularAxis()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AngularAxis), new FrameworkPropertyMetadata(typeof(AngularAxis)));
        }

        /// <summary>
        /// Gets or sets the <see cref="P:AngularAxis.MinAngle" />
        /// The default is -180
        /// </summary>
        public double MinAngle
        {
            get => (double)this.GetValue(MinAngleProperty);
            set => this.SetValue(MinAngleProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="P:AngularAxis.MaxAngle" />
        /// The default is 0
        /// </summary>
        public double MaxAngle
        {
            get => (double)this.GetValue(MaxAngleProperty);
            set => this.SetValue(MaxAngleProperty, value);
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
    }
}