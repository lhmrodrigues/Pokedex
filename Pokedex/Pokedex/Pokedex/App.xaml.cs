using FreshMvvm;
using Pokedex.Interface;
using Pokedex.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pokedex
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            InitializeIoC();
            var page = FreshPageModelResolver.ResolvePageModel<PokemonPageModel>();
            var navegacao = new FreshNavigationContainer(page)
            {                
                BarBackgroundColor = Color.FromHex("#e61919"),
                BarTextColor = Color.White
            };
            MainPage = navegacao;
        }

        void InitializeIoC()
        {
            FreshIOC.Container.Register<IPokemonService, PokemonService>();
            FreshIOC.Container.Register<ITypeService, TypeService>();
        }


        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
