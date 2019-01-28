using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.DynaX
{
    /// <summary>
    /// DynaX 工具集合
    /// </summary>
    public static partial class DynaX
    {
        /// <summary>
        /// DynaX 注入服务
        /// </summary>
        public class DiDynaX : IDynaX
        {
            private readonly IServiceCollection _services;

            public DiDynaX(IServiceCollection services) { _services = services; }

            /// <summary>
            /// DynaX 日志组件注入服务
            /// </summary>
            public IDynaXLogs Logs => new DiDynaXLogs(_services);
        }
    }
}
