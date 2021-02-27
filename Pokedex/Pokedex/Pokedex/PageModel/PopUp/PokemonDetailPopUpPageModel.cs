using Pokedex.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pokedex
{
    public class PokemonDetailPopUpPageModel: BasePageModel
    {
        public Pokemon SelectedPokemon { get; set; }
        public string Type { get; set; }
        public override async void Init(object initData)
        {
            SelectedPokemon = (Pokemon)initData;

            Type = SelectedPokemon.Types.ToList()[0].Type.Name.ToUpper();
        }

        private void SetPhotosAtList()
        {

        }
    }
}
