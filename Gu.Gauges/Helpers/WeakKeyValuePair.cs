namespace Gu.Gauges
{
    using System;

    /// <summary>
    /// Weak reference to the key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
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