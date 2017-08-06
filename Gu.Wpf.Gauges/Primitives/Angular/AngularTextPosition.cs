namespace Gu.Wpf.Gauges
{
    using System;
    using System.Windows;

    public abstract class AngularTextPosition
    {
        public event EventHandler ArrangeDirty;

        public static AngularTextPosition Default { get; } = DefaultAngularTextPosition.Instance;

        /// <summary>
        /// Updates the transform of <paramref name="tickText"/>
        /// </summary>
        /// <returns>Returns the overflow.</returns>
        public abstract Thickness ArrangeTick(TickText tickText, ArcInfo arc, AngularTextBar textBar);

        protected virtual void OnArrangeDirty()
        {
            this.ArrangeDirty?.Invoke(this, EventArgs.Empty);
        }
    }
}