using Flurl.Http;
using Pokedex.Configuration;
using Pokedex.Interface;
using Pokedex.Model;
using Pokedex.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Pokedex
{
    public class PokemonPageModel: BasePageModel
    {
        public ObservableCollection<Pokemon> ObservablePokemonList { get; set; }
        public ObservableCollection<string> ObservableTypeList { get; set; }
        public string Pesquisa { get; set; }

        public ICommand typeSearchCommannd => new Command(async () => await TypeSearchCommannd());
        public ICommand pokemmonDetails => new Command<Pokemon>(async (pokemonDetails) => await PokemonDetailsAsync(pokemonDetails)); // mudar a chama

        private readonly IPokemonService _pokemonService;
        private readonly ITypeService _typeService;

        public PokemonPageModel(IPokemonService pokemonService,
            ITypeService typeService)
        {
            _pokemonService = pokemonService;
            _typeService = typeService;
            SetTypeList();
        }

        public override async void Init(object initData)
        {
            await TypeSearchCommannd();
            await SearchRangeAndSetValues(0);
        }

        public async Task SearchRangeAndSetValues(int index)
        {
            List<Pokemon> pokemonList = new List<Pokemon>();
            var result = await _pokemonService.GetFromApi<ListPaginationInfo>($"/?limit={index}&offset={UrlConfiguration.OffSet}");

            foreach (var results in result.Results)
            {
                Pokemon pokemon = await _pokemonService.GetFromApi<Pokemon>($"/{results.Name}/");
                pokemon.Name = pokemon.Name.ToUpper();
                pokemon.Url = pokemon.Sprites.Front_Default;

                pokemonList.Add(pokemon);
            }

            ObservablePokemonList = new ObservableCollection<Pokemon>(pokemonList);
        }

        public async void SetTypeList()
        {
            List<string> aux = new List<string>();
            ListPaginationInfo typePokemonList = await _typeService.GetFromApi<ListPaginationInfo>("/");

            foreach(var type in typePokemonList.Results)
            {
                aux.Add(type.Name.ToUpper());
            }

            ObservableTypeList = new ObservableCollection<string>(aux);
        }
        private async Task TypeSearchCommannd()
        {
            string requesturl = UrlConfiguration.BaseUrl() + "type" + "/normal/";
            var a = await requesturl.GetJsonAsync<PokemonType>();



            //IEnumerable<Pokemon> pokemonList = await _typeService.GetFromApi<IEnumerable<Pokemon>>($"/normal/");

            //IEnumerable<Pokemon> pokemonList = await _typeService.GetFromApi<IEnumerable<Pokemon>>($"/{Pesquisa.ToLower()}/");
            int b = 2;
        }

        private async Task PokemonDetailsAsync(Pokemon pokemon)
        {
            await CoreMethods.PushPopupPageModel<PokemonDetailPopUpPageModel>(pokemon);
        }
    }
}
