using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Services.Model
{
    public class CardModel
    {
        [JsonPropertyName("TurnNum")]
        public int turnNum { get; set; }
        [JsonPropertyName("CardVal")]
        public int cardVal { get; set; }

        [JsonPropertyName("Suit")]
        public string Suit { get; set; }

        [JsonPropertyName("id")]
        public int id { get; set; }


    }
}
