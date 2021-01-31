using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Core;
using Core.Configuration;
using PluginCore.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Repositories.Core;
using WebApi.Infrastructure;
using Framework.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace WebApi
{
    public class Startup
    {
        #region Fields

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private RemConfig _remConfig;

        #endregion

        #region Ctor

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        #endregion

        #region ConfigureServices
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // 配置注入
            _remConfig = _configuration.GetSection(RemConfig.Rem).Get<RemConfig>();
            services.Configure<RemConfig>(_configuration.GetSection(
                RemConfig.Rem));

            #region 选择数据库类型
            string dbType = _configuration["Rem:DbType"];
            string connStr = _configuration.GetConnectionString("DefaultConnection");
            switch (dbType.ToLower())
            {
                case "sqlite":

                    if (connStr.StartsWith("~"))
                    {
                        // 相对路径转绝对路径
                        string dir = Directory.GetCurrentDirectory();
                        string dbFilePath = Path.Combine(dir, connStr);

                        connStr = dbFilePath;
                    }

                    services.AddDbContext<RemDbContext>(options =>
                        options.UseSqlite(connStr));
                    break;
                case "mysql":
                    services.AddDbContext<RemDbContext>(options =>
                        options.UseMySQL(connStr));
                    break;
                case "sqlserver":
                    services.AddDbContext<RemDbContext>(options =>
                        options.UseSqlServer(connStr));
                    break;
                default:

                    if (connStr.StartsWith("~"))
                    {
                        // 相对路径转绝对路径
                        string dir = Directory.GetCurrentDirectory();
                        string dbFilePath = Path.Combine(dir, connStr);

                        connStr = dbFilePath;
                    }

                    services.AddDbContext<RemDbContext>(options =>
                        options.UseSqlite(connStr));
                    break;
            }
            #endregion

            //services.AddControllers();

            #region for UHub IdentityServer4
            // accepts any access token issued by identity server
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = _configuration["Rem:Authority"];

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        // 多长时间来验证以下 Token
                        ClockSkew = TimeSpan.FromSeconds(5),
                        // 我们要求 Token 需要有超时时间这个参数
                        RequireExpirationTime = true,
                    };

                    options.RequireHttpsMetadata = false;
                });
            #endregion

            #region 添加授权策略-所有标记 [WebApiAuthorize] 都需要权限检查
            services.AddSingleton<IAuthorizationHandler, WebApiAuthorizationHandler>();

            // adds an authorization policy to make sure the token is for scope 'webapi'
            services.AddAuthorization(options =>
            {
                options.AddPolicy("WebApi", policy =>
                {
                    // 无法满足 下方任何一项：HTTP 403 错误
                    // 1. 需登录即已认证用户  (注意：无法满足此项，即 JWT Token 无法通过效验, HTTP 401 错误)
                    policy.RequireAuthenticatedUser();
                    // 2.需要 JWT scope 中包含 Remember.Core.WebApi
                    policy.RequireClaim("scope", "Remember.Core.WebApi");
                    // 3.需要 检查是否拥有当前请求资源的权限
                    policy.Requirements.Add(new WebApiRequirement());
                });
            });
            #endregion

            // MVC: Install 页面使用 Views
            services.AddControllersWithViews();

            // 添加插件框架
            services.AddPluginFramework();

            // 开发环境下随便跨域
            if (_webHostEnvironment.IsDevelopment())
            {
                services.AddCors(m => m.AddPolicy("Development", a => a.AllowAnyOrigin().AllowAnyHeader()));
            }
        }
        #endregion

        #region Configure
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // 开发环境下随便跨域
                app.UseCors("Development");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            // 需要授权: 为了保护 api 资源
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // TODO: Debug Assemblies 用
            //var ass = AppDomain.CurrentDomain.GetAssemblies();
        }
        #endregion

        #region ConfigureContainer
        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you by the factory.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac, like:
            builder.RegisterModule(new AutofacApplicationModule());
        }
        #endregion
    }
}
