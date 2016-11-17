using Stb.Data.Models;
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

        public DistrictViewModel()
        {

        }

        public DistrictViewModel(District district)
        {
            DistrictAdcode = district.Id;
            DistrictName = district.Name;
            CityAdcode = district.CityId;
            CityName = district.CityName;
            ProvinceAdcode = district.ProvinceId;
            ProvinceName = district.ProvinceName;
        }

        public District ToDistrict()
        {
            return new District
            {
                Id = DistrictAdcode,
                Name = DistrictName,
                CityId = CityAdcode,
                CityName = CityName,
                ProvinceId = ProvinceAdcode,
                ProvinceName = ProvinceName,
            };
        }

        public City ToCity()
        {
            return new City
            {
                Id = CityAdcode,
                Name = CityName,
                ProvinceId = ProvinceAdcode,
                ProvinceName = ProvinceName,
            };
        }

        public Province ToProvince()
        {
            return new Province
            {
                Id = ProvinceAdcode,
                Name = ProvinceName,
            };
        }
    }
}
