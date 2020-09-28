using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebApi
{
    public class Program
    {
        private static Assembly[] ass;

        public static void Main(string[] args)
        {
            // TODO: Debug Assemblies 用
            //ass = AppDomain.CurrentDomain.GetAssemblies();
            //string dllFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ViewModel.dll");
            //var name = System.Reflection.AssemblyName.GetAssemblyName(dllFilePath);
            // 1.加载 Assembly 到 AppDomain.CurrentDomain
            // AppDomain.CurrentDomain.Load(name);
            // 2.加载 Assembly 到 AppDomain.CurrentDomain
            // Assembly.Load(name);
            //ass = AppDomain.CurrentDomain.GetAssemblies();

            CreateHostBuilder(args)
                .Build()
                .Run();

            //ass = AppDomain.CurrentDomain.GetAssemblies();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();

                    // TODO: Debug Assemblies 用
                    //ass = AppDomain.CurrentDomain.GetAssemblies();
                });
    }
}
