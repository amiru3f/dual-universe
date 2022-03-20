namespace DualUniverse.Entrypoint.Controllers;
using Microsoft.AspNetCore.Mvc;
using Services;

[Readonly]
public class ReadUniverseController : Controller
{
    private readonly ReportService reportService;

    public ReadUniverseController(ReportService reportService)
    {
        this.reportService = reportService;
    }

    [HttpGet("rUniverse/read")]
    public async Task<string> Read()
    {
        return await reportService.GetAllReports();
    }
}