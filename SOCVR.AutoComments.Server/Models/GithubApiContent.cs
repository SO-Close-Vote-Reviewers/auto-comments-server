using Newtonsoft.Json;

namespace SOCVR.AutoComments.Server.Models
{
    public class GithubApiContent
    {
        [JsonProperty("name")]
        public string Name { get; set; } = default!;

        [JsonProperty("path")]
        public string Path { get; set; } = default!;

        [JsonProperty("type")]
        public string Type { get; set; } = default!;

        [JsonProperty("content")]
        public string? Content { get; set; }
    }
}
