using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



namespace WebApplication2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            

            //services.AddDistributedMemoryCache();//启用Session之前必须先添加的内存

            services.AddSession(options=> {

                options.Cookie.IsEssential = true;//启用Cookie的最重要的
                //options.Cookie.Name = ".AdventureWorks.Session";
                //options.IdleTimeout = TimeSpan.FromSeconds(2000);///设置Session过期时间
                //options.Cookie.HttpOnly = true;

            });
            services.AddSingleton(typeof(MyExceptionFilter));
            services.AddSingleton(typeof(TestService));
            services.AddMvc(options => {

                var serviceProvider = services.BuildServiceProvider();
                var filter = serviceProvider.GetService<MyExceptionFilter>();
                options.Filters.Add(filter);

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSession();

            if (env.IsDevelopment())
            {
                //开发环境
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //自定义错误页
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseAuthentication();
            app.UseCookiePolicy();

            

            app.UseMvc(routes =>
            {
                //路由
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
