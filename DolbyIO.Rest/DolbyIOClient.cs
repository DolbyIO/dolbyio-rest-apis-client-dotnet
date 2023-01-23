using System;
using System.Net.Http;
using DolbyIO.Rest.Communications;
using DolbyIO.Rest.Media;
using DolbyIO.Rest.Streaming;

namespace DolbyIO.Rest;

public sealed class DolbyIOClient : IDisposable
{
    private readonly HttpClient _httpClient;

    public Authentication Authentication { get; }

    public Capi Communications { get; }

    public Mapi Media { get; }

    public Sapi Streaming { get; }

    public DolbyIOClient()
        : this(new HttpClient())
    {
    }

    internal DolbyIOClient(HttpClient httpClient)
    {
        _httpClient = httpClient;

        Authentication = new Authentication(_httpClient);
        Communications = new Capi(_httpClient);
        Media = new Mapi(_httpClient);
        Streaming = new Sapi(_httpClient);
    }

    ~DolbyIOClient()
    {
        Dispose(false);
    }

    /// <summary>
    /// Releases the unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases the unmanaged resources.
    /// </summary>
    /// <param name="disposing">A boolean that indicates whether the method call comes from the Dispose method (true) or from a finalizer (false).</param>
    void Dispose(bool disposing)
    {
        _httpClient.Dispose();
    }
}
