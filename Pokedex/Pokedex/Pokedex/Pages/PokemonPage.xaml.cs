using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pokedex
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PokemonPage : ContentPage
    {
        public PokemonPage()
        {
            InitializeComponent();
            listPokemon.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            var imageButton = sender as Xamarin.Forms.ImageButton;
            string a = imageButton.Source.ToString();

            if (a == "File: star.png")
                imageButton.Source = "starYellow.png";
            else
                imageButton.Source = "star.png";

        }
    }
}