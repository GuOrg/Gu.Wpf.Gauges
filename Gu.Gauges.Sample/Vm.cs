namespace Gu.Gauges.Sample
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    using Gu.Gauges.Sample.Annotations;

    public class Vm : INotifyPropertyChanged
    {
        private double max;
        private double min;
        private double value;
        private bool showLabels;
        private TickBarPlacement placement;
        private double majorTickFrequency;
        private double minorTickFrequency;
        private bool isDirectionReversed;

        private DoubleCollection majorTicks;

        public Vm()
        {
            this.Min = -200;
            this.Max = 200;
            this.Value = 0.3;
            this.showLabels = true;
            this.placement = TickBarPlacement.Top;
            this.majorTickFrequency = 100;
            this.minorTickFrequency = 25;
            this.majorTicks = new DoubleCollection(new double[] { 50, 150 });
            this.TickBarVm = new TickBarVm();
            this.AngularTickBarVm = new AngularTickBarVm();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public TickBarVm TickBarVm { get; private set; }

        public AngularTickBarVm AngularTickBarVm { get; private set; }

        public double Value
        {
            get
            {
                return this.value;
            }

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

        public double Min
        {
            get
            {
                return this.min;
            }

            set
            {
                if (value.Equals(this.min))
                {
                    return;
                }

                this.min = value;
                this.OnPropertyChanged();
            }
        }

        public double Max
        {
            get
            {
                return this.max;
            }

            set
            {
                if (value.Equals(this.max))
                {
                    return;
                }

                this.max = value;
                this.OnPropertyChanged();
            }
        }

        public bool ShowLabels
        {
            get
            {
                return this.showLabels;
            }
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

        public double MajorTickFrequency
        {
            get
            {
                return this.majorTickFrequency;
            }
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
            get
            {
                return this.majorTicks;
            }
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
            get { return this.minorTickFrequency; }
            set
            {
                if (value.Equals(this.minorTickFrequency)) return;
                this.minorTickFrequency = value;
                this.OnPropertyChanged();
            }
        }

        public bool IsDirectionReversed
        {
            get { return this.isDirectionReversed; }
            set
            {
                if (value.Equals(this.isDirectionReversed)) return;
                this.isDirectionReversed = value;
                this.OnPropertyChanged();
            }
        }

        public TickBarPlacement Placement
        {
            get
            {
                return this.placement;
            }
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

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}