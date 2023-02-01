using System.Net.Http;
using DolbyIO.Rest.Streaming.Models;

namespace DolbyIO.Rest.Streaming;

public sealed class Sapi
{
    public Cluster Cluster { get; }

    public Geo Geo { get; }

    public PublishTokens PublishToken { get; }

    public Stream Stream { get; }

    public SubscribeTokens SubscribeToken { get; }

    public Director Director { get; }

    public Whip Whip { get; }

    public Whep Whep { get; }

    internal Sapi(HttpClient httpClient)
    {
        Cluster = new Cluster(httpClient);
        Geo = new Geo(httpClient);
        PublishToken = new PublishTokens(httpClient);
        Stream = new Stream(httpClient);
        SubscribeToken = new SubscribeTokens(httpClient);

        Director = new Director(httpClient);
        Whip = new Whip(httpClient);
        Whep = new Whep(httpClient);
    }
}
