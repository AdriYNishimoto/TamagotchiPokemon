using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TamagotchiPokemon.Model {
    public class EvolutionLink {
        [JsonPropertyName("species")]
        public Species Species { get; set; }

        [JsonPropertyName("evolves_to")]
        public List<EvolutionLink> EvolvesTo { get; set; } = new List<EvolutionLink>();
    }
}