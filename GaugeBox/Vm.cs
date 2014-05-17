namespace GaugeBox
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using Annotations;

    public class Vm : INotifyPropertyChanged
    {
        private double _value;
        private double _min;
        private double _max;

        public Vm()
        {
            Min = -200;
            Max = 200;
            Value = 0.3;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public double Value
        {
            get { return _value; }
            set
            {
                if (value.Equals(_value)) return;
                _value = value;
                OnPropertyChanged();
            }
        }

        public double Min
        {
            get { return _min; }
            set
            {
                if (value.Equals(_min)) return;
                _min = value;
                OnPropertyChanged();
            }
        }

        public double Max
        {
            get { return _max; }
            set
            {
                if (value.Equals(_max)) return;
                _max = value;
                OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
