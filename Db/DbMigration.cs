public class DbMigration
{
    public static async Task MigrateDb(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateAsyncScope();
        var logger = scope.ServiceProvider.GetService<ILogger<DbMigration>>();
        var dbContext = scope.ServiceProvider.GetRequiredService<DeviceContext>();
    

        while (true)
        {
            IEnumerable<string> pendingMigrations = new List<string>();
            try
            {
                logger.LogInformation("Going to migrate database if any pending exist");
                pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();

            }
            catch
            {
                await Task.Delay(2000);
                continue;
            }

            if (pendingMigrations.Any())
            {
                await dbContext.Database.MigrateAsync();
            }

            logger.LogInformation("Migration done");
            break;
        }
    }
}