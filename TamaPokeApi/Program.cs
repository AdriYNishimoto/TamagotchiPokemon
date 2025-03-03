using TamagotchiPokemon.Controller;

class Program {
    static async Task Main(string[] args) {
        var controller = new TamagotchiController();
        await controller.Jogar();
    }
}