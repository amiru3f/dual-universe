namespace Services;

public class ReportService
{
    private readonly IServiceProvider serviceProvider;
    public ReportService(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public async Task<string> GetAllReports()
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<DeviceContext>();
            string values = string.Join(",", db.Devices.ToList().Select(x => x.Id));

            return await Task.FromResult(values);
        }
    }
}