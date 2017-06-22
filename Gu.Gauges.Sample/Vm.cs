namespace Gu.Gauges.Sample
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class Vm : INotifyPropertyChanged
    {
        private double value;
        private bool showLabels;

        public Vm()
        {
            this.Value = 0.3;
            this.showLabels = true;
            this.AngularTickBarVm = new AngularTickBarVm();
            this.TickBarVm = new TickBarVm();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public TickBarVm TickBarVm { get; }

        public AngularTickBarVm AngularTickBarVm { get; }

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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}