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

        public static readonly DependencyProperty TickGapProperty = AngularGauge.TickGapProperty.AddOwner(
            typeof(AngularAxis),
            new FrameworkPropertyMetadata(
                0.0d,
                FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty TickLengthProperty = AngularGauge.TickLengthProperty.AddOwner(
            typeof(AngularAxis),
            new FrameworkPropertyMetadata(
                0.0d,
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
        /// Gets or sets the <see cref="P:AngularAxis.TickGap" />
        /// </summary>
        public double TickGap
        {
            get => (double)this.GetValue(TickGapProperty);
            set => this.SetValue(TickGapProperty, value);
        }

        /// <summary>
        /// Gets or sets the <see cref="P:AngularAxis.TickLength" />
        /// </summary>
        public double TickLength
        {
            get => (double)this.GetValue(TickLengthProperty);
            set => this.SetValue(TickLengthProperty, value);
        }
    }
}