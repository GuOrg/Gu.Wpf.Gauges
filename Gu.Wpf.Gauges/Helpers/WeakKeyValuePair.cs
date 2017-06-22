namespace Gu.Wpf.Gauges
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Weak reference to the key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class WeakKeyValuePair<TKey, TValue> : INotifyPropertyChanged
        where TKey : class
    {
        private readonly WeakReference<TKey> keyRef = new WeakReference<TKey>(null);
        private TValue value;

        public WeakKeyValuePair(TKey key, TValue value)
        {
            this.keyRef.SetTarget(key);
            this.Value = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public TKey Key
        {
            get
            {
                {
                    TKey key;
                    this.keyRef.TryGetTarget(out key);
                    return key;
                }
            }
        }

        public TValue Value
        {
            get => this.value;

            internal set
            {
                if (System.Collections.Generic.EqualityComparer<TValue>.Default.Equals(value, this.value))
                {
                    return;
                }

                this.value = value;
                this.OnPropertyChanged();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}