using System.Net.Http;

namespace DolbyIO.Rest.Streaming;

public sealed class Sapi
{
    public Cluster Cluster { get; }

    public Geo Geo { get; }

    public PublishToken PublishToken { get; }

    public Director Director { get; }

    public Whip Whip { get; }

    public Whep Whep { get; }

    internal Sapi(HttpClient httpClient)
    {
        Cluster = new Cluster(httpClient);
        Geo = new Geo(httpClient);
        PublishToken = new PublishToken(httpClient);

        Director = new Director(httpClient);
        Whip = new Whip(httpClient);
        Whep = new Whep(httpClient);
    }
}
