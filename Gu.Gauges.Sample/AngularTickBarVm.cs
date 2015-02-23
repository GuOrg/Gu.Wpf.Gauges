namespace Gu.Gauges.Sample
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
        }

        public double MinAngle
        {
            get
            {
                return this.minAngle;
            }
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
            get
            {
                return this.maxAngle;
            }
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