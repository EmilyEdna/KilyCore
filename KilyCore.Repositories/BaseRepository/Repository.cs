using KilyCore.Cache;
using KilyCore.Cache.MongoCache;
using KilyCore.Cache.RedisCache;
using KilyCore.Configure;
using KilyCore.DataEntity.ResponseMapper.Repast;
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
using KilyCore.Extension.HttpClientFactory;
using KilyCore.DataEntity.ResponseMapper.Cook;
using KilyCore.DataEntity.ResponseMapper.Govt;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;
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
        public virtual bool Delete<TEntity>(Expression<Func<TEntity, bool>> exp, string FieldName = null, object Data = null) where TEntity : class, new()
        {
            try
            {
                TEntity Entity = Kily.Set<TEntity>().Where(exp).FirstOrDefault();
                RemovePath(Entity);
                List<PropertyInfo> props = Entity.GetType().GetProperties().Where(t => t.Name.Contains("Delete")).ToList();
                if (!string.IsNullOrEmpty(FieldName))
                    Entity.GetType().GetProperties().Where(t => t.Name.Equals(FieldName)).FirstOrDefault().SetValue(Entity, Data);
                props.Where(t => t.Name.Equals("IsDelete")).FirstOrDefault().SetValue(Entity, true);
                props.Where(t => t.Name.Equals("DeleteTime")).FirstOrDefault().SetValue(Entity, DateTime.Now);
                if (UserInfo() != null)
                    props.Where(t => t.Name.Equals("DeleteUser")).FirstOrDefault().SetValue(Entity, UserInfo().Id.ToString());
                else if (CompanyInfo() != null)
                    props.Where(t => t.Name.Equals("DeleteUser")).FirstOrDefault().SetValue(Entity, CompanyInfo().Id.ToString());
                else if (CompanyUser() != null)
                    props.Where(t => t.Name.Equals("DeleteUser")).FirstOrDefault().SetValue(Entity, CompanyUser().CompanyId.ToString());
                else if (MerchantInfo() != null)
                    props.Where(t => t.Name.Equals("DeleteUser")).FirstOrDefault().SetValue(Entity, MerchantInfo().Id.ToString());
                else if (MerchantUser() != null)
                    props.Where(t => t.Name.Equals("DeleteUser")).FirstOrDefault().SetValue(Entity, MerchantUser().InfoId.ToString());
                else if (CookInfo() != null)
                    props.Where(t => t.Name.Equals("DeleteUser")).FirstOrDefault().SetValue(Entity, CookInfo().Id.ToString());
                else if (GovtInfo() != null)
                    props.Where(t => t.Name.Equals("DeleteUser")).FirstOrDefault().SetValue(Entity, GovtInfo().Id.ToString());
                else
                    props.Where(t => t.Name.Equals("DeleteUser")).FirstOrDefault().SetValue(Entity, null);
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
                else if (MerchantInfo() != null)
                    props.Where(t => t.Name.Equals("CreateUser")).FirstOrDefault().SetValue(Entity, MerchantInfo().Id.ToString());
                else if (MerchantUser() != null)
                    props.Where(t => t.Name.Equals("CreateUser")).FirstOrDefault().SetValue(Entity, MerchantUser().InfoId.ToString());
                else if (CookInfo() != null)
                    props.Where(t => t.Name.Equals("CreateUser")).FirstOrDefault().SetValue(Entity, CookInfo().Id.ToString());
                else if (GovtInfo() != null)
                    props.Where(t => t.Name.Equals("CreateUser")).FirstOrDefault().SetValue(Entity, GovtInfo().Id.ToString());
                else
                    props.Where(t => t.Name.Equals("CreateUser")).FirstOrDefault().SetValue(Entity, null);
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
                else if (MerchantInfo() != null)
                    EntityProp.Where(t => t.Name.Equals("UpdateUser")).FirstOrDefault().SetValue(Entity, MerchantInfo().Id.ToString());
                else if (MerchantUser() != null)
                    EntityProp.Where(t => t.Name.Equals("UpdateUser")).FirstOrDefault().SetValue(Entity, MerchantUser().InfoId.ToString());
                else if (CookInfo() != null)
                    EntityProp.Where(t => t.Name.Equals("UpdateUser")).FirstOrDefault().SetValue(Entity, CookInfo().Id.ToString());
                else if (GovtInfo() != null)
                    EntityProp.Where(t => t.Name.Equals("UpdateUser")).FirstOrDefault().SetValue(Entity, GovtInfo().Id.ToString());
                else
                    EntityProp.Where(t => t.Name.Equals("UpdateUser")).FirstOrDefault().SetValue(Entity, null);
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
                    else if (MerchantInfo() != null)
                        EntityProps.Where(t => t.Name.Equals("UpdateUser")).FirstOrDefault().SetValue(Entity, MerchantInfo().Id.ToString());
                    else if (MerchantUser() != null)
                        EntityProps.Where(t => t.Name.Equals("UpdateUser")).FirstOrDefault().SetValue(Entity, MerchantUser().InfoId.ToString());
                    else if (CookInfo() != null)
                        EntityProps.Where(t => t.Name.Equals("UpdateUser")).FirstOrDefault().SetValue(Entity, CookInfo().Id.ToString());
                    else if (GovtInfo() != null)
                        EntityProps.Where(t => t.Name.Equals("UpdateUser")).FirstOrDefault().SetValue(Entity, GovtInfo().Id.ToString());
                    else
                        EntityProps.Where(t => t.Name.Equals("UpdateUser")).FirstOrDefault().SetValue(Entity, null);
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
                    else if (MerchantInfo() != null)
                        EntityProps.Where(t => t.Name.Equals("UpdateUser")).FirstOrDefault().SetValue(Entity, MerchantInfo().Id.ToString());
                    else if (MerchantUser() != null)
                        EntityProps.Where(t => t.Name.Equals("UpdateUser")).FirstOrDefault().SetValue(Entity, MerchantUser().InfoId.ToString());
                    else if (CookInfo() != null)
                        EntityProps.Where(t => t.Name.Equals("UpdateUser")).FirstOrDefault().SetValue(Entity, CookInfo().Id.ToString());
                    else if (GovtInfo() != null)
                        EntityProps.Where(t => t.Name.Equals("UpdateUser")).FirstOrDefault().SetValue(Entity, GovtInfo().Id.ToString());
                    else
                        EntityProps.Where(t => t.Name.Equals("UpdateUser")).FirstOrDefault().SetValue(Entity, null);
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
                    RemovePath(t);
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
        public virtual IQueryable<TEntity> ExcuteSQL<TEntity>(string SQL) where TEntity : class, new()
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
        public virtual int ExcuteSQL(string SQL)
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
        /// 从缓存中获取登录的公司信息
        /// </summary>
        /// <returns></returns>
        public ResponseEnterprise CompanyInfo()
        {
            ResponseEnterprise Data = Cache.GetCache<ResponseEnterprise>(SystemInfoKey.PrivateKey);
            return Data == null ? null : (Data.TableName.Equals(typeof(ResponseEnterprise).Name) ? Data : null);
        }
        /// <summary>
        /// 从缓存中获取登录的公司子用户信息
        /// </summary>
        /// <returns></returns>
        public ResponseEnterpriseUser CompanyUser()
        {
            ResponseEnterpriseUser Data = Cache.GetCache<ResponseEnterpriseUser>(SystemInfoKey.PrivateKey);
            return Data == null ? null : (Data.TableName.Equals(typeof(ResponseEnterpriseUser).Name) ? Data : null);
        }
        /// <summary>
        /// 从缓存中获取登录的餐饮商家信息
        /// </summary>
        /// <returns></returns>
        public ResponseMerchant MerchantInfo()
        {
            ResponseMerchant Data = Cache.GetCache<ResponseMerchant>(SystemInfoKey.PrivateKey);
            return Data == null ? null : (Data.TableName.Equals(typeof(ResponseMerchant).Name) ? Data : null);
        }
        /// <summary>
        /// 从缓存中获取登录的餐饮商家子用户信息
        /// </summary>
        /// <returns></returns>
        public ResponseMerchantUser MerchantUser()
        {
            ResponseMerchantUser Data = Cache.GetCache<ResponseMerchantUser>(SystemInfoKey.PrivateKey);
            return Data == null ? null : (Data.TableName.Equals(typeof(ResponseMerchantUser).Name) ? Data : null);
        }
        /// <summary>
        /// 从缓存中获取登录的厨师信息
        /// </summary>
        /// <returns></returns>
        public ResponseCookInfo CookInfo()
        {
            ResponseCookInfo Data = Cache.GetCache<ResponseCookInfo>(SystemInfoKey.PrivateKey);
            return Data == null ? null : (Data.TableName.Equals(typeof(ResponseCookInfo).Name) ? Data : null);
        }
        /// <summary>
        /// 从缓存中获取登录的政府监管信息
        /// </summary>
        /// <returns></returns>
        public ResponseGovtInfo GovtInfo()
        {
            ResponseGovtInfo Data = Cache.GetCache<ResponseGovtInfo>(SystemInfoKey.PrivateKey);
            return Data == null ? null : (Data.TableName.Equals(typeof(ResponseGovtInfo).Name) ? Data : null);
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
        /// <summary>
        /// 删除图片物理路径
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="Entity"></param>
        public virtual String RemovePath<TEntity>(TEntity Entity) where TEntity : class, new()
        {
            try
            {
                IList<String> PhotoPath = new List<String>();
                String Url = $"{ Configer.RemovePathHost}/File/RemovePath";
                Entity.GetType().GetProperties().ToList().ForEach(t =>
                {
                    if (t.PropertyType == typeof(String))
                        if ((t.GetValue(Entity) == null ? "" : t.GetValue(Entity)).ToString().Contains(@"/Upload/Images/"))
                            PhotoPath.Add(t.GetValue(Entity).ToString());
                });
                return HttpClientExtension.HttpPostAsync(Url, null, HttpClientExtension.KeyValuePairs<Object>(new { Path = String.Join(",", PhotoPath) })).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    /// <summary>
    ///  EF查询存储过程拓展
    /// </summary>
    public static class ExtendDBContext
    {
        /// <summary>
        /// 执行存储过程返回DataSet数据集
        /// </summary>
        public static DataSet Execute(this KilyContext db, string sql, SqlParameter[] sqlParams,IList<String> PropertyNames=null) 
        {
            DbConnection connection = db.Database.GetDbConnection();
            SqlCommand cmd = connection.CreateCommand() as SqlCommand;
            db.Database.OpenConnection();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            if (sqlParams != null)
            {
                cmd.Parameters.AddRange(sqlParams);
            }
            DataSet ds = new DataSet();
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                if (PropertyNames != null)
                {
                    string Table = "Table";
                    for (int i = 0; i < PropertyNames.Count; i++)
                    {
                        if (i == 0)
                            adapter.TableMappings.Add(Table, PropertyNames[i]);
                        else
                            adapter.TableMappings.Add(Table+i, PropertyNames[i]);
                    }
                }
                adapter.Fill(ds);
            }
            db.Database.CloseConnection();
            return ds;
        }
        /// <summary>
        /// DateSet转IEnumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static IEnumerable<T> ToCollection<T>(this DataSet ds) where T : new()
        {
            if (ds == null || ds.Tables.Count == 0)
            {
                return Enumerable.Empty<T>();
            }
            IDictionary<String, IList> Map = new Dictionary<String, IList>();
            IList<T> Parent = new List<T>();
            T ParentInstance = new T();
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataRow row in dt.Rows)
                {
                    PropertyInfo[] PropertInfosParent = ParentInstance.GetType().GetProperties();
                    foreach (PropertyInfo ParentInfo in PropertInfosParent)
                    {
                        if (ParentInfo.PropertyType.IsGenericType)
                        {
                            Type ChildType = ParentInfo.PropertyType.GetGenericArguments().FirstOrDefault();
                            var ChildInstrance = Activator.CreateInstance(ChildType);
                            PropertyInfo[] PropertInfosChild = ChildInstrance.GetType().GetProperties();
                            foreach (PropertyInfo ChildInfo in PropertInfosChild)
                            {
                                if (dt.Columns.Contains(ChildInfo.Name))
                                {
                                    if (!ChildInfo.CanWrite) continue;
                                    object value = row[ChildInfo.Name];
                                    if (value != DBNull.Value)
                                        ChildInfo.SetValue(ChildInstrance, value);
                                }
                            }
                            var FirstValue = ChildInstrance.GetType().GetProperties().FirstOrDefault().GetValue(ChildInstrance, null);
                            if (FirstValue != null)
                            {
                                Type DynamicList = typeof(List<>).MakeGenericType(ChildType);
                                IList ChildListType = null;
                                if (!Map.ContainsKey(ChildType.FullName))
                                {
                                    ChildListType = Activator.CreateInstance(DynamicList) as IList;
                                    Map.Add(ChildType.FullName, ChildListType);
                                }
                                else
                                    ChildListType = Map[ChildType.FullName];
                                ChildListType.Add(ChildInstrance);
                                ParentInfo.SetValue(ParentInstance, ChildListType);
                            }
                        }
                        else
                        {
                            if (dt.Columns.Contains(ParentInfo.Name))
                            {
                                if (!ParentInfo.CanWrite) continue;
                                object value = row[ParentInfo.Name];
                                if (value != DBNull.Value)
                                    ParentInfo.SetValue(ParentInstance, value);
                            }
                        }
                    }
                }
            }
            Parent.Add(ParentInstance);
            return Parent;
        }
    }
}
