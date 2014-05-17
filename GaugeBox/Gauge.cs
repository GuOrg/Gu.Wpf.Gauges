namespace GaugeBox
{
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    [TemplatePart(Name = IndicatorTemplateName, Type = typeof(FrameworkElement))]
    public class Gauge : RangeBase
    {
        private FrameworkElement _indicator;
        private TranslateTransform _indicatorTransform;
        private const string IndicatorTemplateName = "PART_Indicator";

        public Gauge()
        {

        }

        /// <summary>
        /// Called when a template is applied to a <see cref="T:System.Windows.Controls.ProgressBar"/>.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this._indicator = this.GetTemplateChild(IndicatorTemplateName) as FrameworkElement;
            if (_indicator != null)
            {
                //if(_indicator.RenderTransform.a)
            }
        }

        private void SetIndicatorPos()
        {
            if (this._indicator == null)
                return;
            double minimum = this.Minimum;
            double maximum = this.Maximum;
            double num = this.Value;
            //this._indicator.RenderTransform =  (num - minimum) / (maximum - minimum);
        }
    }
}
