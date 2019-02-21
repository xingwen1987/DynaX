using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AspNetCore.DynaX
{
    /// <summary>
    /// DynaX 工具集合
    /// </summary>
    public static partial class DynaX
    {
        /// <summary>
        /// DynaX Web 扩展集合
        /// </summary>
        public static partial class Web
        {
            public static class ServiceProvider
            {
                public static IServiceProvider Current { get; private set; }

                internal static void Configure(IServiceProvider serviceProvider)
                {
                    Current = serviceProvider;
                }
            }
        }
    }
}
