using System;
using Serilog;
using Serilog.Events;

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
                #region 写入文件

                /// <summary>
                /// 将日志写入文件
                /// </summary>
                /// <param name="logConfig">输出日志配置</param>
                /// <param name="logFileBytes">日志大小</param>
                /// <returns></returns>
                public static ILogger FileLogger(Logs.SerilogConfig logConfig, long logFileBytes = 21474836480)
                {
                    var logger = new LoggerConfiguration().MinimumLevel.Verbose();
                    if (!string.IsNullOrEmpty(logConfig.FilePath))
                    {
                        logger.WriteTo.RollingFile(logConfig.FilePath, fileSizeLimitBytes: logFileBytes, shared: true, flushToDiskInterval: TimeSpan.FromSeconds(1));
                    }
                    else
                    {
                        foreach (var fileConfig in logConfig.Files)
                        {
                            logger.WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == Utils.EnumParse<LogEventLevel>(fileConfig.Level.ToString())).WriteTo.RollingFile(fileConfig.FilePath, fileSizeLimitBytes: logFileBytes, shared: true, flushToDiskInterval: TimeSpan.FromSeconds(1)));
                        }
                    }
                    return logger.CreateLogger();
                }

                #endregion

                #region 写入数据库

                /// <summary>
                /// 将日志写入数据库
                /// </summary>
                public static class DataBaseLogger
                {
                    /// <summary>
                    /// 写入SqlServer
                    /// </summary>
                    /// <param name="logConfig">输出日志配置</param>
                    /// <returns></returns>
                    public static ILogger SqlServer(Logs.SerilogConfig logConfig)
                    {
                        var logger = new LoggerConfiguration().MinimumLevel.Verbose();
                        if (logConfig.DataBase != null)
                        {
                            logger.WriteTo.MSSqlServer(logConfig.DataBase.ConnectionString, logConfig.DataBase.Table, batchPostingLimit: logConfig.DataBase.Batch);
                        }
                        else
                        {
                            foreach (var dataBaseConfig in logConfig.DataBases)
                            {
                                logger.WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == Utils.EnumParse<LogEventLevel>(dataBaseConfig.Level.ToString())).WriteTo.MSSqlServer(dataBaseConfig.ConnectionString, dataBaseConfig.Table, batchPostingLimit: dataBaseConfig.Batch));
                            }
                        }
                        return logger.CreateLogger();
                    }
                }

                #endregion
            }
        }
    }
}
