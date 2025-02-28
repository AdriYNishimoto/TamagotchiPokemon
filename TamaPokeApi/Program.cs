using TamagotchiPokemon;

class Program {
    static async Task Main(string[] args) {
        var menu = new Menu();
        await menu.Start();
    }
}