namespace Gu.Gauges
{
    using System;

    public class WeakKeyValuePair<TKey, TValue> where TKey : class
    {
        private readonly WeakReference<TKey> keyRef = new WeakReference<TKey>(null);
        public WeakKeyValuePair(TKey key, TValue value)
        {
            this.keyRef.SetTarget(key);
            this.Value = value;
        }

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
        public TValue Value { get; internal set; }
    }
}