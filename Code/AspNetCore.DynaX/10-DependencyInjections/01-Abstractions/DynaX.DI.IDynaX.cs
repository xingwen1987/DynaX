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
        public interface IDynaX
        {
            /// <summary>
            /// DynaX 日志组件注入服务
            /// </summary>
            IDynaXLogs Logs { get; }
        }
    }
}
