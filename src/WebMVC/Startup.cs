using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Redis;
using EFCore;
using Hangfire;
using Hangfire.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Model;
using NLog.Extensions.Logging;
using NLog.Web;
using Service;
using Service.Interface.Service;
using Service.Interface.Store;
using Service.Interface.task;
using StackExchange.Redis;
using Store;
using WebCore.Filter;
using WebCore.Models;
using WebMVC.Filter;
using YYLog.ClassLibrary;

namespace WebCore
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
            services.AddDbContext<MySqlContext>(options =>
       options.UseMySql(Configuration.GetConnectionString("MysqlConnection")));

       //     services.AddDbContext<OfficialMySqlContext>(options =>
       //options.UseMySql(Configuration.GetConnectionString("OfficialMySqlConnection")));
            //services.Configure<DBMonoUtility.DBInitOption>(Configuration.GetSection("DBinitOption"));

            services.AddMvc(
            options =>
            {
                //options.Filters.Add(new CheckMenuFilterAttribute() { needCheck=true}); // an instance
                //options.Filters.Add(new CheckTokenFilterAttribute() { needCheck=true}); // an instance
                options.Filters.Add<CheckTokenFilterAttribute>();
                options.Filters.Add<CheckMenuFilterAttribute>();
                options.Filters.Add<NoFilterAttribute>();
                options.Filters.Add(new CustomExceptionFilterAttribute());
            });
           
            services.AddTransient<IAdminUserStore, AdminUserStore>();
            services.AddTransient<IAdminUserService, AdminUserService>();

            services.AddTransient<IAdminMenuStore, AdminMenuStore>();
            services.AddTransient<IAdminMenuService, AdminMenuService>();

            services.AddTransient<IAdminRoleStore, AdminRoleStore>();
            services.AddTransient<IAdminRoleService, AdminRoleService>();

            //services.AddTransient<IHangFireStore, HangFireStore>();
            //services.AddTransient<IHangFireService, HangFireService>();

            services.AddTransient<IUserSelfService, UserSelfService>();

            services.AddTransient<IEasydentityService, EasyIdentityService>();
            services.AddTransient<IRedisService, RedisService>();
            services.AddTransient<ILogHelper, NLogHelper>();
            services.AddTransient<ITaskHangFire, TaskHangFire>();
            services.AddTransient<IWXPayHelper, WXPayHelper>();

            //services.Configure<RedisSetting>(Configuration.GetSection("RedisSetting"));

            //Log.Init(1, 1024000, "yyyyMMdd", Directory.GetCurrentDirectory()+ "/Log", (LogType)1);
            //var a = Configuration.GetSection("RedisSetting")["RedisHosts"];
            //var b = Configuration.GetSection("RedisSetting")["RedisPwd"];
            //RedisPools.Init(Configuration.GetSection("RedisSetting")["RedisHosts"], 0, 1, Configuration.GetSection("RedisSetting")["RedisPwd"]);

            services.AddSingleton(option => new RedisCore(Configuration.GetValue<string>("RedisSetting:RedisConnection"), Configuration.GetValue("RedisSetting:RedisIndex", 0)));

            services.AddHangfire(opt =>
            {
                opt.UseRedisStorage(Configuration["RedisSetting:RedisConnection"], new RedisStorageOptions { Db = 1 });
            });
            //services.AddHangfire(r => r.UseRedisStorage(Configuration["HangfireSetting:RedisConnection"]));
            //RedisPools.Init(Configuration.GetSection("RedisSetting").Value,  1);
            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()
            //     .AddInMemoryClients(Config.GetClients());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseBrowserLink();
                EnvConfig.IsDevelopment = true;
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                EnvConfig.IsDevelopment = false;
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            //添加Nlog
            loggerFactory.AddNLog();
            env.ConfigureNLog("Nlog.config");
            ////启用服务
            //app.UseHangfireServer();

            ////启用Dashboard面板
            //app.UseHangfireDashboard();

            //app.UseIdentityServer();
            //任务处理
            app.UseHangfireServer();
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireFilter() }
            });
        }
    }
}
