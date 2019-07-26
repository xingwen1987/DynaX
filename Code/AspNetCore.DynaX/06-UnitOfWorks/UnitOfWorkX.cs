using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

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
        public class UnitOfWorkX<TContext> : IUnitOfWorkX<TContext> where TContext : DbContext
        {
            private readonly IDbContextTransaction _dbContextTransaction;
            private readonly IUnitOfWork<TContext> _dbUnitOfWork;
            private Dictionary<Type, object> _repositoryDir;
            private readonly TContext _dbContext;

            public UnitOfWorkX(DataBaseType dataBaseType, string connectionString, bool checkDatabase = true)
            {
                _dbContext = DbContexts.CreateDbContext<TContext>(GetDataBaseInfo(dataBaseType, connectionString), checkDatabase).Result;
                _dbUnitOfWork = new UnitOfWork<TContext>(_dbContext);
                _dbContextTransaction = _dbContext.Database.BeginTransaction();
            }

            public UnitOfWorkX(DataBaseInfo dataBaseInfo, bool checkDatabase = true)
            {
                _dbContext = DbContexts.CreateDbContext<TContext>(dataBaseInfo, checkDatabase).Result;
                _dbUnitOfWork = new UnitOfWork<TContext>(_dbContext);
                _dbContextTransaction = _dbContext.Database.BeginTransaction();
            }

            /// <inheritdoc />
            /// <summary>
            /// 返回 DbContext
            /// </summary>
            public TContext DbContext => _dbContext;

            #region 动态创建数据库对象

            /// <summary>
            /// 修改数据库对象
            /// </summary>
            /// <param name="connectionString">连接字符串</param>
            public void ChangeDatabase(string connectionString)
            {
                ChangeDataBase(DataBaseType.SqlServer, connectionString);
            }

            /// <summary>
            /// 修改数据库对象
            /// </summary>
            /// <param name="dataBaseType">数据库类型</param>
            /// <param name="connectionString">连接字符串</param>
            public UnitOfWorkX<TContext> ChangeDataBase(DataBaseType dataBaseType, string connectionString, bool checkDatabase = true)
            {
                return ChangeDataBase(GetDataBaseInfo(dataBaseType, connectionString), checkDatabase);
            }

            /// <summary>
            /// 修改数据库对象
            /// </summary>
            /// <param name="dataBaseInfo">数据库信息</param>
            public UnitOfWorkX<TContext> ChangeDataBase(DataBaseInfo dataBaseInfo, bool checkDatabase = true)
            {
                return new UnitOfWorkX<TContext>(dataBaseInfo, checkDatabase);
            }

            #endregion

            #region 获取当前数据库下的仓储

            /// <summary>
            /// 获取当前数据库下的仓储
            /// </summary>
            /// <typeparam name="TEntity">仓储类型</typeparam>
            /// <returns></returns>
            public IRepositoryX<TEntity> Repository<TEntity>() where TEntity : class
            {
                if (_repositoryDir == null) { _repositoryDir = new Dictionary<Type, object>(); }
                var type = typeof(TEntity);
                if (!_repositoryDir.ContainsKey(type))
                {
                    _repositoryDir[type] = new RepositoryX<TEntity>(DbContext);
                }
                return (IRepositoryX<TEntity>)_repositoryDir[type];
            }

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            [Obsolete("此方法已过期！建议使用 Repository 方法获取仓储信息。")]
            public IRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class
            {
                return new Repository<TEntity>(DbContext);
            }

            #endregion

            #region 提交数据修改

            /// <summary>
            /// 提交数据修改
            /// </summary>
            public CommitResult Commit()
            {
                var commitResult = new CommitResult();
                try
                {
                    commitResult.ExecuteNum = SaveChange();
                    commitResult.State = true;
                }
                catch (Exception ex)
                {
                    commitResult.State = false;
                    commitResult.Exception = ex;
                }
                return commitResult;
            }

            /// <summary>
            /// 异步提交数据修改
            /// </summary>
            public async Task<CommitResult> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
            {
                var commitResult = new CommitResult();
                try
                {
                    commitResult.ExecuteNum = await SaveChangeAsync(cancellationToken);
                    commitResult.State = true;
                }
                catch (Exception ex)
                {
                    commitResult.State = false;
                    commitResult.Exception = ex;
                }
                return commitResult;
            }

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            [Obsolete("此方法已过期！建议使用 Commit 方法，可以获取更多提交数据修改时的信息。")]
            public int SaveChanges(bool ensureAutoHistory = false)
            {
                return _dbUnitOfWork.SaveChanges(ensureAutoHistory);
            }

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            [Obsolete("此方法已过期！建议使用 CommitAsync 方法，可以获取更多提交数据修改时的信息。")]
            public async Task<int> SaveChangesAsync(bool ensureAutoHistory = false)
            {
                return await _dbUnitOfWork.SaveChangesAsync(ensureAutoHistory);
            }

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            [Obsolete("此方法已过期！建议使用 CommitAsync 方法，可以获取更多提交数据修改时的信息。")]
            public async Task<int> SaveChangesAsync(bool ensureAutoHistory = false, params IUnitOfWork[] unitOfWorks)
            {
                return await _dbUnitOfWork.SaveChangesAsync(ensureAutoHistory, unitOfWorks);
            }

            #endregion

            #region Tools

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            public int ExecuteSqlCommand(string sql, params object[] parameters)
            {
                return _dbUnitOfWork.ExecuteSqlCommand(sql, parameters);
            }

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            public IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class
            {
                return _dbUnitOfWork.FromSql<TEntity>(sql, parameters);
            }

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            public void TrackGraph(object rootEntity, Action<EntityEntryGraphNode> callback)
            {
               _dbUnitOfWork.TrackGraph(rootEntity, callback);
            }

            #endregion

            #region 释放资源

            public void Dispose()
            {
                _dbUnitOfWork.Dispose();
            }

            #endregion

            #region 内部方法

            /// <summary>
            /// EFCore 提交
            /// </summary>
            private int SaveChange()
            {
                int result;
                try
                {
                    result = _dbUnitOfWork.SaveChanges();
                    _dbContextTransaction?.Commit();
                }
                catch
                {
                    _dbContextTransaction?.Rollback();
                    throw;
                }
                finally
                {
                    _dbContextTransaction?.Dispose();
                    _dbUnitOfWork.Dispose();
                }
                return result;
            }

            /// <summary>
            /// EFCore 异步提交
            /// </summary>
            private async Task<int> SaveChangeAsync(CancellationToken cancellationToken = default(CancellationToken))
            {
                int result;
                try
                {
                    result = await _dbUnitOfWork.SaveChangesAsync();
                    _dbContextTransaction?.Commit();
                }
                catch
                {
                    _dbContextTransaction?.Rollback();
                    throw;
                }
                finally
                {
                    _dbContextTransaction?.Dispose();
                    _dbUnitOfWork.Dispose();
                }
                return result;
            }

            /// <summary>
            /// 返回数据库信息
            /// </summary>
            /// <param name="dataBaseType">数据库类型</param>
            /// <param name="connectionString">连接字符串</param>
            private static DataBaseInfo GetDataBaseInfo(DataBaseType dataBaseType, string connectionString)
            {
                return new DataBaseInfo { Type = dataBaseType, ConnnectionString = connectionString };
            }

            #endregion
        }
    }
}
