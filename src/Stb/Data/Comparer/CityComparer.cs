using Stb.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Data.Comparer
{
    public class CityComparer : IEqualityComparer<City>
    {
        public bool Equals(City x, City y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(City obj)
        {
            return obj.Id;
        }
    }
}
