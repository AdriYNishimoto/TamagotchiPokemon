using RestSharp;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using TamagotchiPokemon;

namespace TamagotchiPokemon {
    public class Menu {
        private readonly RestClient _client;
        private List<Pokemon> adoptedPokemons = new List<Pokemon>();
        private string userName;

        public Menu() {
            _client = new RestClient();
        }

        public async Task Start() {
            // Boas-vindas e leitura do nome do usuário
            Console.WriteLine("  _______       __  __          _____  ____ _______ _____ _    _ _____ \r\n |__   __|/\\   |  \\/  |   /\\   / ____|/ __ \\__   __/ ____| |  | |_   _|\r\n    | |  /  \\  | \\  / |  /  \\ | |  __| |  | | | | | |    | |__| | | |  \r\n    | | / /\\ \\ | |\\/| | / /\\ \\| | |_ | |  | | | | | |    |  __  | | |  \r\n    | |/ ____ \\| |  | |/ ____ \\ |__| | |__| | | | | |____| |  | |_| |_ \r\n    |_/_/    \\_\\_|  |_/_/    \\_\\_____|\\____/  |_|  \\_____|_|  |_|_____|\r\n                                                                       \r\n                                                                       ");
            Console.Write("\nQual é seu nome? ");
            userName = Console.ReadLine();
            Console.WriteLine($"\nOlá, {userName}! Bem-vindo ao Tamagotchi Pokémon!");

            while (true) {
                // Exibir menu principal
                Console.WriteLine("\n------------------------ MENU ------------------");
                Console.WriteLine("1 - Adotar um mascote virtual");
                Console.WriteLine("2 - Ver seus mascotes");
                Console.WriteLine("3 - Sair do Jogo");
                Console.WriteLine("------------------------------------------------");
                Console.Write("O que você deseja? ");

                string choice = Console.ReadLine();
                switch (choice) {
                    case "1":
                        await ShowAdoptionMenu();
                        break;
                    case "2":
                        ShowAdoptedPokemons();
                        break;
                    case "3":
                        Console.WriteLine($"\nAté logo, {userName}! Obrigado por jogar!");
                        return;
                    default:
                        Console.WriteLine("\nOpção inválida! Tente novamente.");
                        break;
                }
            }
        }

        private async Task ShowAdoptionMenu() {
            Console.WriteLine("\n---------------- ADOTAR UM MASCOTE -------------");
            Console.WriteLine("Escolha uma espécie:");
            Console.WriteLine("1 - Bulbasaur");
            Console.WriteLine("2 - Charmander");
            Console.WriteLine("3 - Squirtle");
            Console.WriteLine("------------------------------------------------");
            Console.Write("Qual espécie você deseja? ");

            string speciesChoice = Console.ReadLine();
            Pokemon pokemon = null;

            switch (speciesChoice) {
                case "1":
                    pokemon = await FetchPokemonDetails("https://pokeapi.co/api/v2/pokemon/1/"); // Bulbasaur
                    break;
                case "2":
                    pokemon = await FetchPokemonDetails("https://pokeapi.co/api/v2/pokemon/4/"); // Charmander
                    break;
                case "3":
                    pokemon = await FetchPokemonDetails("https://pokeapi.co/api/v2/pokemon/7/"); // Squirtle
                    break;
                default:
                    Console.WriteLine("\nEspécie inválida! Voltando ao menu principal...");
                    return;
            }

            if (pokemon != null) {
                while (true) {
                    Console.WriteLine("\n---------------- DETALHES DO MASCOTE -----------");
                    Console.WriteLine($"Nome Pokémon: {pokemon.Name.ToUpper()}");
                    Console.WriteLine($"Altura: {(pokemon.Height / 10.0)} m");
                    Console.WriteLine($"Peso: {(pokemon.Weight / 10.0)} kg");
                    Console.WriteLine("Habilidades:");
                    if (pokemon.Abilities != null && pokemon.Abilities.Any()) {
                        foreach (var ability in pokemon.Abilities) {
                            Console.WriteLine($"- {ability.Name}");
                        }
                    }
                    else {
                        Console.WriteLine("- Nenhuma habilidade encontrada.");
                    }
                    Console.WriteLine("------------------------------------------------");
                    Console.WriteLine("1 - Saber mais sobre o Pokémon");
                    Console.WriteLine("2 - Adotar este Pokémon");
                    Console.WriteLine("3 - Voltar");
                    Console.Write("O que você deseja? ");

                    string actionChoice = Console.ReadLine();
                    switch (actionChoice) {
                        case "1":
                            Console.WriteLine($"\nMais detalhes sobre {pokemon.Name.ToUpper()}:");
                            Console.WriteLine($"Tipo: {GetPokemonTypes(await GetJsonContent(pokemon.Name))}"); // Usar função auxiliar para tipos
                            Console.WriteLine("Este Pokémon é perfeito para aventuras no seu Tamagotchi!");
                            break;
                        case "2":
                            adoptedPokemons.Add(pokemon);
                            Console.WriteLine($"\nMascote adotado com sucesso, o ovo está chocando: {pokemon.Name.ToUpper()}!");
                            Console.WriteLine(@"
          ████████          
        ██        ██        
      ██▒▒▒▒        ██      
    ██▒▒▒▒▒▒      ▒▒▒▒██    
    ██▒▒▒▒▒▒      ▒▒▒▒██    
  ██  ▒▒▒▒        ▒▒▒▒▒▒██  
  ██                ▒▒▒▒██  
██▒▒      ▒▒▒▒▒▒          ██
██      ▒▒▒▒▒▒▒▒▒▒        ██
██      ▒▒▒▒▒▒▒▒▒▒    ▒▒▒▒██
██▒▒▒▒  ▒▒▒▒▒▒▒▒▒▒  ▒▒▒▒▒▒██
  ██▒▒▒▒  ▒▒▒▒▒▒    ▒▒▒▒██  
  ██▒▒▒▒            ▒▒▒▒██  
    ██▒▒              ██    
      ████        ████      
          ████████           
");
                            return;
                        case "3":
                            return;
                        default:
                            Console.WriteLine("\nOpção inválida! Tente novamente.");
                            break;
                    }
                }
            }
        }

        private void ShowAdoptedPokemons() {
            if (adoptedPokemons.Count == 0) {
                Console.WriteLine("\nVocê ainda não adotou nenhum mascote! Vamos adotar um?");
            }
            else {
                Console.WriteLine("\n------------- SEUS MASCOTES ADOTADOS -----------");
                foreach (var pokemon in adoptedPokemons) {
                    Console.WriteLine($"\nNome: {pokemon.Name.ToUpper()}");
                    Console.WriteLine($"Altura: {(pokemon.Height / 10.0)} m");
                    Console.WriteLine($"Peso: {(pokemon.Weight / 10.0)} kg");
                }
                Console.WriteLine("------------------------------------------------");
            }
        }

        private async Task<Pokemon> FetchPokemonDetails(string url) {
            var request = new RestRequest(url, Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful) {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<Pokemon>(response.Content, options);
            }
            return null;
        }

        private async Task<string> GetJsonContent(string pokemonName) {
            string url = $"https://pokeapi.co/api/v2/pokemon/{pokemonName.ToLower()}/";
            var request = new RestRequest(url, Method.Get);
            var response = await _client.ExecuteAsync(request);
            return response.IsSuccessful ? response.Content : string.Empty;
        }

        private string GetPokemonTypes(string jsonContent) {
            if (string.IsNullOrEmpty(jsonContent)) return "Tipo não encontrado";

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
}





