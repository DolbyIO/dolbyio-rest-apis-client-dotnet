using Newtonsoft.Json;

namespace DolbyIO.Rest.Communications.Models
{
    internal sealed class StartRecordingRequest
    {
        [JsonProperty("layoutUrl")]
        public string LayoutUrl { get; set; }
    }
}
