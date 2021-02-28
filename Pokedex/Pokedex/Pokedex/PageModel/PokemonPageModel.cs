using Flurl.Http;
using Pokedex.Configuration;
using Pokedex.Interface;
using Pokedex.Model;
using Pokedex.Util;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        public ObservableCollection<string> ObservableTypeList { get; set; }
        public ObservableCollection<Pokemon> ObservablePokemonList { get; set; }
        public List<Results> PokemonFilterList { get; set; }
        public List<Pokemon> PokemonCache { get; set; }
        public bool IsFilter { get; set; } = false;
        public string TextInfo { get; set; }
        public string Pesquisa { get; set; }
        public int TotalPokemon { get; set; }
        public int IndexAtual { get; set; }
        public int IndexNext { get; set; }


        public ICommand nextCommannd => new Command(async () => await NextCommannd());
        public ICommand previousCommannd => new Command(async () => await PreviousCommannd());
        public ICommand typeSearchCommannd => new Command(async () => await TypeSearchCommannd());
        public ICommand pokemmonDetails => new Command<Pokemon>(async (pokemonDetails) => await PokemonDetailsAsync(pokemonDetails));
        public ICommand starPokemon => new Command<Pokemon>(async (pokemonDetails) => await StarPokemon(pokemonDetails));

        public PokemonPageModel(IPokemonService pokemonService,
            ITypeService typeService)
        {
            _pokemonService = pokemonService;
            _typeService = typeService;
        }
        public override async void Init(object initData)
        {
            IndexAtual = 0;
            IndexNext = 20;
            SetTypeList();
            SearchRangeAndSetValues();
        }
        public async void SetTypeList()
        {
            try
            {
                List<string> aux = new List<string>();
                ListPaginationInfo typePokemonList = await _typeService.GetFromApi<ListPaginationInfo>(string.Empty);

                aux.Add("Select Type");
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
        public async void SearchRangeAndSetValues()
        {
            try
            {
                var resultRange = await _pokemonService.GetFromApi<ListPaginationInfo>($"?limit={UrlConfiguration.OffSet}&offset={IndexAtual}");

                SetPokemonsOnList(resultRange.Results);
                TotalPokemon = resultRange.Count;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        private async void SetPokemonsOnList(IEnumerable<Results> listPaginationInfo)
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                PokemonCache = Db.Table<Pokemon>().ToList();

                TextInfo = $"{IndexAtual + 1} to {IndexNext}";

                List<Pokemon> pokemonList = new List<Pokemon>();

                foreach (var results in listPaginationInfo)
                {
                    Pokemon pokemon = await _pokemonService.GetFromApi<Pokemon>($"{results.Name}");
                    pokemon.Name = pokemon.Name.ToUpper();
                    pokemon.Url = pokemon.Sprites.Front_Default;

                    if (PokemonCache.Where(x => x.Id == pokemon.Id).Any())
                        pokemon.IsFavorite = "starYellow.png";
                    else
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
        public async void GetListPokemonType()
        {
            PokemonFilterList = new List<Results>();

            PokemonType pokemonType = await _typeService.GetFromApi<PokemonType>($"{Pesquisa.ToLower()}");

            TotalPokemon = pokemonType.Pokemon.Count();

            foreach (var item in pokemonType.Pokemon)
            {
                PokemonFilterList.Add(new Results
                {
                    Name = item.PokemonAux.Name,
                    Url = item.PokemonAux.Url

                });

            }

            SetPokemonsOnList(PokemonFilterList.GetRange(IndexAtual, IndexNext));
        }
        private async Task TypeSearchCommannd()
        {
            if (IsBusy) return;
            try
            {

                IsBusy = true;
                IndexAtual = 0;
                IndexNext = 20;
                if (Pesquisa == "Select Type")
                {
                    IsFilter = false;
                    SearchRangeAndSetValues();
                    return;
                }

                IsFilter = true;
                GetListPokemonType();
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
        private async Task StarPokemon(Pokemon pokemon)
        {
            if (pokemon.IsFavorite == "star.png")
            {
                pokemon.IsFavorite = "starYellow.png";
                Db.Insert(pokemon);
            }
            else
            {
                pokemon.IsFavorite = "star.png";
                Db.Delete(pokemon);
            }
        }
        private async Task NextCommannd()
        {
            int result = 20;
            if (IndexNext > TotalPokemon)
            {
                IndexAtual = TotalPokemon;
                return;
            }

            IndexAtual += 20;
            IndexNext += 20;
            if (IsFilter)
            {
                if (IndexAtual + 20 > TotalPokemon)
                    result = TotalPokemon - IndexAtual;

                SetPokemonsOnList(PokemonFilterList.GetRange(IndexAtual, result));
            }
            else
            {
                SearchRangeAndSetValues();
            }
        }
        private async Task PreviousCommannd()
        {
            int result = 20;

            if (IndexAtual <= 0)
            {
                IndexAtual = 0;
                return;
            }

            IndexAtual -= 20;
            IndexNext -= 20;
            if (IsFilter)
            {
                SetPokemonsOnList(PokemonFilterList.GetRange(IndexAtual, 20));
            }
            else
            {
                SearchRangeAndSetValues();
            }
        }
        private async Task PokemonDetailsAsync(Pokemon pokemon)
        {
            await CoreMethods.PushPopupPageModel<PokemonDetailPopUpPageModel>(pokemon);
        }
    }
}
