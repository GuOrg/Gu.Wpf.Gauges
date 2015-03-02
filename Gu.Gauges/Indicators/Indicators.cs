// ReSharper disable StaticMemberInGenericType
namespace Gu.Gauges
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    using Gu.Gauges.Helpers;

    public abstract  class Indicators<TGauge> : Control
        where TGauge : IGauge
    {
        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            var gauge = this.VisualAncestors()
                             .OfType<TGauge>()
                             .FirstOrDefault();
            this.OnGaugeChanged(gauge);
            base.OnVisualParentChanged(oldParent);
        }

        protected abstract void OnGaugeChanged(TGauge newValue);
    }
}