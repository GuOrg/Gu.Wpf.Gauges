namespace Gu.Wpf.Gauges
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class AngularIndicator : ValueIndicator
    {
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


        private ContainerVisual internalVisual;
        private RotateTransform rotateTransform;
        private TranslateTransform translateTransform;

        static AngularIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AngularIndicator), new FrameworkPropertyMetadata(typeof(AngularIndicator)));
        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            this.InternalChild = (UIElement)newContent;
        }

        //protected override Size MeasureOverride(Size constraint)
        //{
        //    if (this.VisualChild != null)
        //    {
        //        this.VisualChild.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        //        return true
        //            ? new Size(this.VisualChild.DesiredSize.Width, this.VisualChild.DesiredSize.Height)
        //            : new Size(this.VisualChild.DesiredSize.Width, 0);
        //    }

        //    var baseSize = base.MeasureOverride(constraint);
        //    return baseSize;
        //}

        /// <summary>
        /// Gets a <see cref="Thickness"/> with values indicating how much the control draws outside its bounds.
        /// </summary>
        public Thickness Overflow
        {
            get => (Thickness)this.GetValue(OverflowProperty);
            protected set => this.SetValue(OverflowPropertyKey, value);
        }

        public Angle Start
        {
            get => (Angle)this.GetValue(StartProperty);
            set => this.SetValue(StartProperty, value);
        }

        /// <summary>
        /// Gets or sets the end angle of the arc.
        /// Degrees clockwise from the y axis.
        /// The default is 140
        /// </summary>
        public Angle End
        {
            get => (Angle)this.GetValue(EndProperty);
            set => this.SetValue(EndProperty, value);
        }

        //protected override Size ArrangeOverride(Size arrangeBounds)
        //{
        //    var child = this.VisualChild;
        //    if (!double.IsNaN(this.Value) &&
        //        child != null)
        //    {
        //        var pos = Interpolate.Linear(this.Minimum, this.Maximum, this.Value).Value;
        //        var pixelPosX = arrangeBounds.Width*pos;
        //        var pixelPosY = arrangeBounds.Height * pos;

        //        var childRect = new Rect(pixelPosX, pixelPosY, this.VisualChild.DesiredSize.Width, this.VisualChild.DesiredSize.Height);
        //        child.Arrange(childRect); //this.ChildRect(this.PixelPosition(arrangeBounds)));
        //        var w = true //this.Placement.IsHorizontal()
        //            ? child.RenderSize.Width / 2
        //            : child.RenderSize.Height / 2;

        //        this.Overflow = true //this.Placement.IsHorizontal()
        //            ? new Thickness(Math.Max(0, w - this.Padding.Left), 0, Math.Max(0, w - this.Padding.Right), 0)
        //            : new Thickness(0, Math.Max(0, w - this.Padding.Top), 0, Math.Max(0, w - this.Padding.Bottom));
        //    }
        //    else
        //    {
        //        this.Overflow = default(Thickness);
        //    }

        //    this.RegisterOverflow(this.Overflow);
        //    return arrangeBounds;
        //}



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
            //var arcInfo = new ArcInfo(arrangeSize.MidPoint(), arrangeSize.Height/2, this.Start, this.End);
            var width = child.DesiredSize.Width;
            var height = angularArc.Radius;
            var rect = new Rect(angularArc.Center.X-width/2, angularArc.Center.Y-height, width, height);
            child?.Arrange(rect); //new Rect(child.DesiredSize));
            var valueInRange = Interpolate.Linear(this.Minimum, this.Maximum, this.Value);
            var compensatedAngle = Interpolate.Linear(this.Start, this.End, valueInRange);
            this.rotateTransform.Angle = compensatedAngle.Degrees;
            this.rotateTransform.CenterX = angularArc.Center.X;
            this.rotateTransform.CenterY = angularArc.Center.Y;
            return arrangeSize;
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


        /// <inheritdoc />
        protected override int VisualChildrenCount => 1;

        protected override Visual GetVisualChild(int index)
        {
            if (index != 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, "Always exactly one child");
            }

            return this.InternalVisual;
        }

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

        private ContainerVisual InternalVisual
        {
            get
            {
                if (this.internalVisual == null)
                {
                    this.rotateTransform = new RotateTransform();
                    this.translateTransform = new TranslateTransform();
                    var transGroup = new TransformGroup();
                    transGroup.Children.Add(this.rotateTransform);
                    transGroup.Children.Add(this.translateTransform);
                    this.internalVisual = new ContainerVisual
                    {
                        Transform = transGroup,
                    };
                    this.AddVisualChild(this.internalVisual);
                }

                return this.internalVisual;
            }
        }
    }
}