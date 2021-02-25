using Pokedex.Interface;
using Pokedex.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Services
{
    public class TypeService : BaseService, ITypeService
    {
        public TypeService() : base("type")
        {

        }
    }
}
