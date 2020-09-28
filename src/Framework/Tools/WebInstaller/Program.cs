using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Repositories.Core;

namespace WebInstaller
{
    class Program
    {
        private static IConfiguration _configuration;

        static void Main(string[] args)
        {
            #region 测试获取插件目录

            //string dir = Directory.GetCurrentDirectory();
            //string appDomainBaseDir = AppDomain.CurrentDomain.BaseDirectory;
            //string pluginRootPath = Path.Combine(Directory.GetCurrentDirectory(), "Plugins");
            //string[] pluginDirs = Directory.GetDirectories(Path.Combine(Directory.GetCurrentDirectory(), "Plugins"), "*");
            //IList<string> pluginFolderNames = new List<string>();
            //foreach (var item in pluginDirs)
            //{
            //    string pluginFolderName = item.Replace(pluginRootPath + Path.DirectorySeparatorChar, "");
            //    pluginFolderNames.Add(pluginFolderName);
            //}

            #endregion


            Console.WriteLine("开始初始化数据库 !");
            _configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var optionsBuilder = new DbContextOptionsBuilder<RemDbContext>();
            optionsBuilder.UseMySQL(_configuration.GetConnectionString("DefaultConnection"));
            var dbContext = new RemDbContext(optionsBuilder.Options);

            DbInitializer.Initialize(dbContext);

            Console.WriteLine("初始化数据库完成 !");

            Console.ReadLine();
        }
    }
}
