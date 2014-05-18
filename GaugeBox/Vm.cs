namespace GaugeBox
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls.Primitives;
    using Annotations;
    using Gauges;

    public class Vm : INotifyPropertyChanged
    {
        private double max;
        private double min;
        private double value;
        private bool showLabels;
        private bool showTrack;
        private bool showTicks;
        private TickBarPlacement placement;
        private double tickFrequency;
        private Marker marker;

        public Vm()
        {
            this.Min = -200;
            this.Max = 200;
            this.Value = 0.3;
            this.showLabels = true;
            this.showTicks = true;
            this.ShowTrack = true;
            this.placement = TickBarPlacement.Bottom;
            this.tickFrequency = 25;
        }

        public event PropertyChangedEventHandler PropertyChanged;

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

        public bool ShowTrack
        {
            get
            {
                return this.showTrack;
            }
            set
            {
                if (value.Equals(this.showTrack))
                {
                    return;
                }
                this.showTrack = value;
                this.OnPropertyChanged();
            }
        }

        public bool ShowTicks
        {
            get
            {
                return this.showTicks;
            }
            set
            {
                if (value.Equals(this.showTicks))
                {
                    return;
                }
                this.showTicks = value;
                this.OnPropertyChanged();
            }
        }

        public double TickFrequency
        {
            get
            {
                return this.tickFrequency;
            }
            set
            {
                if (value.Equals(this.tickFrequency))
                {
                    return;
                }
                this.tickFrequency = value;
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
       
        public Marker Marker
        {
            get
            {
                return this.marker;
            }
            set
            {
                if (value == this.marker)
                {
                    return;
                }
                this.marker = value;
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