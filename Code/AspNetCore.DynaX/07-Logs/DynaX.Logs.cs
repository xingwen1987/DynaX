namespace AspNetCore.DynaX
{
    /// <summary>
    /// DynaX 工具集合
    /// </summary>
    public static partial class DynaX
    {
        /// <summary>
        /// DynaX Logs 日志集合
        /// </summary>
        public static partial class Logs
        {
            /// <summary>
            /// 日志组件
            /// </summary>
            public enum Plugin
            {
                Serilog = 1,
                NLog = 2,
                Log4Net = 3
            }

        }
    }
}


