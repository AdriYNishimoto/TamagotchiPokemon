using RestSharp;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using TamagotchiPokemon.Model;

namespace TamagotchiPokemon.View {
    public class TamagotchiView {
        private readonly PokemonService _pokemonService;

        public TamagotchiView() {
            _pokemonService = new PokemonService();
        }

        public async Task<string> GetUserName() {
            Console.WriteLine("\n  _______       __  __          _____  ____ _______ _____ _    _ _____ \r\n |__   __|/\\   |  \\/  |   /\\   / ____|/ __ \\__   __/ ____| |  | |_   _|\r\n    | |  /  \\  | \\  / |  /  \\ | |  __| |  | | | | | |    | |__| | | |  \r\n    | | / /\\ \\ | |\\/| | / /\\ \\| | |_ | |  | | | | | |    |  __  | | |  \r\n    | |/ ____ \\| |  | |/ ____ \\ |__| | |__| | | | | |____| |  | |_| |_ \r\n    |_/_/    \\_\\_|  |_/_/    \\_\\_____|\\____/  |_|  \\_____|_|  |_|_____|\r\n                                                                       \r\n                                                                       ");
            Console.Write("\nQual é seu nome? ");
            string name = Console.ReadLine();
            return name.ToUpper();
        }

        public async Task<string> ShowMainMenu() {
            Console.WriteLine("\n------------------------ MENU ------------------");
            Console.WriteLine("1 - Adotar um mascote virtual");
            Console.WriteLine("2 - Ver seus mascotes");
            Console.WriteLine("3 - Sair do Jogo");
            Console.WriteLine("------------------------------------------------");
            Console.Write("O que você deseja? ");
            return Console.ReadLine();
        }

        public async Task<string> ShowAdoptionMenu() {
            Console.WriteLine("\n---------------- ADOTAR UM MASCOTE -------------");
            Console.WriteLine("Escolha uma espécie:");
            Console.WriteLine("1 - Bulbasaur");
            Console.WriteLine("2 - Charmander");
            Console.WriteLine("3 - Squirtle");
            Console.WriteLine("------------------------------------------------");
            Console.Write("Qual espécie você deseja? ");
            return Console.ReadLine();
        }

        public void ShowInvalidOption() {
            Console.WriteLine("\nOpção inválida! Tente novamente.");
        }

        public void ShowInvalidSpecies() {
            Console.WriteLine("\nEspécie inválida! Voltando ao menu principal...");
        }

        public async Task<string> ShowPokemonDetails(Pokemon pokemon) {
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
            return Console.ReadLine();
        }

        public async Task ShowMoreDetails(Pokemon pokemon) {
            Console.WriteLine($"\nMais detalhes sobre {pokemon.Name.ToUpper()}:");
            Console.WriteLine($"Tipo: {await GetPokemonTypes(pokemon.Name)}");
            Console.WriteLine("Este Pokémon é perfeito para aventuras no seu Tamagotchi!");
        }

        public void ShowAdoptionSuccess(string pokemonName) {
            Console.WriteLine($"\nMascote adotado com sucesso, o ovo está chocando: {pokemonName.ToUpper()}!");
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
        }

        public void ShowAdoptedPokemons(List<Pokemon> pokemons) {
            if (pokemons.Count == 0) {
                Console.WriteLine("\nVocê ainda não adotou nenhum mascote! Vamos adotar um?");
            }
            else {
                Console.WriteLine("\n------------- SEUS MASCOTES ADOTADOS -----------");
                foreach (var pokemon in pokemons) {
                    Console.WriteLine($"\nNome: {pokemon.Name.ToUpper()}");
                    Console.WriteLine($"Altura: {(pokemon.Height / 10.0)} m");
                    Console.WriteLine($"Peso: {(pokemon.Weight / 10.0)} kg");
                }
                Console.WriteLine("------------------------------------------------");
            }
        }

        public void ShowGoodbyeMessage(string userName) {
            Console.WriteLine($"\nAté logo, {userName}! Obrigado por jogar!");
        }

        private async Task<string> GetPokemonTypes(string pokemonName) {
            string jsonContent = await _pokemonService.GetJsonContent(pokemonName);
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


