using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace AspNetCore.DynaX
{
    /// <summary>
    /// DynaX 工具集合
    /// </summary>
    public static partial class DynaX
    {
        public class RepositoryX<TEntity> : IRepositoryX<TEntity> where TEntity : class
        {
            protected readonly DbContext DbContext;
            protected readonly Repository<TEntity> DbRepository;

            public RepositoryX(DbContext dbContext)
            {
                DbContext = dbContext;
                DbRepository = new Repository<TEntity>(DbContext);
            }

            #region Insert

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            public void Insert(TEntity entity)
            {
                DbRepository.Insert(entity);
            }

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            [Obsolete("此方法性能较弱！不考虑事务，建议使用 BulkInsert 方法，可以高效、快速提交批量信息。")]
            public void Insert(params TEntity[] entityArray)
            {
                DbRepository.Insert(entityArray);
            }

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            [Obsolete("此方法性能较弱！不考虑事务，建议使用 BulkInsert 方法，可以高效、快速提交批量信息。")]
            public void Insert(IEnumerable<TEntity> entityList)
            {
                DbRepository.Insert(entityList);
            }

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            public async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = new CancellationToken())
            {
                await DbRepository.InsertAsync(entity, cancellationToken);
            }

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            [Obsolete("此方法性能较弱！不考虑事务，建议使用 BulkInsertAsync 方法，可以高效、快速提交批量信息。")]
            public async Task InsertAsync(params TEntity[] entityArray)
            {
                await DbRepository.InsertAsync(entityArray);
            }

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            [Obsolete("此方法性能较弱！不考虑事务，建议使用 BulkInsertAsync 方法，可以高效、快速提交批量信息。")]
            public async Task InsertAsync(IEnumerable<TEntity> entityList, CancellationToken cancellationToken = new CancellationToken())
            {
                await DbRepository.InsertAsync(entityList, cancellationToken);
            }

            /// <summary>
            /// 添加批量数据
            /// </summary>
            /// <param name="entityList">实体集合</param>
            public void BulkInsert(IList<TEntity> entityList)
            {
                DbContext.BulkInsert(entityList);
            }

            /// <summary>
            /// 异步添加批量数据
            /// </summary>
            /// <param name="entityList">实体集合</param>
            public async Task BulkInsertAsync(IList<TEntity> entityList)
            {
                await DbContext.BulkInsertAsync(entityList);
            }

            #endregion

            #region Update

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            public void Update(TEntity entity)
            {
                DbRepository.Update(entity);
            }

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            public void Update(params TEntity[] entityArray)
            {
                DbRepository.Update(entityArray);
            }

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            public void Update(IEnumerable<TEntity> entityList)
            {
                DbRepository.Update(entityList);
            }

            /// <summary>
            /// 更新数据
            /// </summary>
            /// <param name="whereFilter">更新条件</param>
            /// <param name="action">更新字段</param>
            public void Update(Expression<Func<TEntity, bool>> whereFilter, Action<TEntity> action)
            {
                if (whereFilter == null)
                {
                    throw new ArgumentNullException("whereFilter");
                }
                if (action == null)
                {
                    throw new ArgumentNullException("action");
                }
                DbContext.Set<TEntity>().Where(whereFilter).ToList().ForEach(action);
            }

            #endregion

            #region Delete

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            public void Delete(object id)
            {
                DbRepository.Delete(id);
            }

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            public void Delete(TEntity entity)
            {
                DbRepository.Delete(entity);
            }

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            public void Delete(params TEntity[] entityArray)
            {
                DbRepository.Delete(entityArray);
            }

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            public void Delete(IEnumerable<TEntity> entityList)
            {
                DbRepository.Delete(entityList);
            }

            /// <summary>
            /// 删除数据
            /// </summary>
            /// <param name="whereFilter">删除条件</param>
            public void Delete(Expression<Func<TEntity, bool>> whereFilter)
            {
                DbContext.Set<TEntity>().RemoveRange(DbContext.Set<TEntity>().Where(whereFilter));
            }

            #endregion

            #region Read

            #region FirstOrDefault

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true)
            {
                return DbRepository.GetFirstOrDefault(predicate, orderBy, include, disableTracking);
            }

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true)
            {
                return await DbRepository.GetFirstOrDefaultAsync(predicate, orderBy, include, disableTracking);
            }

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            public TResult GetFirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true)
            {
                return DbRepository.GetFirstOrDefault<TResult>(selector, predicate, orderBy, include, disableTracking);
            }

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            public async Task<TResult> GetFirstOrDefaultAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true)
            {
                return await DbRepository.GetFirstOrDefaultAsync<TResult>(selector, predicate, orderBy, include, disableTracking);
            }

            #endregion

            #region Find

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            public TEntity Find(params object[] keyValues)
            {
                return DbRepository.Find(keyValues);
            }

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            public async Task<TEntity> FindAsync(params object[] keyValues)
            {
                return await DbRepository.FindAsync(keyValues);
            }

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            public async Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken)
            {
                return await DbRepository.FindAsync(keyValues, cancellationToken);
            }

            #endregion

            #region GetPagedList

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            public IPagedList<TEntity> GetPagedList(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int pageIndex = 0, int pageSize = 20, bool disableTracking = true)
            {
                return DbRepository.GetPagedList(predicate, orderBy, include, pageIndex, pageSize, disableTracking);
            }

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            public async Task<IPagedList<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int pageIndex = 0, int pageSize = 20, bool disableTracking = true, CancellationToken cancellationToken = new CancellationToken())
            {
                return await DbRepository.GetPagedListAsync(predicate, orderBy, include, pageIndex, pageSize, disableTracking, cancellationToken);
            }

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            public IPagedList<TResult> GetPagedList<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int pageIndex = 0, int pageSize = 20, bool disableTracking = true) where TResult : class
            {
                return DbRepository.GetPagedList(selector, predicate, orderBy, include, pageIndex, pageSize, disableTracking);
            }

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            public async Task<IPagedList<TResult>> GetPagedListAsync<TResult>(Expression<Func<TEntity, TResult>> selector, Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, int pageIndex = 0, int pageSize = 20, bool disableTracking = true, CancellationToken cancellationToken = new CancellationToken()) where TResult : class
            {
                return await DbRepository.GetPagedListAsync(selector, predicate, orderBy, include, pageIndex, pageSize, disableTracking, cancellationToken);
            }

            #endregion

            #region GetAll

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            public IQueryable<TEntity> GetAll()
            {
                return DbContext.Set<TEntity>();
            }

            #endregion

            #region Count

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            public int Count(Expression<Func<TEntity, bool>> predicate = null)
            {
                return DbRepository.Count(predicate);
            }

            #endregion

            #region Tools

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            public void ChangeTable(string tableName)
            {
                DbRepository.ChangeTable(tableName);
            }

            /// <summary>
            /// 第三方组件封装
            /// </summary>
            public IQueryable<TEntity> FromSql(string sql, params object[] parameters)
            {
                return DbRepository.FromSql(sql, parameters);
            }

            #endregion

            #endregion
        }
    }
}
