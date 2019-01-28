using System;
using System.Collections.Generic;
using Serilog;

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
            /// DynaX Logs Serilogs 日志集合
            /// </summary>
            public static partial class Serilogs
            {
                /// <summary>
                /// 日志对象集合
                /// </summary>
                private static readonly Dictionary<string, ILogger> Loggers = new Dictionary<string, ILogger>();

                /// <summary>
                /// 日志初始化
                /// </summary>
                /// <param name="logConfigs">日志配置集合</param>
                public static void Setup(List<Logs.SerilogConfig> logConfigs)
                {
                    foreach (var logConfig in logConfigs)
                    {
                        if (logConfig.Type == Logs.Type.File)
                        {
                            Loggers.Add(logConfig.Key, FileLogger(logConfig));
                        }
                        else
                        {
                            if (logConfig.DbType == DataBaseType.SqlServer)
                            {
                                Loggers.Add(logConfig.Key, DataBaseLogger.SqlServer(logConfig));
                            }
                        }
                    }
                }

                /// <summary>
                /// 获取日志对象
                /// </summary>
                /// <param name="logKey">日志Key</param>
                /// <returns></returns>
                public static ILogger GetLogger(string logKey)
                {
                    if (string.IsNullOrEmpty(logKey)) throw new ArgumentNullException($"请输入需要调取的日志对象Key。");
                    if (!Loggers.ContainsKey(logKey)) throw new NullReferenceException("没有找到所需的日志对象Key。");
                    return Loggers[logKey];
                }
            }
        }
    }
}
