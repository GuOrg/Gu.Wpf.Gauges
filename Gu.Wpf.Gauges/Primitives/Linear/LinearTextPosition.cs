namespace Gu.Wpf.Gauges
{
    using System;
    using System.Windows;

    public abstract class LinearTextPosition
    {
        public event EventHandler ArrangeDirty;

        public static LinearTextPosition Default { get; } = DefaultLinearTextPosition.Instance;

        public abstract void ArrangeTick(TickText tickText, Size arrangeSize, LinearTextBar textBar);

        protected virtual void OnArrangeDirty()
        {
            this.ArrangeDirty?.Invoke(this, EventArgs.Empty);
        }
    }
}
