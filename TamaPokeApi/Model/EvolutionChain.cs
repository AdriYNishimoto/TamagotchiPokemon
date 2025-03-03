using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TamagotchiPokemon.Model {
    // Classe para representar a cadeia evolutiva de um Pokémon
    public class EvolutionChain {
        [JsonPropertyName("chain")]
        public EvolutionLink Chain { get; set; }
    }
}