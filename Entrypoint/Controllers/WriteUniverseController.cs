namespace DualUniverse.Entrypoint.Controllers;
using Microsoft.AspNetCore.Mvc;

public class WriteUniverseController : Controller
{
    [HttpGet("wUniverse/write")]
    public async Task<string> Write([FromServices] DeviceContext db)
    {
        return await Task.FromResult("going to write on db");
    }
}