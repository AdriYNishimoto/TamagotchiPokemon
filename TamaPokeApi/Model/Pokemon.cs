using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TamagotchiPokemon.Model {
    // Classe para representar um Pokémon (mascote)
    public class Pokemon {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; } // Em decímetros

        [JsonPropertyName("weight")]
        public int Weight { get; set; } // Em hectogramas

        [JsonPropertyName("abilities")]
        public List<Ability> Abilities { get; set; } = new List<Ability>(); // Inicializa como lista vazia
    }
}