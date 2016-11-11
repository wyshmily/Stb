using Stb.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Data.Comparer
{
    public class EndUserDistrictComparer : IEqualityComparer<EndUserDistrict>
    {
        public bool Equals(EndUserDistrict x, EndUserDistrict y)
        {
            return x.EndUserId == y.EndUserId && x.DistrictId == y.DistrictId;
        }

        public int GetHashCode(EndUserDistrict obj)
        {
            return obj.DistrictId;
        }
    }
}
