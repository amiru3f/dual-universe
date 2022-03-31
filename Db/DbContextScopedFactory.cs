using Microsoft.EntityFrameworkCore.Infrastructure;

public class DeviceContextScopedFactory : IDbContextFactory<DeviceContext>
    {

        #region static field(s)


        private static readonly IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.Developement.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}")
            .AddEnvironmentVariables()
            .Build();

        private static readonly string connectionString = config.GetConnectionString("DeviceDb");
        private static readonly DbContextOptions<DeviceContext> options = new DbContextOptionsBuilder<DeviceContext>()
                .UseSqlServer(connectionString)
                .Options;
        private static readonly DbContextOptions<DeviceContext> readonlyOptions = new DbContextOptionsBuilder<DeviceContext>()
                .UseSqlServer(connectionString + ";Application Intent=readonly")
                .Options;
        private static readonly PooledDbContextFactory<DeviceContext> readFactory = new PooledDbContextFactory<DeviceContext>(readonlyOptions, 1000);
        private static readonly PooledDbContextFactory<DeviceContext> writeFactory = new PooledDbContextFactory<DeviceContext>(options, 1000);

        #endregion

        private readonly IReadonlyZoneDetector _isReadonlyZoneDetector;

        public DeviceContextScopedFactory(IReadonlyZoneDetector isReadonlyZoneDetector)
        {
            _isReadonlyZoneDetector = isReadonlyZoneDetector;
        }

        public DeviceContext CreateDbContext()
        {
            if (_isReadonlyZoneDetector.GetIsReadonly()) return readFactory.CreateDbContext();
            
            return writeFactory.CreateDbContext();
        }
    }