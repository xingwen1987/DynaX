using Microsoft.AspNetCore.Builder;
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
        /// DynaX 启用应用服务
        /// </summary>
        public class AppDynaX : IAppDynaX
        {
            private readonly IApplicationBuilder _app;

            public AppDynaX(IApplicationBuilder app) { _app = app; }

            /// <summary>
            /// DynaX Web 组件注入服务
            /// </summary>
            public IApplicationBuilder Webs()
            {
                var serviceProvider = _app.ApplicationServices;
                Web.ServiceProvider.Configure(serviceProvider);
                var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
                Web.HttpContext.Configure(httpContextAccessor);
                return _app;
            }
        }
    }
}
