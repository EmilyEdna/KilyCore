using Autofac;
using Autofac.Extensions.DependencyInjection;
using KilyCore.Cache.MongoCache;
using KilyCore.Cache.RedisCache;
using KilyCore.Configure;
using KilyCore.EntityFrameWork;
using KilyCore.Extension.ApplicationService.IocManager;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace KilyCore.Extension.ApplicationService.DependencyIdentity
{
    /// <summary>
    /// 系统初始化引擎
    /// </summary>
    public class AutoFacEngine : IEngine
    {
        AutoFacManager IocInstance = AutoFacManager.IocInstance;
        private IList<Assembly> Assembly => Configer.Assembly;
        private IEnumerable<Type> Service => Assembly.SelectMany(t => t.ExportedTypes.Where(x => x.GetInterfaces().Contains(typeof(IService))));
        private IEnumerable<Type> Cache => Assembly.SelectMany(t => t.ExportedTypes.Where(x => x.GetInterfaces().Contains(typeof(ICache))));
        private IEnumerable<Type> Caches => Assembly.SelectMany(t => t.ExportedTypes.Where(x => x.GetInterfaces().Contains(typeof(IMongoDbCache))));
        private IEnumerable<Type> Context => Assembly.SelectMany(t => t.ExportedTypes.Where(x => x.GetInterfaces().Contains(typeof(IKilyContext))));
        /// <summary>
        /// 取出实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return IocInstance.Resolve<T>();
        }
        /// <summary>
        /// 返回AutoFac服务
        /// </summary>
        /// <param name="Collection"></param>
        /// <returns></returns>
        public IServiceProvider ServiceProvider(IServiceCollection Collection)
        {
            var Builder = IocInstance.Builder;
            //托管.net core自带的DI
            Builder.Populate(Collection);
            Register(Builder);
            IocInstance.CompleteBuiler();
            var container = IocInstance.Container;
            //return new AutofacServiceProvider(container);
            return container.Resolve<IServiceProvider>();
        }
        /// <summary>
        /// 程序集注入
        /// </summary>
        /// <param name="builder"></param>
        protected void Register(ContainerBuilder builder)
        {
            //数据库注入
            Context.ToList().ForEach(t =>
            {
                builder.RegisterType(Activator.CreateInstance(t).GetType()).AsImplementedInterfaces().InstancePerLifetimeScope();
            });
            //业务逻辑注入
            Service.ToList().ForEach(t =>
            {
                if (t.IsClass)
                {
                    builder.RegisterType(Activator.CreateInstance(t).GetType()).As(t.GetInterfaces().Where(x => x.GetInterfaces().Contains(typeof(IService))).FirstOrDefault()).SingleInstance();
                }
            });
            //redis注入
            Cache.ToList().ForEach(t =>
            {
                //可以通过CacheFactory取也可以通过AutoFac取
                builder.RegisterType(Activator.CreateInstance(t).GetType()).As(t.GetInterfaces().FirstOrDefault()).SingleInstance();
            });
            //mongodb注入
            Caches.ToList().ForEach(t => {
                //可以通过CacheFactory取也可以通过AutoFac取
                builder.RegisterType(Activator.CreateInstance(t).GetType()).As(t.GetInterfaces().FirstOrDefault()).SingleInstance();
            });
        }
    }
}
