using Pokedex.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Interface
{
    public interface ITypeService : IBaseService
    {
        Task<ListPaginationInfo> GetListType();
        Task<PokemonType> GetListPokemonType(string type);
    }
}
