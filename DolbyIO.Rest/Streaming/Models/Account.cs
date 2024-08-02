using System.Collections.Generic;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Streaming.Models;

public sealed class GeoCascade
{
    [JsonProperty("isEnabled")]
    public bool IsEnabled { get; internal set; }

    [JsonProperty("clusters")]
    public IEnumerable<string> Clusters { get; internal set; }
}

public sealed class UpdateGeoCascade
{
    [JsonProperty("isEnabled", NullValueHandling = NullValueHandling.Ignore)]
    public bool? IsEnabled { get; set; }

    [JsonProperty("clusters", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<string> Clusters { get; set; }
}

public sealed class GeoRestrictions
{
    [JsonProperty("allowedCountries")]
    public IEnumerable<string> AllowedCountries { get; internal set; }

    [JsonProperty("deniedCountries")]
    public IEnumerable<string> DeniedCountries { get; internal set; }
}

public sealed class UpdateGeoRestrictions
{
    [JsonProperty("updateAllowedCountries", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<string> UpdateAllowedCountries { get; set; }

    [JsonProperty("updateDeniedCountries", NullValueHandling = NullValueHandling.Ignore)]
    public IEnumerable<string> UpdateDeniedCountries { get; set; }
}
