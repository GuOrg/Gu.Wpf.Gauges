namespace Gauges
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    [TemplatePart(Name = IndicatorTemplateName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = TrackTemplateName, Type = typeof(FrameworkElement))]
    public class Gauge : RangeBase
    {
        private const string IndicatorTemplateName = "PART_Indicator";
        private const string TrackTemplateName = "PART_Track";
        private readonly TranslateTransform _indicatorTransform = new TranslateTransform();
        private FrameworkElement _indicator;
        private FrameworkElement _track;
        static Gauge()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Gauge), new FrameworkPropertyMetadata(typeof(Gauge)));
        }
        public Gauge()
        {
            Lables = new ObservableCollection<double>();
        }

        public static readonly DependencyProperty MarkerProperty = DependencyProperty.Register(
            "Marker", typeof (Marker), typeof (Gauge), new PropertyMetadata(default(Marker), OnMarkerChanged));

        public Marker Marker
        {
            get { return (Marker) GetValue(MarkerProperty); }
            set { SetValue(MarkerProperty, value); }
        }

        public ObservableCollection<double> Lables { get; private set; }

        /// <summary>
        /// Called when a template is applied to a <see cref="T:System.Windows.Controls.ProgressBar"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            if (Template == null)
            {
                return;
            }
            base.OnApplyTemplate();
            this._indicator = this.GetTemplateChild(IndicatorTemplateName) as FrameworkElement;
            this._track = this.GetTemplateChild(TrackTemplateName) as FrameworkElement;
            if (_indicator != null)
            {
                _indicator.RenderTransform = _indicatorTransform;
                _indicator.HorizontalAlignment = HorizontalAlignment.Center;
            }
        }

        private static void OnMarkerChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the current position of the <see cref="T:System.Windows.Controls.ProgressBar"/> when the <see cref="P:System.Windows.Controls.Primitives.RangeBase.Minimum"/> property changes.
        /// </summary>
        /// <param name="oldMinimum">Old value of the <see cref="P:System.Windows.Controls.Primitives.RangeBase.Minimum"/> property.</param><param name="newMinimum">New value of the <see cref="P:System.Windows.Controls.Primitives.RangeBase.Minimum"/> property.</param>
        protected override void OnMinimumChanged(double oldMinimum, double newMinimum)
        {
            base.OnMinimumChanged(oldMinimum, newMinimum);
            this.SetIndicatorPos();
        }

        /// <summary>
        /// Updates the current position of the <see cref="T:System.Windows.Controls.ProgressBar"/> when the <see cref="P:System.Windows.Controls.Primitives.RangeBase.Maximum"/> property changes.
        /// </summary>
        /// <param name="oldMaximum">Old value of the <see cref="P:System.Windows.Controls.Primitives.RangeBase.Maximum"/> property.</param><param name="newMaximum">New value of the <see cref="P:System.Windows.Controls.Primitives.RangeBase.Maximum"/> property.</param>
        protected override void OnMaximumChanged(double oldMaximum, double newMaximum)
        {
            base.OnMaximumChanged(oldMaximum, newMaximum);
            this.SetIndicatorPos();
        }

        /// <summary>
        /// Updates the current position of the <see cref="T:System.Windows.Controls.ProgressBar"/> when the <see cref="P:System.Windows.Controls.Primitives.RangeBase.Value"/> property changes.
        /// </summary>
        /// <param name="oldValue">Old value of the <see cref="P:System.Windows.Controls.Primitives.RangeBase.Value"/> property.</param><param name="newValue">New value of the <see cref="P:System.Windows.Controls.Primitives.RangeBase.Value"/> property.</param>
        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            this.SetIndicatorPos();
        }

        private void SetIndicatorPos()
        {
            if (this._indicator == null || _track == null)
                return;
            double x = PosFromValue(this.Value);
            _indicatorTransform.SetCurrentValue(TranslateTransform.XProperty, _track.ActualWidth * (x - 0.5));
        }

        private double PosFromValue(double value)
        {
            double minimum = this.Minimum;
            double maximum = this.Maximum;
            double num = this.Value;
            return (value - minimum) / Math.Abs(maximum - minimum);
        }
    }
}
