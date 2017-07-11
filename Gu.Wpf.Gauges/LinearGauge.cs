namespace Gu.Wpf.Gauges
{
    using System.Windows;

    public partial class LinearGauge : Gauge
    {
#pragma warning disable SA1202 // Elements must be ordered by access
        private static readonly DependencyPropertyKey ContentOverflowPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(ContentOverflow),
            typeof(Thickness),
            typeof(LinearGauge),
            new FrameworkPropertyMetadata(
                default(Thickness),
                FrameworkPropertyMetadataOptions.AffectsArrange));

        public static readonly DependencyProperty ContentOverflowProperty = ContentOverflowPropertyKey.DependencyProperty;
#pragma warning restore SA1202 // Elements must be ordered by access

        private Thickness contentOverflow = default(Thickness);

        static LinearGauge()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinearGauge), new FrameworkPropertyMetadata(typeof(LinearGauge)));
        }

        /// <summary>
        /// The aggregate overflow of all children
        /// </summary>
        public Thickness ContentOverflow
        {
            get => (Thickness)this.GetValue(ContentOverflowProperty);
            protected set => this.SetValue(ContentOverflowPropertyKey, value);
        }

        public void RegisterOverflow(Thickness overflow)
        {
            this.contentOverflow = this.contentOverflow.Union(overflow);
        }

        protected override Size MeasureOverride(Size constraint)
        {
            this.contentOverflow = default(Thickness);
            return base.MeasureOverride(constraint);
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            var size = base.ArrangeOverride(arrangeBounds);
            this.ContentOverflow = this.contentOverflow;
            return size;
        }
    }
}