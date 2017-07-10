namespace Gu.Wpf.Gauges
{
    using System;
    using System.Windows;

    public abstract class AngularTextPosition
    {
        public event EventHandler ArrangeDirty;

        public static AngularTextPosition Default { get; } = DefaultAngularTextPosition.Instance;

        public abstract void ArrangeTick(TickText tickText, Size arrangeSize, AngularTextBar textBar);

        protected virtual void OnArrangeDirty()
        {
            this.ArrangeDirty?.Invoke(this, EventArgs.Empty);
        }
    }
}