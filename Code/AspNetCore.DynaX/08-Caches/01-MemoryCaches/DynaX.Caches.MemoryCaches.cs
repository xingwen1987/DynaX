using System;
using System.Threading;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace AspNetCore.DynaX
{
    /// <summary>
    /// DynaX 工具集合
    /// </summary>
    public static partial class DynaX
    {
        /// <summary>
        /// DynaX Cache 缓存集合
        /// </summary>
        public static partial class Caches
        {
            /// <summary>
            /// DynaX Caches MemoryCaches 缓存集合
            /// </summary>
            public static partial class MemoryCaches
            {
                private static readonly MemoryCache MemoryCache = new MemoryCache(new MemoryCacheOptions());
                private static CancellationTokenSource _memoryCacheToken = new CancellationTokenSource();

                /// <summary>
                /// 获取缓存值
                /// </summary>
                /// <param name="key">缓存Key</param>
                /// <returns></returns>
                public static T Get<T>(object key)
                {
                    return key != null && MemoryCache.TryGetValue(key, out T val) ? val : default(T);
                }

                /// <summary>
                /// 添加缓存内容
                /// </summary>
                /// <param name="key">缓存Key</param>
                /// <param name="value">缓存值</param>
                public static void Set(object key, object value)
                {
                    if (key == null) return;
                    var cacheOptions = new MemoryCacheEntryOptions { SlidingExpiration = TimeSpan.FromDays(1) }.AddExpirationToken(new CancellationChangeToken(_memoryCacheToken.Token));
                    MemoryCache.Set(key, value, cacheOptions);
                }

                /// <summary>
                /// 删除缓存
                /// </summary>
                /// <param name="key">缓存Key</param>
                public static void Remove(object key)
                {
                    if (key != null)
                    {
                        MemoryCache.Remove(key);
                    }
                }

                /// <summary>
                /// 删除缓存
                /// </summary>
                public static void Clear()
                {
                    if (_memoryCacheToken != null && !_memoryCacheToken.IsCancellationRequested && _memoryCacheToken.Token.CanBeCanceled)
                    {
                        _memoryCacheToken.Cancel();
                        _memoryCacheToken.Dispose();
                    }
                    _memoryCacheToken = new CancellationTokenSource();
                }
            }
        }
    }
}
