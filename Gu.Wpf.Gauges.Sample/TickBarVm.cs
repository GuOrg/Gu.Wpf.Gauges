// ReSharper disable ExplicitCallerInfoArgument
namespace Gu.Wpf.Gauges.Sample
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    public class TickBarVm : INotifyPropertyChanged
    {
        private double minimum;
        private double maximum = 100;
        private double majorTickFrequency = 50;
        private bool snapsToDevicePixels;
        private DoubleCollection majorTicks;
        private TickBarPlacement placement = TickBarPlacement.Bottom;
        private bool isDirectionReversed;

        private double value;
        private HorizontalTextAlignment horizontalTextAlignment;
        private VerticalTextAlignment verticalTextAlignment;
        private double minorTickFrequency;
        private DoubleCollection minorTicks;
        private double tickGap = 1.0;
        private double tickWidth = 1.0;
        private double thickness;
        private bool showLabels;

        public event PropertyChangedEventHandler PropertyChanged;

        public double Minimum
        {
            get => this.minimum;
            set
            {
                if (value.Equals(this.minimum))
                {
                    return;
                }

                this.minimum = value;
                this.OnPropertyChanged();
            }
        }

        public double Maximum
        {
            get => this.maximum;
            set
            {
                if (value.Equals(this.maximum))
                {
                    return;
                }

                this.maximum = value;
                this.OnPropertyChanged();
            }
        }

        public double Value
        {
            get => this.value;
            set
            {
                if (value.Equals(this.value))
                {
                    return;
                }

                this.value = value;
                this.OnPropertyChanged();
            }
        }

        public double MajorTickFrequency
        {
            get => this.majorTickFrequency;
            set
            {
                if (value.Equals(this.majorTickFrequency))
                {
                    return;
                }

                this.majorTickFrequency = value;
                this.OnPropertyChanged();
            }
        }

        public DoubleCollection MajorTicks
        {
            get => this.majorTicks;
            set
            {
                if (Equals(value, this.majorTicks))
                {
                    return;
                }

                this.majorTicks = value;
                this.OnPropertyChanged();
            }
        }

        public double MinorTickFrequency
        {
            get => this.minorTickFrequency;
            set
            {
                if (value.Equals(this.minorTickFrequency))
                {
                    return;
                }

                this.minorTickFrequency = value;
                this.OnPropertyChanged();
            }
        }

        public DoubleCollection MinorTicks
        {
            get => this.minorTicks;

            set
            {
                if (ReferenceEquals(value, this.minorTicks))
                {
                    return;
                }

                this.minorTicks = value;
                this.OnPropertyChanged();
            }
        }

        public double TickGap
        {
            get => this.tickGap;

            set
            {
                if (value == this.tickGap)
                {
                    return;
                }

                this.tickGap = value;
                this.OnPropertyChanged();
            }
        }

        public double TickWidth
        {
            get => this.tickWidth;

            set
            {
                if (value == this.tickWidth)
                {
                    return;
                }

                this.tickWidth = value;
                this.OnPropertyChanged();
            }
        }

        public double Thickness
        {
            get => this.thickness;

            set
            {
                if (value == this.thickness)
                {
                    return;
                }

                this.thickness = value;
                this.OnPropertyChanged();
            }
        }

        public bool SnapsToDevicePixels
        {
            get => this.snapsToDevicePixels;

            set
            {
                if (value == this.snapsToDevicePixels)
                {
                    return;
                }

                this.snapsToDevicePixels = value;
                this.OnPropertyChanged();
            }
        }

        public TickBarPlacement Placement
        {
            get => this.placement;
            set
            {
                if (value == this.placement)
                {
                    return;
                }

                this.placement = value;
                this.OnPropertyChanged();
            }
        }

        public HorizontalTextAlignment HorizontalTextAlignment
        {
            get => this.horizontalTextAlignment;

            set
            {
                if (value == this.horizontalTextAlignment)
                {
                    return;
                }

                this.horizontalTextAlignment = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(this.ExplicitLinearTextPosition));
            }
        }

        public VerticalTextAlignment VerticalTextAlignment
        {
            get => this.verticalTextAlignment;

            set
            {
                if (value == this.verticalTextAlignment)
                {
                    return;
                }

                this.verticalTextAlignment = value;
                this.OnPropertyChanged();
                this.OnPropertyChanged(nameof(this.ExplicitLinearTextPosition));
            }
        }

        public ExplicitLinearTextPosition ExplicitLinearTextPosition => new ExplicitLinearTextPosition(this.horizontalTextAlignment, this.verticalTextAlignment);

        public bool IsDirectionReversed
        {
            get => this.isDirectionReversed;
            set
            {
                if (value.Equals(this.isDirectionReversed))
                {
                    return;
                }

                this.isDirectionReversed = value;
                this.OnPropertyChanged();
            }
        }

        public bool ShowLabels
        {
            get => this.showLabels;
            set
            {
                if (value.Equals(this.showLabels))
                {
                    return;
                }

                this.showLabels = value;
                this.OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
