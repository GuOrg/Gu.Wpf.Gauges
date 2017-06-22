namespace Gu.Gauges
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Markup;
    using System.Windows.Media;

    using Gu.Gauges.Helpers;

    [ContentProperty("Items")]
    public class LinearIndicators : FrameworkElement
    {
        private static readonly DependencyPropertyKey GaugePropertyKey = DependencyProperty.RegisterReadOnly(
nameof(Gauge),
            typeof(LinearGauge),
            typeof(LinearIndicators),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty GaugeProperty = GaugePropertyKey.DependencyProperty;
        private readonly LinearPanel panel;

        static LinearIndicators()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinearIndicators), new FrameworkPropertyMetadata(typeof(LinearIndicators)));
        }

        public LinearIndicators()
        {
            this.panel = new LinearPanel();
            this.AddVisualChild(this.panel);
            this.AddLogicalChild(this.panel);
        }

        public UIElementCollection Items => this.panel.Children;

        public LinearGauge Gauge
        {
            get => (LinearGauge)this.GetValue(GaugeProperty);
            internal set => this.SetValue(GaugePropertyKey, value);
        }

        protected override int VisualChildrenCount => 1;

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            BindingHelper.BindOneWay(this, () => this.Gauge.Axis.Minimum, this.panel, LinearPanel.MinimumProperty);
            BindingHelper.BindOneWay(this, () => this.Gauge.Axis.Maximum, this.panel, LinearPanel.MaximumProperty);
            BindingHelper.BindOneWay(this, () => this.Gauge.Axis.IsDirectionReversed, this.panel, LinearPanel.IsDirectionReversedProperty);
            BindingHelper.BindOneWay(this, () => this.Gauge.Axis.Placement, this.panel, LinearPanel.PlacementProperty);
            BindingHelper.BindOneWay(this, () => this.Gauge.Axis.ReservedSpace, this.panel, LinearPanel.ReservedSpaceProperty);
        }

        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            var gauge = this.VisualAncestors()
                .OfType<LinearGauge>()
                .FirstOrDefault();
            this.OnGaugeChanged(gauge);
            base.OnVisualParentChanged(oldParent);
            base.OnVisualParentChanged(oldParent);
        }

        protected override Visual GetVisualChild(int index)
        {
            return index == 0 ? this.panel : null;
        }

        /// <summary>
        ///     Default control measurement is to measure only the first visual child.
        ///     This child would have been created by the inflation of the
        ///     visual tree from the control's style.
        ///
        ///     Derived controls may want to override this behavior.
        /// </summary>
        /// <param name="constraint">The measurement constraints.</param>
        /// <returns>The desired size of the control.</returns>
        protected override Size MeasureOverride(Size constraint)
        {
            this.panel.Measure(constraint);
            return this.panel.DesiredSize;
        }

        /// <summary>
        ///     Default control arrangement is to only arrange
        ///     the first visual child. No transforms will be applied.
        /// </summary>
        /// <param name="arrangeBounds">The computed size.</param>
        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            this.panel.Arrange(new Rect(arrangeBounds));
            return arrangeBounds;
        }

        private void OnGaugeChanged(LinearGauge newGauge)
        {
            BindingOperations.ClearAllBindings(this.panel);
            this.Gauge = newGauge;
        }
    }
}
