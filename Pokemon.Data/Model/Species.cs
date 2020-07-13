using Newtonsoft.Json;
using System.Collections.Generic;

namespace Pokemon.Data.Model
{
    public class Species
    {
        [JsonProperty("flavor_text_entries")]
        public IList<FlavorTextEntries> FlavorTextEntries { get; set; }
    }
}
