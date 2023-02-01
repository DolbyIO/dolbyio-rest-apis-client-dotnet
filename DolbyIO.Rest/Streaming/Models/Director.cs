using System.Collections.Generic;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Streaming.Models;

public sealed class IceServer
{
    [JsonProperty("urls")]
    public IEnumerable<string> Urls { get; internal set; }

    [JsonProperty("userName")]
    public string UserName { get; internal set; }

    [JsonProperty("credential")]
    public string Credential { get; internal set; }
}

public class SubscribeResponse
{
    [JsonProperty("urls")]
    public IEnumerable<string> Urls { get; internal set; }

    [JsonProperty("jwt")]
    public string Jwt { get; internal set; }

    [JsonProperty("iceServers")]
    public IEnumerable<IceServer> IceServers { get; internal set; }

    [JsonProperty("streamAccountId")]
    public string StreamAccountId { get; internal set; }
}

public sealed class PublishResponse : SubscribeResponse
{
    [JsonProperty("subscribeRequiresAuth")]
    public bool SubscribeRequiresAuth { get; internal set; }
}
