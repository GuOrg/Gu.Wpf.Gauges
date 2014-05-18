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

        public Vm()
        {
            this.Min = -200;
            this.Max = 200;
            this.Value = 0.3;
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