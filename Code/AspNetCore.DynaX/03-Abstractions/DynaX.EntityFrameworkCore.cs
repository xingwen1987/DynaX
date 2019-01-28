using System;

namespace AspNetCore.DynaX
{
    /// <summary>
    /// DynaX 工具集合
    /// </summary>
    public static partial class DynaX
    {
        /// <summary>
        /// DynaX Abstractions DataBaseInfo 数据库信息抽象
        /// </summary>
        public partial class DataBaseInfo
        {
            /// <summary>
            /// 数据库类型
            /// </summary>
            public DataBaseType Type { get; set; }

            /// <summary>
            /// 数据库连接字符串
            /// </summary>
            public string ConnnectionString { get; set; }

            /// <summary>
            /// 数据库地址
            /// </summary>
            public string DataSource { get; set; }

            /// <summary>
            /// 数据库端口
            /// </summary>
            public string Port { get; set; }

            /// <summary>
            /// 数据库名称
            /// </summary>
            public string Catalog { get; set; }

            /// <summary>
            /// 用户名
            /// </summary>
            public string UserId { get; set; }

            /// <summary>
            /// 密码
            /// </summary>
            public string UserPassword { get; set; }

            /// <summary>
            /// Oracle 服务名称
            /// </summary>
            public string OracleServiceName { get; set; }
        }

        /// <summary>
        /// DynaX Abstractions CommitResult 提交信息抽象
        /// </summary>
        public partial class CommitResult
        {
            /// <summary>
            /// 处理数据条数
            /// </summary>
            public int ExecuteNum { get; set; }

            /// <summary>
            /// 处理状态
            /// </summary>
            public bool State { get; set; }

            /// <summary>
            /// 处理异常
            /// </summary>
            public Exception Exception { get; set; }
        }
    }
}
