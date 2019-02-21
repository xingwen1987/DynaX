using Microsoft.AspNetCore.Builder;

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
        public interface IAppDynaX
        {
            /// <summary>
            /// DynaX Web 组件服务
            /// </summary>
            IApplicationBuilder Webs();
        }
    }
}
