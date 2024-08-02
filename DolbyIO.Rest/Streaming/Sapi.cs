using System.Net.Http;

namespace DolbyIO.Rest.Streaming;

public sealed class Sapi
{
    public Account Account { get; }

    public Cluster Cluster { get; }

    public PublishTokens PublishToken { get; }

    public Stream Stream { get; }

    public SubscribeTokens SubscribeToken { get; }

    public Director Director { get; }

    public Whip Whip { get; }

    public Whep Whep { get; }

    internal Sapi(HttpClient httpClient)
    {
        Account = new Account(httpClient);
        Cluster = new Cluster(httpClient);
        PublishToken = new PublishTokens(httpClient);
        Stream = new Stream(httpClient);
        SubscribeToken = new SubscribeTokens(httpClient);

        Director = new Director(httpClient);
        Whip = new Whip(httpClient);
        Whep = new Whep(httpClient);
    }
}
