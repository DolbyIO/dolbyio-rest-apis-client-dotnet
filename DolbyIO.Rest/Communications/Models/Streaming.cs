﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace DolbyIO.Rest.Communications.Models;

internal sealed class StartRtmpRequest
{
    [JsonProperty("uri")]
    public string Uri { get; set; }

    [JsonProperty("layoutUrl")]
    public string LayoutUrl { get; set; }

    [JsonProperty("layoutName")]
    public string LayoutName { get; set; }
}

internal sealed class StartRtsRequest
{
    [JsonProperty("streamName")]
    public string StreamName { get; set; }

    [JsonProperty("publishingToken")]
    public string PublishingToken { get; set; }

    [JsonProperty("layoutUrl")]
    public string LayoutUrl { get; set; }

    [JsonProperty("layoutName")]
    public string LayoutName { get; set; }
}
