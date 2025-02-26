using RestSharp;
using Newtonsoft.Json.Linq;

class Program {
    static void Main(string[] args) {
        // URL da API com limite de 9 Pokémon
        string url = "https://pokeapi.co/api/v2/pokemon?limit=9";

        // Criar o cliente RestSharp
        var client = new RestClient(url);

        // Criar a requisição GET
        var request = new RestRequest("", Method.Get);

        // Executar a requisição
        var response = client.Execute(request);

        // Verificar se deu certo
        if (response.IsSuccessful) {
            // Parsear o JSON usando Newtonsoft.Json
            JObject jsonResponse = JObject.Parse(response.Content);
            var pokemons = jsonResponse["results"];

            // Exibir a lista de Pokémon
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
        // URL para detalhes de um Pokémon específico
        string url = $"https://pokeapi.co/api/v2/pokemon/{nome.ToLower()}";

        // Criar o cliente RestSharp
        var client = new RestClient(url);

        // Criar a requisição GET
        var request = new RestRequest("", Method.Get);

        // Executar a requisição
        var response = client.Execute(request);

        // Verificar se deu certo
        if (response.IsSuccessful) {
            // Parsear o JSON
            JObject pokemonData = JObject.Parse(response.Content);
            string nomePokemon = pokemonData["name"].ToString();
            double altura = pokemonData["height"] != null ? pokemonData["height"].Value<double>() : 0; // Converte para double
            double peso = pokemonData["weight"] != null ? pokemonData["weight"].Value<double>() : 0; // Converte para double

            // Extrair os tipos do Pokémon (pode ter mais de um tipo)
            var types = pokemonData["types"];
            string tipagem = "";
            if (types != null && types.HasValues) {
                tipagem = string.Join(", ", types.Select(t => t["type"]["name"].ToString()));
            }
            else {
                tipagem = "Tipo não encontrado";
            }

            // Exibir detalhes
            Console.WriteLine($"\nDetalhes do Pokémon {nomePokemon.ToUpper()}:");
            Console.WriteLine($"Tipo: {tipagem}");
            Console.WriteLine($"Altura: {altura / 10} m"); // A API retorna altura em decímetros (dividimos por 10 para metros)
            Console.WriteLine($"Peso: {peso / 10} kg");   // A API retorna peso em hectogramas (dividimos por 10 para kg)
        }
        else {
            Console.WriteLine("Não foi possível obter detalhes do Pokémon.");
        }
    }
}