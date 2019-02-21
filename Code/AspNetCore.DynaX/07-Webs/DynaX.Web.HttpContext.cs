using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

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
            public static class HttpContext
            {
                private static IHttpContextAccessor _contextAccessor;

                public static Microsoft.AspNetCore.Http.HttpContext Current => _contextAccessor.HttpContext;

                internal static void Configure(IHttpContextAccessor contextAccessor)
                {
                    _contextAccessor = contextAccessor;
                }
            }
        }
    }
}
