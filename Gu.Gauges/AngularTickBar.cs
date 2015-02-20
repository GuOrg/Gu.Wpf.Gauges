using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Gu.Gauges
{
    using System.Windows.Controls.Primitives;

    public class AngularTickBar : TickBar
    {
        public static readonly DependencyProperty MinAngleProperty = DependencyProperty.Register(
            "MinAngle",
            typeof(double),
            typeof(AngularTickBar),
            new FrameworkPropertyMetadata(
                default(double),
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty MaxAngleProperty = DependencyProperty.Register(
            "MaxAngle",
            typeof(double),
            typeof(AngularTickBar),
            new FrameworkPropertyMetadata(
                default(double),
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty StrokeProperty = Shape.StrokeProperty.AddOwner(
            typeof(AngularTickBar),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty StrokeThicknessProperty = Shape.StrokeThicknessProperty.AddOwner(
            typeof(AngularTickBar),
            new FrameworkPropertyMetadata(
                default(double),
                FrameworkPropertyMetadataOptions.AffectsRender));

        static AngularTickBar()
        {
        }

        public double MinAngle
        {
            get { return (double)this.GetValue(MinAngleProperty); }
            set { this.SetValue(MinAngleProperty, value); }
        }

        public double MaxAngle
        {
            get { return (double)this.GetValue(MaxAngleProperty); }
            set { this.SetValue(MaxAngleProperty, value); }
        }

        public Brush Stroke
        {
            get { return (Brush)this.GetValue(StrokeProperty); }
            set { this.SetValue(StrokeProperty, value); }
        }

        public double StrokeThickness
        {
            get { return (double)this.GetValue(StrokeThicknessProperty); }
            set { this.SetValue(StrokeThicknessProperty, value); }
        }

        protected override void OnRender(DrawingContext dc)
        {
            var pen = new Pen(this.Stroke, this.StrokeThickness);
            var midPoint = new Point(this.ActualWidth / 2, this.ActualHeight / 2);
            var pi = new Point(this.ActualWidth - this.ReservedSpace, midPoint.Y);
            var po = new Point(this.ActualWidth, midPoint.Y);
            var ticks = this.Ticks.Concat(this.CreateTicks(this.Minimum, this.Maximum, this.TickFrequency));
            foreach (var tick in ticks)
            {
                if (tick < this.Minimum || tick > this.Maximum)
                {
                    continue;
                }
                var angle = this.ToAngle(tick);
                var rotateTransform = new RotateTransform(angle, midPoint.X, midPoint.Y);
                var p1 = rotateTransform.Transform(pi);
                var p2 = rotateTransform.Transform(po);
                dc.DrawLine(pen, p1, p2);
            }
        }

        protected override void OnStyleChanged(Style oldStyle, Style newStyle)
        {
            base.OnStyleChanged(oldStyle, newStyle);
        }

        protected virtual void OnGeometryChanged()
        {
            this.Ticks.Clear();
            if (this.TickFrequency > 0)
            {
                var ticks = Math.Abs(this.Maximum - this.Minimum) / this.TickFrequency;
                var d = (this.MaxAngle - this.MinAngle) / ticks;
                for (double a = this.MinAngle; a < this.MaxAngle; a += d)
                {
                    this.Ticks.Add(a);
                }
                this.Ticks.Add(this.MaxAngle);
            }
        }

        private IEnumerable<double> CreateTicks(double minimum, double maximum, double tickFrequency)
        {
            if (tickFrequency <= 0)
            {
                yield break;
            }

            var min = Math.Min(minimum, maximum);
            var max = Math.Max(minimum, maximum);
            var threshold = 0.1 * tickFrequency + max;
            for (var v = min; v < threshold; v+=tickFrequency)
            {
                yield return v;
            }
        }

        private double ToAngle(double tick)
        {
            var dv = (tick - this.Minimum) / (this.Maximum - this.Minimum);
            var a = dv * (this.MaxAngle - this.MinAngle) + this.MinAngle;
            return a;
        }
    }
}