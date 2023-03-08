using System.Net.Http;

namespace DolbyIO.Rest.Media;

public sealed class Mapi
{
    public Analyze Analyze { get; }

    public AnalyzeMusic AnalyzeMusic { get; }

    public AnalyzeSpeech AnalyzeSpeech { get; }

    public Diagnose Diagnose { get; }

    public Enhance Enhance { get; }

    public Io Io { get; }

    public Jobs Jobs { get; }

    public Mastering Mastering { get; }

    public Transcode Transcode { get; }

    public Webhooks Webhooks { get; }

    internal Mapi(HttpClient httpClient)
    {
        Analyze = new Analyze(httpClient);
        AnalyzeMusic = new AnalyzeMusic(httpClient);
        AnalyzeSpeech = new AnalyzeSpeech(httpClient);
        Diagnose = new Diagnose(httpClient);
        Enhance = new Enhance(httpClient);
        Io = new Io(httpClient);
        Jobs = new Jobs(httpClient);
        Mastering = new Mastering(httpClient);
        Transcode = new Transcode(httpClient);
        Webhooks = new Webhooks(httpClient);
    }
}
