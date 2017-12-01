namespace Gu.Wpf.Gauges
{
    using System;
    using System.Collections;
    using System.Windows;
    using System.Windows.Media;

    public class AngularIndicator : ValueIndicator
    {
#pragma warning disable SA1202 // Elements must be ordered by access
        private static readonly DependencyPropertyKey OverflowPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(Overflow),
            typeof(Thickness),
            typeof(AngularIndicator),
            new PropertyMetadata(default(Thickness)));

        public static readonly DependencyProperty OverflowProperty = OverflowPropertyKey.DependencyProperty;

        public static readonly DependencyProperty StartProperty = AngularGauge.StartProperty.AddOwner(
            typeof(AngularIndicator),
            new FrameworkPropertyMetadata(
                Angle.DefaultStart,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        public static readonly DependencyProperty EndProperty = AngularGauge.EndProperty.AddOwner(
            typeof(AngularIndicator),
            new FrameworkPropertyMetadata(
                Angle.DefaultEnd,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));
#pragma warning restore SA1202 // Elements must be ordered by access

        private ContainerVisual internalVisual;
        private RotateTransform rotateTransform;

        static AngularIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AngularIndicator), new FrameworkPropertyMetadata(typeof(AngularIndicator)));
        }

        /// <summary>
        /// Gets a <see cref="Thickness"/> with values indicating how much the control draws outside its bounds.
        /// </summary>
        public Thickness Overflow
        {
            get => (Thickness)this.GetValue(OverflowProperty);
            protected set => this.SetValue(OverflowPropertyKey, value);
        }

        protected override int VisualChildrenCount => 1;

        protected override IEnumerator LogicalChildren
        {
            get
            {
                if (this.InternalChild == null)
                {
                    return EmptyEnumerator.Instance;
                }

                return new SingleChildEnumerator(this.InternalChild);
            }
        }

        private UIElement InternalChild
        {
            get
            {
                var vc = this.InternalVisual.Children;
                if (vc.Count != 0)
                {
                    return vc[0] as UIElement;
                }
                else
                {
                    return null;
                }
            }

            set
            {
                var vc = this.InternalVisual.Children;
                if (vc.Count != 0)
                {
                    vc.Clear();
                }

                vc.Add(value);
            }
        }

        private ContainerVisual InternalVisual
        {
            get
            {
                if (this.internalVisual == null)
                {
                    this.rotateTransform = new RotateTransform();
                    this.internalVisual = new ContainerVisual
                    {
                        Transform = this.rotateTransform,
                    };
                    this.AddVisualChild(this.internalVisual);
                }

                return this.internalVisual;
            }
        }

        private Angle Start
        {
            get => (Angle)this.GetValue(StartProperty);
            set => this.SetValue(StartProperty, value);
        }

        /// <summary>
        /// Gets or sets the end angle of the arc.
        /// Degrees clockwise from the y axis.
        /// The default is 140
        /// </summary>
        private Angle End
        {
            get => (Angle)this.GetValue(EndProperty);
            set => this.SetValue(EndProperty, value);
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            this.InternalChild = (UIElement)newContent;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            this.InternalChild?.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            return double.IsPositiveInfinity(constraint.Width) || double.IsPositiveInfinity(constraint.Height)
                ? this.InternalChild?.DesiredSize ?? default(Size)
                : constraint;
        }

        /// <inheritdoc />
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            var child = this.InternalChild;
            var angularArc = ArcInfo.Fit(arrangeSize, this.Padding, this.Start, this.End);
            var width = child.DesiredSize.Width;
            var height = angularArc.Radius;
            var rect = new Rect(angularArc.Center.X - (width / 2), angularArc.Center.Y - height, width, height);
            child.Arrange(rect);
            var valueInRange = Interpolate.Linear(this.Minimum, this.Maximum, this.Value);
            var compensatedAngle = Interpolate.Linear(this.Start, this.End, valueInRange);
            this.rotateTransform.SetCurrentValue(RotateTransform.AngleProperty, compensatedAngle.Degrees);
            this.rotateTransform.SetCurrentValue(RotateTransform.CenterXProperty, angularArc.Center.X);
            this.rotateTransform.SetCurrentValue(RotateTransform.CenterYProperty, angularArc.Center.Y);
            return arrangeSize;
        }

        /// <inheritdoc />
        protected override Visual GetVisualChild(int index)
        {
            if (index != 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, "Always exactly one child");
            }

            return this.InternalVisual;
        }
    }
}