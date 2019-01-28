using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.DynaX
{
    /// <summary>
    /// DynaX 工具集合
    /// </summary>
    public static partial class DynaX
    {
        /// <summary>
        /// DynaX 日志组件注入服务
        /// </summary>
        public class DiDynaXLogs : IDynaXLogs
        {
            private readonly IServiceCollection _services;

            public DiDynaXLogs(IServiceCollection services)
            {
                _services = services;
            }

            /// <summary>
            /// Serilog 日志组件注入服务
            /// </summary>
            /// <param name="configuration">系统配置</param>
            /// <param name="sectionName">配置区块名城管</param>
            /// <returns></returns>
            public IServiceCollection Serilog(IConfiguration configuration, string sectionName)
            {
                var sectionData = configuration.GetSection(sectionName);
                var serilogConfigs = sectionData.Get<List<Logs.SerilogConfig>>();
                Logs.Serilogs.Setup(serilogConfigs);
                return _services;
            }

            /// <summary>
            /// NLog 日志组件注入服务
            /// </summary>
            /// <param name="configuration">系统配置</param>
            /// <param name="sectionName">配置区块名城管</param>
            /// <returns></returns>
            public IServiceCollection NLog(IConfiguration configuration, string sectionName)
            {
                return _services;
            }

            /// <summary>
            /// Log4Net 日志组件注入服务
            /// </summary>
            /// <param name="configuration">系统配置</param>
            /// <param name="sectionName">配置区块名城管</param>
            /// <returns></returns>
            public IServiceCollection Log4Net(IConfiguration configuration, string sectionName)
            {
                return _services;
            }
        }
    }
}
