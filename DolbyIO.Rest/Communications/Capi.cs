using System.Net.Http;
using DolbyIO.Rest.Communications.Monitor;

namespace DolbyIO.Rest.Communications;

public sealed class Capi
{
    public Authentication Authentication { get; }

    public Conferences Conferences { get; }

    public Monitoring Monitor { get; }

    public Recording Recording { get; }

    public Remix Remix { get; }

    public Streaming Streaming { get; }

    internal Capi(HttpClient httpClient)
    {
        Authentication = new Authentication(httpClient);
        Conferences = new Conferences(httpClient);
        Monitor = new Monitoring(httpClient);
        Recording = new Recording(httpClient);
        Remix = new Remix(httpClient);
        Streaming = new Streaming(httpClient);
    }
}
