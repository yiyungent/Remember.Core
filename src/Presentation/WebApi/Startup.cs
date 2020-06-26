using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }

        // ConfigureContainer is where you can register things directly
        // with Autofac. This runs after ConfigureServices so the things
        // here will override registrations made in ConfigureServices.
        // Don't build the container; that gets done for you by the factory.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac, like:
            builder.RegisterModule(new AutofacModuleRegister());

            // 注册基于接口约束的实体
            //var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>()
            //    .Where(
            //        assembly =>
            //            assembly.GetTypes().FirstOrDefault(type => type.GetInterfaces().Contains(typeof(IDependency))) !=
            //            null
            //    );
            //builder.RegisterAssemblyTypes(assemblies.ToArray())
            //    .AsImplementedInterfaces()
            //    .InstancePerDependency();

            // add the Entity Framework context to make sure only one context per request
            //builder.RegisterType<RemDbContext>().InstancePerRequest();
            //builder.RegisterType<RemDbContext>().InstancePerRequest();
            //builder.Register(c => c.Resolve<RemDbContext>()).As<DbContext>().InstancePerRequest();
        }
    }
}
