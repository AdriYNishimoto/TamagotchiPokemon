using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TamagotchiPokemon.Model {
    public class Pokemon {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("weight")]
        public int Weight { get; set; }

        [JsonPropertyName("abilities")]
        public List<Ability>? Abilities { get; set; } = new List<Ability>();

        // Atributos Tamagotchi
        public int Alimentacao { get; set; } // 0 = muito fome, 10 = bem alimentado
        public int Humor { get; set; }        // 0 = muito triste, 10 = muito alegre
        public int Sono { get; set; }         // 0 = muito sonolento, 10 = bem descansado

        public Pokemon() {
            Random random = new Random();
            Alimentacao = random.Next(0, 11); // Valores de 0 a 10
            Humor = random.Next(0, 11);       // Valores de 0 a 10
            Sono = random.Next(0, 11);        // Valores de 0 a 10
        }
    }
}