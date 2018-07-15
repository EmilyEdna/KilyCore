using KilyCore.Cache;
using KilyCore.Cache.MongoCache;
using KilyCore.Cache.RedisCache;
using KilyCore.Configure;
using KilyCore.DataEntity.ResponseMapper.Enterprise;
using KilyCore.DataEntity.ResponseMapper.System;
using KilyCore.EntityFrameWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日12点01分
/// </summary>
namespace KilyCore.Repositories.BaseRepository
{
    public class Repository : IRepository
    {
        public ICache Cache = CacheFactory.Cache();
        public IMongoDbCache Caches = CacheFactory.Caches();
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
                if (UserInfo() != null)
                    props.Where(t => t.Name.Equals("DeleteUser")).FirstOrDefault().SetValue(Entity, UserInfo().Id.ToString());
                else if (CompanyInfo() != null)
                    props.Where(t => t.Name.Equals("DeleteUser")).FirstOrDefault().SetValue(Entity, CompanyInfo().Id.ToString());
                else if (CompanyUser() != null)
                    props.Where(t => t.Name.Equals("DeleteUser")).FirstOrDefault().SetValue(Entity, CompanyUser().CompanyId.ToString());
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
        public virtual bool Insert<TEntity>(TEntity Entity, bool PrimaryKey = true) where TEntity : class, new()
        {
            try
            {
                List<PropertyInfo> props = Entity.GetType().GetProperties().Where(t => t.Name.Contains("Create")).ToList();
                Entity.GetType().GetProperty("IsDelete").SetValue(Entity, false);
                if (PrimaryKey)
                    Entity.GetType().GetProperty("Id").SetValue(Entity, Guid.NewGuid());
                props.Where(t => t.Name.Equals("CreateTime")).FirstOrDefault().SetValue(Entity, DateTime.Now);
                if (UserInfo() != null)
                    props.Where(t => t.Name.Equals("CreateUser")).FirstOrDefault().SetValue(Entity, UserInfo().Id.ToString());
                else if (CompanyInfo() != null)
                    props.Where(t => t.Name.Equals("CreateUser")).FirstOrDefault().SetValue(Entity, CompanyInfo().Id.ToString());
                else if (CompanyUser() != null)
                    props.Where(t => t.Name.Equals("CreateUser")).FirstOrDefault().SetValue(Entity, CompanyUser().CompanyId.ToString());
                Kily.Entry<TEntity>(Entity).State = EntityState.Added;
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
        public virtual bool Update<TEntity, DEntity>(TEntity Entity, DEntity dto) where TEntity : class, new() where DEntity : class, new()
        {
            try
            {
                List<PropertyInfo> DtoProps = dto.GetType().GetProperties().ToList();
                List<PropertyInfo> EntityProps = Entity.GetType().GetProperties().ToList();
                List<PropertyInfo> EntityProp = EntityProps.Where(t => t.Name.Contains("Update")).ToList();
                EntityProp.Where(t => t.Name.Equals("UpdateTime")).FirstOrDefault().SetValue(Entity, DateTime.Now);
                if (UserInfo() != null)
                    EntityProp.Where(t => t.Name.Equals("UpdateUser")).FirstOrDefault().SetValue(Entity, UserInfo().Id.ToString());
                else if (CompanyInfo() != null)
                    EntityProp.Where(t => t.Name.Equals("UpdateUser")).FirstOrDefault().SetValue(Entity, CompanyInfo().Id.ToString());
                else if (CompanyUser() != null)
                    EntityProp.Where(t => t.Name.Equals("UpdateUser")).FirstOrDefault().SetValue(Entity, CompanyUser().CompanyId.ToString());
                foreach (var Prop in EntityProp)
                {
                    Kily.Entry<TEntity>(Entity).Property(Prop.Name).IsModified = true;//更新的时间和更新人
                }
                foreach (var Prop in DtoProps)
                {
                    if (!Prop.Name.ToUpper().Equals("Id".ToUpper()))//Id不更新
                    {
                        //判断实体中是否存在DTO中的字段
                        if (EntityProps.Select(t => t.Name.ToUpper()).Contains(Prop.Name.ToUpper()))
                            //需要更新的字段
                            Kily.Entry<TEntity>(Entity).Property(Prop.Name).IsModified = true;
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
        public virtual bool UpdateField<TEntity>(TEntity Entity, string Field, IList<string> Fields = null) where TEntity : class, new()
        {
            try
            {
                if (Fields != null && string.IsNullOrEmpty(Field))
                {
                    List<PropertyInfo> EntityProps = Entity.GetType().GetProperties().Where(t => t.Name.Contains("Update")).ToList();
                    EntityProps.Where(t => t.Name.Equals("UpdateTime")).FirstOrDefault().SetValue(Entity, DateTime.Now);
                    if (UserInfo() != null)
                        EntityProps.Where(t => t.Name.Equals("UpdateUser")).FirstOrDefault().SetValue(Entity, UserInfo().Id.ToString());
                    else if (CompanyInfo() != null)
                        EntityProps.Where(t => t.Name.Equals("UpdateUser")).FirstOrDefault().SetValue(Entity, CompanyInfo().Id.ToString());
                    else if (CompanyUser() != null)
                        EntityProps.Where(t => t.Name.Equals("UpdateUser")).FirstOrDefault().SetValue(Entity, CompanyUser().CompanyId.ToString());
                    foreach (var Prop in EntityProps)
                    {
                        Kily.Entry<TEntity>(Entity).Property(Prop.Name).IsModified = true;//更新的时间和更新人
                    }
                    foreach (var Prop in Fields)
                    {
                        Kily.Entry<TEntity>(Entity).Property(Prop).IsModified = true;
                    }
                    this.SaveChages();
                    return true;
                }
                else
                {
                    List<PropertyInfo> EntityProps = Entity.GetType().GetProperties().Where(t => t.Name.Contains("Update")).ToList();
                    EntityProps.Where(t => t.Name.Equals("UpdateTime")).FirstOrDefault().SetValue(Entity, DateTime.Now);
                    if (UserInfo() != null)
                        EntityProps.Where(t => t.Name.Equals("UpdateUser")).FirstOrDefault().SetValue(Entity, UserInfo().Id.ToString());
                    else if (CompanyInfo() != null)
                        EntityProps.Where(t => t.Name.Equals("UpdateUser")).FirstOrDefault().SetValue(Entity, CompanyInfo().Id.ToString());
                    else if (CompanyUser() != null)
                        EntityProps.Where(t => t.Name.Equals("UpdateUser")).FirstOrDefault().SetValue(Entity, CompanyUser().CompanyId.ToString());
                    foreach (var Prop in EntityProps)
                    {
                        Kily.Entry<TEntity>(Entity).Property(Prop.Name).IsModified = true;//更新的时间和更新人
                    }
                    Kily.Entry<TEntity>(Entity).Property(Field).IsModified = true;
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
            ResponseAdmin Data = Cache.GetCache<ResponseAdmin>(SystemInfoKey.PrivateKey);
            return Data == null ? null : (Data.TableName.Equals(typeof(ResponseAdmin).Name) ? Data : null);
        }
        /// <summary>
        /// 重缓存中获取登录的公司信息
        /// </summary>
        /// <returns></returns>
        public ResponseEnterprise CompanyInfo()
        {
            ResponseEnterprise Data = Cache.GetCache<ResponseEnterprise>(SystemInfoKey.PrivateKey);
            return Data == null ? null : (Data.TableName.Equals(typeof(ResponseEnterprise).Name) ? Data : null);
        }
        /// <summary>
        /// 重缓存中获取登录的公司子用户信息
        /// </summary>
        /// <returns></returns>
        public ResponseEnterpriseUser CompanyUser()
        {
            ResponseEnterpriseUser Data = Cache.GetCache<ResponseEnterpriseUser>(SystemInfoKey.PrivateKey);
            return Data == null ? null : (Data.TableName.Equals(typeof(ResponseEnterpriseUser).Name) ? Data : null);
        }
        /// <summary>
        /// 返回动态属性集合
        /// </summary>
        /// <typeparam name="DEntity">数据传输对象</typeparam>
        /// <typeparam name="TAttribute">特性标签</typeparam>
        /// <param name="entity"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public PropertyDescriptorCollection PropertyCollection<DEntity, TAttribute>(DEntity entity, object value) where DEntity : class, new() where TAttribute : Attribute
        {
            PropertyDescriptorCollection Props = TypeDescriptor.GetProperties(typeof(DEntity));
            PropertyInfo Prop = typeof(TAttribute).GetProperties().Where(t => t.CanWrite == true).FirstOrDefault();
            typeof(DEntity).GetProperties().ToList().ForEach(t =>
            {
                var Attr = t.GetCustomAttribute(typeof(TAttribute));
                if (Attr != null)
                {
                    var data = t.GetValue(entity, null);
                    if (!string.IsNullOrEmpty((string)data))
                    {
                        Prop.SetValue(Props[t.Name].Attributes[(typeof(TAttribute))], value);
                    }
                }
            });
            return Props;
        }
    }
}
