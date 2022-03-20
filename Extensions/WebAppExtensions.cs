
namespace DualUniverse.Extensions;

public static class WebAppExtensions
{
    public static void MapHandler<THandler>(this WebApplication app)
        where THandler : IHttpHandler
    {
        app.MapMethods(
            THandler.Template,
            new[] { THandler.Method.ToString() },
            THandler.Handle
            );
    }
}