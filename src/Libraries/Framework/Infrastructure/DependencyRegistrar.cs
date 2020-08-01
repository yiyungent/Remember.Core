using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Core.Configuration;
using Core.Infrastructure;
using Core.Infrastructure.DependencyManagement;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Core;
using Repositories.Implement;
using Repositories.Interface;
using Services.Core;
using Services.Implement;
using Services.Interface;

namespace Framework.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, RemConfig config)
        {
            //file provider
            builder.RegisterType<RemFileProvider>().As<IRemFileProvider>().InstancePerLifetimeScope();

            // 基于接口的注册
            var basePath = AppContext.BaseDirectory;
            var servicesDllFile = Path.Combine(basePath, "Services.dll");
            var RepositoriesDllFile = Path.Combine(basePath, "Repositories.dll");

            // 获取 Service.dll 程序集服务，并注册
            var assemblysServices = Assembly.LoadFrom(servicesDllFile);
            builder.RegisterAssemblyTypes(assemblysServices)
                .AsImplementedInterfaces()
                .InstancePerDependency();

            // 获取 Repository.dll 程序集服务，并注册
            var assemblysRepository = Assembly.LoadFrom(RepositoriesDllFile);
            builder.RegisterAssemblyTypes(assemblysRepository)
                .AsImplementedInterfaces()
                .InstancePerDependency();

        }

        public int Order => 0;
    }
}
