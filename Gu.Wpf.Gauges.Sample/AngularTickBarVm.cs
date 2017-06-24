namespace Gu.Wpf.Gauges.Sample
{
    public class AngularTickBarVm : TickBarVm
    {
        private double minAngle;
        private double maxAngle;

        public AngularTickBarVm()
        {
            this.minAngle = -180;
            this.maxAngle = 0;
            this.Maximum = 100;
            this.MajorTickFrequency = 25;
        }

        public double MinAngle
        {
            get => this.minAngle;
            set
            {
                if (value.Equals(this.minAngle))
                {
                    return;
                }

                this.minAngle = value;
                this.OnPropertyChanged();
            }
        }

        public double MaxAngle
        {
            get => this.maxAngle;
            set
            {
                if (value.Equals(this.maxAngle))
                {
                    return;
                }

                this.maxAngle = value;
                this.OnPropertyChanged();
            }
        }
    }
}