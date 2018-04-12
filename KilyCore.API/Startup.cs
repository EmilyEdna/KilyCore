using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using KilyCore.Configure;
using KilyCore.Extension.ApplicationService.DependencyIdentity;
using KilyCore.Extension.FilterGroup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using NLog.Extensions.Logging;
using NLog.Web;

namespace KilyCore.API
{
    public class Startup
    {
        public IEngine Engine { get; private set; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            GetAssembly();
            GetSystemConfiger();
            Engine = EngineExtension.Context;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //注册过滤器
            services.AddMvc(option =>
            {
                option.Filters.Add(typeof(AuthorizationFilter));
                option.Filters.Add(typeof(ResourceFilter));
                option.Filters.Add(typeof(ActionFilter));
                option.Filters.Add(typeof(ExceptionFilter));
                option.Filters.Add(typeof(ResultFilter));
                option.RespectBrowserAcceptHeader = true;
            });
            //返回数据格式
            services.AddMvc().AddJsonOptions(option =>
            {
                option.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                option.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            //添加跨域
            services.AddCors(option =>
            {
                option.AddPolicy("KilyCore", builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
            });
            //添加Session
            services.AddSession();
            return Engine.ServiceProvider(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            //Nlog
            logger.AddNLog();
            env.ConfigureNLog("Nlog.config");
            //设置全局跨域
            app.UseCors("KilyCore");
            //启用Session
            app.UseSession();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
        }
        /// <summary>
        /// 加载所有程序集
        /// </summary>
        public void GetAssembly()
        {
            IList<Assembly> ass = new List<Assembly>();
            var lib = DependencyContext.Default;
            var libs = lib.CompileLibraries.Where(t => !t.Serviceable).Where(t => t.Type != "package").ToList();
            foreach (var item in libs)
            {
                Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(item.Name));
                ass.Add(assembly);
            }
            Configer.Assembly = ass;
        }
        /// <summary>
        /// 获取系统配置
        /// </summary>
        public void GetSystemConfiger()
        {
            Configer.DataProvider = Configuration.GetConnectionString("DataProvider");
            Configer.ConnentionString = Configuration.GetConnectionString("ConnectionString");
            Configer.RedisConnectionString = Configuration["RedisConnectionString:ConnectionString"];
            Configer.MongoDBConnectionString = Configuration["MongoDBConnectionString:ConnectionString"];
            Configer.MongoDBName= Configuration["MongoDBConnectionString:MongoDBName"];
            Configer.ApiKey = Configuration["Key:ApiKey"];
        }
    }
}
