namespace Gu.Gauges
{
    using System;
    using System.Windows.Media;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    public class LinearPanel : Panel
    {
        /// <summary>
        /// Identifies the <see cref="P:LinearPanel.Minimum" /> dependency property. 
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:LinearPanel.Minimum" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty MinimumProperty = RangeBase.MinimumProperty.AddOwner(
            typeof(LinearPanel),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:LinearPanel.Maximum" /> dependency property. 
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:LinearPanel.Maximum" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty MaximumProperty = RangeBase.MaximumProperty.AddOwner(
            typeof(LinearPanel),
            new FrameworkPropertyMetadata(
                1.0,
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:BlockLinearPanel.Placement" /> dependency property. This property is read-only.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:BlockLinearPanel.Placement" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty PlacementProperty = TickBar.PlacementProperty.AddOwner(
            typeof(LinearPanel),
            new FrameworkPropertyMetadata(
                TickBarPlacement.Bottom,
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:LinearPanel.IsDirectionReversed" /> dependency property. 
        /// </summary>
        public static readonly DependencyProperty IsDirectionReversedProperty = Slider.IsDirectionReversedProperty.AddOwner(
            typeof(LinearPanel),
            new FrameworkPropertyMetadata(
                false,
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Identifies the <see cref="P:LinearPanel.ReservedSpace" /> dependency property. This property is read-only.
        /// </summary>
        /// <returns>
        /// The identifier for the <see cref="P:LinearPanel.ReservedSpace" /> dependency property.
        /// </returns>
        public static readonly DependencyProperty ReservedSpaceProperty = TickBar.ReservedSpaceProperty.AddOwner(
            typeof(LinearPanel),
            new FrameworkPropertyMetadata(
                0.0,
                FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty StartProperty = DependencyProperty.RegisterAttached(
            "Start",
            typeof(double),
            typeof(LinearPanel),
            new PropertyMetadata(double.NaN, OnPositionChanged));

        public static readonly DependencyProperty EndProperty = DependencyProperty.RegisterAttached(
            "End",
            typeof(double),
            typeof(LinearPanel),
            new PropertyMetadata(double.NaN, OnPositionChanged));

        public static readonly DependencyProperty AtValueProperty = DependencyProperty.RegisterAttached(
            "AtValue",
            typeof(double),
            typeof(LinearPanel),
            new PropertyMetadata(double.NaN, OnPositionChanged));

        /// <summary>
        /// Gets or sets the <see cref="P:LinearPanel.Minimum" />
        /// The default is 0
        /// </summary>
        public double Minimum
        {
            get { return (double)this.GetValue(MinimumProperty); }
            set { this.SetValue(MinimumProperty, value); }
        }

        /// <summary>
        /// Gets or sets the highest possible <see cref="P:LinearPanel.Maximum" /> of the range element.  
        /// </summary>
        /// <returns>
        /// The highest possible <see cref="P:LinearPanel.Maximum" /> of the range element. The default is 1.
        /// </returns>
        public double Maximum
        {
            get { return (double)this.GetValue(MaximumProperty); }
            set { this.SetValue(MaximumProperty, value); }
        }

        /// <summary>
        /// Gets or sets where tick marks appear  relative to a <see cref="T:System.Windows.Controls.Primitives.Track" /> of a <see cref="T:System.Windows.Controls.Slider" /> control.  
        /// </summary>
        /// <returns>
        /// A <see cref="T:BlockBarPlacement" /> enumeration value that identifies the position of the <see cref="T:LinearPanel" /> in the <see cref="T:System.Windows.Style" /> layout of a <see cref="T:System.Windows.Controls.Slider" />. The default value is <see cref="F:BlockBarPlacement.Top" />.
        /// </returns>
        public TickBarPlacement Placement
        {
            get { return (TickBarPlacement)this.GetValue(PlacementProperty); }
            set { this.SetValue(PlacementProperty, value); }
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
        /// Gets or sets a space buffer for the area that contains the tick marks that are specified for a <see cref="T:Bar" />.  
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
            element.SetValue(StartProperty, value);
        }

        public static double GetStart(DependencyObject element)
        {
            return (double)element.GetValue(StartProperty);
        }

        public static void SetEnd(DependencyObject element, double value)
        {
            element.SetValue(EndProperty, value);
        }

        public static double GetEnd(DependencyObject element)
        {
            return (double)element.GetValue(EndProperty);
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
            Line l1;
            Line l2;

            switch (this.Placement)
            {
                case TickBarPlacement.Left:
                case TickBarPlacement.Right:
                    l1 = new Line(arrangeSize, this.ReservedSpace, TickBarPlacement.Left, this.IsDirectionReversed);
                    l2 = new Line(arrangeSize, this.ReservedSpace, TickBarPlacement.Right, this.IsDirectionReversed);
                    break;
                case TickBarPlacement.Top:
                case TickBarPlacement.Bottom:
                    l1 = new Line(arrangeSize, this.ReservedSpace, TickBarPlacement.Top, this.IsDirectionReversed);
                    l2 = new Line(arrangeSize, this.ReservedSpace, TickBarPlacement.Bottom, this.IsDirectionReversed);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            foreach (UIElement child in this.InternalChildren)
            {
                if (child == null) { continue; }

                Point ps = new Point();
                Point pe = new Point();

                double start = GetStart(child);
                double end = GetEnd(child);

                if (double.IsNaN(start) || double.IsNaN(end))
                {
                    double center = GetAtValue(child);

                    if (!double.IsNaN(center))
                    {
                        var p1 = l1.Interpolate(center, this.Minimum, this.Maximum);
                        var p2 = l2.Interpolate(center, this.Minimum, this.Maximum);
                        var w2 = child.DesiredSize.Width / 2;
                        var h2 = child.DesiredSize.Height / 2;
                        ps = new Point(p1.X - w2, p1.Y - h2);
                        pe = new Point(p2.X + w2, p2.Y + h2);
                        var rect = new Rect(arrangeSize);
                        var rect1 = new Rect(ps, pe);
                        rect.Intersect(rect1);
                        if (rect.IsEmpty)
                        {
                            child.Arrange(new Rect());
                        }
                        else
                        {
                            child.Arrange(rect);
                        }
                    }
                }
                else
                {
                    ps = l1.Interpolate(start, this.Minimum, this.Maximum);
                    pe = l2.Interpolate(end, this.Minimum, this.Maximum);
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
                var panel = VisualTreeHelper.GetParent(uie) as LinearPanel;
                if (panel != null)
                {
                    panel.InvalidateArrange();
                }
            }
        }
    }
}
