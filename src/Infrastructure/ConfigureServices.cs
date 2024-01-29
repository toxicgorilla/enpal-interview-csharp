using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UrlShortenerService.Application.Common.Interfaces;
using UrlShortenerService.Infrastructure.Persistence;
using UrlShortenerService.Infrastructure.Persistence.Interceptors;
using UrlShortenerService.Infrastructure.Services;

namespace UrlShortenerService.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            _ = services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("UrlShortenerServiceDb"));
        }
        else
        {
            _ = services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        _ = services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        _ = services.AddScoped<ApplicationDbContextInitializer>();

        _ = services.AddTransient<IDateTime, DateTimeService>();

        return services;
    }
}
