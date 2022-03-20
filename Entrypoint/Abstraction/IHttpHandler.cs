public interface IHttpHandler
{
    static abstract string Template { get; }
    static abstract HttpMethod Method { get; }
    static abstract Delegate Handle { get; }
}