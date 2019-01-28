using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.DynaX
{
    /// <summary>
    /// DynaX 工具集合
    /// </summary>
    public static partial class DynaX
    {
        /// <summary>
        /// 添加 DynaX 服务注入
        /// </summary>
        /// <param name="services">系统服务集合</param>
        /// <returns></returns>
        public static IDynaX AddDynaX(this IServiceCollection services)
        {
            return new DiDynaX(services);
        }
    }
}
