using System.IO;
using Core.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Core;

namespace Framework.Infrastructure
{
    public class RemWebApiStartup : IRemStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            #region 选择数据库类型
            string dbType = configuration["Rem:DbType"];
            string connStr = configuration.GetConnectionString("DefaultConnection");
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

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder application)
        {
            application.UseHttpsRedirection();

            application.UseRouting();

            application.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public int Order => 10000;
    }
}
