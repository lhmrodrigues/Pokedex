using Flurl.Http;
using Pokedex.Configuration;
using Pokedex.Interface;
using Pokedex.Model;
using Pokedex.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Pokedex
{
    public class PokemonPageModel : BasePageModel
    {

        private readonly IPokemonService _pokemonService;
        private readonly ITypeService _typeService;

        public ObservableCollection<Pokemon> ObservablePokemonList { get; set; }
        public ObservableCollection<string> ObservableTypeList { get; set; }
        public string InfoList { get; set; }
        public string Pesquisa { get; set; }
        public int IndexList { get; set; }
        public int TotalPokemon { get; set; }

        public ICommand nextCommannd => new Command(async () => await NextCommannd());
        public ICommand previousCommannd => new Command(async () => await PreviousCommannd());
        public ICommand typeSearchCommannd => new Command(async () => await TypeSearchCommannd());
        public ICommand pokemmonDetails => new Command<Pokemon>(async (pokemonDetails) => await PokemonDetailsAsync(pokemonDetails)); // mudar a chama
        public ICommand starPokemon => new Command<Pokemon>(async (pokemonDetails) => await StarPokemon(pokemonDetails)); // mudar a chama

        public PokemonPageModel(IPokemonService pokemonService,
            ITypeService typeService)
        {
            _pokemonService = pokemonService;
            _typeService = typeService;
        }

        public override async void Init(object initData)
        {
            SetTypeList();
            SearchRangeAndSetValues(0);
        }
        public async void SearchRangeAndSetValues(int index)
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;

                List<Pokemon> pokemonList = new List<Pokemon>();
                var resultRange = await _pokemonService.GetListRange(index);

                InfoList = $"{index + 1} to {index + 20}";
                TotalPokemon = resultRange.Count;

                foreach (var results in resultRange.Results)
                {
                    Pokemon pokemon = await _pokemonService.GetPokemon(results.Name);
                    pokemon.Name = pokemon.Name.ToUpper();
                    pokemon.Url = pokemon.Sprites.Front_Default;
                    pokemon.IsFavorite = "star.png";
                    pokemonList.Add(pokemon);
                }

                ObservablePokemonList = new ObservableCollection<Pokemon>(pokemonList);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
        public async void SetTypeList()
        {
            try
            {
                List<string> aux = new List<string>();
                ListPaginationInfo typePokemonList = await _typeService.GetListType();

                foreach (var type in typePokemonList.Results)
                {
                    aux.Add(type.Name.ToUpper());
                }

                ObservableTypeList = new ObservableCollection<string>(aux);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public async void GetListPokemonType()
        {
            //await _typeService.GetListPokemonType(Pesquisa);
        }
        private async Task TypeSearchCommannd()
        {
            GetListPokemonType();
        }
        private async Task PokemonDetailsAsync(Pokemon pokemon)
        {
            await CoreMethods.PushPopupPageModel<PokemonDetailPopUpPageModel>(pokemon);
        }
        private async Task StarPokemon(Pokemon pokemon)
        {
            if (pokemon.IsFavorite == "star.png")
                pokemon.IsFavorite = "starYellow.png";
            else
                pokemon.IsFavorite = "star.png";
        }
        private async Task NextCommannd()
        {
            IndexList += 20;
            if (IndexList > TotalPokemon)
                return;

            SearchRangeAndSetValues(IndexList);
        }
        private async Task PreviousCommannd()
        {
            IndexList -= 20;
            if (IndexList < 0)
                IndexList = 0;

            SearchRangeAndSetValues(IndexList);
        }
    }
}
