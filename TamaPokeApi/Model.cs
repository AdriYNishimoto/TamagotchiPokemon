using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TamagotchiPokemon {
    public class Ability {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("is_hidden")]
        public bool IsHidden { get; set; }

        [JsonPropertyName("slot")]
        public int Slot { get; set; }
    }

    public class Pokemon {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; } 

        [JsonPropertyName("weight")]
        public int Weight { get; set; } 

        [JsonPropertyName("abilities")]
        public List<Ability> Abilities { get; set; } = new List<Ability>(); 
    }

    public class PokemonResponse {
        [JsonPropertyName("results")]
        public List<Pokemon> Results { get; set; } = new List<Pokemon>(); 
    }
}