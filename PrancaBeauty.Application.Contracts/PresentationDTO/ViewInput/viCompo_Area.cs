using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrancaBeauty.Application.Contracts.PresentationDTO.ViewInput
{
    public class viCompo_Area
    {
        public string CountryId { get; set; }
        public string CountyFieldName { get; set; }

        public string ProvinceId { get; set; }
        public string ProvinceFieldName { get; set; }

        public string CityId { get; set; }
        public string CityFieldName { get; set; }
    }
}
