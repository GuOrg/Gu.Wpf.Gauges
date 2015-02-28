namespace Gu.Gauges
{
    using System;
    using System.Windows;
    using System.Windows.Controls.Primitives;

    public class LinearAxis : Axis
    {
        private static readonly DependencyPropertyKey ReservedSpaceMarginPropertyKey = DependencyProperty.RegisterReadOnly(
            "ReservedSpaceMargin",
            typeof(Thickness),
            typeof(LinearAxis),
            new PropertyMetadata(default(Thickness)));

        public static readonly DependencyProperty ReservedSpaceMarginProperty = ReservedSpaceMarginPropertyKey.DependencyProperty;

        public static readonly DependencyProperty PenWidthProperty = LinearTickBar.PenWidthProperty.AddOwner(typeof(LinearAxis), new PropertyMetadata(1.0, OnPenWidthChanged));

        static LinearAxis()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LinearAxis), new FrameworkPropertyMetadata(typeof(LinearAxis)));
            ReservedSpaceProperty.OverrideMetadata(typeof(LinearAxis), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.None, OnReservedSpaceChanged));
        }

        public Thickness ReservedSpaceMargin
        {
            get { return (Thickness)this.GetValue(ReservedSpaceMarginProperty); }
            protected set { this.SetValue(ReservedSpaceMarginPropertyKey, value); }
        }

        public double PenWidth
        {
            get { return (double)this.GetValue(PenWidthProperty); }
            set { this.SetValue(PenWidthProperty, value); }
        }

        private static void OnPenWidthChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((LinearAxis)o).UpdateReservedSpaceMargin();
        }
        private static void OnReservedSpaceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ((LinearAxis)o).UpdateReservedSpaceMargin();
        }

        private void UpdateReservedSpaceMargin()
        {
            var margin = (this.ReservedSpace + this.PenWidth) / 2;

            switch (this.Placement)
            {
                case TickBarPlacement.Left:
                case TickBarPlacement.Right:
                    this.ReservedSpaceMargin = new Thickness(0, margin, 0, margin);
                    break;
                case TickBarPlacement.Top:
                case TickBarPlacement.Bottom:
                    this.ReservedSpaceMargin = new Thickness(margin, 0, margin, 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
