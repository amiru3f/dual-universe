namespace DualUniverse.Entrypoint.Handlers;
public record struct IOTDataCaptureHandler : IHttpHandler
{
    public static string Template => "/wUniverse/{deviceId:int}/write";

    public static HttpMethod Method => HttpMethod.Get;

    public static Delegate Handle => HandleIOTDataWrite;

    private static string HandleIOTDataWrite(int deviceId, DeviceContext db)
    {
        return $"Write endpoint :) {deviceId}";
    }
}