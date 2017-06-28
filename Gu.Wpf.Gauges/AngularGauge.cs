namespace Gu.Wpf.Gauges
{
    using System.Windows;

    public partial class AngularGauge : Gauge
    {
        static AngularGauge()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AngularGauge), new FrameworkPropertyMetadata(typeof(AngularGauge)));
        }

        /// <summary>
        ///     This method is invoked when the <see cref="MinAngle"/> property changes.
        /// </summary>
        /// <param name="oldMinAngle">The old value of the <see cref="MinAngle"/> property.</param>
        /// <param name="newMinAngle">The new value of the <see cref="MinAngle"/> property.</param>
        protected virtual void OnMinAngleChanged(double oldMinAngle, double newMinAngle)
        {
        }

        /// <summary>
        ///     This method is invoked when the <see cref="MaxAngle"/> property changes.
        /// </summary>
        /// <param name="oldMaxAngle">The old value of the <see cref="MaxAngle"/> property.</param>
        /// <param name="newMaxAngle">The new value of the <see cref="MaxAngle"/> property.</param>
        protected virtual void OnMaxAngleChanged(double oldMaxAngle, double newMaxAngle)
        {
        }
    }
}
