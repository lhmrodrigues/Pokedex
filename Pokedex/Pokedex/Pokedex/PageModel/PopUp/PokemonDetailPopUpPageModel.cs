using Pokedex.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Pokedex
{
    public class PokemonDetailPopUpPageModel: BasePageModel
    {
        public Pokemon SelectedPokemon { get; set; }
        public ObservableCollection<PhotoAux> PhotoList { get; set; }


        public string Type { get; set; }
        public override async void Init(object initData)
        {
            SelectedPokemon = (Pokemon)initData;
            SetPhotosAtList();
            Type = SelectedPokemon.Types.ToList()[0].Type.Name.ToUpper();
        }

        private void SetPhotosAtList()
        {
            List<PhotoAux> listAux = new List<PhotoAux>();
            Sprites sprites = SelectedPokemon.Sprites;


            if (!string.IsNullOrEmpty(sprites.Front_Default))
                listAux.Add(new PhotoAux
                {
                    Photo = sprites.Front_Default
                });

            if (!string.IsNullOrEmpty(sprites.Back_Default))
                listAux.Add(new PhotoAux
                {
                    Photo = sprites.Back_Default
                });

            if (!string.IsNullOrEmpty(sprites.Back_Female))
                listAux.Add(new PhotoAux
                {
                    Photo = sprites.Back_Female
                });

            if (!string.IsNullOrEmpty(sprites.Back_Shiny))
                listAux.Add(new PhotoAux
                {
                    Photo = sprites.Back_Shiny
                });

            if (!string.IsNullOrEmpty(sprites.Back_Shiny_Female))
                listAux.Add(new PhotoAux
                {
                    Photo = sprites.Back_Shiny_Female
                });

            if (!string.IsNullOrEmpty(sprites.Front_Female))
                listAux.Add(new PhotoAux
                {
                    Photo = sprites.Front_Female
                });

            if (!string.IsNullOrEmpty(sprites.Front_Shiny))
                listAux.Add(new PhotoAux
                {
                    Photo = sprites.Front_Shiny
                });

            if (!string.IsNullOrEmpty(sprites.Front_Shiny_Female))
                listAux.Add(new PhotoAux
                {
                    Photo = sprites.Front_Shiny_Female
                });

            PhotoList = new ObservableCollection<PhotoAux>(listAux);
        }
    }
}
