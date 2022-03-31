using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DualUniverse.Extensions;

public static class DbContextExtensions
{
    public static void AddDbContextWithPoolingMechanismEnabled(this IServiceCollection services)
    {
        services.AddSingleton<IReadonlyZoneDetector, ReadonlyZoneDetector>();
        services.AddScoped<DeviceContextScopedFactory>();
        services.AddScoped(
            sp => sp.GetRequiredService<DeviceContextScopedFactory>().CreateDbContext());
    }
}