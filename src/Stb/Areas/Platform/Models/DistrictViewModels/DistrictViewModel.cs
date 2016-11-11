using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Platform.Models.DistrictViewModels
{
    public class DistrictViewModel
    {
        public int ProvinceAdcode { get; set; }

        public string ProvinceName { get; set; }

        public int CityAdcode { get; set; }

        public string CityName { get; set; }

        public int DistrictAdcode { get; set; }

        public string DistrictName { get; set; }
    }
}
