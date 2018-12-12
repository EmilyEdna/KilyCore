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
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点13分
/// </summary>
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
            GetVersionPriceConfiger();
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
                option.AddPolicy("KilyCore",
                    builder =>
                    builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins(Configuration["CorsOrigins:Origin"].Split(",").ToArray())
                    .AllowCredentials());
            });
            //添加Session
            services.AddSession(t =>
            {
                //Session 5分钟后过期
                t.IdleTimeout = TimeSpan.FromMinutes(5);
            });
            IServiceProvider IocProviderService = Engine.ServiceProvider(services);
            //持久化任务
            IocProviderServices.Instance.IocProviderService.RestartQuartz();
            return IocProviderService;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            //Nlog
            logger.AddNLog();
            env.ConfigureNLog("Nlog.config");
            //设置全局跨域
            app.UseCors("KilyCore");
            //使用支付
            app.UsePaySharp();
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
        protected void GetAssembly()
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
        protected void GetSystemConfiger()
        {
            Configer.DataProvider = Configuration.GetConnectionString("DataProvider");
            Configer.ConnentionString = Configuration.GetConnectionString("ConnectionString");
            Configer.RedisConnectionString = Configuration["RedisConnectionString:ConnectionString"];
            Configer.MongoDBConnectionString = Configuration["MongoDBConnectionString:ConnectionString"];
            Configer.MongoDBName = Configuration["MongoDBConnectionString:MongoDBName"];
            Configer.ApiKey = Configuration["Key:ApiKey"];
        }
        /// <summary>
        /// 获取版本价格
        /// </summary>
        protected void GetVersionPriceConfiger()
        {
            ConfigMoney.Common = Convert.ToInt32(Configuration["ConfigMoney:Common"]);
            ConfigMoney.Cook = Convert.ToInt32(Configuration["ConfigMoney:Cook"]);
            #region 体验版
            ConfigMoney.PlantAndCultureTest = Convert.ToInt32(Configuration["ConfigMoney:PlantAndCultureTest"]);
            ConfigMoney.ProductionTest = Convert.ToInt32(Configuration["ConfigMoney:ProductionTest"]);
            ConfigMoney.CirculationTest = Convert.ToInt32(Configuration["ConfigMoney:CirculationTest"]);
            ConfigMoney.UnitCanteenTest = Convert.ToInt32(Configuration["ConfigMoney:UnitCanteenTest"]);
            ConfigMoney.RepastTest = Convert.ToInt32(Configuration["ConfigMoney:RepastTest"]);
            #endregion
            #region 基础版
            ConfigMoney.PlantAndCultureBase = Convert.ToInt32(Configuration["ConfigMoney:PlantAndCultureBase"]);
            ConfigMoney.ProductionBase = Convert.ToInt32(Configuration["ConfigMoney:ProductionBase"]);
            ConfigMoney.CirculationBase = Convert.ToInt32(Configuration["ConfigMoney:CirculationBase"]);
            ConfigMoney.UnitCanteenBase = Convert.ToInt32(Configuration["ConfigMoney:UnitCanteenBase"]);
            ConfigMoney.RepastBase = Convert.ToInt32(Configuration["ConfigMoney:RepastBase"]);
            #endregion
            #region 升级版
            ConfigMoney.PlantAndCultureLv = Convert.ToInt32(Configuration["ConfigMoney:PlantAndCultureLv"]);
            ConfigMoney.ProductionLv = Convert.ToInt32(Configuration["ConfigMoney:ProductionLv"]);
            ConfigMoney.CirculationLv = Convert.ToInt32(Configuration["ConfigMoney:CirculationLv"]);
            ConfigMoney.UnitCanteenLv = Convert.ToInt32(Configuration["ConfigMoney:UnitCanteenLv"]);
            ConfigMoney.RepastLv = Convert.ToInt32(Configuration["ConfigMoney:RepastLv"]);
            #endregion
            #region 旗舰版
            ConfigMoney.PlantAndCultureEnterprise = Convert.ToInt32(Configuration["ConfigMoney:PlantAndCultureEnterprise"]);
            ConfigMoney.ProductionEnterprise = Convert.ToInt32(Configuration["ConfigMoney:ProductionEnterprise"]);
            ConfigMoney.CirculationEnterprise = Convert.ToInt32(Configuration["ConfigMoney:CirculationEnterprise"]);
            ConfigMoney.UnitCanteenEnterprise = Convert.ToInt32(Configuration["ConfigMoney:UnitCanteenEnterprise"]);
            ConfigMoney.RepastEnterprise = Convert.ToInt32(Configuration["ConfigMoney:RepastEnterprise"]);
            #endregion

        }
    }
}
