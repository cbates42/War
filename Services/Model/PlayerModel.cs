using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Services.Model
{
    public class PlayerModel
    {
        [JsonPropertyName("name")]
        public string name { get; set; }

        [JsonPropertyName("turns")]
        public int turns { get; set; }

        [JsonPropertyName("id")]
        public int id { get; set; }
    }
}
