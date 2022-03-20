using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace DualUniverse.Extensions;

public static class DbContextExtensions
{
    public static void AddDbContext(this IServiceCollection services)
    {
        services.AddScoped((sp) =>
        {

            var config = sp.GetRequiredService<IConfiguration>();
            var httpContext = sp.GetRequiredService<IHttpContextAccessor>().HttpContext;
            var endpoint = httpContext?.GetEndpoint();

            var descriptor = endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>();

            //readonly detection
            bool isReadonly = descriptor?.MethodInfo.GetCustomAttributes<ReadonlyAttribute>(false).Count() > 0;
            isReadonly |= endpoint?.Metadata?.OfType<ReadonlyAttribute>()?.Count() > 0;

            string connectionString = config.GetConnectionString("DeviceDb");

            var dbContextOptions = new DbContextOptionsBuilder<DeviceContext>();

            if (isReadonly)
            {
                connectionString = connectionString?.TrimEnd(';') ?? "";
                connectionString += ";ApplicationIntent=ReadOnly;";
            }

            dbContextOptions = dbContextOptions.UseSqlServer(connectionString);
            return new DeviceContext(dbContextOptions.Options, !isReadonly);
        });
    }
}