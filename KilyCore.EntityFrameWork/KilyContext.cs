using KilyCore.Configure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KilyCore.EntityFrameWork
{
    public class KilyContext: DbContext,IKilyContext
    {
        public KilyContext()
        {
            //初始化数据库非迁移
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //将所有的实体映射加载
            IEnumerable<Type> Types = GetType().GetTypeInfo().Assembly.GetTypes()
                .Where(t => t.GetInterfaces().Any(x => x.GetTypeInfo().IsGenericType && x.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));
            foreach (var item in Types)
            {
                modelBuilder.ApplyConfiguration(Activator.CreateInstance(item) as dynamic);
            }
            base.OnModelCreating(modelBuilder);
        }
        /// <summary>
        /// 读取配置
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (Configer.DataProvider.ToUpper().Equals("MYSQL"))
                optionsBuilder.UseMySql(Configer.ConnentionString);
            else if (Configer.DataProvider.ToUpper().Equals("SQLSERVER"))
                optionsBuilder.UseSqlServer(Configer.ConnentionString);
        }
    }
}
