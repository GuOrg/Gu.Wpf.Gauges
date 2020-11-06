namespace Gu.Wpf.Gauges
{
    using System.Windows;

    public class AngularBar : TickBarBase
    {
        private static readonly DependencyPropertyKey DiameterPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(Diameter),
            typeof(double),
            typeof(AngularBar),
            new PropertyMetadata(default(double)));

        public static readonly DependencyProperty DiameterProperty = DiameterPropertyKey.DependencyProperty;

        public static readonly DependencyProperty StartProperty = AngularGauge.StartProperty.AddOwner(
            typeof(AngularBar),
            new FrameworkPropertyMetadata(
                Angle.DefaultStart,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty EndProperty = AngularGauge.EndProperty.AddOwner(
            typeof(AngularBar),
            new FrameworkPropertyMetadata(
                Angle.DefaultEnd,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Gets or sets the effective diameter ie ActualWidth - ReservedSpace
        /// The default is -180
        /// </summary>
        public double Diameter
        {
            get => (double)this.GetValue(DiameterProperty);
            protected set => this.SetValue(DiameterPropertyKey, value);
        }

        /// <summary>
        /// Gets or sets the start angle of the arc.
        /// Degrees clockwise from the y axis.
        /// The default is -140
        /// </summary>
        public Angle Start
        {
            get => (Angle)this.GetValue(StartProperty);
            set => this.SetValue(StartProperty, value);
        }

        /// <summary>
        /// Gets or sets the end angle of the arc.
        /// Degrees clockwise from the y axis.
        /// The default is 140
        /// </summary>
        public Angle End
        {
            get => (Angle)this.GetValue(EndProperty);
            set => this.SetValue(EndProperty, value);
        }
    }
}
