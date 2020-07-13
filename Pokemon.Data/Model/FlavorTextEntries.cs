using Newtonsoft.Json;

namespace Pokemon.Data.Model
{
    public class FlavorTextEntries
    {
        [JsonProperty("flavor_text")]
        public string FlavorText { get; set; }

        public Language Language { get; set; }
    }
}