namespace Gu.Wpf.Gauges
{
    using System.Windows;

    public class AngularBar : TickBarBase
    {
#pragma warning disable SA1202 // Elements must be ordered by access
        private static readonly DependencyPropertyKey DiameterPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(Diameter),
            typeof(double),
            typeof(AngularBar),
            new PropertyMetadata(default(double)));

        public static readonly DependencyProperty DiameterProperty = DiameterPropertyKey.DependencyProperty;

        public static readonly DependencyProperty StartProperty = AngularGauge.StartProperty.AddOwner(
            typeof(AngularBar),
            new FrameworkPropertyMetadata(
                Defaults.StartAngle,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty EndProperty = AngularGauge.EndProperty.AddOwner(
            typeof(AngularBar),
            new FrameworkPropertyMetadata(
                Defaults.EndAngle,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));
#pragma warning restore SA1202 // Elements must be ordered by access

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
        public double Start
        {
            get => (double)this.GetValue(StartProperty);
            set => this.SetValue(StartProperty, value);
        }

        /// <summary>
        /// Gets or sets the end angle of the arc.
        /// Degrees clockwise from the y axis.
        /// The default is 140
        /// </summary>
        public double End
        {
            get => (double)this.GetValue(EndProperty);
            set => this.SetValue(EndProperty, value);
        }
    }
}