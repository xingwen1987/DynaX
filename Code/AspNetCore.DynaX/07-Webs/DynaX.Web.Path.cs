using Microsoft.AspNetCore.Hosting;
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
            public static class Path
            {
                private static readonly IHostingEnvironment HostingEnvironment;

                static Path()
                {
                    HostingEnvironment = ServiceProvider.Current.GetRequiredService<IHostingEnvironment>();
                }

                public static string WebRootPath => HostingEnvironment.WebRootPath;

                public static string ContentRootPath => HostingEnvironment.ContentRootPath;
            }
        }
    }
}
