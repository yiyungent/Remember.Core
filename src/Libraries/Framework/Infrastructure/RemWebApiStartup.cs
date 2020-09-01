using System;
using System.IO;
using Core.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
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

            #region for UHub IdentityServer4
            // accepts any access token issued by identity server
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:5001";

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

            // adds an authorization policy to make sure the token is for scope 'api1'
            services.AddAuthorization(options =>
            {
                options.AddPolicy("webapi", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "webapi");
                });
            });
            #endregion
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            // 需要授权: 为了保护 api 资源
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public int Order => 10000;
    }
}
