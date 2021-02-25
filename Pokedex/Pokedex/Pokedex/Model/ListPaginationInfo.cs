using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Model
{
    public class ListPaginationInfo
    {
        public int Count { get; set; }
        public string Previous { get; set; }
        public IEnumerable<Results> Results { get; set; }
    }
    public class Results
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

}
