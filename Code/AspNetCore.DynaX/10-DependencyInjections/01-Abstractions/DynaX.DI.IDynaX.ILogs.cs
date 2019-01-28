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
        public interface IDynaXLogs
        {
            /// <summary>
            /// Serilog 日志组件注入服务
            /// </summary>
            /// <param name="configuration">系统配置</param>
            /// <param name="sectionName">配置区块名城管</param>
            /// <returns></returns>
            IServiceCollection Serilog(IConfiguration configuration, string sectionName);
            /// <summary>
            /// NLog 日志组件注入服务
            /// </summary>
            /// <param name="configuration">系统配置</param>
            /// <param name="sectionName">配置区块名城管</param>
            /// <returns></returns>
            IServiceCollection NLog(IConfiguration configuration, string sectionName);
            /// <summary>
            /// Log4Net 日志组件注入服务
            /// </summary>
            /// <param name="configuration">系统配置</param>
            /// <param name="sectionName">配置区块名城管</param>
            /// <returns></returns>
            IServiceCollection Log4Net(IConfiguration configuration, string sectionName);
        }
    }
}
