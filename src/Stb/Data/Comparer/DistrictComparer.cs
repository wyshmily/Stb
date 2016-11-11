using Stb.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Data.Comparer
{
    public class DistrictComparer : IEqualityComparer<District>
    {
        public bool Equals(District x, District y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(District obj)
        {
            return obj.Id;
        }
    }
}
