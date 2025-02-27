using RestSharp;
using System.Text.Json;
using TamagotchiPokemon;

namespace TamagotchiPokemon {
    public class TamagotchiEvolution {
        private readonly RestClient _client;

        public TamagotchiEvolution() {
            _client = new RestClient();
        }

        // Busca a cadeia evolutiva de um Pokémon pelo ID
        public async Task<EvolutionChain> GetEvolutionChain(int pokemonId) {
            string evolutionUrl = $"https://pokeapi.co/api/v2/evolution-chain/{pokemonId}/";
            var request = new RestRequest(evolutionUrl, Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful) {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<EvolutionChain>(response.Content, options);
            }
            return null;
        }

        // Obtém o próximo estágio evolutivo de um Pokémon com base no nome atual
        public async Task<Pokemon> GetNextEvolution(string currentPokemonName) {
            // Mapeamento manual das evoluções (pode ser expandido com base na cadeia evolutiva)
            Dictionary<string, string> evolutionMap = new()
            {
                { "bulbasaur", "ivysaur" },
                { "ivysaur", "venusaur" },
                { "charmander", "charmeleon" },
                { "charmeleon", "charizard" },
                { "squirtle", "wartortle" },
                { "wartortle", "blastoise" }
            };

            if (evolutionMap.TryGetValue(currentPokemonName.ToLower(), out string nextEvolutionName)) {
                return await FetchPokemonDetails($"https://pokeapi.co/api/v2/pokemon/{nextEvolutionName}/");
            }
            return null;
        }

        private async Task<Pokemon> FetchPokemonDetails(string urlOrName) {
            string url = urlOrName.StartsWith("http") ? urlOrName : $"https://pokeapi.co/api/v2/pokemon/{urlOrName.ToLower()}/";
            var request = new RestRequest(url, Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful) {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<Pokemon>(response.Content, options);
            }
            return null;
        }

        // Método para evoluir um Pokémon com base em critérios (ex.: nível, felicidade)
        public async Task<Pokemon> EvolvePokemon(Pokemon currentPokemon, int level = 0, int happiness = 0) {
            if (currentPokemon == null) return null;

            if (currentPokemon.Name == "bulbasaur" && level >= 16) {
                return await GetNextEvolution("bulbasaur");
            }
            else if (currentPokemon.Name == "ivysaur" && level >= 32) {
                return await GetNextEvolution("ivysaur");
            }
            else if (currentPokemon.Name == "charmander" && level >= 16) {
                return await GetNextEvolution("charmander");
            }
            else if (currentPokemon.Name == "charmeleon" && level >= 36) {
                return await GetNextEvolution("charmeleon");
            }
            else if (currentPokemon.Name == "squirtle" && level >= 16) {
                return await GetNextEvolution("squirtle");
            }
            else if (currentPokemon.Name == "wartortle" && level >= 36) {
                return await GetNextEvolution("wartortle");
            }

            return currentPokemon; // Não evolui se critérios não atendidos
        }
    }
}