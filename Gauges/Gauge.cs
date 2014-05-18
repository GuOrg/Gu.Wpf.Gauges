﻿namespace Gauges
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    [TemplatePart(Name = IndicatorTemplateName, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = TrackTemplateName, Type = typeof(FrameworkElement))]
    public class Gauge : RangeBase
    {
        public static readonly DependencyProperty MarkerProperty = DependencyProperty.Register(
            "Marker", typeof(Marker), typeof(Gauge), new PropertyMetadata(default(Marker), OnMarkerChanged));
        
        private const string IndicatorTemplateName = "PART_Indicator";
        private const string TrackTemplateName = "PART_Track";
        private readonly TranslateTransform indicatorTransform = new TranslateTransform();
        private FrameworkElement indicator;
        private FrameworkElement track;

        static Gauge()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Gauge), new FrameworkPropertyMetadata(typeof(Gauge)));
        }

        public Gauge()
        {
            this.Lables = new ObservableCollection<double>();
        }

        public Marker Marker
        {
            get { return (Marker)this.GetValue(MarkerProperty); }
            set { this.SetValue(MarkerProperty, value); }
        }

        public ObservableCollection<double> Lables { get; private set; }

        /// <summary>
        ///     Called when a template is applied to a <see cref="T:System.Windows.Controls.ProgressBar" />.
        /// </summary>
        public override void OnApplyTemplate()
        {
            if (this.Template == null)
            {
                return;
            }

            base.OnApplyTemplate();
            this.indicator = this.GetTemplateChild(IndicatorTemplateName) as FrameworkElement;
            this.track = this.GetTemplateChild(TrackTemplateName) as FrameworkElement;
            if (this.indicator != null)
            {
                this.indicator.RenderTransform = this.indicatorTransform;
                this.indicator.HorizontalAlignment = HorizontalAlignment.Center;
            }
        }

        /// <summary>
        ///     Updates the current position of the <see cref="T:System.Windows.Controls.ProgressBar" /> when the
        ///     <see cref="P:System.Windows.Controls.Primitives.RangeBase.Minimum" /> property changes.
        /// </summary>
        /// <param name="oldMinimum">
        ///     Old value of the <see cref="P:System.Windows.Controls.Primitives.RangeBase.Minimum" />
        ///     property.
        /// </param>
        /// <param name="newMinimum">
        ///     New value of the <see cref="P:System.Windows.Controls.Primitives.RangeBase.Minimum" />
        ///     property.
        /// </param>
        protected override void OnMinimumChanged(double oldMinimum, double newMinimum)
        {
            base.OnMinimumChanged(oldMinimum, newMinimum);
            this.SetIndicatorPos();
        }

        /// <summary>
        ///     Updates the current position of the <see cref="T:System.Windows.Controls.ProgressBar" /> when the
        ///     <see cref="P:System.Windows.Controls.Primitives.RangeBase.Maximum" /> property changes.
        /// </summary>
        /// <param name="oldMaximum">
        ///     Old value of the <see cref="P:System.Windows.Controls.Primitives.RangeBase.Maximum" />
        ///     property.
        /// </param>
        /// <param name="newMaximum">
        ///     New value of the <see cref="P:System.Windows.Controls.Primitives.RangeBase.Maximum" />
        ///     property.
        /// </param>
        protected override void OnMaximumChanged(double oldMaximum, double newMaximum)
        {
            base.OnMaximumChanged(oldMaximum, newMaximum);
            this.SetIndicatorPos();
        }

        /// <summary>
        ///     Updates the current position of the <see cref="T:System.Windows.Controls.ProgressBar" /> when the
        ///     <see cref="P:System.Windows.Controls.Primitives.RangeBase.Value" /> property changes.
        /// </summary>
        /// <param name="oldValue">Old value of the <see cref="P:System.Windows.Controls.Primitives.RangeBase.Value" /> property.</param>
        /// <param name="newValue">New value of the <see cref="P:System.Windows.Controls.Primitives.RangeBase.Value" /> property.</param>
        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);
            this.SetIndicatorPos();
        }

        private static void OnMarkerChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SetIndicatorPos()
        {
            if (this.indicator == null || this.track == null)
            {
                return;
            }

            double x = this.PosFromValue(this.Value);
            var animation = new DoubleAnimation(this.track.ActualWidth * (x - 0.5), TimeSpan.FromMilliseconds(200));
            this.indicatorTransform.BeginAnimation(TranslateTransform.XProperty, animation);
        }

        private double PosFromValue(double value)
        {
            double minimum = this.Minimum;
            double maximum = this.Maximum;
            return (value - minimum) / Math.Abs(maximum - minimum);
        }
    }
}