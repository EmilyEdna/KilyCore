using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace KilyCore.Cache.MongoCache
{
    public interface IMongoDbCache
    {
        void Insert<T>(T t);
        void InsertMany<T>(IList<T> t);
        T Search<T>(Expression<Func<T, bool>> filter);
        IList<T> SearchMany<T>(Expression<Func<T, bool>> filter);
        int Update<T>(Expression<Func<T, bool>> filter, string name, string param);
        int UpdateMany<T>(Expression<Func<T, bool>> filter, T t);
        int Delete<T>(Expression<Func<T, bool>> filter);
        IMongoQueryable<T> GetPage<T>(int PageIndex, int PageSize);
    }
}
