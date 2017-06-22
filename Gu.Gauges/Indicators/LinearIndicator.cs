namespace Gu.Gauges
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Media;

    using Helpers;

    public class LinearIndicator : ContentControl
    {
#pragma warning disable SA1202 // Elements must be ordered by access
        private static readonly DependencyPropertyKey GaugePropertyKey = DependencyProperty.RegisterReadOnly(
nameof(Gauge),
            typeof(LinearGauge),
            typeof(LinearIndicator),
            new PropertyMetadata(null, OnGaugeChanged));

        public static readonly DependencyProperty GaugeProperty = GaugePropertyKey.DependencyProperty;

        private static readonly DependencyProperty PlacementProxyProperty = DependencyProperty.Register(
            "PlacementProxy",
            typeof(TickBarPlacement),
            typeof(LinearIndicator),
            new PropertyMetadata(default(TickBarPlacement), OnPlacementProxyChanged));

        private static readonly DependencyPropertyKey PlacementTransformPropertyKey = DependencyProperty.RegisterReadOnly(
nameof(PlacementTransform),
            typeof(RotateTransform),
            typeof(LinearIndicator),
            new PropertyMetadata(default(RotateTransform)));

        public static readonly DependencyProperty PlacementTransformProperty = PlacementTransformPropertyKey.DependencyProperty;
#pragma warning restore SA1202 // Elements must be ordered by access

        public LinearIndicator()
        {
            this.PlacementTransform = new RotateTransform();
            var property = NameOf.Property(() => this.Gauge.Axis.Placement);
            var placementBinding = new Binding(property)
            {
                Source = this,
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(this, PlacementProxyProperty, placementBinding);
        }

        public LinearGauge Gauge
        {
            get => (LinearGauge)this.GetValue(GaugeProperty);
            protected set => this.SetValue(GaugePropertyKey, value);
        }

        public RotateTransform PlacementTransform
        {
            get => (RotateTransform)this.GetValue(PlacementTransformProperty);
            protected set => this.SetValue(PlacementTransformPropertyKey, value);
        }

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            this.Gauge = this.VisualAncestors().OfType<LinearGauge>().FirstOrDefault();
            base.OnVisualParentChanged(oldParent);
        }

        protected virtual void OnGaugeChanged(LinearGauge old, LinearGauge newValue)
        {
        }

        protected virtual void OnPlacementChanged(TickBarPlacement placement)
        {
            switch (placement)
            {
                case TickBarPlacement.Left:
                    this.PlacementTransform.SetCurrentValue(RotateTransform.AngleProperty, (double)90);
                    break;
                case TickBarPlacement.Top:
                    this.PlacementTransform.SetCurrentValue(RotateTransform.AngleProperty, (double)180);
                    break;
                case TickBarPlacement.Right:
                    this.PlacementTransform.SetCurrentValue(RotateTransform.AngleProperty, (double)-90);
                    break;
                case TickBarPlacement.Bottom:
                    this.PlacementTransform.SetCurrentValue(RotateTransform.AngleProperty, (double)0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static void OnGaugeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LinearIndicator)d).OnGaugeChanged((LinearGauge)e.OldValue, (LinearGauge)e.NewValue);
        }

        private static void OnPlacementProxyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LinearIndicator)d).OnPlacementChanged((TickBarPlacement)e.NewValue);
        }
    }
}
