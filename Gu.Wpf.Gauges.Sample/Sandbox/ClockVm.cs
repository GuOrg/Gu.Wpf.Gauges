namespace Gu.Wpf.Gauges.Sample.Sandbox
{
    using System;
    using System.ComponentModel;
    using System.Threading;

    public class ClockVm : INotifyPropertyChanged
    {
        // ReSharper disable once NotAccessedField.Local
        private readonly Timer timer;

        public ClockVm()
        {
            this.timer = new Timer(_ => this.OnPropertyChanged(string.Empty), null, TimeSpan.Zero, TimeSpan.FromMilliseconds(50));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public double Second => DateTime.Now.Second + (DateTime.Now.Millisecond / 1000.0);

        public int Minutes => DateTime.Now.Minute;

        public double Hour => DateTime.Now.Hour + ((DateTime.Now.Minute / 60.0) % 12);

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}