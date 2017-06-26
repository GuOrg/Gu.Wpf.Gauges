namespace Gu.Wpf.Gauges.Sample
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    public class TickBarVm : INotifyPropertyChanged
    {
        private double minimum;
        private double maximum;
        private double majorTickFrequency;
        private double reservedSpace;
        private DoubleCollection majorTicks;
        private TickBarPlacement placement;
        private bool isDirectionReversed;

        private double value;
        private TextOrientation textOrientation;
        private HorizontalTextAlignment horizontalTextAlignment;
        private VerticalTextAlignment verticalTextAlignment;
        private double minorTickFrequency;
        private DoubleCollection minorTicks;
        private double tickGap = 1.0;
        private double thickness;
        private bool showLabels;

        public TickBarVm()
        {
            this.Minimum = 0;
            this.Maximum = 100;
            this.value = 50;
            this.MajorTickFrequency = 50;
            this.ReservedSpace = 0;
            this.Placement = TickBarPlacement.Bottom;
        }

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

        public double ReservedSpace
        {
            get => this.reservedSpace;
            set
            {
                if (value.Equals(this.reservedSpace))
                {
                    return;
                }

                this.reservedSpace = value;
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

        public TextOrientation TextOrientation
        {
            get => this.textOrientation;
            set
            {
                if (value == this.textOrientation)
                {
                    return;
                }

                this.textOrientation = value;
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
            }
        }

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
