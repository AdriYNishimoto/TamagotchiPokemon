using RestSharp;
using System.Text.Json;
using TamagotchiPokemon;

class Program {
    static async Task Main(string[] args) {
        string[] pokemonUrls = new[]
        {
            "https://pokeapi.co/api/v2/pokemon/1/", // Bulbasaur
            "https://pokeapi.co/api/v2/pokemon/4/", // Charmander
            "https://pokeapi.co/api/v2/pokemon/7/"  // Squirtle
        };

        var client = new RestClient();

        Console.WriteLine("Lista de Mascotes Virtuais (Pokémon) Disponíveis para Adoção:");
        Console.WriteLine("------------------------------------------------");

        var tasks = pokemonUrls.Select(url => FetchPokemonDetails(url));
        var detailedPokemons = await Task.WhenAll(tasks);

        foreach (var pokemon in detailedPokemons) {
            if (pokemon != null) {
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

        // Futura implementação (Dias 3-7): Para evoluir os Pokémon, podemos usar FetchEvolutionChain para obter as cadeias evolutivas
        // Exemplo: EvolutionChain bulbasaurChain = await FetchEvolutionChain(1);
        // Isso retornará Bulbasaur → Ivysaur → Venusaur. Podemos adicionar lógica para "evoluir" o mascote com base em interações (ex.: nível, felicidade).

        Console.WriteLine("\nPressione qualquer tecla para sair...");
        Console.ReadKey();
    }

    static async Task<Pokemon> FetchPokemonDetails(string url) {
        var client = new RestClient(url);
        var request = new RestRequest("", Method.Get);
        var response = await client.ExecuteAsync(request);

        if (response.IsSuccessful) {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<Pokemon>(response.Content, options);
        }
        return null;
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
}