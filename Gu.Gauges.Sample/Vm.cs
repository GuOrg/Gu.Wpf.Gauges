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
        private double value;
        private bool showLabels;

        public Vm()
        {
            this.Value = 0.3;
            this.showLabels = true;
            this.AngularTickBarVm = new AngularTickBarVm();
            this.TickBarVm = this.AngularTickBarVm;
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