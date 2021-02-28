using Flurl.Http;
using Pokedex.Configuration;
using Pokedex.Interface;
using Pokedex.Model;
using Pokedex.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Services
{
    public class PokemonService : BaseService, IPokemonService
    {

        public PokemonService() : base("pokemon")
        {

        }

    }
}
