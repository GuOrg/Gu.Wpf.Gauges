namespace Gu.Gauges.Sample
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    public class TickBarVm : INotifyPropertyChanged
    {
        private double minimum;
        private double maximum;
        private double tickFrequency;
        private double reservedSpace;
        private DoubleCollection ticks;
        private TickBarPlacement placement;
        private bool isDirectionReversed;

        private double value;
        private TextOrientation textOrientation;

        private double minorTickFrequency;
        private bool showLabels;

        public TickBarVm()
        {
            this.Minimum = 0;
            this.Maximum = 100;
            this.value = 50;
            this.TickFrequency = 50;
            this.ReservedSpace = 0;
            //this.Ticks = new DoubleCollection(new[] { 5, 15, 25.0 });
            this.Placement = TickBarPlacement.Bottom;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public double Minimum
        {
            get { return this.minimum; }
            set
            {
                if (value.Equals(this.minimum)) return;
                this.minimum = value;
                this.OnPropertyChanged();
            }
        }

        public double Maximum
        {
            get { return this.maximum; }
            set
            {
                if (value.Equals(this.maximum)) return;
                this.maximum = value;
                this.OnPropertyChanged();
            }
        }

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

        public double TickFrequency
        {
            get { return this.tickFrequency; }
            set
            {
                if (value.Equals(this.tickFrequency)) return;
                this.tickFrequency = value;
                this.OnPropertyChanged();
            }
        }

        public double MinorTickFrequency
        {
            get
            {
                return this.minorTickFrequency;
            }
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

        public double ReservedSpace
        {
            get { return this.reservedSpace; }
            set
            {
                if (value.Equals(this.reservedSpace)) return;
                this.reservedSpace = value;
                this.OnPropertyChanged();
            }
        }

        public DoubleCollection Ticks
        {
            get { return this.ticks; }
            set
            {
                if (Equals(value, this.ticks)) return;
                this.ticks = value;
                this.OnPropertyChanged();
            }
        }

        public TickBarPlacement Placement
        {
            get { return this.placement; }
            set
            {
                if (value == this.placement) return;
                this.placement = value;
                this.OnPropertyChanged();
            }
        }

        public TextOrientation TextOrientation
        {
            get { return this.textOrientation; }
            set
            {
                if (value == this.textOrientation) return;
                this.textOrientation = value;
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

        public bool ShowLabels
        {
            get { return this.showLabels; }
            set
            {
                if (value.Equals(this.showLabels)) return;
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
