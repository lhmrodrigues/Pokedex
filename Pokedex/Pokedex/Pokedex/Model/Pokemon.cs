using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Model
{
    [Table("Pokemon")]
    public class Pokemon : Base
    {
        [Ignore]
        public string Name { get; set; }
        [Ignore]
        public string Url { get; set; }
        [Ignore]
        public int Height { get; set; }
        [Ignore]
        public int Weight { get; set; }
        [Ignore]
        public IEnumerable<Types> Types { get; set; }
        [Ignore]
        public Sprites Sprites { get; set; }
        [Ignore]
        public PhotoAux AuxPhotos { get; set; }
        [Ignore]
        [JsonProperty("pokemon")]
        public Pokemon PokemonAux { get; set; }
        [Ignore]
        public string UrlPhoto => Sprites.Front_Default;
        [Ignore]
        public string IsFavorite { get; set; }
        [Ignore]
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
