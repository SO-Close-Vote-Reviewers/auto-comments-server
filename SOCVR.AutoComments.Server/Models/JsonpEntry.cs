using Newtonsoft.Json;

namespace SOCVR.AutoComments.Server.Models
{
    public class JsonpEntry
    {
        [JsonProperty("name")]
        public string Name { get; set; } = default!;

        [JsonProperty("description")]
        public string Description { get; set; } = default!;
    }
}
