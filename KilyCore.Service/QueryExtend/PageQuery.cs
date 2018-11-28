using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日12点01分
/// </summary>
namespace KilyCore.Service.QueryExtend
{
    /// <summary>
    /// 分页查询
    /// </summary>
    public static class PageQuery
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PagedResult<T> ToPagedResult<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            PagedResult<T> pagedResult = new PagedResult<T>();
            pagedResult.Index = pageIndex;
            pagedResult.PageSize = pageSize;
            pagedResult.Total = query.Count();
            if (pagedResult.Total != 0)
                pagedResult.List.AddRange(query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
            return pagedResult;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static PagedResult<T> ToPagedResult<T>(this IList<T> query, int pageIndex, int pageSize) {
            PagedResult<T> pagedResult = new PagedResult<T>();
            pagedResult.Index = pageIndex;
            pagedResult.PageSize = pageSize;
            pagedResult.Total = query.Count();
            if (pagedResult.Total != 0)
                pagedResult.List.AddRange(query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
            return pagedResult;
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortName"></param>
        /// <param name="sortDirection"></param>
        /// <returns></returns>
        public static PagedResult<T> ToPagedResult<T>(this IQueryable<T> query, int pageIndex, int pageSize, string sortName, string sortDirection)
        {

            PagedResult<T> pagedResult = new PagedResult<T>();
            pagedResult.Index = pageIndex;
            pagedResult.PageSize = pageSize;
            pagedResult.Total = query.Count();
            if (pagedResult.Total != 0)
            {
                string sortingDir = string.Empty;
                if (sortDirection.ToUpper().Trim() == "ASC")
                    sortingDir = "OrderBy";
                else if (sortDirection.ToUpper().Trim() == "DESC")
                    sortingDir = "OrderByDescending";

                ParameterExpression param = Expression.Parameter(typeof(T), sortName);
                PropertyInfo pi = typeof(T).GetProperty(sortName);
                Type[] types = new Type[2];
                types[0] = typeof(T);
                types[1] = pi.PropertyType;

                Expression expr = Expression.Call(typeof(Queryable), sortingDir, types, query.Expression, Expression.Lambda(Expression.Property(param, sortName), param));

                pagedResult.List.AddRange(query.Provider.CreateQuery<T>(expr).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
            }

            return pagedResult;
        }
    }
}
