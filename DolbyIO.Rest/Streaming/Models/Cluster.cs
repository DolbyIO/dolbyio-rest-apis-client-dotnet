using System.Collections.Generic;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Streaming.Models;

public sealed class AvailableCluster
{
    [JsonProperty("id")]
    public string Id { get; internal set; }

    [JsonProperty("name")]
    public string Name { get; internal set; }

    [JsonProperty("rtmp")]
    public string Rtmp { get; internal set; }
}

public sealed class ClusterData
{
    [JsonProperty("defaultCluster")]
    public string DefaultCluster { get; internal set; }

    [JsonProperty("availableClusters")]
    public IEnumerable<AvailableCluster> AvailableClusters { get; internal set; }
}

public sealed class ClusterResponse : BaseResponse<ClusterData>
{
}
