namespace Gu.Wpf.Gauges.Sample
{
    public class AngularTickBarVm : TickBarVm
    {
        private double start;
        private double end;

        private TextOrientation textOrientation;
        private double textOffset;

        public AngularTickBarVm()
        {
            this.end = -180;
            this.start = 0;
            this.Maximum = 100;
            this.MajorTickFrequency = 25;
        }

        public double Start
        {
            get => this.start;
            set
            {
                if (value.Equals(this.start))
                {
                    return;
                }

                this.start = value;
                this.OnPropertyChanged();
            }
        }

        public double End
        {
            get => this.end;
            set
            {
                if (value.Equals(this.end))
                {
                    return;
                }

                this.end = value;
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

        public double TextOffset
        {
            get => this.textOffset;
            set
            {
                if (value == this.textOffset)
                {
                    return;
                }

                this.textOffset = value;
                this.OnPropertyChanged();
            }
        }
    }
}