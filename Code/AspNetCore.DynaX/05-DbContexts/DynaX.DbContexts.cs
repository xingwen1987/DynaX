using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.DynaX
{
    /// <summary>
    /// DynaX 工具集合
    /// </summary>
    public static partial class DynaX
    {
        /// <summary>
        /// DynaX Bases 动态数据容器
        /// </summary>
        public static partial class DbContexts
        {
            /// <summary>
            /// 数据库动态连接字符串
            /// </summary>
            /// <param name="dataBaseInfo">数据库信息</param>
            /// <returns></returns>
            private static string CreateConnectionString(DataBaseInfo dataBaseInfo)
            {
                if (!dataBaseInfo.ConnnectionString.IsNullOrEmpty()) return dataBaseInfo.ConnnectionString;
                if (dataBaseInfo.DataSource.IsNullOrEmpty()) throw new ArgumentNullException("请传入数据库的【数据库地址】信息。");
                if (dataBaseInfo.Catalog.IsNullOrEmpty()) throw new ArgumentNullException("请传入数据库的【数据库对象】信息。");
                if (dataBaseInfo.UserId.IsNullOrEmpty()) throw new ArgumentNullException("请传入数据库的【数据库登录名】信息。");
                if (dataBaseInfo.UserPassword.IsNullOrEmpty()) throw new ArgumentNullException("请传入数据库的【数据库登录密码】信息。");
                if(dataBaseInfo.Type == DataBaseType.Oracle && dataBaseInfo.OracleServiceName.IsNullOrEmpty()) throw new ArgumentNullException("请传入 Oracle 数据库的【服务名称】信息。");
                var dbPort = dataBaseInfo.Port;
                switch (dataBaseInfo.Type)
                {
                    case DataBaseType.SqlServer:
                        dbPort = dbPort.IsNullOrEmpty() ? "1433" : dbPort;
                        return $"Data Source={dataBaseInfo.DataSource + "," + dbPort};Initial Catalog={dataBaseInfo.Catalog};User Id={dataBaseInfo.UserId};Password={dataBaseInfo.UserPassword};";
                    case DataBaseType.MySql:
                        dbPort = dbPort.IsNullOrEmpty() ? "3306" : dbPort;
                        return $"Data Source={dataBaseInfo.DataSource};port={dbPort};Initial Catalog={dataBaseInfo.Catalog};User Id={dataBaseInfo.UserId};Password={dataBaseInfo.UserPassword};pooling=false;CharSet=utf8;";
                    case DataBaseType.Oracle:
                        dbPort = dbPort.IsNullOrEmpty() ? "1521" : dbPort;
                        return $"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={dataBaseInfo.DataSource})(PORT={dbPort})))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={dataBaseInfo.OracleServiceName})));User ID={dataBaseInfo.UserId};Password={dataBaseInfo.UserPassword};";
                    default: return null;
                }
            }

            /// <summary>
            /// 创建对应的DbContext
            /// </summary>
            /// <param name="dataBaseType">数据库类型</param>
            /// <param name="connectionString">连接字符串</param>
            /// <param name="checkDatabase">检查数据库</param>
            /// <returns></returns>
            public static async Task<T> CreateDbContext<T>(DataBaseType dataBaseType, string connectionString, bool checkDatabase) where T : DbContext
            {
                if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException("数据库连接字符串不能为 Null。");
                var dataBaseInfo = new DataBaseInfo { Type = dataBaseType, ConnnectionString = connectionString };
                return await CreateDbContext<T>(dataBaseInfo, checkDatabase);
            }

            /// <summary>
            /// 创建对应的DbContext
            /// </summary>
            /// <param name="dataBaseInfo">数据库信息</param>
            /// <param name="checkDatabase">检查数据库</param>
            /// <returns></returns>
            public static async Task<T> CreateDbContext<T>(DataBaseInfo dataBaseInfo, bool checkDatabase) where T : DbContext
            {
                var dbConnectionString = CreateConnectionString(dataBaseInfo);
                var optionsBuilder = new DbContextOptionsBuilder<T>();
                switch (dataBaseInfo.Type)
                {
                    case DataBaseType.SqlServer: optionsBuilder.UseSqlServer(dbConnectionString); break;
                    case DataBaseType.MySql: optionsBuilder.UseMySql(dbConnectionString); break;
                    case DataBaseType.Oracle: optionsBuilder.UseOracle(dbConnectionString); break;
                    case DataBaseType.MongoDb:
                    case DataBaseType.PostgreSql:
                    default: break;
                }
                var dbContext = Utils.CreateInstance<T>(optionsBuilder.Options);
                if (checkDatabase)
                {
                    await dbContext.Database.EnsureCreatedAsync();
                }
                dbContext.Database.SetCommandTimeout(0);
                return dbContext;
            }
        }
    }
}
