global using Microsoft.EntityFrameworkCore;

using Services;
using DualUniverse.Entrypoint.Handlers;
using DualUniverse.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddDbContextWithPoolingMechanismEnabled();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddSingleton<ReportService>();


var app = builder.Build();

app.MapHandler<IOTDataCaptureHandler>();
app.MapHandler<IOTDataCaptureHandlerWithError>();

app.UseRouting();
app.UseEndpoints((conf) =>
{
    conf.MapDefaultControllerRoute();
});


app.Lifetime.ApplicationStarted.Register(async () => await DbMigration.MigrateDb(app.Services));
app.Run();
