using DualUniverse;

public interface IReadonlyZoneDetector
{
    bool GetIsReadonly();
}

public class ReadonlyZoneDetector : IReadonlyZoneDetector
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public ReadonlyZoneDetector(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public bool GetIsReadonly()
    {
            var httpContext = _httpContextAccessor.HttpContext;
            var endpoint = httpContext?.GetEndpoint();

            var readonlyAttrMetaData = endpoint?.Metadata.GetMetadata<ReadonlyAttribute>();

            bool isReadonly = readonlyAttrMetaData != null;
            isReadonly |= endpoint?.Metadata?.OfType<ReadonlyAttribute>()?.Count() > 0;

            return isReadonly;
    }
}