namespace Gu.Wpf.Gauges
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;

    public class AngularRange : AngularIndicator
    {
        static AngularRange()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(AngularRange),
                new FrameworkPropertyMetadata(typeof(AngularRange)));
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        protected virtual void OnEndChanged(double oldValue, double newValue)
        {
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        protected virtual void OnStartChanged(double oldValue, double newValue)
        {
        }
    }
}