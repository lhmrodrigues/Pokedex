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

        public async Task<ListPaginationInfo> GetListRange(int index)
        {
            try
            {
                string requesturl = $@"{UrlConfiguration.BaseUrl()}/{Controller}/?limit={UrlConfiguration.OffSet}&offset={index}";
                return await requesturl.GetJsonAsync<ListPaginationInfo>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Pokemon> GetPokemon(string pokemon)
        {
            try
            {
                string requesturl = $@"{UrlConfiguration.BaseUrl()}/{Controller}/{pokemon}";
                return await requesturl.GetJsonAsync<Pokemon>();
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
