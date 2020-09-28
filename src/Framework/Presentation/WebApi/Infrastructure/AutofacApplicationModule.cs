using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core.Registration;

namespace WebApi.Infrastructure
{
    public class AutofacApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            #region 注册服务，仓储层
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
            #endregion
        }
    }
}
