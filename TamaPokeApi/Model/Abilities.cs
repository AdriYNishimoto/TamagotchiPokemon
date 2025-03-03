using System.Text.Json.Serialization;

namespace TamagotchiPokemon.Model {
    // Classe para representar a habilidade interna (dentro de "ability")
    public class AbilityDetail {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }

    // Classe para representar as habilidades de um Pokémon
    public class Ability {
        [JsonPropertyName("ability")]
        public AbilityDetail? AbilityDetail { get; set; }

        [JsonPropertyName("is_hidden")]
        public bool IsHidden { get; set; }

        [JsonPropertyName("slot")]
        public int Slot { get; set; }

        // Propriedade para acessar o nome da habilidade de forma direta
        public string? Name => AbilityDetail?.Name ?? "Nome não encontrado";
    }
}