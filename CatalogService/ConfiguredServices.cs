using CatalogService.Database;
using Microsoft.EntityFrameworkCore;

namespace CatalogService
{
    public class ConfiguredServices
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DbConnection"));
            });
            services.AddScoped<DbContext, AppDbContext>();
        }
    }
}
