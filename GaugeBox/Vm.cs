namespace GaugeBox
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using Annotations;

    public class Vm : INotifyPropertyChanged
    {
        private double max;
        private double min;
        private double value;
        private bool showLabels;
        private bool showTrack;
        private bool showTicks;

        public Vm()
        {
            this.Min = -200;
            this.Max = 200;
            this.Value = 0.3;
            this.showLabels = true;
            this.showTicks = true;
            this.ShowTrack = true;
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