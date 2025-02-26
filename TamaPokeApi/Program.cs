using RestSharp;
using Newtonsoft.Json.Linq;

class Program {
    static void Main(string[] args) {

        string url = "https://pokeapi.co/api/v2/pokemon?limit=9";
        var client = new RestClient(url);
        var request = new RestRequest("", Method.Get);
        var response = client.Execute(request);

        if (response.IsSuccessful) {
            JObject jsonResponse = JObject.Parse(response.Content);
            var pokemons = jsonResponse["results"];

            Console.WriteLine("Lista de 9 Pokémon iniciais:");
            foreach (var pokemon in pokemons) {
                Console.WriteLine($"- {pokemon["name"]}");
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
            
            JObject pokemonData = JObject.Parse(response.Content);
            string nomePokemon = pokemonData["name"].ToString();
            double altura = pokemonData["height"] != null ? pokemonData["height"].Value<double>() : 0; 
            double peso = pokemonData["weight"] != null ? pokemonData["weight"].Value<double>() : 0; 

            
            var types = pokemonData["types"];
            string tipagem = "";
            if (types != null && types.HasValues) {
                tipagem = string.Join(", ", types.Select(t => t["type"]["name"].ToString()));
            }
            else {
                tipagem = "Tipo não encontrado";
            }

            Console.WriteLine($"\nDetalhes do Pokémon {nomePokemon.ToUpper()}:");
            Console.WriteLine($"Tipo: {tipagem}");
            Console.WriteLine($"Altura: {altura / 10} m"); 
            Console.WriteLine($"Peso: {peso / 10} kg");   
        }
        else {
            Console.WriteLine("Não foi possível obter detalhes do Pokémon.");
        }
    }
}