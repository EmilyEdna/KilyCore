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
        bool Update<TEntity, DEntity>(TEntity entity, DEntity dto, PropertyDescriptorCollection collection = null) where TEntity : class, new() where DEntity : class, new();
        bool UpdateField<TEntity>(TEntity entity, string field, IList<String> fields = null) where TEntity : class, new();
        bool Delete<TEntity>(Expression<Func<TEntity, bool>> exp) where TEntity : class, new();
        bool Remove<TEntity>(Expression<Func<TEntity, bool>> exp) where TEntity : class, new();
        IQueryable<TEntity> ExcuteSQL<TEntity> (string SQL) where TEntity : class, new();
        int ExcuteSQL(string SQL);
        PropertyDescriptorCollection PropertyCollection<DEntity, TAttribute>(DEntity entity, Object value) where DEntity : class, new() where TAttribute : Attribute;
    }
}
