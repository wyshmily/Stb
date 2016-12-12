using Stb.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Data.Comparer
{
    public class WorkLoadComparer : IEqualityComparer<WorkLoad>
    {
        public bool Equals(WorkLoad x, WorkLoad y)
        {
            return x.OrderId == y.OrderId && x.JobMeasurementId == y.JobMeasurementId && x.WorkerId == y.WorkerId;
        }

        public int GetHashCode(WorkLoad obj)
        {
            return obj.Id;
        }
    }
}
