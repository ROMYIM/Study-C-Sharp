using DotNetty.Transport.Channels;
using Microsoft.Extensions.Logging;
using NettyDemo.Infrastructure.Caches.Abbractions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace NettyDemo.Infrastructure.Caches
{
    public class ChannelCache : IKeyValueCache<string, IChannel>
    {
        /// <summary>
        /// 判断缓存集合有没有更新
        /// </summary>
        private ulong _updated = 0;

        /// <summary>
        /// 键缓存集合
        /// </summary>
        private ICollection<string> _keysCache;

        /// <summary>
        /// 值缓存集合
        /// </summary>
        private ICollection<IChannel> _valueCache;

        private readonly ILogger _logger;

        private readonly ConcurrentDictionary<string, IChannel> _channels = new ConcurrentDictionary<string, IChannel>();

        public ChannelCache(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(GetType());
        }

        /// <summary>
        /// 如果判断没有数据修改就返回缓存的键集合
        /// </summary>
        /// <value>键集合</value>
        public ICollection<string> Keys 
        {
            get
            {
                if (_keysCache == null || Interlocked.Read(ref _updated) != 0)
                {
                    _keysCache = _channels.Keys;
                    Interlocked.Exchange(ref _updated, 0L);
                    return _keysCache;
                }

                return _keysCache;
            }
        }

        /// <summary>
        /// 如果判断没有数据更改就直接返回缓存
        /// </summary>
        /// <value>值集合的缓存</value>
        public ICollection<IChannel> Values 
        {
            get
            {
                if (_valueCache == null || Interlocked.Read(ref _updated) != 0)
                {
                    _valueCache = _channels.Values;
                    Interlocked.Exchange(ref _updated, 0L);
                    return _valueCache;
                }

                return _valueCache;
            }
        }

        public bool TryAdd(string key, IChannel value)
        {
            if (_channels.TryAdd(key, value))
            {
                return Interlocked.Increment(ref _updated) != 0;
            }
            return false;
        }

        public bool TryAddOrUpdate(string key, IChannel value)
        {
            if (value == _channels.AddOrUpdate(key, value, (k, v) => value))
                return Interlocked.Increment(ref _updated) != 0;
            return false;
        }

        public bool TryGetValue(string key, out IChannel value)
        {
            return _channels.TryGetValue(key, out value);
        }

        public bool TryRemove(string key, out IChannel channel)
        {
            if (_channels.TryRemove(key, out channel))
                return Interlocked.Increment(ref _updated) != 0;
            return false;
        }

    }
}