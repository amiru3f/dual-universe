namespace DualUniverse.Entrypoint.Handlers;
public record struct IOTDataCaptureHandler : IHttpHandler
{
    public static string Template => "/wUniverse/{deviceName}/write";

    public static HttpMethod Method => HttpMethod.Get;

    public static Delegate Handle => HandleIOTDataWrite;

    private static async Task<string> HandleIOTDataWrite(string deviceName, DeviceContext db)
    {
        db.Devices.Add(new DeviceMetadataModel()
        {
            Value = deviceName
        });

        await db.SaveChangesAsync();
        return $"Write endpoint :) {deviceName}";
    }
}