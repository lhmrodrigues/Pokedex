using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Configuration
{
    public static class UrlConfiguration
    {
        public static string BaseUrl() => "https://pokeapi.co/api/v2";
        public static int OffSet => 20;
    }
}
