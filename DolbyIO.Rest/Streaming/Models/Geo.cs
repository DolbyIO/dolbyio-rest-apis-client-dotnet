using System.Collections.Generic;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Streaming.Models;

public sealed class GeoUpdate
{
    [JsonProperty("updateAllowedCountries", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<string> UpdateAllowedCountries { get; set; }

    [JsonProperty("updateDeniedCountries", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<string> UpdateDeniedCountries { get; set; }
}

public sealed class GeoResponse
{
    [JsonProperty("allowedCountries")]
    public IEnumerable<string> AllowedCountries { get; internal set; }

    [JsonProperty("deniedCountries")]
    public IEnumerable<string> DeniedCountries { get; internal set; }
}
