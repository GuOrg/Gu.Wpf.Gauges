using System;
using System.Linq;

namespace Gu.Gauges
{
    using System.Collections;
    using System.Collections.Generic;

    public class WeakDictionary<TKey, TValue> : IEnumerable<WeakKeyValuePair<TKey, TValue>> where TKey : class
    {
        private readonly List<WeakKeyValuePair<TKey, TValue>> inner = new List<WeakKeyValuePair<TKey, TValue>>();
        public IEnumerator<WeakKeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return this.inner.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void AddOrUpdate(TKey key, TValue value)
        {
            var kvp = this.inner.FirstOrDefault(x => ReferenceEquals(x.Key, key));
            if (kvp != null)
            {
                kvp.Value = value;
            }
            else
            {
                inner.Add(new WeakKeyValuePair<TKey, TValue>(key, value));
            }
        }

        public void Clear()
        {
            this.inner.Clear();
        }
    }

    public class WeakKeyValuePair<TKey, TValue> where TKey : class
    {
        private readonly WeakReference<TKey> keyRef = new WeakReference<TKey>(null);
        public WeakKeyValuePair(TKey key, TValue value)
        {
            this.keyRef.SetTarget(key);
            Value = value;
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