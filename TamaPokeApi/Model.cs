using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TamagotchiPokemon {
    public class AbilityDetail {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class Ability {
        [JsonPropertyName("ability")]
        public AbilityDetail AbilityDetail { get; set; }

        [JsonPropertyName("is_hidden")]
        public bool IsHidden { get; set; }

        [JsonPropertyName("slot")]
        public int Slot { get; set; }

        public string Name => AbilityDetail?.Name ?? "Nome não encontrado";
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

    public class PokemonListResponse {
        [JsonPropertyName("results")]
        public List<PokemonSummary> Results { get; set; } = new List<PokemonSummary>();
    }

    public class PokemonSummary {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}