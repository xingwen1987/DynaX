using System.Threading;
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
        /// DynaX Bases UnitOfWorkEx 工作单元扩展
        /// </summary>
        public interface IUnitOfWorkX<TContext> : IUnitOfWork<TContext> where TContext : DbContext
        {
            /// <summary>
            /// 修改数据库对象
            /// </summary>
            /// <param name="dataBaseType">数据库类型</param>
            /// <param name="connectionObject">连接字符串对象</param>
            /// <param name="checkDatabase">检查数据库</param>
            UnitOfWorkX<TContext> ChangeDataBase(DataBaseType dataBaseType, string connectionObject, bool checkDatabase = true);

            /// <summary>
            /// 修改数据库对象
            /// </summary>
            /// <param name="dataBaseInfo">数据库信息</param>
            /// <param name="checkDatabase">检查数据库</param>
            UnitOfWorkX<TContext> ChangeDataBase(DynaX.DataBaseInfo dataBaseInfo, bool checkDatabase = true);

            /// <summary>
            /// 获取当前数据库下的仓储
            /// </summary>
            /// <typeparam name="TEntity">仓储类型</typeparam>
            /// <returns></returns>
            IRepositoryX<TEntity> Repository<TEntity>() where TEntity : class;

            /// <summary>
            /// 保存事务数据修改
            /// </summary>
            CommitResult Commit();

            /// <summary>
            /// 异步保存事务数据修改
            /// </summary>
            Task<CommitResult> CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
        }
    }
}
