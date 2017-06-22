// ReSharper disable StaticMemberInGenericType
namespace Gu.Wpf.Gauges
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    using Gu.Wpf.Gauges.Helpers;

    public abstract class Indicators<TGauge> : Control
        where TGauge : Gauge
    {
        protected override void OnInitialized(EventArgs e)
        {
            var gauge = this.VisualAncestors()
                 .OfType<TGauge>()
                 .FirstOrDefault();
            this.OnGaugeChanged(gauge);
            base.OnInitialized(e);
        }

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