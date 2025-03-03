using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TamagotchiPokemon.Model {
    // Classe para a resposta inicial da API (lista de Pokémon)
    public class PokemonListResponse {
        [JsonPropertyName("results")]
        public List<PokemonSummary> Results { get; set; } = new List<PokemonSummary>();
    }
}