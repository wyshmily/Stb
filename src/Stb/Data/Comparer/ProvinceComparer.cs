using Stb.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Data.Comparer
{
    public class ProvinceComparer : IEqualityComparer<Province>
    {
        public bool Equals(Province x, Province y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Province obj)
        {
            return obj.Id;
        }
    }
}
