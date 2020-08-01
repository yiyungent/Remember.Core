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
            services.AddDbContext<RemDbContext>(options =>
                options.UseMySQL(configuration.GetConnectionString("DefaultConnection")));

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
