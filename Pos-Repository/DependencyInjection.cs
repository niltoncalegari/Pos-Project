using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pos_Service.Common;

namespace Pos_Repository;

public static class DependencyInjection
{
    public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
    {
        var environmentName = AppEnvironment.GetCurrentEnvironment();
        var connectionString = configuration.GetConnectionString("AppDatabase");
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<AppDbContext>();

        return services;
    }
}
