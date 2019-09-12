using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KilyCore.WEB.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.StaticFiles;

namespace KilyCore.WEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            GetSystemConfiger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = new FileExtensionContentTypeProvider(new Dictionary<string, string>
                {
                      { ".apk", "application/vnd.android.package-archive" }
                })
            });
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Admin",
                    template: "{controller}/{action}/{id?}");
                routes.MapRoute(
                    name: "Area",
                    template: "{area:exists}/{controller}/{action}/{id?}"
                    );
            });
        }

        public void GetSystemConfiger()
        {
            Configer.Host = Configuration["Host:ApiHost"];
            Configer.WebHost = Configuration["Host:WebHost"];
            Configer.WebHostClass = Configuration["Host:WebHostClass"];
            Configer.WebHostBox = Configuration["Host:WebHostBox"];
        }
    }
}
