namespace Gu.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    public class AngularPanel : Panel
    {
        public static readonly DependencyProperty MinAngleProperty = AngularBar.MinAngleProperty.AddOwner(
        typeof(AngularPanel),
        new FrameworkPropertyMetadata(
            -180.0,
            FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty MaxAngleProperty = AngularBar.MaxAngleProperty.AddOwner(
                typeof(AngularPanel),
                new FrameworkPropertyMetadata(
                    0.0,
                    FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:AngularPanel.Minimum" /> dependency property. 
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:AngularPanel.Minimum" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty MinimumProperty = RangeBase.MinimumProperty.AddOwner(
            typeof(AngularPanel),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:AngularPanel.Maximum" /> dependency property. 
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:AngularPanel.Maximum" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty MaximumProperty = RangeBase.MaximumProperty.AddOwner(
            typeof(AngularPanel),
            new FrameworkPropertyMetadata(
                1.0,
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:AngularPanel.IsDirectionReversed" /> dependency property. 
        /// </summary>
        public static readonly DependencyProperty IsDirectionReversedProperty = Slider.IsDirectionReversedProperty.AddOwner(
            typeof(AngularPanel),
            new FrameworkPropertyMetadata(
                false,
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:AngularPanel.ReservedSpace" /> dependency property. This property is read-only.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:AngularPanel.ReservedSpace" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty ReservedSpaceProperty = TickBar.ReservedSpaceProperty.AddOwner(
            typeof(AngularPanel),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty Start = DependencyProperty.RegisterAttached(
            "Start",
            typeof(double),
            typeof(AngularPanel),
            new PropertyMetadata(double.NaN, OnPositionChanged));

        public static readonly DependencyProperty End = DependencyProperty.RegisterAttached(
            "End",
            typeof(double),
            typeof(AngularPanel),
            new PropertyMetadata(double.NaN, OnPositionChanged));

        public static readonly DependencyProperty AtValueProperty = DependencyProperty.RegisterAttached(
            "AtValue",
            typeof(double),
            typeof(AngularPanel),
            new PropertyMetadata(double.NaN, OnPositionChanged));


        /// <summary>
        /// Gets or sets the <see cref="P:AngularPanel.MinAngle" />
        /// The default is -180
        /// </summary>
        public double MinAngle
        {
            get { return (double)this.GetValue(MinAngleProperty); }
            set { this.SetValue(MinAngleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="P:AngularPanel.MaxAngle" />
        /// The default is 0
        /// </summary>
        public double MaxAngle
        {
            get { return (double)this.GetValue(MaxAngleProperty); }
            set { this.SetValue(MaxAngleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the <see cref="P:AngularPanel.Minimum" />
        /// The default is 0
        /// </summary>
        public double Minimum
        {
            get { return (double)this.GetValue(MinimumProperty); }
            set { this.SetValue(MinimumProperty, value); }
        }

        /// <summary>
        /// Gets or sets the highest possible <see cref="P:AngularPanel.Maximum" /> of the range element.  
        /// </summary>
        /// <returns>
        /// The highest possible <see cref="P:AngularPanel.Maximum" /> of the range element. The default is 1.
        /// </returns>
        public double Maximum
        {
            get { return (double)this.GetValue(MaximumProperty); }
            set { this.SetValue(MaximumProperty, value); }
        }

        /// <summary>
        /// Gets or sets the direction of increasing value. 
        /// </summary>
        /// <returns>
        /// true if the direction of increasing value is to the left for a horizontal tickbar or down for a vertical tickbar; otherwise, false. 
        /// The default is false.
        /// </returns>
        public bool IsDirectionReversed
        {
            get { return (bool)this.GetValue(IsDirectionReversedProperty); }
            set { this.SetValue(IsDirectionReversedProperty, value); }
        }

        /// <summary>
        /// Gets or sets a space buffer for the area that contains the tick marks that are specified for a <see cref="T:AngularPanel" />.  
        /// </summary>
        /// <returns>
        /// A value that represents the total buffer area on either side of the row or column of tick marks. The default value is zero (0.0).
        /// </returns>
        public double ReservedSpace
        {
            get { return (double)this.GetValue(ReservedSpaceProperty); }
            set { this.SetValue(ReservedSpaceProperty, value); }
        }

        public static void SetStart(DependencyObject element, double value)
        {
            element.SetValue(Start, value);
        }

        public static double GetStart(DependencyObject element)
        {
            return (double)element.GetValue(Start);
        }

        public static void SetEnd(DependencyObject element, double value)
        {
            element.SetValue(End, value);
        }

        public static double GetEnd(DependencyObject element)
        {
            return (double)element.GetValue(End);
        }

        public static void SetAtValue(DependencyObject element, double value)
        {
            element.SetValue(AtValueProperty, value);
        }

        public static double GetAtValue(DependencyObject element)
        {
            return (double)element.GetValue(AtValueProperty);
        }


        /// <summary>
        /// Updates DesiredSize of the LinearPanel.
        /// </summary>
        /// <param name="constraint">Constraint size is an "upper limit" that LinearPanel should not exceed.</param>
        /// <returns>LinearPanel's desired size.</returns>
        protected override Size MeasureOverride(Size constraint)
        {
            UIElementCollection children = this.InternalChildren;
            var desiredSize = new Size();
            for (int i = 0, count = children.Count; i < count; ++i)
            {
                UIElement child = children[i];
                if (child != null)
                {
                    child.Measure(constraint);
                    desiredSize.Width = Math.Max(desiredSize.Width, child.DesiredSize.Width);
                    desiredSize.Height = Math.Max(desiredSize.Height, child.DesiredSize.Height);
                }
            }
            return desiredSize;
        }

        /// <summary>
        /// LinearPanel computes a position for each of its children taking into account their  
        /// </summary>
        /// <param name="arrangeSize">Size that LinearPanel will assume to position children.</param>
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            var arc = Arc.Fill(arrangeSize, this.MinAngle, this.MaxAngle, this.IsDirectionReversed);
            var rotateTransform = new RotateTransform(0, arc.Centre.X, arc.Centre.Y);
            foreach (UIElement child in this.InternalChildren)
            {
                if (child == null)
                {
                    continue;
                }

                Point ps;
                Point pe;

                double start = GetStart(child);
                double end = GetEnd(child);

                if (double.IsNaN(start) || double.IsNaN(end))
                {
                    double center = GetAtValue(child);

                    if (!double.IsNaN(center))
                    {
                        var angle = TickHelper.ToAngle(center, this.Minimum, this.Maximum, arc);
                        var p1 = arc.GetPoint(angle);
                        var rect = new Rect(child.DesiredSize);
                        rect.Offset(new Vector(p1.X, p1.Y));
                        child.Arrange(rect);
                    }
                }
                else
                {
                    ps = arc.GetPoint(start);
                    pe = arc.GetPoint(end);
                    child.Arrange(new Rect(ps, pe));
                }
            }
            return arrangeSize;
        }

        private static void OnPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uie = d as UIElement;
            if (uie != null)
            {
                var panel = VisualTreeHelper.GetParent(uie) as AngularPanel;
                if (panel != null)
                {
                    panel.InvalidateArrange();
                }
            }
        }
    }
}