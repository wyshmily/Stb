using Stb.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Data.Comparer
{
    public class EndUserJobClassComparer : IEqualityComparer<EndUserJobClass>
    {
        public bool Equals(EndUserJobClass x, EndUserJobClass y)
        {
            return x.EndUserId == y.EndUserId && x.JobClassId == y.JobClassId;
        }

        public int GetHashCode(EndUserJobClass obj)
        {
            return obj.JobClassId;
        }
    }
}
