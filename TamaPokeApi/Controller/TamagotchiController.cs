using RestSharp;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using TamagotchiPokemon.Model;
using TamagotchiPokemon.View;

namespace TamagotchiPokemon.Controller {
    public class TamagotchiController {
        private readonly PokemonService _pokemonService;
        private readonly TamagotchiView _view;
        private List<Pokemon> adoptedPokemons = new List<Pokemon>();
        private string userName;

        public TamagotchiController() {
            _pokemonService = new PokemonService();
            _view = new TamagotchiView();
        }

        public async Task Jogar() {
            // Boas-vindas e leitura do nome (usando a View)
            userName = await _view.GetUserName();
            Console.WriteLine($"\nOlá, {userName}! Bem-vindo ao Tamagotchi Pokémon!");

            int jogar = 1;
            while (jogar == 1) {
                string opcaoUsuario = await _view.ShowMainMenu();
                switch (opcaoUsuario) {
                    case "1":
                        await AdotarMascote();
                        break;
                    case "2":
                        _view.ShowAdoptedPokemons(adoptedPokemons);
                        break;
                    case "3":
                        jogar = 0;
                        _view.ShowGoodbyeMessage(userName);
                        break;
                    default:
                        _view.ShowInvalidOption();
                        break;
                }
            }
        }

        private async Task AdotarMascote() {
            string speciesChoice = await _view.ShowAdoptionMenu();
            Pokemon pokemon = null;

            switch (speciesChoice) {
                case "1":
                    pokemon = await _pokemonService.FetchPokemonDetails("https://pokeapi.co/api/v2/pokemon/1/"); // Bulbasaur
                    break;
                case "2":
                    pokemon = await _pokemonService.FetchPokemonDetails("https://pokeapi.co/api/v2/pokemon/4/"); // Charmander
                    break;
                case "3":
                    pokemon = await _pokemonService.FetchPokemonDetails("https://pokeapi.co/api/v2/pokemon/7/"); // Squirtle
                    break;
                default:
                    _view.ShowInvalidSpecies();
                    return;
            }

            if (pokemon != null) {
                string action = await _view.ShowPokemonDetails(pokemon);
                switch (action) {
                    case "1":
                        await _view.ShowMoreDetails(pokemon);
                        break;
                    case "2":
                        adoptedPokemons.Add(pokemon);
                        _view.ShowAdoptionSuccess(pokemon.Name);
                        break;
                    case "3":
                        return;
                    default:
                        _view.ShowInvalidOption();
                        break;
                }
            }
        }
    }
}