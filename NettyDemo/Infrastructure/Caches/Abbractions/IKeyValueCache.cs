using System;
using System.Collections.Generic;

namespace NettyDemo.Infrastructure.Caches.Abbractions
{
    public interface IKeyValueCache<TKey, TValue>
    {
        bool TryAdd(TKey key, TValue value);

        bool TryAddOrUpdate(TKey key, TValue value);

        bool TryGetValue(TKey key, out TValue value);

        bool TryRemove(TKey key, out TValue value);

        ICollection<TKey> Keys { get; }

        ICollection<TValue> Values { get; }
    }
}