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
    public class TypeService : BaseService, ITypeService
    {
        public TypeService() : base("type")
        {

        }

        public async Task<ListPaginationInfo> GetListType()
        {
            try
            {
                string requesturl = $@"{UrlConfiguration.BaseUrl()}/{Controller}";
                return await requesturl.GetJsonAsync<ListPaginationInfo>();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PokemonType> GetListPokemonType(string type)
        {
            try
            {
                string requesturl = $@"{UrlConfiguration.BaseUrl()}/{Controller}/{type.ToLower()}";
                return await requesturl.GetJsonAsync<PokemonType>();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
