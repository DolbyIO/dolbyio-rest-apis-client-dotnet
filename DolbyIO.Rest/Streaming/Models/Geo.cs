using System.Collections.Generic;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Streaming.Models;

public sealed class GeoData
{
    [JsonProperty("allowedCountries")]
    public IEnumerable<string> AllowedCountries { get; internal set; }

    [JsonProperty("deniedCountries")]
    public IEnumerable<string> DeniedCountries { get; internal set; }
}

public sealed class GeoResponse : BaseResponse<GeoData>
{
}
