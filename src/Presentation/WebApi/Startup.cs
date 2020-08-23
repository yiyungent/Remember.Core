using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Core;
using Core.Configuration;
using Core.Infrastructure;
using Framework.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApi.Extensions;

namespace WebApi
{
    public class Startup
    {
        #region Fields

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private IEngine _engine;
        private RemConfig _remConfig;

        #endregion

        #region Ctor

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        #endregion

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            (_engine, _remConfig) = services.ConfigureApplicationServices(_configuration, _webHostEnvironment);

            // MVC: Install 页面使用 Views
            services.AddControllersWithViews();

            // 程序启动时 加载 已安装插件
            services.AddPluginLoad();

            // 开发环境下随便跨域
            if (_webHostEnvironment.IsDevelopment())
            {
                services.AddCors(m => m.AddPolicy("Development", a => a.AllowAnyOrigin().AllowAnyHeader()));
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // 开发环境下随便跨域
                app.UseCors("Development");
            }

            app.ConfigureRequestPipeline();

            app.StartEngine();

            // TODO: Debug Assemblies 用
            //var ass = AppDomain.CurrentDomain.GetAssemblies();
        }

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you by the factory.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            _engine.RegisterDependencies(builder, _remConfig);
        }
    }
}
