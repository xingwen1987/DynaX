using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        public interface IRepositoryX<TEntity> : IRepository<TEntity> where TEntity : class
        {
            /// <summary>
            /// 批量添加数据
            /// </summary>
            /// <param name="entityList">实体集合</param>
            void BulkInsert(IList<TEntity> entityList);

            /// <summary>
            /// 批量添加数据
            /// </summary>
            /// <param name="entityList">实体集合</param>
            Task BulkInsertAsync(IList<TEntity> entityList);

            /// <summary>
            /// 更新数据
            /// </summary>
            /// <param name="whereFilter">更新条件</param>
            /// <param name="action">更新字段</param>
            void Update(Expression<Func<TEntity, bool>> whereFilter, Action<TEntity> action);

            /// <summary>
            /// 删除数据
            /// </summary>
            /// <param name="whereFilter">删除条件</param>
            void Delete(Expression<Func<TEntity, bool>> whereFilter);
        }
    }
}
