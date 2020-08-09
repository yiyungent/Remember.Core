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
            var settings = new AppSettings();

            //new ConfigurationBuilder().AddConfiguration()

            _configuration = new ConfigurationBuilder().Build();

            // TODO: _configuration 添加appsettings.json

            var optionsBuilder = new DbContextOptionsBuilder<RemDbContext>();
            optionsBuilder.UseMySQL(
                settings.ConnectionStrings.DefaultConnection);
            var dbContext = new RemDbContext(optionsBuilder.Options);

            DbInitializer.Initialize(dbContext);

            Console.WriteLine("Hello World!");

            Console.ReadLine();
        }
    }
}
