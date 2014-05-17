namespace GaugeBox
{
    using System;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    [TemplatePart(Name = IndicatorTemplateName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = TrackTemplateName, Type = typeof(FrameworkElement))]
    public class Gauge : RangeBase
    {
        private FrameworkElement _indicator;
        private FrameworkElement _track;
        private readonly TranslateTransform _indicatorTransform = new TranslateTransform();
        private const string IndicatorTemplateName = "PART_Indicator";
        private const string TrackTemplateName = "PART_Track";

        public Gauge()
        {

        }

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
            }
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
            if (this._indicator == null)
                return;
            double minimum = this.Minimum;
            double maximum = this.Maximum;
            double num = this.Value;
            double d = Math.Abs(num - minimum) / Math.Abs(maximum - minimum);
            _indicator.Width = d * 10;
            //_indicatorTransform.SetCurrentValue(TranslateTransform.XProperty, d);
        }
    }
}
