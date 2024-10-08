using System.Collections.Generic;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Streaming.Models;

public sealed class ClusterLocation
{
    [JsonProperty("city")]
    public string City { get; internal set; }

    [JsonProperty("region")]
    public string Region { get; internal set; }

    [JsonProperty("country")]
    public string Country { get; internal set; }
}

public sealed class ClusterFeatures
{
    [JsonProperty("transcoding")]
    public bool Transcoding { get; internal set; }
}

public sealed class ClusterDescription
{
    [JsonProperty("id")]
    public string Id { get; internal set; }

    [JsonProperty("name")]
    public string Name { get; internal set; }

    [JsonProperty("rtmp")]
    public string Rtmp { get; internal set; }

    [JsonProperty("location")]
    public ClusterLocation Location { get; internal set; }

    [JsonProperty("features")]
    public ClusterFeatures Features { get; internal set; }
}

public sealed class ClusterResponse
{
    [JsonProperty("defaultCluster")]
    public string DefaultCluster { get; internal set; }

    [JsonProperty("availableClusters")]
    public IEnumerable<ClusterDescription> AvailableClusters { get; internal set; }
}
