namespace AspNetCore.DynaX
{
    /// <summary>
    /// DynaX 工具集合
    /// </summary>
    public static partial class DynaX
    {
        /// <summary>
        /// DynaX Abstractions Logs 日志抽象集合
        /// </summary>
        public partial class Logs
        {
            /// <summary>
            /// 日志类型
            /// </summary>
            public enum Type
            {
                File = 1,
                DataBase = 2
            }

            /// <summary>
            /// 日志级别
            /// </summary>
            public enum Level
            {
                Debug = 1,
                Information = 2,
                Warning = 3,
                Error = 4,
                Fatal = 5,
                Verbose = 6
            }
        }
    }
}
