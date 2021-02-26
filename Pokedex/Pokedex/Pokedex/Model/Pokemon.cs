using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Model
{
    public class Pokemon : Base
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public IEnumerable<Types> Types { get; set; }
        public Sprites Sprites { get; set; }

        [JsonProperty("pokemon")]
        public Pokemon PokemonAux { get; set; }
        public string UrlPhoto => Sprites.Front_Default;
        public string IsFavorite { get; set; }
        public int Count { get; set; }

    }
    public class Sprites
    {
        public string Back_Default { get; set; }
        public string Back_Female { get; set; }
        public string Back_Shiny { get; set; }
        public string Back_Shiny_Female { get; set; }
        public string Front_Default { get; set; }
        public string Front_Female { get; set; }
        public string Front_Shiny { get; set; }
        public string Front_Shiny_Female { get; set; }
    }
}
