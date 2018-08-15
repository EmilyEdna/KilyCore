using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点51分
/// </summary>
namespace KilyCore.Repositories.BaseRepository
{
    public interface IRepository
    {
        bool Insert<TEntity>(TEntity Entity,bool PrimaryKey=true) where TEntity : class, new();
        bool Update<TEntity, DEntity>(TEntity Entity, DEntity dto) where TEntity : class, new() where DEntity : class, new();
        bool UpdateField<TEntity>(TEntity Entity, string Field, IList<string> Fields = null) where TEntity : class, new();
        bool Delete<TEntity>(Expression<Func<TEntity, bool>> exp) where TEntity : class, new();
        bool Remove<TEntity>(Expression<Func<TEntity, bool>> exp) where TEntity : class, new();
        IQueryable<TEntity> ExcuteSQL<TEntity> (string SQL) where TEntity : class, new();
        int ExcuteSQL(string SQL);
        String RemovePath<TEntity>(TEntity Entity) where TEntity : class, new();
    }
}
