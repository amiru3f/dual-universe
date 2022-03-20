namespace DualUniverse.Entrypoint.Handlers;
public record struct IOTDataCaptureHandlerWithError : IHttpHandler
{
    public static string Template => "/wUniverse/{deviceName}/writeError";

    public static HttpMethod Method => HttpMethod.Get;

    public static Delegate Handle => HandleIOTDataWrite;

    [Readonly]
    private static async Task<string> HandleIOTDataWrite(string deviceName, DeviceContext db)
    {
        db.Devices.Add(new DeviceMetadataModel()
        {
            Value = deviceName
        });

        return $"Write endpoint with readonly connection string :) \n" + db.Database.GetConnectionString().ToString();
    }
}