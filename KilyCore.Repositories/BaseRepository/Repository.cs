using KilyCore.Cache;
using KilyCore.Configure;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.EntityFrameWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace KilyCore.Repositories.BaseRepository
{
    public class Repository : IRepository
    {
        public ICache Cache = CacheFactory.Cache();
        public KilyContext Kily = KilyContextFactory.GetContext();

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public virtual bool Delete<TEntity>(Expression<Func<TEntity, bool>> exp) where TEntity : class, new()
        {
            try
            {
                TEntity Entity = Kily.Set<TEntity>().Where(exp).SingleOrDefault();
                List<PropertyInfo> props = Entity.GetType().GetProperties().Where(t => t.Name.Contains("Delete")).ToList();
                props.Where(t => t.Name.Equals("IsDelete")).FirstOrDefault().SetValue(Entity, true);
                props.Where(t => t.Name.Equals("DeleteTime")).FirstOrDefault().SetValue(Entity, DateTime.Now);
                props.Where(t => t.Name.Equals("DeleteUser")).FirstOrDefault().SetValue(Entity, UserInfo().Id.ToString());
                Kily.Entry<TEntity>(Entity).State = EntityState.Modified;
                this.SaveChages();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool Insert<TEntity>(TEntity entity) where TEntity : class, new()
        {
            try
            {
                List<PropertyInfo> props = entity.GetType().GetProperties().Where(t => t.Name.Contains("Create")).ToList();
                entity.GetType().GetProperty("IsDelete").SetValue(entity, false);
                entity.GetType().GetProperty("Id").SetValue(entity, Guid.NewGuid());
                props.Where(t => t.Name.Equals("CreateTime")).FirstOrDefault().SetValue(entity, DateTime.Now);
                props.Where(t => t.Name.Equals("CreateUser")).FirstOrDefault().SetValue(entity, UserInfo().Id.ToString());
                Kily.Entry<TEntity>(entity).State = EntityState.Added;
                this.SaveChages();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <typeparam name="TEntity">实体</typeparam>
        /// <typeparam name="DEntity">数据传输对象</typeparam>
        /// <param name="entity"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public virtual bool Update<TEntity, DEntity>(TEntity entity, DEntity dto) where TEntity : class, new() where DEntity : class, new()
        {
            try
            {
                List<PropertyInfo> DtoProps = dto.GetType().GetProperties().ToList();
                List<PropertyInfo> EntityProps = entity.GetType().GetProperties().ToList();
                List<PropertyInfo> EntityProp = EntityProps.Where(t => t.Name.Contains("Update")).ToList();
                EntityProp.Where(t => t.Name.Equals("UpdateTime")).FirstOrDefault().SetValue(entity, DateTime.Now);
                EntityProp.Where(t => t.Name.Equals("UpdateUser")).FirstOrDefault().SetValue(entity, UserInfo().Id.ToString());
                foreach (var Prop in EntityProp)
                {
                    Kily.Entry<TEntity>(entity).Property(Prop.Name).IsModified = true;//更新的时间和更新人
                }
                foreach (var Prop in DtoProps)
                {
                    if (!Prop.Name.ToUpper().Equals("Id".ToUpper()))//Id不更新
                    {
                        //判断实体中是否存在DTO中的字段
                        if (EntityProps.Select(t => t.Name.ToUpper()).Contains(Prop.Name.ToUpper()))
                            //需要更新的字段
                            Kily.Entry<TEntity>(entity).Property(Prop.Name).IsModified = true;
                    }
                }
                this.SaveChages();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        ///更新指定单个字段或者多个字段
        /// </summary>
        /// <typeparam name="TEntity">实体</typeparam>
        /// <param name="entity">实体</param>
        /// <param name="field">单个字段</param>
        /// <param name="fields">多个字段</param>
        /// <returns></returns>
        public virtual bool UpdateField<TEntity>(TEntity entity, string field, List<string> fields = null) where TEntity : class, new()
        {
            try
            {
                if (fields != null&&string.IsNullOrEmpty(field))
                {
                    List<PropertyInfo> EntityProps = entity.GetType().GetProperties().Where(t => t.Name.Contains("Update")).ToList();
                    EntityProps.Where(t => t.Name.Equals("UpdateTime")).FirstOrDefault().SetValue(entity, DateTime.Now);
                    EntityProps.Where(t => t.Name.Equals("UpdateUser")).FirstOrDefault().SetValue(entity, UserInfo().Id.ToString());
                    foreach (var Prop in EntityProps)
                    {
                        Kily.Entry<TEntity>(entity).Property(Prop.Name).IsModified = true;//更新的时间和更新人
                    }
                    foreach (var Prop in fields)
                    {
                        Kily.Entry<TEntity>(entity).Property(Prop).IsModified = true;
                    }
                    this.SaveChages();
                    return true;
                }
                else
                {
                    List<PropertyInfo> EntityProps = entity.GetType().GetProperties().Where(t => t.Name.Contains("Update")).ToList();
                    EntityProps.Where(t => t.Name.Equals("UpdateTime")).FirstOrDefault().SetValue(entity, DateTime.Now);
                    EntityProps.Where(t => t.Name.Equals("UpdateUser")).FirstOrDefault().SetValue(entity, UserInfo().Id.ToString());
                    foreach (var Prop in EntityProps)
                    {
                        Kily.Entry<TEntity>(entity).Property(Prop.Name).IsModified = true;//更新的时间和更新人
                    }
                    Kily.Entry<TEntity>(entity).Property(field).IsModified = true;
                    this.SaveChages();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 真删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="exp"></param>
        /// <returns></returns>
        public virtual bool Remove<TEntity>(Expression<Func<TEntity, bool>> exp) where TEntity : class, new()
        {
            try
            {
                Kily.Set<TEntity>().Where(exp).ToList().ForEach(t =>
                {
                    Kily.Set<TEntity>().Remove(t);
                });
                this.SaveChages();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// SQL查询
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="SQL"></param>
        public IQueryable<TEntity> ExcuteSQL<TEntity>(string SQL) where TEntity : class, new()
        {
            try
            {
                return Kily.Set<TEntity>().FromSql(SQL).AsNoTracking().AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public int ExcuteSQL(string SQL)
        {
            try
            {
                return Kily.Database.ExecuteSqlCommand(SQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 提交
        /// </summary>
        /// <returns></returns>
        public virtual int SaveChages()
        {
            return Kily.SaveChanges();
        }
        /// <summary>
        /// 缓存中取登陆信息
        /// </summary>
        /// <returns></returns>
        public ResponseAdmin UserInfo()
        {
            return Cache.GetCache<ResponseAdmin>(Configer.ClientIP);
        }
    }
}
