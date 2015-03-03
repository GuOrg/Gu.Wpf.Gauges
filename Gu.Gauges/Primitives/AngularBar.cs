namespace Gu.Gauges
{
    using System.Windows;

    public class AngularBar : TickBarBase
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
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty MaxAngleProperty = DependencyProperty.Register(
            "MaxAngle",
            typeof(double),
            typeof(AngularBar),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

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
    }
}