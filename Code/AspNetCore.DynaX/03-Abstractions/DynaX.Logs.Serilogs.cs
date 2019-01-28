using System.Collections.Generic;

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
            /// Serilog AppSettings 配置
            /// </summary>
            public class SerilogConfig
            {
                /// <summary>
                /// 日志 Key
                /// </summary>
                public string Key { get; set; }
                /// <summary>
                /// 日志类型
                /// </summary>
                public Type Type { get; set; }
                /// <summary>
                /// 日志输出路径
                /// </summary>
                public string FilePath { get; set; }
                /// <summary>
                /// 日志输出集合
                /// </summary>
                public List<SerilogFileConfig> Files { get; set; } = new List<SerilogFileConfig>();
                /// <summary>
                /// 数据库类型
                /// </summary>
                public DataBaseType DbType { get; set; }
                /// <summary>
                /// 日志数据库配置
                /// </summary>
                public SerilogDataBaseConfig DataBase { get; set; }
                /// <summary>
                /// 日志数据库集合
                /// </summary>
                public List<SerilogDataBaseConfig> DataBases { get; set; } = new List<SerilogDataBaseConfig>();
            }

            /// <summary>
            /// Serilog AppSettings File 配置
            /// </summary>
            public class SerilogFileConfig
            {
                /// <summary>
                /// 日志等级
                /// </summary>
                public Level Level { get; set; }
                /// <summary>
                /// 日志输出路径
                /// </summary>
                public string FilePath { get; set; }
            }

            /// <summary>
            /// Serilog AppSettings DataBase 配置
            /// </summary>
            public class SerilogDataBaseConfig
            {
                /// <summary>
                /// 日志等级
                /// </summary>
                public Level Level { get; set; }
                /// <summary>
                /// 数据库连接字符串
                /// </summary>
                public string ConnectionString { get; set; }
                /// <summary>
                /// 数据库输出表
                /// </summary>
                public string Table { get; set; }

                /// <summary>
                /// 日志批量打包数量
                /// </summary>
                public int Batch { get; set; } = 50;
            }
        }
    }
}
