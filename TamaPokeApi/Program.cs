using RestSharp;
using System.Text.Json;
using TamagotchiPokemon; 

class Program {
    static void Main(string[] args) {
        string url = "https://pokeapi.co/api/v2/pokemon?limit=9";
        var client = new RestClient(url);
        var request = new RestRequest("", Method.Get);
        var response = client.Execute(request);

        if (response.IsSuccessful) {
            var options = new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            };
            PokemonResponse pokemonList = JsonSerializer.Deserialize<PokemonResponse>(response.Content, options);

            Console.WriteLine("Lista de Mascotes Virtuais (Pokémon) Disponíveis para Adoção:");
            Console.WriteLine("------------------------------------------------");

            foreach (var pokemon in pokemonList.Results) {
                Console.WriteLine($"\nNome: {pokemon.Name.ToUpper()}");
                Console.WriteLine($"Altura: {(pokemon.Height / 10.0)} m"); 
                Console.WriteLine($"Peso: {(pokemon.Weight / 10.0)} kg");  
                Console.WriteLine("Habilidades:");
                if (pokemon.Abilities != null && pokemon.Abilities.Any()) 
                {
                    foreach (var ability in pokemon.Abilities) {
                        Console.WriteLine($"- {ability.Name} (Escondida: {ability.IsHidden}, Slot: {ability.Slot})");
                    }
                }
                else {
                    Console.WriteLine("- Nenhuma habilidade encontrada.");
                }
                Console.WriteLine("------------------------------------------------");
            }
        }
        else {
            Console.WriteLine("Erro ao buscar dados da API: " + response.ErrorMessage);
        }

        Console.WriteLine("\nPressione qualquer tecla para sair...");
        Console.ReadKey();
    }

    static void BuscarDetalhesPokemon(string nome) {
        string url = $"https://pokeapi.co/api/v2/pokemon/{nome.ToLower()}";
        var client = new RestClient(url);
        var request = new RestRequest("", Method.Get);
        var response = client.Execute(request);

        if (response.IsSuccessful) {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            Pokemon pokemon = JsonSerializer.Deserialize<Pokemon>(response.Content, options);

            Console.WriteLine($"\nDetalhes do Pokémon {pokemon.Name.ToUpper()}:");
            Console.WriteLine($"Tipo: {GetPokemonTypes(response.Content)}"); 
            Console.WriteLine($"Altura: {(pokemon.Height / 10.0)} m");
            Console.WriteLine($"Peso: {(pokemon.Weight / 10.0)} kg");
            Console.WriteLine("Habilidades:");
            if (pokemon.Abilities != null && pokemon.Abilities.Any())
            {
                foreach (var ability in pokemon.Abilities) {
                    Console.WriteLine($"- {ability.Name} (Escondida: {ability.IsHidden}, Slot: {ability.Slot})");
                }
            }
            else {
                Console.WriteLine("- Nenhuma habilidade encontrada.");
            }
        }
        else {
            Console.WriteLine("Não foi possível obter detalhes do Pokémon.");
        }
    }

    static string GetPokemonTypes(string jsonContent) {
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var pokemonData = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonContent, options);
        if (pokemonData.TryGetValue("types", out JsonElement typesElement) && typesElement.ValueKind == JsonValueKind.Array) {
            var types = new List<string>();
            foreach (var type in typesElement.EnumerateArray()) {
                if (type.TryGetProperty("type", out JsonElement typeObj) && typeObj.TryGetProperty("name", out JsonElement nameElement)) {
                    types.Add(nameElement.GetString());
                }
            }
            return string.Join(", ", types);
        }
        return "Tipo não encontrado";
    }
}