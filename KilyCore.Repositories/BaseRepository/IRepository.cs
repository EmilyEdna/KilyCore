using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace KilyCore.Repositories.BaseRepository
{
    public interface IRepository
    {
        bool Insert<TEntity>(TEntity entity) where TEntity : class, new();
        bool Update<TEntity, DEntity>(TEntity entity, DEntity dto) where TEntity : class, new() where DEntity : class, new();
        bool UpdateField<TEntity>(TEntity entity, string field, IList<string> fields = null) where TEntity : class, new();
        bool Delete<TEntity>(Expression<Func<TEntity, bool>> exp) where TEntity : class, new();
        bool Remove<TEntity>(Expression<Func<TEntity, bool>> exp) where TEntity : class, new();
        IQueryable<TEntity> ExcuteSQL<TEntity> (string SQL) where TEntity : class, new();
        int ExcuteSQL(string SQL);
    }
}
