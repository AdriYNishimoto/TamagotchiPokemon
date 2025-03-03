using RestSharp;
using System.Text.Json;
using System.Threading.Tasks;

namespace TamagotchiPokemon.Model {
    public class PokemonService {
        private readonly RestClient _client;

        public PokemonService() {
            _client = new RestClient();
        }

        public async Task<Pokemon> FetchPokemonDetails(string url) {
            var request = new RestRequest(url, Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful) {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<Pokemon>(response.Content, options);
            }
            return null;
        }

        public async Task<string> GetJsonContent(string pokemonName) {
            string url = $"https://pokeapi.co/api/v2/pokemon/{pokemonName.ToLower()}/";
            var request = new RestRequest(url, Method.Get);
            var response = await _client.ExecuteAsync(request);
            return response.IsSuccessful ? response.Content : string.Empty;
        }

        public async Task<EvolutionChain> FetchEvolutionChain(int pokemonId) {
            string evolutionUrl = $"https://pokeapi.co/api/v2/evolution-chain/{pokemonId}/";
            var request = new RestRequest(evolutionUrl, Method.Get);
            var response = await _client.ExecuteAsync(request);

            if (response.IsSuccessful) {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                return JsonSerializer.Deserialize<EvolutionChain>(response.Content, options);
            }
            return null;
        }
    }
}