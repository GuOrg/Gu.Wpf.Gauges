namespace Gu.Wpf.Gauges
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class WeakDictionary<TKey, TValue> : IEnumerable<WeakKeyValuePair<TKey, TValue>>
        where TKey : class
    {
        private readonly List<WeakKeyValuePair<TKey, TValue>> inner = new List<WeakKeyValuePair<TKey, TValue>>();

        public IEnumerator<WeakKeyValuePair<TKey, TValue>> GetEnumerator()
        {
            this.inner.RemoveAll(x => x.Key is null);
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
                this.inner.Add(new WeakKeyValuePair<TKey, TValue>(key, value));
            }

            this.inner.RemoveAll(x => x.Key is null);
        }

        public void Clear()
        {
            this.inner.Clear();
        }
    }
}