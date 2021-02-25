using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Interface
{
    public interface IBaseService
    {
        Task<T> GetFromApi<T>(string url) where T : class;
    }
}
