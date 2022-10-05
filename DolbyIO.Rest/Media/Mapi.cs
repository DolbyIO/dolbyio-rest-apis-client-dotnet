using System.Net.Http;

namespace DolbyIO.Rest.Media;

public class Mapi
{
    public Analyze Analyze { get; }

    internal Mapi(HttpClient httpClient)
    {
        Analyze = new Analyze(httpClient);
    }
}
