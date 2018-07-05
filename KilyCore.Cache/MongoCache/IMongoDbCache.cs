using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
namespace KilyCore.Cache.MongoCache
{
    public interface IMongoDbCache
    {
        /// <summary>
        /// 新增单个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        void Insert<T>(T t);
        /// <summary>
        /// 批量新增
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        void InsertMany<T>(IList<T> t);
        /// <summary>
        /// 查询单个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        T Search<T>(Expression<Func<T, bool>> filter);
        /// <summary>
        /// 查询集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        IList<T> SearchMany<T>(Expression<Func<T, bool>> filter);
        /// <summary>
        /// 更新单个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="name"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        int Update<T>(Expression<Func<T, bool>> filter, string name, string param);
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        int UpdateMany<T>(Expression<Func<T, bool>> filter, T t);
        /// <summary>
        /// 删除单条计量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        int Delete<T>(Expression<Func<T, bool>> filter);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        IMongoQueryable<T> GetPage<T>(int PageIndex, int PageSize);
    }
}
