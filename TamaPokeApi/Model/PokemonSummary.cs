using System.Text.Json.Serialization;

namespace TamagotchiPokemon.Model {
    // Classe para o resumo de cada Pokémon na lista inicial (apenas name e url)
    public class PokemonSummary {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}