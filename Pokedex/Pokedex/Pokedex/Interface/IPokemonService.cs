using Pokedex.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Interface
{
    public interface IPokemonService : IBaseService
    {
        Task<ListPaginationInfo> GetListRange(int index);
        Task<Pokemon> GetPokemon(string pokemon);
    }
}
